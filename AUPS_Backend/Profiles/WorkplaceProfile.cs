using AUPS_Backend.DTO;
using AUPS_Backend.Entities;
using AutoMapper;

namespace AUPS_Backend.Profiles
{
    public class WorkplaceProfile : Profile
    {
        public WorkplaceProfile()
        {
            CreateMap<Workplace, WorkplaceDTO>();
            CreateMap<WorkplaceCreateDTO, Workplace>();
            CreateMap<WorkplaceUpdateDTO, Workplace>();
        }
    }
}
