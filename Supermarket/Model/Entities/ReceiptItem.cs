using System.Data.SqlClient;

namespace Supermarket.Model.Entities
{
	internal class ReceiptItem
	{
		public int ID { get; set; }
		public int ReceiptID { get; set; }
		public int ProductID { get; set; }
		public int Quantity { get; set; }
		public decimal TotalPrice { get; set; }

		public ReceiptItem()
		{
			ID = 0;
			ReceiptID = 0;
			ProductID = 0;
			Quantity = 0;
			TotalPrice = 0;
		}

		public ReceiptItem(int id, int receiptID, int productID, int quantity, decimal totalPrice)
		{
			ID = id;
			ReceiptID = receiptID;
			ProductID = productID;
			Quantity = quantity;
			TotalPrice = totalPrice;
		}

		public ReceiptItem(SqlDataReader reader)
		{
			ID = reader.GetInt32(0);
			ReceiptID = reader.GetInt32(1);
			ProductID = reader.GetInt32(2);
			Quantity = reader.GetInt32(3);
			TotalPrice = reader.GetDecimal(4);
		}

		public override string ToString()
		{
			return $"{nameof(ReceiptItem)}{{ ID: {ID}, ReceiptID: {ReceiptID}, ProductID: {ProductID}, Quantity: {Quantity}, TotalPrice: {TotalPrice} }}";
		}
	}
}
