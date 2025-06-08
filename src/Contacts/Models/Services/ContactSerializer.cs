using System;
using System.IO;
using Newtonsoft.Json; // For serialization
using Contacts.Models;

namespace Contacts.Services
{
    // Handles serialization and deserialization of Contact objects to/from a JSON file.
    public class ContactSerializer
    {
        private readonly string _filePath;

        // Constructor initializes the file path for storing contacts.
        public ContactSerializer()
        {
            // Gets the path to the "My Documents" folder.
            var myDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            // Creates a "Contacts" directory inside "My Documents".
            var contactsDir = Path.Combine(myDocuments, "Contacts");
            Directory.CreateDirectory(contactsDir);

            // Specifies the file path for the contacts JSON file.
            _filePath = Path.Combine(contactsDir, "contacts.json");
        }

        // Saves the contact object to a JSON file.
        public void SaveContact(Contact contact)
        {
            // Serializes the contact object to a JSON string with indentation for readability.
            string json = JsonConvert.SerializeObject(contact, Formatting.Indented);

            // Writes the JSON string to the file.
            File.WriteAllText(_filePath, json);
        }

        // Loads the contact object from the JSON file.
        public Contact LoadContact()
        {
            // Checks if the file exists; if not, returns a new Contact object.
            if (!File.Exists(_filePath))
            {
                return new Contact();
            }

            // Reads the JSON content from the file.
            string json = File.ReadAllText(_filePath);

            // Deserializes the JSON string into a Contact object.
            return JsonConvert.DeserializeObject<Contact>(json) ?? new Contact();
        }
    }
}