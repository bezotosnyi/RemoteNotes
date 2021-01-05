using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using RemoteNotes.BLL.Contract;
using RemoteNotes.DAL.Contact;
using RemoteNotes.DAL.Domain.Entities;
using RemoteNotes.Logging.Contract;
using RemoteNotes.Service.Domain.DTO;

namespace RemoteNotes.BLL.Services
{
    [ExcludeFromCodeCoverage] // no logic yet
    public class AccountService : ServiceBase<Account, AccountDTO>, IAccountService
    {
        public AccountService(IRemoteNotesLogger<ServiceBase<Account, AccountDTO>> logger, IMapper mapper,
            IUnitOfWork unitOfWork, IRepository<Account> currentRepository) : base(logger, mapper, unitOfWork,
            currentRepository)
        {
        }
    }
}