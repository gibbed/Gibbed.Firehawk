using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Gibbed.Firehawk.FileFormats;

namespace Gibbed.Firehawk.ExtractCachedINIs
{
	class Program
	{
		static Dictionary<string, string> RealNames = new Dictionary<string, string>();
		private static void BuildRealNames()
		{
			RealNames["datavoicerecvoice"] = "Data\\VoiceRec\\voice.ini";
			RealNames["datavoicerecvocabscommands_uk"] = "Data\\VoiceRec\\Vocabs\\Commands_UK.ini";
			RealNames["datavoicerecvocabscommands_sp"] = "Data\\VoiceRec\\Vocabs\\Commands_SP.ini";
			RealNames["datavoicerecvocabscommands_ko"] = "Data\\VoiceRec\\Vocabs\\Commands_KO.ini";
			RealNames["datavoicerecvocabscommands_jp"] = "Data\\VoiceRec\\Vocabs\\Commands_JP.ini";
			RealNames["datavoicerecvocabscommands_it"] = "Data\\VoiceRec\\Vocabs\\Commands_IT.ini";
			RealNames["datavoicerecvocabscommands_ge"] = "Data\\VoiceRec\\Vocabs\\Commands_GE.ini";
			RealNames["datavoicerecvocabscommands_fr"] = "Data\\VoiceRec\\Vocabs\\Commands_FR.ini";
			RealNames["datavoicerecvocabscommands_en"] = "Data\\VoiceRec\\Vocabs\\Commands_EN.ini";
			RealNames["datascriptssu-47_berkut"] = "Data\\Scripts\\Su-47_Berkut.ini";
			RealNames["datascriptssu-37_terminator_cockpit"] = "Data\\Scripts\\Su-37_Terminator_Cockpit.ini";
			RealNames["datascriptssu-37_terminator"] = "Data\\Scripts\\Su-37_Terminator.ini";
			RealNames["datascriptssu-35_superflanker"] = "Data\\Scripts\\Su-35_SuperFlanker.ini";
			RealNames["datascriptsstall_fxs"] = "Data\\Scripts\\stall_fxs.ini";
			RealNames["datascriptsskin2_av-8b_harrier_ii"] = "Data\\Scripts\\skin2_AV-8B_Harrier_II.ini";
			RealNames["datascriptsreactor_fx"] = "Data\\Scripts\\reactor_fx.ini";
			RealNames["datascriptsmenu_su-35_superflanker"] = "Data\\Scripts\\menu_Su-35_SuperFlanker.ini";
			RealNames["datascriptsmenu_skin2_su-35_superflanker"] = "Data\\Scripts\\menu_skin2_Su-35_SuperFlanker.ini";
			RealNames["datascriptsmenu_skin2_f-16c_fightingfalcon"] = "Data\\Scripts\\menu_skin2_F-16C_FightingFalcon.ini";
			RealNames["datascriptsmenu_skin2_av-8b_harrier_ii"] = "Data\\Scripts\\menu_skin2_AV-8B_Harrier_II.ini";
			RealNames["datascriptsmenu_f-16c_fightingfalcon"] = "Data\\Scripts\\menu_F-16C_FightingFalcon.ini";
			RealNames["datascriptsmenu_f-16a_fightingfalcon"] = "Data\\Scripts\\menu_F-16A_FightingFalcon.ini";
			RealNames["datascriptsmenu_f-15_active"] = "Data\\Scripts\\menu_F-15_Active.ini";
			RealNames["datascriptsmenu_av-8b_harrier_ii"] = "Data\\Scripts\\menu_AV-8B_Harrier_II.ini";
			RealNames["datascriptsmaneuversystem"] = "Data\\Scripts\\ManeuverSystem.ini";
			RealNames["datascriptslight_shafts"] = "Data\\Scripts\\light_shafts.ini";
			RealNames["datascriptsjamming"] = "Data\\Scripts\\Jamming.ini";
			RealNames["datascriptshangar"] = "Data\\Scripts\\hangar.ini";
			RealNames["datascriptsfa-22_raptor_cockpit"] = "Data\\Scripts\\FA-22_Raptor_Cockpit.ini";
			RealNames["datascriptsfa-22_raptor"] = "Data\\Scripts\\FA-22_Raptor.ini";
			RealNames["datascriptsf-20_tigershark"] = "Data\\Scripts\\F-20_Tigershark.ini";
			RealNames["datascriptsf-16c_fightingfalcon_cockpit"] = "Data\\Scripts\\F-16C_FightingFalcon_Cockpit.ini";
			RealNames["datascriptsf-16c_fightingfalcon"] = "Data\\Scripts\\F-16C_FightingFalcon.ini";
			RealNames["datascriptsf-16c_brown"] = "Data\\Scripts\\F-16C_BROWN.ini";
			RealNames["datascriptsf-16a_fightingfalcon"] = "Data\\Scripts\\F-16A_FightingFalcon.ini";
			RealNames["datascriptsf-15_active_cockpit"] = "Data\\Scripts\\F-15_Active_Cockpit.ini";
			RealNames["datascriptsf-15_active"] = "Data\\Scripts\\F-15_Active.ini";
			RealNames["datascriptsf-15c_eagle_cockpit"] = "Data\\Scripts\\F-15C_Eagle_Cockpit.ini";
			RealNames["datascriptsdodgesystem"] = "Data\\Scripts\\DodgeSystem.ini";
			RealNames["datascriptscylinders"] = "Data\\Scripts\\Cylinders.ini";
			RealNames["datascriptsav-8b_harrier_ii_cockpit"] = "Data\\Scripts\\AV-8B_Harrier_II_Cockpit.ini";
			RealNames["datascriptsav-8b_harrier_ii"] = "Data\\Scripts\\AV-8B_Harrier_II.ini";
			RealNames["datamissionsterrainrio_banchmarksafepath"] = "Data\\missions\\TerrainRio_banchmark\\safepath.ini";
			RealNames["datamissionsterrainrio_banchmarkmission"] = "Data\\missions\\TerrainRio_banchmark\\mission.ini";
			RealNames["datamissionsterrainrio_banchmarkminimap"] = "Data\\missions\\TerrainRio_banchmark\\minimap.ini";
			RealNames["datamissionsterrainrio_banchmarkloadscreens"] = "Data\\missions\\TerrainRio_banchmark\\loadscreens.ini";
			RealNames["datamissionsterrainrio_banchmarkfxcustomize"] = "Data\\missions\\TerrainRio_banchmark\\FXCustomize.ini";
			RealNames["datamissionsterrainrio_banchmarkfx"] = "Data\\missions\\TerrainRio_banchmark\\FX.ini";
			RealNames["datamissionsterrainrio_banchmarkactors"] = "Data\\missions\\TerrainRio_banchmark\\actors.ini";
			RealNames["datamissionsrio_demosafepath"] = "Data\\missions\\Rio_Demo\\safepath.ini";
			RealNames["datamissionsrio_demomission"] = "Data\\missions\\Rio_Demo\\mission.ini";
			RealNames["datamissionsrio_demominimap"] = "Data\\missions\\Rio_Demo\\minimap.ini";
			RealNames["datamissionsrio_demoloadscreens"] = "Data\\missions\\Rio_Demo\\loadscreens.ini";
			RealNames["datamissionsrio_demofx"] = "Data\\missions\\Rio_Demo\\FX.ini";
			RealNames["datamissionsdemo_trainingsafepath"] = "Data\\missions\\Demo_training\\safepath.ini";
			RealNames["datamissionsdemo_trainingmission"] = "Data\\missions\\Demo_training\\mission.ini";
			RealNames["datamissionsdemo_trainingminimap"] = "Data\\missions\\Demo_training\\minimap.ini";
			RealNames["datamissionsdemo_trainingloadscreens"] = "Data\\missions\\Demo_training\\loadscreens.ini";
			RealNames["datamissionsdemo_trainingfx"] = "Data\\missions\\Demo_training\\FX.ini";
			RealNames["datamissionscommonmpload"] = "Data\\missions\\Common\\mpload.ini";
			RealNames["datamissionscommonfx"] = "Data\\Missions\\Common\\FX.ini";
			RealNames["datamenus2persistent_mainmenu_filelist"] = "Data\\Menus2\\persistent_mainmenu_filelist.ini";
			RealNames["datamenus2persistent_game_filelist_pc"] = "Data\\Menus2\\persistent_game_filelist_pc.ini";
			RealNames["datamenus2mainmenu_filelist_pc"] = "Data\\Menus2\\mainmenu_filelist_pc.ini";
			RealNames["datamenus2loadscreensloadscreens_filelist"] = "Data\\Menus2\\LoadScreens\\loadscreens_filelist.ini";
			RealNames["datamenus2lights"] = "Data\\Menus2\\lights.ini";
			RealNames["datamenus2hud_filelist"] = "Data\\Menus2\\hud_filelist.ini";
			RealNames["datamenus2gamemenu_filelist_pc"] = "Data\\Menus2\\gamemenu_filelist_pc.ini";
			RealNames["datamenus2filters"] = "Data\\Menus2\\filters.ini";
			RealNames["datafxsplumes"] = "Data\\Fxs\\plumes.ini";
			RealNames["datafxslens"] = "Data\\Fxs\\lens.ini";
			RealNames["datafxshaze"] = "Data\\Fxs\\haze.ini";
			RealNames["datafxsfilters"] = "Data\\Fxs\\filters.ini";
			RealNames["datafontsfonts"] = "Data\\fonts\\fonts.ini";
			RealNames["dataenvironmentrio_gewater"] = "Data\\Environment\\RIO_GE\\water.ini";
			RealNames["dataenvironmentrio_gesunshafts"] = "Data\\Environment\\RIO_GE\\sunshafts.ini";
			RealNames["dataenvironmentrio_geunits"] = "Data\\Environment\\RIO_GE\\units.ini";
			RealNames["dataenvironmentrio_gestatics"] = "Data\\Environment\\RIO_GE\\statics.ini";
			RealNames["dataenvironmentrio_gespecialbuildings"] = "Data\\Environment\\RIO_GE\\specialbuildings.ini";
			RealNames["dataenvironmentrio_gerio_ge"] = "Data\\Environment\\RIO_GE\\rio_ge.ini";
			RealNames["dataenvironmentrio_gerio_ers"] = "Data\\Environment\\RIO_GE\\rio_ers.ini";
			RealNames["dataenvironmentrio_gerio_clouds_texatlas"] = "Data\\Environment\\RIO_GE\\rio_clouds_texatlas.ini";
			RealNames["dataenvironmentrio_gerio_clouds"] = "Data\\Environment\\RIO_GE\\rio_clouds.ini";
			RealNames["dataenvironmentrio_gerain"] = "Data\\Environment\\RIO_GE\\rain.ini";
			RealNames["dataenvironmentrio_gehaze"] = "Data\\Environment\\RIO_GE\\haze.ini";
			RealNames["dataenvironmentrio_geforest"] = "Data\\Environment\\RIO_GE\\forest.ini";
			RealNames["dataenvironmentrio_gefilters"] = "Data\\Environment\\RIO_GE\\filters.ini";
			RealNames["dataenvironmentrio_gebuildings"] = "Data\\Environment\\RIO_GE\\buildings.ini";
			RealNames["datacamerassu35"] = "Data\\Cameras\\su35.ini";
			RealNames["datacamerasdefault"] = "Data\\Cameras\\default.ini";
			RealNames["configunlockinfo"] = "Config\\UnlockInfo.ini";
			RealNames["configrumble"] = "Config\\rumble.ini";
			RealNames["configpcconfig"] = "Config\\pc\\config.ini";
			RealNames["configmultiplayer"] = "Config\\multiplayer.ini";
			RealNames["configmovies"] = "Config\\movies.ini";
			RealNames["configmissionsinfo_pc"] = "Config\\MissionsInfo_pc.ini";
			RealNames["configmissionsinfo"] = "Config\\MissionsInfo.ini";
			RealNames["configgamemodes"] = "Config\\gamemodes.ini";
			RealNames["configdifflevels"] = "Config\\difflevels.ini";
			RealNames["datamissionsmp_riosafepath"] = "Data\\missions\\MP_Rio\\safepath.ini";
			RealNames["datamissionsmp_riomission"] = "Data\\missions\\MP_Rio\\mission.ini";
			RealNames["datamissionsmp_riominimap"] = "Data\\missions\\MP_Rio\\minimap.ini";
			RealNames["datamissionsmp_rioloadscreens"] = "Data\\missions\\MP_Rio\\loadscreens.ini";
			RealNames["datamissionsmp_riofxcustomize"] = "Data\\missions\\MP_Rio\\FXCustomize.ini";
			RealNames["datamissionsmp_riofx"] = "Data\\missions\\MP_Rio\\FX.ini";
			RealNames["datamissionsff_terrainriomission"] = "Data\\missions\\FF_TerrainRio\\mission.ini";
			RealNames["datamissionsff_terrainriominimap"] = "Data\\missions\\FF_TerrainRio\\minimap.ini";
			RealNames["datamissionsff_terrainrioloadscreens"] = "Data\\missions\\FF_TerrainRio\\loadscreens.ini";
			RealNames["datamissionsff_terrainriofxcustomize"] = "Data\\missions\\FF_TerrainRio\\FXCustomize.ini";

			// Sanity check... just to be sure I made no mistakes when making the list.
			/*
			foreach (KeyValuePair<string, string> name in RealNames)
			{
				string path = name.Value;
				path = Path.ChangeExtension(path, null);
				path = path.ToLowerInvariant();
				path = path.Replace("\\", "");

				if (name.Key != path)
				{
					throw new Exception();
				}
			}
			*/
		}

		public static void Main(string[] args)
		{
			BuildRealNames();

			Stream stream = File.OpenRead("inicache.bin");
			IniCacheFile ini = new IniCacheFile();
			ini.Read(stream);
			stream.Close();

			foreach (IniCacheCachedFile cachedFile in ini.CachedFiles)
			{
				if (RealNames.ContainsKey(cachedFile.Name) == false)
				{
					Console.WriteLine("Don't know the real name for '" + cachedFile.Name + "'");
					continue;
				}

				string realPath = Path.Combine("extracted", RealNames[cachedFile.Name]);

				Directory.CreateDirectory(Path.GetDirectoryName(realPath));
				StreamWriter writer = new StreamWriter(realPath, false, Encoding.ASCII);

				bool started = false;
				foreach (KeyValuePair<string, Dictionary<string, object>> section in cachedFile.Sections)
				{
					if (started == false)
					{
						started = true;
					}
					else
					{
						writer.WriteLine();
					}

					writer.WriteLine("[" + section.Key + "]");

					foreach (KeyValuePair<string, object> key in section.Value)
					{
						writer.WriteLine(key.Key + " = " + key.Value.ToString());
					}
				}

				writer.Flush();
				writer.Close();
			}
		}
	}
}
