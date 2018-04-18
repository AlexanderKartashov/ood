using NUnit.Framework;
using command.storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace command.storage.Tests
{
	[TestFixture]
	[Parallelizable]
	public class FileStorageTests
	{
		[Test]
		public void FileStorageTest()
		{
			Assert.Fail();
			//Assert.That(() => new FileStorage(null), Throws.ArgumentNullException);
			//Assert.That(() => { using (var fileStorage = new FileStorage(FileStoragePathGenerator.GetTempRandomPath("1"))) { } }, Throws.Nothing);
		}

		[Test]
		public void AddTest()
		{
			Assert.Fail();
			//var path = Path.Combine(TestContext.CurrentContext.TestDirectory, "..\\..\\test.txt");
			//using (var fileStorage = new FileStorage(FileStoragePathGenerator.GetTempRandomPath("TMP")))
			//{
			//	IResource res = null;
			//	Assert.That(() => { res = fileStorage.Add(path); }, Throws.Nothing);
			//	Assert.That(res, Is.Not.Null);
			//	Assert.That(File.Exists(Path.Combine(fileStorage.Path, res.FilePath)), Is.True);
			//}
		}

		[Test]
		public void RemoveTest()
		{
			Assert.Fail();
			//var path = Path.Combine(TestContext.CurrentContext.TestDirectory, "..\\..\\test.txt");
			//using (var fileStorage = new FileStorage(FileStoragePathGenerator.GetTempRandomPath("TMP")))
			//{
			//	IResource res = null;
			//	Assert.That(() => { res = fileStorage.Add(path); }, Throws.Nothing);
			//	Assert.That(File.Exists(Path.Combine(fileStorage.Path, res.FilePath)), Is.True);
			//	Assert.That(() => res.Remove(), Throws.Nothing);
			//	Assert.That(File.Exists(Path.Combine(fileStorage.Path, res.FilePath)), Is.False);
			//}
		}

		[Test]
		public void DisposeTest()
		{
			Assert.Fail();
			//IResource res = null;
			//var path = Path.Combine(TestContext.CurrentContext.TestDirectory, "..\\..\\test.txt");
			//var fileStorage = new FileStorage(FileStoragePathGenerator.GetTempRandomPath("TMP"));
			//Assert.That(() => { res = fileStorage.Add(path); }, Throws.Nothing);
			//Assert.That(File.Exists(Path.Combine(fileStorage.Path, res.FilePath)), Is.True);
			//Assert.That(() => fileStorage.Dispose(), Throws.Nothing);
			//Assert.That(File.Exists(Path.Combine(fileStorage.Path, res.FilePath)), Is.False);
			//Assert.That(Directory.Exists(fileStorage.Path), Is.False);
		}
	}
}