﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace painter.sdk
{
	public interface ICanvasFactory
	{
		ICanvas CreateCanvas(int w, int h);
	}
}
