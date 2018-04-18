using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace command.storage
{
	public interface IResource : IDisposable
	{
		string FilePath { get; }
		void Remove();
	}
}
