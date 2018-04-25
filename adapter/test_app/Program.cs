using adapters;
using graphics_lib;
using modern_graphics_lib;
using shape_drawing_lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_app
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Should we use new API (y)?");
			string userInput = Console.ReadLine();

			if ((userInput != null) && (userInput == "y" || userInput == "Y"))
			{
				PaintPictureOnModernGraphicsRenderer();
			}
			else
			{
				PaintPictureOnCanvas();
			}
		}

		static void PaintPictureOnCanvas()
		{
			Canvas simpleCanvas = new Canvas(Console.Out);
			CanvasPainter canvasPainter = new CanvasPainter(simpleCanvas);
			Painiter picturePainter = new Painiter();
			picturePainter.PaintPicture(canvasPainter);
		}

		static void PaintPictureOnModernGraphicsRenderer()
		{
			using (var painter = new ObjectAdapter(new ModernGraphicsRenderer(Console.Out)))
			{
				CanvasPainter canvasPainter = new CanvasPainter(painter);
				Painiter picturePainter = new Painiter();
				picturePainter.PaintPicture(canvasPainter);
			}
		}
	}
}
