using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace canvas
{
	public class BitmapCanvasWithSave : BitmapCanvas
	{
		public BitmapCanvasWithSave(int w, int h)
			: base(w, h)
		{
		}

		public void Save(string directory)
		{
			Data.Save(Path.Combine(directory, "bitmap.bmp"), System.Drawing.Imaging.ImageFormat.Bmp);
		}
	}
}
