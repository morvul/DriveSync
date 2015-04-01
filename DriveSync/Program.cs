using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace DriveSync
{
	static class Program
	{
		private static Mutex _syncObject;
		private const string _syncObjectName = "{E663FA11-AE0D-480e-9FCA-4BE9B8CDB4E9}";

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			bool createdNew;
			_syncObject = new Mutex(true, _syncObjectName, out createdNew);
			if (!createdNew)
			{
				MessageBox.Show("Программа уже запущена.");
				return;
			}
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			bool startMinimized = false;
			for (int i = 0; i < args.Length; ++i)
			{
				if (args[i] == "/StartMinimized")
				{
					startMinimized = true;
				}
			}
			Application.Run(new MainForm(startMinimized));
		}
	}
}
