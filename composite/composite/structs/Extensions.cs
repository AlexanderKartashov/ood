using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PointD = composite.Point<double>;
using PointI = composite.Point<int>;

namespace composite
{
	public static class Extensions
	{
		public static Point<T> Clone<T>(this Point<T> value)
		{
			return new Point<T>(value.X, value.Y);
		}

		public static RGBAColor Clone(this RGBAColor value)
		{
			return new RGBAColor(value.R, value.G, value.B, value.A);
		}

		public static Rect Clone(this Rect value)
		{
			return new Rect(value.LeftTop, value.RightBottom);
		}

		public static Rect Union(this Rect value, Rect other)
		{
			var minX = Math.Min(value.LeftTop.X, other.LeftTop.X);
			var maxX = Math.Max(value.RightBottom.X, other.RightBottom.X);
			var minY = Math.Min(value.LeftTop.Y, other.LeftTop.Y);
			var maxY = Math.Max(value.RightBottom.Y, other.RightBottom.Y);

			return new Rect(new PointI(minX, minY), new PointI(maxX, maxY));
		}

		public static PointD Normalize(this PointI value, Rect bounds)
		{
			double normalizeValue(double val, double min, double max, double size)
			{
				var res = 0.0;
				if (val <= min)
				{
					res = 0;
				}
				else if (val >= max)
				{
					res = 1;
				}
				else
				{
					res = (val - min) / size;
				}
				return res;
			}

			return new PointD(
				normalizeValue(value.X, bounds.LeftTop.X, bounds.RightBottom.X, bounds.Size.X),
				normalizeValue(value.Y, bounds.LeftTop.Y, bounds.RightBottom.Y, bounds.Size.Y)
			).ClampNormalized();
		}

		public static PointI Denormalize(this PointD value, Rect bounds)
		{
			var pt = value.ClampNormalized();

			int denormalizeValue(double val, double size, double min)
			{
				return (int)Math.Round(val * size + min);
			}

			return new PointI(
				denormalizeValue(value.X, bounds.Size.X, bounds.LeftTop.X),
				denormalizeValue(value.Y, bounds.Size.Y, bounds.LeftTop.Y)
			);
		}

		public static PointD ClampNormalized(this PointD value)
		{
			double ClampValue(double val)
			{
				if (val < 0)
				{
					return 0;
				}
				if (val > 1)
				{
					return 1;
				}
				return val;
			}

			return new PointD(
				ClampValue(value.X),
				ClampValue(value.Y)
			);
		}
	}
}
