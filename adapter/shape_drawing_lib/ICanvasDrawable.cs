using graphics_lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shape_drawing_lib
{
	public interface ICanvasDrawable
	{
		void Draw(ICanvas canvas);
	}
}
