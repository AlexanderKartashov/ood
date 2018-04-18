using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace command.document.visitors
{
	public abstract class DocumentVisitor
	{
		public void VisitDocument(IDocument document, TextWriter textWriter)
		{
			BeforeVisit(textWriter);
			Visit(textWriter, document.Title);
			for(var i = 0; i < document.ItemsCount; ++i)
			{
				Visit(textWriter, i, (dynamic)document.GetItem(i));
			}
			AfterVisit(textWriter);
		}

		protected virtual void BeforeVisit(TextWriter textWriter) { }
		protected virtual void AfterVisit(TextWriter textWriter) { }
		protected abstract void Visit(TextWriter textWriter, int i, IImage image);
		protected abstract void Visit(TextWriter textWriter, int i, IParagraph paragraph);
		protected abstract void Visit(TextWriter textWriter, string title);
	}
}
