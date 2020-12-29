using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Description;
using RemoteNotes.Service.Domain.DTO;
using RemoteNotes.Service.Domain.Helpers;
using RemoteNotes.Service.Front.Contract;

namespace RemoteNotes.Service.Client.Front
{
    public class RemoteNotesClient : ClientBase<IRemoteNotesService>, IRemoteNotesService
    {
        public RemoteNotesClient(string address)
            : base(new WebHttpBinding(), new EndpointAddress(address))
        {
            Endpoint.Behaviors.Add(new WebHttpBehavior());
        }

        public OperationStatusInfo<UserDTO> Login(string login, string password)
        {
            using (new OperationContextScope(InnerChannel))
            {
                return Channel.Login(login, password);
            }
        }

        public OperationStatusInfo<UserDTO> Registration(UserDTO user)
        {
            using (new OperationContextScope(InnerChannel))
            {
                return Channel.Registration(user);
            }
        }

        public OperationStatusInfo<NoteDTO> AddNote(NoteDTO note)
        {
            using (new OperationContextScope(InnerChannel))
            {
                return Channel.AddNote(note);
            }
        }

        public OperationStatusInfo<NoteDTO> EditNote(NoteDTO note)
        {
            using (new OperationContextScope(InnerChannel))
            {
                return Channel.EditNote(note);
            }
        }

        public OperationStatusInfo<bool> DeleteNote(int noteId)
        {
            using (new OperationContextScope(InnerChannel))
            {
                return Channel.DeleteNote(noteId);
            }
        }

        public OperationStatusInfo<List<NoteDTO>> GetNotes(int accountId)
        {
            using (new OperationContextScope(InnerChannel))
            {
                return Channel.GetNotes(accountId);
            }
        }
    }
}