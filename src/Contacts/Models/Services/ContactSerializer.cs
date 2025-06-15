using Contacts.Models;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Contacts.Models.Services
{
    public static class ContactSerializer
    {
        private static readonly string FilePath = "contacts.xml";

        // Loads contacts from XML file, returns empty list if file doesn't exist
        public static List<Contact> LoadContacts()
        {
            if (!File.Exists(FilePath))
                return new List<Contact>();

            var serializer = new XmlSerializer(typeof(List<Contact>));
            using (var reader = new StreamReader(FilePath))
            {
                return (List<Contact>)serializer.Deserialize(reader);
            }
        }

        // Saves contacts to XML file
        public static void SaveContacts(List<Contact> contacts)
        {
            var serializer = new XmlSerializer(typeof(List<Contact>));
            using (var writer = new StreamWriter(FilePath))
            {
                serializer.Serialize(writer, contacts);
            }
        }
    }
}

