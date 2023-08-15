using AUPS_Backend.DTO;
using AUPS_Backend.Entities;
using AutoMapper;

namespace AUPS_Backend.Profiles
{
    public class ProductionOrderProfile : Profile
    {
        public ProductionOrderProfile()
        {
            CreateMap<ProductionOrder, ProductionOrderDTO>();
            CreateMap<ProductionOrderCreateDTO, ProductionOrder>();
            CreateMap<ProductionOrderUpdateDTO, ProductionOrder>();
        }
    }
}
