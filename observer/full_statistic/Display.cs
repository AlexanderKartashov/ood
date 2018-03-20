using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherStationPro;

namespace WeatherStationPro
{
	public class Display : WeatherStationPro.IObserver<WeatherInfo>
	{
		private readonly TextWriter _textWriter;

		public Display(TextWriter tw) => _textWriter = tw;

		public void Update(WeatherInfo data)
		{
			_textWriter.WriteLine(string.Format("Weather station {0}", data.StationName));
			_textWriter.WriteLine(String.Format("Current Temp {0}", data.Temperature));
			_textWriter.WriteLine(String.Format("Current Hum {0}", data.Humidity));
			_textWriter.WriteLine(String.Format("Current Pressure {0}", data.Pressure));
			_textWriter.WriteLine(string.Format("Wind Direction {0}", data.WindDirection));
			_textWriter.WriteLine(string.Format("Wind Speed {0}", data.WindSpeed));
			_textWriter.WriteLine("========================");
		}
	}
}
