using Supermarket.Model.DataAccess.EntityDALs;
using Supermarket.ViewModel.DataVM;
using System;
using System.Collections.ObjectModel;

namespace Supermarket.Model.BusinessLogic
{
	internal static class ReceiptBL
	{
		public static ObservableCollection<ReceiptVM> GetReceiptsFiltered(int cashierID, DateTime startDate, DateTime endDate)
		{
			var receipts = new ObservableCollection<ReceiptVM>();
			foreach (var receipt in ReceiptDAL.GetReceiptsFiltered(cashierID, startDate, endDate))
			{
				receipts.Add(new ReceiptVM(receipt));
			}
			return receipts;
		}

		public static ReceiptVM GetLargestReceipt(DateTime date)
		{
			var receiptModel = ReceiptDAL.GetLargestReceipt(date);
			return receiptModel != null ? new ReceiptVM(receiptModel) : null;
		}
	}
}
