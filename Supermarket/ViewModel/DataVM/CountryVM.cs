using Supermarket.Model.Entities;

namespace Supermarket.ViewModel.DataVM
{
	internal class CountryVM
	{
		public int ID { get; set; }
		public string Name { get; set; }

		public CountryVM()
		{
			ID = 0;
			Name = string.Empty;
		}

		public CountryVM(int id, string name)
		{
			ID = id;
			Name = name;
		}

		public CountryVM(Country country)
		{
			ID = country.ID;
			Name = country.Name;
		}

		public override string ToString()
		{
			return $"{nameof(CountryVM)}{{ ID: {ID}, Name: {Name} }}";
		}
	}
}
