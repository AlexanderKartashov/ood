using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace streams
{
	public interface IEncriptionStrategy
	{
		byte Encript(byte value);
		byte Decript(byte value);
	}
}
