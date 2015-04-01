using DriveSync.Forms;
using DriveSync.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
namespace DriveSync
{
	namespace bf
	{

	}
	public partial class MainForm : Form
	{
		public const string FileRemConf = "SyncDriveConfig.ini";
		[DllImport("user32.dll")]
		static extern int ShowWindow(IntPtr hWnd, uint Msg);
		const uint SW_RESTORE = 0x09;
		DriveDetector driveDetector = null;
		SyncDirsList syncDirs;
		List<int> idRemovableDrives;
		List<string> nameRemovableDrives;
		Form syncForm = null;
		ListViewColumnSorter lvwColumnSorter;
		bool startMinimized;

		public MainForm(bool startMinimized)
		{
			InitializeComponent();
			syncDirs = new SyncDirsList(lVDirs, statusStrip1);
			SettingsForm.MainForm = this;
			SettingsForm.Pointer.LoadSettings();
			idRemovableDrives = SettingsForm.Pointer.IdRemovableDrives;
			nameRemovableDrives = SettingsForm.Pointer.NameRemovableDrives;
			this.startMinimized = startMinimized;
			if (startMinimized)
			{
				ShowInTaskbar = false;
				Opacity = 0;
			}
			for (int i = 0; i < lVDirs.Columns.Count && i < SettingsForm.Pointer.ColSizes.Count; i++)
				lVDirs.Columns[i].Width = (int)SettingsForm.Pointer.ColSizes[i];
			StringCollection pathsA = SettingsForm.Pointer.PathsA;
			StringCollection pathsB = SettingsForm.Pointer.PathsB;
			StringCollection marksA = SettingsForm.Pointer.MarksA;
			StringCollection marksB = SettingsForm.Pointer.MarksB;
			List<SyncState> states = SettingsForm.Pointer.States;
			List<bool> allowAutoSyncs = SettingsForm.Pointer.AllowAutoSyncs;
			for (int i = 0; i < pathsA.Count; i++)
				syncDirs.Add(new SyncDirs(pathsA[i], pathsB[i], marksA[i], marksB[i], states[i], allowAutoSyncs[i]));
			foreach (DriveInfo drive in DriveInfo.GetDrives())
				markRemoveble(drive);
			timerInterfaceUpdate.Enabled = true;
			lvwColumnSorter = new ListViewColumnSorter();
			lVDirs.ListViewItemSorter = lvwColumnSorter;
		}

		private void Form1_Shown(object sender, EventArgs e)
		{
			driveDetector = new DriveDetector();
			driveDetector.DeviceArrived += new DriveDetectorEventHandler(OnDriveArrived);
			if (startMinimized)
			{
				Hide();
				Opacity = 100;
				ShowInTaskbar = true;
			}
		}

		private void OnDriveArrived(object sender, DriveDetectorEventArgs e)
		{
			foreach (DriveInfo drive in DriveInfo.GetDrives())
				markRemoveble(drive);
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (SettingsForm.Pointer.AlowQuestionForClose && Opacity > 0)
			{
				bool alow = false;
				MsgBoxCheck.MessageBox dlg = new MsgBoxCheck.MessageBox();
				DialogResult dr = dlg.Show("Запомнить выбор (можно изменить в настройках)",
																	 "Свернуть в трей вместо закрытия?",
																	 "Закрытие окна программы..." + new String(' ', 25),
																		MessageBoxButtons.YesNo, MessageBoxIcon.Question, ref alow);
				SettingsForm.Pointer.AlowQuestionForClose = !alow;
				SettingsForm.Pointer.AlowClose = dr == DialogResult.No;
			}
			if (!SettingsForm.Pointer.AlowClose)
			{
				Hide();
				e.Cancel = true;
			}
			else
			{
				SettingsForm.Pointer.ColSizes.Clear();
				for (int i = 0; i < lVDirs.Columns.Count; i++)
					SettingsForm.Pointer.ColSizes.Add(lVDirs.Columns[i].Width);
				StringCollection pathsA = new StringCollection();
				StringCollection pathsB = new StringCollection();
				StringCollection marksA = new StringCollection();
				StringCollection marksB = new StringCollection();
				List<SyncState> states = new List<SyncState>();
				List<bool> allowAutoSync = new List<bool>();
				foreach (SyncDirs item in syncDirs)
				{
					pathsA.Add(item.PathA);
					pathsB.Add(item.PathB);
					marksA.Add(item.MarkA);
					marksB.Add(item.MarkB);
					states.Add(item.State);
					allowAutoSync.Add(item.AllowAutoSync);
				}
				SettingsForm.Pointer.PathsA = pathsA;
				SettingsForm.Pointer.PathsB = pathsB;
				SettingsForm.Pointer.MarksA = marksA;
				SettingsForm.Pointer.MarksB = marksB;
				SettingsForm.Pointer.States = states;
				SettingsForm.Pointer.AllowAutoSyncs = allowAutoSync;
				SettingsForm.Pointer.IdRemovableDrives = idRemovableDrives;
				SettingsForm.Pointer.NameRemovableDrives = nameRemovableDrives;
				if (syncForm != null)
					syncForm.Close();
				SettingsForm.Pointer.SaveSettings();
				if (syncDirs.CurrentTask.State == SyncState.InProcess)
					syncDirs.CurrentTask.Cancel();
				SettingsForm.Pointer.Close();
			}
		}

		private void tSMIExit_Click(object sender, EventArgs e)
		{
			Opacity = 0;
			SettingsForm.Pointer.AlowClose = true;
			Close();
		}

		private void tSMISettings_Click(object sender, EventArgs e)
		{
			SettingsForm.Pointer.Show();
			SettingsForm.Pointer.Activate();
		}

		private void btCancelAdd_Click(object sender, EventArgs e)
		{
			tBPathA.Text = "";
			tBPathB.Text = "";
			gBAddSync.Visible = false;
			//statusStrip1.Visible = false;
			gBCommandsSync.Visible = true;
			lVDirs.Enabled = true;
			lVDirs.Focus();
		}

		private void btAddSync_Click(object sender, EventArgs e)
		{
			btApplyAdd.Visible = true;
			btApplyEdit.Visible = false;
			gBAddSync.Visible = true;
			//statusStrip1.Visible = true;
			gBCommandsSync.Visible = false;
			lVDirs.Enabled = false;
			cBAutoSync.Checked = true;
		}

		private void btReviewA_Click(object sender, EventArgs e)
		{
			folderBrowserDialog.SelectedPath = tBPathA.Text;
			DialogResult result = folderBrowserDialog.ShowDialog();
			if (result == DialogResult.OK)
				tBPathA.Text = folderBrowserDialog.SelectedPath;
		}

		private void btReviewB_Click(object sender, EventArgs e)
		{
			folderBrowserDialog.SelectedPath = tBPathB.Text;
			DialogResult result = folderBrowserDialog.ShowDialog();
			if (result == DialogResult.OK)
				tBPathB.Text = folderBrowserDialog.SelectedPath;
		}

		private void btApplyAdd_Click(object sender, EventArgs e)
		{
			string errMess = null;
			SyncDirs syncDirs = new SyncDirs(tBPathA.Text, tBPathB.Text, SyncState.Analyzing, cBAutoSync.Checked);

			if (SyncDirs.IsValidPaths(tBPathA.Text, tBPathB.Text, ref errMess))
			{
				foreach (SyncDirs item in this.syncDirs)
				{
					if (item.PathA == tBPathA.Text && item.PathB == tBPathB.Text ||
							item.PathB == tBPathA.Text && item.PathA == tBPathB.Text)
					{
						errMess = "Данная пара каталогов уже есть в списке синхронизации!";
						break;
					}
				}
				if (errMess == null && wellBeCycle(tBPathA.Text, tBPathB.Text))
					errMess = "Синхронизация данных каталогов приведет к зацикливанию!";
				if (errMess == null)
					this.syncDirs.Add(syncDirs);
			}
			if (errMess != null)
			{
				tSSLabel.Text = errMess;
				tSSIcon.Visible = true;
			}
			else
			{
				DriveInfo driveA = new DriveInfo(tBPathA.Text[0].ToString());
				DriveInfo driveB = new DriveInfo(tBPathB.Text[0].ToString());
				markRemoveble(driveA, true);
				markRemoveble(driveB, true);
				btCancelAdd_Click(sender, e);
			}
			lVDirs.Focus();
		}

		private void btApplyEdit_Click(object sender, EventArgs e)
		{
			string errMess = null;
			SyncDirs curItem = (SyncDirs)lVDirs.SelectedItems[0].Tag;
			if (!((curItem.PathA == tBPathA.Text && curItem.PathB == tBPathB.Text) ||
						(curItem.PathB == tBPathA.Text && curItem.PathA == tBPathB.Text))
				 && SyncDirs.IsValidPaths(tBPathA.Text, tBPathB.Text, ref errMess))
			{
				foreach (SyncDirs item in syncDirs)
				{
					if (item != curItem && (
							item.PathA == tBPathA.Text && item.PathB == tBPathB.Text ||
							item.PathB == tBPathA.Text && item.PathA == tBPathB.Text))
					{
						errMess = "Данная пара каталогов уже есть в списке синхронизации!";
						break;
					}
				}
				if (wellBeCycle(tBPathA.Text, tBPathB.Text))
					errMess = "Синхронизация данных каталогов приведет к зацикливанию!";
			}
			if (errMess != null)
			{
				tSSLabel.Text = errMess;
				tSSIcon.Visible = true;
				syncDirs.StateChanged = true;
			}
			else
			{
				curItem.AllowAutoSync = cBAutoSync.Checked;
				curItem.PathA = tBPathA.Text;
				curItem.PathB = tBPathB.Text;
				DriveInfo driveA = new DriveInfo(tBPathA.Text[0].ToString());
				DriveInfo driveB = new DriveInfo(tBPathB.Text[0].ToString());
				markRemoveble(driveA, true);
				markRemoveble(driveB, true);
				btCancelAdd_Click(sender, e);
			}
			lVDirs.Focus();
		}

		bool isSubDir(string dirA, string dirB)
		{
			if (dirA == dirB)
				return true;
			else if (dirA.Length > dirB.Length)
			{
				if (dirA.StartsWith(dirB) && dirA.Substring(dirB.Length).Contains("\\"))
					return true;
			}
			else
			{
				if (dirB.StartsWith(dirA) && dirB.Substring(dirA.Length).Contains("\\"))
					return true;
			}
			return false;
		}

		bool wellBeCycle(string pathA, string pathB, List<SyncDirs> tackedItems = null)
		{
			if (tackedItems == null)
				tackedItems = new List<SyncDirs>();
			foreach (SyncDirs item in syncDirs)
			{
				if (tackedItems.Contains(item))
					continue;
				tackedItems.Add(item);
				if ((isSubDir(pathA, item.PathA) && isSubDir(pathB, item.PathB)) ||
						(isSubDir(pathA, item.PathB) && isSubDir(pathB, item.PathA)))
					return true;
				if (isSubDir(pathA, item.PathA) && wellBeCycle(item.PathB, pathB, new List<SyncDirs>(tackedItems)))
					return true;
				if (isSubDir(pathA, item.PathB) && wellBeCycle(item.PathA, pathB, new List<SyncDirs>(tackedItems)))
					return true;
				tackedItems.Remove(item);
			}
			return false;
		}

		void markRemoveble(DriveInfo drive, bool append = false)
		{
			StringBuilder resultFile = new StringBuilder();
			String newDrive = drive.RootDirectory.ToString();
			bool found = false;
			int id = 1, j = 0;
			if (drive.DriveType == DriveType.Removable && drive.IsReady)
			{
				try
				{
					using (StreamReader sr = new StreamReader(newDrive + FileRemConf))
					{
						String line;
						String bufPathFile;
						int bufId;
						while ((line = sr.ReadLine()) != null)
						{
							if (line.StartsWith("syncronization"))
							{
								bufId = int.Parse(line.Substring(15));
								bufPathFile = sr.ReadLine();
								resultFile.AppendLine(line);
								for (int i = 0; i < idRemovableDrives.Count; i++)
								{
									if (idRemovableDrives[i] == id && nameRemovableDrives[i] == bufPathFile)
									{
										resultFile.AppendLine(newDrive);
										id = bufId;
										found = true;
										j = i;
									}

								}
								if (!found)
								{
									resultFile.AppendLine(bufPathFile);
									id = bufId + 1;
								}
							}
						}
						sr.Close();
					}
				}
				catch (Exception)
				{ }

				if (found)
				{
					syncDirs.UpdatePaths(nameRemovableDrives[j], newDrive);
					nameRemovableDrives[j] = newDrive;
				}
				else
				{
					idRemovableDrives.Add(id);
					nameRemovableDrives.Add(newDrive);
				}
				if (File.Exists(newDrive + FileRemConf))
					File.SetAttributes(newDrive + FileRemConf, FileAttributes.Normal);
				if (File.Exists(newDrive + FileRemConf) || append)
				{
					if (!found && append)
						using (StreamWriter w = File.AppendText(drive.RootDirectory + FileRemConf))
						{
							w.WriteLine("syncronization " + id);
							w.WriteLine(newDrive);
							w.Close();
						}
					else
						using (StreamWriter w = new StreamWriter(drive.RootDirectory.ToString() + FileRemConf))
						{
							w.Write(resultFile);
							w.Close();
						};
					File.SetAttributes(newDrive + FileRemConf, FileAttributes.Hidden);
				}
			}
		}

		private void tBPath_TextChanged(object sender, EventArgs e)
		{
			tSSLabel.Text = "";
			tSSIcon.Visible = false;
		}

		private void btDelSync_Click(object sender, EventArgs e)
		{
			foreach (ListViewItem item in lVDirs.SelectedItems)
			{
				syncDirs.Remove((SyncDirs)item.Tag);
			}
			btSync.Enabled = false;
			btEditSync.Enabled = false;
			btDelSync.Enabled = false;
			btOnSync.Enabled = false;
			btOffSync.Enabled = false;
		}

		private void btOnSync_Click(object sender, EventArgs e)
		{
			foreach (DriveInfo drive in DriveInfo.GetDrives())
				markRemoveble(drive);
			syncDirs.SelSetState(SyncState.Analyzing);
		}

		private void btOffSync_Click(object sender, EventArgs e)
		{
			syncDirs.SelSetState(SyncState.Off);
		}

		private void lVDirs_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			bool isSelected = lVDirs.SelectedItems.Count > 0;
			bool isNeedSync = lVDirs.SelectedItems.Count > 0;
			foreach (ListViewItem item in lVDirs.SelectedItems)
				if (!((item.Tag as SyncDirs).State == SyncState.WaitSolution))
				{
					isNeedSync = false;
					break;
				}
			btSync.Enabled = isNeedSync;
			btEditSync.Enabled = lVDirs.SelectedItems.Count == 1;
			btDelSync.Enabled = isSelected;
			btOnSync.Enabled = isSelected;
			btOffSync.Enabled = isSelected;
			((SyncDirs)e.Item.Tag).Selected = e.IsSelected;
		}

		private void btEditSync_Click(object sender, EventArgs e)
		{
			btApplyAdd.Visible = false;
			btApplyEdit.Visible = true;
			tBPathA.Text = (lVDirs.SelectedItems[0].Tag as SyncDirs).PathA;
			tBPathB.Text = (lVDirs.SelectedItems[0].Tag as SyncDirs).PathB;
			cBAutoSync.Checked = (lVDirs.SelectedItems[0].Tag as SyncDirs).AllowAutoSync;
			gBAddSync.Visible = true;
			//statusStrip1.Visible = true;
			gBCommandsSync.Visible = false;
			lVDirs.Enabled = false;
		}

		private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				if (WindowState == FormWindowState.Minimized || !Visible)
				{
					Show();
					Activate();
					ShowWindow(this.Handle, SW_RESTORE);
				}
				else
					Hide();
			}
			else
			{
				List<ToolStripItem> items = new List<ToolStripItem>();
				foreach (DriveInfo drive in DriveInfo.GetDrives())
					if (drive.DriveType == DriveType.Removable && drive.IsReady)
					{
						ToolStripItem item = new System.Windows.Forms.ToolStripMenuItem("Извлечь " + drive.VolumeLabel + "(" + drive.RootDirectory + ")");
						item.Tag = drive.RootDirectory.ToString()[0];
						item.Click += new EventHandler(Eject_Click);
						items.Add(item);
					}
				if (items.Count > 0)
					items.Add(new ToolStripSeparator());
				foreach (ToolStripItem item in cMSTray.Items)
					if (item.Tag != null && item.Tag.ToString() == "1")
						items.Add(item);
				cMSTray.Items.Clear();
				cMSTray.Items.AddRange(items.ToArray());
				MethodInfo mi = typeof(NotifyIcon).GetMethod("ShowContextMenu", BindingFlags.Instance | BindingFlags.NonPublic);
				mi.Invoke(notifyIcon, null);
			}
		}

		private void Eject_Click(object sender, EventArgs e)
		{
			List<SyncDirs> syncs = new List<SyncDirs>();
			foreach (SyncDirs item in syncDirs)
			{
				if (item.State != SyncState.Off && (
					item.PathA.StartsWith((sender as ToolStripItem).Tag.ToString()) ||
					item.PathB.StartsWith((sender as ToolStripItem).Tag.ToString())))
				{
					item.State = SyncState.Off;
					syncs.Add(item);
				}
			}
			DriveDetector.EjectDrive((char)(sender as ToolStripItem).Tag);
			foreach (SyncDirs item in syncs)
				item.State = SyncState.Analyzing;
		}

		private void timerScanner_Tick(object sender, EventArgs e)
		{
			notifyIcon.Text = syncDirs.SyncsUpdate();
		}

		private void lVDirs_DoubleClick(object sender, EventArgs e)
		{
			if (lVDirs.SelectedItems.Count == 1 && lVDirs.SelectedItems[0].SubItems[2].Text == StateStrings.Items[(int)SyncState.WaitSolution])
			{
				syncForm = new SynchronizationForm((SyncDirs)lVDirs.SelectedItems[0].Tag);
				syncForm.ShowDialog();
				syncForm = null;
			}

		}

		private void lVDirs_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			if (e.Column == lvwColumnSorter.SortColumn)
			{
				if (lvwColumnSorter.Order == SortOrder.Ascending)
					lvwColumnSorter.Order = SortOrder.Descending;
				else
					lvwColumnSorter.Order = SortOrder.Ascending;
			}
			else
			{
				lvwColumnSorter.SortColumn = e.Column;
				lvwColumnSorter.Order = SortOrder.Ascending;
			}
			this.lVDirs.Sort();
		}

		private void btSync_Click(object sender, EventArgs e)
		{
			SyncDirs currSync;
			foreach (ListViewItem item in lVDirs.SelectedItems)
			{
				currSync = (item.Tag as SyncDirs);
				currSync.AutoSync = true;
				currSync.State = SyncState.Analyzing;
			}
		}

		private void lVDirs_KeyUp(object sender, KeyEventArgs e)
		{
			switch (e.KeyValue)
			{
				case (int)Keys.Delete:
					if (btDelSync.Enabled)
						btDelSync_Click(null, null);
					break;
				case (int)Keys.Insert:
					btAddSync_Click(null, null);
					break;
				case (int)Keys.F2:
					btEditSync_Click(null, null);
					break;
				case (int)Keys.Enter:
					if (e.Shift && btSync.Enabled)
						btSync_Click(null, null);
					else
						lVDirs_DoubleClick(null, null);
					break;
				case (int)Keys.Escape:
					btOffSync_Click(null, null);
					break;
				case (int)Keys.F1:
					btOnSync_Click(null, null);
					break;
			}
		}
		//highp Здесь вонючий баг - не забыть поправить
		private void tBPathA_KeyUp(object sender, KeyEventArgs e)
		{
			switch (e.KeyData)
			{
				case Keys.Escape:
					btCancelAdd_Click(null, null);
					break;
				case Keys.Enter:
					if (btApplyAdd.Visible)
						btApplyAdd_Click(null, null);
					if (btApplyEdit.Visible)
						btApplyEdit_Click(null, null);
					break;
			}
		}
	}
}
