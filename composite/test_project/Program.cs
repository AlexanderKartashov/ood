using composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PointI = composite.Point<int>;
using PointD = composite.Point<double>;

namespace test_project
{
	class Program
	{
		static void Main(string[] args)
		{
			const int slideW = 1920;
			const int slideH = 1080;

			var facade = new ShapesFacade(new PointI(slideW, slideH));

			var earth = facade.AddNewShapeInSlide(new Rectangle(new PointD(0, 0), new PointD(1, 1)) { Frame = new Rect(new PointI(0, 0), slideW, 600) });
			earth.FillStyle = new FillStyle() { Enable = true, Color = new RGBAColor(0x656600FF) };

			var lake = facade.AddNewShapeInSlide(new Ellipse(new PointD(0, 0), new PointD(1, 1)) { Frame = new Rect(new PointI(1000, 200), 500, 200) });
			lake.LineStyle = new LineStyle() { Enable = true, LineWidth = 2, Color = new RGBAColor(0x000000FF) };
			lake.FillStyle = new FillStyle() { Enable = true, Color = new RGBAColor(0x00cdd4FF) };

			var sun = facade.AddNewShapeInSlide(new Ellipse(new PointD(0, 0), new PointD(1, 1)) { Frame = new Rect(new PointI(1000, 700), 200, 200) });
			sun.FillStyle = new FillStyle() { Enable = true, Color = new RGBAColor(0xffff00FF) };
			
			var walls = new Rectangle(new PointD(0, 0), new PointD(1, 1)) { Frame = new Rect(new PointI(200, 400), 400, 400) };
			walls.FillStyle = new FillStyle() { Enable = true, Color = new RGBAColor(0xff0000FF) };

			var window = new Rectangle(new PointD(0, 0), new PointD(1, 1)) { Frame = new Rect(new PointI(300, 550), 150, 150) };
			window.FillStyle = new FillStyle() { Enable = true, Color = new RGBAColor(0x0000ffFF) };

			var roof = new Triangle(new PointD(0, 0), new PointD(0.5, 0.5), new PointD(1, 0)) { Frame = new Rect(new PointI(200, 800), 400, 400) };
			roof.FillStyle = new FillStyle() { Enable = true, Color = new RGBAColor(0x00ff00FF) };

			var home = new GroupShape();
			home.Insert(walls);
			home.Insert(window);
			home.Insert(roof);

			facade.AddNewShapeInSlide(home);

			var circle = facade.AddNewShapeInSlide(new Ellipse(new PointD(0, 0), new PointD(1, 1)) { Frame = new Rect(new PointI(0, 100), new PointI(100, 0)) });
			circle.LineStyle = new LineStyle() { Enable = true, LineWidth = 4, Color = new RGBAColor(0xFF0000FF) };

			DrawOnCanvasAndSave(facade, "original");

			home.Frame = new Rect(new PointI(100, 300), 200, 200);
			DrawOnCanvasAndSave(facade, "resized");

			facade.RemoveShapeFromGroup(home, window);
			home.FillStyle = new FillStyle() { Enable = true, Color = new RGBAColor(0xFFFFFFFF) };
			home.LineStyle = new LineStyle() { Enable = true, Color = new RGBAColor(0x000000FF), LineWidth = 4 };
			DrawOnCanvasAndSave(facade, "white_house");
		}

		static void DrawOnCanvasAndSave(ShapesFacade facade, string dir)
		{
			var bitmapCanvas = Draw((w, h) => new BitmapCanvas(new PointI(w, h)), facade.Slide);
			var dummyCanvas = Draw((w, h) => new DummyCanvas(new PointI(w, h)), facade.Slide);

			var saver = new CanvasVisitor(dir);
			saver.Visit((dynamic)bitmapCanvas);
			saver.Visit((dynamic)dummyCanvas);
		}

		private delegate ICanvas CanvasCreator(int w, int h);

		private static ICanvas Draw(CanvasCreator canvasCreator, ISlide slide)
		{
			var canvas = canvasCreator(slide.Size.X, slide.Size.Y);
			slide.Draw(canvas);
			return canvas;
		}
	}

	class ShapesFacade
	{
		public ISlide Slide { get; private set; }

		public ShapesFacade(PointI size) => Slide = new Slide(size);

		public IShape AddNewShapeInSlide(IShape shape)
		{
			Slide.Shapes.Insert(shape);
			return shape;
		}

		public void Ungroup(IGroupShape shapeGroup)
		{
			shapeGroup.Shapes.ToList().ForEach(x => {
				Slide.Shapes.Insert(x);
				shapeGroup.Remove(x);
			});
		}

		public void RemoveShapeFromSlide(IShape shape)
		{
			// remove shape from root slide shapes, if exists
			Slide.Shapes.Remove(shape);
		}

		public void RemoveShapeFromGroup(IGroupShape groupShape, IShape shape)
		{
			groupShape.Remove(shape);
			// add shape to root slide shape list
			Slide.Shapes.Insert(shape);
		}
	}
}
