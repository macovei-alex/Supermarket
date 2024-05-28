using Supermarket.Model.DataAccess.EntityDALs;
using Supermarket.Utilities;
using Supermarket.ViewModel.DataVM;
using System.Collections.ObjectModel;

namespace Supermarket.Model.BusinessLogic
{
	internal static class ProducerBL
	{
		public static ObservableCollection<ProducerVM> GetAllProducers()
		{
			var producers = new ObservableCollection<ProducerVM>();
			foreach (var producer in ProducerDAL.GetAllProducers())
			{
				producers.Add(new ProducerVM(producer));
			}
			return producers;
		}

		public static bool CreateProducer(string name, int countryID)
		{
			string message = ProducerDAL.CreateProducer(name, countryID);
			if (message != Cache.Instance.SuccessMessage)
			{
				Functions.LogError(message);
				return false;
			}

			Cache.Instance.Invalidate(Cache.CacheType.Producer);
			return true;
		}

		public static bool EditProducer(string oldName, string name, string countryName)
		{
			var producer = Cache.Instance.Producers.Find((p) => p.Name == oldName);
			if (producer == null)
			{
				Functions.LogError($"There is no producer with name ( {oldName} )");
				return false;
			}

			int countryID = producer.CountryID;
			if (countryName != null && countryName != string.Empty)
			{
				var country = Cache.Instance.Countries.Find((c) => c.Name == countryName);
				if (country == null)
				{
					Functions.LogError($"There is no country with name ( {countryName} )");
					return false;
				}
				countryID = country.ID;
			}

			return EditProducer(producer.ID, name, countryID);
		}

		public static bool EditProducer(int id, string name, int countryID)
		{
			string message = ProducerDAL.EditProducer(id, name, countryID);
			if (message != Cache.Instance.SuccessMessage)
			{
				Functions.LogError(message);
				return false;
			}

			Cache.Instance.Invalidate(Cache.CacheType.Producer);
			return true;
		}

		public static bool DeleteProducer(string name)
		{
			var producer = Cache.Instance.Producers.Find((p) => p.Name == name);
			if (producer == null)
			{
				Functions.LogError($"There is no producer with name ( {name} ).");
				return false;
			}

			return DeleteProducer(producer.ID);
		}

		public static bool DeleteProducer(int id)
		{
			string message = ProducerDAL.DeleteProducer(id);
			if (message != Cache.Instance.SuccessMessage)
			{
				Functions.LogError(message);
				return false;
			}

			Cache.Instance.Invalidate(Cache.CacheType.Producer);
			return true;
		}
	}
}
