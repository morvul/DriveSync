namespace DriveSync.Forms
{
	partial class SynchronizationForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SynchronizationForm));
			this.lVFiles = new System.Windows.Forms.ListView();
			this.cHFile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.cHDateA = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.cHDateB = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.cHOperation = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.cmSymcMode = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tSMNoneSync = new System.Windows.Forms.ToolStripMenuItem();
			this.вНаправленииАBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.вНаправленииВAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.pPaths = new System.Windows.Forms.Panel();
			this.btSync = new System.Windows.Forms.Button();
			this.cBSyncMode = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.tBPathB = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.tBPathA = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.cmSymcMode.SuspendLayout();
			this.pPaths.SuspendLayout();
			this.SuspendLayout();
			// 
			// lVFiles
			// 
			this.lVFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lVFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.cHFile,
            this.cHDateA,
            this.cHDateB,
            this.cHOperation});
			this.lVFiles.ContextMenuStrip = this.cmSymcMode;
			this.lVFiles.FullRowSelect = true;
			this.lVFiles.HideSelection = false;
			this.lVFiles.LabelWrap = false;
			this.lVFiles.Location = new System.Drawing.Point(0, 94);
			this.lVFiles.Name = "lVFiles";
			this.lVFiles.Size = new System.Drawing.Size(644, 140);
			this.lVFiles.SmallImageList = this.imageList;
			this.lVFiles.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.lVFiles.TabIndex = 8;
			this.lVFiles.UseCompatibleStateImageBehavior = false;
			this.lVFiles.View = System.Windows.Forms.View.Details;
			this.lVFiles.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lVFiles_ColumnClick);
			this.lVFiles.SelectedIndexChanged += new System.EventHandler(this.lVFiles_SelectedIndexChanged);
			// 
			// cHFile
			// 
			this.cHFile.Text = "Файл";
			this.cHFile.Width = 268;
			// 
			// cHDateA
			// 
			this.cHDateA.Text = "Дата изменения А";
			this.cHDateA.Width = 111;
			// 
			// cHDateB
			// 
			this.cHDateB.Text = "Дата изменения Б";
			this.cHDateB.Width = 112;
			// 
			// cHOperation
			// 
			this.cHOperation.Text = "Операция";
			this.cHOperation.Width = 147;
			// 
			// cmSymcMode
			// 
			this.cmSymcMode.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tSMNoneSync,
            this.вНаправленииАBToolStripMenuItem,
            this.вНаправленииВAToolStripMenuItem});
			this.cmSymcMode.Name = "cmSymcMode";
			this.cmSymcMode.Size = new System.Drawing.Size(198, 70);
			this.cmSymcMode.Opening += new System.ComponentModel.CancelEventHandler(this.cmSymcMode_Opening);
			// 
			// tSMNoneSync
			// 
			this.tSMNoneSync.Name = "tSMNoneSync";
			this.tSMNoneSync.Size = new System.Drawing.Size(197, 22);
			this.tSMNoneSync.Tag = "0";
			this.tSMNoneSync.Text = "Не синхронизировать";
			this.tSMNoneSync.Click += new System.EventHandler(this.tSMNoneSync_Click);
			// 
			// вНаправленииАBToolStripMenuItem
			// 
			this.вНаправленииАBToolStripMenuItem.Name = "вНаправленииАBToolStripMenuItem";
			this.вНаправленииАBToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
			this.вНаправленииАBToolStripMenuItem.Tag = "1";
			this.вНаправленииАBToolStripMenuItem.Text = "В направлении: А -> Б";
			this.вНаправленииАBToolStripMenuItem.Click += new System.EventHandler(this.вНаправленииАBToolStripMenuItem_Click);
			// 
			// вНаправленииВAToolStripMenuItem
			// 
			this.вНаправленииВAToolStripMenuItem.Name = "вНаправленииВAToolStripMenuItem";
			this.вНаправленииВAToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
			this.вНаправленииВAToolStripMenuItem.Tag = "2";
			this.вНаправленииВAToolStripMenuItem.Text = "В направлении: Б -> A";
			this.вНаправленииВAToolStripMenuItem.Click += new System.EventHandler(this.вНаправленииВAToolStripMenuItem_Click);
			// 
			// imageList
			// 
			this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
			this.imageList.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList.Images.SetKeyName(0, "folder_small.ico");
			this.imageList.Images.SetKeyName(1, "files_small.ico");
			// 
			// pPaths
			// 
			this.pPaths.Controls.Add(this.btSync);
			this.pPaths.Controls.Add(this.cBSyncMode);
			this.pPaths.Controls.Add(this.label3);
			this.pPaths.Controls.Add(this.tBPathB);
			this.pPaths.Controls.Add(this.label2);
			this.pPaths.Controls.Add(this.tBPathA);
			this.pPaths.Controls.Add(this.label1);
			this.pPaths.Dock = System.Windows.Forms.DockStyle.Top;
			this.pPaths.Location = new System.Drawing.Point(0, 0);
			this.pPaths.Name = "pPaths";
			this.pPaths.Size = new System.Drawing.Size(644, 88);
			this.pPaths.TabIndex = 9;
			// 
			// btSync
			// 
			this.btSync.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btSync.Location = new System.Drawing.Point(517, 56);
			this.btSync.Name = "btSync";
			this.btSync.Size = new System.Drawing.Size(115, 23);
			this.btSync.TabIndex = 6;
			this.btSync.Text = "Синхронизировать";
			this.btSync.UseVisualStyleBackColor = true;
			this.btSync.Click += new System.EventHandler(this.btSync_Click);
			// 
			// cBSyncMode
			// 
			this.cBSyncMode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cBSyncMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cBSyncMode.FormattingEnabled = true;
			this.cBSyncMode.Items.AddRange(new object[] {
            "Не синхронизировать",
            "А -> Б",
            "Б -> А"});
			this.cBSyncMode.Location = new System.Drawing.Point(173, 58);
			this.cBSyncMode.Name = "cBSyncMode";
			this.cBSyncMode.Size = new System.Drawing.Size(338, 21);
			this.cBSyncMode.TabIndex = 5;
			this.cBSyncMode.SelectedIndexChanged += new System.EventHandler(this.cBSyncMode_SelectedIndexChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 61);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(155, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "Направление синхронизации";
			// 
			// tBPathB
			// 
			this.tBPathB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tBPathB.Location = new System.Drawing.Point(76, 32);
			this.tBPathB.Name = "tBPathB";
			this.tBPathB.ReadOnly = true;
			this.tBPathB.Size = new System.Drawing.Size(556, 20);
			this.tBPathB.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 35);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(58, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Каталог Б";
			// 
			// tBPathA
			// 
			this.tBPathA.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tBPathA.Location = new System.Drawing.Point(76, 6);
			this.tBPathA.Name = "tBPathA";
			this.tBPathA.ReadOnly = true;
			this.tBPathA.Size = new System.Drawing.Size(556, 20);
			this.tBPathA.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(58, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Каталог А";
			// 
			// SynchronizationForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(644, 234);
			this.Controls.Add(this.pPaths);
			this.Controls.Add(this.lVFiles);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(660, 273);
			this.Name = "SynchronizationForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Выбор направления синхронизации";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SynchronizationForm_FormClosing);
			this.Shown += new System.EventHandler(this.SynchronizationForm_Shown);
			this.cmSymcMode.ResumeLayout(false);
			this.pPaths.ResumeLayout(false);
			this.pPaths.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView lVFiles;
		private System.Windows.Forms.ColumnHeader cHFile;
		private System.Windows.Forms.ColumnHeader cHOperation;
		private System.Windows.Forms.ImageList imageList;
		private System.Windows.Forms.ContextMenuStrip cmSymcMode;
		private System.Windows.Forms.ToolStripMenuItem tSMNoneSync;
		private System.Windows.Forms.ToolStripMenuItem вНаправленииАBToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem вНаправленииВAToolStripMenuItem;
		private System.Windows.Forms.Panel pPaths;
		private System.Windows.Forms.TextBox tBPathB;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox tBPathA;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cBSyncMode;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ColumnHeader cHDateA;
		private System.Windows.Forms.ColumnHeader cHDateB;
		private System.Windows.Forms.Button btSync;
	}
}