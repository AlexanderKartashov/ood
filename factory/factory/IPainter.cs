using painter.sdk;

namespace painter
{
	public interface IPainter
	{
		void DrawPicture(PictureDraft draft, ICanvas canvas);
	}
}
