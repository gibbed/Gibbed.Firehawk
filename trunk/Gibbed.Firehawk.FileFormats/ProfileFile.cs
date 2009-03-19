using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Gibbed.Firehawk.Helpers;

namespace Gibbed.Firehawk.FileFormats
{
	public class ModelProfileData
	{
		public string PlayerModel;
		public string WeaponPackModel;
	}

	public class InputProfileData
	{
		public int InvertYAxis;
		public int InvertCamera;
		public int InvertCameraX;
		public int InvertThrottle;
		public int InvertYaw;
		public int GameplayMouseDisable;
		public int CurrentScheme;
		public int EnableExpertControls;
	}

	public class HudProfileData
	{
		public int RenderTheHudOption;
		public int MeasurementsStyle;
		public int Subtitles;
	}

	public class MenusProfileData
	{
		public int HasPlayedOffMode;
	}

	public class SoundProfileData
	{
		public float Music;
		public float Voices;
		public float MasterSFX;
	}

	public class VoiceCommProfileData
	{
		public int DefaultState;
		public int ControlMethod;
	}

	public class ConfigProfileData
	{
		public int DifficultyLevel;
		public InputProfileData Input = new InputProfileData();
		public string CameraMode;
		public HudProfileData Hud = new HudProfileData();
		public MenusProfileData Menus = new MenusProfileData();
		public SoundProfileData Sound = new SoundProfileData();
		public VoiceCommProfileData VoiceComm = new VoiceCommProfileData();
	}

	public class MissionProfileData
	{
		public int ActiveInCampaign;
		public int Invisible;
		public int UnlockMode;
		public int[] Unknown04;

		public void Read(Stream stream)
		{
			this.ActiveInCampaign = stream.ReadS32();
			this.Invisible = stream.ReadS32();
			this.UnlockMode = stream.ReadS32();

			int count = stream.ReadS32();
			this.Unknown04 = new int[count];
			for (int i = 0; i < count; i++)
			{
				throw new Exception();
				this.Unknown04[i] = stream.ReadS32();
			}
		}

		public void Write(Stream stream)
		{
			stream.WriteS32(this.ActiveInCampaign);
			stream.WriteS32(this.Invisible);
			stream.WriteS32(this.UnlockMode);

			stream.WriteS32(this.Unknown04.Length);
			for (int i = 0; i < this.Unknown04.Length; i++)
			{
				stream.WriteS32(this.Unknown04[i]);
			}
		}
	}

	public class ChallengeProfileData
	{
		public int Unknown01;
		public int Unknown02;

		public void Read(Stream stream)
		{
			this.Unknown01 = stream.ReadS32();
			this.Unknown02 = stream.ReadS32(); // always little-endian
		}

		public void Write(Stream stream)
		{
			stream.WriteS32(this.Unknown01);
			stream.WriteS32(this.Unknown02); // always little-endian
		}
	}

	public class PlaneProfileData
	{
		public int UnlockMode;
		public int SpecialUnlockMode;
		public int UnlockSkin;
		public int Unknown04;

		public int Unknown05;
		public int Unknown06;
		public int Unknown07;
		public int Unknown08;
		public int Unknown09;
		public int Unknown10;
		public int Unknown11;
		public int Unknown12;

		public int[] SPPacks;
		public int[] MPPacks;

		public void Read(Stream stream, int planeIndex)
		{
			Gibbed.Firehawk.PlaneInformation details = Gibbed.Firehawk.GameInformation.Planes[planeIndex];

			this.UnlockMode = stream.ReadS32();
			this.SpecialUnlockMode = stream.ReadS32();
			this.UnlockSkin = stream.ReadS32(); // always little-endian
			this.Unknown04 = stream.ReadS32();

			this.Unknown05 = stream.ReadS32(); // always little-endian
			this.Unknown06 = stream.ReadS32(); // always little-endian
			this.Unknown07 = stream.ReadS32(); // always little-endian
			this.Unknown08 = stream.ReadS32(); // always little-endian
			this.Unknown09 = stream.ReadS32(); // always little-endian
			this.Unknown10 = stream.ReadS32(); // always little-endian
			this.Unknown11 = stream.ReadS32(); // always little-endian
			this.Unknown12 = stream.ReadS32(); // always little-endian

			// singleplayer weapon packs
			this.SPPacks = new int[details.SPPacks];
			for (int i = 0; i < this.SPPacks.Length; i++)
			{
				this.SPPacks[i] = stream.ReadS32();
			}

			// multiplayer weapon packs
			this.MPPacks = new int[details.MPPacks];
			for (int i = 0; i < this.MPPacks.Length; i++)
			{
				this.MPPacks[i] = stream.ReadS32();
			}
		}

		public void Write(Stream stream, int planeIndex)
		{
			stream.WriteS32(this.UnlockMode);
			stream.WriteS32(this.SpecialUnlockMode);
			stream.WriteS32(this.UnlockSkin); // always little-endian
			stream.WriteS32(this.Unknown04);

			stream.WriteS32(this.Unknown05); // always little-endian
			stream.WriteS32(this.Unknown06); // always little-endian
			stream.WriteS32(this.Unknown07); // always little-endian
			stream.WriteS32(this.Unknown08); // always little-endian
			stream.WriteS32(this.Unknown09); // always little-endian
			stream.WriteS32(this.Unknown10); // always little-endian
			stream.WriteS32(this.Unknown11); // always little-endian
			stream.WriteS32(this.Unknown12); // always little-endian

			// singleplayer weapon packs
			for (int i = 0; i < this.SPPacks.Length; i++)
			{
				stream.WriteS32(this.SPPacks[i]);
			}

			// multiplayer weapon packs
			for (int i = 0; i < this.MPPacks.Length; i++)
			{
				stream.WriteS32(this.MPPacks[i]);
			}
		}
	}

	public class UnknownProfileData
	{
		public int Unknown01;
		public int Unknown02;
		public int Unknown03;
		public int Unknown04;
		public int Unknown05;

		public void Read(Stream stream)
		{
			this.Unknown01 = stream.ReadS32();
			this.Unknown02 = stream.ReadS32();
			this.Unknown03 = stream.ReadS32();
			this.Unknown04 = stream.ReadS32();
			this.Unknown05 = stream.ReadS32();
		}

		public void Write(Stream stream)
		{
			stream.WriteS32(this.Unknown01);
			stream.WriteS32(this.Unknown02);
			stream.WriteS32(this.Unknown03);
			stream.WriteS32(this.Unknown04);
			stream.WriteS32(this.Unknown05);
		}
	}

	public class ProfileFile
	{
		public ModelProfileData Model = new ModelProfileData();
		public ConfigProfileData Config = new ConfigProfileData();

		public int Cheated;
		public int Unknown02;
		public int Unknown03;
		public int Unknown04;

		public MissionProfileData[] Missions = new MissionProfileData[19];
		public PlaneProfileData[] Planes = new PlaneProfileData[67];
		public ChallengeProfileData[] Challenges = new ChallengeProfileData[303];

		public UnknownProfileData[] Unknown05;
		public int[] Unknown06;
		public byte[] Unknown07;

		public int Unknown08;
		public int Unknown09;
		public int Unknown10;

		public int XP;
		public int Unknown11;
		public int Unknown12;
		public int Unknown13;
		public string Callsign;
		public int Unknown14;

		public void Read(Stream stream)
		{
			stream.ReadU32(); // crc

			int version = stream.ReadS32();
			if (version != 82)
			{
				throw new Exception();
			}

			if (stream.ReadU32() != 0) // 
			{
				throw new Exception();
			}

			// Configuration
			this.Model.PlayerModel = stream.ReadASCIIZ(260);
			this.Model.WeaponPackModel = stream.ReadASCIIZ(260);
			this.Config.DifficultyLevel = stream.ReadS32();
			this.Config.Input.InvertYAxis = stream.ReadS32();
			this.Config.Input.InvertCamera = stream.ReadS32();
			this.Config.Input.InvertCameraX = stream.ReadS32();
			this.Config.Input.InvertThrottle = stream.ReadS32();
			this.Config.Input.InvertYaw = stream.ReadS32();
			this.Config.CameraMode = stream.ReadASCIIZ(32);
			this.Config.Hud.RenderTheHudOption = stream.ReadS32();
			this.Config.Input.GameplayMouseDisable = stream.ReadS32();
			this.Config.Hud.MeasurementsStyle = stream.ReadS32();
			this.Config.Hud.Subtitles = stream.ReadS32();
			this.Config.Menus.HasPlayedOffMode = stream.ReadS32();
			this.Config.Input.CurrentScheme = stream.ReadS32();
			this.Config.Input.EnableExpertControls = stream.ReadS32();
			this.Config.Sound.Music = stream.ReadF32();
			this.Config.Sound.Voices = stream.ReadF32();
			this.Config.Sound.MasterSFX = stream.ReadF32();
			this.Config.VoiceComm.DefaultState = stream.ReadS32();
			this.Config.VoiceComm.ControlMethod = stream.ReadS32();

			// Onward, ho!
			this.Cheated = stream.ReadS32();
			this.Unknown02 = stream.ReadS32();
			this.Unknown03 = stream.ReadS32();
			this.Unknown04 = stream.ReadS32();

			/* There are 19 missions in retail HAWX. Unfortunately this design means
			 * if the mission count *ever* changes, the profiles become corrupt (argh). */
			for (int i = 0; i < this.Missions.Length; i++)
			{
				this.Missions[i] = new MissionProfileData();
				this.Missions[i].Read(stream);
			}

			/* There are 67 planes in retail HAWX. Unfortunately this design means
			 * if the plane count *ever* changes, the profiles become corrupt (argh). */
			for (int i = 0; i < this.Planes.Length; i++)
			{
				this.Planes[i] = new PlaneProfileData();
				this.Planes[i].Read(stream, i);
			}

			/* There are 303 challenges in retail HAWX. Unfortunately this design means
			 * if the challenge count *ever* changes, the profiles become corrupt (argh). */
			for (int i = 0; i < this.Challenges.Length; i++)
			{
				this.Challenges[i] = new ChallengeProfileData();
				this.Challenges[i].Read(stream);
			}

			int count = stream.ReadS32();
			this.Unknown05 = new UnknownProfileData[count];
			for (int i = 0; i < count; i++)
			{
				this.Unknown05[i] = new UnknownProfileData();
				this.Unknown05[i].Read(stream);
			}

			int unknownSize = stream.ReadS32();
			this.Unknown06 = new int[(int)Math.Floor((double)((unknownSize / 32) + 1))];
			for (int i = 0; i < this.Unknown06.Length; i++)
			{
				this.Unknown06[i] = stream.ReadS32();
			}

			this.Unknown07 = new byte[unknownSize];
			stream.Read(this.Unknown07, 0, unknownSize);
			this.Unknown08 = stream.ReadS32();
			this.Unknown09 = stream.ReadS32();
			this.Unknown10 = stream.ReadS32();

			this.XP = stream.ReadS32();
			this.Unknown11 = stream.ReadS32();
			this.Unknown12 = stream.ReadS32();
			this.Unknown13 = stream.ReadS32();
			this.Callsign = stream.ReadUTF16LEZ(12);
			this.Unknown14 = stream.ReadS32();
		}

		public void Write(Stream stream)
		{
			stream.WriteU32(0); // crc

			stream.WriteS32(82); // version

			stream.WriteU32(0);

			// Configuration
			stream.WriteASCIIZ(this.Model.PlayerModel, 260);
			stream.WriteASCIIZ(this.Model.WeaponPackModel, 260);
			stream.WriteS32(this.Config.DifficultyLevel);
			stream.WriteS32(this.Config.Input.InvertYAxis);
			stream.WriteS32(this.Config.Input.InvertCamera);
			stream.WriteS32(this.Config.Input.InvertCameraX);
			stream.WriteS32(this.Config.Input.InvertThrottle);
			stream.WriteS32(this.Config.Input.InvertYaw);
			stream.WriteASCIIZ(this.Config.CameraMode, 32);
			stream.WriteS32(this.Config.Hud.RenderTheHudOption);
			stream.WriteS32(this.Config.Input.GameplayMouseDisable);
			stream.WriteS32(this.Config.Hud.MeasurementsStyle);
			stream.WriteS32(this.Config.Hud.Subtitles);
			stream.WriteS32(this.Config.Menus.HasPlayedOffMode);
			stream.WriteS32(this.Config.Input.CurrentScheme);
			stream.WriteS32(this.Config.Input.EnableExpertControls);
			stream.WriteF32(this.Config.Sound.Music);
			stream.WriteF32(this.Config.Sound.Voices);
			stream.WriteF32(this.Config.Sound.MasterSFX);
			stream.WriteS32(this.Config.VoiceComm.DefaultState);
			stream.WriteS32(this.Config.VoiceComm.ControlMethod);

			// Onward, ho!
			stream.WriteS32(this.Cheated);
			stream.WriteS32(this.Unknown02);
			stream.WriteS32(this.Unknown03);
			stream.WriteS32(this.Unknown04);

			for (int i = 0; i < this.Missions.Length; i++)
			{
				this.Missions[i].Write(stream);
			}

			for (int i = 0; i < this.Planes.Length; i++)
			{
				this.Planes[i].Write(stream, i);
			}

			for (int i = 0; i < this.Challenges.Length; i++)
			{
				this.Challenges[i].Write(stream);
			}

			stream.WriteS32(this.Unknown05.Length);
			for (int i = 0; i < this.Unknown05.Length; i++)
			{
				this.Unknown05[i].Write(stream);
			}

			stream.WriteS32(this.Unknown07.Length);
			for (int i = 0; i < this.Unknown06.Length; i++)
			{
				stream.WriteS32(this.Unknown06[i]);
			}

			stream.Write(this.Unknown07, 0, this.Unknown07.Length);
			
			stream.WriteS32(this.Unknown08);
			stream.WriteS32(this.Unknown09);
			stream.WriteS32(this.Unknown10);

			stream.WriteS32(this.XP);
			stream.WriteS32(this.Unknown11);
			stream.WriteS32(this.Unknown12);
			stream.WriteS32(this.Unknown13);
			stream.WriteUTF16LEZ(this.Callsign, 12);
			stream.WriteS32(this.Unknown14);
		}
	}
}
