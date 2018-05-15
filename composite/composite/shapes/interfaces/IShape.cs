using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace composite
{
	public interface IShape : IDrawable
	{
		ILineStyle LineStyle { get; set; }
		IFillStyle FillStyle { get; set; }
		Rect Frame { get; set; }
	}
}
