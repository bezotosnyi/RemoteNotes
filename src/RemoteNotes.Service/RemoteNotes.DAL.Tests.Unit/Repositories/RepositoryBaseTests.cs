using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using RemoteNotes.DAL.Domain.Entities;
using RemoteNotes.Tests.Fixture;
using Xunit;

namespace RemoteNotes.DAL.Tests.Unit.Repositories
{
    public class RepositoryBaseTests
    {
        private readonly Mock<DbContext> _dbContextMock;
        private TestRepository _sut;

        public RepositoryBaseTests()
        {
            _dbContextMock = new Mock<DbContext>(MockBehavior.Strict);
            _sut = new TestRepository(_dbContextMock.Object);
        }

        [Fact]
        public void UnitOfWork_IfPassingNullDbContext_ThrowsArgumentNullException()
        {
            // arrange
            TestDbContext nullDbContext = null;

            // act, assert
            Assert.Throws<ArgumentNullException>(() => _sut = new TestRepository(nullDbContext));
        }

        [Theory]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(1000)]
        public void FindAll_IfFindAll_FindsAllSuccess(int entityCount)
        {
            // arrange
            var testEntities = GenerateTestEntities(entityCount).ToList();
            var dbSetMock = GetQueryableMockDbSet(testEntities);
            _dbContextMock.Setup(_ => _.Set<TestEntity>()).Returns(dbSetMock.Object);

            // act
            var entities = _sut.FindAll().ToList();

            // assert
            Assert.Equal(entityCount, entities.Count);
            Assert.True(entities.SequenceEqual(testEntities));
            _dbContextMock.Verify(_ => _.Set<TestEntity>(), Times.Once);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(1000)]
        public void FindByCondition_IfTrackChanges_FindsByConditionSuccess(int entityCount)
        {
            // arrange
            var testEntities = GenerateTestEntities(entityCount).ToList();
            var dbSetMock = GetQueryableMockDbSet(testEntities);
            Expression<Func<TestEntity, bool>> condition = _ => _.Id % 2 == 0;
            _dbContextMock.Setup(_ => _.Set<TestEntity>()).Returns(dbSetMock.Object);

            // act
            var entities = _sut.FindByCondition(condition).ToList();

            // assert
            Assert.Equal(entityCount, entities.Count);
            Assert.True(entities.SequenceEqual(testEntities.Where(condition.Compile())));
            _dbContextMock.Verify(_ => _.Set<TestEntity>(), Times.Once);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(1000)]
        public void FindById_IfFindById_FindsByIdSuccess(int entityCount)
        {
            // arrange
            var testEntities = GenerateTestEntities(entityCount).ToList();
            var dbSetMock = GetQueryableMockDbSet(testEntities);
            var id = new Random().Next(0, testEntities.Count - 1);
            var expectedEntity = testEntities.FirstOrDefault(_ => _.Id == id);
            _dbContextMock.Setup(_ => _.Set<TestEntity>()).Returns(dbSetMock.Object);
            dbSetMock.Setup(_ => _.Find(id)).Returns(expectedEntity);

            // act
            var actualEntity = _sut.FindById(id);

            // assert
            Assert.Equal(expectedEntity, actualEntity);
            _dbContextMock.Verify(_ => _.Set<TestEntity>(), Times.Once);
            dbSetMock.Verify(_ => _.Find(id), Times.Once);
        }

        [Fact]
        public void Insert_IfInsert_InsertSuccess()
        {
            // arrange
            var testEntities = GenerateTestEntities(0).ToList();
            var dbSetMock = GetQueryableMockDbSet(testEntities);
            var entity = new TestEntity {Id = 0, Title = Guid.NewGuid().ToString()};
            _dbContextMock.Setup(_ => _.Set<TestEntity>()).Returns(dbSetMock.Object);
            dbSetMock.Setup(_ => _.Add(entity)).Callback<TestEntity>(_ => testEntities.Add(_)).Returns(It.IsAny<EntityEntry<TestEntity>>());

            // act
            _sut.Insert(entity);

            // assert
            Assert.True(testEntities.Count == 1);
            Assert.Equal(entity, testEntities[0]);
            _dbContextMock.Verify(_ => _.Set<TestEntity>(), Times.Once);
            dbSetMock.Verify(_ => _.Add(entity), Times.Once);
        }

        [Fact]
        public void Delete_IfDelete_DeleteSuccess()
        {
            // arrange
            var testEntities = GenerateTestEntities(1).ToList();
            var entity = testEntities[0];
            var dbSetMock = GetQueryableMockDbSet(testEntities);
            _dbContextMock.Setup(_ => _.Set<TestEntity>()).Returns(dbSetMock.Object);
            dbSetMock.Setup(_ => _.Remove(entity)).Callback<TestEntity>(_ => testEntities.Remove(entity)).Returns(It.IsAny<EntityEntry<TestEntity>>());
            dbSetMock.Setup(_ => _.Find(entity.Id)).Returns(entity);

            // act
            _sut.Delete(entity.Id);

            // assert
            Assert.True(testEntities.Count == 0);
            _dbContextMock.Verify(_ => _.Set<TestEntity>(), Times.Exactly(2));
            dbSetMock.Verify(_ => _.Remove(entity), Times.Once);
            dbSetMock.Verify(_ => _.Find(entity.Id), Times.Once);
        }

        [Fact]
        public void Update_IfUpdate_UpdatesSuccess()
        {
            // arrange
            var testEntities = GenerateTestEntities(1).ToList();
            var dbSetMock = GetQueryableMockDbSet(testEntities);
            var entityToUpdate = testEntities[0];
            entityToUpdate.Title = Guid.NewGuid().ToString();
            _dbContextMock.Setup(_ => _.Set<TestEntity>()).Returns(dbSetMock.Object);
            dbSetMock.Setup(_ => _.Update(entityToUpdate)).Callback<TestEntity>(_ => testEntities[0] = _).Returns(It.IsAny<EntityEntry<TestEntity>>());

            // act
            _sut.Update(entityToUpdate);

            // assert
            Assert.True(testEntities.Count == 1);
            Assert.Equal(testEntities[0], entityToUpdate);
            _dbContextMock.Verify(_ => _.Set<TestEntity>(), Times.Once);
            dbSetMock.Verify(_ => _.Update(entityToUpdate), Times.Once);
        }

        [Fact]
        public void Dispose_IfDispose_DisposesSuccess()
        {
            // arrange
            _dbContextMock.Setup(_ => _.Dispose());

            // act
            _sut.Dispose();

            // assert
            _dbContextMock.Verify(_ => _.Dispose(), Times.Once);
        }

        private static IEnumerable<TestEntity> GenerateTestEntities(int count)
        {
            for (var i = 0; i < count; i++)
            {
                yield return new TestEntity { Id = 0, Title = Guid.NewGuid().ToString()};
            }
        }

        private static Mock<DbSet<T>> GetQueryableMockDbSet<T>(ICollection<T> sourceList) where T : BaseEntity
        {
            var queryable = sourceList.AsQueryable();
            var dbSet = new Mock<DbSet<T>>(MockBehavior.Strict);
            dbSet.As<IQueryable<T>>().Setup(_ => _.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(_ => _.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(_ => _.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(_ => _.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            dbSet.Setup(_ => _.Add(It.IsAny<T>())).Callback<T>(sourceList.Add);
            return dbSet;
        }
    }
}