namespace DriveSync
{
	partial class SettingsForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
			this.cBAlowMinimize = new System.Windows.Forms.CheckBox();
			this.cBShowQuestMinimize = new System.Windows.Forms.CheckBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.bApplySettings = new System.Windows.Forms.Button();
			this.bClose = new System.Windows.Forms.Button();
			this.cBAutoRun = new System.Windows.Forms.CheckBox();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// cBAlowMinimize
			// 
			this.cBAlowMinimize.AutoSize = true;
			this.cBAlowMinimize.Location = new System.Drawing.Point(12, 37);
			this.cBAlowMinimize.Name = "cBAlowMinimize";
			this.cBAlowMinimize.Size = new System.Drawing.Size(183, 17);
			this.cBAlowMinimize.TabIndex = 0;
			this.cBAlowMinimize.Text = "Сворачивать вместо закрытия";
			this.cBAlowMinimize.UseVisualStyleBackColor = true;
			this.cBAlowMinimize.CheckedChanged += new System.EventHandler(this.cBAlowMinimize_CheckedChanged);
			// 
			// cBShowQuestMinimize
			// 
			this.cBShowQuestMinimize.AutoSize = true;
			this.cBShowQuestMinimize.Location = new System.Drawing.Point(12, 14);
			this.cBShowQuestMinimize.Name = "cBShowQuestMinimize";
			this.cBShowQuestMinimize.Size = new System.Drawing.Size(259, 17);
			this.cBShowQuestMinimize.TabIndex = 0;
			this.cBShowQuestMinimize.Text = "Запрос подтверждения закрытия программы";
			this.cBShowQuestMinimize.UseVisualStyleBackColor = true;
			this.cBShowQuestMinimize.CheckedChanged += new System.EventHandler(this.cBShowQuestMinimize_CheckedChanged);
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.bApplySettings);
			this.panel1.Controls.Add(this.bClose);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 335);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(292, 29);
			this.panel1.TabIndex = 1;
			// 
			// bApplySettings
			// 
			this.bApplySettings.Enabled = false;
			this.bApplySettings.Location = new System.Drawing.Point(133, 3);
			this.bApplySettings.Name = "bApplySettings";
			this.bApplySettings.Size = new System.Drawing.Size(75, 23);
			this.bApplySettings.TabIndex = 0;
			this.bApplySettings.Text = "Сохранить";
			this.bApplySettings.UseVisualStyleBackColor = true;
			this.bApplySettings.Click += new System.EventHandler(this.bApplySettings_Click);
			// 
			// bClose
			// 
			this.bClose.Location = new System.Drawing.Point(214, 3);
			this.bClose.Name = "bClose";
			this.bClose.Size = new System.Drawing.Size(75, 23);
			this.bClose.TabIndex = 0;
			this.bClose.Text = "Закрыть";
			this.bClose.UseVisualStyleBackColor = true;
			this.bClose.Click += new System.EventHandler(this.bClose_Click);
			// 
			// cBAutoRun
			// 
			this.cBAutoRun.AutoSize = true;
			this.cBAutoRun.Location = new System.Drawing.Point(12, 60);
			this.cBAutoRun.Name = "cBAutoRun";
			this.cBAutoRun.Size = new System.Drawing.Size(184, 17);
			this.cBAutoRun.TabIndex = 0;
			this.cBAutoRun.Text = "Запускать при старте Windows";
			this.cBAutoRun.UseVisualStyleBackColor = true;
			this.cBAutoRun.CheckedChanged += new System.EventHandler(this.cBAlowMinimize_CheckedChanged);
			// 
			// SettingsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(292, 364);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.cBShowQuestMinimize);
			this.Controls.Add(this.cBAutoRun);
			this.Controls.Add(this.cBAlowMinimize);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SettingsForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Настройки";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SettingsForm_FormClosed);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox cBAlowMinimize;
		private System.Windows.Forms.CheckBox cBShowQuestMinimize;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button bApplySettings;
		private System.Windows.Forms.Button bClose;
		private System.Windows.Forms.CheckBox cBAutoRun;
	}
}