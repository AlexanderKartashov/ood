using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graphics_lib
{
	public interface ICanvas
	{
		void Moveto(int x, int y);
		void LineTo(int x, int y);
		void SetColor(uint rgbColor);
	}
}
