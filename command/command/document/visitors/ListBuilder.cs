using System;
using System.IO;

namespace command.document.visitors
{
	public class ListBuilder : IDocumentContentBuilder
	{
		private readonly TextWriter _textWriter;
		private int _count = 0;

		public ListBuilder(TextWriter textWriter) => _textWriter = textWriter ?? throw new ArgumentNullException(nameof(textWriter));

		public void AfterBuild(){}
		public void BeforeBuild(){}

		public void BuildImage(IImage image)
		{
			_textWriter.WriteLine($"{_count++}. Image: {image.Width} {image.Height} {image.Resource.FilePath}");
		}

		public void BuildParagraph(IParagraph paragraph)
		{
			_textWriter.WriteLine($"{_count++}. Paragraph: {paragraph.Text}");
		}

		public void BuildTitle(string title)
		{
			_textWriter.WriteLine($"Title: {title}");
		}
	}
}
