using System;

namespace command.storage
{
	public interface IResource : IDisposable
	{
		string FilePath { get; }
	}
}
