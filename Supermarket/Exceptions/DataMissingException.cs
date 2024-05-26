using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Exceptions
{
	internal class DataMissingException : Exception
	{
		public object Load { get; }

		public DataMissingException(string message, object load = null)
			: base(message)
		{
			Load = load;
		}
	}
}
