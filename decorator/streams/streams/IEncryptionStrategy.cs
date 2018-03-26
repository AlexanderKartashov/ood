using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace streams
{
	public interface IEncryptionStrategy
	{
		byte Encrypt(byte value);
		byte Decrypt(byte value);
	}
}
