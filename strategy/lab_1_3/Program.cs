using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_1_3
{
	class Program
	{
		static void Main(string[] args)
		{
			// wrap classes in lambdas
			{
				var stringBuilder = new StringBuilder();
				var mallardDuck = ducks_functional.Duck.CreateDuck(
					stringBuilder,
					() => new ducks.Dance.Waltz(stringBuilder).Dance(),
					() => new ducks.Quack.DuckQuack(stringBuilder).Quack(),
					()=> new ducks.Fly.FlyWithWings(stringBuilder).Fly()
				);
				Console.Out.WriteLine(FormatDuckActivitiesString(mallardDuck, stringBuilder, "Mallard duck"));
			}

			// only lambdas
			{
				var stringBuilder = new StringBuilder();
				var redheadDuck = ducks_functional.Duck.CreateDuck(
					stringBuilder,
					() => stringBuilder.Append("manuette"),
					() => stringBuilder.Append("quack"),
					() => stringBuilder.Append("fly with wings")
				);
				Console.Out.WriteLine(FormatDuckActivitiesString(redheadDuck, stringBuilder, "Readhead duck"));
			}

			// functions
			{
				var stringBuilder = new StringBuilder();
				var rubberDuck = ducks_functional.Duck.CreateDuck(
					stringBuilder,
					ducks_functional.Dance.DanceNoWay.Dance,
					() => ducks_functional.Quack.Squeak.Quack(stringBuilder),
					ducks_functional.Fly.FlyNoWay.Fly
				);
				Console.Out.WriteLine(FormatDuckActivitiesString(rubberDuck, stringBuilder, "Rubber duck"));
			}

			// functional with state
			{
				var stringBuilder = new StringBuilder();
				var smartMallargDuck = ducks_functional.Duck.CreateDuck(
					stringBuilder,
					() => ducks_functional.Dance.Manuette.Dance(stringBuilder),
					() => ducks_functional.Quack.DuckQuack.Quack(stringBuilder),
					ducks_functional.Fly.FlyWithWingsWithState.Fly(stringBuilder)
				);

				Console.Out.WriteLine(FormatDuckActivitiesString(smartMallargDuck, stringBuilder, "Smart mallard duck"));
				Console.Out.WriteLine(FormatDuckActivitiesString(smartMallargDuck, stringBuilder, "Smart mallard duck"));
				Console.Out.WriteLine(FormatDuckActivitiesString(smartMallargDuck, stringBuilder, "Smart mallard duck"));

				{
					var anotherSmartMallargDuck = ducks_functional.Duck.CreateDuck(
						stringBuilder,
						() => ducks_functional.Dance.Manuette.Dance(stringBuilder),
						() => ducks_functional.Quack.DuckQuack.Quack(stringBuilder),
						ducks_functional.Fly.FlyWithWingsWithState.Fly(stringBuilder)
					);

					Console.Out.WriteLine(FormatDuckActivitiesString(anotherSmartMallargDuck, stringBuilder, "Smart mallard duck #2"));
					Console.Out.WriteLine(FormatDuckActivitiesString(anotherSmartMallargDuck, stringBuilder, "Smart mallard duck #2"));
				}
			}
		}

		private static string FormatDuckActivitiesString(ducks_functional.Duck duck, StringBuilder stringBuilder, string duckName)
		{
			stringBuilder.AppendFormat(String.Format("{0} flies: ", duckName));
			duck.Fly();
			stringBuilder.Append(", dances: ");
			duck.Dance();
			stringBuilder.Append(", quacks: ");
			duck.Quack();

			var str = stringBuilder.ToString();
			stringBuilder.Clear();
			return str;
		}
	}
}
