using Supermarket.Model.Entities;
using Supermarket.Utilities;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Supermarket.Model.DataAccess.EntityDALs
{
	internal static class ProducerDAL
	{
		public static List<Producer> GetAllProducers()
		{
			using (SqlConnection connection = DALHelper.NewConnection())
			using (SqlCommand command = new SqlCommand
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = nameof(GetAllProducers),
				Connection = connection
			})
			{
				connection.Open();
				using (SqlDataReader reader = command.ExecuteReader())
				{
					List<Producer> products = new List<Producer>();
					while (reader.Read())
					{
						products.Add(new Producer(reader));
					}
					return products;
				}
			}
		}

		public static List<Producer> GetActiveProducers()
		{
			using (SqlConnection connection = DALHelper.NewConnection())
			using (SqlCommand command = new SqlCommand
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = nameof(GetActiveProducers),
				Connection = connection
			})
			{
				connection.Open();
				using (SqlDataReader reader = command.ExecuteReader())
				{
					List<Producer> products = new List<Producer>();
					while (reader.Read())
					{
						products.Add(new Producer(reader));
					}
					return products;
				}
			}
		}

		public static string CreateProducer(string name, int countryID)
		{
			using (SqlConnection connection = DALHelper.NewConnection())
			using (SqlCommand command = new SqlCommand
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = nameof(CreateProducer),
				Connection = connection,
				Parameters =
				{
					new SqlParameter("@Name", name),
					new SqlParameter("@CountryID", countryID)
				},
			})
			{
				connection.Open();
				return Functions.SqlCallWrapper(() => command.ExecuteScalar());
			}
		}

		public static string EditProducer(int id, string name, int countryID)
		{
			using (SqlConnection connection = DALHelper.NewConnection())
			using (SqlCommand command = new SqlCommand
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = nameof(EditProducer),
				Connection = connection,
				Parameters =
				{
					new SqlParameter("@ID", id),
					new SqlParameter("@Name", name),
					new SqlParameter("@CountryID", countryID)
				},
			})
			{
				connection.Open();
				return Functions.SqlCallWrapper(() => command.ExecuteScalar());
			}
		}

		public static string DeleteProducer(int id)
		{
			using (SqlConnection connection = DALHelper.NewConnection())
			using (SqlCommand command = new SqlCommand
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = nameof(DeleteProducer),
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
