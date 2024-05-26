using Supermarket.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.ViewModel.DataVM
{
	internal class CategoryVM
	{
		public int ID { get; set; }
		public string Name { get; set; }

		public CategoryVM()
		{
			ID = 0;
			Name = string.Empty;
		}

		public CategoryVM(int id, string name)
		{
			ID = id;
			Name = name;
		}

		public CategoryVM(Category category)
		{
			ID = category.ID;
			Name = category.Name;
		}

		public override string ToString()
		{
			return $"{nameof(CategoryVM)}{{ ID: {ID}, Name: {Name} }}";
		}
	}
}
