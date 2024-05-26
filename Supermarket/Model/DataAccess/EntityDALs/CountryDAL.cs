using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Supermarket.Model.Entities;
using Supermarket.Utilities;

namespace Supermarket.Model.DataAccess.EntityDALs
{
	internal static class CountryDAL
	{
		public static List<Country> GetAllCountries()
		{
			using (SqlConnection connection = DALHelper.NewConnection())
			using (SqlCommand command = new SqlCommand
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = nameof(GetAllCountries),
				Connection = connection,
			})
			{
				List<Country> countries = new List<Country>();
				connection.Open();
				using (SqlDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						countries.Add(new Country(reader.GetInt32(0), reader.GetString(1)));
					}
					return countries;
				}
			}
		}

		public static string CreateCountry(string name)
		{
			using (SqlConnection connection = DALHelper.NewConnection())
			using (SqlCommand command = new SqlCommand
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = nameof(CreateCountry),
				Connection = connection,
				Parameters =
				{
					new SqlParameter("@Name", name)
				},
			})
			{
				connection.Open();
				return Functions.SqlCallWrapper(() => command.ExecuteScalar());
			}
		}

		public static string EditCountry(int id, string newName)
		{
			using (SqlConnection connection = DALHelper.NewConnection())
			using (SqlCommand command = new SqlCommand
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = nameof(EditCountry),
				Connection = connection,
				Parameters =
				{
					new SqlParameter("@ID", id),
					new SqlParameter("@Name", newName),
				},
			})
			{
				connection.Open();
				return Functions.SqlCallWrapper(() => command.ExecuteScalar());
			}
		}

		public static string DeleteCountry(int id)
		{
			using (SqlConnection connection = DALHelper.NewConnection())
			using (SqlCommand command = new SqlCommand
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = nameof(DeleteCountry),
				Connection = connection,
				Parameters =
				{
					new SqlParameter("@ID", id),
				},
			})
			{
				connection.Open();
				return Functions.SqlCallWrapper(() => command.ExecuteScalar());
			}
		}
	}
}
