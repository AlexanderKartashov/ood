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
	public class DeleteItemTests
	{
		[TestCaseSource(typeof(Data), "TestCases")]
		public void DeleteItemTest(IDocument document, IResolveConstraint constraint)
		{
			Assert.That(() => new DeleteItem(document, 0), constraint);
		}

		[Test]
		public void DeleteItemInitTest()
		{
			var document = Substitute.For<IDocument>();
			var command = new DeleteItem(document, 0);
			document.Received(1).GetItem(Arg.Is(0));
		}

		[Test]
		public void ExecuteTest()
		{
			var document = Substitute.For<IDocument>();
			var command = new DeleteItem(document, 0);
			Assert.That(() => command.Execute(), Throws.Nothing);
			document.Received(1).DeleteItem(Arg.Is(0));
			document.DidNotReceive().InsertItem(Arg.Any<DocumentItem>(), Arg.Any<int>());
		}

		[Test]
		public void UnexecuteTest()
		{
			var document = Substitute.For<IDocument>();
			var item = Substitute.For<DocumentItem>();
			document.GetItem(Arg.Any<int>()).Returns(item);
			var command = new DeleteItem(document, 0);

			Assert.That(() => command.Execute(), Throws.Nothing);
			document.Received(1).DeleteItem(Arg.Is(0));

			Assert.That(() => command.Unexecute(), Throws.Nothing);
			document.Received(1).InsertItem(Arg.Is(item), Arg.Is(0));
		}

		[Test]
		public void ExecuteOnCollectionTest()
		{
			var item1 = Substitute.For<DocumentItem>();
			var item2 = Substitute.For<DocumentItem>();
			var item3 = Substitute.For<DocumentItem>();
			var item4 = Substitute.For<DocumentItem>();
			var collection = new List<DocumentItem>(){
				item1, item2, item3, item4
			};
			var document = Substitute.For<IDocument>();
			document.When(x => x.DeleteItem(Arg.Any<int>())).Do(x => collection.RemoveAt(x.ArgAt<int>(0)));
			document.When(x => x.InsertItem(Arg.Any<DocumentItem>(), Arg.Any<int>())).Do(x => collection.Insert(x.ArgAt<int>(1), x.ArgAt<DocumentItem>(0)));

			var command = new DeleteItem(document, 1);
			Assert.That(() => command.Execute(), Throws.Nothing);
			CollectionAssert.AreEqual(new List<DocumentItem>() { item1, item3, item4 }, collection);
		}

		[Test]
		public void UnexecuteOnCollectionTest()
		{
			var item1 = Substitute.For<DocumentItem>();
			var item2 = Substitute.For<DocumentItem>();
			var item3 = Substitute.For<DocumentItem>();
			var item4 = Substitute.For<DocumentItem>();
			var collection = new List<DocumentItem>(){
				item1, item2, item3, item4
			};
			var document = Substitute.For<IDocument>();
			document.When(x => x.DeleteItem(Arg.Any<int>())).Do(x => collection.RemoveAt(x.ArgAt<int>(0)));
			document.GetItem(Arg.Any<int>()).Returns(x => collection.ElementAt(x.ArgAt<int>(0)));
			document.When(x => x.InsertItem(Arg.Any<DocumentItem>(), Arg.Any<int>())).Do(x => collection.Insert(x.ArgAt<int>(1), x.ArgAt<DocumentItem>(0)));

			var command = new DeleteItem(document, 1);
			Assert.That(() => command.Execute(), Throws.Nothing);
			CollectionAssert.AreEqual(new List<DocumentItem>() { item1, item3, item4 }, collection);

			Assert.That(() => command.Unexecute(), Throws.Nothing);
			CollectionAssert.AreEqual(new List<DocumentItem>() { item1, item2, item3, item4 }, collection);
		}

		[Test]
		public void DestroyExecutedTest()
		{
			var document = Substitute.For<IDocument>();
			var item = Substitute.For<DocumentItem>();
			document.GetItem(Arg.Any<int>()).Returns(item);
			var command = new DeleteItem(document, 0);

			Assert.That(() => command.Execute(), Throws.Nothing);
			Assert.That(() => command.Dispose(), Throws.Nothing);
			item.Received(1).Dispose();
		}

		[Test]
		public void DestroyUnexecutedTest()
		{
			var document = Substitute.For<IDocument>();
			var item = Substitute.For<DocumentItem>();
			document.GetItem(Arg.Any<int>()).Returns(item);
			var command = new DeleteItem(document, 0);
			Assert.That(() => command.Execute(), Throws.Nothing);
			Assert.That(() => command.Unexecute(), Throws.Nothing);
			Assert.That(() => command.Dispose(), Throws.Nothing);
			item.DidNotReceiveWithAnyArgs().Dispose();
		}

		[Test]
		public void DestroyNotExecutedTest()
		{
			var document = Substitute.For<IDocument>();
			var item = Substitute.For<DocumentItem>();
			document.GetItem(Arg.Any<int>()).Returns(item);
			var command = new DeleteItem(document, 0);
			Assert.That(() => command.Dispose(), Throws.Nothing);
			item.DidNotReceiveWithAnyArgs().Dispose();
		}

		class Data
		{
			public static IEnumerable TestCases
			{
				get
				{
					yield return new TestCaseData(null, Throws.ArgumentNullException);
					yield return new TestCaseData(Substitute.For<IDocument>(), Throws.Nothing);
				}
			}
		}
	}
}