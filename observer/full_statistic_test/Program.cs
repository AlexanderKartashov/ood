using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherStationPro;
using WeatherStation;

namespace WeatherStationProDuoTest
{
	class Program
	{
		static void Main(string[] args)
		{
			var tw = Console.Out;

			WeatherStationPro.WeatherData wdOut = new WeatherStationPro.WeatherData("out");
			WeatherStation.WeatherData wdIn = new WeatherStation.WeatherData("in");

			WeatherStation.Display displayIn = new WeatherStation.Display(tw);
			wdIn.RegisterObserver(displayIn, 0);

			WeatherStationPro.Display statsDisplayOut = new WeatherStationPro.Display(tw);
			wdOut.RegisterObserver(statsDisplayOut, 10);

			wdOut.SetMeasurements(3, 0.7, 760, 90, 2);
			wdIn.SetMeasurements(4, 0.8, 761);

			wdOut.RemoveObserver(statsDisplayOut);

			wdIn.SetMeasurements(10, 0.8, 761);
			wdOut.SetMeasurements(-10, 0.8, 761, 220, 10);
		}
	}
}
