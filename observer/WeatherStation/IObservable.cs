using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherStation
{
	public interface IObservable<T>
	{
		void RegisterObserver(IObserver<T> observer, uint priority = 0);
		void NotifyObservers();
		void RemoveObserver(IObserver<T> observer);
	}
}
