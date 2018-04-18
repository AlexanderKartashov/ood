using NUnit.Framework;
using command.externals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace command.externals.Tests
{
	[TestFixture]
	[Parallelizable]
	public class HtmlEncoderTests
	{
		[TestCase("1", "1")]
		[TestCase("abc", "abc")]
		[TestCase("<", "&lt;")]
		[TestCase(">", "&gt;")]
		[TestCase("&", "&amp;")]
		[TestCase("\"", "&quot;")]
		[TestCase("\'", "&#39;")]
		public void EncodeTest(string input, string expected)
		{
			var encoder = new HtmlEncoder();
			var encoded = encoder.Encode(input);
			Assert.That(encoded, Is.EqualTo(expected));
		}
	}
}