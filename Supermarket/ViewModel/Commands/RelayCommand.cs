using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Supermarket.ViewModel.Commands
{
	public class RelayCommand<T> : ICommand
	{
		private readonly Action<T> execute;
		private readonly Predicate<T> canExecute;

#pragma warning disable CS0067
		public event EventHandler CanExecuteChanged;
#pragma warning restore CS0067

		public RelayCommand(Action<T> execute, Predicate<T> canExecute)
		{
			this.execute = execute;
			this.canExecute = canExecute;
		}

		public RelayCommand(Action<T> execute)
		{
			this.execute = execute;
			this.canExecute = (x) => true;
		}

		public bool CanExecute(object parameter)
		{
			return canExecute((T)parameter);
		}

		public void Execute(object parameter)
		{
			execute((T)parameter);
		}
	}
}
