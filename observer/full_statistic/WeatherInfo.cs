using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace full_statistic
{
	public class WeatherInfo
	{
		public double Temperature { get; set; }
		public double Humidity { get; set; }
		public double Pressure { get; set; }
		public double WindSpeed { get; set; }
		public double WindDirection { get; set; }
		public String StationName { get; set; }

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
				WindDirection == other.WindDirection &&
				StationName == other.StationName;
		}
	}
}
