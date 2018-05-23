using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace composite
{
	class BitmapSaver
	{
		public void Save(String directory, BitmapCanvas canvas)
		{
			canvas.Data.RotateFlip(RotateFlipType.RotateNoneFlipY);
			canvas.Data.Save(Path.Combine(directory, "bitmap.bmp"));
		}
	}
}
