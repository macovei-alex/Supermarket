using Supermarket.Model.BusinessLogic;
using Supermarket.ViewModel.DataVM;
using System.Collections.ObjectModel;

namespace Supermarket.ViewModel.MainWindowVMs
{
	internal class ViewReceiptsPageVM : BaseVM
	{
		private ObservableCollection<ReceiptVM> _receipts;
		public ObservableCollection<ReceiptVM> Receipts
		{
			get => _receipts;
			set
			{
				_receipts = value;
				OnPropertyChanged(nameof(Receipts));
			}
		}

		private ReceiptVM _selectedReceipt;
		public ReceiptVM SelectedReceipt
		{
			get => _selectedReceipt;
			set
			{
				_selectedReceipt = value;
				OnPropertyChanged(nameof(SelectedReceipt));
			}
		}

		public ViewReceiptsPageVM()
		{
			Receipts = ReceiptBL.GetAllReceipts();
		}
	}
}
