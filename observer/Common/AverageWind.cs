using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace common
{
	public class AverageWind
	{
		// max, min, average
		private double _radius = 0.0;
		private double _angle = 0.0;

		public (double, double) Average { get => (_radius, _angle); }

		public void Update(double radius, double angle)
		{
			// average value zero, set 
			if ((_radius == 0.0) && (_angle == 0.0))
			{
				_radius = radius;
				_angle = angle;
				return;
			}

			if ((radius == 0.0) && (angle == 0.0))
			{
				return;
			}

			(double averageX, double averageY) = ToDecart(_radius, _angle);
			(double currentX, double currentY) = ToDecart(radius, angle);

			double newValX = averageX + currentX;
			double newValY = averageY + currentY;

			(double newRadius, double newAngle) = ToRadial(newValX, newValY);

			_radius = newRadius;
			_angle = ToDegrees(newAngle);
		}

		private double ToDegrees(double rad)
		{
			return 180.0 * rad / Math.PI;
		}

		private double ToRadian(double deg)
		{
			return Math.PI * deg / 180.0;
		}

		private (double, double) ToRadial(double x, double y)
		{
			double radius = Math.Sqrt(x * x + y * y);

			if (radius == 0)
			{
				return (0, 0);
			}

			if (x == 0 && y > 0)
			{
				return (
					radius,
					Math.PI / 2
				);
			}
			if (x == 0 && y < 0)
			{
				return (
					radius,
					-1 * Math.PI / 2
				);
			}
			if (x == 0 && y == 0)
			{
				return (0, 0);
			}

			var div = y / x;
			if (x > 0)
			{
				return (
					radius,
					Math.Atan(div)
				);
			}
			if (x < 0 && y >= 0)
			{
				return (
					radius,
					Math.Atan(div) + Math.PI
				);
			}
			if (x < 0 && y < 0)
			{
				return (
					radius,
					Math.Atan(div) - Math.PI
				);
			}

			return (0, 0);
		}

		private (double, double) ToDecart(double value, double angle)
		{
			return (
				Math.Cos(ToRadian(angle)) * value,
				Math.Sin(ToRadian(angle)) * value
			);
		}
	}
}
