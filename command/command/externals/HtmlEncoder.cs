using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace command.externals
{
	public class HtmlEncoder : IHtmlEncoder
	{
		public string Encode(string value)
		{
			return System.Web.HttpUtility.HtmlEncode(value);
		}
	}
}
