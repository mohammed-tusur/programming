using Avalonia.Controls;
using Contacts.ViewModels;
namespace Contacts.Views
{
    // The main window of the application.
    public partial class MainWindow : Window
    {
        // Constructor for MainWindow.
        public MainWindow()
        {
            InitializeComponent();

            // Initializes the DataContext property with a new instance of MainViewModel.
            // This binds the UI elements to the properties and commands in MainViewModel.
            DataContext = new MainViewModel();
        }
    }
}