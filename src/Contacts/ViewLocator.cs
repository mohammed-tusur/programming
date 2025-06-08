using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using System.ComponentModel;
namespace Contacts
{
    // This class implements the IDataTemplate interface to automatically map view models to their corresponding views.
    public class ViewLocator : IDataTemplate
    {
        // Builds a control based on the provided parameter (usually a view model).
        public Control? Build(object? param)
        {
            if (param is null)
                return null;

            // Derives the view type name by replacing "ViewModel" with "View" in the parameter's type name.
            var name = param.GetType().FullName!.Replace("ViewModel", "View", StringComparison.Ordinal);

            // Attempts to get the Type object for the derived view name.
            var type = Type.GetType(name);

            if (type != null)
            {
                // Creates an instance of the view using Activator.CreateInstance.
                return (Control)Activator.CreateInstance(type)!;
            }

            // If the view type is not found, returns a TextBlock indicating the missing view.
            return new TextBlock { Text = "Not Found: " + name };
        }

        // Determines whether this template can be applied to the given data.
        public bool Match(object? data)
        {
            // Matches if the data implements INotifyPropertyChanged (common for view models).
            return data is INotifyPropertyChanged;
        }
    }
}