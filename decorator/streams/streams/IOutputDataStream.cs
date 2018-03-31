using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace streams
{
	public interface IOutputDataStream : IDisposable
	{
		// Записывает в поток данных байт
		// Выбрасывает исключение std::ios_base::failure в случае ошибки
		void WriteByte(byte data);

		// Записывает в поток блок данных размером size байт, 
		// располагающийся по адресу srcData,
		// В случае ошибки выбрасывает исключение std::ios_base::failure
		void WriteBlock(byte[] data);
	}
}
