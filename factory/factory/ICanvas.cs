using painter.shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace painter
{
	public interface ICanvas
	{
		void SetColor(Color color);
		void DrawLine(Point from, Point to);
		void DrawEllipse(Point leftTop, Point size);
	}
}
