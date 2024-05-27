using Supermarket.Model.BusinessLogic;
using Supermarket.ViewModel.Commands;
using Supermarket.ViewModel.DataVM;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Linq;

namespace Supermarket.ViewModel.MainWindowVMs
{
	internal partial class ProductsPageVM : BaseVM
	{
		#region Properties

		public List<CategoryVM> Categories { get; private set; }
		public List<CountryVM> Countries { get; private set; }
		public List<ProducerVM> Producers { get; private set; }
		public List<ProductVM> Products { get; private set; }

		private List<CategoryVM> _filteredCategories;
		public List<CategoryVM> FilteredCategories
		{
			get => _filteredCategories;
			set
			{
				_filteredCategories = value;
				OnPropertyChanged(nameof(FilteredCategories));
			}
		}

		private List<CountryVM> _filteredCountries;
		public List<CountryVM> FilteredCountries
		{
			get => _filteredCountries;
			set
			{
				_filteredCountries = value;
				OnPropertyChanged(nameof(FilteredCountries));
			}
		}

		private List<ProducerVM> _filteredProducers;
		public List<ProducerVM> FilteredProducers
		{
			get => _filteredProducers;
			set
			{
				_filteredProducers = value;
				OnPropertyChanged(nameof(FilteredProducers));
			}
		}

		private List<ProductVM> _filteredProducts;
		public List<ProductVM> FilteredProducts
		{
			get => _filteredProducts;
			set
			{
				_filteredProducts = value;
				OnPropertyChanged(nameof(FilteredProducts));
			}
		}

		private CategoryVM _selectedCategory;
		public CategoryVM SelectedCategory
		{
			get => _selectedCategory;
			set
			{
				_selectedCategory = value;
				OnPropertyChanged(nameof(SelectedCategory));
				if (_doFilter)
				{
					_doFilter = false;
					UpdateFilters();
					_doFilter = true;
				}
			}
		}

		private CountryVM _selectedCountry;
		public CountryVM SelectedCountry
		{
			get => _selectedCountry;
			set
			{
				_selectedCountry = value;
				OnPropertyChanged(nameof(SelectedCountry));
				if (_doFilter)
				{
					_doFilter = false;
					UpdateFilters();
					_doFilter = true;
				}
			}
		}

		private ProducerVM _selectedProducer;
		public ProducerVM SelectedProducer
		{
			get => _selectedProducer;
			set
			{
				_selectedProducer = value;
				OnPropertyChanged(nameof(SelectedProducer));
				if (_doFilter)
				{
					_doFilter = false;
					UpdateFilters();
					_doFilter = true;
				}
			}
		}

		private bool _doFilter;

		private decimal _filteredItemsValue;
		public decimal FilteredItemsValue
		{
			get => _filteredItemsValue;
			set
			{
				_filteredItemsValue = value;
				OnPropertyChanged(nameof(FilteredItemsValue));
			}
		}

		private string _productNameFilter;
		public string ProductNameFilter
		{
			get => _productNameFilter;
			set
			{
				_productNameFilter = value;
				OnPropertyChanged(nameof(ProductNameFilter));
				if (_doFilter)
				{
					_doFilter = false;
					UpdateFilters();
					_doFilter = true;
				}
			}
		}

		private string _barcodeFilter;
		public string BarcodeFilter
		{
			get => _barcodeFilter;
			set
			{
				_barcodeFilter = value;
				OnPropertyChanged(nameof(BarcodeFilter));
				if (_doFilter)
				{
					_doFilter = false;
					UpdateFilters();
					_doFilter = true;
				}
			}
		}

		#endregion

		#region Commands

		public ICommand ResetFiltersCommand { get; }

		public ICommand AddProductCommand { get; }
		public ICommand AddCategoryCommand { get; }
		public ICommand AddCountryCommand { get; }
		public ICommand AddProducerCommand { get; }

		public ICommand EditProductCommand { get; }
		public ICommand EditCategoryCommand { get; }
		public ICommand EditCountryCommand { get; }
		public ICommand EditProducerCommand { get; }

		public ICommand DeleteProductCommand { get; }
		public ICommand DeleteCategoryCommand { get; }
		public ICommand DeleteCountryCommand { get; }
		public ICommand DeleteProducerCommand { get; }

		#endregion

		public ProductsPageVM()
		{
			Products = new List<ProductVM>(ProductBL.GetAllProducts());
			Categories = new List<CategoryVM>(CategoryBL.GetAllCategories());
			Countries = new List<CountryVM>(CountryBL.GetAllCountries());
			Producers = new List<ProducerVM>(ProducerBL.GetAllProducers());

			ResetFiltersCommand = new RelayCommandVoid(ResetFilters);

			AddCategoryCommand = new RelayCommandVoid(AddCategory, () => LocalStorage.CurrentUser.IsAdmin);
			AddCountryCommand = new RelayCommandVoid(AddCountry, () => LocalStorage.CurrentUser.IsAdmin);
			AddProducerCommand = new RelayCommandVoid(AddProducer, () => LocalStorage.CurrentUser.IsAdmin);
			AddProductCommand = new RelayCommandVoid(AddProduct, () => LocalStorage.CurrentUser.IsAdmin);

			EditCategoryCommand = new RelayCommandVoid(EditCategory, () => LocalStorage.CurrentUser.IsAdmin);
			EditCountryCommand = new RelayCommandVoid(EditCountry, () => LocalStorage.CurrentUser.IsAdmin);
			EditProducerCommand = new RelayCommandVoid(EditProducer, () => LocalStorage.CurrentUser.IsAdmin);
			EditProductCommand = new RelayCommandVoid(EditProduct, () => LocalStorage.CurrentUser.IsAdmin);

			DeleteCategoryCommand = new RelayCommandVoid(DeleteCategory, () => LocalStorage.CurrentUser.IsAdmin);
			DeleteCountryCommand = new RelayCommandVoid(DeleteCountry, () => LocalStorage.CurrentUser.IsAdmin);
			DeleteProducerCommand = new RelayCommandVoid(DeleteProducer, () => LocalStorage.CurrentUser.IsAdmin);
			DeleteProductCommand = new RelayCommandVoid(DeleteProduct, () => LocalStorage.CurrentUser.IsAdmin);

			ResetFilters();
			CalculateFilteredItemsValue();
		}

		private void CalculateFilteredItemsValue()
		{
			FilteredItemsValue = ProductBL.GetProductsValueFiltered(
				FilteredCategories.Select((category) => category.ID).ToList(),
				FilteredProducers.Select((producer) => producer.ID).ToList());
		}

		private void ResetFilters()
		{
			_doFilter = false;

			SelectedCategory = null;
			SelectedCountry = null;
			SelectedProducer = null;

			FilteredCategories = new List<CategoryVM>(Categories);
			FilteredCountries = new List<CountryVM>(Countries);
			FilteredProducers = new List<ProducerVM>(Producers);
			FilteredProducts = new List<ProductVM>(Products);

			ProductNameFilter = string.Empty;
			BarcodeFilter = string.Empty;

			CalculateFilteredItemsValue();

			_doFilter = true;
		}

		private void UpdateFilters()
		{
			FilteredProducts = GetFilteredProductsQueryable().ToList();

			UpdateCategories();
			UpdateCountries();
			UpdateProducers();

			CalculateFilteredItemsValue();
		}

		private List<Func<ProductVM, bool>> CalculateProductConditions()
		{
			var conditions = new List<Func<ProductVM, bool>>();

			if (SelectedCategory != null)
			{
				conditions.Add((product) => product.Category.ID == SelectedCategory.ID);
			}

			if (SelectedCountry != null)
			{
				conditions.Add((product) => product.Producer.Country.ID == SelectedCountry.ID);
			}

			if (SelectedProducer != null)
			{
				conditions.Add((product) => product.Producer.ID == SelectedProducer.ID);
			}

			return conditions;
		}

		private IQueryable<ProductVM> GetFilteredProductsQueryable()
		{
			var productConditions = CalculateProductConditions();
			var filteredProducts = Products.AsQueryable();
			foreach (var condition in productConditions)
			{
				filteredProducts = filteredProducts.Where(condition).AsQueryable();
			}

			if (ProductNameFilter != string.Empty)
			{
				filteredProducts = filteredProducts
					.Where((product) => product.Name.Contains(ProductNameFilter))
					.AsQueryable();
			}

			if (BarcodeFilter != string.Empty)
			{
				filteredProducts = filteredProducts
					.Where((product) => product.Barcode.Contains(BarcodeFilter))
					.AsQueryable();
			}

			return filteredProducts;
		}

		private void UpdateCategories()
		{
			var categoriesQueryable = from product in FilteredProducts
									  join category in Categories
									  on product.Category.ID equals category.ID
									  select category;
			var categories = categoriesQueryable.Distinct().ToList();
			FilteredCategories = categoriesQueryable.Distinct().ToList();
			if (SelectedCategory != null)
			{
				SelectedCategory = FilteredCategories.Find((category) => category.ID == SelectedCategory.ID);
			}
		}

		private void UpdateCountries()
		{
			var countriesQueryable = from product in FilteredProducts
									 join country in Countries on product.Producer.Country.ID equals country.ID
									 select country;
			FilteredCountries = countriesQueryable.Distinct().ToList();
			if (SelectedCountry != null)
			{
				SelectedCountry = FilteredCountries.Find((country) => country.ID == SelectedCountry.ID);
			}
		}

		private void UpdateProducers()
		{
			var producersQueryable = from product in FilteredProducts
									 join producer in Producers on product.Producer.ID equals producer.ID
									 select producer;
			FilteredProducers = producersQueryable.Distinct().ToList();
			if (SelectedProducer != null)
			{
				SelectedProducer = FilteredProducers.Find((producer) => producer.ID == SelectedProducer.ID);
			}
		}
	}
}
