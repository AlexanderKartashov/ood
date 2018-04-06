using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using painter.parsers;

namespace painter
{
	public class ShapeFactory : IShapeFactory
	{
		private readonly IEnumerator<IShapeParser> _shapesParsers;

		public ShapeFactory(IEnumerator<IShapeParser> shapesParsers) => _shapesParsers = shapesParsers ?? throw new ArgumentNullException(nameof(shapesParsers));

		public Shape CreateShape(string descr)
		{
			const string pattern = @"^(\w+?)\s(.+)$";

			var match = Regex.Match(descr, pattern, RegexOptions.IgnoreCase);
			if (match.Success && match.Groups.Count == 3)
			{
				var shapeName = match.Groups[1];
				var shapeDescr = match.Groups[2];

				_shapesParsers.Reset();
				while(_shapesParsers.MoveNext())
				{
					if (String.Compare(_shapesParsers.Current.ShapeType, shapeName.Value, true) == 0)
					{
						return _shapesParsers.Current.Parse(shapeDescr.Value);
					}
				}
				throw new ArgumentException($"invalid shape name {shapeName.Value}");
			}
			else
			{
				throw new ArgumentException("invalid string format");
			}
		}
	}

}
