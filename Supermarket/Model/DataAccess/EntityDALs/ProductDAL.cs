using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Supermarket.Model.Entities;
using System.Data.SqlClient;
using System.Data;
using System.Runtime.InteropServices;
using System.Windows.Markup;
using Supermarket.Utilities;

namespace Supermarket.Model.DataAccess.EntityDALs
{
	internal static class ProductDAL
	{
		public static List<Product> GetAllProducts()
		{
			using (SqlConnection connection = DALHelper.NewConnection())
			using (SqlCommand command = new SqlCommand
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = nameof(GetAllProducts),
				Connection = connection
			})
			{
				connection.Open();
				using (SqlDataReader reader = command.ExecuteReader())
				{
					List<Product> products = new List<Product>();
					while (reader.Read())
					{
						products.Add(new Product(reader));
					}
					return products;
				}
			}
		}

		public static decimal GetProductsValueFiltered(int categoryID, int producerID)
		{
			using (SqlConnection connection = DALHelper.NewConnection())
			using (SqlCommand command = new SqlCommand
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = nameof(GetProductsValueFiltered),
				Connection = connection,
				Parameters =
				{
					new SqlParameter("@CategoryID", categoryID),
					new SqlParameter("@ProducerID", producerID)
				}
			})
			{
				connection.Open();
				object result = command.ExecuteScalar();
				return result != DBNull.Value ? (decimal)result : 0;
			}
		}

		public static string CreateProduct(string name, string barcode, int categoryID, int producerID)
		{
			using (SqlConnection connection = DALHelper.NewConnection())
			using (SqlCommand command = new SqlCommand
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = nameof(CreateProduct),
				Connection = connection,
				Parameters =
				{
					new SqlParameter("@Name", name),
					new SqlParameter("@Barcode", barcode),
					new SqlParameter("@CategoryID", categoryID),
					new SqlParameter("@producerID", producerID),
				}
			})
			{
				connection.Open();
				return Functions.SqlCallWrapper(() => command.ExecuteScalar());
			}
		}

		public static string EditProduct(int id, string name, string barcode, int categoryID, int producerID)
		{
			using (SqlConnection connection = DALHelper.NewConnection())
			using (SqlCommand command = new SqlCommand
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = nameof(EditProduct),
				Connection = connection,
				Parameters =
				{
					new SqlParameter("@ID", id),
					new SqlParameter("@Name", name),
					new SqlParameter("@Barcode", barcode),
					new SqlParameter("@CategoryID", categoryID),
					new SqlParameter("@ProducerID", producerID)
				}
			})
			{
				connection.Open();
				return Functions.SqlCallWrapper(() => command.ExecuteScalar());
			}
		}

		public static string DeleteProduct(int id)
		{
			using (SqlConnection connection = DALHelper.NewConnection())
			using (SqlCommand command = new SqlCommand
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = nameof(DeleteProduct),
				Connection = connection,
				Parameters =
				{
					new SqlParameter("@ID", id)
				}
			})
			{
				connection.Open();
				return Functions.SqlCallWrapper(() => command.ExecuteScalar());
			}
		}
	}
}
