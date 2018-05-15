using System.Collections.Generic;

namespace composite
{
	public interface IShapes
	{
		void Insert(IShape shape, int? position = null);
		void Remove(IShape shape);

		IEnumerable<IShape> Shapes { get; }
	}
}
