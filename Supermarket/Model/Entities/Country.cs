namespace Supermarket.Model.Entities
{
	internal class Country
	{
		public int ID { get; set; }
		public string Name { get; set; }

		public Country(int id, string name)
		{
			ID = id;
			Name = name;
		}

		public override string ToString()
		{
			return $"{nameof(Country)}{{ ID: {ID}, Name: {Name} }}";
		}
	}
}
