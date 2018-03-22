using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using common;

namespace WeatherStationProSeparateEvents
{
	public class StatsDisplay : IObserver<WeatherInfo>
	{
		private TextWriter _tw;

		private AverageValue _temp = new AverageValue();
		private AverageValue _pressure = new AverageValue();
		private AverageValue _humidity = new AverageValue();
		private AverageWind _wind = new AverageWind();

		public StatsDisplay(TextWriter tw) => _tw = tw;

		public void Update(WeatherInfo data)
		{
			_temp.Update(data.Temperature);
			_pressure.Update(data.Pressure);
			_humidity.Update(data.Humidity);
			_wind.Update(data.WindSpeed, data.WindDirection);

			PrintStatsData();
		}

		private void PrintStatsData()
		{
			PrintValue("Temperature", _temp);
			PrintValue("Humidity", _humidity);
			PrintValue("Pressure", _pressure);
			var averageWind = _wind.Average;
			_tw.WriteLine($"Average wind: speed {averageWind.Item1}, direction {averageWind.Item2}");
			_tw.WriteLine("========================");
		}

		private void PrintValue(string valueName, AverageValue value)
		{
			_tw.WriteLine(String.Format("Min {0} {1}", valueName, value.Min));
			_tw.WriteLine(String.Format("Max {0} {1}", valueName, value.Max));
			_tw.WriteLine(String.Format("Average {0} {1}", valueName, value.Average));
			_tw.WriteLine("----------------");
		}
	}
}
