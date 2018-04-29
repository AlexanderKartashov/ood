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
			Triangle blackTriangle = new Triangle(new Point { X = 10, Y = 15 }, new Point { X = 100, Y = 200 }, new Point { X = 150, Y = 250 });
			Rectangle whiteRectangle = new Rectangle(new Point { X = 30, Y = 40 }, new Point { X = 18, Y = 24 }, 0xFFFFFF);

			Triangle redTriangle = new Triangle(new Point { X = 10, Y = 15 }, new Point { X = 100, Y = 200 }, new Point { X = 150, Y = 250 }, 0xFF0000);
			Rectangle blueRectangle = new Rectangle(new Point { X = 30, Y = 40 }, new Point { X = 18, Y = 24 }, 0x0000FF);

			Triangle yellowTriangle = new Triangle(new Point { X = 10, Y = 15 }, new Point { X = 100, Y = 200 }, new Point { X = 150, Y = 250 }, 0xFFFF00);

			painter.Draw(blackTriangle);
			painter.Draw(whiteRectangle);
			painter.Draw(redTriangle);
			painter.Draw(blueRectangle);
			painter.Draw(yellowTriangle);
		}
	}
}
