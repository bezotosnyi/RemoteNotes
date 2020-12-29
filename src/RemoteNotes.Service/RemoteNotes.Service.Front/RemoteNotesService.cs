using System.Collections.Generic;
using System.ServiceModel;
using RemoteNotes.Service.Domain.DTO;
using RemoteNotes.Service.Domain.Helpers;
using RemoteNotes.Service.Front.Contract;

namespace RemoteNotes.Service.Front
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class RemoteNotesService : IRemoteNotesService
    {
        public OperationStatusInfo<UserDTO> Login(string login, string password)
        {
            throw new System.NotImplementedException();
        }

        public OperationStatusInfo<UserDTO> Registration(UserDTO user)
        {
            throw new System.NotImplementedException();
        }

        public OperationStatusInfo<NoteDTO> AddNote(NoteDTO note)
        {
            throw new System.NotImplementedException();
        }

        public OperationStatusInfo<NoteDTO> EditNote(NoteDTO note)
        {
            throw new System.NotImplementedException();
        }

        public OperationStatusInfo<bool> DeleteNote(int noteId)
        {
            throw new System.NotImplementedException();
        }

        public OperationStatusInfo<List<NoteDTO>> GetNotes(int accountId)
        {
            throw new System.NotImplementedException();
        }
    }
}