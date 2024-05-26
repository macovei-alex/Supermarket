using Supermarket.Model.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

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
