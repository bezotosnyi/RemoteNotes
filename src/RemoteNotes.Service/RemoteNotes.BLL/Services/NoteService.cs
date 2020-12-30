using AutoMapper;
using RemoteNotes.BLL.Contract;
using RemoteNotes.DAL.Contact;
using RemoteNotes.Logging.Contract;
using RemoteNotes.Service.Domain.DTO;

namespace RemoteNotes.BLL.Services
{
    public class NoteService : ServiceBase<DAL.Domain.Entities.Note, NoteDTO>, INoteService
    {
        public NoteService(IRemoteNotesLogger<ServiceBase<DAL.Domain.Entities.Note, NoteDTO>> logger, IMapper mapper,
            IUnitOfWork unitOfWork, IRepository<DAL.Domain.Entities.Note> currentRepository) : base(logger, mapper, unitOfWork,
            currentRepository)
        {
        }
    }
}