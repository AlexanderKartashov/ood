namespace command.storage
{
	public interface IFileStorage
	{
		IResource Add(string path);

		void Remove(IResource resource);

		string Path { get; }
	}
}
