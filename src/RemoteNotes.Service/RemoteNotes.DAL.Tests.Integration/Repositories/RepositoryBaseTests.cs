using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RemoteNotes.Tests.Fixture;
using Xunit;

namespace RemoteNotes.DAL.Tests.Integration.Repositories
{
    public class RepositoryBaseTests : IClassFixture<InMemoryTestDbContextFixture>
    {
        private const int Count = 1000;
        private readonly List<TestEntity> _testEntities;
        private readonly TestDbContext _dbContext;
        private TestRepository _sut;

        public RepositoryBaseTests(InMemoryTestDbContextFixture inMemoryTestDbContextFixture)
        {
            _dbContext = inMemoryTestDbContextFixture.DbContext;
            _testEntities = GenerateTestEntities(Count);
            _dbContext.TestEntities.AddRange(_testEntities);
            _dbContext.SaveChanges();

            _sut = new TestRepository(_dbContext);
        }

        [Fact]
        public void UnitOfWork_IfPassingNullDbContext_ThrowsArgumentNullException()
        {
            // arrange
            TestDbContext nullDbContext = null;

            // act, assert
            Assert.Throws<ArgumentNullException>(() => _sut = new TestRepository(nullDbContext));
        }

        [Fact]
        public void FindAll_IfNotTrackChanges_FindsAllSuccess()
        {
            // arrange
            const bool trackChanges = false;

            // act
            var entities = _sut.FindAll(trackChanges).ToList();

            // assert
            Assert.Equal(Count, entities.Count);
            Assert.True(entities.SequenceEqual(_testEntities));
            DetachedEntities(entities);
        }

        [Fact]
        public void FindAll_IfTrackChanges_FindsAllSuccess()
        {
            // arrange
            const bool trackChanges = true;

            // act
            var entities = _sut.FindAll(trackChanges).ToList();

            // assert
            Assert.Equal(Count, entities.Count);
            Assert.True(entities.SequenceEqual(_testEntities));
            DetachedEntities(entities);
        }

        /*[Fact]
        public void FindByCondition_IfTrackChanges_FindsByConditionSuccess()
        {
            // arrange
            const bool trackChanges = true;
            Expression<Func<TestEntity, bool>> condition = _ => _.Id < 10;

            // act
            var entities = _sut.FindAll(trackChanges).ToList();

            // assert
            Assert.True(entities.SequenceEqual(_testEntities.Where(condition.Compile()).ToList()));
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
        }*/

        private void DetachedEntities(IEnumerable<TestEntity> entities)
        {
            foreach (var entity in entities)
            {
                _dbContext.Entry(entity).State = EntityState.Detached;
            }
        }

        private static List<TestEntity> GenerateTestEntities(int count)
        {
            var testEntities = new List<TestEntity>();
            for (var i = 0; i < count; i++)
            {
                testEntities.Add(new TestEntity { Title = Guid.NewGuid().ToString() });
            }

            return testEntities;
        }
    }
}