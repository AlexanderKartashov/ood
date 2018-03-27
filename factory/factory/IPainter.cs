using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace painter
{
	public interface IPainter
	{
		void DrawPicture(PictureDraft draft, ICanvas canvas);
	}
}
