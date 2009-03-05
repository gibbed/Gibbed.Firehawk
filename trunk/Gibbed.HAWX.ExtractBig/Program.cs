using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Gibbed.HAWX.Helpers;

namespace Gibbed.HAWX.ExtractBig
{
	public class FileEntry
	{
		public string Path;
		public uint Offset;
		public uint Size;
	}

	public class Program
	{
		private static List<FileEntry> Files = new List<FileEntry>();

		public static void ReadDirectory(string parentDirectory, Stream input)
		{
			string path;
			string currentDirectory = input.ReadASCIIZ();

			if (parentDirectory == null)
			{
				path = ""; // currentDirectory;
			}
			else
			{
				path = Path.Combine(parentDirectory, currentDirectory);
			}

			uint fileCount = input.ReadU32BE();
			for (uint i = 0; i < fileCount; i++)
			{
				FileEntry entry = new FileEntry();
				entry.Size = input.ReadU32BE();
				entry.Path = Path.Combine(path, input.ReadASCIIZ());
				entry.Offset = input.ReadU32BE();

				if (entry.Offset != 0xFFFFFFFF)
				{
					Files.Add(entry);
					Console.WriteLine(Path.Combine(path, entry.Path));
				}
			}

			uint dirCount = input.ReadU32BE();
			for (uint i = 0; i < dirCount; i++)
			{
				ReadDirectory(path, input);
			}
		}

		public static void Main(string[] args)
		{
			Stream input;
			
			input = File.OpenRead("filelist.lst");
			if (input.ReadU32BE() != 0x1F0000F1)
			{
				throw new Exception();
			}
			ReadDirectory(null, input);
			input.Close();

			input = File.OpenRead("bigfile.bin");
			foreach (FileEntry entry in Files)
			{
				string dir = Path.GetDirectoryName(entry.Path);
				if (dir.Length > 0)
				{
					Directory.CreateDirectory(dir);
				}

				input.Seek(entry.Offset, SeekOrigin.Begin);
				Stream output = File.OpenWrite(entry.Path);

				int left = (int)entry.Size;
				byte[] data = new byte[4096];
				while (left > 0)
				{
					int block = Math.Min(left, 4096);
					input.Read(data, 0, block);
					output.Write(data, 0, block);
					left -= block;
				}

				output.Close();
			}
		}
	}
}
