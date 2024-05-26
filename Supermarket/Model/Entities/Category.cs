using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Model.Entities
{
	internal class Category
	{
		public int ID { get; set; }
		public string Name { get; set; }

		public Category(int id, string name)
		{
			ID = id;
			Name = name;
		}

		public override string ToString()
		{
			return $"{nameof(Category)}{{ ID: {ID}, Name: {Name} }}";
		}
	}
}
