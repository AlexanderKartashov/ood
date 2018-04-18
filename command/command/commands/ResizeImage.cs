using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using command.document;

namespace command.commands
{
	public class ResizeImage : AbstractCommand
	{
		private readonly IImage _image;
		private readonly IMemento _memento;
		private readonly uint _newWidth;
		private readonly uint _newHeight;

		public ResizeImage(IImage image, uint newWidth, uint newHeight)
		{
			_image = image ?? throw new ArgumentNullException(nameof(image));
			_memento = _image.CreateMemento();
			_newWidth = newWidth;
			_newHeight = newHeight;
		}

		protected override void ExecuteImpl() => _image.Resize(_newWidth, _newHeight);
		protected override void UnexecuteImpl() => _memento.Restore();
	}
}
