using AUPS_Backend.DTO;
using AUPS_Backend.Entities;
using AutoMapper;

namespace AUPS_Backend.Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDTO>()
                .ForMember(dest => dest.WorkplaceName,
                opt => opt.MapFrom(src => src.Workplace.WorkplaceName))
                .ForMember(dest => dest.OrganizationalUnitName,
                opt => opt.MapFrom(src => src.OrganizationalUnit.OrganizationalUnitName));
            CreateMap<EmployeeCreateDTO, Employee>();
            CreateMap<EmployeeUpdateDTO, Employee>();
        }
    }
}
