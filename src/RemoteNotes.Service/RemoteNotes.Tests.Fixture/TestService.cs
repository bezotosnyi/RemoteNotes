using AutoMapper;
using RemoteNotes.BLL.Services;
using RemoteNotes.DAL.Contact;
using RemoteNotes.Logging.Contract;

namespace RemoteNotes.Tests.Fixture
{
    public class TestService : ServiceBase<TestEntity, TestDTO>
    {
        public TestService(IRemoteNotesLogger<ServiceBase<TestEntity, TestDTO>> logger, IMapper mapper,
            IUnitOfWork unitOfWork, IRepository<TestEntity> currentRepository) : base(logger, mapper, unitOfWork,
            currentRepository)
        {
        }
    }
}