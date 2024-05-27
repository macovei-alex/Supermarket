using System;
using Supermarket.Model.Entities;
using Supermarket.Model;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Supermarket.ViewModel.DataVM
{
	internal class ReceiptVM
	{
		public int ID { get; set; }
		public DateTime IssueDate { get; set; }
		public MarketUserVM Cashier { get; set; }
		public decimal TotalPrice { get; set; }
		public ObservableCollection<ReceiptItemVM> Items { get; set; }

		public ReceiptVM()
		{
			ID = 0;
			IssueDate = DateTime.Now;
			Cashier = new MarketUserVM();
			TotalPrice = 0;
			Items = new ObservableCollection<ReceiptItemVM>();
		}

		public ReceiptVM(int id, DateTime issueDate, MarketUserVM cashier, decimal totalPrice, ObservableCollection<ReceiptItemVM> items)
		{
			ID = id;
			IssueDate = issueDate;
			Cashier = cashier;
			TotalPrice = totalPrice;
			Items = items;
		}

		public ReceiptVM(Receipt receipt, List<ReceiptItem> items)
		{
			ID = receipt.ID;
			IssueDate = receipt.IssueDate;
			Cashier = new MarketUserVM(Cache.Instance.Users.Find((u) => u.ID == receipt.CashierID));
			TotalPrice = receipt.TotalPrice;
			Items = new ObservableCollection<ReceiptItemVM>();
			foreach (var item in items)
			{
				Items.Add(new ReceiptItemVM(item));
			}
		}

		public override string ToString()
		{
			StringBuilder items = new StringBuilder();
			foreach (var item in Items)
			{
				items.Append(item.ToString()).Append(", ");
			}
			items.Remove(items.Length - 2, 2);
			return $"{nameof(ReceiptVM)}{{ ID: {ID}, IssueDate: {IssueDate}, Cashier: {Cashier}, TotalPrice: {TotalPrice}, Items: {items} }}";
		}
	}
}
