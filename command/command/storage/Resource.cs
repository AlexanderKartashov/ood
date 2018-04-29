using System;
using command.externals;

namespace command.storage
{
	public class Resource : IResource
	{
		private readonly string _filePath;
		private readonly IFileStorage _storage;
		private readonly IFileSystem _fileSystem;

		public Resource(string filePath, IFileStorage storage, IFileSystem fileSystem)
		{
			_filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
			_storage = storage ?? throw new ArgumentNullException(nameof(storage));
			_fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));

			if (!_fileSystem.FileExists(filePath))
			{
				throw new ArgumentException($"File {filePath} not exists");
			}
		}

		public string FilePath => _fileSystem.GetFileName(_filePath);

		public void Dispose() => _storage.Remove(this);

		public void Remove() => _fileSystem.DeleteFile(_filePath);
	}
}
