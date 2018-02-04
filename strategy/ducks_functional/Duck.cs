using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ducks_functional
{
	using DanceBehabiour = Action;
	using QuackBehaviour = Action;
	using FlyBehaviour = Action;

	public class Duck
    {
		public static Duck CreateDuck(StringBuilder stringBuilder,
			DanceBehabiour danceBehaviuor,
			QuackBehaviour quackBehaviuor,
			FlyBehaviour flyBehaviuor)
		{
			return new Duck(stringBuilder, danceBehaviuor, quackBehaviuor, flyBehaviuor);
		}

		public DanceBehabiour DanceBehaviuor { set => _danceBehaviuor = value; }
		public QuackBehaviour QuackBehaviuor { set => _quackBehaviuor = value; }
		public FlyBehaviour FlyBehaviuor { set => _flyBehaviuor = value; }

		public void Fly() => _flyBehaviuor();

		public void Quack() => _quackBehaviuor();

		public void Dance() => _danceBehaviuor();

		public void Draw() => _stringBuilder?.Append("draw");

		public void Swim() => _stringBuilder?.Append("swim");

		private Duck(StringBuilder stringBuilder,
			DanceBehabiour danceBehaviuor,
			QuackBehaviour quackBehaviuor,
			FlyBehaviour flyBehaviuor)
		{
			_stringBuilder = stringBuilder;
			_danceBehaviuor = danceBehaviuor;
			_quackBehaviuor = quackBehaviuor;
			_flyBehaviuor = flyBehaviuor;
		}

		private readonly StringBuilder _stringBuilder;
		private DanceBehabiour _danceBehaviuor;
		private QuackBehaviour _quackBehaviuor;
		private FlyBehaviour _flyBehaviuor;
	}
}
