using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gibbed.Firehawk
{
	public class PlaneInformation
	{
		public string Name;
		public int SPPacks;
		public int MPPacks;
		public bool HasSkin;

		public PlaneInformation(string name, int sppacks, int mppacks, bool hasskin)
		{
			this.Name = name;
			this.SPPacks = sppacks;
			this.MPPacks = mppacks;
			this.HasSkin = hasskin;
		}
	}

	public static class GameInformation
	{
		static GameInformation()
		{
			Planes = new PlaneInformation[67];
			Planes[0] = new PlaneInformation("FA22", 3, 4, true);
			Planes[1] = new PlaneInformation("SU47", 3, 3, true);
			Planes[2] = new PlaneInformation("EF2000", 3, 3, true);
			Planes[3] = new PlaneInformation("F35JSF", 3, 3, true);
			Planes[4] = new PlaneInformation("RAFALEM", 3, 3, true);
			Planes[5] = new PlaneInformation("SU37", 3, 3, true);
			Planes[6] = new PlaneInformation("MIG142", 3, 3, false);
			Planes[7] = new PlaneInformation("YF23", 3, 3, true);
			Planes[8] = new PlaneInformation("RAFALEC", 3, 3, false);
			Planes[9] = new PlaneInformation("SU35", 3, 3, true);
			Planes[10] = new PlaneInformation("F15ACTIVE", 3, 3, false);
			Planes[11] = new PlaneInformation("MIRAGE4000", 3, 3, false);
			Planes[12] = new PlaneInformation("RF15", 3, 4, false);
			Planes[13] = new PlaneInformation("SU27", 3, 3, true);
			Planes[14] = new PlaneInformation("F18HARV", 3, 3, false);
			Planes[15] = new PlaneInformation("SAAB-39Gripen", 3, 4, true);
			Planes[16] = new PlaneInformation("X29A", 3, 3, false);
			Planes[17] = new PlaneInformation("SU34", 3, 3, false);
			Planes[18] = new PlaneInformation("FA-18E", 3, 4, true);
			Planes[19] = new PlaneInformation("F/A18C", 3, 3, true);
			Planes[20] = new PlaneInformation("F15E", 3, 3, false);
			Planes[21] = new PlaneInformation("F16CD", 3, 4, true);
			Planes[22] = new PlaneInformation("MIG33", 3, 3, false);
			Planes[23] = new PlaneInformation("MIRAGE2000_5", 3, 3, true);
			Planes[24] = new PlaneInformation("F15", 3, 3, false);
			Planes[25] = new PlaneInformation("F14A", 3, 3, true);
			Planes[26] = new PlaneInformation("F14D", 3, 3, false);
			Planes[27] = new PlaneInformation("MIG31", 3, 3, false);
			Planes[28] = new PlaneInformation("YF17", 3, 3, false);
			Planes[29] = new PlaneInformation("FB22", 3, 3, false);
			Planes[30] = new PlaneInformation("F16FF", 3, 3, false);
			Planes[31] = new PlaneInformation("AV8B-Harrier", 3, 3, true);
			Planes[32] = new PlaneInformation("F2", 3, 3, false);
			Planes[33] = new PlaneInformation("SAAB37", 3, 3, false);
			Planes[34] = new PlaneInformation("MIRAGE2000C", 3, 3, false);
			Planes[35] = new PlaneInformation("MIG-29", 3, 3, true);
			Planes[36] = new PlaneInformation("MIRAGEF1", 3, 3, false);
			Planes[37] = new PlaneInformation("MIRAGEIII", 3, 3, false);
			Planes[38] = new PlaneInformation("F20", 3, 3, false);
			Planes[39] = new PlaneInformation("F5E", 3, 3, false);
			Planes[40] = new PlaneInformation("MIG23", 3, 3, false);
			Planes[41] = new PlaneInformation("SAAB35", 3, 3, false);
			Planes[42] = new PlaneInformation("F5A", 3, 3, false);
			Planes[43] = new PlaneInformation("SR71", 3, 4, false);
			Planes[44] = new PlaneInformation("YF12A", 3, 4, false);
			Planes[45] = new PlaneInformation("A12", 3, 4, false);
			Planes[46] = new PlaneInformation("SEPECAT", 3, 3, false);
			Planes[47] = new PlaneInformation("MIRAGE_V", 3, 3, false);
			Planes[48] = new PlaneInformation("SU32", 3, 3, false);
			Planes[49] = new PlaneInformation("MIRAGE2000N", 3, 3, false);
			Planes[50] = new PlaneInformation("EF111A", 3, 4, false);
			Planes[51] = new PlaneInformation("F/A-18", 3, 4, false);
			Planes[52] = new PlaneInformation("MIG25", 3, 3, false);
			Planes[53] = new PlaneInformation("MIG21", 3, 3, false);
			Planes[54] = new PlaneInformation("A10", 3, 3, true);
			Planes[55] = new PlaneInformation("F117", 3, 3, true);
			Planes[56] = new PlaneInformation("F14B", 3, 3, false);
			Planes[57] = new PlaneInformation("MIRAGEIVP", 3, 3, false);
			Planes[58] = new PlaneInformation("F111F", 3, 3, false);
			Planes[59] = new PlaneInformation("F4E", 3, 3, false);
			Planes[60] = new PlaneInformation("F4G", 3, 4, false);
			Planes[61] = new PlaneInformation("EA6B", 3, 4, false);
			Planes[62] = new PlaneInformation("A6", 3, 3, false);
			Planes[63] = new PlaneInformation("A7", 3, 3, false);
			Planes[64] = new PlaneInformation("SU25", 3, 3, false);
			Planes[65] = new PlaneInformation("SU39", 3, 3, false);
			Planes[66] = new PlaneInformation("XA20", 3, 3, false);
		}

		public static PlaneInformation[] Planes;
	}
}
