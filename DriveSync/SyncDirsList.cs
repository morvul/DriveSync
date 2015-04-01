using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DriveSync
{
	class SyncDirsList : List<SyncDirs>
	{
		StatusStrip statusStrip;
		ListView listView;
		SyncDirs currentTask;
		Queue<SyncDirs> itemsInProcess;
		bool stateChanged;

		public SyncDirs CurrentTask
		{
			get { return currentTask; }
		}

		public bool StateChanged
		{
			get
			{
				bool bufVal = stateChanged;
				if (bufVal)
					stateChanged = false;
				return bufVal;
			}
			set { stateChanged = value; }
		}

		internal Queue<SyncDirs> ItemsInProcess
		{
			get { return itemsInProcess; }
		}

		public ListView ListView
		{
			get { return listView; }
			set { listView = value; }
		}

		public SyncDirsList(ListView listView, StatusStrip statusStrip)
		{
			this.listView = listView;
			this.statusStrip = statusStrip; ;
			itemsInProcess = new Queue<SyncDirs>();
			currentTask = new SyncDirs();
			stateChanged = true;
		}

		public new void Add(SyncDirs newItem)
		{
			base.Add(newItem);
			ListViewItem parentItem = new ListViewItem(newItem.PathA);
			ListViewItem.ListViewSubItem childItem = new ListViewItem.ListViewSubItem(parentItem, newItem.PathB);
			ListViewItem.ListViewSubItem childItem2 = new ListViewItem.ListViewSubItem(parentItem, newItem.StateString);
			parentItem.SubItems.Add(childItem);
			parentItem.SubItems.Add(childItem2);
			parentItem.Tag = newItem;
			listView.Items.Add(parentItem);
			stateChanged = true;
		}

		public new bool Remove(SyncDirs removingItem)
		{
			foreach (ListViewItem item in listView.Items)
				if (item.Tag == removingItem)
				{
					if (removingItem == currentTask)
					{
						currentTask.Cancel();
						currentTask = new SyncDirs();
					}
					listView.Items.Remove(item);
					base.Remove(removingItem);
					stateChanged = true;
					return true;
				}
			return false;
		}

		public void UpdateList()
		{
			if (!StateChanged)
				return;
			listView.Items.Clear();
			ListViewItem pIPathA;
			string pathA, pathB;
			foreach (SyncDirs item in this)
			{
				pathA = item.MarkA != "" ? "[" + item.MarkA + "] " + item.PathA : item.PathA;
				pathB = item.MarkB != "" ? "[" + item.MarkB + "] " + item.PathB : item.PathB;
				pIPathA = new ListViewItem(pathA);
				ListViewItem.ListViewSubItem cIPathB = new ListViewItem.ListViewSubItem(pIPathA, pathB);
				ListViewItem.ListViewSubItem cIState = new ListViewItem.ListViewSubItem(pIPathA, item.StateString);
				pIPathA.SubItems.Add(cIPathB);
				pIPathA.SubItems.Add(cIState);
				pIPathA.Tag = item;
				pIPathA.Selected = item.Selected;
				listView.Items.Add(pIPathA);
			}
		}

		public void SelSetState(SyncState state)
		{
			SyncDirs sDItem;
			List<ListViewItem> items = new List<ListViewItem>();
			foreach (ListViewItem item in listView.SelectedItems)
				items.Add(item);
			foreach (ListViewItem item in items)
			{
				item.Selected = true;
				sDItem = (SyncDirs)item.Tag;
				if (sDItem == currentTask)
					if (state != SyncState.InProcess || state != SyncState.Analyzing)
					{
						currentTask.Cancel();
						currentTask = new SyncDirs();
					}
					else
						continue;
				sDItem.State = state;
				sDItem.SyncList.Clear();
			}
			stateChanged = true;
			UpdateList();
		}

		public string SyncsUpdate()
		{
			SyncState state = SyncState.Ok;
			foreach (SyncDirs item in this)
			{
				if (item.State != SyncState.Off)
				{
					if (!item.DirsExists())
					{
						if (item.State != SyncState.NotAvailable)
						{
							item.State = SyncState.NotAvailable;
							stateChanged = true;
						}
					}
					else if (item.State == SyncState.NotAvailable)
						item.State = SyncState.Analyzing;
				}
				if (item.State == SyncState.Analyzing || item.State == SyncState.Ok || item.State == SyncState.UnSuccess)
				{
					if (item.NeedSync)
					{
						item.State = SyncState.WaitProcess;
						if (!itemsInProcess.Contains(item))
							itemsInProcess.Enqueue(item);
						stateChanged = true;
					}
					else if (item.State == SyncState.Analyzing)
					{
						item.State = SyncState.Ok;
						stateChanged = true;
					}
				}
				if (item.State == SyncState.WaitSolution)
				{
					state = SyncState.WaitSolution;
				}
			}
			if (currentTask.State != SyncState.InProcess && itemsInProcess.Count > 0)
			{
				currentTask = itemsInProcess.Dequeue();
				if (currentTask.State == SyncState.WaitProcess)
				{
					currentTask.Analyze();
					stateChanged = true;
				}
			}
			if (currentTask.NeedShow)
			{
				if (currentTask.State == SyncState.Ok)
					currentTask = new SyncDirs();
				stateChanged = true;
			}
			if (currentTask.State == SyncState.InProcess)
				state = SyncState.InProcess;
			UpdateList();
			StringBuilder programmState = new StringBuilder("SyncDrive - ");
			if (state != SyncState.InProcess)
			{
				if (statusStrip.Items[1].Text != "" && !statusStrip.Items[0].Visible)
				{
					statusStrip.Items[1].Text = "";
					statusStrip.Items[3].Visible = false;
					(statusStrip.Items[3] as ToolStripProgressBar).Value = 0;
				}
			}
			switch (state)
			{
				case SyncState.Ok:
					programmState.Append("Все ок");
					break;
				case SyncState.UnSuccess:
					programmState.Append("Не все ок");
					statusStrip.Items[3].Visible = false;
					break;
				case SyncState.InProcess:
					programmState.Append("Идет синхронизация...");
					string progressMess = currentTask.ProgressMess;
					double progress = currentTask.ProgressPercent;
					if (progressMess != "")
					{
						statusStrip.Items[1].Text = "Идет копирование: " + progressMess;
						if (progress > -1)
						{
							statusStrip.Items[3].Visible = true;
							(statusStrip.Items[3] as ToolStripProgressBar).Value = (int)progress;
						}
						statusStrip.Update();
					}
					break;
				case SyncState.WaitSolution:
					programmState.Append("Выберите направление синхронизации");
					break;
			}
			return programmState.ToString();
		}

		public void UpdatePaths(string oldDrive, string newDrive)
		{
			foreach (SyncDirs item in this)
			{
				if (item.PathA[0] == oldDrive[0])
					item.PathA = newDrive[0] + item.PathA.Remove(0, 1);
				if (item.PathB[0] == oldDrive[0])
					item.PathB = newDrive[0] + item.PathB.Remove(0, 1);
			}
		}


	}
}
