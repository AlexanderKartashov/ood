using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace streams
{
	public class EncryptionWithReplacementTable : IEncryptionStrategy
	{
		private readonly Dictionary<byte, byte> _replacementTable;

		public EncryptionWithReplacementTable(Dictionary<byte, byte> replacementTable) => _replacementTable = replacementTable ?? throw new ArgumentNullException(nameof(replacementTable));

		public byte Decrypt(byte value)
		{
			return _replacementTable.ContainsValue(value)
				? _replacementTable.First(x => x.Value == value).Key
				: throw new ArgumentException(value.ToString());
		}

		public byte Encrypt(byte value)
		{
			return _replacementTable.ContainsKey(value)
				? _replacementTable[value]
				: throw new KeyNotFoundException(value.ToString());
		}
	}
}
