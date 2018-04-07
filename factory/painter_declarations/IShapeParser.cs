using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace painter_declarations
{
	public interface IShapeParser
	{
		string ShapeType { get; }

		Shape Parse(string description);
	}
}
