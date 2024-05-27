using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Supermarket.Utilities;
using Supermarket.Model.Entities;
using Supermarket.Model.DataAccess;
using System.Collections.ObjectModel;
using Supermarket.Model;
using Supermarket.ViewModel;
using Supermarket.ViewModel.MainWindowVMs;
using Supermarket.Utils;

namespace Supermarket.View.MainWindow
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		public void MainWindow_Loaded(object sender, RoutedEventArgs e)
		{
			LocalStorage.WindowVM = DataContext as MainWindowVM;
		}
	}
}
