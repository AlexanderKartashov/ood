using NUnit.Framework;
using command.commandFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace command.commandFactory.Tests
{
	[TestFixture]
	[Parallelizable]
	public class UndoParserTests
	{
		[TestCase("")]
		public void ParseCommandTest(string data)
		{
			var parser = new UndoParser();
			ActionContainer action = null;
			Assert.That(() => { action = parser.ParseCommand(data); }, Throws.Nothing);
			Assert.That(action, Is.Not.Null);
		}

		[Test]
		public void PropertiesTest()
		{
			var parser = new UndoParser();
			Assert.That(parser.CommandName, Is.EqualTo("Undo"));
			Assert.That(parser.HelpString, Is.Not.Null);
		}
	}
}