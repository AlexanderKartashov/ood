using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherStationDuoProEventsPush
{
	public class WeatherInfo : EventArgs
	{
		public double Temperature { get; set; }
		public double Humidity { get; set; }
		public double Pressure { get; set; }
		public double WindSpeed { get; set; }
		public double WindDirection { get; set; }
		public String StationName { get; set; }
	}
}
