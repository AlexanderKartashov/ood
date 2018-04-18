using command.storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace command.document
{
	public class DocumentItemFactory : IDocumentItemFactory
	{
		private readonly IFileStorage _fileStorage;

		public DocumentItemFactory(IFileStorage fileStorage) => _fileStorage = fileStorage ?? throw new ArgumentNullException(nameof(fileStorage));

		public IImage CreateImage(string path, uint width, uint height)
		{
			return new Image(_fileStorage.Add(path), width, height);
		}

		public IParagraph CreateParagraph(string text)
		{
			return new Paragraph(text);
		}
	}
}
