using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Contacts.ViewModels;
using Contacts.Views;
namespace Contacts
{
    // The main application class that configures and initializes the Avalonia application.
    public class App : Application
    {
        // Called when the application is initializing.
        public override void Initialize()
        {
            // Loads the XAML resources defined in App.axaml.
            AvaloniaXamlLoader.Load(this);
        }

        // Called when the framework initialization is completed.
        public override void OnFrameworkInitializationCompleted()
        {
            // Checks if the application lifetime is of type IClassicDesktopStyleApplicationLifetime.
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                // Sets up the main window with its data context initialized to a new instance of MainViewModel.
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainViewModel(),
                };
            }

            // Calls the base method to complete the initialization.
            base.OnFrameworkInitializationCompleted();
        }
    }
}