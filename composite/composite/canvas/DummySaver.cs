using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace composite
{
	class DummySaver
	{
		public void Save(String directory, DummyCanvas canvas)
		{
			using (var file = new StreamWriter(new FileStream(Path.Combine(directory, "commands.txt"), FileMode.Create, FileAccess.Write), Encoding.UTF8))
			{
				file.Write(canvas.Data);
			}
		}
	}
}
