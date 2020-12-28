using System;
using System.Windows;
using RemoteNotes.Service.Client.Contract;
using RemoteNotes.UI.Contract;
using RemoteNotes.UI.Utility;

namespace RemoteNotes.UI.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        public static readonly DependencyProperty LoginProperty =
            DependencyProperty.Register("Login", typeof(string),
                typeof(LoginViewModel), new PropertyMetadata(null));

        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(string),
                typeof(LoginViewModel), new PropertyMetadata(null));

        private RelayCommand _loginCommand;
        private RelayCommand _signUpCommand;

        public LoginViewModel(IMainWindowController mainWindowController, IFrontServiceClient frontServiceClient) :
            base(mainWindowController, frontServiceClient)
        {
        }

        public string LoginName
        {
            get => (string)GetValue(LoginProperty);
            set => SetValue(LoginProperty, value);
        }

        public string Password
        {
            get => (string)GetValue(PasswordProperty);
            set => SetValue(PasswordProperty, value);
        }

        public ICommand LoginCommand =>
            _loginCommand ?? (_loginCommand = new RelayCommand(_ => Login(), CanLogin));

        public ICommand SignUpCommand =>
            _signUpCommand ?? (_signUpCommand = new RelayCommand(_ => SignUp(), CanSignUp));

        private void Login()
        {
            try
            {
                _ = _frontServiceClient.Login(LoginName, Password);
                _mainWindowController.LoadDashboard();
            }
            catch (Exception ex)
            {
                View.ShowError(ex.ToString());
            }
        }

        private void SignUp()
        {
            try
            {
                _mainWindowController.LoadRegister();
            }
            catch (Exception ex)
            {
                View.ShowError(ex.ToString());
            }
        }

        private bool CanLogin(object _) => true;

        private bool CanSignUp(object _) => true;

        protected override string GetValidationError(string property)
        {
            throw new NotImplementedException();
        }
    }
}