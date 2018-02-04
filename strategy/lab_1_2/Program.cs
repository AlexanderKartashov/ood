using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ducks;
using ducks.Dance;
using ducks.Fly;
using ducks.Quack;

namespace lab_1_2
{
	class Program
	{
		static void Main(string[] args)
		{
			{
				var stringBuilder = new StringBuilder();
				var mallardDuckWithState = Duck.CreateDuck(
					stringBuilder,
					new Waltz(stringBuilder),
					new DuckQuack(stringBuilder),
					new FlyWithWingsWithState(stringBuilder)
				);
				Console.Out.WriteLine(FormatDuckActivitiesString(mallardDuckWithState, stringBuilder, "Smart mallard duck"));
			}

			{
				var stringBuilder = new StringBuilder();
				var mallardDuck = Duck.CreateDuck(
					stringBuilder,
					new Waltz(stringBuilder),
					new DuckQuack(stringBuilder),
					new FlyWithWings(stringBuilder)
				);
				Console.Out.WriteLine(FormatDuckActivitiesString(mallardDuck, stringBuilder, "Mallard duck"));
			}

			{
				var stringBuilder = new StringBuilder();
				var decoyDuck = Duck.CreateDuck(
					stringBuilder,
					new DanceNoWay(),
					new MuteQuack(),
					new FlyNoWay()
				);
				Console.Out.WriteLine(FormatDuckActivitiesString(decoyDuck, stringBuilder, "Decoy duck"));
			}
		}

		private static string FormatDuckActivitiesString(Duck duck, StringBuilder stringBuilder, string duckName)
		{
			stringBuilder.AppendFormat(String.Format("{0} ", duckName));

			for(var i = 0; i < 13; ++i)
			{
				stringBuilder.Append("flies: ");
				duck.Fly();
				stringBuilder.AppendLine();
			}
			
			stringBuilder.Append(", dances: ");
			duck.Dance();
			stringBuilder.Append(", quacks: ");
			duck.Quack();

			return stringBuilder.ToString();
		}
	}
}
