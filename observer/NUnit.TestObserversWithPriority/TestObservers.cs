using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using full_statistic;
using NSubstitute;

namespace NUnit.TestObserversWithPriority
{
	[TestFixture]
	public class TestObservers
	{
		[Test]
		public void TestObserverRegistrationWithPriority()
		{
			WeatherData data = new WeatherData();

			var simpleObserver1 = Substitute.For<full_statistic.IObserver<WeatherInfo>>();
			var simpleObserver2 = Substitute.For<full_statistic.IObserver<WeatherInfo>>();
			var simpleObserver3 = Substitute.For<full_statistic.IObserver<WeatherInfo>>();

			data.RegisterObserver(simpleObserver1, 0); // lowest priority
			data.RegisterObserver(simpleObserver2, 42); // hightest priority
			data.RegisterObserver(simpleObserver3, 13); // middle priority

			Assert.That(() => { data.SetMeasurements(0, 0.7, 750); }, Throws.Nothing);
			var expected = new WeatherInfo()
			{
				Temperature = 0,
				Pressure = 750,
				Humidity = 0.7
			};
			Received.InOrder(() => {
				simpleObserver2.Update(Arg.Is(expected));
				simpleObserver3.Update(Arg.Is(expected));
				simpleObserver1.Update(Arg.Is(expected));
			});
		}
	}
}
