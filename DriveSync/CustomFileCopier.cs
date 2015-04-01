﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DriveSync
{
	public delegate void ProgressChangeDelegate(double Persentage, ref bool Cancel);
	public delegate void Completedelegate();

	class CustomFileCopier
	{
		public static string lastCopiedFile="";
		public CustomFileCopier(string Source, string Dest)
		{
			this.SourceFilePath = Source;
			this.DestFilePath = Dest;

			OnProgressChanged += delegate { };
			OnComplete += delegate { };
		}

		public void Copy()
		{
			byte[] buffer = new byte[1024 * 1024]; // 1MB buffer
			bool cancelFlag = false;
			lastCopiedFile = DestFilePath;
			try
			{
				if (File.Exists(DestFilePath))
					File.Delete(DestFilePath);
				using (FileStream source = new FileStream(SourceFilePath, FileMode.Open, FileAccess.Read))
				{
					long fileLength = source.Length;
					using (FileStream dest = new FileStream(DestFilePath, FileMode.CreateNew, FileAccess.Write))
					{
						long totalBytes = 0;
						int currentBlockSize = 0;

						while ((currentBlockSize = source.Read(buffer, 0, buffer.Length)) > 0)
						{
							totalBytes += currentBlockSize;
							double persentage = (double)totalBytes * 100.0 / fileLength;

							dest.Write(buffer, 0, currentBlockSize);

							cancelFlag = false;
							OnProgressChanged(persentage, ref cancelFlag);

							if (cancelFlag == true)
							{
								// Delete dest file here
								dest.Dispose();
								if (File.Exists(DestFilePath))
									File.Delete(DestFilePath);
								break;
							}
						}
					}
				}
				lastCopiedFile = "";
			}
			catch(Exception)
			{}
			if (!cancelFlag)
			{
				File.SetLastWriteTime(DestFilePath, (new FileInfo(SourceFilePath)).LastWriteTime);
			}
			OnComplete();
		}

		public string SourceFilePath { get; set; }
		public string DestFilePath { get; set; }

		public event ProgressChangeDelegate OnProgressChanged;
		public event Completedelegate OnComplete;
	}
}
