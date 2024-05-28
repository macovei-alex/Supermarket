using Supermarket.Model.Entities;
using Supermarket.Utilities;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Supermarket.Model.DataAccess.EntityDALs
{
	internal static class StockDAL
	{
		public static List<Stock> GetAllStocks()
		{
			using (SqlConnection connection = DALHelper.NewConnection())
			using (SqlCommand command = new SqlCommand
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = nameof(GetAllStocks),
				Connection = connection,
			})
			{
				connection.Open();
				using (SqlDataReader reader = command.ExecuteReader())
				{
					List<Stock> stocks = new List<Stock>();
					while (reader.Read())
					{
						stocks.Add(new Stock(reader));
					}
					return stocks;
				}
			}
		}

		public static string AddNewStock(Stock stock)
		{
			using (SqlConnection connection = DALHelper.NewConnection())
			using (SqlCommand command = new SqlCommand
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = nameof(AddNewStock),
				Connection = connection,
				Parameters =
				{
					new SqlParameter("@ProductID", stock.ProductID),
					new SqlParameter("@Quantity", stock.Quantity),
					new SqlParameter("@Unit", stock.Unit),
					new SqlParameter("@SupplyDate", stock.SupplyDate),
					new SqlParameter("@ExpirationDate", stock.ExpirationDate),
					new SqlParameter("@PurchasePrice", stock.PurchasePrice),
					new SqlParameter("@SellingPrice", stock.SellingPrice),
				}
			})
			{
				connection.Open();
				return Functions.SqlCallWrapper(() => command.ExecuteScalar());
			}
		}

		public static string DeleteStock(int id)
		{
			using (SqlConnection connection = DALHelper.NewConnection())
			using (SqlCommand command = new SqlCommand
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = nameof(DeleteStock),
				Connection = connection,
				Parameters =
				{
					new SqlParameter("@ID", id)
				}
			})
			{
				connection.Open();
				return command.ExecuteScalar() as string;
			}
		}

		public static string EditStock(Stock stock)
		{
			using (SqlConnection connection = DALHelper.NewConnection())
			using (SqlCommand command = new SqlCommand
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = nameof(EditStock),
				Connection = connection,
				Parameters =
				{
					new SqlParameter("@ID", stock.ID),
					new SqlParameter("@ProductID", stock.ProductID),
					new SqlParameter("@Quantity", stock.Quantity),
					new SqlParameter("@InitialQuantity", stock.InitialQuantity),
					new SqlParameter("@Unit", stock.Unit),
					new SqlParameter("@SupplyDate", stock.SupplyDate),
					new SqlParameter("@ExpirationDate", stock.ExpirationDate),
					new SqlParameter("@PurchasePrice", stock.PurchasePrice),
					new SqlParameter("@SellingPrice", stock.SellingPrice),
				}
			})
			{
				connection.Open();
				return Functions.SqlCallWrapper(() => command.ExecuteScalar());
			}
		}

		public static string InvalidateExpiredStocks()
		{
			using (SqlConnection connection = DALHelper.NewConnection())
			using (SqlCommand command = new SqlCommand
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = nameof(InvalidateExpiredStocks),
				Connection = connection,
			})
			{
				connection.Open();
				return Functions.SqlCallWrapper(() => command.ExecuteScalar());
			}
		}

		public static string InvalidateEmptyStocks()
		{
			using (SqlConnection connection = DALHelper.NewConnection())
			using (SqlCommand command = new SqlCommand
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = nameof(InvalidateEmptyStocks),
				Connection = connection,
			})
			{
				connection.Open();
				return Functions.SqlCallWrapper(() => command.ExecuteScalar());
			}
		}
	}
}
