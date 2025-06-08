using System.ComponentModel;
using System.Runtime.CompilerServices;
using Contacts.Models;
using Contacts.Services;
using System.Windows.Input; // For ICommand

namespace Contacts.ViewModels
{
    // The main view model for the application, implementing INotifyPropertyChanged for property change notifications.
    public class MainViewModel : INotifyPropertyChanged
    {
        private Contact _currentContact;
        private readonly ContactSerializer _serializer;

        // Event for property change notifications.
        public event PropertyChangedEventHandler? PropertyChanged;

        // Constructor initializes the current contact and serializer.
        public MainViewModel()
        {
            _currentContact = new Contact();
            _serializer = new ContactSerializer();
            SaveCommand = new SaveCommand(this);
            LoadCommand = new LoadCommand(this);
        }

        // Property for binding the contact's name.
        public string? Name
        {
            get => _currentContact.Name;
            set
            {
                if (_currentContact.Name != value)
                {
                    _currentContact.Name = value;
                    OnPropertyChanged();
                }
            }
        }

        // Property for binding the contact's phone number.
        public string? PhoneNumber
        {
            get => _currentContact.PhoneNumber;
            set
            {
                if (_currentContact.PhoneNumber != value)
                {
                    _currentContact.PhoneNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        // Property for binding the contact's email.
        public string? Email
        {
            get => _currentContact.Email;
            set
            {
                if (_currentContact.Email != value)
                {
                    _currentContact.Email = value;
                    OnPropertyChanged();
                }
            }
        }

        // Command for saving the contact.
        public ICommand SaveCommand { get; }

        // Command for loading the contact.
        public ICommand LoadCommand { get; }

        // Method to save the current contact to a file.
        internal void Save()
        {
            _serializer.SaveContact(_currentContact);
        }

        // Method to load the contact from a file.
        internal void Load()
        {
            var loadedContact = _serializer.LoadContact();
            Name = loadedContact.Name;
            PhoneNumber = loadedContact.PhoneNumber;
            Email = loadedContact.Email;
        }

        // Raises the PropertyChanged event for the specified property.
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Raises the CanExecuteChanged event for the commands.
        public void RaiseCommandsCanExecute()
        {
            ((SaveCommand)SaveCommand).RaiseCanExecuteChanged();
            ((LoadCommand)LoadCommand).RaiseCanExecuteChanged();
        }
    }
}