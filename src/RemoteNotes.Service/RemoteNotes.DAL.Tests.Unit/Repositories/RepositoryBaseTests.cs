using System;
using Microsoft.EntityFrameworkCore;
using Moq;
using RemoteNotes.DAL.Domain.Entities;
using RemoteNotes.DAL.Repositories;
using RemoteNotes.DAL.Tests.Unit.Fixtures;
using Xunit;

namespace RemoteNotes.DAL.Tests.Unit.Repositories
{
    public class RepositoryBaseTests
    {
        private readonly Mock<DbContext> _dbContextMock;
        private readonly DbSet<BaseEntity> _baseEntityDbSet;
        private RepositoryBase<BaseEntity> _sut;

        public RepositoryBaseTests()
        {
            _dbContextMock = new Mock<DbContext>(MockBehavior.Strict);
            _baseEntityDbSet = new Mock<DbSet<BaseEntity>>(MockBehavior.Strict).Object;
            _dbContextMock.Setup(_ => _.Set<BaseEntity>()).Returns(_baseEntityDbSet);

            _sut = new TestRepositorySut(_dbContextMock.Object);
        }

        [Fact]
        public void UnitOfWork_IfPassingNullDbContext_ThrowsArgumentNullException()
        {
            // arrange
            DbContext nullDbContext = null;

            // act, assert
            Assert.Throws<ArgumentNullException>(() => _sut = new TestRepositorySut(nullDbContext));
        }

        [Fact]
        public void FindAll_IfTrackChanges_FindsAllSuccess()
        {
            // arrange
            const bool trackChanges = true;

            // act
            _sut.FindAll(trackChanges);

            // assert
            _dbContextMock.Verify(_ => _.Set<BaseEntity>(), Times.Once);
        }

        [Fact]
        public void FindAll_IfNotTrackChanges_FindsAllSuccess()
        {
            // arrange
            const bool trackChanges = false;

            // act
            _sut.FindAll(trackChanges);

            // assert
            _dbContextMock.Verify(_ => _.Set<BaseEntity>(), Times.Once);
        }

        [Fact]
        public void FindByConditionTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact]
        public void FindByIdTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact]
        public void InsertTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact]
        public void DeleteTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact]
        public void DeleteTest1()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact]
        public void UpdateTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact]
        public void DisposeTest()
        {
            Assert.True(false, "This test needs an implementation");
        }
    }
}