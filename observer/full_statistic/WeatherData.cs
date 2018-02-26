using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace full_statistic
{
	public class WeatherData : Observable<WeatherInfo>
	{
		private double _temperature = 0.0;
		private double _humidity = 0.0;
		private double _pressure = 760.0;
		private double _windDirection = 0;
		private double _windSpeed = 0;
		private String _stationName;

		public WeatherData(String stationName)
		{
			_stationName = stationName;
		}

		public double Temperature { get => _temperature; }
		public double Humidity { get => _humidity; }
		public double Pressure { get => _pressure; }
		public double WindDirection { get => _windDirection; }
		public double WindSpeed { get => _windSpeed; }

		public void MeasurementsChanged() => NotifyObservers();

		public void SetMeasurements(double temp, double humidity, double pressure, double windDirection, double windSpeed)
		{
			_humidity = humidity;
			_temperature = temp;
			_pressure = pressure;
			_windSpeed = windSpeed;
			_windDirection = windDirection;

			MeasurementsChanged();
		}

		protected override WeatherInfo GetChangedData()
		{
			return new WeatherInfo
			{
				Temperature = Temperature,
				Humidity = Humidity,
				Pressure = Pressure,
				WindSpeed = WindSpeed,
				WindDirection = WindDirection,
				StationName = _stationName
			};
		}
	}
}
