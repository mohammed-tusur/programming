using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Model
{
    /// <summary>
    /// Stores and returns contact information.
    /// </summary>
    public class Contact : INotifyPropertyChanged, IDataErrorInfo
    {
        /// <summary>
        /// Name
        /// </summary>
        private string _name { get; set; }

        /// <summary>
        /// Phone number
        /// </summary>
        private string _phone { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        private string _email { get; set; }
        
        /// <summary>
        /// Returns and creates name.
        /// </summary>
        public string Name 
        {
            get 
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Rerturns and creates a Phone number.
        /// </summary>
        public string Phone
        {
            get
            {
                return _phone;
            }
            set
            {
                _phone = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Returns and creates email.
        /// </summary>
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Returns the error text.
        /// </summary>
        public string Error => "";

        /// <summary>
        /// Returns the error text.
        /// </summary>
        public string this[string propertyName]
        {
            get
            {
                string error = string.Empty;
                switch (propertyName)
                {
                    case nameof(Contact.Name):
                        {
                            if (Name == null || Name.Length == 0 || Name.Length > 100)
                            {
                                error = "Name should not be more than 100 characters";
                            }
                            break;
                        }
                    case nameof(Contact.Phone):
                        {
                            if (Phone == null || Phone.Length == 0 || Phone.Length > 100)
                            {
                                error = "Phone number must be no longer than 100 characters and can only contain digits or +-() symbols";
                            }
                            break;
                        }
                    case nameof(Contact.Email):
                        {
                            if (Email == null || Email.Length == 0 || Email.Length > 100 || !Email.Contains("@"))
                            {
                                error = "Email must be no longer than 100 characters and must contain @ symbol";
                            }
                            break;
                        }
                }
                return error;
            }
        }

        /// <summary>
        ///  Creates an instance of the class without initialization.
        /// </summary>
        public Contact()
        {

        }

        /// <summary>
        /// Creates an instance of the class.
        /// </summary>
        public Contact(string name, string phone, string email)
        {
            Name = name;
            Phone = phone;
            Email = email;
        }

        /// <summary>
        /// Creates a copy of the class instance.
        /// </summary>
        public Contact(Contact contact)
        {
            this.Name = contact.Name;
            this.Phone = contact.Phone;
            this.Email = contact.Email;
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;


        /// <summary>
        /// Updates bound objects.
        /// </summary>
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
