using AUPS_Backend.DTO;
using AUPS_Backend.Entities;
using AutoMapper;

namespace AUPS_Backend.Profiles
{
    public class TechnologicalProcedureProfile : Profile
    {
        public TechnologicalProcedureProfile()
        {
            CreateMap<TechnologicalProcedure, TechnologicalProcedureDTO>()
                .ForMember(dest => dest.OrganizationalUnitName,
                opt => opt.MapFrom(src => src.OrganizationalUnit.OrganizationalUnitName))
                .ForMember(dest => dest.TechnologicalSystemName,
                opt => opt.MapFrom(src => src.TechnologicalSystem.TechnologicalSystemName))
                .ForMember(dest => dest.PlantName,
                opt => opt.MapFrom(src => src.Plant.PlantName));
            CreateMap<TechnologicalProcedureCreateDTO, TechnologicalProcedure>();
            CreateMap<TechnologicalProcedureUpdateDTO, TechnologicalProcedure>();
        }
    }
}
