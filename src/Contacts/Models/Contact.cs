
using System.ComponentModel;

namespace Contacts.Models
{
    public class Contact : INotifyPropertyChanged // Contact model implementing INotifyPropertyChanged for data binding
    {
        // Backing fields
        private string? _name;
        private string? _phoneNumber;
        private string? _email;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string? PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                _phoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }

        public string? Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        // Property change notification event
        public event PropertyChangedEventHandler? PropertyChanged;

        /// Raises PropertyChanged event
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}