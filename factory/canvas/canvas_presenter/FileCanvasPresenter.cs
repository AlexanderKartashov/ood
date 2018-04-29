using painter.sdk;
using System;
using System.ComponentModel.Composition;

namespace canvas
{
	[Export(typeof(ICanvasVisitor))]
	public class FileCanvasPresenter : ICanvasVisitor
	{
		public string Directory { private get; set; }

		public void Visit(IVectorCanvas vectorCanvas)
		{
			var canvasPresenter = new FileVectorCanvasPresenter() { FolderPath = Directory };
			canvasPresenter.DrawCanvas(vectorCanvas);
		}

		public void Visit(IBitmapCanvas bitmapCanvas)
		{
			var canvasPresenter = new FileBitmapCanvasPresenter() { FolderPath = Directory };
			canvasPresenter.DrawCanvas(bitmapCanvas);
		}
	}
}
