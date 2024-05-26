using Supermarket.Model;
using Supermarket.Model.BusinessLogic;
using Supermarket.Utilities;
using Supermarket.View.MainWindow;
using Supermarket.ViewModel.Commands;
using Supermarket.ViewModel.DataVM;
using System.Windows.Forms;

namespace Supermarket.ViewModel.MainWindowVMs
{
	public class LoginPageVM : BaseVM
	{
		#region Properties

		private string _username;
		public string Username
		{
			get => _username;
			set
			{
				_username = value;
				OnPropertyChanged(nameof(Username));
			}
		}

		private string _password;
		public string Password
		{
			get => _password;
			set
			{
				_password = value;
				OnPropertyChanged(nameof(Password));
			}
		}

		private bool _loginAsAdmin;
		public bool LoginAsAdmin
		{
			get => _loginAsAdmin;
			set
			{
				_loginAsAdmin = value;
				OnPropertyChanged(nameof(LoginAsAdmin));
			}
		}

		#endregion

		public RelayCommand<string> SetLoginModeCommand { get; }
		public RelayCommandVoid LoginCommand { get; }

		public LoginPageVM()
		{
			LoginAsAdmin = true;
			Username = string.Empty;
			Password = string.Empty;

			SetLoginModeCommand = new RelayCommand<string>((content) => LoginAsAdmin = content.ToString().ToLower() == Cache.Instance.AdminStr);
			LoginCommand = new RelayCommandVoid(Login);
		}

		public void Login()
		{
			MarketUserVM user = MarketUserBL.Login(Username, Password, LoginAsAdmin);
			if (user == null)
			{
				MessageBox.Show($"There is no {(LoginAsAdmin ? Cache.Instance.AdminStr : Cache.Instance.CashierStr)} with username ( {Username} ), password ( {Password} )");
				return;
			}

			LocalStorage.CurrentUser = user;
			LocalStorage.WindowVM.ChangePage(nameof(MainMenuPage));
		}
	}
}
