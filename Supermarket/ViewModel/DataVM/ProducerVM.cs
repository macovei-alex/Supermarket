using Supermarket.Model;
using Supermarket.Model.Entities;

namespace Supermarket.ViewModel.DataVM
{
	internal class ProducerVM
	{
		public int ID { get; set; }
		public CountryVM Country { get; set; }
		public string Name { get; set; }

		public ProducerVM()
		{
			ID = 0;
			Country = new CountryVM();
			Name = string.Empty;
		}

		public ProducerVM(int id, CountryVM country, string name)
		{
			ID = id;
			Country = country;
			Name = name;
		}

		public ProducerVM(Producer producer)
		{
			ID = producer.ID;
			Country = new CountryVM(Cache.Instance.Countries.Find((c) => c.ID == producer.CountryID));
			Name = producer.Name;
		}

		public override string ToString()
		{
			return $"{nameof(Producer)}{{ ID: {ID}, Country: {Country}, Name: {Name} }}";
		}
	}
}
