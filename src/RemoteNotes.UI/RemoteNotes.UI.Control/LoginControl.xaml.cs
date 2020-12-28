using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using RemoteNotes.UI.Contract;

namespace RemoteNotes.UI.Control
{
    /// <summary>
    /// Логика взаимодействия для LoginControl.xaml
    /// </summary>
    public partial class LoginControl : UserControl, IView
    {
        public LoginControl()
        {
            InitializeComponent();
        }

        public void SetFocus() => FocusManager.SetFocusedElement(this, LogInButton);

        public void ClearError()
        {
            throw new NotImplementedException();
        }

        public void ShowError(string errorMessage) => MessageBox.Show(errorMessage);
    }
}
