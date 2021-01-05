using System;
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
    public abstract class ServiceBase<DomainEntity, DTO> : IServiceBase<DomainEntity, DTO>
        where DomainEntity : BaseEntity, new()
        where DTO : BaseEntityDTO, new()
    {
        protected readonly IRemoteNotesLogger<ServiceBase<DomainEntity, DTO>> _logger;
        protected readonly IMapper _mapper;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IRepository<DomainEntity> _currentRepository;

        protected ServiceBase(IRemoteNotesLogger<ServiceBase<DomainEntity, DTO>> logger, IMapper mapper, IUnitOfWork unitOfWork,
            IRepository<DomainEntity> currentRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _currentRepository = currentRepository ?? throw new ArgumentNullException(nameof(currentRepository));
        }

        public virtual DTO Add(DTO entity)
        {
            var entityToAdd = _mapper.Map<DTO, DomainEntity>(entity);
            _currentRepository.Insert(entityToAdd);
            _unitOfWork.Commit();
            return _mapper.Map<DomainEntity, DTO>(entityToAdd);
        }

        public virtual bool Delete(int id)
        {
            bool deleteResult;
            var entityToDelete = _currentRepository.FindById(id);
            if (entityToDelete == null)
            {
                deleteResult = false;
            }
            else
            {
                _currentRepository.Delete(id);
                _unitOfWork.Commit();
                deleteResult = true;
            }

            return deleteResult;
        }

        public virtual DTO Get(int id)
        {
            var entity = _currentRepository.FindById(id);
            return _mapper.Map<DomainEntity, DTO>(entity);
        }

        public virtual IEnumerable<DTO> Get()
        {
            var entities = _currentRepository.FindAll();
            return entities.AsEnumerable().Select(_mapper.Map<DomainEntity, DTO>);
        }

        public virtual DTO Update(DTO entity)
        {
            var changedDomainEntity = _mapper.Map<DTO, DomainEntity>(entity);
            _currentRepository.Update(changedDomainEntity);
            _unitOfWork.Commit();
            return _mapper.Map<DomainEntity, DTO>(changedDomainEntity);
        }
    }
}