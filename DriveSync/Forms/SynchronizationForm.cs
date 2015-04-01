using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DriveSync.Forms
{
	public partial class SynchronizationForm : Form
	{
		SyncDirs syncDirs;
		ListViewColumnSorter lvwColumnSorter;

		public SynchronizationForm()
		{
			InitializeComponent();
			Size = SettingsForm.SyncForm.Size;
			Location = SettingsForm.SyncForm.Location;
			WindowState = SettingsForm.SyncForm.WindowState;
			for (int i = 0; i < lVFiles.Columns.Count && i < SettingsForm.Pointer.ColSizesSyncForm.Count; i++)
				lVFiles.Columns[i].Width = (int)SettingsForm.Pointer.ColSizesSyncForm[i];
		}

		public SynchronizationForm(SyncDirs syncDirs)
			: this()
		{
			this.syncDirs = syncDirs;
		}

		private void SynchronizationForm_Shown(object sender, EventArgs e)
		{
			tBPathA.Text = (syncDirs.MarkA == "" ? "" : "[" + syncDirs.MarkA + "] ") + syncDirs.PathA;
			tBPathB.Text = (syncDirs.MarkB == "" ? "" : "[" + syncDirs.MarkB + "] ") + syncDirs.PathB;
			cBSyncMode.SelectedIndex = -1;
			lvwColumnSorter = new ListViewColumnSorter();
			lVFiles.ListViewItemSorter = lvwColumnSorter;
			foreach (var item in syncDirs.SyncList)
			{
				string fileName = item.Key.Substring(1);
				ListViewItem pIFile = new ListViewItem();
				if (Directory.Exists(syncDirs.PathA + item.Key) || Directory.Exists(syncDirs.PathB + item.Key))
					pIFile.ImageIndex = 0;
				else
					pIFile.ImageIndex = 1;
				FileInfo fileA = new FileInfo(syncDirs.PathA + item.Key);
				FileInfo fileB = new FileInfo(syncDirs.PathB + item.Key);
				string dateA = fileA.LastWriteTime>new DateTime(1602,1,1) ? fileA.LastWriteTime.ToString("dd.MM.yyyy hh:mm") : "-";
				string dateB = fileB.LastWriteTime > new DateTime(1602,1,1) ? fileB.LastWriteTime.ToString("dd.MM.yyyy hh:mm") : "-";
				pIFile.Text = fileName;
				ListViewItem.ListViewSubItem cIDateA = new ListViewItem.ListViewSubItem(pIFile, dateA);
				ListViewItem.ListViewSubItem cIDateB = new ListViewItem.ListViewSubItem(pIFile, dateB);
				ListViewItem.ListViewSubItem cIMode = new ListViewItem.ListViewSubItem(pIFile, ModeStrings.Items[(int)item.Value]);
				pIFile.SubItems.Add(cIDateA);
				pIFile.SubItems.Add(cIDateB);
				pIFile.SubItems.Add(cIMode);
				pIFile.Tag = item.Value;
				lVFiles.Items.Add(pIFile);
			}
			this.lVFiles.Sort();
		}

		private void SynchronizationForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			SettingsForm.SyncForm.WindowState = WindowState;
			Opacity = 0;
			WindowState = FormWindowState.Normal;
			SettingsForm.SyncForm.Size = Size;
			SettingsForm.SyncForm.Location = Location;
			SettingsForm.Pointer.ColSizesSyncForm.Clear();
			for (int i = 0; i < lVFiles.Columns.Count; i++)
				SettingsForm.Pointer.ColSizesSyncForm.Add(lVFiles.Columns[i].Width);
			foreach (ListViewItem item in lVFiles.Items)
			{
				syncDirs.SyncList["\\" + item.Text] = (SyncMode)item.Tag;
			}
			if (syncDirs.AutoSync)
				syncDirs.State = SyncState.Analyzing;
		}

		private void cmSymcMode_Opening(object sender, CancelEventArgs e)
		{
			if (lVFiles.SelectedItems.Count == 0)
			{
				e.Cancel = true;
			}
		}

		private void cBSyncMode_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cBSyncMode.SelectedIndex == -1)
				return;
			if (lVFiles.SelectedItems.Count == 0 || lVFiles.SelectedItems.Count == lVFiles.Items.Count)
				foreach (ListViewItem item in lVFiles.Items)
				{
					item.SubItems[3].Text = ModeStrings.Items[cBSyncMode.SelectedIndex];
					item.Tag = (SyncMode)cBSyncMode.SelectedIndex;
				}
			else
			{
				foreach (ListViewItem item in lVFiles.SelectedItems)
				{
					item.SubItems[3].Text = ModeStrings.Items[cBSyncMode.SelectedIndex];
					item.Tag = (SyncMode)cBSyncMode.SelectedIndex;
				}
			}
		}

		private void tSMNoneSync_Click(object sender, EventArgs e)
		{
			cBSyncMode.SelectedIndex = (int)SyncMode.NoneNync;
		}



		private void lVFiles_SelectedIndexChanged(object sender, EventArgs e)
		{
			SyncMode prewMode;
			if (lVFiles.SelectedItems.Count == 0)
			{
				prewMode = (SyncMode)lVFiles.Items[0].Tag;
				foreach (ListViewItem item in lVFiles.Items)
				{
					if ((SyncMode)item.Tag != prewMode)
					{
						cBSyncMode.SelectedIndex = -1;
						return;
					}
				}
			}
			else
			{
				prewMode = (SyncMode)lVFiles.SelectedItems[0].Tag;
				foreach (ListViewItem item in lVFiles.SelectedItems)
				{
					if ((SyncMode)item.Tag != prewMode)
					{
						cBSyncMode.SelectedIndex = -1;
						return;
					}
				}
			}
			cBSyncMode.SelectedIndex = (int)prewMode;
		}

		private void вНаправленииАBToolStripMenuItem_Click(object sender, EventArgs e)
		{
			cBSyncMode.SelectedIndex = (int)SyncMode.AToB;
		}

		private void вНаправленииВAToolStripMenuItem_Click(object sender, EventArgs e)
		{
			cBSyncMode.SelectedIndex = (int)SyncMode.BToA;
		}

		private void lVFiles_ColumnClick(object sender, ColumnClickEventArgs e)
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
			this.lVFiles.Sort();
		}

		private void btSync_Click(object sender, EventArgs e)
		{
			syncDirs.AutoSync = true;
			Close();
		}


	}
}
