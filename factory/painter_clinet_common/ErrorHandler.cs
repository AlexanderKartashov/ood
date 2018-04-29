using painter.sdk;
using painter_declarations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace painter_clinet_common
{
	public class ErrorHandler : TextWriter
	{
		private readonly TextWriter _textWriter;

		public IEnumerable<IShapeInfo> ShapeInfos { private get; set; }

		public override Encoding Encoding => _textWriter.Encoding;

		public ErrorHandler(TextWriter textWriter)
		{
			_textWriter = textWriter ?? throw new ArgumentNullException(nameof(textWriter));
		}

		public override void WriteLine(string value)
		{
			_textWriter.WriteLine(value);
			if (ShapeInfos != null)
			{
				_textWriter.WriteLine("Supported shapes list:");
			}
			ShapeInfos?.ToList().ForEach(x => _textWriter.WriteLine($"Shape: {x.ShapeType} {x.ShapeInfoFormat}"));
			_textWriter.Write("Supported colors list:");
			foreach(Color color in Enum.GetValues(typeof(Color)))
			{
				_textWriter.Write($" {color.ToString()}");
			}
			_textWriter.WriteLine();
		}
	}
}
