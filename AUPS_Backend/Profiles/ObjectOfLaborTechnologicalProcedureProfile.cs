using AUPS_Backend.DTO;
using AUPS_Backend.Entities;
using AutoMapper;

namespace AUPS_Backend.Profiles
{
    public class ObjectOfLaborTechnologicalProcedureProfile : Profile
    {
        public ObjectOfLaborTechnologicalProcedureProfile()
        {
            CreateMap<ObjectOfLaborTechnologicalProcedure, ObjectOfLaborTechnologicalProcedureDTO>();
            CreateMap<ObjectOfLaborTechnologicalProcedureCreateDTO, ObjectOfLaborTechnologicalProcedure>();
            CreateMap<ObjectOfLaborTechnologicalProcedureUpdateDTO, ObjectOfLaborTechnologicalProcedure>();
        }
    }
}
