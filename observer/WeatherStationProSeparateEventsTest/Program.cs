using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherStationProSeparateEvents;

namespace WeatherStationProSeparateEventsTest
{
	class Program
	{
		static void Main(string[] args)
		{
			TextWriter tw = Console.Out;

			var display = new Display(tw);
			var wd = new WeatherData();

			// no observers
			Console.Out.WriteLine("===No observers===");
			wd.SetMeasurements(12, 0.3, 765, 0, 0);

			wd.RegisterObserver(display, EventType.Humidity, 0);
			wd.RegisterObserver(display, EventType.Pressure, 0);

			// temperature changed
			Console.Out.WriteLine("===Temperature changed===");
			wd.SetMeasurements(22, 0.3, 765, 0, 0);

			// temperature changed
			Console.Out.WriteLine("===Values changed===");
			wd.SetMeasurements(22, 0.4, 740, 0, 0);
			
			wd.RemoveObserver(display, EventType.Pressure);

			// pressure changed
			Console.Out.WriteLine("===Pressure changed===");
			wd.SetMeasurements(22, 0.4, 745, 0, 0);

			// humidity changed
			Console.Out.WriteLine("===Humidity changed===");
			wd.SetMeasurements(22, 0.5, 745, 0, 0);
		}
	}
}
