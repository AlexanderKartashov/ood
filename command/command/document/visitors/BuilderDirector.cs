using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace command.document.visitors
{
	public class BuilderDirector
	{
		private IDocumentContentBuilder _builder;
		private IDocument _document;

		public void Build(IDocumentContentBuilder builder, IDocument document)
		{
			_builder = builder ?? throw new ArgumentNullException(nameof(builder));
			_document = document ?? throw new ArgumentNullException(nameof(document));

			_builder.BeforeBuild();
			_builder.BuildTitle(_document.Title);
			for(var i = 0; i < _document.ItemsCount; ++i)
			{
				Visit((dynamic)_document.GetItem(i));
			}
			_builder.AfterBuild();
		}

		private void Visit(IImage image) => _builder.BuildImage(image);
		private void Visit(IParagraph paragraph) => _builder.BuildParagraph(paragraph);
	}
}
