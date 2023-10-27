using AUPS_Backend.DTO;
using AUPS_Backend.Entities;
using AutoMapper;

namespace AUPS_Backend.Profiles
{
    public class ProductionOrderProfile : Profile
    {
        public ProductionOrderProfile()
        {
            CreateMap<ProductionOrder, ProductionOrderDTO>()
                .ForMember(dest => dest.ObjectOfLaborName,
                opt => opt.MapFrom(src => src.ObjectOfLabor.ObjectOfLaborName))
                .ForMember(dest => dest.Manager,
                opt => opt.MapFrom(src => src.Employee.FirstName + " " + src.Employee.LastName))
                .ForMember(dest => dest.ManagerEmail,
                opt => opt.MapFrom(src => src.Employee.Email));
            CreateMap<ProductionOrderCreateDTO, ProductionOrder>();
            CreateMap<ProductionOrderUpdateDTO, ProductionOrder>();
        }
    }
}
