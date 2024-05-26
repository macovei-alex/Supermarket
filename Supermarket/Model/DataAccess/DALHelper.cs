using System.Configuration;
using System.Data.SqlClient;

namespace Supermarket.Model.DataAccess
{
	internal static class DALHelper
	{
		private static string DBConnectionString { get; } = ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString;

		public static SqlConnection NewConnection()
		{
			return new SqlConnection(DBConnectionString);
		}
	}
}
