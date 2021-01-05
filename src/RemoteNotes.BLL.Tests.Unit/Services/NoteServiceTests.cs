using System;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Moq;
using RemoteNotes.BLL.Contract;
using RemoteNotes.BLL.Services;
using RemoteNotes.DAL.Contact;
using RemoteNotes.DAL.Domain.Entities;
using RemoteNotes.Logging.Contract;
using RemoteNotes.Service.Domain.DTO;
using Xunit;

namespace RemoteNotes.BLL.Tests.Unit.Services
{
    public class NoteServiceTests
    {
        private readonly Mock<IRemoteNotesLogger<ServiceBase<Note, NoteDTO>>> _loggerMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IRepository<Note>> _currentRepositoryMock;
        private readonly INoteService _sut;

        public NoteServiceTests()
        {
            _loggerMock = new Mock<IRemoteNotesLogger<ServiceBase<Note, NoteDTO>>>(MockBehavior.Strict);
            _mapperMock = new Mock<IMapper>(MockBehavior.Strict);
            _unitOfWorkMock = new Mock<IUnitOfWork>(MockBehavior.Strict);
            _currentRepositoryMock = new Mock<IRepository<Note>>(MockBehavior.Strict);

            _sut = new NoteService(_loggerMock.Object, _mapperMock.Object, _unitOfWorkMock.Object,
                _currentRepositoryMock.Object);
        }

        [Fact]
        public void GetNotes_IfGetNotes_GetsNotesSuccess()
        {
            // arrange
            const int accountId = 1;
            const bool trackChanges = false;
            var note = new Note {UserId = accountId};
            var noteDTO = new NoteDTO {Account = new AccountDTO {Id = accountId}};
            var notes = new[] {note};
            var notesDTO = new[] {noteDTO};
            _currentRepositoryMock.Setup(_ => _.FindByCondition(It.IsAny<Expression<Func<Note, bool>>>(), trackChanges))
                .Returns(notes.AsQueryable());
            _mapperMock.Setup(_ => _.Map<Note, NoteDTO>(note)).Returns(noteDTO);

            // act
            var actualNotes = _sut.GetNotes(accountId).ToArray();

            // assert
            _currentRepositoryMock.Verify(
                _ => _.FindByCondition(It.IsAny<Expression<Func<Note, bool>>>(), trackChanges), Times.Once);
            _mapperMock.Verify(_ => _.Map<Note, NoteDTO>(note), Times.Once);
            Assert.True(notesDTO.SequenceEqual(actualNotes));

        }
    }
}