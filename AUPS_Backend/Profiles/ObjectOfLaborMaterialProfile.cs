using AUPS_Backend.DTO;
using AUPS_Backend.Entities;
using AutoMapper;

namespace AUPS_Backend.Profiles
{
    public class ObjectOfLaborMaterialProfile : Profile
    {
        public ObjectOfLaborMaterialProfile()
        {
            CreateMap<ObjectOfLaborMaterial, ObjectOfLaborMaterialDTO>()
                .ForMember(dest => dest.MaterialName,
                opt => opt.MapFrom(src => src.Material.MaterialName))
                .ForMember(dest => dest.StockQuantity,
                opt => opt.MapFrom(src => src.Material.StockQuantity));
            CreateMap<ObjectOfLaborMaterialCreateDTO, ObjectOfLaborMaterial>();
            CreateMap<ObjectOfLaborMaterialUpdateDTO, ObjectOfLaborMaterial>();
        }
    }
}
