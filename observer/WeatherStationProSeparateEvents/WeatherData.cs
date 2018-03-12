using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherStationProSeparateEvents
{
	public class WeatherData : Observable<WeatherInfo, EventType>
	{
		private WeatherInfo _prevValue = new WeatherInfo {
			Pressure = double.MaxValue,
			Temperature = double.MaxValue,
			Humidity = double.MaxValue,
			WindDirection = double.MaxValue,
			WindSpeed = double.MaxValue
		};

		public void MeasurementsChanged(IList<EventType> changedValues) => NotifyObservers(changedValues);

		public void SetMeasurements(double temp, double humidity, double pressure, double windDirection, double windSpeed)
		{
			var events = new List<EventType>();

			void testSwapValuesAndAddEventChanged<T>(Func<T> getter, Action<T> setter, T newVal, EventType eventType)
			{
				if (!getter().Equals(newVal))
				{
					setter(newVal);
					events.Add(eventType);
				}
			}

			testSwapValuesAndAddEventChanged(
				() => _prevValue.Temperature,
				(v) => _prevValue.Temperature = v,
				temp,
				EventType.Temperature);
			testSwapValuesAndAddEventChanged(
				() => _prevValue.Pressure,
				(v) => _prevValue.Pressure = v,
				pressure,
				EventType.Pressure);
			testSwapValuesAndAddEventChanged(
				() => _prevValue.Humidity,
				(v) => _prevValue.Humidity = v,
				humidity,
				EventType.Humidity);
			testSwapValuesAndAddEventChanged(
				() => new { Speed = _prevValue.WindSpeed, Direction = _prevValue.WindDirection },
				(v) => { _prevValue.WindSpeed = v.Speed; _prevValue.WindDirection = v.Direction; },
				new { Speed = windSpeed, Direction = windDirection },
				EventType.Wind);

			MeasurementsChanged(events);
		}

		protected override WeatherInfo GetChangedData() => _prevValue;
	}
}
