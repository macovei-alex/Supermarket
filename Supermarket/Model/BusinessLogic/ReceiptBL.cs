using Supermarket.Model.DataAccess.EntityDALs;
using Supermarket.Utilities;
using Supermarket.ViewModel.DataVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Transactions;

namespace Supermarket.Model.BusinessLogic
{
	internal static class ReceiptBL
	{
		public static ObservableCollection<ReceiptVM> GetReceiptsFiltered(int cashierID, DateTime startDate, DateTime endDate)
		{
			var receipts = new ObservableCollection<ReceiptVM>();
			foreach (var receipt in ReceiptDAL.GetReceiptsFiltered(cashierID, startDate, endDate))
			{
				var items = ReceiptItemDAL.GetReceiptItems(receipt.ID);
				receipts.Add(new ReceiptVM(receipt, items));
			}
			return receipts;
		}

		public static ReceiptVM GetLargestReceipt(DateTime date)
		{
			var receiptModel = ReceiptDAL.GetLargestReceipt(date);
			if (receiptModel != null)
			{
				var items = ReceiptItemDAL.GetReceiptItems(receiptModel.ID);
				return new ReceiptVM(receiptModel, items);
			}
			return null;
		}

		public static ObservableCollection<ReceiptVM> GetAllReceipts()
		{
			var receipts = new ObservableCollection<ReceiptVM>();
			foreach (var receipt in ReceiptDAL.GetAllReceipts())
			{
				var items = ReceiptItemDAL.GetReceiptItems(receipt.ID);
				receipts.Add(new ReceiptVM(receipt, items));
			}
			return receipts;
		}

		public static bool CreateReceipt(ReceiptVM receipt)
		{
			if (receipt == null)
			{
				Functions.LogError("The receipt cannot be null");
				return false;
			}

			if (ReceiptDAL.CreateReceipt(receipt.IssueDate, receipt.Cashier.ID, receipt.TotalPrice) != Cache.Instance.SuccessMessage)
			{
				Functions.LogError("The receipt could not be created");
				return false;
			}

			using (TransactionScope transaction = new TransactionScope())
			{
				var receiptModel = ReceiptDAL.GetLastReceipt();
				foreach (var item in receipt.Items)
				{
					if (ReceiptItemDAL.CreateReceiptItem(receiptModel.ID, item.Product.ID, item.Quantity, item.TotalPrice) != Cache.Instance.SuccessMessage)
					{
						Functions.LogError("The receipt item could not be created");
						return false;
					}
				}

				List<StockVM> stocks = GetAffectedStocks(receipt);
				List<StockVM> originalStocks = new List<StockVM>(StocksBL.GetAllStocks());

				for (int i = 0; i < stocks.Count; i++)
				{
					if (stocks[i].Quantity != originalStocks[i].Quantity)
					{
						if (!StocksBL.EditStock(stocks[i]))
						{
							Functions.LogError("The stock could not be updated");
							return false;
						}
					}
				}

				StocksBL.DeactivateExpiredStocks();

				transaction.Complete();
			}

			return true;
		}

		private static List<StockVM> GetAffectedStocks(ReceiptVM receipt)
		{
			List<StockVM> stocks = new List<StockVM>(StocksBL.GetAllStocks());

			foreach (ReceiptItemVM receiptItem in receipt.Items)
			{
				decimal subtotal = 0;
				for (int quantityObtained = 0; quantityObtained < receiptItem.Quantity; /* empty */)
				{
					var stock = stocks.Find((s) => s.Product.Name == receiptItem.Product.Name && s.Quantity > 0);
					int quantity = Math.Min(receiptItem.Quantity - quantityObtained, stock.Quantity);
					quantityObtained += quantity;
					stock.Quantity -= quantity;
					subtotal += quantity * stock.SellingPrice;
				}

			}

			return stocks;
		}
	}
}
