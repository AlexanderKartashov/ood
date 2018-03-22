using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherStationPro
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
		public double WindSpeed { get; private set; } = 0.0;
		public double WindAngle { get; private set; } = 0.0;

		public void MeasurementsChanged() => NotifyObservers();

		public void SetMeasurements(double temp, double humidity, double pressure, double windDirection, double windSpeed)
		{
			Humidity = humidity;
			Temperature = temp;
			Pressure = pressure;
			WindSpeed = windSpeed;
			WindAngle = windDirection;

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
				WindDirection = WindAngle,
				StationName = _stationName
			};
		}
	}
}
