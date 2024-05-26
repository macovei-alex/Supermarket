using Supermarket.Model;
using Supermarket.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Proxies;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.ViewModel.DataVM
{
	internal class ProductVM
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public string Barcode { get; set; }
		public CategoryVM Category { get; set; }
		public ProducerVM Producer { get; set; }

		public ProductVM()
		{
			ID = 0;
			Name = string.Empty;
			Barcode = string.Empty;
			Category = new CategoryVM();
			Producer = new ProducerVM();
		}

		public ProductVM(int id, string name, string barcode, CategoryVM category, ProducerVM producer)
		{
			ID = id;
			Name = name;
			Barcode = barcode;
			Category = category;
			Producer = producer;
		}

		public ProductVM(Product product)
		{
			ID = product.ID;
			Name = product.Name;
			Barcode = product.Barcode;
			Category = new CategoryVM(Cache.Instance.Categories.Find((c) => c.ID == product.CategoryID));
			Producer = new ProducerVM(Cache.Instance.Producers.Find((p) => p.ID == product.ProducerID));
		}

		public override string ToString()
		{
			return $"{nameof(ProductVM)}{{ ID: {ID}, Name: {Name}, Barcode: {Barcode}, Category: {Category}, Producer: {Producer} }}";
		}
	}
}
