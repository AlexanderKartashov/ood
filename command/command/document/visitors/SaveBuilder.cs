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
	public class SaveBuilder : IDocumentContentBuilder, IDisposable
	{
		private readonly IFileStorage _fileStorage;
		private readonly IHtmlEncoder _htmlEncoder;
		private readonly IFileSystem _fileSystem;
		private readonly string _filePath;
		private readonly TextWriter _textWriter;

		public SaveBuilder(IFileStorage fileStorage, IHtmlEncoder htmlEncoder, IFileSystem fileSystem, string filePath)
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

			_textWriter = _fileSystem.CreateTextFile(_filePath);
		}

		public void BuildTitle(string title)
		{
			_textWriter.WriteLine($"<title>{_htmlEncoder.Encode(title)}</title></head><body>");
		}

		public void BuildParagraph(IParagraph paragraph)
		{
			_textWriter.WriteLine($"<p>{_htmlEncoder.Encode(paragraph.Text)}</p>");
		}

		public void BuildImage(IImage image)
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

			_textWriter.WriteLine($"<image src=\"{_htmlEncoder.Encode(relativeDstPath)}\" width=\"{image.Width}\" height=\"{image.Height}\"></image>");
		}

		public void BeforeBuild()
		{
			_textWriter.WriteLine("<!DOCTYPE html><html><head>");
		}

		public void AfterBuild()
		{
			_textWriter.WriteLine("</body></html>");
		}

		public void Dispose()
		{
			_textWriter?.Flush();
			_textWriter.Dispose();
		}
	}
}
