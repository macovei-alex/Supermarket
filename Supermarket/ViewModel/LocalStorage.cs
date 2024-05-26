using Supermarket.ViewModel.DataVM;
using Supermarket.ViewModel.MainWindowVMs;

namespace Supermarket.ViewModel
{
	internal static class LocalStorage
	{
		public static MainWindowVM WindowVM { get; set; }
		public static MarketUserVM CurrentUser { get; set; }
	}
}
