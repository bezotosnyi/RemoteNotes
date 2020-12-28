using System;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Input;
using RemoteNotes.Service.Client.Contract;
using RemoteNotes.Service.Domain.DTO;
using RemoteNotes.UI.Contract;
using RemoteNotes.UI.Utility;

namespace RemoteNotes.UI.ViewModel
{
    public class RegisterViewModel : ViewModelBase
    {
        public static readonly DependencyProperty FirstNameProperty =
            DependencyProperty.Register("FirstName", typeof(string),
                typeof(RegisterViewModel), new PropertyMetadata(null));

        public static readonly DependencyProperty LastNameProperty =
            DependencyProperty.Register("LastName", typeof(string),
                typeof(RegisterViewModel), new PropertyMetadata(null));

        public static readonly DependencyProperty NickNameProperty =
            DependencyProperty.Register("NickName", typeof(string),
                typeof(RegisterViewModel), new PropertyMetadata(null));

        public static readonly DependencyProperty BirthdayProperty =
            DependencyProperty.Register("Birthday", typeof(string),
                typeof(RegisterViewModel), new PropertyMetadata(null));

        public static readonly DependencyProperty EmailProperty =
            DependencyProperty.Register("Email", typeof(string),
                typeof(RegisterViewModel), new PropertyMetadata(null));

        public static readonly DependencyProperty LoginProperty =
            DependencyProperty.Register("Login", typeof(string),
                typeof(RegisterViewModel), new PropertyMetadata(null));

        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(string),
                typeof(RegisterViewModel), new PropertyMetadata(null));

        private ICommand _choosePhotoCommand;
        private ICommand _registerCommand;
        private ICommand _signInCommand;
        private string _imagePath;

        public RegisterViewModel(IMainWindowController mainWindowController, IFrontServiceClient frontServiceClient) :
            base(mainWindowController, frontServiceClient)
        {
        }

        public string FirstName
        {
            get => (string)GetValue(FirstNameProperty);
            set => SetValue(FirstNameProperty, value);
        }

        public string LastName
        {
            get => (string)GetValue(LastNameProperty);
            set => SetValue(LastNameProperty, value);
        }

        public string NickName
        {
            get => (string)GetValue(NickNameProperty);
            set => SetValue(NickNameProperty, value);
        }

        public string Birthday
        {
            get => (string)GetValue(BirthdayProperty);
            set => SetValue(BirthdayProperty, value);
        }

        public string Email
        {
            get => (string)GetValue(EmailProperty);
            set => SetValue(EmailProperty, value);
        }

        public string Login
        {
            get => (string)GetValue(LoginProperty);
            set => SetValue(LoginProperty, value);
        }

        public string Password
        {
            get => (string)GetValue(PasswordProperty);
            set => SetValue(PasswordProperty, value);
        }

        public ICommand ChoosePhotoCommand =>
            _choosePhotoCommand ?? (_choosePhotoCommand = new RelayCommand(_ => ChoosePhoto(), CanChoosePhoto));

        public ICommand LoginCommand =>
            _registerCommand ?? (_registerCommand = new RelayCommand(_ => Register(), CanRegister));

        public ICommand SignInCommand =>
            _signInCommand ?? (_signInCommand = new RelayCommand(_ => SignIn(), CanSignIn));

        private void ChoosePhoto()
        {
            try
            {
                var imagePicker = new ImagePicker();
                _imagePath = imagePicker.ChooseImage();
            }
            catch (Exception ex)
            {
                View.ShowError(ex.ToString());
            }
        }

        private void Register()
        {
            try
            {
                var user = new UserDTO
                {
                    Account = new AccountDTO
                    {
                        Photo = File.ReadAllBytes(_imagePath),
                        FirstName = FirstName,
                        LastName = LastName,
                        Nickname = NickName,
                        Birthday = DateTime.ParseExact(Birthday, "dd.MM.yyyy", CultureInfo.InvariantCulture),
                        Email = Email
                    },
                    Login = Login,
                    Password = Password
                };

                _frontServiceClient.RegisterUser(user);
                _mainWindowController.LoadLogin();
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

        private bool CanChoosePhoto(object _) => true;

        private bool CanRegister(object _) => true;

        private bool CanSignIn(object _) => true;

        protected override string GetValidationError(string property)
        {
            throw new NotImplementedException();
        }
    }
}
