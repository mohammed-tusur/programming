using System;
using System.Windows.Input; // For ICommand

namespace Contacts.ViewModels
{
    // Implementation of the ICommand pattern for the Save operation.
    public class SaveCommand : ICommand
    {
        private readonly MainViewModel _viewModel;

        // Constructor injects the MainViewModel instance.
        public SaveCommand(MainViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        // Event for notifying changes in the command's executability.
        public event EventHandler? CanExecuteChanged;

        // Determines whether the command can execute (always true in this case).
        public bool CanExecute(object? parameter) => true;

        // Executes the save operation by calling the Save method in MainViewModel.
        public void Execute(object? parameter)
        {
            _viewModel.Save();
        }

        // Raises the CanExecuteChanged event.
        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}