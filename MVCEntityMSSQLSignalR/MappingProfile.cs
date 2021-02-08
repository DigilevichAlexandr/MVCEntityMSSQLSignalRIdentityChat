using AutoMapper;
using MVCEntityMSSQLSignalR.DAL.Entities;
using MVCEntityMSSQLSignalR.Models;

namespace MVCEntityMSSQLSignalR
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<File, FileViewModel>();
        }
    }
}
