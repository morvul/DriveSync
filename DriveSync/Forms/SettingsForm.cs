using DriveSync.Properties;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DriveSync
{
	public partial class SettingsForm : Form
	{
		static SettingsForm pointer = null;
		public static Form MainForm;
		public static Form SyncForm;
		ArrayList colSizes;
		ArrayList colSizesSyncForm;
		bool alowClose;
		bool alowQuestionForClose;
		StringCollection pathsA;
		StringCollection pathsB;
		StringCollection marksA;
		StringCollection marksB;
		List<SyncState> states;
		List<bool> allowAutoSyncs;
		List<int> idRemovableDrives;
		List<string> nameRemovableDrives;

		public List<bool> AllowAutoSyncs
		{
			get { return allowAutoSyncs; }
			set { allowAutoSyncs = value; }
		}

		public List<string> NameRemovableDrives
		{
			get { return nameRemovableDrives; }
			set { nameRemovableDrives = value; }
		}

		public List<int> IdRemovableDrives
		{
			get { return idRemovableDrives; }
			set { idRemovableDrives = value; }
		}

		internal List<SyncState> States
		{
			get { return states; }
			set { states = value; }
		}

		public StringCollection PathsA
		{
			get { return pathsA; }
			set { pathsA = value; }
		}

		public StringCollection PathsB
		{
			get { return pathsB; }
			set { pathsB = value; }
		}

		public StringCollection MarksA
		{
			get { return marksA; }
			set { marksA = value; }
		}

		public StringCollection MarksB
		{
			get { return marksB; }
			set { marksB = value; }
		}

		public ArrayList ColSizes
		{
			get { return colSizes; }
			set { colSizes = value; }
		}

		public ArrayList ColSizesSyncForm
		{
			get { return colSizesSyncForm; }
			set { colSizesSyncForm = value; }
		}

		public bool AlowQuestionForClose
		{
			get { return alowQuestionForClose; }
			set
			{
				alowQuestionForClose = value;
				if (cBShowQuestMinimize.Checked != alowQuestionForClose)
					cBShowQuestMinimize.Checked = alowQuestionForClose;
			}
		}

		public bool AlowClose
		{
			get { return alowClose; }
			set
			{
				alowClose = value;
				if (cBAlowMinimize.Checked == alowClose)
					cBAlowMinimize.Checked = !AlowClose;
			}
		}
		public static SettingsForm Pointer
		{
			get
			{
				if (pointer == null)
					pointer = new SettingsForm();
				return pointer;
			}
		}
		private SettingsForm()
		{
			InitializeComponent();
			SyncForm = new Form();
			LoadSettings();
		}

		private void SettingsForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			pointer = null;
		}

		private void bClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void bApplySettings_Click(object sender, EventArgs e)
		{
			SaveSettings(false);
			bApplySettings.Enabled = false;
		}

		private void cBAlowMinimize_CheckedChanged(object sender, EventArgs e)
		{
			alowClose = !cBAlowMinimize.Checked;
			bApplySettings.Enabled = true;
		}

		private void cBShowQuestMinimize_CheckedChanged(object sender, EventArgs e)
		{
			alowQuestionForClose = cBShowQuestMinimize.Checked;
			bApplySettings.Enabled = true;
			cBAlowMinimize.Enabled = !cBShowQuestMinimize.Checked;
		}
		public void LoadSettings()
		{
			Settings set = Settings.Default;
			ArrayList states;
			ArrayList iDRemovableDrives;
			ArrayList allowAutoSyncs;
			StringCollection nameRemovableDrives;
			RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
			cBAutoRun.Checked = (rkApp.GetValue("DriveSync") != null);
			alowClose = set.AlowClose;
			alowQuestionForClose = set.AlowQuestionForClose;
			cBAlowMinimize.Checked = !alowClose;
			cBShowQuestMinimize.Checked = alowQuestionForClose;
			colSizes = set.CollsSize;
			colSizesSyncForm = set.CollsSizeSyncForm;
			pathsA = set.PathsA;
			pathsB = set.PathsB;
			marksA = set.MarksA;
			marksB = set.MarksB;
			states = set.States;
			MainForm.Size = set.Size;
			MainForm.Location = set.Location;
			SyncForm.Size = set.SizeSyncForm;
			SyncForm.Location = set.LocationSyncForm;
			States = new List<SyncState>();
			if (states != null)
			{
				foreach (SyncState item in states)
				{
					if(item ==SyncState.Off)
						States.Add(item);
					else
						States.Add(SyncState.Analyzing);
				}
			}
			AllowAutoSyncs = new List<bool>();
			allowAutoSyncs = set.AllowAutoSyncs;
			if (allowAutoSyncs != null)
			{
				foreach (object item in allowAutoSyncs)
					AllowAutoSyncs.Add((bool)item);
			}
			iDRemovableDrives = set.IDRemovableDrives;
			nameRemovableDrives = set.RemovableDrives;
			this.idRemovableDrives = new List<int>();
			this.nameRemovableDrives = new List<string>();
			if (iDRemovableDrives != null)
			{
				for (int i = 0; i < iDRemovableDrives.Count; i++)
				{
					this.idRemovableDrives.Add((int)iDRemovableDrives[i]);
					this.nameRemovableDrives.Add(nameRemovableDrives[i]);
				}
			}
			if (set.SyncFormMax)
				SyncForm.WindowState = FormWindowState.Maximized;
			if (set.FormMax)
				MainForm.WindowState = FormWindowState.Maximized;
			if (colSizes == null)
				colSizes = new ArrayList();
			if (colSizesSyncForm == null)
				colSizesSyncForm = new ArrayList();
			if (pathsB == null)
				pathsB = new StringCollection();
			if (pathsA == null)
				pathsA = new StringCollection();
			if (marksB == null)
				marksB = new StringCollection();
			if (marksA == null)
				marksA = new StringCollection();
		}

		public void SaveSettings(bool saveWinSize = true)
		{
			Settings set = Settings.Default;
			ArrayList states = new ArrayList();
			ArrayList iDRemovableDrives = new ArrayList();
			ArrayList allowAutoSyncs = new ArrayList();
			StringCollection namesRemovableDrives = new StringCollection();
			RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
			if (cBAutoRun.Checked)
				rkApp.SetValue("DriveSync", Application.ExecutablePath.ToString() + " /StartMinimized");
			else
				rkApp.DeleteValue("DriveSync", false);
			foreach (SyncState item in this.states)
			{
				if (item != SyncState.Off)
					states.Add(SyncState.Analyzing);
				else
					states.Add(item);
			}
			foreach (int item in idRemovableDrives)
				iDRemovableDrives.Add(item);
			foreach (string item in nameRemovableDrives)
				namesRemovableDrives.Add(item);
			foreach (bool item in AllowAutoSyncs)
				allowAutoSyncs.Add(item);
			set.AllowAutoSyncs = allowAutoSyncs;
			set.AlowClose = alowClose;
			set.AlowQuestionForClose = alowQuestionForClose;
			set.PathsA = pathsA;
			set.PathsB = pathsB;
			set.MarksA = marksA;
			set.MarksB = marksB;
			set.States = states;
			if (saveWinSize)
			{
				set.FormMax = (MainForm.WindowState == FormWindowState.Maximized);
				MainForm.Opacity = 0;
				MainForm.WindowState = FormWindowState.Normal;
				set.Size = MainForm.Size;

				set.SyncFormMax = (SyncForm.WindowState == FormWindowState.Maximized);
				SyncForm.WindowState = FormWindowState.Normal;
				set.SizeSyncForm = SyncForm.Size;
			}
			set.Location = MainForm.Location;
			set.LocationSyncForm = SyncForm.Location;
			set.CollsSize = colSizes;
			set.CollsSizeSyncForm = colSizesSyncForm;
			set.IDRemovableDrives = iDRemovableDrives;
			set.RemovableDrives = namesRemovableDrives;

			set.Save();
		}
	}
}
