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
    }
}