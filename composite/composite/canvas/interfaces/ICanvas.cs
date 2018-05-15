using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace composite
{
	public interface ICanvas
	{
		void BeginFill(RGBAColor color);
		void EndFill();

		void SetLineColor(RGBAColor color);
		void SetLineWidth(uint width);

		void MoveTo(int x, int y);
		void LineTo(int x, int y);

		void DrawEllipse(int left, int top, int width, int height);
	}
}
