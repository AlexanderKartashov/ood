using NUnit.Framework;
using NSubstitute;
using command.document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace command.document.Tests
{
	[TestFixture]
	[Parallelizable]
	public class DocumentItemsCollectionTests
	{
		[Test]
		public void TestCount()
		{
			var collection = new DocumentItemsCollection();
			Assert.That(collection.ItemsCount, Is.EqualTo(0));
			var docItem = Substitute.For<IDocumentItem>();
			Assert.That(() => collection.InsertItem(docItem, 0), Throws.Nothing);
			Assert.That(collection.ItemsCount, Is.EqualTo(1));
			Assert.That(() => collection.DeleteItem(0), Throws.Nothing);
			Assert.That(collection.ItemsCount, Is.EqualTo(0));
		}

		[Test]
		public void TestGetItem()
		{
			var collection = new DocumentItemsCollection();
			var docItem = Substitute.For<IDocumentItem>();
			Assert.That(() => collection.InsertItem(docItem, 0), Throws.Nothing);
			IDocumentItem item = null;
			Assert.That(() => { item = collection.GetItem(0); }, Throws.Nothing);
			Assert.That(item, Is.EqualTo(docItem));
		}

		[Test]
		public void DeleteItemTest()
		{
			var collection = new DocumentItemsCollection();
			var docItem = Substitute.For<IDocumentItem>();
			Assert.That(() => collection.DeleteItem(0), Throws.TypeOf<ArgumentOutOfRangeException>());
			Assert.That(() => collection.DeleteItem(1), Throws.TypeOf<ArgumentOutOfRangeException>());
			Assert.That(() => collection.InsertItem(docItem, 0), Throws.Nothing);
			Assert.That(() => collection.DeleteItem(1), Throws.TypeOf<ArgumentOutOfRangeException>());
			Assert.That(() => collection.DeleteItem(0), Throws.Nothing);
		}

		[Test]
		public void InsertItemTest()
		{
			var collection = new DocumentItemsCollection();
			var docItem = Substitute.For<IDocumentItem>();
			Assert.That(() => collection.InsertItem(docItem, 5), Throws.TypeOf<ArgumentOutOfRangeException>());
			Assert.That(() => collection.InsertItem(docItem, 0), Throws.Nothing);
		}
	}
}