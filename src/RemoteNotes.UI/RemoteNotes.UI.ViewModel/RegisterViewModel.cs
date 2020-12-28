using System;
using System.Windows;
using System.Windows.Input;
using RemoteNotes.Service.Client.Contract;
using RemoteNotes.UI.Contract;
using RemoteNotes.UI.Utility;

namespace RemoteNotes.UI.ViewModel
{
    public class RegisterViewModel : ViewModelBase
    {
        private RelayCommand _registerCommand;
        private RelayCommand _signInCommand;

        public RegisterViewModel(IMainWindowController mainWindowController, IFrontServiceClient frontServiceClient) :
            base(mainWindowController, frontServiceClient)
        {
        }

        public ICommand LoginCommand =>
            _registerCommand ?? (_registerCommand = new RelayCommand(_ => Register(), CanRegister));

        public ICommand SignInCommand =>
            _signInCommand ?? (_signInCommand = new RelayCommand(_ => SignIn(), CanSignIn));

        private void Register()
        {
            try
            {
                //_ = _frontServiceClient.Login(LoginName, Password);
                _mainWindowController.LoadDashboard();
            }
            catch (Exception ex)
            {
                View.ShowError(ex.ToString());
            }
        }

        private void SignIn()
        {
            try
            {
                _mainWindowController.LoadLogin();
            }
            catch (Exception ex)
            {
                View.ShowError(ex.ToString());
            }
        }

        private bool CanRegister(object _) => true;

        private bool CanSignIn(object _) => true;

        protected override string GetValidationError(string property)
        {
            throw new NotImplementedException();
        }
    }
}
