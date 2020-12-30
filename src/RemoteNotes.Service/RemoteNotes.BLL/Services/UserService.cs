using System;
using System.Linq;
using AutoMapper;
using RemoteNotes.BLL.Contract;
using RemoteNotes.DAL.Contact;
using RemoteNotes.DAL.Domain.Entities;
using RemoteNotes.Logging.Contract;
using RemoteNotes.Service.Domain.DTO;

namespace RemoteNotes.BLL.Services
{
    public class UserService : ServiceBase<User, UserDTO>, IUserService
    {
        public UserService(IRemoteNotesLogger<ServiceBase<User, UserDTO>> logger, IMapper mapper,
            IUnitOfWork unitOfWork, IRepository<User> currentRepository) : base(logger, mapper, unitOfWork,
            currentRepository)
        {
        }

        public UserDTO Login(string login, string password) =>
            _currentRepository
                .FindByCondition(_ => _.Login.Equals(login) && _.Password.Equals(password))
                .AsEnumerable()
                .Select(_mapper.Map<User, UserDTO>)
                .First();

        public UserDTO Registration(UserDTO user)
        {
            var domainUser = _mapper.Map<UserDTO, User>(user);
            if (_currentRepository
                .FindByCondition(_ => _.Login.Equals(domainUser.Login))
                .Any())
            {
                throw new OperationCanceledException($"User {domainUser.Login} has already bean exist.");
            }

            _currentRepository.Insert(domainUser);
            _unitOfWork.Commit();
            return _mapper.Map<User, UserDTO>(domainUser);
        }
    }
}