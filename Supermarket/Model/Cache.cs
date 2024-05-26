﻿using Supermarket.Model.DataAccess.EntityDALs;
using Supermarket.Model.Entities;
using System.Collections.Generic;
using System.Configuration;

namespace Supermarket.Model
{
	internal class Cache
	{
		public enum CacheType
		{
			UserType,
			Country,
			Category,
			Producer,
			Product,
			User
		}

		public static Cache Instance { get; } = new Cache();

		private List<UserType> _userTypes;
		public List<UserType> UserTypes
		{
			get
			{
				if (_userTypes == null)
				{
					LoadList(CacheType.UserType);
				}
				return _userTypes;
			}
			private set { _userTypes = value; }
		}

		private List<Country> _countries;
		public List<Country> Countries
		{
			get
			{
				if (_countries == null)
				{
					LoadList(CacheType.Country);
				}
				return _countries;
			}
			private set { Countries = value; }
		}

		private List<Category> _categories;
		public List<Category> Categories
		{
			get
			{
				if (_categories == null)
				{
					LoadList(CacheType.Category);
				}
				return _categories;
			}
			private set { Categories = value; }
		}

		private List<Producer> _producers;
		public List<Producer> Producers
		{
			get
			{
				if (_producers == null)
				{
					LoadList(CacheType.Producer);
				}
				return _producers;
			}
			private set { Producers = value; }
		}

		private List<Product> _products;
		public List<Product> Products
		{
			get
			{
				if (_products == null)
				{
					LoadList(CacheType.Product);
				}
				return _products;
			}
			private set { Products = value; }
		}

		private List<MarketUser> _users;
		public List<MarketUser> Users
		{
			get
			{
				if (_users == null)
				{
					LoadList(CacheType.User);
				}
				return _users;
			}
			private set { _users = value; }
		}

		public decimal VAT { get; } = decimal.Parse(ConfigurationManager.AppSettings["VAT"]);
		public string AdminStr { get; } = "administrator";
		public string CashierStr { get; } = "cashier";
		public string SuccessMessage { get; } = "Success";
		public int AdminID { get; } = 1;
		public int CashierID { get; } = 2;

		private Cache()
		{
			// singleton constructor
		}

		private void LoadList(CacheType updateType)
		{
			switch (updateType)
			{
				case CacheType.UserType:
					_userTypes = UserTypeDAL.GetAllUserTypes();
					break;

				case CacheType.Country:
					_countries = CountryDAL.GetAllCountries();
					break;

				case CacheType.Category:
					_categories = CategoryDAL.GetAllCategories();
					break;

				case CacheType.Producer:
					_producers = ProducerDAL.GetAllProducers();
					break;

				case CacheType.Product:
					_products = ProductDAL.GetAllProducts();
					break;

				case CacheType.User:
					_users = MarketUserDAL.GetAllUsers();
					break;

				default:
					break;
			}
		}

		public void Invalidate(CacheType type)
		{
			switch (type)
			{
				case CacheType.UserType:
					_userTypes = null;
					break;

				case CacheType.Country:
					_countries = null;
					break;

				case CacheType.Category:
					_categories = null;
					break;

				case CacheType.Producer:
					_producers = null;
					break;

				case CacheType.Product:
					_products = null;
					break;

				case CacheType.User:
					_users = null;
					break;

				default:
					break;
			}
		}
	}
}