using System.Collections.Generic;
using System.Linq;
using RemoteNotes.Service.Client.Contract;
using RemoteNotes.Service.Domain.DTO;

namespace RemoteNotes.Service.Client.Stub
{
    public class ServiceClientStub : IServiceClient
    {
        private static readonly UserDTO _adminUser = new UserDTO
        {
            Id = 1,
            Account = new AccountDTO
            {
                Id = 1
            },
            Login = "admin",
            Password = "admin",
            IsActive = true
        };

        private readonly List<UserDTO> _users = new List<UserDTO>
        {
            _adminUser
        };

        private readonly List<NoteDTO> _notes = new List<NoteDTO>();

        public UserDTO Login(string login, string password) =>
            _users.FirstOrDefault(_ => _.Login.Equals(login) && _.Password.Equals(password));

        public UserDTO RegisterUser(UserDTO user)
        {
            _users.Add(user);
            return user;
        }

        public NoteDTO AddNote(NoteDTO note)
        {
            _notes.Add(note);
            return note;
        }

        public NoteDTO EditNote(NoteDTO note)
        {
            var oldNoteIndex = _notes.FindIndex(_ => _.Id == note.Id);
            _notes[oldNoteIndex] = note;
            return note;
        }

        public bool DeleteNote(int noteId)
        {
            var noteIndex = _notes.FindIndex(_ => _.Id == noteId);
            if (noteIndex < 0)
            {
                return false;
            }

            _notes.RemoveAt(noteIndex);
            return true;
        }

        public List<NoteDTO> GetNotes(int accountId) => _notes;
    }
}