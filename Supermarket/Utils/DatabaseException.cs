using System;

namespace Supermarket.Utils
{
	internal class DatabaseException : ApplicationException
	{
		public DatabaseException() : base() { }

		public DatabaseException(string message) : base(message) { }
	}
}
