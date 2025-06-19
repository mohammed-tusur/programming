using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace View.Controls
{
    /// <summary>
    /// Логика взаимодействия для ContactControl.xaml
    /// </summary>
    public partial class ContactControl : UserControl
    {
        public ContactControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Валидирует вводимый текст в PhoneTextBox.
        /// </summary>
        private void PhoneTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string pattern = @"[^\d()+-]";
            Regex regex = new Regex(pattern);
            e.Handled = regex.IsMatch(e.Text);
        }

        /// <summary>
        /// Валидирует вставку текста в PhoneTextBox.
        /// </summary>
        private void PhoneTextBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (!e.DataObject.GetDataPresent(DataFormats.Text))
            {
                e.CancelCommand();
                return;
            }
            string pattern = @"[^\d()+-]";
            Regex regex = new Regex(pattern);
            if (!regex.IsMatch((e.DataObject.GetData(DataFormats.Text)).ToString()))
            {
                e.CancelCommand();
            }
        }
    }
}
