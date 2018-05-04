using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace canvas
{
	public class DummyCanvasWithSave : DummyCanvas
	{
		public DummyCanvasWithSave(int w, int h) 
			: base(w, h)
		{
		}

		public void Save(string directory)
		{
			using (var file = File.CreateText(Path.Combine(directory, "canvas.txt")))
			{
				file.Write(Data);
			}
		}
	}
}
