using Supermarket.Model.Entities;
using Supermarket.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Model.DataAccess.EntityDALs
{
	internal static class CategoryDAL
	{
		public static List<Category> GetAllCategories()
		{
			using (SqlConnection connection = DALHelper.NewConnection())
			using (SqlCommand command = new SqlCommand
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = nameof(GetAllCategories),
				Connection = connection,
			})
			{
				List<Category> categories = new List<Category>();
				connection.Open();
				using (SqlDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						categories.Add(new Category(reader.GetInt32(0), reader.GetString(1)));
					}
					return categories;
				}
			}
		}

		public static string CreateCategory(string name)
		{
			using (SqlConnection connection = DALHelper.NewConnection())
			using (SqlCommand command = new SqlCommand
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = nameof(CreateCategory),
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

		public static string EditCategory(int id, string newName)
		{
			using (SqlConnection connection = DALHelper.NewConnection())
			using (SqlCommand command = new SqlCommand
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = nameof(EditCategory),
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

		public static string DeleteCategory(int id)
		{
			using (SqlConnection connection = DALHelper.NewConnection())
			using (SqlCommand command = new SqlCommand
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = nameof(DeleteCategory),
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
