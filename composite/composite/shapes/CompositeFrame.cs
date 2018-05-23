using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace composite
{
	public class CompositeFrame
	{
		private IEnumerable<IShape> _shapes;

		public CompositeFrame(IEnumerable<IShape> shapes) => _shapes = shapes ?? throw new ArgumentNullException(nameof(shapes));

		public Rect Frame
		{
			get
			{
				if (_shapes.Count() == 0)
				{
					return new Rect(0, 0, 0, 0);
				}

				var result = _shapes.First().Frame.Clone();
				_shapes.ToList().ForEach(x => { result = result.Union(x.Frame); });
				return result;
			}
			set
			{
				var currentFrame = Frame;
				double deltaW = value.Size.X == currentFrame.Size.X ? 0 :
					(double)(value.Size.X - currentFrame.Size.X) / (double)(currentFrame.Size.X);
				double deltaH = value.Size.Y == currentFrame.Size.Y ? 0 :
					(double)(value.Size.Y - currentFrame.Size.Y) / (double)(currentFrame.Size.Y);

				_shapes.ToList().ForEach(x => {

					int resize(int size, double delta)
					{
						if (delta >= 0)
						{
							return (int)Math.Round((double)size * Math.Abs(delta)) + size;
						}
						else
						{
							return (int)Math.Round((double)size * (1.0 - Math.Abs(delta)));
						}
					}

					var shapeOldFrame = x.Frame;
					var newX = value.LeftTop.X + resize(shapeOldFrame.LeftTop.X - currentFrame.LeftTop.X, deltaW);
					var newY = value.LeftTop.Y + resize(shapeOldFrame.LeftTop.Y - currentFrame.LeftTop.Y, deltaH);
					var newW = resize(shapeOldFrame.Size.X, deltaW);
					var newH = resize(shapeOldFrame.Size.Y, deltaH);
					var shapeNewFrame = new Rect(newX, newY, newW, newH);
					x.Frame = shapeNewFrame;
				});
			}
		}
	}
}
