using Supermarket.Model.DataAccess.EntityDALs;
using Supermarket.Utilities;
using Supermarket.ViewModel.DataVM;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Supermarket.Model.BusinessLogic
{
	internal static class ProductBL
	{
		public static ObservableCollection<ProductVM> GetAllProducts()
		{
			var products = new ObservableCollection<ProductVM>();
			foreach (var product in ProductDAL.GetAllProducts())
			{
				products.Add(new ProductVM(product));
			}
			return products;
		}

		public static decimal GetProductsValueFiltered(List<int> categoryIDs, List<int> producerIDs)
		{
			decimal value = 0;
			foreach (int categoryID in categoryIDs)
			{
				foreach (int producerID in producerIDs)
				{
					value += ProductDAL.GetProductsValueFiltered(categoryID, producerID);
				}
			}
			return value;
		}

		public static bool CreateProduct(string name, string barcode, string categoryName, string producerName)
		{
			var category = Cache.Instance.Categories.Find((c) => c.Name == categoryName);
			if (category == null)
			{
				Functions.LogError($"There is no category with name ( {name} ).");
				return false;
			}

			var producer = Cache.Instance.Producers.Find((p) => p.Name == producerName);
			if (producer == null)
			{
				Functions.LogError($"There is no producer with name ( {name} ).");
				return false;
			}

			return CreateProduct(name, barcode, category.ID, producer.ID);
		}

		public static bool CreateProduct(string name, string barcode, int categoryID, int producerID)
		{
			string message = ProductDAL.CreateProduct(name, barcode, categoryID, producerID);
			if (message != Cache.Instance.SuccessMessage)
			{
				Functions.LogError(message);
				return false;
			}

			Cache.Instance.Invalidate(Cache.CacheType.Product);
			return true;
		}

		public static bool EditProduct(string oldName, string name, string barcode, string categoryName, string producerName)
		{
			var product = Cache.Instance.Products.Find((p) => p.Name == oldName);
			if (product == null)
			{
				Functions.LogError($"There is no product with name ( {oldName} ).");
				return false;
			}

			int categoryID = product.CategoryID;
			if (categoryName != null && categoryName != string.Empty)
			{
				var category = Cache.Instance.Categories.Find((c) => c.Name == categoryName);
				if (category == null)
				{
					Functions.LogError($"There is no category with name ( {name} ).");
					return false;
				}
				categoryID = category.ID;
			}

			int producerID = product.ProducerID;
			if (producerName != null && producerName != string.Empty)
			{
				var producer = Cache.Instance.Producers.Find((p) => p.Name == producerName);
				if (producer == null)
				{
					Functions.LogError($"There is no producer with name ( {name} ).");
					return false;
				}
				producerID = producer.ID;
			}

			name = (name == string.Empty ? product.Name : name);
			barcode = (barcode == string.Empty ? product.Barcode : barcode);

			return EditProduct(product.ID, name, barcode, categoryID, producerID);
		}

		public static bool EditProduct(int id, string name, string barcode, int categoryID, int producerID)
		{
			string message = ProductDAL.EditProduct(id, name, barcode, categoryID, producerID);
			if (message != Cache.Instance.SuccessMessage)
			{
				Functions.LogError(message);
				return false;
			}

			Cache.Instance.Invalidate(Cache.CacheType.Product);
			return true;
		}

		public static bool DeleteProduct(string name)
		{
			var product = Cache.Instance.Products.Find((p) => p.Name == name);
			if (product == null)
			{
				Functions.LogError($"There is no product with name ( {name} ).");
				return false;
			}

			return DeleteProduct(product.ID);
		}

		public static bool DeleteProduct(int id)
		{
			string message = ProductDAL.DeleteProduct(id);
			if (message != Cache.Instance.SuccessMessage)
			{
				Functions.LogError(message);
				return false;
			}

			Cache.Instance.Invalidate(Cache.CacheType.Product);
			return true;
		}
	}
}
