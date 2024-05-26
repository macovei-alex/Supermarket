using Supermarket.Model;
using Supermarket.Model.Entities;
using System;
using System.Runtime.InteropServices;
using System.Windows;

namespace Supermarket.ViewModel.DataVM
{
	internal class StockVM : BaseVM
	{
		public bool IsExpired => ExpirationDate < DateTime.Now;
		public bool IsEmpty => Quantity == 0;
		public bool IsExpiredOrEmpty => IsExpired || IsEmpty;

		#region Properties

		private int _id;
		public int ID
		{
			get => _id;
			set
			{
				_id = value;
				OnPropertyChanged(nameof(ID));
			}
		}

		private ProductVM _product;
		public ProductVM Product
		{
			get => _product;
			set
			{
				_product = value;
				OnPropertyChanged(nameof(Product));
			}
		}

		private int _quantity;
		public int Quantity
		{
			get => _quantity;
			set
			{
				_quantity = value;
				OnPropertyChanged(nameof(Quantity));
			}
		}

		private int _initialQuantity;
		public int InitialQuantity
		{
			get => _initialQuantity;
			set
			{
				_initialQuantity = value;
				OnPropertyChanged(nameof(InitialQuantity));
			}
		}

		private string _unit;
		public string Unit
		{
			get => _unit;
			set
			{
				_unit = value;
				OnPropertyChanged(nameof(Unit));
			}
		}

		private DateTime _supplyDate;
		public DateTime SupplyDate
		{
			get => _supplyDate;
			set
			{
				_supplyDate = value;
				OnPropertyChanged(nameof(SupplyDate));
			}
		}

		private DateTime _expirationDate;
		public DateTime ExpirationDate
		{
			get => _expirationDate;
			set
			{
				_expirationDate = value;
				OnPropertyChanged(nameof(ExpirationDate));
			}
		}

		private decimal _purchasePrice;
		public decimal PurchasePrice
		{
			get => _purchasePrice;
			set
			{
				_purchasePrice = value;
				OnPropertyChanged(nameof(PurchasePrice));
				SellingPrice = value * (1 + Cache.Instance.VAT);
			}
		}

		private decimal _sellingPrice;
		public decimal SellingPrice
		{
			get => _sellingPrice;
			set
			{
				_sellingPrice = value;
				OnPropertyChanged(nameof(SellingPrice));
			}
		}

		#endregion

		public StockVM()
		{
			ID = 0;
			Product = new ProductVM();
			Quantity = 0;
			InitialQuantity = 0;
			Unit = string.Empty;
			SupplyDate = DateTime.Now;
			ExpirationDate = DateTime.Now;
			PurchasePrice = 0;
			SellingPrice = 0;
		}

		public StockVM(int id, ProductVM product, int quantity, int initialQunatity, string unit, DateTime supplyDate, DateTime expirationDate, decimal purchasePrice, decimal sellingPrice)
		{
			ID = id;
			Product = product;
			Quantity = quantity;
			InitialQuantity = initialQunatity;
			Unit = unit;
			SupplyDate = supplyDate;
			ExpirationDate = expirationDate;
			PurchasePrice = purchasePrice;
			SellingPrice = sellingPrice;
		}

		public StockVM(Stock stock)
		{
			ID = stock.ID;
			Product = new ProductVM(Cache.Instance.Products.Find((p) => p.ID == stock.ProductID));
			Quantity = stock.Quantity;
			InitialQuantity = stock.InitialQuantity;
			Unit = stock.Unit;
			SupplyDate = stock.SupplyDate;
			ExpirationDate = stock.ExpirationDate;
			PurchasePrice = stock.PurchasePrice;
			SellingPrice = stock.SellingPrice;
		}

		public StockVM(StockVM other)
		{
			ID = other.ID;
			Product = other.Product;
			Quantity = other.Quantity;
			InitialQuantity = other.InitialQuantity;
			Unit = other.Unit;
			SupplyDate = other.SupplyDate;
			ExpirationDate = other.ExpirationDate;
			PurchasePrice = other.PurchasePrice;
			SellingPrice = other.SellingPrice;
		}

		public override string ToString()
		{
			return $"{nameof(StockVM)}{{ ID: {ID}, ProductName: {Product}, Quantity: {Quantity}, InitialQuantity: {InitialQuantity}, Unit: {Unit}, SupplyDate: {SupplyDate}, ExpirationDate: {ExpirationDate}, PurchasePrice: {PurchasePrice}, SellingPrice: {SellingPrice} }}";
		}

		public static StockVM DeepCopyOrNew(StockVM stock)
		{
			if (stock == null)
			{
				return new StockVM();
			}

			return new StockVM(stock);
		}
	}
}
