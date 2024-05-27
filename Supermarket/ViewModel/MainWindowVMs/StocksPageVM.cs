using Supermarket.Model;
using Supermarket.Model.BusinessLogic;
using Supermarket.Utilities;
using Supermarket.Utils;
using Supermarket.ViewModel.Commands;
using Supermarket.ViewModel.DataVM;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System;

namespace Supermarket.ViewModel.MainWindowVMs
{
	internal class StocksPageVM : BaseVM
	{

		#region Properties

		public ObservableCollection<StockVM> Stocks { get; set; }

		private StockVM _selectedStock;
		public StockVM SelectedStock
		{
			get => _selectedStock;
			set
			{
				_selectedStock = value;
				OnPropertyChanged(nameof(SelectedStock));
				SelectedStockCopy = StockVM.DeepCopyOrNew(value);
				PurchasePriceString = SelectedStockCopy.PurchasePrice.ToString();
				SellingPriceString = SelectedStockCopy.SellingPrice.ToString();
			}
		}

		private StockVM _selectedStockCopy;
		public StockVM SelectedStockCopy
		{
			get => _selectedStockCopy;
			set
			{
				_selectedStockCopy = value;
				OnPropertyChanged(nameof(SelectedStockCopy));
			}
		}

		private string _purchasePriceString;
		public string PurchasePriceString
		{
			get => _purchasePriceString;
			set
			{
				_purchasePriceString = value;
				try
				{
					SellingPriceString = (decimal.Parse(value) * (1 + Cache.Instance.VAT)).ToString();
				}
				catch (FormatException)
				{
					SellingPriceString = string.Empty;
				}
				OnPropertyChanged(nameof(PurchasePriceString));
			}
		}


		private string _sellingPriceString;
		public string SellingPriceString
		{
			get => _sellingPriceString;
			set
			{
				_sellingPriceString = value;
				OnPropertyChanged(nameof(SellingPriceString));
			}
		}

		#endregion

		public ICommand AddCommand { get; }
		public ICommand EditCommand { get; }
		public ICommand DeleteCommand { get; }

		public StocksPageVM()
		{
			Stocks = StocksBL.GetAllStocks();
			SelectedStockCopy = new StockVM();

			AddCommand = new RelayCommandVoid(Add, () => LocalStorage.CurrentUser.IsAdmin);
			EditCommand = new RelayCommandVoid(Edit, () => LocalStorage.CurrentUser.IsAdmin);
			DeleteCommand = new RelayCommandVoid(Delete, () => LocalStorage.CurrentUser.IsAdmin);
		}

		private bool CommonChecks()
		{
			if (SelectedStockCopy == null)
			{
				Functions.LogError("Please first select the stock you want to modify from the table below");
				return false;
			}

			if (!Functions.AreNotNullOrEmpty(PurchasePriceString, SellingPriceString))
			{
				Functions.LogError("Please enter the prices");
				return false;
			}

			if (!Functions.AreNotNullOrEmpty(SelectedStockCopy.Product?.Name))
			{
				Functions.LogError("Please enter a product name first");
				return false;
			}

			if (!Functions.AreNotNullOrEmpty(SelectedStockCopy.Unit))
			{
				Functions.LogError("Please enter a unit of measure first");
				return false;
			}

			var product = Cache.Instance.Products.Find((p) => p.Name == SelectedStockCopy.Product.Name);
			if (product == null)
			{
				Functions.LogError($"There is no product with name ( {SelectedStockCopy.Product.ID} )");
				return false;
			}

			return true;
		}

		private void Add()
		{
			if (!CommonChecks())
			{
				return;
			}

			try
			{
				SelectedStockCopy.PurchasePrice = decimal.Parse(PurchasePriceString);
				SelectedStockCopy.SellingPrice = decimal.Parse(SellingPriceString);
			}
			catch (FormatException)
			{
				Functions.LogError("Please enter a valid number for the prices");
				return;
			}

			if (SelectedStockCopy.PurchasePrice > SelectedStockCopy.SellingPrice)
			{
				Functions.LogError("Selling price cannot be less than purchase price");
				return;
			}

			if (SelectedStockCopy.InitialQuantity <= 0)
			{
				Functions.LogError("Initial Quantity cannot be null or negative");
				return;
			}

			if (SelectedStockCopy.ExpirationDate < DateTime.Now)
			{
				Functions.LogError("Expiration date cannot be in the past");
				return;
			}

			if (SelectedStockCopy.ExpirationDate < SelectedStockCopy.SupplyDate)
			{
				Functions.LogError("Expiration date cannot be before supply date");
				return;
			}

			SelectedStockCopy.Product = new ProductVM(Cache.Instance.Products.Find((p) => p.Name == SelectedStockCopy.Product.Name));
			if (StocksBL.CreateStock(SelectedStockCopy))
			{
				Functions.LogError("Stock created successfully");
				Stocks.RepopulateFrom(StocksBL.GetAllStocks());
			}
		}

		private void Edit()
		{
			if (SelectedStockCopy.ID <= 0)
			{
				Functions.LogError("Please select the stock you want to edit first");
			}

			if (!CommonChecks())
			{
				return;
			}

			if (SelectedStock == null)
			{
				Functions.LogError("Please first select the stock you want to edit");
				return;
			}

			if (!Functions.AreNotNullOrEmpty(SelectedStockCopy.Product?.Name))
			{
				Functions.LogError("Please enter a product name first");
				return;
			}

			try
			{
				SelectedStockCopy.PurchasePrice = decimal.Parse(PurchasePriceString);
				SelectedStockCopy.SellingPrice = decimal.Parse(SellingPriceString);
			}
			catch (FormatException)
			{
				Functions.LogError("Please enter a valid number for the prices");
				return;
			}

			if (SelectedStock.PurchasePrice != SelectedStockCopy.PurchasePrice)
			{
				Functions.LogError("You cannot change the purchase price of a stock");
				return;
			}

			if (SelectedStockCopy.PurchasePrice > SelectedStockCopy.SellingPrice)
			{
				Functions.LogError("Selling price cannot be less than purchase price");
				return;
			}

			if (SelectedStockCopy.Quantity <= 0 || SelectedStockCopy.InitialQuantity <= 0)
			{
				Functions.LogError("Quantity and Initial Quantity cannot be null negative");
				return;
			}

			if (SelectedStockCopy.ExpirationDate < DateTime.Now)
			{
				Functions.LogError("Expiration date cannot be in the past");
				return;
			}

			if (SelectedStockCopy.ExpirationDate < SelectedStockCopy.SupplyDate)
			{
				Functions.LogError("Expiration date cannot be before supply date");
				return;
			}

			if (StocksBL.EditStock(SelectedStockCopy))
			{
				Functions.LogInfo("Stock edited successfully");
				Stocks.RepopulateFrom(StocksBL.GetAllStocks());
			}
		}

		private void Delete()
		{
			if (StocksBL.DeleteStock(SelectedStockCopy.ID))
			{
				Functions.LogInfo("Stock deleted successfully");
				Stocks.RepopulateFrom(StocksBL.GetAllStocks());
			}
		}
	}
}
