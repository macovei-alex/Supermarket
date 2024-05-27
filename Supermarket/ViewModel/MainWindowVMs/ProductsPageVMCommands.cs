using Supermarket.Model;
using Supermarket.Model.BusinessLogic;
using Supermarket.Utilities;
using Supermarket.Utils;
using Supermarket.View;
using System.Collections.Generic;
using System.Windows;

namespace Supermarket.ViewModel.MainWindowVMs
{
	internal partial class ProductsPageVM
	{
		private void AddCategory()
		{
			var inputWindow = new InputWindow("Add new Category", new List<string> { "Name" });
			var inputs = (inputWindow.DataContext as InputWindowVM).InputCollection;
			var dialogResult = inputWindow.ShowDialog() ?? false;
			if (!dialogResult)
			{
				return;
			}

			var result = MessageBox.Show($"Do you really want to add the category {{ {Input.CollectionToString(inputs)} }}?", "Confirmation", MessageBoxButton.YesNo);
			if (result == MessageBoxResult.Yes)
			{
				string name = inputs[0].Value;
				if (CategoryBL.CreateCategory(name))
				{
					MessageBox.Show($"Category {name} created successfuly");
					Categories.RepopulateFrom(CategoryBL.GetAllCategories());
					ResetFilters();
				}
			}
		}

		private void AddCountry()
		{
			var inputWindow = new InputWindow("Add new Country", new List<string> { "Name" });
			var inputs = (inputWindow.DataContext as InputWindowVM).InputCollection;
			var dialogResult = inputWindow.ShowDialog() ?? false;
			if (!dialogResult)
			{
				return;
			}

			var result = MessageBox.Show($"Do you really want to add the country {{ {Input.CollectionToString(inputs)} }}?", "Confirmation", MessageBoxButton.YesNo);
			if (result == MessageBoxResult.Yes)
			{
				string name = inputs[0].Value;
				if (CountryBL.CreateCountry(name))
				{
					MessageBox.Show($"Country {name} created successfuly");
					Countries.RepopulateFrom(CountryBL.GetAllCountries());
					ResetFilters();
				}
			}
		}

		private void AddProducer()
		{
			var inputWindow = new InputWindow("Add new Producer", new List<string> { "Name", "Country" });
			var inputs = (inputWindow.DataContext as InputWindowVM).InputCollection;
			var dialogResult = inputWindow.ShowDialog() ?? false;
			if (!dialogResult)
			{
				return;
			}

			var result = MessageBox.Show($"Do you really want to add the producer {{ {Input.CollectionToString(inputs)} }}?", "Confirmation", MessageBoxButton.YesNo);
			if (result == MessageBoxResult.Yes)
			{
				string producerName = inputs[0].Value;
				string countryName = inputs[1].Value;
				var country = Cache.Instance.Countries.Find((c) => c.Name == countryName);
				if (country == null)
				{
					MessageBox.Show($"Please add the country ( {countryName} ) first");
					return;
				}

				if (ProducerBL.CreateProducer(producerName, country.ID))
				{
					MessageBox.Show($"Category {producerName} created successfuly");
					Producers.RepopulateFrom(ProducerBL.GetAllProducers());
					ResetFilters();
				}
			}
		}

		private void AddProduct()
		{
			var inputWindow = new InputWindow("Add new Product", new List<string> { "Name", "Barcode", "Category", "Producer" });
			var inputs = (inputWindow.DataContext as InputWindowVM).InputCollection;
			var dialogResult = inputWindow.ShowDialog() ?? false;
			if (!dialogResult)
			{
				return;
			}

			var result = MessageBox.Show($"Do you really want to add the product {{ {Input.CollectionToString(inputs)} }}?", "Confirmation", MessageBoxButton.YesNo);
			if (result == MessageBoxResult.Yes)
			{
				if (ProductBL.CreateProduct(inputs[0].Value, inputs[1].Value, inputs[2].Value, inputs[3].Value))
				{
					MessageBox.Show($"Product {inputs[0].Value} created successfuly");
					Products.RepopulateFrom(ProductBL.GetAllProducts());
					ResetFilters();
				}
			}
		}

		private void EditCategory()
		{
			var inputWindow = new InputWindow("Edit Category", new List<string> { "Old Name", "New Name" });
			var inputs = (inputWindow.DataContext as InputWindowVM).InputCollection;
			var dialogResult = inputWindow.ShowDialog() ?? false;
			if (!dialogResult)
			{
				return;
			}

			var result = MessageBox.Show($"Do you really want to edit the category {{ {Input.CollectionToString(inputs)} }}?", "Confirmation", MessageBoxButton.YesNo);
			if (result == MessageBoxResult.Yes)
			{
				if (CategoryBL.EditCategory(inputs[0].Value, inputs[1].Value))
				{
					MessageBox.Show($"Category {inputs[0].Value} edited successfuly");
					Categories.RepopulateFrom(CategoryBL.GetAllCategories());
					Products.RepopulateFrom(ProductBL.GetAllProducts());
					ResetFilters();
				}
			}
		}

		private void EditCountry()
		{
			var inputWindow = new InputWindow("Edit Country", new List<string> { "Old Name", "New Name" });
			var inputs = (inputWindow.DataContext as InputWindowVM).InputCollection;
			var dialogResult = inputWindow.ShowDialog() ?? false;
			if (!dialogResult)
			{
				return;
			}

			var result = MessageBox.Show($"Do you really want to edit the country {{ {Input.CollectionToString(inputs)} }}?", "Confirmation", MessageBoxButton.YesNo);
			if (result == MessageBoxResult.Yes)
			{
				if (CountryBL.EditCountry(inputs[0].Value, inputs[1].Value))
				{
					MessageBox.Show($"Country {inputs[0].Value} edited successfuly");
					Countries.RepopulateFrom(CountryBL.GetAllCountries());
					Products.RepopulateFrom(ProductBL.GetAllProducts());
					ResetFilters();
				}
			}
		}

		private void EditProducer()
		{
			var inputWindow = new InputWindow("Edit Producer", new List<string> { "Old Name", "New Name (leave empty if unchanged)", "New Country (leave empty if unchanged)" });
			var inputs = (inputWindow.DataContext as InputWindowVM).InputCollection;
			var dialogResult = inputWindow.ShowDialog() ?? false;
			if (!dialogResult)
			{
				return;
			}

			var result = MessageBox.Show($"Do you really want to edit the producer {{ {Input.CollectionToString(inputs)} }}?", "Confirmation", MessageBoxButton.YesNo);
			if (result == MessageBoxResult.Yes)
			{
				if (ProducerBL.EditProducer(inputs[0].Value, inputs[1].Value, inputs[2].Value))
				{
					MessageBox.Show($"Product {inputs[0].Value} edited successfuly");
					Producers.RepopulateFrom(ProducerBL.GetAllProducers());
					Products.RepopulateFrom(ProductBL.GetAllProducts());
					ResetFilters();
				}
			}
		}

		private void EditProduct()
		{
			var inputWindow = new InputWindow("Edit Product", new List<string> { "Old Name", "New Name (leave empty if unchanged)", "New Barcode (leave empty if unchanged)", "New category (leave empty if unchanged)", "New Producer (leave empty if unchanged)" });
			var inputs = (inputWindow.DataContext as InputWindowVM).InputCollection;
			var dialogResult = inputWindow.ShowDialog() ?? false;
			if (!dialogResult)
			{
				return;
			}

			var result = MessageBox.Show($"Do you really want to edit the category {{ {Input.CollectionToString(inputs)} }}?", "Confirmation", MessageBoxButton.YesNo);
			if (result != MessageBoxResult.Yes)
			{
				return;
			}

			var productModel = Cache.Instance.Products.Find((p) => p.Name == inputs[0].Value);
			if (productModel == null)
			{
				Functions.LogError($"Product ( {inputs[0].Value} ) does not exist");
				return;
			}

			int productID = productModel.ID;
			string newName = inputs[1].Value;
			string newBarcode = inputs[2].Value;
			int categoryID = productModel.CategoryID;
			int producerID = productModel.ProducerID;

			if (!Functions.AreNotNullOrEmpty(newName))
			{
				newName = productModel.Name;
			}
			if (!Functions.AreNotNullOrEmpty(newBarcode))
			{
				newBarcode = productModel.Barcode;
			}

			string newCategoryName = inputs[3].Value;
			if (Functions.AreNotNullOrEmpty(newCategoryName))
			{
				var categoryModel = Cache.Instance.Categories.Find((c) => c.Name == newCategoryName);
				if (categoryModel == null)
				{
					Functions.LogError($"Category ( {newCategoryName} ) does not exist");
					return;
				}
				categoryID = categoryModel.ID;
			}

			string newProducerName = inputs[4].Value;
			if (Functions.AreNotNullOrEmpty(newProducerName))
			{
				var producerModel = Cache.Instance.Producers.Find((c) => c.Name == newProducerName);
				if (productModel == null)
				{
					Functions.LogError($"Producer ( {newProducerName} ) does not exist");
					return;
				}
				producerID = producerModel.ID;
			}

			if (ProductBL.EditProduct(productID, newName, newBarcode, categoryID, producerID))
			{
				MessageBox.Show($"Product {productModel.Name} edited successfuly");
				Products.RepopulateFrom(ProductBL.GetAllProducts());
				ResetFilters();
			}
		}

		private void DeleteCategory()
		{
			var inputWindow = new InputWindow("Delete Category", new List<string> { "Name" });
			var inputs = (inputWindow.DataContext as InputWindowVM).InputCollection;
			var dialogResult = inputWindow.ShowDialog() ?? false;
			if (!dialogResult)
			{
				return;
			}

			var result = MessageBox.Show($"Do you really want to delete the category {{ {Input.CollectionToString(inputs)} }}?", "Confirmation", MessageBoxButton.YesNo);
			if (result == MessageBoxResult.Yes)
			{
				if (CategoryBL.DeleteCategory(inputs[0].Value))
				{
					MessageBox.Show($"Category {inputs[0].Value} deleted successfuly");
					Categories.RepopulateFrom(CategoryBL.GetAllCategories());
					ResetFilters();
				}
			}
		}

		private void DeleteCountry()
		{
			var inputWindow = new InputWindow("Delete Country", new List<string> { "Name" });
			var inputs = (inputWindow.DataContext as InputWindowVM).InputCollection;
			var dialogResult = inputWindow.ShowDialog() ?? false;
			if (!dialogResult)
			{
				return;
			}

			var result = MessageBox.Show($"Do you really want to delete the country {{ {Input.CollectionToString(inputs)} }}?", "Confirmation", MessageBoxButton.YesNo);
			if (result == MessageBoxResult.Yes)
			{
				if (CountryBL.DeleteCountry(inputs[0].Value))
				{
					MessageBox.Show($"Country {inputs[0].Value} deleted successfuly");
					Countries.RepopulateFrom(CountryBL.GetAllCountries());
					ResetFilters();
				}
			}
		}

		private void DeleteProducer()
		{
			var inputWindow = new InputWindow("Delete Producer", new List<string> { "Name" });
			var inputs = (inputWindow.DataContext as InputWindowVM).InputCollection;
			var dialogResult = inputWindow.ShowDialog() ?? false;
			if (!dialogResult)
			{
				return;
			}

			var result = MessageBox.Show($"Do you really want to delete the producer {{ {Input.CollectionToString(inputs)} }}?", "Confirmation", MessageBoxButton.YesNo);
			if (result == MessageBoxResult.Yes)
			{
				if (ProducerBL.DeleteProducer(inputs[0].Value))
				{
					MessageBox.Show($"Producer {inputs[0].Value} deleted successfuly");
					Producers.RepopulateFrom(ProducerBL.GetAllProducers());
					ResetFilters();
				}
			}
		}

		private void DeleteProduct()
		{
			var inputWindow = new InputWindow("Delete Product", new List<string> { "Name" });
			var inputs = (inputWindow.DataContext as InputWindowVM).InputCollection;
			var dialogResult = inputWindow.ShowDialog() ?? false;
			if (!dialogResult)
			{
				return;
			}

			var result = MessageBox.Show($"Do you really want to delete the product {{ {Input.CollectionToString(inputs)} }}?", "Confirmation", MessageBoxButton.YesNo);
			if (result == MessageBoxResult.Yes)
			{
				if (ProductBL.DeleteProduct(inputs[0].Value))
				{
					MessageBox.Show($"Product {inputs[0].Value} deleted successfuly");
					Products.RepopulateFrom(ProductBL.GetAllProducts());
					ResetFilters();
				}
			}
		}
	}
}
