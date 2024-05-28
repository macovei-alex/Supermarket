using Supermarket.Model.Entities;
using Supermarket.Utilities;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Supermarket.Model.DataAccess.EntityDALs
{
	internal static class ReceiptItemDAL
	{
		public static List<ReceiptItem> GetReceiptItems(int receiptID)
		{
			using (SqlConnection connection = DALHelper.NewConnection())
			using (SqlCommand command = new SqlCommand
			{
				Connection = connection,
				CommandType = CommandType.StoredProcedure,
				CommandText = nameof(GetReceiptItems),
				Parameters =
				{
					new SqlParameter("@ReceiptID", receiptID)
				}
			})
			{
				connection.Open();
				using (SqlDataReader reader = command.ExecuteReader())
				{
					List<ReceiptItem> items = new List<ReceiptItem>();
					while (reader.Read())
					{
						items.Add(new ReceiptItem(reader));
					}
					return items;
				}
			}
		}

		public static string CreateReceiptItem(int receiptID, int productID, int quantity, decimal totalPrice)
		{
			using (SqlConnection connection = DALHelper.NewConnection())
			using (SqlCommand command = new SqlCommand
			{
				Connection = connection,
				CommandType = CommandType.StoredProcedure,
				CommandText = nameof(CreateReceiptItem),
				Parameters =
				{
					new SqlParameter("@ReceiptID", receiptID),
					new SqlParameter("@ProductID", productID),
					new SqlParameter("@Quantity", quantity),
					new SqlParameter("@TotalPrice", totalPrice)
				}
			})
			{
				connection.Open();
				return Functions.SqlCallWrapper(() => command.ExecuteScalar());
			}
		}
	}
}
