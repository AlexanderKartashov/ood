using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace composite
{
	public interface ISlide : IDrawable
	{
		Point Size { get; }

		IShapes Shapes { get; }
	}
}
