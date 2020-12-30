using RemoteNotes.DAL.Domain.Entities;
using RemoteNotes.Service.Domain.DTO;

namespace RemoteNotes.BLL.Contract
{
    public interface IUserService : IServiceBase<User, UserDTO>
    {
    }
}