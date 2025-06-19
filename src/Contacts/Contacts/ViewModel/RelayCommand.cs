using System;
using System.Windows.Input;

namespace View.ViewModel
{
    /// <summary>
    /// Stores and executes commands.
    /// </summary>
    public class RelayCommand : ICommand
    {
        private Action<object> _execute;
        private Func<object, bool> _canExecute;

        /// <summary>
        /// Occurs when changes occur that affect whether the command should execute.
        /// </summary>
        public event EventHandler? CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public bool CanExecute(object? parameter)
        {
            return this._canExecute == null || this._canExecute(parameter);
        }

        public void Execute(object? parameter)
        {
            this._execute(parameter);
        }

        /// <summary>
        /// Creates a new instance of the RelayCommand class.
        /// </summary>
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this._execute = execute;
            this._canExecute = canExecute;
        }
    }
}