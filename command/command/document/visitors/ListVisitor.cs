using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace command.document.visitors
{
	public class ListVisitor : DocumentVisitor
	{
		protected override void Visit(TextWriter textWriter, int i, IImage image)
		{
			textWriter.WriteLine($"{i}. Image: {image.Width} {image.Height} {image.Resource.FilePath}");
		}

		protected override void Visit(TextWriter textWriter, int i, IParagraph paragraph)
		{
			textWriter.WriteLine($"{i}. Paragraph: {paragraph.Text}");
		}

		protected override void Visit(TextWriter textWriter, string title)
		{
			textWriter.WriteLine($"Title: {title}");
		}
	}
}
