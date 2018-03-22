using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherStation
{
	public class WeatherData : Observable<WeatherInfo>
	{
		private String _stationName;

		public WeatherData(String stationName)
		{
			_stationName = stationName;
		}

		public double Temperature { get; private set; } = 0.0;
		public double Humidity { get; private set; } = 0.0;
		public double Pressure { get; private set; } = 760.0;

		public void MeasurementsChanged() => NotifyObservers();

		public void SetMeasurements(double temp, double humidity, double pressure)
		{
			Humidity = humidity;
			Temperature = temp;
			Pressure = pressure;

			MeasurementsChanged();
		}

		protected override WeatherInfo GetChangedData()
		{
			return new WeatherInfo
			{
				Temperature = Temperature,
				Humidity = Humidity,
				Pressure = Pressure,
				StationName = _stationName
			};
		}
	}
}
