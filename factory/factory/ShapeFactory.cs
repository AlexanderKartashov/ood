using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using painter.parsers;
using painter_declarations;

namespace painter
{
	public class ShapeFactory : IShapeFactory
	{
		private readonly IEnumerable<IShapeParser> _shapesParsers;

		public ShapeFactory(IEnumerable<IShapeParser> shapesParsers) => _shapesParsers = shapesParsers ?? throw new ArgumentNullException(nameof(shapesParsers));

		public Shape CreateShape(string descr)
		{
			if (descr == null || descr.Length == 0)
			{
				throw new ArgumentNullException("Shape data is null or empty");
			}

			const string pattern = @"^(\w+?)\s(.+)$";

			var match = Regex.Match(descr, pattern, RegexOptions.IgnoreCase);
			if (match.Success && match.Groups.Count == 3)
			{
				var shapeName = match.Groups[1];
				var shapeDescr = match.Groups[2];

				var enumerator = _shapesParsers.GetEnumerator();
				while(enumerator.MoveNext())
				{
					if (String.Compare(enumerator.Current.ShapeType, shapeName.Value, true) == 0)
					{
						return enumerator.Current.Parse(shapeDescr.Value);
					}
				}
				throw new ArgumentException($"Unsupported shape type {shapeName.Value}");
			}
			else
			{
				throw new ArgumentException($"Invalid shape data format {descr}");
			}
		}
	}

}
