using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Supermarket.ViewModel.Commands
{
	public class RelayCommandVoid : ICommand
	{
		private readonly Action execute;
		private readonly Func<bool> canExecute;

#pragma warning disable CS0067
		public event EventHandler CanExecuteChanged;
#pragma warning restore CS0067

		public RelayCommandVoid(Action execute)
		{
			this.execute = execute;
			this.canExecute = () => true;
		}

		public RelayCommandVoid(Action execute, Func<bool> canExecute)
		{
			this.execute = execute;
			this.canExecute = canExecute;
		}

		public bool CanExecute(object parameter)
		{
			return canExecute();
		}

		public void Execute(object parameter)
		{
			execute();
		}
	}
}
