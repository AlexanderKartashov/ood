using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherStationProSeparateEvents
{
	public class WeatherInfo
	{
		public double Temperature { get; set; }
		public double Humidity { get; set; }
		public double Pressure { get; set; }
		public double WindSpeed { get; set; }
		public double WindDirection { get; set; }

		public override bool Equals(object obj)
		{
			if (!(obj is WeatherInfo))
			{
				return false;
			}

			var other = obj as WeatherInfo;
			return
				Temperature == other.Temperature &&
				Pressure == other.Pressure &&
				Humidity == other.Humidity &&
				WindSpeed == other.WindSpeed &&
				WindDirection == other.WindDirection;
		}

		public override int GetHashCode()
		{
			var hashCode = -428500087;
			hashCode = hashCode * -1521134295 + Temperature.GetHashCode();
			hashCode = hashCode * -1521134295 + Humidity.GetHashCode();
			hashCode = hashCode * -1521134295 + Pressure.GetHashCode();
			hashCode = hashCode * -1521134295 + WindSpeed.GetHashCode();
			hashCode = hashCode * -1521134295 + WindDirection.GetHashCode();
			return hashCode;
		}
	}
}
