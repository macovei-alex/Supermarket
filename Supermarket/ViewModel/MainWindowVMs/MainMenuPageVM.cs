using Supermarket.View.MainWindow;
using Supermarket.ViewModel.Commands;
using System.Windows.Input;

namespace Supermarket.ViewModel.MainWindowVMs
{
	internal class MainMenuPageVM : BaseVM
	{
		public ICommand UsersCommand { get; }
		public ICommand ProductsCommand { get; }
		public ICommand StocksCommand { get; }
		public ICommand EmployeeStatisticsCommand { get; }

		public MainMenuPageVM()
		{
			UsersCommand = new RelayCommandVoid(() => LocalStorage.WindowVM.ChangePage(nameof(UsersPage)));
			ProductsCommand = new RelayCommandVoid(() => LocalStorage.WindowVM.ChangePage(nameof(ProductsPage)));
			StocksCommand = new RelayCommandVoid(() => LocalStorage.WindowVM.ChangePage(nameof(StocksPage)));
			EmployeeStatisticsCommand = new RelayCommandVoid(
				() => LocalStorage.WindowVM.ChangePage(nameof(EmployeeStatisticsPage)),
				() => LocalStorage.CurrentUser.IsAdmin);
		}
	}
}
