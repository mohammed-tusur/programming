using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Services
{
    /// <summary>
    /// Provides methods for serializing and deserializing contact data to/from JSON format.
    /// </summary>
    public static class ContactSerializer
    {
        /// <summary>
        /// Gets or sets the full path to the contacts data file.
        /// </summary>
        public static string FileName { get; set; }

        /// <summary>
        /// Serializes a collection of contacts to JSON format and saves to file.
        /// </summary>
        public static void SaveData(ObservableCollection<Contact> contacts)
        {
            string directoryName = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), 
                "Contacts");
            DirectoryInfo directoryInfo = new DirectoryInfo(directoryName);
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }

            FileName = Path.Combine(directoryName, "contacts.json");
            File.WriteAllText(FileName, string.Empty);
            for (int i = 0; i < contacts.Count; i++)
            {
                File.AppendAllText(FileName, JsonConvert.SerializeObject(contacts[i]));
            }
        }

        /// <summary>
        /// Deserializes contacts from JSON file.
        /// </summary>
        public static ObservableCollection<Contact> LoadData()
        {
            string directoryName = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "Contacts");
            DirectoryInfo directoryInfo = new DirectoryInfo(directoryName);
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }

            FileName = Path.Combine(directoryName, "contacts.json");
            FileInfo fileInfo = new FileInfo(FileName);
            if (!fileInfo.Exists)
            {
                File.WriteAllText(FileName, string.Empty);
                return new ObservableCollection<Contact>();
            }
            else
            {
                Contact contact = new Contact();
                ObservableCollection<Contact> contacts = new ObservableCollection<Contact>();
                JsonTextReader reader = new JsonTextReader(new StreamReader(FileName));
                reader.SupportMultipleContent = true;
                while (reader.Read())
                {
                    JsonSerializer serializer = new JsonSerializer();
                    contact = serializer.Deserialize<Contact>(reader);
                    contacts.Add(contact);
                }
                reader.Close();
                return contacts;
            }
        }
    }
}
