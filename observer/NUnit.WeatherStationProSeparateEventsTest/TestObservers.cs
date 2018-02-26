using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherStationProSeparateEvents;
using NSubstitute;

namespace NUnit.WeatherStationProSeparateEventsTest
{
	[TestFixture]
	public class TestObservers
	{
		[Test]
		public void TestObserverRegistrationWithEventType()
		{
			WeatherData data = new WeatherData();

			var simpleObserver1 = Substitute.For<WeatherStationProSeparateEvents.IObserver<WeatherInfo>>();
			var simpleObserver2 = Substitute.For<WeatherStationProSeparateEvents.IObserver<WeatherInfo>>();

			data.RegisterObserver(simpleObserver1, EventType.Pressure, 0); // lowest priority
			data.RegisterObserver(simpleObserver2, EventType.Humidity, 10); // hightest priority

			// initial event
			{
				Assert.That(() => { data.SetMeasurements(0, 0.7, 750, 90, 10); }, Throws.Nothing);
				var expected = new WeatherInfo()
				{
					Temperature = 0,
					Pressure = 750,
					Humidity = 0.7,
					WindSpeed = 10,
					WindDirection = 90,
				};

				Received.InOrder(() => {
					simpleObserver2.Update(Arg.Is(expected));
					simpleObserver1.Update(Arg.Is(expected));
				});

				simpleObserver1.ClearReceivedCalls();
				simpleObserver2.ClearReceivedCalls();
			}

			// recieve only changed events
			{
				Assert.That(() => { data.SetMeasurements(0, 0.7, 751, 90, 10); }, Throws.Nothing); // humidity changed
				var expected = new WeatherInfo()
				{
					Temperature = 0,
					Pressure = 751,
					Humidity = 0.7,
					WindSpeed = 10,
					WindDirection = 90,
				};

				simpleObserver1.Received(1).Update(Arg.Is(expected));
				simpleObserver2.DidNotReceiveWithAnyArgs();

				simpleObserver1.ClearReceivedCalls();
				simpleObserver2.ClearReceivedCalls();
			}

			// change receieve event types
			{
				data.RegisterObserver(simpleObserver1, EventType.Wind, 5);
				data.RemoveObserver(simpleObserver2, EventType.Humidity);
				data.RegisterObserver(simpleObserver2, EventType.Temperature);

				Assert.That(() => { data.SetMeasurements(0, 0.2, 755, 45, 10); }, Throws.Nothing); // wind changed and pressure changed
				var expected = new WeatherInfo()
				{
					Temperature = 0,
					Pressure = 755,
					Humidity = 0.2,
					WindSpeed = 10,
					WindDirection = 45,
				};

				simpleObserver2.DidNotReceiveWithAnyArgs();
				simpleObserver1.Received(1).Update(Arg.Is(expected));
			}
		}
	}
}
