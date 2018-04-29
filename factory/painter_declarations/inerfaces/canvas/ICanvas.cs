using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace painter.sdk
{
	public interface ICanvas
	{
		void SetColor(Color color);
		void DrawLine(Point from, Point to);
		void DrawEllipse(Point center, Point size);

		void Accept(ICanvasVisitor visitor);
	}
}
