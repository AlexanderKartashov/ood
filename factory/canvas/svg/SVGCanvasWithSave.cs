using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace canvas
{
	public class SVGCanvasWithSave : SvgCanvas
	{
		public SVGCanvasWithSave(int w, int h)
			: base(w, h)
		{
		}

		public void Save(string directory)
		{
			using (var file = File.CreateText(Path.Combine(directory, "index.html")))
			{
				file.Write(Data);
			}
		}
	}
}
