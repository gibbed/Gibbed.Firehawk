using System;
using System.IO;
using Gibbed.Firehawk.Helpers;

namespace Gibbed.Firehawk.FileFormats
{
	public class MissionEntitiesFile
	{
		public void Read(Stream stream)
		{
			if (stream.ReadASCII(4) != "EMHF") // FHME = 'Firehawk Mission Entities'
			{
				throw new Exception();
			}

			if (stream.ReadU32BE() != 14) // version
			{
				throw new Exception();
			}

			while (stream.Position < stream.Length)
			{
				uint unk1 = stream.ReadU32BE();
				uint size = stream.ReadU32BE();

				long next = stream.Position + size;

				if (unk1 == 0)
				{
					string name = stream.ReadASCIIZ(1024);
				}
				else
				{
					throw new Exception();
				}

				stream.Seek(next, SeekOrigin.Begin);
			}
		}

		public void Write(Stream stream)
		{
			throw new NotImplementedException();
		}
	}
}
