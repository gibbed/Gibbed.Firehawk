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
		}

		public void Write(Stream stream)
		{
			throw new NotImplementedException();
		}
	}
}
