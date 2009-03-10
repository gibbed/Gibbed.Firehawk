using System;
using System.IO;
using Gibbed.Firehawk.FileFormats;

namespace Gibbed.Firehawk.Tester
{
	class Program
	{
		static void Main(string[] args)
		{
			MissionModelsFile missionModels = new MissionModelsFile();
			
			Stream input = File.OpenRead("entities.mdl");
			missionModels.Read(input);
			input.Close();
		}
	}
}
