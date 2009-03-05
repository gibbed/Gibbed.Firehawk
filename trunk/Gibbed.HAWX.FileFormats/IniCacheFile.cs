using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Gibbed.HAWX.Helpers;

namespace Gibbed.HAWX.FileFormats
{
	namespace IniTypes
	{
		public abstract class Base
		{
			public abstract void Read(Stream stream);
			public abstract void Write(Stream stream);
		}

		public class Boolean : Base
		{
			public bool Value;

			public override string ToString()
			{
				return this.Value.ToString(CultureInfo.InvariantCulture).ToLowerInvariant();
			}

			public override void Read(Stream stream)
			{
				this.Value = stream.ReadBoolean();
			}

			public override void Write(Stream stream)
			{
				throw new NotImplementedException();
			}
		}

		public class Integer : Base
		{
			public int Value;

			public override string ToString()
			{
				return this.Value.ToString(CultureInfo.InvariantCulture);
			}

			public override void Read(Stream stream)
			{
				this.Value = stream.ReadS32BE();
			}

			public override void Write(Stream stream)
			{
				throw new NotImplementedException();
			}
		}

		public class Color : Base
		{
			public uint Value;

			public override string ToString()
			{
				return "0x" + this.Value.ToString("X8", CultureInfo.InvariantCulture);
			}

			public override void Read(Stream stream)
			{
				this.Value = stream.ReadU32BE();
				//this.Value = ((this.Value >> 8) & 0xFFFFFF) | ((this.Value & 0xFF) << 24);
			}

			public override void Write(Stream stream)
			{
				throw new NotImplementedException();
			}
		}

		public class Float : Base
		{
			public float Value;

			public override string ToString()
			{
				return this.Value.ToString(CultureInfo.InvariantCulture) + "f";
			}

			public override void Read(Stream stream)
			{
				this.Value = stream.ReadF32BE();
			}

			public override void Write(Stream stream)
			{
				throw new NotImplementedException();
			}
		}

		public class Position : Base
		{
			public float X;
			public float Y;

			public override string ToString()
			{
				return
					this.X.ToString(CultureInfo.InvariantCulture) + ", " +
					this.Y.ToString(CultureInfo.InvariantCulture);
			}

			public override void Read(Stream stream)
			{
				this.X = stream.ReadF32BE();
				this.Y = stream.ReadF32BE();
			}

			public override void Write(Stream stream)
			{
				throw new NotImplementedException();
			}
		}

		public class Vector : Base
		{
			public float X;
			public float Y;
			public float Z;

			public override string ToString()
			{
				return
					this.X.ToString(CultureInfo.InvariantCulture) + ", " +
					this.Y.ToString(CultureInfo.InvariantCulture) + ", " +
					this.Z.ToString(CultureInfo.InvariantCulture);
			}

			public override void Read(Stream stream)
			{
				this.X = stream.ReadF32BE();
				this.Y = stream.ReadF32BE();
				this.Z = stream.ReadF32BE();
			}

			public override void Write(Stream stream)
			{
				throw new NotImplementedException();
			}
		}

		public class Rectangle : Base
		{
			public float Top;
			public float Left;
			public float Bottom;
			public float Right;

			public override string ToString()
			{
				return
					this.Top.ToString(CultureInfo.InvariantCulture) + ", " +
					this.Left.ToString(CultureInfo.InvariantCulture) + ", " +
					this.Bottom.ToString(CultureInfo.InvariantCulture) + ", " +
					this.Right.ToString(CultureInfo.InvariantCulture);
			}

			public override void Read(Stream stream)
			{
				this.Top = stream.ReadF32BE();
				this.Left = stream.ReadF32BE();
				this.Bottom = stream.ReadF32BE();
				this.Right = stream.ReadF32BE();
			}

			public override void Write(Stream stream)
			{
				throw new NotImplementedException();
			}
		}

		public class String : Base
		{
			public string Value;

			public override string  ToString()
			{
				return "'" + this.Value.Replace("'", "\\'") + "'";
			}

			public override void Read(Stream stream)
			{
 				this.Value = stream.ReadASCIIZ();
			}

			public override void Write(Stream stream)
			{
				throw new NotImplementedException();
			}
		}
	}

	public class IniCacheCachedFile
	{
		public string Name;
		public UInt32 Offset;
		public UInt32 Size;
		public Dictionary<string, Dictionary<string, object>> Sections = new Dictionary<string, Dictionary<string, object>>();
	}

	public class IniCacheFile
	{
		public List<IniCacheCachedFile> CachedFiles = new List<IniCacheCachedFile>();

		public void Read(Stream stream)
		{
			this.CachedFiles.Clear();

			int count = stream.ReadS32BE();

			for (int i = 0; i < count; i++)
			{
				IniCacheCachedFile entry = new IniCacheCachedFile();
				entry.Name = stream.ReadASCIIZ();
				entry.Offset = stream.ReadU32BE();
				entry.Size = stream.ReadU32BE();
				this.CachedFiles.Add(entry);
			}

			foreach (IniCacheCachedFile entry in this.CachedFiles)
			{
				stream.Seek(entry.Offset, SeekOrigin.Begin);

				Dictionary<string, object> section = null;
				while (stream.Position < entry.Offset + entry.Size)
				{
					byte keyType = stream.ReadU8();
					if (keyType == 0) // section
					{
						section = new Dictionary<string, object>();
						string name = stream.ReadASCIIZ();
						entry.Sections[name] = section;
					}
					else if (keyType == 1) // key
					{
						string name = stream.ReadASCIIZ();
						IniTypes.Base value = null;

						switch (stream.ReadU8())
						{
							case 0: value = new IniTypes.Integer(); break;
							case 2: value = new IniTypes.Float(); break;
							case 4: value = new IniTypes.String(); break;
							case 10: value = new IniTypes.Vector(); break;
							case 14: value = new IniTypes.Boolean(); break;
							case 16: value = new IniTypes.Rectangle(); break;
							case 18: value = new IniTypes.Position(); break;
							case 20: value = new IniTypes.Color(); break;
							default: throw new Exception();
						}

						value.Read(stream);
						section[name] = value;
					}
					else
					{
						throw new Exception();
					}
				}
			}
		}

		public void Write(Stream stream)
		{
			throw new NotImplementedException();
		}
	}
}
