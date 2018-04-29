using painter.sdk;
using System.IO;

namespace canvas
{
	class FileVectorCanvasPresenter
	{
		private string _filePath { get => Path.ChangeExtension(Path.Combine(FolderPath, Path.GetRandomFileName()), "html"); }

		public string FolderPath { private get; set; }

		public void DrawCanvas(IVectorCanvas canvas)
		{
			if (canvas == null)
			{
				// do nothing
				return;
			}

			using (var file = File.CreateText(_filePath))
			{
				file.Write(canvas.Data);
			}
		}
	}
}
