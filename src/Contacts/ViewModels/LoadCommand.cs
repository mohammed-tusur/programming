using System;
using System.Windows.Input; // For ICommand

namespace Contacts.ViewModels
{
    // Implementation of the ICommand pattern for the Load operation.
    public class LoadCommand : ICommand
    {
        private readonly MainViewModel _viewModel;

        // Constructor injects the MainViewModel instance.
        public LoadCommand(MainViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        // Event for notifying changes in the command's executability.
        public event EventHandler? CanExecuteChanged;

        // Determines whether the command can execute (always true in this case).
        public bool CanExecute(object? parameter) => true;

        // Executes the load operation by calling the Load method in MainViewModel.
        public void Execute(object? parameter)
        {
            _viewModel.Load();
        }

        // Raises the CanExecuteChanged event.
        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}