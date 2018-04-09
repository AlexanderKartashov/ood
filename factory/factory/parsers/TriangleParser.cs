using painter.shapes;
using painter_declarations;
using System;
using System.ComponentModel.Composition;
using System.Text.RegularExpressions;

namespace painter.parsers
{
	[Export(typeof(IShapeParser))]
	public class TriangleParser : IShapeParser
	{
		private readonly string _regex;

		public string ShapeType => "triangle";
		public string ShapeInfoFormat { get => _regex; }

		public TriangleParser()
		{
			var regexGenerator = new RegexGenerator();
			regexGenerator.Start();
			regexGenerator.ParsePoint("v1");
			regexGenerator.ParsePoint("v2");
			regexGenerator.ParsePoint("v3");
			regexGenerator.ParseColor("c");
			regexGenerator.End();
			_regex = regexGenerator.Value;
		}

		public Shape Parse(string description)
		{
			var match = Regex.Match(description, _regex, RegexOptions.IgnoreCase);
			if (match.Success && match.Groups.Count == 8)
			{
				var groups = match.Groups;
				return new Triangle(
					new Point(int.Parse(groups[1].Value), int.Parse(groups[2].Value)),
					new Point(int.Parse(groups[3].Value), int.Parse(groups[4].Value)),
					new Point(int.Parse(groups[5].Value), int.Parse(groups[6].Value)),
					ColorParser.Parse(groups[7].Value)
				);
			}
			else
			{
				throw new ArgumentException($"Invalid triangle data {description}");
			}
		}
	}

}
