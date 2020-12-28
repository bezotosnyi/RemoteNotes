using System;
using System.Collections.Generic;
using RemoteNotes.UI.Contract;
using RemoteNotes.UI.ViewModel;

namespace RemoteNotes.UI.Control
{
    public class ControlFactory : IControlFactory
    {
        private readonly Dictionary<Type, object> _controlCollection = new Dictionary<Type, object>();
        private readonly IViewModelFactory _viewModelFactory;

        public ControlFactory(IViewModelFactory viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
            
            var loginControl = new LoginControl();
            loginControl.DataContext = _viewModelFactory.Create<LoginViewModel>(loginControl);
            _controlCollection.Add(typeof(LoginControl), loginControl);
        }

        public T Create<T>()
        {
            var type = typeof(T);
            if (!_controlCollection.ContainsKey(type))
            {
                throw new MissingMemberException($"{type} is missing in the control collection");
            }

            return (T)_controlCollection[type];
        }
    }
}