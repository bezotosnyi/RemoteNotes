using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using RemoteNotes.BLL.Contract;
using RemoteNotes.Logging.Contract;
using RemoteNotes.Service.Domain.DTO;
using RemoteNotes.Service.Domain.Helpers;
using RemoteNotes.Service.Front.Contract;

namespace RemoteNotes.Service.Front
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class RemoteNotesService : IRemoteNotesService
    {
        private readonly IRemoteNotesLogger<RemoteNotesService> _logger;
        private readonly IAccountService _accountService;
        private readonly INoteService _noteService;
        private readonly IUserService _userService;

        public RemoteNotesService(IRemoteNotesLogger<RemoteNotesService> logger, IAccountService accountService,
            INoteService noteService, IUserService userService)
        {
            _logger = logger;
            _accountService = accountService;
            _noteService = noteService;
            _userService = userService;
        }

        public OperationStatusInfo<UserDTO> Login(string login, string password)
        {
            _logger.Info($"User {GetClientAddress()} invoked {nameof(Login)} method.");
            try
            {
                var loggedUser = _userService.Login(login, password);
                return GetSuccessOperationStatusInfo(loggedUser);

            }
            catch (Exception ex)
            {
                return GetFailOperationStatusInfo<UserDTO>(ex);
            }
        }

        public OperationStatusInfo<UserDTO> Registration(UserDTO user)
        {
            _logger.Info($"User {GetClientAddress()} invoked {nameof(Registration)} method.");
            try
            {
                var registeredUser = _userService.Registration(user);
                return GetSuccessOperationStatusInfo(registeredUser);
            }
            catch (Exception ex)
            {
                return GetFailOperationStatusInfo<UserDTO>(ex);
            }
        }

        public OperationStatusInfo<NoteDTO> AddNote(NoteDTO note)
        {
            _logger.Info($"User {GetClientAddress()} invoked {nameof(AddNote)} method.");
            try
            {
                var addedNote = _noteService.Add(note);
                return GetSuccessOperationStatusInfo(addedNote);
            }
            catch (Exception ex)
            {
                return GetFailOperationStatusInfo<NoteDTO>(ex);
            }
        }

        public OperationStatusInfo<NoteDTO> EditNote(NoteDTO note)
        {
            _logger.Info($"User {GetClientAddress()} invoked {nameof(EditNote)} method.");
            try
            {
                var updatedNote = _noteService.Update(note);
                return GetSuccessOperationStatusInfo(updatedNote);
            }
            catch (Exception ex)
            {
                return GetFailOperationStatusInfo<NoteDTO>(ex);
            }
        }

        public OperationStatusInfo<bool> DeleteNote(int noteId)
        {
            _logger.Info($"User {GetClientAddress()} invoked {nameof(DeleteNote)} method.");
            try
            {
                var isDeleted = _noteService.Delete(noteId);
                return GetSuccessOperationStatusInfo(isDeleted);

            }
            catch (Exception ex)
            {
                return GetFailOperationStatusInfo<bool>(ex);
            }
        }

        public OperationStatusInfo<List<NoteDTO>> GetNotes(int accountId)
        {
            _logger.Info($"User {GetClientAddress()} invoked {nameof(GetNotes)} method.");
            try
            {
                var notes = _noteService.GetNotes(accountId).ToList();
                return GetSuccessOperationStatusInfo(notes);
            }
            catch (Exception ex)
            {
                return GetFailOperationStatusInfo<List<NoteDTO>>(ex);
            }
        }

        private static string GetClientAddress()
        {
            var context = OperationContext.Current;
            var prop = context.IncomingMessageProperties;
            var endpoint = prop[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            return $"{endpoint?.Address}:{endpoint?.Port}";
        }

        private OperationStatusInfo<T> GetSuccessOperationStatusInfo<T>(T obj)
        {
            return new OperationStatusInfo<T>
            {
                AttachedObject = obj,
                OperationStatus = OperationStatus.Success
            };
        }

        private OperationStatusInfo<T> GetFailOperationStatusInfo<T>(Exception ex)
        {
            var errorMessage =
                $"The following exception was thrown: '{ex.Message}'. Stack trace: '{ex.StackTrace}'.";
            _logger.Error(errorMessage);
            return new OperationStatusInfo<T>
            {
                AttachedInfo = errorMessage,
                OperationStatus = OperationStatus.Fail
            };
        }
    }
}