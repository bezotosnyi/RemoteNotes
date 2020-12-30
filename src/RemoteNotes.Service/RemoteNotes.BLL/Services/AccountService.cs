using AutoMapper;
using RemoteNotes.BLL.Contract;
using RemoteNotes.DAL.Contact;
using RemoteNotes.Logging.Contract;
using RemoteNotes.Service.Domain.DTO;

namespace RemoteNotes.BLL.Services
{
    public class AccountService : ServiceBase<DAL.Domain.Entities.Account, AccountDTO>, IAccountService
    {
        public AccountService(IRemoteNotesLogger<ServiceBase<DAL.Domain.Entities.Account, AccountDTO>> logger, IMapper mapper,
            IUnitOfWork unitOfWork, IRepository<DAL.Domain.Entities.Account> currentRepository) : base(logger, mapper, unitOfWork,
            currentRepository)
        {
        }
    }
}