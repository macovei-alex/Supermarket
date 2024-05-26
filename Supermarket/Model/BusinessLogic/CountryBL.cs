using Supermarket.Model.DataAccess.EntityDALs;
using Supermarket.Model.Entities;
using Supermarket.Utilities;
using Supermarket.ViewModel.DataVM;
using System;
using System.Collections.ObjectModel;

namespace Supermarket.Model.BusinessLogic
{
	internal static class CountryBL
	{
		public static ObservableCollection<CountryVM> GetAllCountries()
		{
			var countries = new ObservableCollection<CountryVM>();
			foreach (Country country in CountryDAL.GetAllCountries())
			{
				countries.Add(new CountryVM(country));
			}
			return countries;
		}

		public static bool CreateCountry(string name)
		{
			string message = CountryDAL.CreateCountry(name);
			if (message != Cache.Instance.SuccessMessage)
			{
				Functions.LogError(message);
				return false;
			}

			Cache.Instance.Invalidate(Cache.CacheType.Country);
			return true;
		}

		public static bool EditCountry(string oldName, string newName)
		{
			var country = Cache.Instance.Countries.Find((c) => c.Name == oldName);
			if (country == null)
			{
				Functions.LogError($"There is no country with name ( {oldName} ).");
				return false;
			}

			return EditCountry(country.ID, newName);
		}

		public static bool EditCountry(int id, string newName)
		{
			string message = CountryDAL.EditCountry(id, newName);
			if (message != Cache.Instance.SuccessMessage)
			{
				Functions.LogError(message);
				return false;
			}

			Cache.Instance.Invalidate(Cache.CacheType.Country);
			return true;
		}

		public static bool DeleteCountry(string name)
		{
			var country = Cache.Instance.Countries.Find((c) => c.Name == name);
			if (country == null)
			{
				Functions.LogError($"There is no country with name ( {name} ).");
				return false;
			}

			return DeleteCountry(country.ID);
		}

		public static bool DeleteCountry(int id)
		{
			string message = CountryDAL.DeleteCountry(id);
			if (message != Cache.Instance.SuccessMessage)
			{
				Functions.LogError(message);
				return false;
			}

			Cache.Instance.Invalidate(Cache.CacheType.Country);
			return true;
		}
	}
}
