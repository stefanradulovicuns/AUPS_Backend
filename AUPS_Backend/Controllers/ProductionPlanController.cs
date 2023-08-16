using AUPS_Backend.DTO;
using AUPS_Backend.Entities;
using AUPS_Backend.Enums;
using AUPS_Backend.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AUPS_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionPlanController : ControllerBase
    {
        private readonly IProductionPlanRepository _productionPlanRepository;
        private readonly IMapper _mapper;

        public ProductionPlanController(IProductionPlanRepository productionPlanRepository, IMapper mapper)
        {
            _productionPlanRepository = productionPlanRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ProductionPlanDTO>> GetProductionPlans(string? search, string? sortBy, SortOrderOptions? sortOrder, int page, int count)
        {
            var productionPlans = await _productionPlanRepository.GetAllProductionPlans();

            if (!string.IsNullOrEmpty(search))
            {
                productionPlans = productionPlans
                    .Where(pp => pp.ProductionPlanName.Contains(search, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            productionPlans = (sortBy, sortOrder) switch
            {
                (nameof(ProductionPlanDTO.ProductionPlanId), SortOrderOptions.ASC) => productionPlans.OrderBy(pp => pp.ProductionPlanId).ToList(),
                (nameof(ProductionPlanDTO.ProductionPlanId), SortOrderOptions.DESC) => productionPlans.OrderByDescending(pp => pp.ProductionPlanId).ToList(),
                (nameof(ProductionPlanDTO.ProductionPlanName), SortOrderOptions.ASC) => productionPlans.OrderBy(pp => pp.ProductionPlanName).ToList(),
                (nameof(ProductionPlanDTO.ProductionPlanName), SortOrderOptions.DESC) => productionPlans.OrderByDescending(pp => pp.ProductionPlanName).ToList(),
                (nameof(ProductionPlanDTO.Description), SortOrderOptions.ASC) => productionPlans.OrderBy(pp => pp.Description).ToList(),
                (nameof(ProductionPlanDTO.Description), SortOrderOptions.DESC) => productionPlans.OrderByDescending(pp => pp.Description).ToList(),
                (nameof(ProductionPlanDTO.ObjectOfLaborId), SortOrderOptions.ASC) => productionPlans.OrderBy(pp => pp.ObjectOfLaborId).ToList(),
                (nameof(ProductionPlanDTO.ObjectOfLaborId), SortOrderOptions.DESC) => productionPlans.OrderByDescending(pp => pp.ObjectOfLaborId).ToList(),
                _ => productionPlans.OrderBy(pp => pp.ProductionPlanName).ToList(),
            };

            int totalCount = productionPlans.Count();
            productionPlans = productionPlans.Skip(page > 0 ? (page - 1) * count : 0)
                .Take(count > 0 ? count : totalCount)
                .ToList();

            if (!productionPlans.Any())
            {
                return NoContent();
            }

            var productionPlansDto = _mapper.Map<List<ProductionPlanDTO>>(productionPlans);
            productionPlansDto[0].TotalCount = totalCount;

            return Ok(productionPlansDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductionPlanDTO>> GetProductionPlan(Guid id)
        {
            var productionPlan = await _productionPlanRepository.GetProductionPlanById(id);

            if (productionPlan == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ProductionPlanDTO>(productionPlan));
        }

        [HttpPost]
        public async Task<ActionResult<ProductionPlanDTO>> CreateProductionPlan(ProductionPlanCreateDTO productionPlan)
        {
            var createdProductionPlan = await _productionPlanRepository.AddProductionPlan(_mapper.Map<ProductionPlan>(productionPlan));

            return CreatedAtAction("GetProductionPlan", new { id = createdProductionPlan.ProductionPlanId }, _mapper.Map<ProductionPlanDTO>(createdProductionPlan));
        }

        [HttpPut]
        public async Task<ActionResult<ProductionPlanDTO>> UpdateProductionPlan(ProductionPlanUpdateDTO productionPlan)
        {
            var matchingProductionPlan = await _productionPlanRepository.GetProductionPlanById(productionPlan.ProductionPlanId);
            if (matchingProductionPlan == null)
            {
                return NotFound();
            }

            var updatedProductionPlan = await _productionPlanRepository.UpdateProductionPlan(_mapper.Map<ProductionPlan>(productionPlan));

            return Ok(_mapper.Map<ProductionPlanDTO>(updatedProductionPlan));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductionPlan(Guid id)
        {
            bool isDeleted = await _productionPlanRepository.DeleteProductionPlan(id);
            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
