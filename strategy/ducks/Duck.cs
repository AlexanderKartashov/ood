using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ducks
{
    public class Duck
    {
		public static Duck CreateDuck(StringBuilder stringBuilder,
			IDanceBehaviuor danceBehaviuor,
			IQuackBehaviuor quackBehaviuor,
			IFlyBehaviuor flyBehaviuor)
		{
			return new Duck(stringBuilder, danceBehaviuor, quackBehaviuor, flyBehaviuor);
		}

		public IDanceBehaviuor DanceBehaviuor { set => _danceBehaviuor = value; }
		public IQuackBehaviuor QuackBehaviuor { set => _quackBehaviuor = value; }
		public IFlyBehaviuor FlyBehaviuor { set => _flyBehaviuor = value; }

		public void Fly() => _flyBehaviuor?.Fly();

		public void Quack() => _quackBehaviuor?.Quack();

		public void Dance() => _danceBehaviuor?.Dance();

        public void Draw() => _stringBuilder?.Append("draw");

		public void Swim() => _stringBuilder?.Append("swim");

		private Duck(StringBuilder stringBuilder,
			IDanceBehaviuor danceBehaviuor,
			IQuackBehaviuor quackBehaviuor,
			IFlyBehaviuor flyBehaviuor)
		{
			_stringBuilder = stringBuilder;
			_danceBehaviuor = danceBehaviuor;
			_quackBehaviuor = quackBehaviuor;
			_flyBehaviuor = flyBehaviuor;
		}

		private readonly StringBuilder _stringBuilder;
		private IDanceBehaviuor _danceBehaviuor;
		private IQuackBehaviuor _quackBehaviuor;
		private IFlyBehaviuor _flyBehaviuor;
	}
}
