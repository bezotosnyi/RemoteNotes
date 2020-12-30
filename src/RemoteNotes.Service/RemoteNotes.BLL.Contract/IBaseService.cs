using System.Collections.Generic;
using RemoteNotes.DAL.Domain.Entities;
using RemoteNotes.Service.Domain.DTO;

namespace RemoteNotes.BLL.Contract
{
    public interface IBaseService<DomainEntity, DTO>
        where DomainEntity : BaseEntity, new() where DTO : BaseEntityDTO, new()
    {
        DTO Add(DTO entity);

        bool Delete(int id);

        DTO Get(int id);

        IEnumerable<DTO> Get();

        DTO Update(DTO entity);
    }
}