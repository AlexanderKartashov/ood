using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace streams
{
	interface IInputDataStream
	{
		// Возвращает признак достижения конца данных потока
		// Выбрасывает исключение std::ios_base::failuer в случае ошибки
		bool IsEOF();

		// Считывает байт из потока. 
		// Выбрасывает исключение std::ios_base::failure в случае ошибки
		byte ReadByte();

		// Считывает из потока блок данных размером size байт, записывая его в память
		// по адресу dstBuffer
		// Возвращает количество реально прочитанных байт. Выбрасывает исключение в случае ошибки
		void ReadBlock(byte[] buffer);
	}
}
