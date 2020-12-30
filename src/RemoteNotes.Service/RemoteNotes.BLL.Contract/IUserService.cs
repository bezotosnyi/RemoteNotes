using RemoteNotes.DAL.Core.Entities;
using RemoteNotes.Service.Domain.DTO;

namespace RemoteNotes.BLL.Contract
{
    public interface IUserService : IBaseService<User, UserDTO>
    {
    }
}