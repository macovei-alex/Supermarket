using Supermarket.Model.BusinessLogic;
using Supermarket.View.MainWindow;
using System;
using System.Windows.Controls;

namespace Supermarket.ViewModel.MainWindowVMs
{
	public class MainWindowVM : BaseVM
	{
		#region Properties

		private Page _currentPage;
		public Page CurrentPage
		{
			get => _currentPage;
			set
			{
				_currentPage = value;
				OnPropertyChanged(nameof(CurrentPage));
			}
		}

		#endregion

		public MainWindowVM()
		{
			ChangePage(nameof(LoginPage));
			StocksBL.InvalidateExpiredStocks();
		}

		public void ChangePage(string pageType)
		{
			switch (pageType)
			{
				case nameof(LoginPage):
					CurrentPage = new LoginPage();
					break;

				case nameof(MainMenuPage):
					CurrentPage = new MainMenuPage();
					break;

				case nameof(UsersPage):
					CurrentPage = new UsersPage();
					break;

				case nameof(StocksPage):
					CurrentPage = new StocksPage();
					break;

				case nameof(ProductsPage):
					CurrentPage = new ProductsPage();
					break;

				case nameof(EmployeeStatisticsPage):
					CurrentPage = new EmployeeStatisticsPage();
					break;

				case nameof(PurchasePage):
					CurrentPage = new PurchasePage();
					break;

				case nameof(ViewReceiptsPage):
					CurrentPage = new ViewReceiptsPage();
					break;

				default:
					Console.WriteLine($"Page {pageType} not found.");
					break;
			}
		}
	}
}
