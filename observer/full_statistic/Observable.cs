using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace full_statistic
{
	public abstract class Observable<T> : IObservable<T>
	{
		private readonly ISet<ObserverInfo<T>> _observers = new HashSet<ObserverInfo<T>>();

		public void NotifyObservers()
		{
			_observers.OrderBy(x => x.Priority).ToList().ForEach(x => x.Observer.Update(GetChangedData()));
		}

		public void RegisterObserver(IObserver<T> observer, uint priority = 0)
		{
			_observers.Add(new ObserverInfo<T>{ Observer = observer, Priority = priority });
		}

		public void RemoveObserver(IObserver<T> observer)
		{
			_observers.Remove(new ObserverInfo<T> { Observer = observer, Priority = 0 });
		}

		protected abstract T GetChangedData();
	}
}
