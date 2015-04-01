using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DriveSync
{
	public enum SyncState
	{
		Analyzing = 0,
		Ok = 1,
		Off = 2,
		InProcess = 3,
		WaitSolution = 4,
		WaitProcess = 5,
		NotAvailable = 6,
		UnSuccess = 7
	}

	public enum SyncMode
	{
		NoneNync,
		AToB,
		BToA
	}

	static class StateStrings
	{
		public static readonly string[] Items = 
		{
			"Анализируется...", 
			"Готово", 
			"Отключено", 
			"Синхронизируется...",
 			"Выберите направление",
			"В очереди на синхронизацию",
			"Один из каталогов недоступен",
			"Не все файлы синхронизировались"
		};
	}

	static class ModeStrings
	{
		public static readonly string[] Items = 
		{
			"Не синхронизировать",
			"А -> Б", 
			"Б -> А"
		};
	}
	public class SyncDirs
	{
		bool remA, remB;
		string pathA;
		string pathB;
		string markA;
		string markB;
		bool selected;
		bool needShow;
		bool needSync;
		bool autoSync;
		bool allowAutoSync;
		bool success;
		SyncState state;
		FileSystemWatcher fileWatcherA;
		FileSystemWatcher fileWatcherB;
		Dictionary<string, SyncMode> syncList;
		string progressMess;
		double progressPercent;
		private AutoResetEvent _resetEvent = new AutoResetEvent(false);

		public double ProgressPercent
		{
			get { return progressPercent; }
		}

		public string ProgressMess
		{
			get { return progressMess; }
		}

		internal Dictionary<string, SyncMode> SyncList
		{
			get { return syncList; }
		}

		public bool AllowAutoSync
		{
			get { return allowAutoSync; }
			set
			{
				if (value && value != allowAutoSync && state == SyncState.Off)
					state = SyncState.Analyzing;
				allowAutoSync = value;
			}
		}

		public bool AutoSync
		{
			get { return autoSync; }
			set { autoSync = value; }
		}

		public string MarkA
		{
			get { return markA; }
		}

		public string MarkB
		{
			get { return markB; }
		}

		public bool NeedSync
		{
			get { return needSync; }
		}

		BackgroundWorker backgroundSync;

		public string PathA
		{
			get { return pathA; }
			set
			{
				if (pathA != value)
				{
					if (fileWatcherA != null)
						fileWatcherA.Dispose();
					fileWatcherA = null;
				}
				needSync = true;
				pathA = value;
				if (pathA.Length > 0 && ((pathA[0] <= 'z' && pathA[0] >= 'a') || (pathA[0] <= 'Z' && pathA[0] >= 'A')))
				{
					DriveInfo drive = new DriveInfo(pathA.Substring(0, 1));
					if (drive.IsReady)
						markA = drive.VolumeLabel;
				}
			}
		}

		public string PathB
		{
			get { return pathB; }
			set
			{
				if (pathB != value)
				{
					if (fileWatcherB != null)
						fileWatcherB.Dispose();
					fileWatcherB = null;
				}
				needSync = true;
				pathB = value;
				if (pathB.Length > 0 && ((pathB[0] <= 'z' && pathB[0] >= 'a') || (pathB[0] <= 'Z' && pathB[0] >= 'A')))
				{
					DriveInfo drive = new DriveInfo(pathB.Substring(0, 1));
					if (drive.IsReady)
						markB = drive.VolumeLabel;
				}
			}
		}

		public SyncState State
		{
			get
			{
				return state;
			}
			set
			{
				state = value;
				switch (state)
				{
					case SyncState.Analyzing:
						needSync = true;
						break;
					case SyncState.Off:
					case SyncState.NotAvailable:
						if (fileWatcherA != null)
							fileWatcherA.Dispose();
						if (fileWatcherB != null)
							fileWatcherB.Dispose();
						fileWatcherA = null;
						fileWatcherB = null;
						autoSync = false;
						syncList.Clear();
						break;
					case SyncState.Ok:
					case SyncState.UnSuccess:
						syncList.Clear();
						autoSync = true;
						if (fileWatcherA == null && AllowAutoSync)
						{
							fileWatcherA = new FileSystemWatcher(pathA);
							fileWatcherA.Changed += new FileSystemEventHandler(dirChanged);
							fileWatcherA.Created += new FileSystemEventHandler(dirChanged);
							fileWatcherA.Renamed += new RenamedEventHandler(dirRenamed);
							fileWatcherA.Deleted += new FileSystemEventHandler(dirDeleted);
							fileWatcherA.NotifyFilter = NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastWrite;

							fileWatcherA.IncludeSubdirectories = true;
							fileWatcherA.EnableRaisingEvents = true;
						}
						if (fileWatcherB == null && AllowAutoSync)
						{
							fileWatcherB = new FileSystemWatcher(pathB);
							fileWatcherB.Changed += new FileSystemEventHandler(dirChanged);
							fileWatcherB.Created += new FileSystemEventHandler(dirChanged);
							fileWatcherB.Renamed += new RenamedEventHandler(dirRenamed);
							fileWatcherB.Deleted += new FileSystemEventHandler(dirDeleted);
							fileWatcherB.NotifyFilter = NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastWrite;

							fileWatcherB.IncludeSubdirectories = true;
							fileWatcherB.EnableRaisingEvents = true;
						}
						break;
				}
			}
		}

		public bool Selected
		{
			get { return selected; }
			set { selected = value; }
		}

		public string StateString
		{
			get
			{
				if (state == SyncState.InProcess && progressMess == "")
					return StateStrings.Items[(int)SyncState.Analyzing];
				else
					return StateStrings.Items[(int)state];
			}
		}

		public bool NeedShow
		{
			get
			{
				bool buf = needShow;
				needShow = false;
				return buf;
			}
			set { needShow = value; }
		}

		void dirChanged(object sender, FileSystemEventArgs e)
		{
			if (e.FullPath.StartsWith(pathA) && !e.FullPath.StartsWith(pathB))
				remA = true;
			else
				remB = true;
			state = SyncState.Analyzing;
			needSync = true;
			if (fileWatcherA != null)
			{
				fileWatcherA.Dispose();
				fileWatcherA = null;
			}
			if (fileWatcherB != null)
			{
				fileWatcherB.Dispose();
				fileWatcherB = null;
			}
		}

		void dirDeleted(object sender, FileSystemEventArgs e)
		{
			if (e.FullPath.StartsWith(pathA) && !e.FullPath.StartsWith(pathB))
				remA = true;
			else
				remB = true;
			state = SyncState.Analyzing;
			needSync = true;
			if (fileWatcherA != null)
			{
				fileWatcherA.Dispose();
				fileWatcherA = null;
			}
			if (fileWatcherB != null)
			{
				fileWatcherB.Dispose();
				fileWatcherB = null;
			}
		}

		void dirRenamed(object sender, RenamedEventArgs e)
		{
			if (e.OldFullPath.StartsWith(pathA) && !e.OldFullPath.StartsWith(pathB))
				remA = true;
			else
				remB = true;
			state = SyncState.Analyzing;
			needSync = true;
			if (fileWatcherA != null)
			{
				fileWatcherA.Dispose();
				fileWatcherA = null;
			}
			if (fileWatcherB != null)
			{
				fileWatcherB.Dispose();
				fileWatcherB = null;
			}
		}

		public bool DirsExists()
		{
			if (Directory.Exists(pathA) && Directory.Exists(pathB))
				return true;
			else
				return false;
		}

		public SyncDirs()
		{
			progressMess = "";
			progressPercent = -1;
			state = SyncState.Ok;
			remA = false;
			remB = false;
			selected = false;
			needShow = false;
			needSync = true;
			autoSync = false;
			allowAutoSync = true;
			syncList = new Dictionary<string, SyncMode>();
			backgroundSync = new BackgroundWorker();
			backgroundSync.WorkerSupportsCancellation = true;
			backgroundSync.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundSync_DoWork);
			backgroundSync.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundSync_RunWorkerCompleted);
			backgroundSync.ProgressChanged += new ProgressChangedEventHandler(reportProgress);
			backgroundSync.WorkerReportsProgress = true;
		}

		public SyncDirs(string pathA, string pathB, SyncState state, bool allowAutoSync)
			: this()
		{
			PathA = pathA;
			PathB = pathB;
			if (!allowAutoSync && state == SyncState.Off)
				state = SyncState.Analyzing;
			this.state = state;
			this.allowAutoSync = allowAutoSync;
		}

		public SyncDirs(string pathA, string pathB, string markA, string markB, SyncState state, bool allowAutoSync)
			: this(pathA, pathB, state, allowAutoSync)
		{
			this.markA = markA;
			this.markB = markB;
		}

		public static bool IsValidPaths(string pathA, string pathB, ref string errMess)
		{
			errMess = null;
			if (pathA == "")
				errMess = "Выберите каталог А";
			else if (pathB == "")
				errMess = "Выберите каталог Б";
			else
			{
				if (pathA[pathA.Length - 1] == '\\')
					pathA = pathA.Remove(pathA.Length - 1, 1);
				if (pathB[pathB.Length - 1] == '\\')
					pathB = pathB.Remove(pathB.Length - 1, 1);
				if (!Directory.Exists(pathA))
					errMess = "Такого пути не существует! (Каталог А)";
				else if (!Directory.Exists(pathB))
					errMess = "Такого пути не существует! (Каталог Б)";
				else if (Path.Equals(pathA, pathB))
					errMess = "Нет смысла синхронизировать каталог сам с собой...";
				else if (pathA.StartsWith(pathB) && pathA.Substring(pathB.Length).Contains("\\"))
					errMess = "Не надо пытаться синхронизировать корневой каталог с его дочерними...";
				else if (pathB.StartsWith(pathA) && pathB.Substring(pathA.Length).Contains("\\"))
					errMess = "Не надо пытаться синхронизировать корневой каталог с его дочерними...";
			}
			return (errMess == null);
		}

		private void backgroundSync_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			needShow = true;
			needSync = false;
			progressMess = "";
			progressPercent = -1;
			try
			{
				if (!success && File.Exists(CustomFileCopier.lastCopiedFile))
					File.Delete(CustomFileCopier.lastCopiedFile);
			}
			catch (Exception)
			{
			}
			_resetEvent.Set();
		}

		public void Cancel()
		{
			success = false;
			_resetEvent.WaitOne();	
			try
			{
				if (File.Exists(CustomFileCopier.lastCopiedFile))
					File.Delete(CustomFileCopier.lastCopiedFile);
			}
			catch (Exception)
			{ }
		}
		public void Analyze()
		{
			State = SyncState.InProcess;
			if (!backgroundSync.IsBusy)
				backgroundSync.RunWorkerAsync();
		}

		private void backgroundSync_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			string fileSystemEntry;
			FileInfo fileA;
			FileInfo fileB;
			SyncMode mode;
			if (syncList.Count == 0)
			{
				foreach (string currentFile in Directory.EnumerateFileSystemEntries(pathA))
				{
					fileSystemEntry = currentFile.Substring(pathA.Length);
					if (fileSystemEntry == "\\" + MainForm.FileRemConf ||
							fileSystemEntry.StartsWith("\\~"))
						continue;
					fileA = new FileInfo(pathA + fileSystemEntry);
					fileB = new FileInfo(pathB + fileSystemEntry);
					if (Directory.Exists(pathB + fileSystemEntry))
					{
						int cmp = DirsCompare(fileA.FullName, fileB.FullName);
						if (cmp != 0)
						{
							if (cmp>0)
								mode = SyncMode.AToB;
							else
								mode = SyncMode.BToA;
							if (remA)
								mode = SyncMode.AToB;
							if (remB)
								mode = SyncMode.BToA;
							syncList.Add(fileSystemEntry, mode);
						}
					}
					else if (!File.Exists(pathB + fileSystemEntry) ||
						fileA.LastWriteTime != fileB.LastWriteTime)
					{
						mode = SyncMode.AToB;
						if (remB)
							mode = SyncMode.BToA;
						if (remA)
							mode = SyncMode.AToB;
						syncList.Add(fileSystemEntry, mode);
					}
				}
				foreach (string currentFile in Directory.EnumerateFileSystemEntries(pathB))
				{
					fileSystemEntry = currentFile.Substring(pathB.Length);
					if (fileSystemEntry == "\\" + MainForm.FileRemConf ||
							fileSystemEntry.StartsWith("\\~"))
						continue;
					if (!Directory.Exists(pathA + fileSystemEntry) && !File.Exists(pathA + fileSystemEntry))
					{
						mode = SyncMode.BToA;
						if (remB)
							mode = SyncMode.BToA;
						if (remA)
							mode = SyncMode.AToB;
						syncList.Add(fileSystemEntry, mode);
					}
				}
			}
			if (syncList.Count > 0)
			{
				needShow = true;
				if (autoSync)
					//k
					Synchronize();
				else
					State = SyncState.WaitSolution;
			}
			else
				State = SyncState.Ok;
			remA = false;
			remB = false;
			needSync = true;
		}

		private static int DirsCompare(string dirNameA, string dirNameB)
		{
			foreach (string fileNameA in Directory.GetDirectories(dirNameA, "*"))
			{
				string fileNameB = fileNameA.Replace(dirNameA, dirNameB);
				if (Directory.Exists(fileNameB))
				{
					int cmpRes = DirsCompare(fileNameA, fileNameB);
					if (cmpRes != 0)
						return cmpRes;
				}
				else
					return 1;
			}
			foreach (string fileNameB in Directory.GetDirectories(dirNameB, "*"))
			{
				string fileNameA = fileNameB.Replace(dirNameB, dirNameA);
				if (!Directory.Exists(fileNameA))
					return 1;
			}
			FileInfo fileA, fileB;
			foreach (string fileName in Directory.GetFiles(dirNameA, "*.*"))
			{
				fileA = new FileInfo(fileName);
				fileB = new FileInfo(fileName.Replace(dirNameA, dirNameB));
				if (fileB.Exists)
				{
					if (fileA.LastWriteTime != fileB.LastWriteTime)
						return fileA.LastWriteTime.CompareTo(fileB.LastWriteTime);
				}
				else
					return 1;
			}
			foreach (string fileName in Directory.GetFiles(dirNameB, "*.*"))
			{
				fileA = new FileInfo(fileName.Replace(dirNameB, dirNameA));
				if (!fileA.Exists)
					return -1;
			}
			return 0;
		}

		private void Synchronize()
		{
			int compliteCount = 0;
			progressMess = " ";
			needShow = true;
			List<string> removeItems = new List<string>();
			foreach (var item in syncList)
			{				
				switch (item.Value)
				{
					case SyncMode.AToB:
						syncDirsProcess(pathA, pathB, item.Key);
						compliteCount++;
						break;
					case SyncMode.BToA:
						syncDirsProcess(pathB, pathA, item.Key);
						compliteCount++;
						break;
					case SyncMode.NoneNync:
						success = false;
						break;
				}
				if (success)
					removeItems.Add(item.Key);
				if (syncList.Count == 0)
					break;
			}
			if (compliteCount == syncList.Count)
			{
				if (AllowAutoSync)
				{
					if (success)
						State = SyncState.Ok;
					else
						State = SyncState.UnSuccess;
				}
				else
					State = SyncState.Off;
			}
			else
				State = SyncState.WaitSolution;
			foreach (string item in removeItems)
				syncList.Remove(item);
		}

		private void syncDirsProcess(string pathA, string pathB, string file)
		{
			success = true;
			try
			{
				if (Directory.Exists(pathA + file))
				{
					Directory.CreateDirectory(pathB + file);
					foreach (string dirPath in Directory.GetDirectories(pathA + file, "*"))
						syncDirsProcess(pathA, pathB, dirPath.Substring(pathA.Length));
					foreach (string dirPath in Directory.GetDirectories(pathB + file, "*"))
					{
						if (!Directory.Exists(dirPath.Replace(pathB, pathA)))
							Directory.Delete(dirPath, true);
					}
					foreach (string fileName in Directory.GetFiles(pathB + file, "*.*"))
					{
						if (!File.Exists(fileName.Replace(pathB, pathA)))
							File.Delete(fileName);
					}
					foreach (string newFile in Directory.GetFiles(pathA + file, "*.*"))
					{
						FileInfo fileA = new FileInfo(newFile);
						FileInfo fileB = new FileInfo(newFile.Replace(pathA, pathB));
						if (!fileB.Exists || fileA.LastWriteTime > fileB.LastWriteTime)
						{
							backgroundSync.ReportProgress(1, fileA.Name);
							CustomFileCopier fileCopier = new CustomFileCopier(newFile, newFile.Replace(pathA, pathB));
							fileCopier.OnProgressChanged += new ProgressChangeDelegate(fileCopier_OnProgressChanged);
							fileCopier.Copy();
							//File.Copy(newFile, newFile.Replace(pathA, pathB), true);
						}
					}
				}
				else
				{
					if (File.Exists(pathA + file))
					{
						backgroundSync.ReportProgress(1, (new FileInfo(pathA + file)).Name);
						CustomFileCopier fileCopier = new CustomFileCopier(pathA + file, pathB + file);
						fileCopier.OnProgressChanged += new ProgressChangeDelegate(fileCopier_OnProgressChanged);
						fileCopier.Copy();
						//File.Copy(pathA + file, pathB + file, true);
					}
					else if (File.Exists(pathB + file))
						File.Delete(pathB + file);
					else
						Directory.Delete(pathB + file, true);
				}
			}
			catch (Exception)
			{
				success = false;
				//System.Windows.Forms.MessageBox.Show(e.GetType().ToString());
			}
		}

		private void reportProgress(object sender, ProgressChangedEventArgs e)
		{
			progressMess = e.UserState.ToString();
		}
		private void fileCopier_OnProgressChanged(double persentage, ref bool cancel)
		{
			if (!success)
				cancel = true;
			progressPercent = persentage;
		}
	}
}