using Supermarket.Model.DataAccess.EntityDALs;
using Supermarket.Model.Entities;
using Supermarket.Utilities;
using Supermarket.ViewModel.DataVM;
using System.Collections.ObjectModel;

namespace Supermarket.Model.BusinessLogic
{
	internal static class CategoryBL
	{
		public static ObservableCollection<CategoryVM> GetAllCategories()
		{
			var categories = new ObservableCollection<CategoryVM>();
			foreach (Category category in CategoryDAL.GetAllCategories())
			{
				categories.Add(new CategoryVM(category));
			}
			return categories;
		}

		public static bool CreateCategory(string name)
		{
			string message = CategoryDAL.CreateCategory(name);
			if (message != Cache.Instance.SuccessMessage)
			{
				Functions.LogError(message);
				return false;
			}

			Cache.Instance.Invalidate(Cache.CacheType.Category);
			return true;
		}

		public static bool EditCategory(string oldName, string newName)
		{
			var category = Cache.Instance.Categories.Find((c) => c.Name == oldName);
			if (category == null)
			{
				Functions.LogError($"There is no category with name ( {oldName} ).");
				return false;
			}

			return EditCategory(category.ID, newName);
		}

		public static bool EditCategory(int id, string newName)
		{
			string message = CategoryDAL.EditCategory(id, newName);
			if (message != Cache.Instance.SuccessMessage)
			{
				Functions.LogError(message);
				return false;
			}

			Cache.Instance.Invalidate(Cache.CacheType.Category);
			return true;
		}

		public static bool DeleteCategory(string name)
		{
			var category = Cache.Instance.Categories.Find((c) => c.Name == name);
			if (category == null)
			{
				Functions.LogError($"There is no category with name ( {name} ).");
				return false;
			}

			return DeleteCategory(category.ID);
		}

		public static bool DeleteCategory(int id)
		{
			string message = CategoryDAL.DeleteCategory(id);
			if (message != Cache.Instance.SuccessMessage)
			{
				Functions.LogError(message);
				return false;
			}

			Cache.Instance.Invalidate(Cache.CacheType.Category);
			return true;
		}
	}
}
