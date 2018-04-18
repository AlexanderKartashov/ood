using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace command.externals
{
	public interface IHtmlEncoder
	{
		string Encode(string value);
	}
}
