using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ducks;
using ducks.Dance;
using ducks.Fly;
using ducks.Quack;

namespace lab_1_1
{
	class Program
	{
		static void Main(string[] args)
		{
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
				var redheadDuck = Duck.CreateDuck(
					stringBuilder,
					new Manuette(stringBuilder),
					new DuckQuack(stringBuilder),
					new FlyWithWings(stringBuilder)
				);
				Console.Out.WriteLine(FormatDuckActivitiesString(redheadDuck, stringBuilder, "Readhead duck"));
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
			stringBuilder.AppendFormat(String.Format("{0} flies: ", duckName));
			duck.Fly();
			stringBuilder.Append(", dances: ");
			duck.Dance();
			stringBuilder.Append(", quacks: ");
			duck.Quack();

			return stringBuilder.ToString();
		}
	}
}
