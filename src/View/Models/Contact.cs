namespace Contacts.Models
{
    // Represents a contact with basic information: name, phone number, and email.
    public class Contact
    {
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }

        // Default constructor.
        public Contact() { }

        // Parameterized constructor for initializing all properties.
        public Contact(string? name, string? phoneNumber, string? email)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            Email = email;
        }
    }
}