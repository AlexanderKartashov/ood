using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace painter
{
	public class Designer : IDesigner
	{
		private readonly IShapeFactory _shapeFactory;

		public Designer(IShapeFactory shapeFactory) => _shapeFactory = shapeFactory ?? throw new ArgumentNullException(nameof(shapeFactory));

		public PictureDraft CreateDraft(TextReader textReader, TextWriter errorReporter)
		{
			var pictureDraft = new PictureDraft();

			if (textReader == null)
			{
				return pictureDraft;
			}

			string str = textReader.ReadLine();
			while (str != null && str.Length != 0)
			{
				try
				{
					pictureDraft.AddShape(_shapeFactory.CreateShape(str));
				}
				catch (Exception e)
				{
					errorReporter?.WriteLine(e.Message);
				}

				str = textReader.ReadLine();
			}

			return pictureDraft;
		}
	}
}
