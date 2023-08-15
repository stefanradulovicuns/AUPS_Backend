using AUPS_Backend.DTO;
using AUPS_Backend.Entities;
using AutoMapper;

namespace AUPS_Backend.Profiles
{
    public class ObjectOfLaborProfile : Profile
    {
        public ObjectOfLaborProfile()
        {
            CreateMap<ObjectOfLabor, ObjectOfLaborDTO>();
            CreateMap<ObjectOfLaborCreateDTO, ObjectOfLabor>();
            CreateMap<ObjectOfLaborUpdateDTO, ObjectOfLabor>();
        }
    }
}
