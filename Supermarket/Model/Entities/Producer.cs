using Supermarket.ViewModel.DataVM;
using System.Data.SqlClient;

namespace Supermarket.Model.Entities
{
	internal class Producer
	{
		public int ID { get; set; }
		public int CountryID { get; set; }
		public string Name { get; set; }

		public Producer(int id, int countryID, string name)
		{
			ID = id;
			CountryID = countryID;
			Name = name;
		}

		public Producer(SqlDataReader reader)
		{
			ID = reader.GetInt32(0);
			CountryID = reader.GetInt32(1);
			Name = reader.GetString(2);
		}

		public Producer(ProducerVM producerVM)
		{
			ID = producerVM.ID;
			CountryID = producerVM.Country.ID;
			Name = producerVM.Name;
		}

		public override string ToString()
		{
			return $"{nameof(Producer)}{{ ID: {ID}, CountryID: {CountryID}, Name: {Name} }}";
		}
	}
}
