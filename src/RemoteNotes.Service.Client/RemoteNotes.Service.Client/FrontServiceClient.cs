﻿using System;
using System.Collections.Generic;
using RemoteNotes.Service.Client.Contract;
using RemoteNotes.Service.Domain.DTO;
using RemoteNotes.Service.Domain.Helpers;
using RemoteNotes.Service.Front.Contract;

namespace RemoteNotes.Service.Client
{
    public class FrontServiceClient : IFrontServiceClient
    {
        private readonly IRemoteNotesService _remoteNotesClient;

        public FrontServiceClient(IRemoteNotesService remoteNotesClient)
        {
            _remoteNotesClient = remoteNotesClient;
        }

        public UserDTO Login(string login, string password)
        {
            var operationStatusInfo = _remoteNotesClient.Login(login, password);
            return GetAttachedObject(operationStatusInfo);
        }

        public UserDTO RegisterUser(UserDTO user)
        {
            var operationStatusInfo = _remoteNotesClient.Registration(user);
            return GetAttachedObject(operationStatusInfo);
        }

        public NoteDTO AddNote(NoteDTO note)
        {
            var operationStatusInfo = _remoteNotesClient.AddNote(note);
            return GetAttachedObject(operationStatusInfo);
        }

        public NoteDTO EditNote(NoteDTO note)
        {
            var operationStatusInfo = _remoteNotesClient.EditNote(note);
            return GetAttachedObject(operationStatusInfo);
        }

        public bool DeleteNote(int noteId)
        {
            var operationStatusInfo = _remoteNotesClient.DeleteNote(noteId);
            return GetAttachedObject(operationStatusInfo);
        }

        public List<NoteDTO> GetNotes(int accountId)
        {
            var operationStatusInfo = _remoteNotesClient.GetNotes(accountId);
            return GetAttachedObject(operationStatusInfo);
        }

        private static T GetAttachedObject<T>(OperationStatusInfo<T> operationStatusInfo)
        {
            return operationStatusInfo.OperationStatus == OperationStatus.Success
                ? operationStatusInfo.AttachedObject
                : throw new InvalidOperationException(operationStatusInfo.AttachedInfo);
        }
    }
}