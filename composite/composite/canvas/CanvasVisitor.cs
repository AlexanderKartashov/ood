using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace composite
{
	public class CanvasVisitor
	{
		private String _directory;

		public CanvasVisitor(string directory)
		{
			_directory = directory ?? throw new ArgumentNullException(nameof(directory));

			if (!Directory.Exists(directory))
			{
				Directory.CreateDirectory(directory);
			}
		}

		public void Visit(BitmapCanvas bitmap)
		{
			new BitmapSaver().Save(_directory, bitmap);
		}

		public void Visit(DummyCanvas dummy)
		{
			new DummySaver().Save(_directory, dummy);
		}
	}
}
