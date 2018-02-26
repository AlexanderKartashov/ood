using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherStationProSeparateEvents;

namespace WeatherStationProSeparateEvents
{
	public abstract class Observable<T> : IObservable<T>
	{
		private readonly ISet<ObserverInfo<T>> _observers = new HashSet<ObserverInfo<T>>();

		public void NotifyObservers(IList<EventType> changedValues)
		{
			_observers.
				OrderByDescending(observerInfo => observerInfo.Priority).                                       // order observers
				Where(observerInfo => changedValues.Any(eventType => eventType == observerInfo.EventType)).		// find observers, subscribed on changed events
				Distinct(new ObserverComparer<T>()).                                                            // remove equal observers from selection, subscribed on different event to prevent multiple notification
				ToList().                                                                                       // make list of selected elements
				ForEach(x => x.Observer.Update(GetChangedData()));                                              // call update
		}

		public void RegisterObserver(IObserver<T> observer, EventType eventType, uint priority = 0)
		{
			_observers.Add(new ObserverInfo<T> { Observer = observer, Priority = priority, EventType = eventType });
		}

		public void RemoveObserver(IObserver<T> observer, EventType eventType)
		{
			_observers.Remove(new ObserverInfo<T> { Observer = observer, EventType = eventType, Priority = 0 });
		}

		protected abstract T GetChangedData();
	}
}

class ObserverComparer<T> : IEqualityComparer<ObserverInfo<T>>
{
	public bool Equals(ObserverInfo<T> x, ObserverInfo<T> y)
	{
		if (Object.ReferenceEquals(x, y)) return true;

		if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
			return false;

		return x.Observer == y.Observer;
	}

	public int GetHashCode(ObserverInfo<T> obj)
	{
		return obj.Observer.GetHashCode();
	}
}