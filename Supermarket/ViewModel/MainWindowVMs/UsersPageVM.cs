using Supermarket.Model;
using Supermarket.Model.BusinessLogic;
using Supermarket.Utilities;
using Supermarket.Utils;
using Supermarket.ViewModel.Commands;
using Supermarket.ViewModel.DataVM;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Supermarket.ViewModel.MainWindowVMs
{
	public class UsersPageVM : BaseVM
	{
		#region Properties

		public ObservableCollection<MarketUserVM> Users { get; private set; }
		public List<string> UserTypes { get; }

		private MarketUserVM _selectedUser;
		public MarketUserVM SelectedUser
		{
			get => _selectedUser;
			set
			{
				_selectedUser = value;
				OnPropertyChanged(nameof(SelectedUser));

				NewName = value?.Name ?? string.Empty;
				NewPassword = string.Empty;
				NewPasswordConfirmation = string.Empty;
				NewType = value?.Type ?? string.Empty;
			}
		}

		private string _newPassword;
		public string NewPassword
		{
			get => _newPassword;
			set
			{
				_newPassword = value;
				OnPropertyChanged(nameof(NewPassword));
			}
		}

		private string _newPasswordConfirmation;
		public string NewPasswordConfirmation
		{
			get => _newPasswordConfirmation;
			set
			{
				_newPasswordConfirmation = value;
				OnPropertyChanged(nameof(NewPasswordConfirmation));
			}
		}

		private string _newName;
		public string NewName
		{
			get => _newName;
			set
			{
				_newName = value;
				OnPropertyChanged(nameof(NewName));
			}
		}

		private string _newType;
		public string NewType
		{
			get => _newType;
			set
			{
				_newType = value;
				OnPropertyChanged(nameof(NewType));
			}
		}

		#endregion

		public ICommand AddCommand { get; }
		public ICommand EditCommand { get; }
		public ICommand DeleteCommand { get; }

		public UsersPageVM()
		{
			Users = MarketUserBL.GetAllUsers();
			UserTypes = new List<string> { Cache.Instance.AdminStr, Cache.Instance.CashierStr };

			AddCommand = new RelayCommandVoid(Add, () => LocalStorage.CurrentUser.IsAdmin);
			EditCommand = new RelayCommandVoid(Edit, () => LocalStorage.CurrentUser.IsAdmin);
			DeleteCommand = new RelayCommandVoid(Delete, () => LocalStorage.CurrentUser.IsAdmin);
		}

		private void Add()
		{
			if (!Functions.AreNotNullOrEmpty(NewName, NewPassword, NewPasswordConfirmation))
			{
				Functions.LogError("Please make sure the username, the password and the password confirmation are not empty");
				return;
			}

			if (NewPassword != NewPasswordConfirmation)
			{
				Functions.LogError("Passwords do not match");
				return;
			}

			if (MarketUserBL.CreateUser(NewName, NewPassword, MarketUserVM.IsAdminStr(NewType)))
			{
				Functions.LogInfo($"Successfuly added user ( {NewName} )");
				Users.RepopulateFrom(MarketUserBL.GetAllUsers());
			}
		}

		private void Edit()
		{
			if (SelectedUser == null)
			{
				Functions.LogError("Please select a user to edit first");
				return;
			}

			if (!Functions.AreNotNullOrEmpty(NewName))
			{
				Functions.LogError("Please make sure the username is not empty");
				return;
			}

			if (NewPassword != NewPasswordConfirmation)
			{
				Functions.LogError("Passwords do not match");
				return;
			}

			if (MarketUserBL.EditUser(SelectedUser.ID, NewName, SelectedUser.PasswordHash, NewPassword, MarketUserVM.IsAdminStr(NewType)))
			{
				Functions.LogInfo($"Successfuly edited user ( {NewName} )");
				Users.RepopulateFrom(MarketUserBL.GetAllUsers());
			}
		}

		private void Delete()
		{
			if (!Functions.AreNotNullOrEmpty(NewName))
			{
				Functions.LogError("Please make sure the username is not empty");
				return;
			}

			var result = MessageBox.Show($"Are you sure you want to delete the user ( {NewName} )?", "Delete user", MessageBoxButton.YesNo);
			if (result != MessageBoxResult.Yes)
			{
				return;
			}

			if (MarketUserBL.DeleteUser(NewName))
			{
				Functions.LogInfo($"Successfuly deleted user ( {NewName} )");
				Users.RepopulateFrom(MarketUserBL.GetAllUsers());
			}
		}
	}
}
