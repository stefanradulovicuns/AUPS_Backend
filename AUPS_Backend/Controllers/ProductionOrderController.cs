using AUPS_Backend.DTO;
using AUPS_Backend.Entities;
using AUPS_Backend.Enums;
using AUPS_Backend.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AUPS_Backend.Controllers
{
    [Authorize(Roles = nameof(UserTypeOptions.Admin) + "," + nameof(UserTypeOptions.User))]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionOrderController : ControllerBase
    {
        private readonly IProductionOrderRepository _productionOrderRepository;
        private readonly IMapper _mapper;

        public ProductionOrderController(IProductionOrderRepository productionOrderRepository, IMapper mapper)
        {
            _productionOrderRepository = productionOrderRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ProductionOrderDTO>> GetProductionOrders(string? search, string? sortBy, SortOrderOptions? sortOrder, int page, int count)
        {
            var productionOrders = await _productionOrderRepository.GetAllProductionOrders();
            
            if (!string.IsNullOrEmpty(search))
            {
                productionOrders = productionOrders.Where(po => po.StartDate.ToShortDateString().Contains(search, StringComparison.OrdinalIgnoreCase)
                                                || po.EndDate.ToShortDateString().Contains(search, StringComparison.OrdinalIgnoreCase)
                                                || po.Quantity.ToString().Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            
            productionOrders = (sortBy, sortOrder) switch
            {
                (nameof(ProductionOrderDTO.ProductionOrderId), SortOrderOptions.ASC) => productionOrders.OrderBy(po => po.ProductionOrderId).ToList(),
                (nameof(ProductionOrderDTO.ProductionOrderId), SortOrderOptions.DESC) => productionOrders.OrderByDescending(po => po.ProductionOrderId).ToList(),
                (nameof(ProductionOrderDTO.StartDate), SortOrderOptions.ASC) => productionOrders.OrderBy(po => po.StartDate).ToList(),
                (nameof(ProductionOrderDTO.StartDate), SortOrderOptions.DESC) => productionOrders.OrderByDescending(po => po.StartDate).ToList(),
                (nameof(ProductionOrderDTO.EndDate), SortOrderOptions.ASC) => productionOrders.OrderBy(po => po.EndDate).ToList(),
                (nameof(ProductionOrderDTO.EndDate), SortOrderOptions.DESC) => productionOrders.OrderByDescending(po => po.EndDate).ToList(),
                (nameof(ProductionOrderDTO.Quantity), SortOrderOptions.ASC) => productionOrders.OrderBy(po => po.Quantity).ToList(),
                (nameof(ProductionOrderDTO.Quantity), SortOrderOptions.DESC) => productionOrders.OrderByDescending(po => po.Quantity).ToList(),
                (nameof(ProductionOrderDTO.Note), SortOrderOptions.ASC) => productionOrders.OrderBy(po => po.Note).ToList(),
                (nameof(ProductionOrderDTO.Note), SortOrderOptions.DESC) => productionOrders.OrderByDescending(po => po.Note).ToList(),
                (nameof(ProductionOrderDTO.EmployeeId), SortOrderOptions.ASC) => productionOrders.OrderBy(po => po.EmployeeId).ToList(),
                (nameof(ProductionOrderDTO.EmployeeId), SortOrderOptions.DESC) => productionOrders.OrderByDescending(po => po.EmployeeId).ToList(),
                (nameof(ProductionOrderDTO.ObjectOfLaborId), SortOrderOptions.ASC) => productionOrders.OrderBy(po => po.ObjectOfLaborId).ToList(),
                (nameof(ProductionOrderDTO.ObjectOfLaborId), SortOrderOptions.DESC) => productionOrders.OrderByDescending(po => po.ObjectOfLaborId).ToList(),
                _ => productionOrders.OrderBy(po => po.StartDate).ThenBy(po => po.EndDate).ToList(),
            };

            int totalCount = productionOrders.Count();
            productionOrders = productionOrders.Skip(page > 0 ? (page - 1) * count : 0)
                .Take(count > 0 ? count : totalCount)
                .ToList();

            if (!productionOrders.Any())
            {
                return NoContent();
            }

            var productionOrdersDto = _mapper.Map<List<ProductionOrderDTO>>(productionOrders);
            productionOrdersDto[0].TotalCount = totalCount;

            return Ok(productionOrdersDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductionOrderDTO>> GetProductionOrder(Guid id)
        {
            var productionOrder = await _productionOrderRepository.GetProductionOrderById(id);

            if (productionOrder == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ProductionOrderDTO>(productionOrder));
        }

        [HttpPost]
        public async Task<ActionResult<ProductionOrderDTO>> CreateProductionOrder(ProductionOrderCreateDTO productionOrder)
        {
            var createdProductionOrder = await _productionOrderRepository.AddProductionOrder(_mapper.Map<ProductionOrder>(productionOrder));

            return CreatedAtAction("GetProductionOrder", new { id = createdProductionOrder.ProductionOrderId }, _mapper.Map<ProductionOrderDTO>(createdProductionOrder));
        }

        [HttpPut]
        public async Task<ActionResult<ProductionOrderDTO>> UpdateProductionOrder(ProductionOrderUpdateDTO productionOrder)
        {
            var matchingProductionOrder = await _productionOrderRepository.GetProductionOrderById(productionOrder.ProductionOrderId);
            if (matchingProductionOrder == null)
            {
                return NotFound();
            }

            var updatedProductionOrder = await _productionOrderRepository.UpdateProductionOrder(_mapper.Map<ProductionOrder>(productionOrder));

            return Ok(_mapper.Map<ProductionOrderDTO>(updatedProductionOrder));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductionOrder(Guid id)
        {
            bool isDeleted = await _productionOrderRepository.DeleteProductionOrder(id);
            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
