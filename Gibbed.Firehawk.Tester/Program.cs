using System;
using System.IO;
using Gibbed.Firehawk.FileFormats;
using Gibbed.Firehawk.Helpers;

namespace Gibbed.Firehawk.Tester
{
	class Program
	{
		static void Main(string[] args)
		{
			/*
			MissionModelsFile missionModels = new MissionModelsFile();
			Stream input = File.OpenRead("entities.mdl");
			missionModels.Read(input);
			input.Close();
			*/

			/*
			MissionEntitiesFile missionEntities = new MissionEntitiesFile();
			Stream input = File.OpenRead("mission.gbe");
			missionEntities.Read(input);
			input.Close();
			*/

			ProfileFile profile = new ProfileFile();
			Stream input = File.OpenRead("David");
			profile.Read(input);
			input.Close();

			for (int i = 0; i < profile.Missions.Length; i++)
			{
				profile.Missions[i].ActiveInCampaign = i == 0 ? 1 : 0;
				profile.Missions[i].Invisible = 0;
				profile.Missions[i].UnlockMode = 2;
			}

			for (int i = 0; i < profile.Planes.Length; i++)
			{
				profile.Planes[i].UnlockMode = 2;

				PlaneInformation details = Gibbed.Firehawk.GameInformation.Planes[i];

				if (details.Name == "RAFALEM" ||
					details.Name == "MIG142" ||
					details.Name == "MIRAGE4000" ||
					details.Name == "RF15" ||
					details.Name == "F18HARV" ||
					details.Name == "MIG31" ||
					details.Name == "FB22" ||
					details.Name == "SAAB37" ||
					details.Name == "MIG23" ||
					details.Name == "SAAB35" ||
					details.Name == "SR71" ||
					details.Name == "A12" ||
					details.Name == "MIRAGE2000N" ||
					details.Name == "F111F" ||
					details.Name == "F4E" ||
					details.Name == "SU39")
				{
					profile.Planes[i].SpecialUnlockMode = 1;
				}

				if (Gibbed.Firehawk.GameInformation.Planes[i].HasSkin)
				{
					profile.Planes[i].UnlockSkin = 1;
				}

				for (int j = 0; j < profile.Planes[i].SPPacks.Length; j++)
				{
					profile.Planes[i].SPPacks[j] = 2;
				}

				for (int j = 0; j < profile.Planes[i].MPPacks.Length; j++)
				{
					profile.Planes[i].MPPacks[j] = 2;
				}
			}

			// profile.Callsign = "Rick";

			Stream output = File.Open("David.new", FileMode.Create, FileAccess.ReadWrite);
			
			output.Seek(0, SeekOrigin.Begin);
			profile.Write(output);

			// Compute CRC of profile data

			output.Seek(4, SeekOrigin.Begin);
			uint allOnes = output.ReadU32BE();
			allOnes = ~allOnes;

			ProfileCRC32 crc = new ProfileCRC32();
			crc.CRC = allOnes;

			byte[] hash = crc.ComputeHash(output);

			output.Seek(0, SeekOrigin.Begin);
			output.Write(hash, 0, hash.Length);

			output.Close();
		}
	}
}
