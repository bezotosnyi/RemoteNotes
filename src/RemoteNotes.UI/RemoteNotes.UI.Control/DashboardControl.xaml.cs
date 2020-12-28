using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using RemoteNotes.UI.Contract;

namespace RemoteNotes.UI.Control
{
    /// <summary>
    /// Interaction logic for DashboardControl.xaml
    /// </summary>
    public partial class DashboardControl : UserControl, IView
    {
        public DashboardControl()
        {
            InitializeComponent();
        }

        public void SetFocus() => FocusManager.SetFocusedElement(this, null);

        public void ClearError()
        {
            throw new NotImplementedException();
        }

        public void ShowError(string errorMessage) => MessageBox.Show(errorMessage);
    }
}
