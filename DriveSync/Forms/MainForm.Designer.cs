namespace DriveSync
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.cMSTray = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tSMISettings = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.tSMIExit = new System.Windows.Forms.ToolStripMenuItem();
			this.parentPanel = new System.Windows.Forms.Panel();
			this.gBCommandsSync = new System.Windows.Forms.GroupBox();
			this.btSync = new System.Windows.Forms.Button();
			this.btEditSync = new System.Windows.Forms.Button();
			this.btAddSync = new System.Windows.Forms.Button();
			this.btDelSync = new System.Windows.Forms.Button();
			this.btOffSync = new System.Windows.Forms.Button();
			this.btOnSync = new System.Windows.Forms.Button();
			this.gBAddSync = new System.Windows.Forms.GroupBox();
			this.cBAutoSync = new System.Windows.Forms.CheckBox();
			this.btApplyEdit = new System.Windows.Forms.Button();
			this.btCancelAdd = new System.Windows.Forms.Button();
			this.btApplyAdd = new System.Windows.Forms.Button();
			this.btReviewB = new System.Windows.Forms.Button();
			this.tBPathB = new System.Windows.Forms.TextBox();
			this.btReviewA = new System.Windows.Forms.Button();
			this.lDirB = new System.Windows.Forms.Label();
			this.tBPathA = new System.Windows.Forms.TextBox();
			this.lDirA = new System.Windows.Forms.Label();
			this.lVDirs = new System.Windows.Forms.ListView();
			this.chDirA = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.chDirB = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.cHState = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.tSSIcon = new System.Windows.Forms.ToolStripStatusLabel();
			this.tSSLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.tSCurrentProgress = new System.Windows.Forms.ToolStripProgressBar();
			this.timerInterfaceUpdate = new System.Windows.Forms.Timer(this.components);
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.cMSTray.SuspendLayout();
			this.parentPanel.SuspendLayout();
			this.gBCommandsSync.SuspendLayout();
			this.gBAddSync.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// notifyIcon
			// 
			this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
			this.notifyIcon.ContextMenuStrip = this.cMSTray;
			this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
			this.notifyIcon.Visible = true;
			this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
			// 
			// cMSTray
			// 
			this.cMSTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tSMISettings,
            this.toolStripMenuItem1,
            this.tSMIExit});
			this.cMSTray.Name = "cMSTray";
			this.cMSTray.Size = new System.Drawing.Size(135, 54);
			// 
			// tSMISettings
			// 
			this.tSMISettings.Name = "tSMISettings";
			this.tSMISettings.Size = new System.Drawing.Size(134, 22);
			this.tSMISettings.Tag = "1";
			this.tSMISettings.Text = "Настройки";
			this.tSMISettings.Click += new System.EventHandler(this.tSMISettings_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(131, 6);
			this.toolStripMenuItem1.Tag = "1";
			// 
			// tSMIExit
			// 
			this.tSMIExit.Name = "tSMIExit";
			this.tSMIExit.Size = new System.Drawing.Size(134, 22);
			this.tSMIExit.Tag = "1";
			this.tSMIExit.Text = "Выход";
			this.tSMIExit.Click += new System.EventHandler(this.tSMIExit_Click);
			// 
			// parentPanel
			// 
			this.parentPanel.AutoSize = true;
			this.parentPanel.Controls.Add(this.gBCommandsSync);
			this.parentPanel.Controls.Add(this.gBAddSync);
			this.parentPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.parentPanel.Location = new System.Drawing.Point(0, 71);
			this.parentPanel.Name = "parentPanel";
			this.parentPanel.Padding = new System.Windows.Forms.Padding(3, 4, 3, 3);
			this.parentPanel.Size = new System.Drawing.Size(572, 163);
			this.parentPanel.TabIndex = 6;
			// 
			// gBCommandsSync
			// 
			this.gBCommandsSync.Controls.Add(this.btSync);
			this.gBCommandsSync.Controls.Add(this.btEditSync);
			this.gBCommandsSync.Controls.Add(this.btAddSync);
			this.gBCommandsSync.Controls.Add(this.btDelSync);
			this.gBCommandsSync.Controls.Add(this.btOffSync);
			this.gBCommandsSync.Controls.Add(this.btOnSync);
			this.gBCommandsSync.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.gBCommandsSync.Location = new System.Drawing.Point(3, 4);
			this.gBCommandsSync.Name = "gBCommandsSync";
			this.gBCommandsSync.Size = new System.Drawing.Size(566, 49);
			this.gBCommandsSync.TabIndex = 8;
			this.gBCommandsSync.TabStop = false;
			this.gBCommandsSync.Text = "Операции над синхронизациями";
			// 
			// btSync
			// 
			this.btSync.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btSync.Enabled = false;
			this.btSync.Location = new System.Drawing.Point(97, 19);
			this.btSync.Name = "btSync";
			this.btSync.Size = new System.Drawing.Size(126, 23);
			this.btSync.TabIndex = 6;
			this.btSync.Text = "Синхронизировать";
			this.toolTip.SetToolTip(this.btSync, "Cинхронизация с автовыбором направления.\r\nShift+Enter");
			this.btSync.UseVisualStyleBackColor = true;
			this.btSync.Click += new System.EventHandler(this.btSync_Click);
			// 
			// btEditSync
			// 
			this.btEditSync.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btEditSync.Enabled = false;
			this.btEditSync.Location = new System.Drawing.Point(229, 19);
			this.btEditSync.Name = "btEditSync";
			this.btEditSync.Size = new System.Drawing.Size(75, 23);
			this.btEditSync.TabIndex = 2;
			this.btEditSync.Text = "Изменить";
			this.toolTip.SetToolTip(this.btEditSync, "Изменение синхронизируемых каталогов.\r\nВыбор режима синхронизации (автоматический" +
        " / ручной).\r\nF2");
			this.btEditSync.UseVisualStyleBackColor = true;
			this.btEditSync.Click += new System.EventHandler(this.btEditSync_Click);
			// 
			// btAddSync
			// 
			this.btAddSync.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btAddSync.Location = new System.Drawing.Point(16, 20);
			this.btAddSync.Name = "btAddSync";
			this.btAddSync.Size = new System.Drawing.Size(75, 23);
			this.btAddSync.TabIndex = 1;
			this.btAddSync.Text = "Добавить";
			this.toolTip.SetToolTip(this.btAddSync, "Создание новой пары синхронизируемых каталогов.\r\nInsert\r\n");
			this.btAddSync.UseVisualStyleBackColor = true;
			this.btAddSync.Click += new System.EventHandler(this.btAddSync_Click);
			// 
			// btDelSync
			// 
			this.btDelSync.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btDelSync.Enabled = false;
			this.btDelSync.Location = new System.Drawing.Point(310, 19);
			this.btDelSync.Name = "btDelSync";
			this.btDelSync.Size = new System.Drawing.Size(75, 23);
			this.btDelSync.TabIndex = 3;
			this.btDelSync.Text = "Удалить";
			this.toolTip.SetToolTip(this.btDelSync, "Удаление выбранных синхронизаций.\r\nDelete");
			this.btDelSync.UseVisualStyleBackColor = true;
			this.btDelSync.Click += new System.EventHandler(this.btDelSync_Click);
			// 
			// btOffSync
			// 
			this.btOffSync.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btOffSync.Enabled = false;
			this.btOffSync.Location = new System.Drawing.Point(472, 19);
			this.btOffSync.Name = "btOffSync";
			this.btOffSync.Size = new System.Drawing.Size(75, 23);
			this.btOffSync.TabIndex = 5;
			this.btOffSync.Text = "Отключить";
			this.toolTip.SetToolTip(this.btOffSync, "Отключение синхронизации выбранных каталогов.");
			this.btOffSync.UseVisualStyleBackColor = true;
			this.btOffSync.Click += new System.EventHandler(this.btOffSync_Click);
			// 
			// btOnSync
			// 
			this.btOnSync.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btOnSync.Enabled = false;
			this.btOnSync.Location = new System.Drawing.Point(391, 19);
			this.btOnSync.Name = "btOnSync";
			this.btOnSync.Size = new System.Drawing.Size(75, 23);
			this.btOnSync.TabIndex = 4;
			this.btOnSync.Text = "Включить";
			this.toolTip.SetToolTip(this.btOnSync, "Анализ выбранных синхронизаций с первичным \r\nопределением направления синхронизац" +
        "ии.\r\nF1");
			this.btOnSync.UseVisualStyleBackColor = true;
			this.btOnSync.Click += new System.EventHandler(this.btOnSync_Click);
			// 
			// gBAddSync
			// 
			this.gBAddSync.Controls.Add(this.cBAutoSync);
			this.gBAddSync.Controls.Add(this.btApplyEdit);
			this.gBAddSync.Controls.Add(this.btCancelAdd);
			this.gBAddSync.Controls.Add(this.btApplyAdd);
			this.gBAddSync.Controls.Add(this.btReviewB);
			this.gBAddSync.Controls.Add(this.tBPathB);
			this.gBAddSync.Controls.Add(this.btReviewA);
			this.gBAddSync.Controls.Add(this.lDirB);
			this.gBAddSync.Controls.Add(this.tBPathA);
			this.gBAddSync.Controls.Add(this.lDirA);
			this.gBAddSync.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.gBAddSync.Location = new System.Drawing.Point(3, 53);
			this.gBAddSync.Margin = new System.Windows.Forms.Padding(3, 3, 3, 6);
			this.gBAddSync.Name = "gBAddSync";
			this.gBAddSync.Padding = new System.Windows.Forms.Padding(3, 3, 3, 6);
			this.gBAddSync.Size = new System.Drawing.Size(566, 107);
			this.gBAddSync.TabIndex = 7;
			this.gBAddSync.TabStop = false;
			this.gBAddSync.Text = "Создание пары синхронизируемых каталогов";
			this.gBAddSync.Visible = false;
			// 
			// cBAutoSync
			// 
			this.cBAutoSync.AutoSize = true;
			this.cBAutoSync.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.cBAutoSync.Checked = true;
			this.cBAutoSync.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cBAutoSync.Location = new System.Drawing.Point(15, 85);
			this.cBAutoSync.Name = "cBAutoSync";
			this.cBAutoSync.Size = new System.Drawing.Size(190, 17);
			this.cBAutoSync.TabIndex = 7;
			this.cBAutoSync.Text = "Автоматическая синхронизация";
			this.cBAutoSync.UseVisualStyleBackColor = true;
			// 
			// btApplyEdit
			// 
			this.btApplyEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btApplyEdit.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btApplyEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btApplyEdit.Location = new System.Drawing.Point(361, 81);
			this.btApplyEdit.Name = "btApplyEdit";
			this.btApplyEdit.Size = new System.Drawing.Size(94, 23);
			this.btApplyEdit.TabIndex = 6;
			this.btApplyEdit.Text = "Сохранить";
			this.btApplyEdit.UseVisualStyleBackColor = true;
			this.btApplyEdit.Click += new System.EventHandler(this.btApplyEdit_Click);
			// 
			// btCancelAdd
			// 
			this.btCancelAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btCancelAdd.Location = new System.Drawing.Point(461, 81);
			this.btCancelAdd.Name = "btCancelAdd";
			this.btCancelAdd.Size = new System.Drawing.Size(94, 23);
			this.btCancelAdd.TabIndex = 5;
			this.btCancelAdd.Text = "Отмена";
			this.btCancelAdd.UseVisualStyleBackColor = true;
			this.btCancelAdd.Click += new System.EventHandler(this.btCancelAdd_Click);
			// 
			// btApplyAdd
			// 
			this.btApplyAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btApplyAdd.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btApplyAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btApplyAdd.Location = new System.Drawing.Point(361, 81);
			this.btApplyAdd.Name = "btApplyAdd";
			this.btApplyAdd.Size = new System.Drawing.Size(94, 23);
			this.btApplyAdd.TabIndex = 4;
			this.btApplyAdd.Text = "Создать";
			this.btApplyAdd.UseVisualStyleBackColor = true;
			this.btApplyAdd.Click += new System.EventHandler(this.btApplyAdd_Click);
			// 
			// btReviewB
			// 
			this.btReviewB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btReviewB.Location = new System.Drawing.Point(479, 53);
			this.btReviewB.Name = "btReviewB";
			this.btReviewB.Size = new System.Drawing.Size(75, 23);
			this.btReviewB.TabIndex = 3;
			this.btReviewB.Text = "Обзор...";
			this.btReviewB.UseVisualStyleBackColor = true;
			this.btReviewB.Click += new System.EventHandler(this.btReviewB_Click);
			// 
			// tBPathB
			// 
			this.tBPathB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tBPathB.Location = new System.Drawing.Point(76, 55);
			this.tBPathB.Name = "tBPathB";
			this.tBPathB.Size = new System.Drawing.Size(397, 20);
			this.tBPathB.TabIndex = 2;
			this.tBPathB.TextChanged += new System.EventHandler(this.tBPath_TextChanged);
			this.tBPathB.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tBPathA_KeyUp);
			// 
			// btReviewA
			// 
			this.btReviewA.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btReviewA.Location = new System.Drawing.Point(479, 24);
			this.btReviewA.Name = "btReviewA";
			this.btReviewA.Size = new System.Drawing.Size(75, 23);
			this.btReviewA.TabIndex = 1;
			this.btReviewA.Text = "Обзор...";
			this.btReviewA.UseVisualStyleBackColor = true;
			this.btReviewA.Click += new System.EventHandler(this.btReviewA_Click);
			// 
			// lDirB
			// 
			this.lDirB.AutoSize = true;
			this.lDirB.Location = new System.Drawing.Point(12, 58);
			this.lDirB.Name = "lDirB";
			this.lDirB.Size = new System.Drawing.Size(58, 13);
			this.lDirB.TabIndex = 3;
			this.lDirB.Text = "Каталог Б";
			// 
			// tBPathA
			// 
			this.tBPathA.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tBPathA.Location = new System.Drawing.Point(76, 26);
			this.tBPathA.Name = "tBPathA";
			this.tBPathA.Size = new System.Drawing.Size(397, 20);
			this.tBPathA.TabIndex = 0;
			this.tBPathA.TextChanged += new System.EventHandler(this.tBPath_TextChanged);
			this.tBPathA.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tBPathA_KeyUp);
			// 
			// lDirA
			// 
			this.lDirA.AutoSize = true;
			this.lDirA.Location = new System.Drawing.Point(12, 29);
			this.lDirA.Name = "lDirA";
			this.lDirA.Size = new System.Drawing.Size(58, 13);
			this.lDirA.TabIndex = 4;
			this.lDirA.Text = "Каталог А";
			// 
			// lVDirs
			// 
			this.lVDirs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chDirA,
            this.chDirB,
            this.cHState});
			this.lVDirs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lVDirs.FullRowSelect = true;
			this.lVDirs.HideSelection = false;
			this.lVDirs.LabelWrap = false;
			this.lVDirs.Location = new System.Drawing.Point(0, 0);
			this.lVDirs.Name = "lVDirs";
			this.lVDirs.Size = new System.Drawing.Size(572, 71);
			this.lVDirs.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.lVDirs.TabIndex = 7;
			this.lVDirs.UseCompatibleStateImageBehavior = false;
			this.lVDirs.View = System.Windows.Forms.View.Details;
			this.lVDirs.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lVDirs_ColumnClick);
			this.lVDirs.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lVDirs_ItemSelectionChanged);
			this.lVDirs.DoubleClick += new System.EventHandler(this.lVDirs_DoubleClick);
			this.lVDirs.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lVDirs_KeyUp);
			// 
			// chDirA
			// 
			this.chDirA.Text = "Каталог А";
			this.chDirA.Width = 117;
			// 
			// chDirB
			// 
			this.chDirB.Text = "Каталог Б";
			this.chDirB.Width = 142;
			// 
			// cHState
			// 
			this.cHState.Text = "Состояние";
			this.cHState.Width = 112;
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tSSIcon,
            this.tSSLabel,
            this.toolStripStatusLabel1,
            this.tSCurrentProgress});
			this.statusStrip1.Location = new System.Drawing.Point(0, 234);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(572, 22);
			this.statusStrip1.TabIndex = 8;
			// 
			// tSSIcon
			// 
			this.tSSIcon.Image = ((System.Drawing.Image)(resources.GetObject("tSSIcon.Image")));
			this.tSSIcon.Name = "tSSIcon";
			this.tSSIcon.Size = new System.Drawing.Size(16, 17);
			this.tSSIcon.Visible = false;
			// 
			// tSSLabel
			// 
			this.tSSLabel.Name = "tSSLabel";
			this.tSSLabel.Size = new System.Drawing.Size(0, 17);
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
			// 
			// tSCurrentProgress
			// 
			this.tSCurrentProgress.Name = "tSCurrentProgress";
			this.tSCurrentProgress.Size = new System.Drawing.Size(100, 16);
			this.tSCurrentProgress.Visible = false;
			// 
			// timerInterfaceUpdate
			// 
			this.timerInterfaceUpdate.Interval = 500;
			this.timerInterfaceUpdate.Tick += new System.EventHandler(this.timerScanner_Tick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(572, 256);
			this.Controls.Add(this.lVDirs);
			this.Controls.Add(this.parentPanel);
			this.Controls.Add(this.statusStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(588, 200);
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Drive Sync";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.Shown += new System.EventHandler(this.Form1_Shown);
			this.cMSTray.ResumeLayout(false);
			this.parentPanel.ResumeLayout(false);
			this.gBCommandsSync.ResumeLayout(false);
			this.gBAddSync.ResumeLayout(false);
			this.gBAddSync.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.NotifyIcon notifyIcon;
		private System.Windows.Forms.ContextMenuStrip cMSTray;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem tSMIExit;
		private System.Windows.Forms.ToolStripMenuItem tSMISettings;
		private System.Windows.Forms.Panel parentPanel;
		private System.Windows.Forms.ListView lVDirs;
		private System.Windows.Forms.ColumnHeader chDirA;
		private System.Windows.Forms.ColumnHeader chDirB;
		private System.Windows.Forms.ColumnHeader cHState;
		private System.Windows.Forms.GroupBox gBAddSync;
		private System.Windows.Forms.Button btReviewB;
		private System.Windows.Forms.TextBox tBPathB;
		private System.Windows.Forms.Button btReviewA;
		private System.Windows.Forms.Label lDirB;
		private System.Windows.Forms.TextBox tBPathA;
		private System.Windows.Forms.Label lDirA;
		private System.Windows.Forms.Button btCancelAdd;
		private System.Windows.Forms.Button btApplyAdd;
		private System.Windows.Forms.GroupBox gBCommandsSync;
		private System.Windows.Forms.Button btAddSync;
		private System.Windows.Forms.Button btDelSync;
		private System.Windows.Forms.Button btOnSync;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel tSSLabel;
		private System.Windows.Forms.ToolStripStatusLabel tSSIcon;
		private System.Windows.Forms.Button btOffSync;
		private System.Windows.Forms.Timer timerInterfaceUpdate;
		private System.Windows.Forms.Button btEditSync;
		private System.Windows.Forms.Button btApplyEdit;
		private System.Windows.Forms.CheckBox cBAutoSync;
		private System.Windows.Forms.Button btSync;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.ToolStripProgressBar tSCurrentProgress;
		private System.Windows.Forms.ToolTip toolTip;
	}
}

