using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Gibbed.HAWX.Helpers;

namespace Gibbed.HAWX.FileFormats
{
	public class IndexEntry
	{
		public uint Unknown1;
		public string ID;
		public uint Index;
	}

	public class LanguageEntry
	{
		public ushort ID;
		public List<string> Texts = new List<string>();
	}

	public class LanguagePackFile
	{
		public List<IndexEntry> Indices = new List<IndexEntry>();
		public List<LanguageEntry> Languages = new List<LanguageEntry>();

		private string ReadUTF16BEString(Stream stream)
		{
			int length = stream.ReadS16BE();

			if (length > 0)
			{
				byte[] data = new byte[length * 2];
				stream.Read(data, 0, length * 2);
				return Encoding.BigEndianUnicode.GetString(data);
			}

			return "";
		}

		public void Read(Stream stream)
		{
			if (stream.ReadASCII(4) != "FHMP") // magic
			{
				throw new Exception();
			}

			if (stream.ReadU32BE() != 6) // file version
			{
				throw new Exception();
			}

			this.Languages.Clear();
			int idCount = stream.ReadS32BE();
			for (int i = 0; i < idCount; i++)
			{
				IndexEntry index = new IndexEntry();
				index.Unknown1 = stream.ReadU32BE();
				index.ID = stream.ReadASCII(stream.ReadU16BE());
				index.Index = stream.ReadU32BE();
				this.Indices.Add(index);
			}

			int languageCount = stream.ReadS32BE();
			for (int i = 0; i < languageCount; i++)
			{
				LanguageEntry language = new LanguageEntry();
				language.ID = stream.ReadU16BE();
				stream.ReadU32BE(); // size of following buffer, nyuh

				int textCount = stream.ReadS32BE();
				for (int j = 0; j < textCount; j++)
				{
					language.Texts.Add(this.ReadUTF16BEString(stream));
				}

				this.Languages.Add(language);
			}
		}

		public void Write(Stream stream)
		{
			throw new NotImplementedException();
		}
	}
}
