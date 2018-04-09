using painter.shapes;
using painter_declarations;
using System;
using System.ComponentModel.Composition;
using System.Text.RegularExpressions;

namespace painter.parsers
{
	[Export(typeof(IShapeParser))]
	public class RectangleParser : IShapeParser
	{
		private readonly string _regex;

		public string ShapeType => "rectangle";
		public string ShapeInfoFormat { get => _regex; }

		public RectangleParser()
		{
			var regexGenerator = new RegexGenerator();
			regexGenerator.Start();
			regexGenerator.ParsePoint("lt");
			regexGenerator.ParsePoint("rb");
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
				return new Rectangle(
					new Point(int.Parse(groups[1].Value), int.Parse(groups[2].Value)),
					new Point(int.Parse(groups[3].Value), int.Parse(groups[4].Value)),
					ColorParser.Parse(groups[5].Value)
				);
			}
			else
			{
				throw new ArgumentException($"Invalid rectangle data {description}");
			}
		}
	}

}
