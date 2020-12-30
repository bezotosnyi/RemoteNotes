using System;
using Microsoft.EntityFrameworkCore;
using Moq;
using RemoteNotes.DAL.Contact;
using Xunit;

namespace RemoteNotes.DAL.Tests.Unit
{
    public class UnitOfWorkTests
    {
        private readonly Mock<DbContext> _dbContextMock;
        private IUnitOfWork _sut;

        public UnitOfWorkTests()
        {
            _dbContextMock = new Mock<DbContext>(MockBehavior.Strict);
        }

        [Fact]
        public void UnitOfWork_IfPassingNullDbContext_ThrowsArgumentNullException()
        {
            // arrange
            DbContext nullDbContext = null;

            // act, assert
            Assert.Throws<ArgumentNullException>(() => _sut = new UnitOfWork(nullDbContext));
        }

        [Fact]
        public void UnitOfWork_IfPassingDbContext_GetsRepositoriesSuccess()
        {
            // act
            _sut = new UnitOfWork(_dbContextMock.Object);

            // assert
            Assert.NotNull(_sut.AccountRepository);
            Assert.NotNull(_sut.NoteRepository);
            Assert.NotNull(_sut.UserRepository);
        }

        [Fact]
        public void Commit_IfCommit_CommitsSuccess()
        {
            // arrange
            _sut = new UnitOfWork(_dbContextMock.Object);
            _dbContextMock.Setup(_ => _.SaveChanges()).Returns(0);

            // act
            _sut.Commit();

            // assert
            _dbContextMock.Verify(_ => _.SaveChanges(), Times.Once);
        }
    }
}