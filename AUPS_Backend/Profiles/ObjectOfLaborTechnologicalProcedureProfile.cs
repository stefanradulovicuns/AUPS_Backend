using AUPS_Backend.DTO;
using AUPS_Backend.Entities;
using AutoMapper;

namespace AUPS_Backend.Profiles
{
    public class ObjectOfLaborTechnologicalProcedureProfile : Profile
    {
        public ObjectOfLaborTechnologicalProcedureProfile()
        {
            CreateMap<ObjectOfLaborTechnologicalProcedure, ObjectOfLaborTechnologicalProcedureDTO>()
                .ForMember(dest => dest.TechnologicalProcedureName,
                opt => opt.MapFrom(src => src.TechnologicalProcedure.TechnologicalProcedureName))
                .ForMember(dest => dest.TechnologicalSystemName,
                opt => opt.MapFrom(src => src.TechnologicalProcedure.TechnologicalSystem.TechnologicalSystemName))
                .ForMember(dest => dest.PlantName,
                opt => opt.MapFrom(src => src.TechnologicalProcedure.Plant.PlantName))
                .ForMember(dest => dest.OrganizationalUnitName,
                opt => opt.MapFrom(src => src.TechnologicalProcedure.OrganizationalUnit.OrganizationalUnitName));
            CreateMap<ObjectOfLaborTechnologicalProcedureCreateDTO, ObjectOfLaborTechnologicalProcedure>();
            CreateMap<ObjectOfLaborTechnologicalProcedureUpdateDTO, ObjectOfLaborTechnologicalProcedure>();
        }
    }
}
