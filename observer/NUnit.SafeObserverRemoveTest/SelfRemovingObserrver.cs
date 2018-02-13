using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using full_statistic;

namespace NUnit.SafeObserverRemoveTest
{
	class SelfRemovingObserver : full_statistic.IObserver<WeatherInfo>
	{
		private WeatherData _data;

		private int _calls = 0;

		public int Calls { get => _calls; }

		public SelfRemovingObserver(WeatherData data) => _data = data;

		public void Update(WeatherInfo value)
		{
			++_calls;
			_data.RemoveObserver(this);
		}
	}
}
