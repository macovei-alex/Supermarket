using System;
using Supermarket.Model.Entities;
using Supermarket.Model;

namespace Supermarket.ViewModel.DataVM
{
	public class MarketUserVM : BaseVM
	{
		private int _id;
		private string _name;
		private string _passwordHash;
		private string _type;
		public bool IsAdmin => Type == Cache.Instance.AdminStr;

		#region Properties

		public int ID
		{
			get => _id;
			set
			{
				_id = value;
				OnPropertyChanged(nameof(ID));
			}
		}

		public string Name
		{
			get => _name;
			set
			{
				_name = value;
				OnPropertyChanged(nameof(Name));
			}
		}

		public string PasswordHash
		{
			get => _passwordHash;
			set
			{
				_passwordHash = value;
				OnPropertyChanged(nameof(PasswordHash));
			}
		}

		public string Type
		{
			get => _type;
			set
			{
				_type = value;
				OnPropertyChanged(nameof(Type));
			}
		}

		#endregion

		public MarketUserVM()
		{
			_id = 0;
			_name = string.Empty;
			_passwordHash = string.Empty;
			_type = Cache.Instance.CashierStr;
		}

		public MarketUserVM(int id, string name, string passwordHash, string type)
		{
			ID = id;
			Name = name;
			PasswordHash = passwordHash;
			Type = type;
		}

		public MarketUserVM(MarketUser user)
		{
			ID = user.ID;
			Name = user.Name;
			PasswordHash = user.PasswordHash;
			var userType = Cache.Instance.UserTypes.Find((t) => t.ID == user.UserTypeID);
			if (userType == null)
			{
				Cache.Instance.Invalidate(Cache.CacheType.UserType);
				throw new ArgumentException($"User with type ID ( {user.UserTypeID} ) not found.");
			}
			Type = userType.Name;
		}

		public MarketUserVM(MarketUserVM other)
		{
			ID = other.ID;
			Name = other.Name;
			PasswordHash = other.PasswordHash;
			Type = other.Type;
		}

		public override string ToString()
		{
			return $"{nameof(MarketUserVM)}{{ ID: {ID}, Name: {Name}, PasswordHash: {PasswordHash}, Type: {Type} }}";
		}

		public static MarketUserVM DeepCopyOrNew(MarketUserVM original)
		{
			if (original == null)
			{
				return new MarketUserVM();
			}

			return new MarketUserVM(original.ID, original.Name, original.PasswordHash, original.Type);
		}

		public static bool IsAdminStr(string type)
		{
			return type == Cache.Instance.AdminStr;
		}
	}
}
