using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherStationDuoProEventsPush
{
	public class WeatherData
	{
		private readonly String _stationName;

		private WeatherInfo _currentValue = new WeatherInfo() {
			Pressure = double.MaxValue,
			WindDirection = double.MaxValue,
			WindSpeed = double.MaxValue,
			Temperature = double.MaxValue,
			Humidity = double.MaxValue
		};

		public WeatherData(String stationName) => _stationName = stationName;

		public string StationName => _stationName;

		public event EventHandler<WeatherInfo> MeasurementsChanged;

		public void SetMeasurements(double temp, double humidity, double pressure, double windDirection, double windSpeed)
		{
			_currentValue = new WeatherInfo() {
				Pressure = pressure,
				WindDirection = windDirection,
				WindSpeed = windSpeed,
				Temperature = temp,
				Humidity = humidity,
				StationName = _stationName
			};

			if (MeasurementsChanged != null)
			{
				MeasurementsChanged.Invoke(this, _currentValue);
			}
		}
	}
}
