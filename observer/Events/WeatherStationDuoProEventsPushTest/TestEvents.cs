using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherStationDuoProEventsPush;
using NSubstitute;

namespace WeatherStationDuoProEventsPushTest
{
	[TestFixture]
	public class TestEvents
	{
		[Test]
		public void TestEventsSubscription()
		{
			WeatherData data1 = new WeatherData("station1");
			WeatherData data2 = new WeatherData("station2");

			var handler = Substitute.For<EventHandler<WeatherInfo>>();

			data1.MeasurementsChanged += handler;
			data2.MeasurementsChanged += handler;

			Assert.That(() => { data1.SetMeasurements(0, 0.7, 751, 90, 10); }, Throws.Nothing);
			Assert.That(() => { data2.SetMeasurements(1, 0.4, 760, 0, 5); }, Throws.Nothing);

			var expected1 = new WeatherInfo()
			{
				Pressure = 751,
				Temperature = 0,
				Humidity = 0.7,
				WindDirection = 90,
				WindSpeed = 10,
				StationName = "station1"
			};
			var expected2 = new WeatherInfo() {
				Pressure = 760,
				Temperature = 1,
				Humidity = 0.4,
				WindDirection = 0,
				WindSpeed = 5,
				StationName = "station2"
			};

			Received.InOrder(() => {
				handler.Received().Invoke(data1, Arg.Is(expected1));
				handler.Received().Invoke(data2, Arg.Is(expected2));
			});

			handler.ClearReceivedCalls();

			data2.MeasurementsChanged -= handler;


			Assert.That(() => { data1.SetMeasurements(0, 0.7, 751, 90, 10); }, Throws.Nothing);
			handler.Received().Invoke(data1, Arg.Is(expected1));
			handler.ClearReceivedCalls();

			Assert.That(() => { data2.SetMeasurements(1, 0.4, 760, 0, 5); }, Throws.Nothing);
			handler.DidNotReceiveWithAnyArgs();
		}
	}
}
