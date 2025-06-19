using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace View.Controls
{
    /// <summary>
    /// Interaction logic for ContactControl.xaml
    /// </summary>
    public partial class ContactControl : UserControl
    {
        public ContactControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Validates text input in the PhoneTextBox.
        /// </summary>
        private void PhoneTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string pattern = @"[^\d()+-]";  // Regex pattern to block unwanted characters
            Regex regex = new Regex(pattern);
            e.Handled = regex.IsMatch(e.Text);  // Block input if invalid characters detected
        }

        /// <summary>
        /// Validates pasted text in the PhoneTextBox.
        /// </summary>
        private void PhoneTextBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (!e.DataObject.GetDataPresent(DataFormats.Text))
            {
                e.CancelCommand();  // cancel if data isn't text
                return;
            }

            string pattern = @"[^\d()+-]";  // same validation pattern as for direct input
            Regex regex = new Regex(pattern);
            string pastedText = e.DataObject.GetData(DataFormats.Text).ToString();

            if (regex.IsMatch(pastedText))
            {
                e.CancelCommand();  // cancel paste if invalid characters found
            }
        }
    }
}