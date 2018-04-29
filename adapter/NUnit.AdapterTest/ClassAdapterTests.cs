using NUnit.Framework;
using adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NSubstitute;

namespace adapters.Tests
{
	[TestFixture()]
	public class ClassAdapterTests
	{
		[Test()]
		public void ClassAdapterTest()
		{
			var tw = new StringWriter();
			using (var adapter = new ClassAdapter(tw))
			{
				adapter.SetColor(0xFFFFFF);
				adapter.Moveto(0, 0);
				adapter.LineTo(1, 1);
				adapter.LineTo(2, 2);
				adapter.SetColor(0x00FF00);
				adapter.LineTo(3, 3);
			}

			var sb = new StringBuilder();
			sb.AppendLine("<draw>");
			sb.AppendLine($"  <line fromX=0 fromY=0 toX=1 toY=1>");
			sb.AppendLine($"    <color r={1.0f:F2} g={1.0f:F2} b={1.0f:F2} a={1.0f:F2}/>");
			sb.AppendLine($"  </line>");
			sb.AppendLine($"  <line fromX=1 fromY=1 toX=2 toY=2>");
			sb.AppendLine($"    <color r={1.0f:F2} g={1.0f:F2} b={1.0f:F2} a={1.0f:F2}/>");
			sb.AppendLine($"  </line>");
			sb.AppendLine($"  <line fromX=2 fromY=2 toX=3 toY=3>");
			sb.AppendLine($"    <color r={0.0f:F2} g={1.0f:F2} b={0.0f:F2} a={1.0f:F2}/>");
			sb.AppendLine($"  </line>");
			sb.AppendLine("</draw>");
			var expected = sb.ToString();
			var value = tw.ToString();
			Assert.That(value, Is.EqualTo(expected));
		}
	}
}