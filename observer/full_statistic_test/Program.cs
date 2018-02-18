using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using full_statistic;

namespace full_statistic_test
{

	class Program
	{
		static void Main(string[] args)
		{
			var tw = Console.Out;

			WeatherData wd = new WeatherData();

			Display display = new Display(tw);
			wd.RegisterObserver(display, 0);

			StatsDisplay statsDisplay = new StatsDisplay(tw);
			wd.RegisterObserver(statsDisplay, 10);

			wd.SetMeasurements(3, 0.7, 760);
			wd.SetMeasurements(4, 0.8, 761);

			wd.RemoveObserver(statsDisplay);

			wd.SetMeasurements(10, 0.8, 761);
			wd.SetMeasurements(-10, 0.8, 761);
		}
	}
}
