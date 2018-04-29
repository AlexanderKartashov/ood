using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace command.document.visitors
{
	public interface IDocumentContentBuilder
	{
		void BuildTitle(string title);
		void BuildParagraph(IParagraph paragraph);
		void BuildImage(IImage image);
		void BeforeBuild();
		void AfterBuild();
	}
}
