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
                opt => opt.MapFrom(src => src.TechnologicalProcedure.TechnologicalProcedureName));
            CreateMap<ObjectOfLaborTechnologicalProcedureCreateDTO, ObjectOfLaborTechnologicalProcedure>();
            CreateMap<ObjectOfLaborTechnologicalProcedureUpdateDTO, ObjectOfLaborTechnologicalProcedure>();
        }
    }
}
