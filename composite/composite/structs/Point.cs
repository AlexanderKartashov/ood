namespace composite
{
	public struct Point<T>
	{
		public Point(T x, T y)
		{
			X = x;
			Y = y;
		}

		public T X { get; private set; }
		public T Y { get; private set; }
	}


}
