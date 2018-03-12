using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherStationProSeparateEvents;

namespace WeatherStationProSeparateEvents
{
	public abstract class Observable<T, EventType> : IObservable<T, EventType>
	{
		private readonly ISet<ObserverInfo<T, EventType>> _observers = new HashSet<ObserverInfo<T, EventType>>();

		public void NotifyObservers(IList<EventType> changedValues)
		{
			_observers.
				OrderByDescending(observerInfo => observerInfo.Priority).                                       // order observers
				Where(observerInfo => changedValues.Any(eventType => eventType.Equals(observerInfo.Type))).		// find observers, subscribed on changed events
				Distinct(new ObserverComparer<T, EventType>()).                                                            // remove equal observers from selection, subscribed on different event to prevent multiple notification
				ToList().                                                                                       // make list of selected elements
				ForEach(x => x.Observer.Update(GetChangedData()));                                              // call update
		}

		public void RegisterObserver(IObserver<T> observer, EventType eventType, uint priority = 0)
		{
			_observers.Add(new ObserverInfo<T, EventType> { Observer = observer, Priority = priority, Type = eventType });
		}

		public void RemoveObserver(IObserver<T> observer, EventType eventType)
		{
			_observers.Remove(new ObserverInfo<T, EventType> { Observer = observer, Type = eventType, Priority = 0 });
		}

		protected abstract T GetChangedData();
	}
}

class ObserverComparer<T, EventType> : IEqualityComparer<ObserverInfo<T, EventType>>
{
	public bool Equals(ObserverInfo<T, EventType> x, ObserverInfo<T, EventType> y)
	{
		if (Object.ReferenceEquals(x, y)) return true;

		if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
			return false;

		return x.Observer == y.Observer;
	}

	public int GetHashCode(ObserverInfo<T, EventType> obj)
	{
		return obj.Observer.GetHashCode();
	}
}