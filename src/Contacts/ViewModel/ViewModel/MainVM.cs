using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Model;
using Model.Services;

namespace ViewModel
{
    /// <summary>
    /// Main ViewModel class for the application.
    /// </summary>
    public class MainVM : ObservableObject
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
        /// Command to remove a contact.        
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
        /// Stores a collection of objects of class.
        /// </summary>
        private ObservableCollection<Contact> _contacts;

        /// <summary>
        /// Gets and sets the contacts collection.
        /// </summary>
        public ObservableCollection<Contact> Contacts
        {
            get
            {
                return _contacts;
            }
            set
            {
                _contacts = value;
                NotifyCanExecuteChanged();
            }
        }

        /// <summary>
        /// Returns and sets the ability to edit a contact.
        /// </summary>
        public bool IsReadOnly
        {
            get
            {
                return _isReadOnly;
            }
            set
            {
                _isReadOnly = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets and sets the visibility of the button.
        /// </summary>
        public bool IsVisibile
        {
            get
            {
                return _isVisibile;
            }
            private set
            {
                _isVisibile = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///  Returns and sets the selected contact.
        /// </summary>
        public Contact CurrentContact
        {
            get
            {
                return _currentContact;
            }
            set
            {
                if (value == null || _currentContact == null)
                {
                    IsReadOnly = true;
                    IsVisibile = false;
                    _currentContact = value;
                    OnPropertyChanged();
                    NotifyCanExecuteChanged();
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
                NotifyCanExecuteChanged();
            }
        }

        /// <summary>
        /// Confirms contact change.
        /// </summary>
        public RelayCommand ApplyCommand
        {
            get
            {
                if (_applyCommand != null)
                {
                    return _applyCommand;
                }
                else
                {
                    Action action = delegate
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
                        NotifyCanExecuteChanged();
                    };
                    _applyCommand = new RelayCommand(action);
                    return _applyCommand;
                }
            }
        }

        /// <summary>
        /// Adds a contact.
        /// </summary>
        public RelayCommand AddCommand
        {
            get
            {
                if (_addCommand != null)
                {
                    return _addCommand;
                }
                else
                {
                    Action action = delegate
                    {
                        Contact contact = new Contact();
                        CurrentContact = contact;
                        IsReadOnly = false;
                        IsVisibile = true;
                    };
                    Func<bool> func = delegate { return IsReadOnly; };
                    _addCommand = new RelayCommand(action, func);
                    return _addCommand;
                }
            }
        }

        /// <summary>
        /// Edits a contact.
        /// </summary>
        public RelayCommand EditCommand
        {
            get
            {
                if (_editCommand != null)
                {
                    return _editCommand;
                }
                else
                {
                    Action action = delegate
                    {
                        CopyContact = CurrentContact;
                        CurrentContact = null;
                        CurrentContact = new Contact(CopyContact);
                        IsReadOnly = false;
                        IsEdit = true;
                        IsVisibile = true;
                    };
                    Func<bool> func = delegate
                    {
                        return (Contacts.Count > 0 && CurrentContact != null &&
                              Contacts.IndexOf(CurrentContact) != -1 && IsReadOnly);
                    };
                    _editCommand = new RelayCommand(action, func);
                    return _editCommand;
                }
            }
        }

        /// <summary>
        /// Deletes a contact.
        /// </summary>
        public RelayCommand RemoveCommand
        {
            get
            {
                if (_removeCommand != null)
                {
                    return _removeCommand;
                }
                else
                {
                    Action action = delegate
                    {
                        int index = Contacts.IndexOf(CurrentContact);
                        Contacts.Remove(CurrentContact);
                        if (Contacts.Count != 0)
                        {
                            if (index == Contacts.Count)
                            {
                                CurrentContact = Contacts[index - 1];
                            }
                            else
                            {
                                CurrentContact = Contacts[index];
                            }
                        }
                        ContactSerializer.SaveData(Contacts);
                    };
                    Func<bool> func = delegate
                    {
                        return (Contacts.Count > 0 && CurrentContact != null &&
                              Contacts.IndexOf(CurrentContact) != -1 && IsReadOnly);
                    };
                    _removeCommand = new RelayCommand(action, func);
                    return _removeCommand;
                }
            }
        }

        /// <summary>
        /// A constructor creates an object of the class MainVM
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
        /// Fires events of change of command execution conditions.
        /// </summary>
        public void NotifyCanExecuteChanged()
        {
            AddCommand.NotifyCanExecuteChanged();
            EditCommand.NotifyCanExecuteChanged();
            RemoveCommand.NotifyCanExecuteChanged();
            ApplyCommand.NotifyCanExecuteChanged();
        }
    }
}
