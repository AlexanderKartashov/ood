﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherStationPro;

namespace WeatherStationMulti
{
	class Program
	{
		static void Main(string[] args)
		{
			var tw = Console.Out;

			WeatherData wdIn = new WeatherData("in");
			WeatherData wdOut = new WeatherData("out");

			Display display = new Display(tw);
			StatsDisplay sDisplay = new StatsDisplay(tw);
			wdIn.RegisterObserver(display, 0);
			wdIn.RegisterObserver(sDisplay, 2);
			wdOut.RegisterObserver(display, 10);

			wdIn.SetMeasurements(3, 0.7, 760, 45, 3);
			wdIn.SetMeasurements(4, 0.8, 761, 0, 3);

			wdOut.SetMeasurements(3, 0.7, 760, 0, 0);
		}
	}
}
