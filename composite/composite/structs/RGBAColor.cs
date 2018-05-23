namespace composite
{
	public struct RGBAColor
	{
		public RGBAColor(uint value)
		{
			R = (byte)(value >> 24 & 0xFF);
			G = (byte)(value >> 16 & 0xFF);
			B = (byte)(value >> 8 & 0xFF);
			A = (byte)(value & 0xFF);
		}

		public RGBAColor(byte r, byte g, byte b, byte a)
		{
			R = r;
			G = g;
			B = b;
			A = a;
		}

		public RGBAColor Clone()
		{
			return new RGBAColor(R, G, B, A);
		}

		public int ToRGBA()
		{
			int result =
				(R << 24) |
				(G << 16) |
				(B << 8) |
				(A);
			return result;
		}

		public byte R { get; private set; }
		public byte G { get; private set; }
		public byte B { get; private set; }
		public byte A { get; private set; }
	}
}
