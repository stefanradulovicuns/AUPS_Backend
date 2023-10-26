using AUPS_Backend.DTO;
using AUPS_Backend.Entities;
using AutoMapper;

namespace AUPS_Backend.Profiles
{
    public class ObjectOfLaborProfile : Profile
    {
        public ObjectOfLaborProfile()
        {
            CreateMap<ObjectOfLabor, ObjectOfLaborDTO>()
                .ForMember(dest => dest.WarehouseFullAddress,
                opt => opt.MapFrom(src => src.Warehouse.City + ", " + src.Warehouse.Address));
            CreateMap<ObjectOfLaborCreateDTO, ObjectOfLabor>();
            CreateMap<ObjectOfLaborUpdateDTO, ObjectOfLabor>();
        }
    }
}
