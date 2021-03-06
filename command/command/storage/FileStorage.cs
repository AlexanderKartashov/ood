﻿using System;
using System.Collections.Generic;
using System.Linq;
using command.externals;

namespace command.storage
{
	public class FileStorage : IFileStorage, IDisposable
	{
		private readonly string _tempFolder;
		private readonly IFileSystem _fileSystem;
		private readonly IList<Resource> _resources = new List<Resource>();

		public FileStorage(IFileSystem fileSystem)
		{
			_fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));

			_tempFolder = _fileSystem.CombinePath(_fileSystem.GetTempDirectoryPath(), _fileSystem.GetRandomFileName("temp"));
			_fileSystem.CreateDirectory(_tempFolder);
		}

		public IResource Add(string path)
		{
			var absSrcPath = _fileSystem.IsAbsPath(path) ? path : _fileSystem.GetAbsPath(_fileSystem.GetDirectoryPath(_fileSystem.GetApplicationPath()), path);

			var newTempFilePath = _fileSystem.CombinePath(_tempFolder, _fileSystem.GetRandomFileName("file"));
			newTempFilePath = _fileSystem.ChangeExtension(newTempFilePath, _fileSystem.GetExtension(absSrcPath));
			_fileSystem.CopyFiles(absSrcPath, newTempFilePath);
			var newResource = new Resource(newTempFilePath, this, _fileSystem);
			_resources.Add(newResource);
			return newResource;
		}

		public void Dispose()
		{
			foreach(var res in _resources)
			{
				res.Remove();
			}
			_resources.Clear();
			_fileSystem.DeleteDirectory(_tempFolder);
		}

		public string Path { get => _tempFolder; }

		public void Remove(IResource resource)
		{
			var resInList = _resources.First(x => x.Equals(resource));
			if (resInList != null)
			{
				resInList.Remove();
				_resources.Remove(resInList);
			}
		}
	}
}
