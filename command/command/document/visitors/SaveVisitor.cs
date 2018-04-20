using command.externals;
using command.storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace command.document.visitors
{
	public class SaveVisitor : DocumentVisitor
	{
		private readonly IFileStorage _fileStorage;
		private readonly IHtmlEncoder _htmlEncoder;
		private readonly IFileSystem _fileSystem;
		private readonly string _filePath;

		public SaveVisitor(IFileStorage fileStorage, IHtmlEncoder htmlEncoder, IFileSystem fileSystem, string filePath)
		{
			_fileStorage = fileStorage ?? throw new ArgumentNullException(nameof(fileStorage));
			_htmlEncoder = htmlEncoder ?? throw new ArgumentNullException(nameof(htmlEncoder));
			_fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
			_filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));

			if (!_fileSystem.IsAbsPath(_filePath))
			{
				_filePath = _fileSystem.GetAbsPath(_fileSystem.GetDirectoryPath(_fileSystem.GetApplicationPath()), _filePath);
			}
			var dir = _fileSystem.GetDirectoryPath(_filePath);
			if (!_fileSystem.DirectoryExists(dir))
			{
				_fileSystem.CreateDirectory(dir);
			}
		}

		public string AbsFilePath { get => _filePath; }

		protected override void Visit(TextWriter textWriter, int i, IImage image)
		{
			var imagesPath = _fileSystem.CombinePath(_fileSystem.GetDirectoryPath(_filePath), "images");
			if (_fileSystem.DirectoryExists(imagesPath))
			{
				_fileSystem.DeleteDirectory(imagesPath);
			}
			_fileSystem.CreateDirectory(imagesPath);
			var absDstPath = _fileSystem.CombinePath(imagesPath, image.Resource.FilePath);
			var relativeDstPath = _fileSystem.GetRelativePath(_filePath, absDstPath);
			var absSrcPath = _fileSystem.GetAbsPath(_fileStorage.Path, image.Resource.FilePath);

			_fileSystem.CopyFiles(absSrcPath, absDstPath);

			textWriter.WriteLine($"<image src=\"{_htmlEncoder.Encode(relativeDstPath)}\" width=\"{image.Width}\" height=\"{image.Height}\"></image>");
		}

		protected override void Visit(TextWriter textWriter, int i, IParagraph paragraph)
		{
			textWriter.WriteLine($"<p>{_htmlEncoder.Encode(paragraph.Text)}</p>");
		}

		protected override void Visit(TextWriter textWriter, string title)
		{
			textWriter.WriteLine($"<title>{_htmlEncoder.Encode(title)}</title></head><body>");
		}

		protected override void BeforeVisit(TextWriter textWriter)
		{
			textWriter.WriteLine("<!DOCTYPE html><html><head>");
		}

		protected override void AfterVisit(TextWriter textWriter)
		{
			textWriter.WriteLine("</body></html>");
		}
	}
}
