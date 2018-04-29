using painter.sdk;
using System;
using System.IO;

namespace canvas
{
	class FileBitmapCanvasPresenter
	{
		private string _filePath { get => Path.ChangeExtension(Path.Combine(FolderPath, Path.GetRandomFileName()), "bmp"); }

		public string FolderPath { private get; set; }

		public void DrawCanvas(IBitmapCanvas canvas)
		{
			if (canvas == null)
			{
				// do nothing
				return;
			}

			var bmp = canvas.Data ?? throw new ArgumentNullException("bitmap must be not null");
			bmp.Save(_filePath, System.Drawing.Imaging.ImageFormat.Bmp);
		}
	}
}
