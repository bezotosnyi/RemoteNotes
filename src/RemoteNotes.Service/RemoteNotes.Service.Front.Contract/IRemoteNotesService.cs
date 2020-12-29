using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using RemoteNotes.Service.Domain.DTO;
using RemoteNotes.Service.Domain.Helpers;

namespace RemoteNotes.Service.Front.Contract
{
    [ServiceContract]
    public interface IRemoteNotesService
    {
        [WebInvoke(
            Method = "PUT",
            UriTemplate = "/api/remotenotes/account/login",
            BodyStyle = WebMessageBodyStyle.Bare,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        OperationStatusInfo<UserDTO> Login(string login, string password);

        [WebInvoke(
            Method = "POST",
            UriTemplate = "/api/remotenotes/account/registration",
            BodyStyle = WebMessageBodyStyle.Bare,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        OperationStatusInfo<UserDTO> Registration(UserDTO user);

        [WebInvoke(
            Method = "POST",
            UriTemplate = "/api/remotenotes/notes",
            BodyStyle = WebMessageBodyStyle.Bare,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        OperationStatusInfo<NoteDTO> AddNote(NoteDTO note);

        [WebInvoke(
            Method = "PUT",
            UriTemplate = "/api/remotenotes/notes",
            BodyStyle = WebMessageBodyStyle.Bare,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        OperationStatusInfo<NoteDTO> EditNote(NoteDTO note);

        [WebInvoke(
            Method = "DELETE",
            UriTemplate = "/api/remotenotes/notes",
            BodyStyle = WebMessageBodyStyle.Bare,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        OperationStatusInfo<bool> DeleteNote(int noteId);

        [WebInvoke(
            Method = "GET",
            UriTemplate = "/api/remotenotes/notes",
            BodyStyle = WebMessageBodyStyle.Bare,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        OperationStatusInfo<List<NoteDTO>> GetNotes(int accountId);
    }
}