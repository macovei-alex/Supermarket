using Supermarket.Model.BusinessLogic;
using Supermarket.ViewModel.DataVM;
using System;
using System.Collections.ObjectModel;

namespace Supermarket.ViewModel.MainWindowVMs
{
	internal class EmployeeStatisticsPageVM : BaseVM
	{
		#region Properties

		private ObservableCollection<MarketUserVM> _employees;
		public ObservableCollection<MarketUserVM> Employees
		{
			get => _employees;
			set
			{
				_employees = value;
				OnPropertyChanged(nameof(Employees));
				if (_doUpdateStatistics)
				{
					_doUpdateStatistics = false;
					UpdateStatistics();
					_doUpdateStatistics = true;
				}
			}
		}

		private MarketUserVM _selectedEmployeeCopy;
		public MarketUserVM SelectedEmployeeCopy
		{
			get => _selectedEmployeeCopy;
			set
			{
				_selectedEmployeeCopy = value != null ? new MarketUserVM(value) : null;
				OnPropertyChanged(nameof(SelectedEmployeeCopy));
				if (_doUpdateStatistics)
				{
					_doUpdateStatistics = false;
					UpdateStatistics();
					_doUpdateStatistics = true;
				}
			}
		}

		private DateTime _startDate;
		public DateTime StartDate
		{
			get => _startDate;
			set
			{
				_startDate = value;
				OnPropertyChanged(nameof(StartDate));
				if (_doUpdateStatistics)
				{
					_doUpdateStatistics = false;
					UpdateStatistics();
					_doUpdateStatistics = true;
				}
			}
		}

		private DateTime _endDate;
		public DateTime EndDate
		{
			get => _endDate;
			set
			{
				_endDate = value;
				OnPropertyChanged(nameof(EndDate));
				if (_doUpdateStatistics)
				{
					_doUpdateStatistics = false;
					UpdateStatistics();
					_doUpdateStatistics = true;
				}
			}
		}

		private ReceiptVM _largestReceipt;
		public ReceiptVM LargestReceipt
		{
			get => _largestReceipt;
			set
			{
				_largestReceipt = value;
				LargestReceiptValue = value?.TotalPrice ?? 0;
				OnPropertyChanged(nameof(LargestReceipt));
				if (_doUpdateStatistics)
				{
					_doUpdateStatistics = false;
					UpdateStatistics();
					_doUpdateStatistics = true;
				}
			}
		}

		private decimal _largestReceiptValue;
		public decimal LargestReceiptValue
		{
			get => _largestReceiptValue;
			set
			{
				_largestReceiptValue = value;
				OnPropertyChanged(nameof(LargestReceiptValue));
			}
		}

		private decimal _totalEarnings;
		public decimal TotalEarnings
		{
			get => _totalEarnings;
			set
			{
				_totalEarnings = value;
				OnPropertyChanged(nameof(TotalEarnings));
			}
		}

		private decimal _dailyEarnings;
		public decimal DailyEarnings
		{
			get => _dailyEarnings;
			set
			{
				_dailyEarnings = value;
				OnPropertyChanged(nameof(DailyEarnings));
			}
		}

		private bool _doUpdateStatistics;

		#endregion

		public EmployeeStatisticsPageVM()
		{
			Employees = MarketUserBL.GetActiveUsers();
			SelectedEmployeeCopy = null;
			StartDate = DateTime.Today;
			EndDate = DateTime.Today + TimeSpan.FromDays(1);
			LargestReceipt = null;

			_doUpdateStatistics = true;
		}

		private void UpdateStatistics()
		{
			LargestReceipt = ReceiptBL.GetLargestReceipt(StartDate);

			if (SelectedEmployeeCopy == null)
			{
				return;
			}

			if (StartDate > EndDate)
			{
				return;
			}

			var receipts = ReceiptBL.GetReceiptsFiltered(SelectedEmployeeCopy.ID, StartDate, EndDate);
			TotalEarnings = 0;
			foreach (var receipt in receipts)
			{
				TotalEarnings += receipt.TotalPrice;
			}

			DailyEarnings = TotalEarnings / Math.Max((EndDate - StartDate).Days, 1);
			DailyEarnings = Math.Round(DailyEarnings, 2);
		}
	}
}
