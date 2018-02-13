using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace full_statistic
{
	class ObserverInfo<T>
	{
		public IObserver<T> Observer { get; set; }
		public uint Priority { get; set; }

		public override bool Equals(object obj)
		{
			var info = obj as ObserverInfo<T>;
			if (info == null)
			{
				return false;
			}
			return info.Observer == Observer;
		}

		public override int GetHashCode()
		{
			return Observer.GetHashCode();
		}
	}
}
