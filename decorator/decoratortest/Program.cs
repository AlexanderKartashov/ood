using decorator.tea;
using decorator.coffee;
using decorator.milkshake;
using decorator.condiments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using decorator;

namespace decoratortest
{
	class Program
	{
		static void Main(string[] args)
		{
			var greenTeaWithLemon = new Lemon(new Tea(), 3);
			Print(greenTeaWithLemon);

			// max chocolate loubles = 5
			var doubleLatteWithChocoladeLoublesAndCream = new Cream(new ChocolateLoubles(new DoubleLatte(), 10));
			Print(doubleLatteWithChocoladeLoublesAndCream);
		}

		private static void Print(IBeverage beverage)
		{
			Console.WriteLine(String.Format("{0} costs = {1}", beverage.Description, beverage.Cost));
		}
	}
}