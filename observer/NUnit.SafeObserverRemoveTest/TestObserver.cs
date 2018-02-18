using NUnit.Framework;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using full_statistic;
using NSubstitute;

namespace NUnit.SafeObserverRemoveTest
{
	[TestFixture]
	public class TestObserver
	{
		[Test]
		public void TestSelfRemovingObserver()
		{
			WeatherData data = new WeatherData();

			var selfRemovingObserver = Substitute.For<full_statistic.IObserver<WeatherInfo>>();
			selfRemovingObserver.When(x => x.Update(Arg.Any<WeatherInfo>())).Do(Callback.Always(x => data.RemoveObserver(selfRemovingObserver)));

			var simpleObserver1 = Substitute.For<full_statistic.IObserver<WeatherInfo>>();
			var simpleObserver2 = Substitute.For<full_statistic.IObserver<WeatherInfo>>();

			data.RegisterObserver(selfRemovingObserver);
			data.RegisterObserver(simpleObserver1);
			data.RegisterObserver(simpleObserver2);

			{
				Assert.That(() => { data.SetMeasurements(0, 0.7, 750); }, Throws.Nothing);

				var expected = new WeatherInfo()
				{
					Temperature = 0,
					Pressure = 750,
					Humidity = 0.7
				};
				selfRemovingObserver.Received(1).Update(expected);
				simpleObserver1.Received(1).Update(expected);
				simpleObserver2.Received(1).Update(expected);
			}

			selfRemovingObserver.ClearReceivedCalls();
			simpleObserver1.ClearReceivedCalls();
			simpleObserver2.ClearReceivedCalls();

			{
				var expected = new WeatherInfo()
				{
					Temperature = 15,
					Pressure = 759,
					Humidity = 0.5
				};

				Assert.That(() => { data.SetMeasurements(15, 0.5, 759); }, Throws.Nothing);

				selfRemovingObserver.DidNotReceive().Update(Arg.Any<WeatherInfo>());
				simpleObserver1.Received(1).Update(Arg.Is(expected));
				simpleObserver2.Received(1).Update(Arg.Is(expected));
			}
		}
	}
}
