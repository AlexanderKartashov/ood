using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace full_statistic
{
	public abstract class Observable<T> : IObservable<T>
	{
		private readonly ISet<IObserver<T>> _observers = new HashSet<IObserver<T>>();

		public void NotifyObservers() => _observers.ToList().ForEach(x => x.Update(GetChangedData()));

		public void RegisterObserver(IObserver<T> observer) => _observers.Add(observer);

		public void RemoveObserver(IObserver<T> observer) => _observers.Remove(observer);

		protected abstract T GetChangedData();
	}
}
