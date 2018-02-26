using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherStationProSeparateEvents
{
	class ObserverInfo<T>
	{
		public IObserver<T> Observer { get; set; }
		public uint Priority { get; set; }
		public EventType EventType { get; set;}

		public override bool Equals(object obj)
		{
			var info = obj as ObserverInfo<T>;
			if (info == null)
			{
				return false;
			}
			return info.Observer == Observer && info.EventType == EventType;
		}

		public override int GetHashCode()
		{
			var hashCode = 1337709174;
			hashCode = hashCode * -1521134295 + EqualityComparer<IObserver<T>>.Default.GetHashCode(Observer);
			hashCode = hashCode * -1521134295 + EventType.GetHashCode();
			return hashCode;
		}
	}
}
