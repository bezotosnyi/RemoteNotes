using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using RemoteNotes.Service.Client.Contract;
using RemoteNotes.Service.Domain.DTO;
using RemoteNotes.UI.Contract;
using RemoteNotes.UI.Utility;

namespace RemoteNotes.UI.ViewModel
{
    public class DashboardViewModel : ViewModelBase
    {
        public static readonly DependencyProperty NoteCollectionProperty = DependencyProperty.Register(
            "NoteCollection",
            typeof(ObservableCollection<NoteDTO>),
            typeof(DashboardViewModel),
            new PropertyMetadata(default(IObservable<NoteDTO>)));

        public static readonly DependencyProperty SelectedNoteIndexProperty = DependencyProperty.Register(
            "SelectedNoteIndex",
            typeof(int),
            typeof(DashboardViewModel),
            new PropertyMetadata(default(int)));

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            "Title",
            typeof(string),
            typeof(DashboardViewModel),
            new PropertyMetadata(default(string)));

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text",
            typeof(string),
            typeof(DashboardViewModel),
            new PropertyMetadata(default(string)));

        private ICommand _addNoteCommand;
        private ICommand _updateNoteCommand;
        private ICommand _deleteNoteCommand;
        private ICommand _selectionChangedCommand;
        private ICommand _loadNotesCommand;
        private ICommand _logoutCommand;
        private int _noteId;

        public DashboardViewModel(IMainWindowController mainWindowController, IFrontServiceClient frontServiceClient) :
            base(mainWindowController, frontServiceClient)
        {
        }

        public ObservableCollection<NoteDTO> NoteCollection
        {
            get => (ObservableCollection<NoteDTO>)GetValue(NoteCollectionProperty);
            set => SetValue(NoteCollectionProperty, value);
        }

        public int SelectedNoteIndex
        {
            get => (int)GetValue(SelectedNoteIndexProperty);
            set => SetValue(SelectedNoteIndexProperty, value);
        }

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public ICommand AddNoteCommand =>
            _addNoteCommand ?? (_addNoteCommand = new RelayCommand(_ => AddRecord(), _ => CanSaveOrAddCommand()));

        public ICommand UpdateNoteCommand =>
            _updateNoteCommand ?? (_updateNoteCommand = new RelayCommand(_ => SaveRecord(), _ => CanSaveOrAddCommand()));

        public ICommand DeleteNoteCommand =>
            _deleteNoteCommand ?? (_deleteNoteCommand = new RelayCommand(_ => DeleteRecord(), _ => CanDeleteCommand()));

        public ICommand SelectionChangedCommand =>
            _selectionChangedCommand ?? (_selectionChangedCommand = new RelayCommand(_ => SelectionChanged()));

        public ICommand LoadNotesCommand =>
            _loadNotesCommand ?? (_loadNotesCommand = new RelayCommand(_ => LoadNotes()));

        public ICommand LogoutCommand =>
            _logoutCommand ?? (_logoutCommand = new RelayCommand(_ => Logout()));

        private void AddRecord()
        {
            var newNote = new NoteDTO
            {
                Account = GetAccount(),
                Title = Title,
                Text = Text,
                PublishTime = DateTime.Now
            };

            var note = _frontServiceClient.AddNote(newNote);
            _noteId = note.Id;
            LoadNotes();
        }

        private void SaveRecord()
        {
            var note = new NoteDTO
            {
                Account = GetAccount(),
                Title = Title,
                Text = Text,
                ModifyTime = DateTime.Now
            };

            note.PublishTime = DateTime.Now;
            _frontServiceClient.EditNote(note);
            LoadNotes();
        }

        private void DeleteRecord()
        {
            _frontServiceClient.DeleteNote(_noteId);
            LoadNotes();
        }

        private void SelectionChanged()
        {
            if (SelectedNoteIndex == -1)
            {
                return;
            }

            Title = NoteCollection[SelectedNoteIndex].Title;
            Text = NoteCollection[SelectedNoteIndex].Text;
        }

        private void LoadNotes() =>
            NoteCollection = _frontServiceClient.GetNotes(GetAccount().Id).ToObservableCollection();

        private void Logout()
        {
            _mainWindowController.LoadLogin();
        }

        private bool CanDeleteCommand() => SelectedNoteIndex != -1;

        private bool CanSaveOrAddCommand()
        {
            return !string.IsNullOrEmpty(Title) && !string.IsNullOrEmpty(Text);
        }

        protected override string GetValidationError(string property)
        {
            throw new NotImplementedException();
        }

        private static AccountDTO GetAccount() => (AccountDTO)CacheManager.Instance.GetFromCache("account");
    }
}
