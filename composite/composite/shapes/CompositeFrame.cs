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
				var result = new Rect(0, 0, 0, 0);
				_shapes.ToList().ForEach(x => result = result.Union(x.Frame));
				return result;
			}
			set
			{
				var currentFrame = Frame;
				var deltaX = value.LeftTop.X - currentFrame.LeftTop.X;
				var deltaY = value.LeftTop.Y - currentFrame.LeftTop.Y;
				double deltaW = value.Size.X == currentFrame.Size.X ? 0 :
					(double)(value.Size.X - currentFrame.Size.X) / (double)(currentFrame.Size.X);
				double deltaH = value.Size.Y == currentFrame.Size.Y ? 0 :
					(double)(value.Size.Y - currentFrame.Size.Y) / (double)(currentFrame.Size.Y);

				_shapes.ToList().ForEach(x => {
					var shapeOldFrame = x.Frame;
					var newX = shapeOldFrame.LeftTop.X + deltaX;
					var newY = shapeOldFrame.LeftTop.Y + deltaY;
					var newW = (int)Math.Round((double)shapeOldFrame.Size.X * Math.Abs(deltaW)) + (deltaW >= 0 ? shapeOldFrame.Size.X : (deltaW < -1 ? -1 * shapeOldFrame.Size.X : 0));
					var newH = (int)Math.Round((double)shapeOldFrame.Size.Y * Math.Abs(deltaH)) + (deltaH >= 0 ? shapeOldFrame.Size.Y : (deltaH < -1 ? -1 * shapeOldFrame.Size.Y : 0));
					var shapeNewFrame = new Rect(newX, newY, newW, newH);
					x.Frame = shapeNewFrame;
				});
			}
		}
	}
}
