using System;
using System.IO;
using Gibbed.Firehawk.Helpers;

namespace Gibbed.Firehawk.FileFormats
{
	public class MissionModelsFile
	{
		public void Read(Stream stream)
		{
			if (stream.ReadASCII(4) != "MMHF") // FHMM = 'Firehawk Mission Models'
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
					uint unk2 = stream.ReadU32BE();

					if (unk2 == 1)
					{
						uint unk3 = stream.ReadU32BE();
						string name = stream.ReadASCIIZ(1024);

						string path;
						path = Path.Combine("tests", "unknown");
						Directory.CreateDirectory(path);
						path = Path.Combine(path, Path.ChangeExtension(name, ".bin"));

						Stream output = File.OpenWrite(path);
						byte[] data = new byte[next - stream.Position];
						stream.Read(data, 0, data.Length);
						output.Write(data, 0, data.Length);
						output.Flush();
						output.Close();
					}
					else
					{
						throw new Exception();
					}
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
