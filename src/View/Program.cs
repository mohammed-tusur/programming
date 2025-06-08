using Avalonia;
using System;
namespace Contacts;

// This is the entry point of the application.
sealed class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args) => BuildAvaloniaApp()
        .StartWithClassicDesktopLifetime(args);

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect() // Detects and uses the appropriate platform (e.g., Windows, macOS, Linux)
            .WithInterFont() // Uses InterFont for consistent typography across platforms
            .LogToTrace(); // Logs application events to the trace output
}