using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace painter
{
	public class Designer
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

			string str = "";
			do
			{
				try
				{
					str = textReader.ReadLine();
					pictureDraft.AddShape(_shapeFactory.CreateShape(str));
				}
				catch (Exception e)
				{
					errorReporter?.WriteLine(e.Message);
				}
			}
			while (str.Length != 0);

			return pictureDraft;
		}
	}
}
