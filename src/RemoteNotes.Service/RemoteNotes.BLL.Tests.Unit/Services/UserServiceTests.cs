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
    public class UserServiceTests
    {
        private readonly Mock<IRemoteNotesLogger<ServiceBase<User, UserDTO>>> _loggerMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IRepository<User>> _currentRepositoryMock;
        private IUserService _sut;

        public UserServiceTests()
        {
            _loggerMock = new Mock<IRemoteNotesLogger<ServiceBase<User, UserDTO>>>(MockBehavior.Strict);
            _mapperMock = new Mock<IMapper>(MockBehavior.Strict);
            _unitOfWorkMock = new Mock<IUnitOfWork>(MockBehavior.Strict);
            _currentRepositoryMock = new Mock<IRepository<User>>(MockBehavior.Strict);

            _sut = new UserService(_loggerMock.Object, _mapperMock.Object, _unitOfWorkMock.Object,
                _currentRepositoryMock.Object);
        }

        [Fact]
        public void Login_IfLogin_LoginsSuccess()
        {
            // arrange
            const string login = "admin";
            const string password = "password";
            const bool trackChanges = false;
            var user = new User();
            var userDTO = new UserDTO();
            var users = new[] {user};
            _currentRepositoryMock.Setup(_ => _.FindByCondition(It.IsAny<Expression<Func<User, bool>>>(), trackChanges))
                .Returns(users.AsQueryable());
            _mapperMock.Setup(_ => _.Map<User, UserDTO>(user)).Returns(userDTO);

            // act
            var actualUser = _sut.Login(login, password);

            // assert
            _currentRepositoryMock.Verify(
                _ => _.FindByCondition(It.IsAny<Expression<Func<User, bool>>>(), trackChanges), Times.Once);
            _mapperMock.Verify(_ => _.Map<User, UserDTO>(user), Times.Once);
            Assert.Equal(userDTO, actualUser);
        }

        [Fact]
        public void Registration_IfRegisterExistUser_ThrowsException()
        {
            // arrange
            const bool trackChanges = false;
            var user = new User();
            var userDTO = new UserDTO();
            var users = new[] { user };
            _mapperMock.Setup(_ => _.Map<UserDTO, User>(userDTO)).Returns(user);
            _currentRepositoryMock.Setup(_ => _.FindByCondition(It.IsAny<Expression<Func<User, bool>>>(), trackChanges))
                .Returns(users.AsQueryable());

            // act, assert
            Assert.Throws<OperationCanceledException>(() => _sut.Registration(userDTO));
            _mapperMock.Verify(_ => _.Map<UserDTO, User>(userDTO), Times.Once);
            _currentRepositoryMock.Verify(
                _ => _.FindByCondition(It.IsAny<Expression<Func<User, bool>>>(), trackChanges), Times.Once);

        }

        [Fact]
        public void Registration_IfRegisterNotExistUser_RegistrationsSuccess()
        {
            // arrange
            const bool trackChanges = false;
            var user = new User();
            var userDTO = new UserDTO();
            var users = Enumerable.Empty<User>();
            _mapperMock.Setup(_ => _.Map<UserDTO, User>(userDTO)).Returns(user);
            _currentRepositoryMock.Setup(_ => _.FindByCondition(It.IsAny<Expression<Func<User, bool>>>(), trackChanges))
                .Returns(users.AsQueryable());
            _currentRepositoryMock.Setup(_ => _.Insert(user));
            _unitOfWorkMock.Setup(_ => _.Commit());
            _mapperMock.Setup(_ => _.Map<User, UserDTO>(user)).Returns(userDTO);

            // act
            var registeredUser = _sut.Registration(userDTO);

            // assert
            _mapperMock.Verify(_ => _.Map<UserDTO, User>(userDTO), Times.Once);
            _currentRepositoryMock.Verify(
                _ => _.FindByCondition(It.IsAny<Expression<Func<User, bool>>>(), trackChanges), Times.Once);
            _currentRepositoryMock.Verify(_ => _.Insert(user), Times.Once);
            _unitOfWorkMock.Verify(_ => _.Commit(), Times.Once);
            _mapperMock.Verify(_ => _.Map<User, UserDTO>(user), Times.Once);
            Assert.Equal(userDTO, registeredUser);
        }
    }
}