using NUnit.Framework;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using full_statistic;

namespace NUnit.SafeObserverRemoveTest
{
	[TestFixture]
	public class TestObserver
	{
		[Test]
		public void TestSelfRemovingObserver()
		{
			WeatherData data = new WeatherData();

			SelfRemovingObserver observer0 = new SelfRemovingObserver(data);
			MockObserver observer1 = new MockObserver();
			MockObserver observer2 = new MockObserver();
			data.RegisterObserver(observer0, 2);
			data.RegisterObserver(observer1, 1);
			data.RegisterObserver(observer2);

			Assert.That(() => { data.SetMeasurements(0, 0.7, 750); }, Throws.Nothing);

			Assert.That(observer0.Calls, Is.EqualTo(1));
			Assert.That(observer1.Calls, Is.EqualTo(1));
			Assert.That(observer2.Calls, Is.EqualTo(1));

			Assert.That(() => { data.SetMeasurements(15, 0.5, 759); }, Throws.Nothing);

			Assert.That(observer0.Calls, Is.EqualTo(1));
			Assert.That(observer1.Calls, Is.EqualTo(2));
			Assert.That(observer2.Calls, Is.EqualTo(2));
		}
	}
}
