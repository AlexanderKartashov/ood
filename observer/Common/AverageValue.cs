using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace common
{
	public class AverageValue
	{
		private int _count = 0;
		private double _minValue = double.MaxValue;
		private double _maxValue = double.MinValue;
		private double _accValue = 0;

		public double Min { get => _minValue; }

		public double Max { get => _maxValue; }

		public double Average { get => _accValue / _count; }

		public void Update(double value)
		{
			if (_minValue > value)
			{
				_minValue = value;
			}
			if (_maxValue < value)
			{
				_maxValue = value;
			}
			_accValue += value;
			++_count;
		}
	}
}
