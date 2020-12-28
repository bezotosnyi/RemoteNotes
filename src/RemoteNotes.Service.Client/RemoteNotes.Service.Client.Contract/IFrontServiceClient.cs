using System.Collections.Generic;
using RemoteNotes.Service.Domain.DTO;

namespace RemoteNotes.Service.Client.Contract
{
    public interface IFrontServiceClient
    {
        UserDTO Login(string login, string password);
        UserDTO RegisterUser(UserDTO user);

        NoteDTO AddNote(NoteDTO note);
        NoteDTO EditNote(NoteDTO note);
        bool DeleteNote(int noteId);
        List<NoteDTO> GetNotes(int accountId);
    }
}