using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Model.Entities
{
	internal class Product
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public string Barcode { get; set; }
		public int CategoryID { get; set; }
		public int ProducerID { get; set; }

		public Product(int id, string name, string barcode, int categoryID, int producerID)
		{
			ID = id;
			Name = name;
			Barcode = barcode;
			CategoryID = categoryID;
			ProducerID = producerID;
		}

		public Product(SqlDataReader reader)
		{
			ID = reader.GetInt32(0);
			Name = reader.GetString(1);
			Barcode = reader.GetString(2);
			CategoryID = reader.GetInt32(3);
			ProducerID = reader.GetInt32(4);
		}

		public override string ToString()
		{
			return $"{nameof(Product)}{{ ID: {ID}, Name: {Name}, Barcode: {Barcode}, CategoryID: {CategoryID}, ProducerID: {ProducerID} }}";
		}
	}
}
