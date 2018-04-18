using command.storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace command.document
{
	public class Image : IImage
	{
		private readonly IResource _resource;

		public Image(IResource resource, uint width, uint height)
		{
			_resource = resource ?? throw new ArgumentNullException(nameof(resource));
			Width = width;
			Height = height;
		}

		public uint Width { get; private set; }

		public uint Height { get; private set; }

		public IResource Resource => _resource;

		public IMemento CreateMemento() => new ImageMemento(this, Width, Height);

		public IParagraph DocumentParagraph => null;

		public IImage DocumentImage => this;

		public void Dispose() => _resource.Dispose();

		public void Resize(uint imageWidth, uint imageHeight)
		{
			Width = imageWidth;
			Height = imageHeight;
		}
	}
}
