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
			data.RegisterObserver(observer0);
			data.RegisterObserver(observer1);
			data.RegisterObserver(observer2);

			Assert.DoesNotThrow(() => { data.SetMeasurements(0, 0.7, 750); });

			Assert.AreEqual(observer0.Calls, 1);
			Assert.AreEqual(observer1.Calls, 1);
			Assert.AreEqual(observer2.Calls, 1);

			Assert.DoesNotThrow(() => { data.SetMeasurements(15, 0.5, 759); });

			Assert.AreEqual(observer0.Calls, 1);
			Assert.AreEqual(observer1.Calls, 2);
			Assert.AreEqual(observer2.Calls, 2);
		}
	}
}
