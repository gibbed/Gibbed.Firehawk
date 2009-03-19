using System;
using System.Collections;
using System.IO;
using System.Security.Cryptography;

namespace Gibbed.Firehawk.Helpers
{
	public class CRC32 : HashAlgorithm
	{
		protected static Hashtable CachedCRC32Tables;
		protected static bool _AutoCache;
	
		protected uint[] CRC32Table; 
		
		public static bool AutoCache
		{
			get { return _AutoCache; }
			set { _AutoCache = value; }
		}

		protected uint AllOnes;
		public uint CRC;

		static CRC32()
		{
			CachedCRC32Tables = Hashtable.Synchronized(new Hashtable());
			_AutoCache = true;
		}

		public static void ClearCache()
		{
			CachedCRC32Tables.Clear();
		}

		protected virtual uint[] BuildCRC32Table(uint polynomial)
		{
			uint crc;
			uint[] table = new uint[256];

			for (uint i = 0; i < 256; i++)
			{
				crc = i;
				for (int j = 8; j > 0; j--)
				{
					if((crc & 1) == 1)
						crc = (crc >> 1) ^ polynomial;
					else
						crc >>= 1;
				}
				table[i] = crc;
			}

			return table;
		}

		public CRC32()
			: this(0xEDB88320)
		{
		}

		public CRC32(uint polynomial) : this(polynomial, CRC32.AutoCache)
		{
		}
		
		public CRC32(uint polymonial, bool cacheTable)
		{
			this.HashSizeValue = 32;

			this.CRC32Table = (uint[])CachedCRC32Tables[polymonial];
			if (this.CRC32Table == null)
			{
				this.CRC32Table = this.BuildCRC32Table(polymonial);
				
				if (cacheTable)
				{
					CachedCRC32Tables.Add(polymonial, this.CRC32Table);
				}
			}

			this.Initialize();
		}
		
		public override void Initialize()
		{
			this.AllOnes = 0xFFFFFFFF;
			this.CRC = 0xFFFFFFFF;
		}
		
		protected override void HashCore(byte[] buffer, int offset, int count)
		{
			// Save the text in the buffer. 
			for (int i = offset; i < count; i++)
			{
				ulong tabPtr = (this.CRC & 0xFF) ^ buffer[i];
				this.CRC >>= 8;
				this.CRC ^= this.CRC32Table[tabPtr];
			}
		}
	
		protected override byte[] HashFinal()
		{
			return BitConverter.GetBytes(this.CRC ^ AllOnes);
		}
	
		new public byte[] ComputeHash(Stream inputStream)
		{
			byte[] buffer = new byte[4096];
			int bytesRead;

			while ((bytesRead = inputStream.Read(buffer, 0, 4096)) > 0)
			{
				this.HashCore(buffer, 0, bytesRead);
			}

			return this.HashFinal();
		}

		new public byte[] ComputeHash(byte[] buffer)
		{
			return this.ComputeHash(buffer, 0, buffer.Length);
		}
	
		new public byte[] ComputeHash(byte[] buffer, int offset, int count)
		{
			this.HashCore(buffer, offset, count);
			return this.HashFinal();
		}
	}

	public class ProfileCRC32 : CRC32
	{
		public ProfileCRC32()
			: base(0x04C11DB7)
		{
		}

		public ProfileCRC32(uint polynomial)
			: base(polynomial, CRC32.AutoCache)
		{
		}

		public ProfileCRC32(uint polymonial, bool cacheTable)
			: base(polymonial, cacheTable)
		{
		}

		protected override void HashCore(byte[] buffer, int offset, int count)
		{
			// Save the text in the buffer. 
			for (int i = offset; i < count; i++)
			{
				this.CRC = this.CRC32Table[this.CRC >> 24] ^ ((this.CRC << 8) | buffer[i]);
			}
		}

		protected override uint[] BuildCRC32Table(uint polynomial)
		{
			uint crc;
			uint[] table = new uint[256];

			// 256 values representing ASCII character codes. 
			for (uint i = 0; i < 256; i++)
			{
				crc = i << 24;
				for (int j = 0; j < 8; j++)
				{
					if ((crc & (0x80000000)) == 0x80000000)
						crc = (crc << 1) ^ polynomial;
					else
						crc <<= 1;
				}
				table[i] = crc;
			}

			return table;
		}
	}
}
