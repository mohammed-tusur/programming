using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.IO;

namespace View.Model.Services
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
        /// <param name="contacts">The collection of contacts to serialize.</param>
        /// <remarks>
        /// Creates the Contacts directory in My Documents if it doesn't exist.
        /// Overwrites existing data in the contacts.json file.
        /// </remarks>
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

            // Clear existing file and write all contacts as JSON
            File.WriteAllText(FileName, string.Empty);
            foreach (var contact in contacts)
            {
                File.AppendAllText(FileName, JsonConvert.SerializeObject(contact));
            }
        }

        /// <summary>
        /// Deserializes contacts from JSON file.
        /// </summary>
        /// <returns>
        /// An ObservableCollection of Contact objects loaded from file.
        /// Returns empty collection if file doesn't exist.
        /// </returns>
        /// <remarks>
        /// Creates the Contacts directory and empty JSON file if they don't exist.
        /// </remarks>
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

            if (!fileInfo.Exists || fileInfo.Length == 0)
            {
                File.WriteAllText(FileName, string.Empty);
                return new ObservableCollection<Contact>();
            }

            var contacts = new ObservableCollection<Contact>();
            using (var reader = new JsonTextReader(new StreamReader(FileName)))
            {
                reader.SupportMultipleContent = true;
                var serializer = new JsonSerializer();

                while (reader.Read())
                {
                    var contact = serializer.Deserialize<Contact>(reader);
                    if (contact != null)
                    {
                        contacts.Add(contact);
                    }
                }
            }

            return contacts;
        }
    }
}