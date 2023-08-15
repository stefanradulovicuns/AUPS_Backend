using AUPS_Backend.DTO;
using AUPS_Backend.Entities;
using AutoMapper;

namespace AUPS_Backend.Profiles
{
    public class TechnologicalProcedureProfile : Profile
    {
        public TechnologicalProcedureProfile()
        {
            CreateMap<TechnologicalProcedure, TechnologicalProcedureDTO>();
            CreateMap<TechnologicalProcedureCreateDTO, TechnologicalProcedure>();
            CreateMap<TechnologicalProcedureUpdateDTO, TechnologicalProcedure>();
        }
    }
}
