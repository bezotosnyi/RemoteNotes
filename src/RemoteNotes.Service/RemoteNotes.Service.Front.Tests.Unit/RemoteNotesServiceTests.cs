using System;
using System.Linq;
using Moq;
using RemoteNotes.BLL.Contract;
using RemoteNotes.Logging.Contract;
using RemoteNotes.Service.Domain.DTO;
using RemoteNotes.Service.Domain.Helpers;
using RemoteNotes.Service.Front.Contract;
using Xunit;

namespace RemoteNotes.Service.Front.Tests.Unit
{
    public class RemoteNotesServiceTests
    {
        private readonly Mock<IRemoteNotesLogger<RemoteNotesService>> _loggerMock;
        private readonly Mock<IAccountService> _accountServiceMock;
        private readonly Mock<INoteService> _noteServiceMock;
        private readonly Mock<IUserService> _userServiceMock;
        private readonly IRemoteNotesService _sut;

        public RemoteNotesServiceTests()
        {
            _loggerMock = new Mock<IRemoteNotesLogger<RemoteNotesService>>(MockBehavior.Strict);
            _accountServiceMock = new Mock<IAccountService>(MockBehavior.Strict);
            _noteServiceMock = new Mock<INoteService>(MockBehavior.Strict);
            _userServiceMock = new Mock<IUserService>(MockBehavior.Strict);

            _sut = new RemoteNotesService(_loggerMock.Object, _accountServiceMock.Object, _noteServiceMock.Object,
                _userServiceMock.Object);
        }

        [Fact]
        public void Login_IfLoginNotExistUser_LoginsFail()
        {
            // arrange
            const string login = "admin";
            const string password = "admin";
            _loggerMock.Setup(_ => _.Info(It.IsAny<string>()));
            _userServiceMock.Setup(_ => _.Login(login, password)).Throws<ArgumentNullException>();
            _loggerMock.Setup(_ => _.Error(It.IsAny<string>()));

            // act
            var statusInfo = _sut.Login(login, password);

            // assert
            _loggerMock.Verify(_ => _.Info(It.IsAny<string>()), Times.Once);
            _userServiceMock.Verify(_ => _.Login(login, password), Times.Once);
            _loggerMock.Verify(_ => _.Error(It.IsAny<string>()), Times.Once);
            Assert.True(statusInfo.OperationStatus == OperationStatus.Fail);
            Assert.NotNull(statusInfo.AttachedInfo);
        }

        [Fact]
        public void Login_IfLoginExistUser_LoginsSuccess()
        {
            // arrange
            const string login = "admin";
            const string password = "admin";
            var userDTO = new UserDTO
            {
                Id = 1,
                Account = new AccountDTO
                {
                    Birthday = DateTime.Now,
                    CreateTime = DateTime.Now,
                    Email = Guid.NewGuid().ToString(),
                    FirstName = Guid.NewGuid().ToString(),
                    Id = 1,
                    LastName = Guid.NewGuid().ToString(),
                    ModifyTime = DateTime.Now,
                    Nickname = Guid.NewGuid().ToString(),
                    Photo = new byte []{}
                },
                IsActive = true,
                Login = login,
                Password = password
            };
            _loggerMock.Setup(_ => _.Info(It.IsAny<string>()));
            _userServiceMock.Setup(_ => _.Login(login, password)).Returns(userDTO);

            // act
            var statusInfo = _sut.Login(login, password);

            // assert
            _loggerMock.Verify(_ => _.Info(It.IsAny<string>()), Times.Exactly(2));
            _userServiceMock.Verify(_ => _.Login(login, password), Times.Once);
            Assert.True(statusInfo.OperationStatus == OperationStatus.Success);
            Assert.Equal(userDTO, statusInfo.AttachedObject);
            Assert.Equal(userDTO.Id, statusInfo.AttachedObject.Id);
            Assert.Equal(userDTO.IsActive, statusInfo.AttachedObject.IsActive);
            Assert.Equal(userDTO.Login, statusInfo.AttachedObject.Login);
            Assert.Equal(userDTO.Password, statusInfo.AttachedObject.Password);
            Assert.Equal(userDTO.Account.Birthday, statusInfo.AttachedObject.Account.Birthday);
            Assert.Equal(userDTO.Account.ModifyTime, statusInfo.AttachedObject.Account.ModifyTime);
            Assert.Equal(userDTO.Account.CreateTime, statusInfo.AttachedObject.Account.CreateTime);
            Assert.Equal(userDTO.Account.Email, statusInfo.AttachedObject.Account.Email);
            Assert.Equal(userDTO.Account.FirstName, statusInfo.AttachedObject.Account.FirstName);
            Assert.Equal(userDTO.Account.Id, statusInfo.AttachedObject.Account.Id);
            Assert.Equal(userDTO.Account.LastName, statusInfo.AttachedObject.Account.LastName);
            Assert.Equal(userDTO.Account.ModifyTime, statusInfo.AttachedObject.Account.ModifyTime);
            Assert.Equal(userDTO.Account.Nickname, statusInfo.AttachedObject.Account.Nickname);
            Assert.Equal(userDTO.Account.Photo, statusInfo.AttachedObject.Account.Photo);
        }

        [Fact]
        public void Registration_IfRegisteringExistUser_RegistrationsFail()
        {
            var userDTO = new UserDTO();
            _loggerMock.Setup(_ => _.Info(It.IsAny<string>()));
            _userServiceMock.Setup(_ => _.Registration(userDTO)).Throws<OperationCanceledException>();
            _loggerMock.Setup(_ => _.Error(It.IsAny<string>()));

            // act
            var statusInfo = _sut.Registration(userDTO);

            // assert
            _loggerMock.Verify(_ => _.Info(It.IsAny<string>()), Times.Once);
            _userServiceMock.Verify(_ => _.Registration(userDTO), Times.Once);
            _loggerMock.Verify(_ => _.Error(It.IsAny<string>()), Times.Once);
            Assert.True(statusInfo.OperationStatus == OperationStatus.Fail);
        }

        [Fact]
        public void Registration_IfRegisteringNewUser_RegistrationsSuccess()
        {
            var userDTO = new UserDTO();
            _loggerMock.Setup(_ => _.Info(It.IsAny<string>()));
            _userServiceMock.Setup(_ => _.Registration(userDTO)).Returns(userDTO);

            // act
            var statusInfo = _sut.Registration(userDTO);

            // assert
            _loggerMock.Verify(_ => _.Info(It.IsAny<string>()), Times.Exactly(2));
            _userServiceMock.Verify(_ => _.Registration(userDTO), Times.Once);
            Assert.True(statusInfo.OperationStatus == OperationStatus.Success);
        }

        [Fact]
        public void AddNote_IfAddNote_AddsNoteSuccess()
        {
            // arrange
            var note = new NoteDTO();
            _loggerMock.Setup(_ => _.Info(It.IsAny<string>()));
            _noteServiceMock.Setup(_ => _.Add(note)).Returns(note);

            // act
            var statusInfo = _sut.AddNote(note);

            // assert
            _loggerMock.Verify(_ => _.Info(It.IsAny<string>()), Times.Exactly(2));
            _noteServiceMock.Verify(_ => _.Add(note), Times.Once);
            Assert.True(statusInfo.OperationStatus == OperationStatus.Success);
        }

        [Fact]
        public void AddNote_IfSomethingHappenedWhileAddingNote_AddsNoteFail()
        {
            // arrange
            var note = new NoteDTO();
            _loggerMock.Setup(_ => _.Info(It.IsAny<string>()));
            _noteServiceMock.Setup(_ => _.Add(note)).Throws<Exception>();
            _loggerMock.Setup(_ => _.Error(It.IsAny<string>()));

            // act
            var statusInfo = _sut.AddNote(note);

            // assert
            _loggerMock.Verify(_ => _.Info(It.IsAny<string>()), Times.Once);
            _noteServiceMock.Verify(_ => _.Add(note), Times.Once);
            _loggerMock.Verify(_ => _.Error(It.IsAny<string>()), Times.Once);
            Assert.True(statusInfo.OperationStatus == OperationStatus.Fail);
        }

        [Fact]
        public void EditNote_IfEditNote_EditsNoteSuccess()
        {
            // arrange
            var note = new NoteDTO();
            _loggerMock.Setup(_ => _.Info(It.IsAny<string>()));
            _noteServiceMock.Setup(_ => _.Update(note)).Returns(note);

            // act
            var statusInfo = _sut.EditNote(note);

            // assert
            _loggerMock.Verify(_ => _.Info(It.IsAny<string>()), Times.Exactly(2));
            _noteServiceMock.Verify(_ => _.Update(note), Times.Once);
            Assert.True(statusInfo.OperationStatus == OperationStatus.Success);
        }

        [Fact]
        public void EditNote_IfSomethingHappenedWhileEditingNote_EditsNoteFail()
        {
            // arrange
            var note = new NoteDTO();
            _loggerMock.Setup(_ => _.Info(It.IsAny<string>()));
            _noteServiceMock.Setup(_ => _.Update(note)).Throws<Exception>();
            _loggerMock.Setup(_ => _.Error(It.IsAny<string>()));

            // act
            var statusInfo = _sut.EditNote(note);

            // assert
            _loggerMock.Verify(_ => _.Info(It.IsAny<string>()), Times.Once);
            _noteServiceMock.Verify(_ => _.Update(note), Times.Once);
            _loggerMock.Verify(_ => _.Error(It.IsAny<string>()), Times.Once);
            Assert.True(statusInfo.OperationStatus == OperationStatus.Fail);
        }

        [Fact]
        public void DeleteNote_IfDeleteNote_DeletesNoteSuccess()
        {
            // arrange
            const int noteId = 1;
            _loggerMock.Setup(_ => _.Info(It.IsAny<string>()));
            _noteServiceMock.Setup(_ => _.Delete(noteId)).Returns(true);

            // act
            var statusInfo = _sut.DeleteNote(noteId);

            // assert
            _loggerMock.Verify(_ => _.Info(It.IsAny<string>()), Times.Exactly(2));
            _noteServiceMock.Verify(_ => _.Delete(noteId), Times.Once);
            Assert.True(statusInfo.OperationStatus == OperationStatus.Success);
            Assert.True(statusInfo.AttachedObject);
        }

        [Fact]
        public void DeleteNote_IfSomethingHappenedWhileDeletingNote_DeletesNoteFail()
        {
            // arrange
            const int noteId = 1;
            _loggerMock.Setup(_ => _.Info(It.IsAny<string>()));
            _noteServiceMock.Setup(_ => _.Delete(noteId)).Throws<Exception>();
            _loggerMock.Setup(_ => _.Error(It.IsAny<string>()));

            // act
            var statusInfo = _sut.DeleteNote(noteId);

            // assert
            _loggerMock.Verify(_ => _.Info(It.IsAny<string>()), Times.Once);
            _noteServiceMock.Verify(_ => _.Delete(noteId), Times.Once);
            _loggerMock.Verify(_ => _.Error(It.IsAny<string>()), Times.Once);
            Assert.True(statusInfo.OperationStatus == OperationStatus.Fail);
        }

        [Fact]
        public void GetNotes_IfGetNotes_GetsNotesSuccess()
        {
            // arrange
            const int accountId = 1;
            var note = new NoteDTO
            {
                Id = 1,
                Account = new AccountDTO(),
                Image = new byte[] { },
                ModifyTime = DateTime.Now,
                PublishTime = DateTime.Now,
                Title = Guid.NewGuid().ToString(),
                Text = Guid.NewGuid().ToString()
            };
            var notes = new[] {note};
            _loggerMock.Setup(_ => _.Info(It.IsAny<string>()));
            _noteServiceMock.Setup(_ => _.GetNotes(accountId)).Returns(notes);

            // act
            var statusInfo = _sut.GetNotes(accountId);

            // assert
            _loggerMock.Verify(_ => _.Info(It.IsAny<string>()), Times.Exactly(2));
            _noteServiceMock.Verify(_ => _.GetNotes(accountId), Times.Once);
            Assert.True(statusInfo.OperationStatus == OperationStatus.Success);
            Assert.True(notes.SequenceEqual(statusInfo.AttachedObject));
            Assert.Equal(note.Id, statusInfo.AttachedObject[0].Id);
            Assert.Equal(note.Account, statusInfo.AttachedObject[0].Account);
            Assert.Equal(note.Image, statusInfo.AttachedObject[0].Image);
            Assert.Equal(note.ModifyTime, statusInfo.AttachedObject[0].ModifyTime);
            Assert.Equal(note.PublishTime, statusInfo.AttachedObject[0].PublishTime);
            Assert.Equal(note.Title, statusInfo.AttachedObject[0].Title);
            Assert.Equal(note.Text, statusInfo.AttachedObject[0].Text);
        }

        [Fact]
        public void GetNotes_IfSomethingHappenedWhileGettingNotes_GetsNotesFail()
        {
            // arrange
            const int accountId = 1;
            _loggerMock.Setup(_ => _.Info(It.IsAny<string>()));
            _noteServiceMock.Setup(_ => _.GetNotes(accountId)).Throws<Exception>();
            _loggerMock.Setup(_ => _.Error(It.IsAny<string>()));

            // act
            var statusInfo = _sut.GetNotes(accountId);

            // assert
            _loggerMock.Verify(_ => _.Info(It.IsAny<string>()), Times.Once);
            _noteServiceMock.Verify(_ => _.GetNotes(accountId), Times.Once);
            _loggerMock.Verify(_ => _.Error(It.IsAny<string>()), Times.Once);
            Assert.True(statusInfo.OperationStatus == OperationStatus.Fail);
        }
    }
}