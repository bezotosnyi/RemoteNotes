using System;
using System.Collections.Generic;
using RemoteNotes.Service.Client.Contract;
using RemoteNotes.UI.Contract;

namespace RemoteNotes.UI.ViewModel
{
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly Dictionary<Type, ViewModelBase> _viewModelCollection = new Dictionary<Type, ViewModelBase>();
        private readonly IMainWindowController _mainWindowController;
        private readonly IFrontServiceClient _frontServiceClient;

        public ViewModelFactory(IMainWindowController mainWindowController, IFrontServiceClient frontServiceClient)
        {
            _mainWindowController = mainWindowController;
            _frontServiceClient = frontServiceClient;

            _viewModelCollection.Add(typeof(LoginViewModel), new LoginViewModel(_mainWindowController, frontServiceClient));
            _viewModelCollection.Add(typeof(RegisterViewModel), new RegisterViewModel(_mainWindowController, frontServiceClient));
        }

        public T Create<T>(IView view)
        {
            var type = typeof(T);
            if (!_viewModelCollection.ContainsKey(type))
            {
                throw new MissingMemberException($"{type} is missing in the view model collection");
            }

            var viewModel = _viewModelCollection[type];
            viewModel.View = view;

            return (T)(object)viewModel;
        }
    }
}