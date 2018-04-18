using command.document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace command.storage
{
	public interface IFileStorage
	{
		IResource Add(string path);
		void Remove(IResource resource);
		string Path { get; }
	}
}
