using RemoteNotes.DAL.Domain.Entities;
using RemoteNotes.Service.Domain.DTO;

namespace RemoteNotes.BLL.Contract
{
    public interface IAccountService : IBaseService<Account, AccountDTO>
    {
    }
}