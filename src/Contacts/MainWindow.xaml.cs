using Contacts.Models;
using Contacts.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Contacts
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
        // Search functionality
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (DataContext is MainViewModel vm)
            {
                var searchText = SearchTextBox.Text.ToLower();
                ContactsListBox.Items.Filter = item =>
                    item is Contact contact &&
                    contact.Name?.ToLower().Contains(searchText) == true;
            }
        }

    }
}