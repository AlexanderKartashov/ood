using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace painter.sdk
{
	public interface IBitmapCanvas : ICanvas
	{
		Bitmap Data { get; }
	}
}
