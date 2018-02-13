using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using full_statistic;

namespace NUnit.SafeObserverRemoveTest
{
	class MockObserver : full_statistic.IObserver<WeatherInfo>
	{
		private int _calls = 0;

		public int Calls { get => _calls; }

		public void Update(WeatherInfo data)
		{
			// do nothing
			++_calls;
		}
	}
}
