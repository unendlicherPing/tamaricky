using System;
using System.Windows.Input;

namespace tamaricky.Core
{
    class RelayCommand : ICommand
    {
        private Action<object> _execute;
        private Predicate<object> _canExecute;

        public RelayCommand(Predicate<object> canExecute, Action<object> execute) =>
            (_canExecute, _execute) = (canExecute, execute);

        public RelayCommand(Action<object> execute) : this(null, execute) { }

        public event EventHandler CanExecuteChanged;
        public void RaiseExecuteChanged() => this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        public bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? true;

        public void Execute(object parameter) => _execute?.Invoke(parameter);
    }
}
