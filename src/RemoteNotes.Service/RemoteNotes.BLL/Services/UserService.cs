using AutoMapper;
using RemoteNotes.BLL.Contract;
using RemoteNotes.DAL.Contact;
using RemoteNotes.Logging.Contract;
using RemoteNotes.Service.Domain.DTO;

namespace RemoteNotes.BLL.Services
{
    public class UserService : ServiceBase<DAL.Domain.Entities.User, UserDTO>, IUserService
    {
        public UserService(IRemoteNotesLogger<ServiceBase<DAL.Domain.Entities.User, UserDTO>> logger, IMapper mapper,
            IUnitOfWork unitOfWork, IRepository<DAL.Domain.Entities.User> currentRepository) : base(logger, mapper, unitOfWork,
            currentRepository)
        {
        }
    }
}