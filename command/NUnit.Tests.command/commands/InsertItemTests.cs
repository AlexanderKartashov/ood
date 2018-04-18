using NUnit.Framework;
using NSubstitute;
using command.commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using command.document;
using NUnit.Framework.Constraints;

namespace command.commands.Tests
{
	[TestFixture]
	[Parallelizable]
	public class InsertItemTests
	{
		[TestCaseSource(typeof(Data), "TestCases")]
		public void InsertItemTest(IDocument document, DocumentItem item, IResolveConstraint constraint)
		{
			Assert.That(() => new InsertItem(document, item, 0), constraint);
		}

		[Test]
		public void ExecuteTest()
		{
			var document = Substitute.For<IDocument>();
			var item = Substitute.For<DocumentItem>();
			var command = new InsertItem(document, item, 0);
			Assert.That(() => command.Execute(), Throws.Nothing);
			document.Received(1).InsertItem(Arg.Is(item), Arg.Is(0));
			document.DidNotReceive().DeleteItem(Arg.Any<int>());
		}

		[Test]
		public void UnexecuteTest()
		{
			var document = Substitute.For<IDocument>();
			var item = Substitute.For<DocumentItem>();
			var command = new InsertItem(document, item, 0);

			Assert.That(() => command.Execute(), Throws.Nothing);
			document.Received(1).InsertItem(Arg.Is(item), Arg.Is(0));

			Assert.That(() => command.Unexecute(), Throws.Nothing);
			document.Received(1).DeleteItem(Arg.Is(0));
		}

		[Test]
		public void DestroyExecutedTest()
		{
			var document = Substitute.For<IDocument>();
			var item = Substitute.For<DocumentItem>();
			var command = new InsertItem(document, item, 0);

			Assert.That(() => command.Execute(), Throws.Nothing);
			Assert.That(() => command.Dispose(), Throws.Nothing);
			item.DidNotReceiveWithAnyArgs().Dispose();
		}

		[Test]
		public void DestroyUnexecutedTest()
		{
			var document = Substitute.For<IDocument>();
			var item = Substitute.For<DocumentItem>();
			var command = new InsertItem(document, item, 0);
			Assert.That(() => command.Execute(), Throws.Nothing);
			Assert.That(() => command.Unexecute(), Throws.Nothing);
			Assert.That(() => command.Dispose(), Throws.Nothing);
			item.Received(1).Dispose();
		}

		[Test]
		public void ExecuteInsertInEndOnCollectionTest()
		{
			var item1 = Substitute.For<DocumentItem>();
			var item2 = Substitute.For<DocumentItem>();
			var newItem = Substitute.For<DocumentItem>();
			var item4 = Substitute.For<DocumentItem>();
			var collection = new List<DocumentItem>(){
				item1, item2, item4
			};
			var document = Substitute.For<IDocument>();
			document.When(x => x.DeleteItem(Arg.Any<int>())).Do(x => collection.RemoveAt(x.ArgAt<int>(0)));
			document.When(x => x.InsertItem(Arg.Any<DocumentItem>(), Arg.Any<int>())).Do(x => collection.Insert(x.ArgAt<int>(1), x.ArgAt<DocumentItem>(0)));

			var command = new InsertItem(document, newItem, 3);
			Assert.That(() => command.Execute(), Throws.Nothing);
			CollectionAssert.AreEqual(new List<DocumentItem>() { item1, item2, item4, newItem }, collection);
		}

		[Test]
		public void UnexecuteInsertInEndOnCollectionTest()
		{
			var item1 = Substitute.For<DocumentItem>();
			var item2 = Substitute.For<DocumentItem>();
			var newItem = Substitute.For<DocumentItem>();
			var item4 = Substitute.For<DocumentItem>();
			var collection = new List<DocumentItem>(){
				item1, item2, item4
			};
			var document = Substitute.For<IDocument>();
			document.When(x => x.DeleteItem(Arg.Any<int>())).Do(x => collection.RemoveAt(x.ArgAt<int>(0)));
			document.When(x => x.InsertItem(Arg.Any<DocumentItem>(), Arg.Any<int>())).Do(x => collection.Insert(x.ArgAt<int>(1), x.ArgAt<DocumentItem>(0)));

			var command = new InsertItem(document, newItem, 3);
			Assert.That(() => command.Execute(), Throws.Nothing);
			CollectionAssert.AreEqual(new List<DocumentItem>() { item1, item2, item4, newItem }, collection);

			Assert.That(() => command.Unexecute(), Throws.Nothing);
			CollectionAssert.AreEqual(new List<DocumentItem>() { item1, item2, item4 }, collection);
		}

		[Test]
		public void ExecuteOnCollectionTest()
		{
			var item1 = Substitute.For<DocumentItem>();
			var item2 = Substitute.For<DocumentItem>();
			var newItem = Substitute.For<DocumentItem>();
			var item4 = Substitute.For<DocumentItem>();
			var collection = new List<DocumentItem>(){
				item1, item2, item4
			};
			var document = Substitute.For<IDocument>();
			document.When(x => x.DeleteItem(Arg.Any<int>())).Do(x => collection.RemoveAt(x.ArgAt<int>(0)));
			document.When(x => x.InsertItem(Arg.Any<DocumentItem>(), Arg.Any<int>())).Do(x => collection.Insert(x.ArgAt<int>(1), x.ArgAt<DocumentItem>(0)));

			var command = new InsertItem(document, newItem, 2);
			Assert.That(() => command.Execute(), Throws.Nothing);
			CollectionAssert.AreEqual(new List<DocumentItem>() { item1, item2, newItem, item4 }, collection);
		}

		[Test]
		public void UnexecuteOnCollectionTest()
		{
			var item1 = Substitute.For<DocumentItem>();
			var item2 = Substitute.For<DocumentItem>();
			var newItem = Substitute.For<DocumentItem>();
			var item4 = Substitute.For<DocumentItem>();
			var collection = new List<DocumentItem>(){
				item1, item2, item4
			};
			var document = Substitute.For<IDocument>();
			document.When(x => x.DeleteItem(Arg.Any<int>())).Do(x => collection.RemoveAt(x.ArgAt<int>(0)));
			document.When(x => x.InsertItem(Arg.Any<DocumentItem>(), Arg.Any<int>())).Do(x => collection.Insert(x.ArgAt<int>(1), x.ArgAt<DocumentItem>(0)));

			var command = new InsertItem(document, newItem, 2);
			Assert.That(() => command.Execute(), Throws.Nothing);
			CollectionAssert.AreEqual(new List<DocumentItem>() { item1, item2, newItem, item4 }, collection);

			Assert.That(() => command.Unexecute(), Throws.Nothing);
			CollectionAssert.AreEqual(new List<DocumentItem>() { item1, item2, item4 }, collection);
		}

		[Test]
		public void DestroyNotExecutedTest()
		{
			var document = Substitute.For<IDocument>();
			var item = Substitute.For<DocumentItem>();
			var command = new InsertItem(document, item, 0);
			Assert.That(() => command.Dispose(), Throws.Nothing);
			item.DidNotReceiveWithAnyArgs().Dispose();
		}

		class Data
		{
			public static IEnumerable TestCases
			{
				get
				{
					yield return new TestCaseData(null, null, Throws.ArgumentNullException);
					yield return new TestCaseData(Substitute.For<IDocument>(), null, Throws.ArgumentNullException);
					yield return new TestCaseData(null, Substitute.For<DocumentItem>(), Throws.ArgumentNullException);
					yield return new TestCaseData(Substitute.For<IDocument>(), Substitute.For<DocumentItem>(), Throws.Nothing);
				}
			}
		}
	}
}