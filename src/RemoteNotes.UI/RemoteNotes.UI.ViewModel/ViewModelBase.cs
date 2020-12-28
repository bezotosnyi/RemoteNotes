using System.Collections.Generic;
using System.Windows;
using RemoteNotes.Service.Client.Contract;
using RemoteNotes.UI.Contract;

namespace RemoteNotes.UI.ViewModel
{
    public abstract class ViewModelBase: DependencyObject, IDataErrorInfo
    {
        protected readonly List<string> _validatablePropertyCollection = new List<string>();
        protected readonly IMainWindowController _mainWindowController;
        protected readonly IFrontServiceClient _frontServiceClient;

        protected ViewModelBase(IMainWindowController mainWindowController, IFrontServiceClient frontServiceClient)
        {
            _mainWindowController = mainWindowController;
            _frontServiceClient = frontServiceClient;
        }

        protected virtual bool IsValid
        {
            get
            {
                foreach (var property in _validatablePropertyCollection)
                {
                    if (!string.IsNullOrEmpty(GetValidationError(property)))
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        protected abstract string GetValidationError(string property);

        string IDataErrorInfo.this[string property]
        {
            get
            {
                var error = GetValidationError(property);
                return error;
            }
        }

        string IDataErrorInfo.Error => null;

        public IView View { get; set; }
    }
}