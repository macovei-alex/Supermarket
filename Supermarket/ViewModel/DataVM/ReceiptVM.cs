using System.Data.SqlClient;
using System;
using Supermarket.Model.Entities;
using Supermarket.Model;

namespace Supermarket.ViewModel.DataVM
{
	internal class ReceiptVM
	{
		public int ID { get; set; }
		public DateTime IssueDate { get; set; }
		public MarketUserVM Cashier { get; set; }
		public decimal TotalPrice { get; set; }

		public ReceiptVM()
		{
			ID = 0;
			IssueDate = DateTime.Now;
			Cashier = null;
			TotalPrice = 0;
		}

		public ReceiptVM(int id, DateTime issueDate, MarketUserVM cashier, decimal totalPrice)
		{
			ID = id;
			IssueDate = issueDate;
			Cashier = cashier;
			TotalPrice = totalPrice;
		}

		public ReceiptVM(Receipt receipt)
		{
			ID = receipt.ID;
			IssueDate = receipt.IssueDate;
			Cashier = new MarketUserVM(Cache.Instance.Users.Find((u) => u.ID == receipt.CashierID));
			TotalPrice = receipt.TotalPrice;
		}

		public override string ToString()
		{
			return $"{nameof(ReceiptVM)}{{ ID: {ID}, IssueDate: {IssueDate}, Cashier: {Cashier}, TotalPrice: {TotalPrice} }}";
		}
	}
}
