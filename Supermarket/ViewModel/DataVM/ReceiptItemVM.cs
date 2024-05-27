using Supermarket.Model;
using Supermarket.Model.Entities;

namespace Supermarket.ViewModel.DataVM
{
	internal class ReceiptItemVM
	{
		public int ID { get; set; }
		public ProductVM Product { get; set; }
		public int Quantity { get; set; }
		public decimal TotalPrice { get; set; }

		public ReceiptItemVM()
		{
			ID = 0;
			Product = new ProductVM();
			Quantity = 0;
			TotalPrice = 0;
		}

		public ReceiptItemVM(int id, ProductVM product, int quantity, decimal totalPrice)
		{
			ID = id;
			Product = product;
			Quantity = quantity;
			TotalPrice = totalPrice;
		}

		public ReceiptItemVM(ReceiptItem item)
		{
			ID = item.ID;
			Product = new ProductVM(Cache.Instance.Products.Find((p) => p.ID == item.ProductID));
			Quantity = item.Quantity;
			TotalPrice = item.TotalPrice;
		}

		public override string ToString()
		{
			return $"{nameof(ReceiptItemVM)}{{ ID: {ID}, Product: {Product}, Quantity: {Quantity}, TotalPrice: {TotalPrice} }}";
		}
	}
}
