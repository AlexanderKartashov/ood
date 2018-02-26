using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherStationProSeparateEvents
{
	public interface IObservable<T>
	{
		void RegisterObserver(IObserver<T> observer, EventType eventType, uint priority = 0);
		void NotifyObservers(IList<EventType> changedValues);
		void RemoveObserver(IObserver<T> observer, EventType eventType);
	}
}
