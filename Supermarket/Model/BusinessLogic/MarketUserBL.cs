using Supermarket.Model.DataAccess.EntityDALs;
using Supermarket.Model.Entities;
using Supermarket.Utilities;
using Supermarket.ViewModel.DataVM;
using System.Collections.ObjectModel;

namespace Supermarket.Model.BusinessLogic
{
	internal class MarketUserBL
	{
		public static MarketUserVM Login(string name, string password, bool isAdmin)
		{
			var user = MarketUserDAL.FindUser(name, Functions.HashString(password), isAdmin);
			return user == null ? null : new MarketUserVM(user);
		}

		public static ObservableCollection<MarketUserVM> GetAllUsers()
		{
			var usersVM = new ObservableCollection<MarketUserVM>();
			foreach (MarketUser userModel in MarketUserDAL.GetAllUsers())
			{
				usersVM.Add(new MarketUserVM(userModel));
			}
			return usersVM;
		}

		public static ObservableCollection<MarketUserVM> GetActiveUsers()
		{
			var usersVM = new ObservableCollection<MarketUserVM>();
			foreach (MarketUser userModel in MarketUserDAL.GetActiveUsers())
			{
				usersVM.Add(new MarketUserVM(userModel));
			}
			return usersVM;
		}

		public static bool CreateUser(string name, string password, bool isAdmin)
		{
			string message = MarketUserDAL.CreateUser(name, Functions.HashString(password), isAdmin ? Cache.Instance.AdminID : Cache.Instance.CashierID);
			if (message != Cache.Instance.SuccessMessage)
			{
				Functions.LogError(message);
				return false;
			}

			Cache.Instance.Invalidate(Cache.CacheType.User);
			Cache.Instance.Invalidate(Cache.CacheType.ActiveUser);
			return true;
		}

		public static bool DeleteUser(int id)
		{
			string message = MarketUserDAL.DeleteUser(id);
			if (message != Cache.Instance.SuccessMessage)
			{
				Functions.LogError(message);
				return false;
			}

			Cache.Instance.Invalidate(Cache.CacheType.User);
			Cache.Instance.Invalidate(Cache.CacheType.ActiveUser);
			return true;
		}

		public static bool DeleteUser(string name)
		{
			string message = MarketUserDAL.DeleteUserByName(name);
			if (message != Cache.Instance.SuccessMessage)
			{
				Functions.LogError(message);
				return false;
			}

			Cache.Instance.Invalidate(Cache.CacheType.User);
			Cache.Instance.Invalidate(Cache.CacheType.ActiveUser);
			return true;
		}

		public static bool EditUser(int id, string name, string oldPasswordHash, string newPasswordString, bool isAdmin)
		{
			if (newPasswordString == null || newPasswordString == string.Empty)
			{
				newPasswordString = oldPasswordHash;
			}

			string message = MarketUserDAL.EditUser(id, name, oldPasswordHash, Functions.HashString(newPasswordString), isAdmin ? Cache.Instance.AdminID : Cache.Instance.CashierID);
			if (message != Cache.Instance.SuccessMessage)
			{
				Functions.LogError(message);
				return false;
			}

			Cache.Instance.Invalidate(Cache.CacheType.User);
			Cache.Instance.Invalidate(Cache.CacheType.ActiveUser);
			return true;
		}
	}
}
