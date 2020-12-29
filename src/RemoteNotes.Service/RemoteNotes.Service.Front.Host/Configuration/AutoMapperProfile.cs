using AutoMapper;
using RemoteNotes.DAL.Core.Entities;
using RemoteNotes.Service.Domain.DTO;

namespace RemoteNotes.Service.Front.Host.Configuration
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Account, AccountDTO>().ReverseMap();
            CreateMap<Note, NoteDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}