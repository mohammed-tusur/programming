using Contacts.Models;
using Contacts.Models.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace Contacts.ViewModels
{
    // Handles contact list operations and UI state
    public class MainViewModel : INotifyPropertyChanged
    {
        private Contact? _selectedContact;
        private Contact _editingContact;
        private bool _isEditing;

        public ObservableCollection<Contact> Contacts { get; } = new ObservableCollection<Contact>();

        // Currently selected contact (cancels edits when changed)
        public Contact? SelectedContact
        {
            get => _selectedContact;
            set
            {
                if (_selectedContact != value)
                {
                    CancelEdit();
                    _selectedContact = value;
                    OnPropertyChanged(nameof(SelectedContact));
                    OnPropertyChanged(nameof(IsEditEnabled));
                    OnPropertyChanged(nameof(IsRemoveEnabled));
                }
            }
        }

        // Temporary contact during edits
        public Contact? EditingContact
        {
            get => _editingContact;
            set
            {
                _editingContact = value;
                OnPropertyChanged(nameof(EditingContact));
            }
        }

        // UI state properties
        public bool IsReadOnly => !_isEditing;
        public bool IsApplyVisible => _isEditing;
        public bool IsEditEnabled => SelectedContact != null && !_isEditing;
        public bool IsRemoveEnabled => SelectedContact != null && !_isEditing;

        // Commands
        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand RemoveCommand { get; }
        public ICommand ApplyCommand { get; }

        public MainViewModel()
        {
            // Load saved contacts
            foreach (var contact in ContactSerializer.LoadContacts())
            {
                Contacts.Add(contact);
            }

            AddCommand = new RelayCommand(_ => AddContact());
            EditCommand = new RelayCommand(_ => EditContact(), _ => IsEditEnabled);
            RemoveCommand = new RelayCommand(_ => RemoveContact(), _ => IsRemoveEnabled);
            ApplyCommand = new RelayCommand(_ => ApplyChanges());
        }

        private void AddContact()
        {
            _isEditing = true;
            EditingContact = new Contact(); // Start with empty contact
            SelectedContact = null;
            UpdateState();
        }

        private void EditContact()
        {
            if (SelectedContact != null)
            {
                _isEditing = true;
                // Create editable copy
                EditingContact = new Contact
                {
                    Name = SelectedContact.Name,
                    PhoneNumber = SelectedContact.PhoneNumber,
                    Email = SelectedContact.Email
                };
                UpdateState();
            }
        }

        private void RemoveContact()
        {
            if (SelectedContact != null)
            {
                int index = Contacts.IndexOf(SelectedContact);
                Contacts.Remove(SelectedContact);

                // Select next contact or clear if empty
                SelectedContact = Contacts.Count > 0
                    ? Contacts[Math.Min(index, Contacts.Count - 1)]
                    : null;

                SaveContacts();
            }
        }

        private void ApplyChanges()
        {
            if (!_isEditing || EditingContact == null || string.IsNullOrWhiteSpace(EditingContact.Name))
                return;

            if (SelectedContact == null) // Add new
            {
                var newContact = new Contact
                {
                    Name = EditingContact.Name,
                    PhoneNumber = EditingContact.PhoneNumber,
                    Email = EditingContact.Email
                };
                Contacts.Add(newContact);
                SelectedContact = newContact;
            }
            else // Update existing
            {
                SelectedContact.Name = EditingContact.Name;
                SelectedContact.PhoneNumber = EditingContact.PhoneNumber;
                SelectedContact.Email = EditingContact.Email;
            }

            _isEditing = false;
            EditingContact = null;
            UpdateState();
            SaveContacts();
        }

        private void CancelEdit()
        {
            _isEditing = false;
            EditingContact = null;
            UpdateState();
        }

        private void UpdateState()
        {
            OnPropertyChanged(nameof(IsReadOnly));
            OnPropertyChanged(nameof(IsApplyVisible));
            OnPropertyChanged(nameof(IsEditEnabled));
            OnPropertyChanged(nameof(IsRemoveEnabled));
        }

        private void SaveContacts() => ContactSerializer.SaveContacts(Contacts.ToList());

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}