using System;
using System.Windows;
using System.Windows.Input;
using RemoteNotes.Service.Client.Contract;
using RemoteNotes.UI.Contract;
using RemoteNotes.UI.Utility;

namespace RemoteNotes.UI.ViewModel
{
    public class DashboardViewModel : ViewModelBase
    {
        public DashboardViewModel(IMainWindowController mainWindowController, IFrontServiceClient frontServiceClient) :
            base(mainWindowController, frontServiceClient)
        {
        }

        protected override string GetValidationError(string property)
        {
            throw new NotImplementedException();
        }
    }
}
