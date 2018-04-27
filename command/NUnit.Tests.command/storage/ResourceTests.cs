using NUnit.Framework;
using command.storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using command.externals;
using NUnit.Framework.Constraints;
using NSubstitute;

namespace command.storage.Tests
{
	[TestFixture]
	[Parallelizable]
	public class ResourceTests
	{
		[Test]
		public void ResourceTest([Values(null, "path")] string filePath, [ValueSource("Storages")] IFileStorage storage, [ValueSource("FileSystems")] IFileSystem fileSystem)
		{
			IResolveConstraint constraint = (filePath != null && storage != null && fileSystem != null) ? (IResolveConstraint)Throws.Nothing : (IResolveConstraint)Throws.ArgumentNullException;
			if (fileSystem != null)
			{
				fileSystem.FileExists(Arg.Any<string>()).Returns(true);
			}
			Assert.That(() => new Resource(filePath, storage, fileSystem), constraint);
		}

		[Test]
		public void ResourceTestInit()
		{
			string filePath = "path";
			var storage = Substitute.For<IFileStorage>();
			var fileSystem = Substitute.For<IFileSystem>();
			fileSystem.FileExists(Arg.Any<string>()).Returns(true);
			Assert.That(() => new Resource(filePath, storage, fileSystem), Throws.Nothing);
		}

		[Test]
		public void ResourceTestInitFailed()
		{
			string filePath = "path";
			var storage = Substitute.For<IFileStorage>();
			var fileSystem = Substitute.For<IFileSystem>();
			fileSystem.FileExists(Arg.Any<string>()).Returns(false);
			Assert.That(() => new Resource(filePath, storage, fileSystem), Throws.ArgumentException);
		}

		[Test]
		public void RemoveTest()
		{
			string filePath = "path";
			var storage = Substitute.For<IFileStorage>();
			var fileSystem = Substitute.For<IFileSystem>();
			fileSystem.FileExists(Arg.Any<string>()).Returns(true);
			var resource = new Resource(filePath, storage, fileSystem);
			Assert.That(() => resource.Remove(), Throws.Nothing);
			fileSystem.Received(1).DeleteFile(Arg.Is(filePath));
		}

		[Test]
		public void FilePathTest()
		{
			string filePath = "path";
			var storage = Substitute.For<IFileStorage>();
			var fileSystem = Substitute.For<IFileSystem>();
			fileSystem.FileExists(Arg.Any<string>()).Returns(true);
			var resource = new Resource(filePath, storage, fileSystem);
			string fileName = "name";
			fileSystem.GetFileName(Arg.Is(filePath)).Returns(fileName);
			Assert.That(resource.FilePath, Is.EqualTo(fileName));
			fileSystem.Received(1).GetFileName(Arg.Is(filePath));
		}

		[Test]
		public void DisposeTest()
		{
			string filePath = "path";
			var storage = Substitute.For<IFileStorage>();
			var fileSystem = Substitute.For<IFileSystem>();
			fileSystem.FileExists(Arg.Any<string>()).Returns(true);
			var resource = new Resource(filePath, storage, fileSystem);
			Assert.That(() => resource.Dispose(), Throws.Nothing);
			storage.Received(1).Remove(Arg.Is(resource));
		}

		static readonly IFileStorage[] Storages = { null, Substitute.For<IFileStorage>() };
		static readonly IFileSystem[] FileSystems = { null, Substitute.For<IFileSystem>() };
	}
}