using painter.shapes;
using painter_declarations;
using System;
using System.ComponentModel.Composition;
using System.Text.RegularExpressions;

namespace painter.parsers
{
	[Export(typeof(IShapeParser))]
	public class RectangularPolygonParser : IShapeParser
	{
		private readonly string _regex;

		public string ShapeType => "rectangularpolygon";
		public string ShapeInfoFormat { get => _regex; }

		public RectangularPolygonParser()
		{
			var regexGenerator = new RegexGenerator();
			regexGenerator.Start();
			regexGenerator.ParsePoint("ct");
			regexGenerator.ParseDigit("r");
			regexGenerator.ParseDigit("vc");
			regexGenerator.ParseColor("c");
			regexGenerator.End();
			_regex = regexGenerator.Value;
		}

		public Shape Parse(string description)
		{
			var match = Regex.Match(description, _regex, RegexOptions.IgnoreCase);
			if (match.Success && match.Groups.Count == 6)
			{
				var groups = match.Groups;
				return new RectangularPolygon(
					new Point(int.Parse(groups[1].Value), int.Parse(groups[2].Value)),
					uint.Parse(groups[3].Value),
					uint.Parse(groups[4].Value),
					ColorParser.Parse(groups[5].Value)
				);
			}
			else
			{
				throw new ArgumentException($"Invalid rectangular polygon data {description}");
			}
		}
	}

}
