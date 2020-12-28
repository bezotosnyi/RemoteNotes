using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using RemoteNotes.UI.Contract;

namespace RemoteNotes.UI.Control
{
    /// <summary>
    /// Interaction logic for RegisterControl.xaml
    /// </summary>
    public partial class RegisterControl : UserControl, IView
    {
        public RegisterControl()
        {
            InitializeComponent();
        }

        public void SetFocus() => FocusManager.SetFocusedElement(this, FirstNameTextBox);

        public void ClearError()
        {
            throw new NotImplementedException();
        }

        public void ShowError(string errorMessage) => MessageBox.Show(errorMessage);
    }
}
