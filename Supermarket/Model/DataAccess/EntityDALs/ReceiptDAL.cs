using Supermarket.Model.Entities;
using Supermarket.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices.ComTypes;

namespace Supermarket.Model.DataAccess.EntityDALs
{
	internal static class ReceiptDAL
	{
		public static List<Receipt> GetReceiptsFiltered(int cashierID, DateTime startDate, DateTime endDate)
		{
			using (SqlConnection connection = DALHelper.NewConnection())
			using (SqlCommand command = new SqlCommand
			{
				Connection = connection,
				CommandType = CommandType.StoredProcedure,
				CommandText = nameof(GetReceiptsFiltered),
				Parameters =
				{
					new SqlParameter("@CashierID", cashierID),
					new SqlParameter("@StartDate", startDate),
					new SqlParameter("@EndDate", endDate)
				}
			})
			{
				connection.Open();
				using (SqlDataReader reader = command.ExecuteReader())
				{
					List<Receipt> receipts = new List<Receipt>();
					while (reader.Read())
					{
						receipts.Add(new Receipt(reader));
					}
					return receipts;
				}
			}
		}

		public static Receipt GetLargestReceipt(DateTime date)
		{
			using (SqlConnection connection = DALHelper.NewConnection())
			using (SqlCommand command = new SqlCommand
			{
				Connection = connection,
				CommandType = CommandType.StoredProcedure,
				CommandText = nameof(GetLargestReceipt),
				Parameters =
				{
					new SqlParameter("@SelectedDate", date)
				}
			})
			{
				connection.Open();
				using (SqlDataReader reader = command.ExecuteReader())
				{
					return reader.Read() ? new Receipt(reader) : null;
				}
			}
		}

		public static string CreateReceipt(DateTime issueDate, int cashierID, decimal totalPrice)
		{
			using (SqlConnection connection = DALHelper.NewConnection())
			using (SqlCommand command = new SqlCommand
			{
				Connection = connection,
				CommandType = CommandType.StoredProcedure,
				CommandText = nameof(CreateReceipt),
				Parameters =
				{
					new SqlParameter("@IssueDate", issueDate),
					new SqlParameter("@CashierID", cashierID),
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
