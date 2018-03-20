using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherStationPro;
using NSubstitute;

namespace NUnit.TestObserversWithPriority
{
	[TestFixture]
	public class TestObservers
	{
		[Test]
		public void TestObserverRegistrationWithPriority()
		{
			var station = "station";
			WeatherData data = new WeatherData(station);

			var simpleObserver1 = Substitute.For<WeatherStationPro.IObserver<WeatherInfo>>();
			var simpleObserver2 = Substitute.For<WeatherStationPro.IObserver<WeatherInfo>>();
			var simpleObserver3 = Substitute.For<WeatherStationPro.IObserver<WeatherInfo>>();

			data.RegisterObserver(simpleObserver1, 0); // lowest priority
			data.RegisterObserver(simpleObserver2, 42); // hightest priority
			data.RegisterObserver(simpleObserver3, 13); // middle priority

			Assert.That(() => { data.SetMeasurements(0, 0.7, 750, 90, 10); }, Throws.Nothing);
			var expected = new WeatherInfo()
			{
				Temperature = 0,
				Pressure = 750,
				Humidity = 0.7,
				WindSpeed = 10,
				WindDirection = 90,
				StationName = station
			};
			Received.InOrder(() => {
				simpleObserver2.Update(Arg.Is(expected));
				simpleObserver3.Update(Arg.Is(expected));
				simpleObserver1.Update(Arg.Is(expected));
			});
		}
	}
}
