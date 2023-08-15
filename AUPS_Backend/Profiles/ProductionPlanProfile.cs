using AUPS_Backend.DTO;
using AUPS_Backend.Entities;
using AutoMapper;

namespace AUPS_Backend.Profiles
{
    public class ProductionPlanProfile : Profile
    {
        public ProductionPlanProfile()
        {
            CreateMap<ProductionPlan, ProductionPlanDTO>();
            CreateMap<ProductionPlanCreateDTO, ProductionPlan>();
            CreateMap<ProductionPlanUpdateDTO, ProductionPlan>();
        }
    }
}
