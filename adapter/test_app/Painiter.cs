using shape_drawing_lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_app
{
	class Painiter
	{
		public void PaintPicture(CanvasPainter painter)
		{
			Triangle triangle = new Triangle(new Point { X = 10, Y = 15 }, new Point { X = 100, Y = 200 }, new Point { X = 150, Y = 250 });
			Rectangle rectangle = new Rectangle(new Point { X = 30, Y = 40 }, new Point { X = 18, Y = 24 });

			painter.Draw(triangle);
			painter.Draw(rectangle);
		}
	}
}
