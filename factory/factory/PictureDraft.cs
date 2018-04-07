using painter_declarations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace painter
{
	public class PictureDraft
	{
		private readonly IList<Shape> _shapes = new List<Shape>();

		public void AddShape(Shape shape)
		{
			_shapes.Add(shape);
		}

		public IEnumerable<Shape> Shapes { get => _shapes; }
	}
}
