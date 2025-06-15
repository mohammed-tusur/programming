using Contacts.ViewModels;
using System.Windows;

namespace Contacts
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}