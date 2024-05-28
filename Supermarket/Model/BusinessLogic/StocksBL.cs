using Supermarket.Model.DataAccess.EntityDALs;
using Supermarket.Model.Entities;
using Supermarket.Utilities;
using Supermarket.ViewModel.DataVM;
using System;
using System.Collections.ObjectModel;

namespace Supermarket.Model.BusinessLogic
{
	internal class StocksBL
	{
		public static ObservableCollection<StockVM> GetAllStocks()
		{
			var stocks = new ObservableCollection<StockVM>();
			foreach (Stock stock in StockDAL.GetAllStocks())
			{
				stocks.Add(new StockVM(stock));
			}
			return stocks;
		}

		public static bool CreateStock(StockVM stockVM)
		{
			stockVM.SellingPrice = stockVM.PurchasePrice * (1 + Cache.Instance.VAT);
			stockVM.Quantity = stockVM.InitialQuantity;
			stockVM.SupplyDate = DateTime.Now;

			string message = StockDAL.AddNewStock(new Stock(stockVM));
			if (message != Cache.Instance.SuccessMessage)
			{
				Functions.LogError(message);
				return false;
			}

			Cache.Instance.Invalidate(Cache.CacheType.Stock);
			return true;
		}

		public static bool DeleteStock(int id)
		{
			string message = StockDAL.DeleteStock(id);
			if (message != Cache.Instance.SuccessMessage)
			{
				Functions.LogError(message);
				return false;
			}

			Cache.Instance.Invalidate(Cache.CacheType.Stock);
			return true;
		}

		public static bool EditStock(StockVM stockVM)
		{
			string message = StockDAL.EditStock(new Stock(stockVM));
			if (message != Cache.Instance.SuccessMessage)
			{
				Functions.LogError(message);
				return false;
			}

			Cache.Instance.Invalidate(Cache.CacheType.Stock);
			return true;
		}

		public static bool DeactivateExpiredStocks()
		{
			return StockDAL.InvalidateExpiredStocks() == Cache.Instance.SuccessMessage;
		}
	}
}
