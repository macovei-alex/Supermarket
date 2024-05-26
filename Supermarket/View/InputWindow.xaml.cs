using System.Collections.Generic;
using System.Windows;
using Supermarket.ViewModel;

namespace Supermarket.View
{
	/// <summary>
	/// Interaction logic for InputWindow.xaml
	/// </summary>
	public partial class InputWindow : Window
	{
		public InputWindow(string title, List<string> textBlockNames)
		{
			InitializeComponent();
			DataContext = new InputWindowVM(title, textBlockNames);
		}

		private void SubmitButton_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
		}

		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = false;
		}
	}
}
