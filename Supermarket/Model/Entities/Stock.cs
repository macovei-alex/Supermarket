using Supermarket.Exceptions;
using Supermarket.ViewModel;
using Supermarket.ViewModel.DataVM;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Model.Entities
{
	internal class Stock
	{
		public int ID { get; set; }
		public int ProductID { get; set; }
		public int Quantity { get; set; }
		public int InitialQuantity { get; set; }
		public string Unit { get; set; }
		public DateTime SupplyDate { get; set; }
		public DateTime ExpirationDate { get; set; }
		public decimal PurchasePrice { get; set; }
		public decimal SellingPrice { get; set; }

		public Stock()
		{
			ID = 0;
			ProductID = 0;
			Quantity = 0;
			InitialQuantity = 0;
			Unit = string.Empty;
			SupplyDate = DateTime.Now;
			ExpirationDate = DateTime.Now;
			PurchasePrice = 0;
			SellingPrice = 0;
		}

		public Stock(int id, int productID, int quantity, int initialQunatity, string unit, DateTime supplyDate, DateTime expirationDate, decimal purchasePrice, decimal sellingPrice)
		{
			ID = id;
			ProductID = productID;
			Quantity = quantity;
			InitialQuantity = initialQunatity;
			Unit = unit;
			SupplyDate = supplyDate;
			ExpirationDate = expirationDate;
			PurchasePrice = purchasePrice;
			SellingPrice = sellingPrice;
		}

		public Stock(SqlDataReader reader)
		{
			ID = reader.GetInt32(0);
			ProductID = reader.GetInt32(1);
			Quantity = reader.GetInt32(2);
			InitialQuantity = reader.GetInt32(3);
			Unit = reader.GetString(4);
			SupplyDate = reader.GetDateTime(5);
			ExpirationDate = reader.GetDateTime(6);
			PurchasePrice = reader.GetDecimal(7);
			SellingPrice = reader.GetDecimal(8);
		}

		public Stock(StockVM stockVM)
		{
			ID = stockVM.ID;
			ProductID = stockVM.Product.ID;
			Quantity = stockVM.Quantity;
			InitialQuantity = stockVM.InitialQuantity;
			Unit = stockVM.Unit;
			SupplyDate = stockVM.SupplyDate;
			ExpirationDate = stockVM.ExpirationDate;
			PurchasePrice = stockVM.PurchasePrice;
			SellingPrice = stockVM.SellingPrice;
		}

		public override string ToString()
		{
			return $"{nameof(Stock)}{{ ID: {ID}, ProductID: {ProductID}, Quantity: {Quantity}, InitialQuantity: {InitialQuantity}, Unit: {Unit}, SupplyDate: {SupplyDate}, ExpirationDate: {ExpirationDate}, PurchasePrice: {PurchasePrice}, SellingPrice: {SellingPrice} }}";
		}
	}
}
