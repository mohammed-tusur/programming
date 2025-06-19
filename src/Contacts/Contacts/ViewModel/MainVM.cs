using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using View.Model;
using View.Model.Services;

namespace View.ViewModel
{
    /// <summary>
    /// Main ViewModel class for the application.
    /// </summary>
    public class MainVM : INotifyPropertyChanged
    {
        /// <summary>
        /// Currently selected contact.
        /// </summary>
        private Contact _currentContact;

        /// <summary>
        /// Command to apply changes.
        /// </summary>
        private RelayCommand _applyCommand;

        /// <summary>
        /// Command to add a new contact.
        /// </summary>
        private RelayCommand _addCommand;

        /// <summary>
        /// Command to remove a contact.
        /// </summary>
        private RelayCommand _removeCommand;

        /// <summary>
        /// Command to edit a contact.
        /// </summary>
        private RelayCommand _editCommand;

        /// <summary>
        /// Determines if editing is allowed.
        /// </summary>
        private bool _isReadOnly;

        /// <summary>
        /// Determines button visibility.
        /// </summary>
        private bool _isVisibile;

        /// <summary>
        /// Gets or sets the edit state of the object.
        /// </summary>
        public bool IsEdit { get; set; }

        /// <summary>
        /// Gets or sets a copy of the contact being edited.
        /// </summary>
        public Contact CopyContact { get; set; }

        /// <summary>
        /// Gets or sets the collection of contacts.
        /// </summary>
        public ObservableCollection<Contact> Contacts { get; set; }

        /// <summary>
        /// Gets or sets whether the contact is read-only.
        /// </summary>
        public bool IsReadOnly
        {
            get => _isReadOnly;
            set
            {
                _isReadOnly = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the button visibility state.
        /// </summary>
        public bool IsVisibile
        {
            get => _isVisibile;
            private set
            {
                _isVisibile = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the currently selected contact.
        /// </summary>
        public Contact CurrentContact
        {
            get => _currentContact;
            set
            {
                if (value == null || _currentContact == null)
                {
                    IsReadOnly = true;
                    IsVisibile = false;
                    _currentContact = value;
                    OnPropertyChanged();
                    return;
                }
                if (IsEdit && CurrentContact.Name != CopyContact.Name &&
                    CurrentContact.Phone != CopyContact.Phone &&
                    CurrentContact.Email != CopyContact.Email)
                {
                    IsEdit = false;
                    CurrentContact = null;
                    CurrentContact = CopyContact;
                }
                IsReadOnly = true;
                IsVisibile = false;
                _currentContact = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Command to confirm contact changes.
        /// </summary>
        public RelayCommand ApplyCommand
        {
            get
            {
                return _applyCommand ??= new RelayCommand(_ =>
                {
                    if (IsEdit)
                    {
                        IsEdit = false;
                        CopyContact.Name = CurrentContact.Name;
                        CopyContact.Phone = CurrentContact.Phone;
                        CopyContact.Email = CurrentContact.Email;
                        CurrentContact = CopyContact;
                        CopyContact = null;
                    }
                    else
                    {
                        Contacts.Add(CurrentContact);
                    }
                    IsReadOnly = true;
                    IsVisibile = false;
                    ContactSerializer.SaveData(Contacts);
                });
            }
        }

        /// <summary>
        /// Command to add a new contact.
        /// </summary>
        public RelayCommand AddCommand
        {
            get
            {
                return _addCommand ??= new RelayCommand(
                    _ => {
                        Contact contact = new Contact();
                        CurrentContact = contact;
                        IsReadOnly = false;
                        IsVisibile = true;
                    },
                    _ => IsReadOnly
                );
            }
        }

        /// <summary>
        /// Command to edit a contact.
        /// </summary>
        public RelayCommand EditCommand
        {
            get
            {
                return _editCommand ??= new RelayCommand(
                    _ => {
                        CopyContact = CurrentContact;
                        CurrentContact = null;
                        CurrentContact = new Contact(CopyContact);
                        IsReadOnly = false;
                        IsEdit = true;
                        IsVisibile = true;
                    },
                    _ => (Contacts.Count > 0 && CurrentContact != null &&
                         Contacts.IndexOf(CurrentContact) != -1 && IsReadOnly)
                );
            }
        }

        /// <summary>
        /// Command to remove a contact.
        /// </summary>
        public RelayCommand RemoveCommand
        {
            get
            {
                return _removeCommand ??= new RelayCommand(
                    _ => {
                        int index = Contacts.IndexOf(CurrentContact);
                        Contacts.Remove(CurrentContact);
                        if (Contacts.Count != 0)
                        {
                            CurrentContact = index == Contacts.Count ?
                                Contacts[index - 1] :
                                Contacts[index];
                        }
                        ContactSerializer.SaveData(Contacts);
                    },
                    _ => (Contacts.Count > 0 && CurrentContact != null &&
                         Contacts.IndexOf(CurrentContact) != -1 && IsReadOnly)
                );
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MainVM"/> class.
        /// </summary>
        public MainVM()
        {
            Contacts = new ObservableCollection<Contact>();
            Contacts = ContactSerializer.LoadData();
            IsReadOnly = true;
            IsEdit = false;
            IsVisibile = false;
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Notifies bound objects that a property has changed.
        /// </summary>
        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}