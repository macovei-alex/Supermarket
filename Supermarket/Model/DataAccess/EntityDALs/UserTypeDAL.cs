using Supermarket.Model.Entities;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Supermarket.Model.DataAccess.EntityDALs
{
	internal static class UserTypeDAL
	{
		public static List<UserType> GetAllUserTypes()
		{
			using (SqlConnection connection = DALHelper.NewConnection())
			using (SqlCommand command = new SqlCommand
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = nameof(GetAllUserTypes),
				Connection = connection,
			})
			{
				List<UserType> types = new List<UserType>();
				connection.Open();
				using (SqlDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						types.Add(new UserType(reader.GetInt32(0), reader.GetString(1)));
					}
					return types;
				}
			}
		}
	}
}
