using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherStation
{
	public class WeatherData : Observable<WeatherInfo>
	{
		private double _temperature = 0.0;
		private double _humidity = 0.0;
		private double _pressure = 760.0;
		private String _stationName;

		public WeatherData(String stationName)
		{
			_stationName = stationName;
		}

		public double Temperature { get => _temperature; }
		public double Humidity { get => _humidity; }
		public double Pressure { get => _pressure; }

		public void MeasurementsChanged() => NotifyObservers();

		public void SetMeasurements(double temp, double humidity, double pressure)
		{
			_humidity = humidity;
			_temperature = temp;
			_pressure = pressure;

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
