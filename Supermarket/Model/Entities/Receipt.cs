using System;
using System.Data.SqlClient;

namespace Supermarket.Model.Entities
{
	internal class Receipt
	{
		public int ID { get; set; }
		public DateTime IssueDate { get; set; }
		public int CashierID { get; set; }
		public decimal TotalPrice { get; set; }

		public Receipt()
		{
			ID = 0;
			IssueDate = DateTime.Now;
			CashierID = 0;
			TotalPrice = 0;
		}

		public Receipt(int id, DateTime issueDate, int cashierID, decimal totalPrice)
		{
			ID = id;
			IssueDate = issueDate;
			CashierID = cashierID;
			TotalPrice = totalPrice;
		}

		public Receipt(SqlDataReader reader)
		{
			ID = reader.GetInt32(0);
			IssueDate = reader.GetDateTime(1);
			CashierID = reader.GetInt32(2);
			TotalPrice = reader.GetDecimal(3);
		}

		public override string ToString()
		{
			return $"{nameof(Receipt)}{{ ID: {ID}, IssueDate: {IssueDate}, CashierID: {CashierID}, TotalPrice: {TotalPrice} }}";
		}
	}
}
