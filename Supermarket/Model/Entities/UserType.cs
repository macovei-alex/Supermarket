namespace Supermarket.Model.Entities
{
	internal class UserType
	{
		public int ID { get; set; }
		public string Name { get; set; }

		public UserType(int id, string name)
		{
			ID = id;
			Name = name;
		}

		public override string ToString()
		{
			return $"{nameof(UserType)}{{ ID: {ID}, Name: {Name} }}";
		}
	}
}
