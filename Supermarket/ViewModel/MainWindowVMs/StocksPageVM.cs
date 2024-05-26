﻿using Supermarket.Model;
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

			AddCommand = new RelayCommandVoid(Add);
			EditCommand = new RelayCommandVoid(Edit);
			DeleteCommand = new RelayCommandVoid(Delete);
		}

		private bool CommonChecks()
		{
			if (SelectedStockCopy == null)
			{
				MessageBox.Show("Please first select the stock you want to modify from the table below");
				return false;
			}

			if (!Functions.AreNotNullOrEmpty(PurchasePriceString, SellingPriceString))
			{
				MessageBox.Show("Please enter the prices");
				return false;
			}

			if (!Functions.AreNotNullOrEmpty(SelectedStockCopy.Product?.Name))
			{
				MessageBox.Show("Please enter a product name first");
				return false;
			}

			if (!Functions.AreNotNullOrEmpty(SelectedStockCopy.Unit))
			{
				MessageBox.Show("Please enter a unit of measure first");
				return false;
			}

			var product = Cache.Instance.Products.Find((p) => p.Name == SelectedStockCopy.Product.Name);
			if (product == null)
			{
				MessageBox.Show($"There is no product with name ( {SelectedStockCopy.Product.ID} )");
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
				MessageBox.Show("Please enter a valid number for the prices");
				return;
			}

			if (SelectedStockCopy.PurchasePrice > SelectedStockCopy.SellingPrice)
			{
				MessageBox.Show("Selling price cannot be less than purchase price");
				return;
			}

			if (SelectedStockCopy.InitialQuantity <= 0)
			{
				MessageBox.Show("Initial Quantity cannot be null or negative");
				return;
			}

			if (SelectedStockCopy.ExpirationDate < DateTime.Now)
			{
				MessageBox.Show("Expiration date cannot be in the past");
				return;
			}

			if (SelectedStockCopy.ExpirationDate < SelectedStockCopy.SupplyDate)
			{
				MessageBox.Show("Expiration date cannot be before supply date");
				return;
			}

			SelectedStockCopy.Product = new ProductVM(Cache.Instance.Products.Find((p) => p.Name == SelectedStockCopy.Product.Name));
			if (StocksBL.CreateStock(SelectedStockCopy))
			{
				MessageBox.Show("Stock created successfully");
				Stocks.RepopulateFrom(StocksBL.GetAllStocks());
			}
		}

		private void Edit()
		{
			if (SelectedStockCopy.ID <= 0)
			{
				MessageBox.Show("Please select the stock you want to edit first");
			}

			if (!CommonChecks())
			{
				return;
			}

			if (SelectedStock == null)
			{
				MessageBox.Show("Please first select the stock you want to edit");
				return;
			}

			if (!Functions.AreNotNullOrEmpty(SelectedStockCopy.Product?.Name))
			{
				MessageBox.Show("Please enter a product name first");
				return;
			}

			try
			{
				SelectedStockCopy.PurchasePrice = decimal.Parse(PurchasePriceString);
				SelectedStockCopy.SellingPrice = decimal.Parse(SellingPriceString);
			}
			catch (FormatException)
			{
				MessageBox.Show("Please enter a valid number for the prices");
				return;
			}

			if (SelectedStock.PurchasePrice != SelectedStockCopy.PurchasePrice)
			{
				MessageBox.Show("You cannot change the purchase price of a stock");
				return;
			}

			if (SelectedStockCopy.PurchasePrice > SelectedStockCopy.SellingPrice)
			{
				MessageBox.Show("Selling price cannot be less than purchase price");
				return;
			}

			if (SelectedStockCopy.Quantity <= 0 || SelectedStockCopy.InitialQuantity <= 0)
			{
				MessageBox.Show("Quantity and Initial Quantity cannot be null negative");
				return;
			}

			if (SelectedStockCopy.ExpirationDate < DateTime.Now)
			{
				MessageBox.Show("Expiration date cannot be in the past");
				return;
			}

			if (SelectedStockCopy.ExpirationDate < SelectedStockCopy.SupplyDate)
			{
				MessageBox.Show("Expiration date cannot be before supply date");
				return;
			}

			if (StocksBL.EditStock(SelectedStockCopy))
			{
				MessageBox.Show("Stock edited successfully");
				Stocks.RepopulateFrom(StocksBL.GetAllStocks());
			}
		}

		private void Delete()
		{
			if (StocksBL.DeleteStock(SelectedStockCopy.ID))
			{
				MessageBox.Show("Stock deleted successfully");
				Stocks.RepopulateFrom(StocksBL.GetAllStocks());
			}
		}
	}
}