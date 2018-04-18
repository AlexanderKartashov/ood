using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using command.externals;

namespace command.storage
{
	public class FileStorage : IFileStorage, IDisposable
	{
		private readonly string _tempFolder;
		private readonly IFileSystem _fileSystem;
		private readonly IList<IResource> _resources = new List<IResource>();

		public FileStorage(IFileSystem fileSystem)
		{
			_fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));

			_tempFolder = _fileSystem.GetTempDirectoryPath();
			_fileSystem.CreateDirectory(_tempFolder);
		}

		public IResource Add(string path)
		{
			var newTempFilePath = _fileSystem.CombinePath(_tempFolder, _fileSystem.GetRandomFileName("file"));
			newTempFilePath = _fileSystem.ChangeExtension(newTempFilePath, _fileSystem.GetExtension(newTempFilePath));
			_fileSystem.CopyFiles(path, newTempFilePath);
			var newResource = new Resource(newTempFilePath, this, _fileSystem);
			_resources.Add(newResource);
			return newResource;
		}

		public void Dispose()
		{
			foreach(var res in _resources)
			{
				res.Dispose();
			}
			_resources.Clear();
			_fileSystem.DeleteDirectory(_tempFolder);
		}

		public string Path { get => _tempFolder; }

		public void Remove(IResource resource)
		{
			resource.Dispose();
			_resources.Remove(resource);
		}
	}
}
