using NUnit.Framework;
using command.storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using NSubstitute;
using command.externals;

namespace command.storage.Tests
{
	[TestFixture]
	[Parallelizable]
	public class FileStorageTests
	{
		[Test]
		public void FileStorageTest()
		{
			Assert.That(() => new FileStorage(null), Throws.ArgumentNullException);
			Assert.That(() => new FileStorage(Substitute.For<IFileSystem>()), Throws.Nothing);
		}

		[Test]
		public void AddTest()
		{
			var fs = Substitute.For<IFileSystem>();
			var tmp = "tmp";
			fs.GetTempDirectoryPath().Returns(tmp);
			fs.DirectoryExists(Arg.Any<string>()).Returns(true);
			fs.FileExists(Arg.Any<string>()).Returns(true);
			var storage = new FileStorage(fs);
			Received.InOrder(() => {
				fs.GetTempDirectoryPath();
				fs.GetRandomFileName(Arg.Any<string>());
				fs.CombinePath(Arg.Any<string[]>());
				fs.Received(1).CreateDirectory(Arg.Any<string>());
			});
			fs.ClearReceivedCalls();

			var path = "path";
			fs.ChangeExtension(Arg.Any<string>(), Arg.Any<string>()).Returns("path");
			IResource res = null;
			Assert.That(() => { res = storage.Add(path); }, Throws.Nothing);
			Assert.That(res, Is.Not.Null);

			Received.InOrder(() => {
				fs.GetRandomFileName(Arg.Any<string>());
				fs.CombinePath(Arg.Any<string[]>());
				fs.GetExtension(Arg.Any<string>());
				fs.ChangeExtension(Arg.Any<string>(), Arg.Any<string>());
				fs.CopyFiles(Arg.Any<string>(), Arg.Any<string>());
			});
		}

		[Test]
		public void RemoveTest()
		{
			var fs = Substitute.For<IFileSystem>();
			var tmp = "tmp";
			fs.GetTempDirectoryPath().Returns(tmp);
			fs.DirectoryExists(Arg.Any<string>()).Returns(true);
			fs.FileExists(Arg.Any<string>()).Returns(true);
			var storage = new FileStorage(fs);
			fs.Received(1).CreateDirectory(Arg.Any<string>());
			var path = "path";
			fs.ChangeExtension(Arg.Any<string>(), Arg.Any<string>()).Returns("path");
			IResource res = null;
			Assert.That(() => { res = storage.Add(path); }, Throws.Nothing);
			Assert.That(res, Is.Not.Null);
			Assert.That(() => storage.Remove(res), Throws.Nothing);
			fs.Received(1).DeleteFile(Arg.Any<string>());
		}

		[Test]
		public void DisposeTest()
		{
			var fs = Substitute.For<IFileSystem>();
			var tmp = "tmp";
			fs.GetTempDirectoryPath().Returns(tmp);
			fs.DirectoryExists(Arg.Any<string>()).Returns(true);
			fs.FileExists(Arg.Any<string>()).Returns(true);
			var storage = new FileStorage(fs);
			fs.Received(1).CreateDirectory(Arg.Any<string>());
			var path = "path";
			fs.ChangeExtension(Arg.Any<string>(), Arg.Any<string>()).Returns("path");
			IResource res = null;
			Assert.That(() => { res = storage.Add(path); }, Throws.Nothing);
			Assert.That(res, Is.Not.Null);
			Assert.That(() => storage.Dispose(), Throws.Nothing);
			fs.Received(1).DeleteDirectory(Arg.Any<string>());
		}
	}
}