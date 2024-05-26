using Supermarket.ViewModel.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Supermarket.ViewModel
{
	internal class InputWindowVM : BaseVM
	{
		#region Properties

		private string _title;
		public string Title
		{
			get => _title;
			set
			{
				_title = value;
				OnPropertyChanged(nameof(Title));
			}
		}

		private ObservableCollection<Input> _inputCollection;
		public ObservableCollection<Input> InputCollection
		{
			get => _inputCollection;
			set
			{
				_inputCollection = value;
				OnPropertyChanged(nameof(InputCollection));
			}
		}

		#endregion

		public InputWindowVM()
		{
			// For xaml designer
		}

		public InputWindowVM(string title, List<string> textBlockNames)
		{
			Title = title;

			InputCollection = new ObservableCollection<Input>();
			foreach (var name in textBlockNames)
			{
				InputCollection.Add(new Input { Label = name, Value = string.Empty });
			}
		}
	}
}
