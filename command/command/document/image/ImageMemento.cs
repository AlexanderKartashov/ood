using System;

namespace command.document
{
	public class ImageMemento : IMemento
	{
		private readonly uint _width;
		private readonly uint _height;
		private readonly IImage _image;

		public ImageMemento(IImage image, uint imageWidth, uint imageHeight)
		{
			_image = image ?? throw new ArgumentNullException(nameof(image));
			_width = imageWidth;
			_height = imageHeight;
		}

		public void Restore()
		{
			_image.Resize(_width, _height);
		}
	}
}
