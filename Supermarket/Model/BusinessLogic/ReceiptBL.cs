using Supermarket.Model.DataAccess.EntityDALs;
using Supermarket.Utilities;
using Supermarket.ViewModel.DataVM;
using System;
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

		public static bool CreateReceipt(ReceiptVM receipt)
		{
			if (receipt == null)
			{
				Functions.LogError("The receipt cannot be null");
				return false;
			}

			using (TransactionScope transaction = new TransactionScope())
			{
				if (ReceiptDAL.CreateReceipt(receipt.IssueDate, receipt.Cashier.ID, receipt.TotalPrice) != Cache.Instance.SuccessMessage)
				{
					Functions.LogError("The receipt could not be created");
					return false;
				}

				foreach (var item in receipt.Items)
				{
					if (ReceiptItemDAL.CreateReceiptItem(item.ID, item.Product.ID, item.Quantity, item.TotalPrice) != Cache.Instance.SuccessMessage)
					{
						Functions.LogError("The receipt item could not be created");
						return false;
					}
				}

				transaction.Complete();
			}

			return true;
		}
	}
}
