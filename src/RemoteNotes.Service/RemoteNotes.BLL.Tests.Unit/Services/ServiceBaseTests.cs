using System;
using System.Linq;
using AutoMapper;
using Moq;
using RemoteNotes.BLL.Services;
using RemoteNotes.DAL.Contact;
using RemoteNotes.Logging.Contract;
using RemoteNotes.Tests.Fixture;
using Xunit;

namespace RemoteNotes.BLL.Tests.Unit.Services
{
    public class ServiceBaseTests
    {
        private readonly Mock<IRemoteNotesLogger<ServiceBase<TestEntity, TestDTO>>> _loggerMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IRepository<TestEntity>> _currentRepositoryMock;
        private TestService _sut;

        public ServiceBaseTests()
        {
            _loggerMock = new Mock<IRemoteNotesLogger<ServiceBase<TestEntity, TestDTO>>>(MockBehavior.Strict);
            _mapperMock = new Mock<IMapper>(MockBehavior.Strict);
            _unitOfWorkMock = new Mock<IUnitOfWork>(MockBehavior.Strict);
            _currentRepositoryMock = new Mock<IRepository<TestEntity>>(MockBehavior.Strict);

            _sut = new TestService(_loggerMock.Object, _mapperMock.Object, _unitOfWorkMock.Object, _currentRepositoryMock.Object);
        }

        [Fact]
        public void ServiceBase_IfPassingNullDbContext_ThrowsArgumentNullException()
        {
            // act, assert
            Assert.Throws<ArgumentNullException>(() => _sut = new TestService(_loggerMock.Object, null, null, null));
            Assert.Throws<ArgumentNullException>(() =>
                _sut = new TestService(_loggerMock.Object, _mapperMock.Object, null, null));
            Assert.Throws<ArgumentNullException>(() =>
                _sut = new TestService(_loggerMock.Object, _mapperMock.Object, _unitOfWorkMock.Object, null));
        }

        [Fact]
        public void Add_IfAdd_AddsSuccess()
        {
            // arrange
            var entity = new TestEntity
            {
                Id = 1,
                Title = Guid.NewGuid().ToString()
            };
            var dto = new TestDTO
            {
                Id = 1,
                Title = Guid.NewGuid().ToString()
            };
            _mapperMock.Setup(_ => _.Map<TestDTO, TestEntity>(dto)).Returns(entity);
            _currentRepositoryMock.Setup(_ => _.Insert(entity));
            _unitOfWorkMock.Setup(_ => _.Commit());
            _mapperMock.Setup(_ => _.Map<TestEntity, TestDTO>(entity)).Returns(dto);

            // act
            var result = _sut.Add(dto);

            // assert
            Assert.Equal(dto, result);
            _mapperMock.Verify(_ => _.Map<TestDTO, TestEntity>(dto), Times.Once);
            _currentRepositoryMock.Verify(_ => _.Insert(entity), Times.Once);
            _unitOfWorkMock.Verify(_ => _.Commit(), Times.Once);
            _mapperMock.Verify(_ => _.Map<TestEntity, TestDTO>(entity), Times.Once);
        }

        [Fact]
        public void Delete_IfEntityToDeleteEqualNull_ReturnsFalse()
        {
            // arrange
            const int id = 1;
            TestEntity testEntity = null;
            _currentRepositoryMock.Setup(_ => _.FindById(id)).Returns(testEntity);
            const bool expectedDeleteResult = false;

            // act
            var actualDeleteResult = _sut.Delete(id);

            // assert
            _currentRepositoryMock.Verify(_ => _.FindById(id), Times.Once);
            Assert.Equal(expectedDeleteResult, actualDeleteResult);
        }

        [Fact]
        public void Delete_IfDelete_DeletesSuccess()
        {
            // arrange
            const int id = 1;
            var entity = new TestEntity
            {
                Id = id,
                Title = Guid.NewGuid().ToString()
            };
            _currentRepositoryMock.Setup(_ => _.FindById(id)).Returns(entity);
            _currentRepositoryMock.Setup(_ => _.Delete(id));
            _unitOfWorkMock.Setup(_ => _.Commit());
            const bool expectedDeleteResult = true;

            // act
            var actualDeleteResult = _sut.Delete(id);

            // assert
            _currentRepositoryMock.Verify(_ => _.FindById(id), Times.Once);
            _currentRepositoryMock.Verify(_ => _.Delete(id), Times.Once);
            _unitOfWorkMock.Verify(_ => _.Commit(), Times.Once);
            Assert.Equal(expectedDeleteResult, actualDeleteResult);
        }

        [Fact]
        public void Get_IfGetById_GetsByIdSuccess()
        {
            const int id = 1;
            var entity = new TestEntity
            {
                Id = id,
                Title = Guid.NewGuid().ToString()
            };
            var dto = new TestDTO
            {
                Id = id,
                Title = Guid.NewGuid().ToString()
            };
            _currentRepositoryMock.Setup(_ => _.FindById(id)).Returns(entity);
            _mapperMock.Setup(_ => _.Map<TestEntity, TestDTO>(entity)).Returns(dto);

            // act
            var actualDTO = _sut.Get(id);

            // assert
            _currentRepositoryMock.Verify(_ => _.FindById(id), Times.Once);
            _mapperMock.Verify(_ => _.Map<TestEntity, TestDTO>(entity), Times.Once);
            Assert.Equal(dto, actualDTO);
        }

        [Fact]
        public void Get_IfGet_GetsSuccess()
        {
            // arrange
            const int id = 1;
            var title = Guid.NewGuid().ToString();
            var entity = new TestEntity
            {
                Id = id,
                Title = title
            };
            var dto = new TestDTO
            {
                Id = id,
                Title = title
            };
            var entities = new[] { entity };
            var dtos = new[] { dto };
            _currentRepositoryMock.Setup(_ => _.FindAll(false)).Returns(entities.AsQueryable());
            _mapperMock.Setup(_ => _.Map<TestEntity, TestDTO>(entity)).Returns(dto);

            // act
            var actualDTOs = _sut.Get().ToArray();

            // assert
            _currentRepositoryMock.Verify(_ => _.FindAll(false), Times.Once);
            _mapperMock.Verify(_ => _.Map<TestEntity, TestDTO>(entity), Times.Once);
            Assert.NotNull(actualDTOs);
            Assert.True(actualDTOs.Length == 1);
            Assert.True(dtos.SequenceEqual(actualDTOs));
        }

        [Fact]
        public void Update_IfUpdate_UpdatesSuccess()
        {
            // arrange
            var entity = new TestEntity
            {
                Id = 1,
                Title = Guid.NewGuid().ToString()
            };
            var dto = new TestDTO
            {
                Id = 1,
                Title = Guid.NewGuid().ToString()
            };
            _mapperMock.Setup(_ => _.Map<TestDTO, TestEntity>(dto)).Returns(entity);
            _currentRepositoryMock.Setup(_ => _.Update(entity));
            _unitOfWorkMock.Setup(_ => _.Commit());
            _mapperMock.Setup(_ => _.Map<TestEntity, TestDTO>(entity)).Returns(dto);

            // act
            var result = _sut.Update(dto);

            // assert
            Assert.Equal(dto, result);
            _mapperMock.Verify(_ => _.Map<TestDTO, TestEntity>(dto), Times.Once);
            _currentRepositoryMock.Verify(_ => _.Update(entity), Times.Once);
            _unitOfWorkMock.Verify(_ => _.Commit(), Times.Once);
            _mapperMock.Verify(_ => _.Map<TestEntity, TestDTO>(entity), Times.Once);
        }
    }
}