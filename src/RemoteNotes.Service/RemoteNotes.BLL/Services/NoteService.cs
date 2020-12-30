using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using RemoteNotes.BLL.Contract;
using RemoteNotes.DAL.Contact;
using RemoteNotes.DAL.Domain.Entities;
using RemoteNotes.Logging.Contract;
using RemoteNotes.Service.Domain.DTO;

namespace RemoteNotes.BLL.Services
{
    public class NoteService : ServiceBase<Note, NoteDTO>, INoteService
    {
        public NoteService(IRemoteNotesLogger<ServiceBase<Note, NoteDTO>> logger, IMapper mapper,
            IUnitOfWork unitOfWork, IRepository<Note> currentRepository) : base(logger, mapper, unitOfWork,
            currentRepository)
        {
        }

        public IEnumerable<NoteDTO> GetNotes(int accountId) =>
            _currentRepository.FindByCondition(_ => _.UserId == accountId, false).AsEnumerable()
                .Select(_mapper.Map<Note, NoteDTO>);
    }
}