using NUnit.Framework;
using MenuCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;

namespace MenuCommon.Tests
{
	[TestFixture()]
	[Parallelizable]
	public class MenuTests
	{
		private Menu _menu;
		private IErrorHandler _errorHandler;

		[SetUp]
		public void Setup()
		{
			_errorHandler = Substitute.For<IErrorHandler>();
			_menu = new Menu(_errorHandler);
		}

		[Test()]
		public void TestCommandProcessed()
		{
			Assert.Fail();
		}

		[Test()]
		public void TestCommandNotProcessed()
		{
			Assert.Fail();
		}

		[Test()]
		public void TestShouldNotCallParserIfCommandEmpty()
		{
			Assert.Fail();
		}
	}
}