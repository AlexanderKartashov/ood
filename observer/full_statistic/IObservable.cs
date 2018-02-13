using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace full_statistic
{
	public interface IObservable<T>
	{
		void RegisterObserver(IObserver<T> observer);
		void NotifyObservers();
		void RemoveObserver(IObserver<T> observer);
	}
}
