using AUPS_Backend.DTO;
using AUPS_Backend.Entities;
using AUPS_Backend.Enums;
using AUPS_Backend.Identity;
using AUPS_Backend.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AUPS_Backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionOrderController : ControllerBase
    {
        private readonly IProductionOrderRepository _productionOrderRepository;
        private readonly IObjectOfLaborTechnologicalProcedureRepository _objectOfLaborTechnologicalProcedureRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProductionOrderController(IProductionOrderRepository productionOrderRepository, IObjectOfLaborTechnologicalProcedureRepository objectOfLaborTechnologicalProcedureRepository, IEmployeeRepository employeeRepository, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _productionOrderRepository = productionOrderRepository;
            _objectOfLaborTechnologicalProcedureRepository = objectOfLaborTechnologicalProcedureRepository;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<ProductionOrderDTO>> GetProductionOrders(string? search, string? sortBy, SortOrderOptions? sortOrder, int page, int count)
        {
            var productionOrders = await _productionOrderRepository.GetAllProductionOrders();
            
            if (!string.IsNullOrEmpty(search))
            {
                productionOrders = productionOrders.Where(po => po.StartDate.ToShortDateString().Contains(search, StringComparison.OrdinalIgnoreCase)
                                                || po.EndDate.ToShortDateString().Contains(search, StringComparison.OrdinalIgnoreCase)
                                                || po.Quantity.ToString().Contains(search, StringComparison.OrdinalIgnoreCase)
                                                || po.ObjectOfLabor.ObjectOfLaborName.Contains(search, StringComparison.OrdinalIgnoreCase)
                                                || (po.Employee.FirstName + " " + po.Employee.LastName).Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
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
                (nameof(ProductionOrderDTO.Manager), SortOrderOptions.ASC) => productionOrders.OrderBy(po => po.Employee.FirstName).ThenBy(po => po.Employee.LastName).ToList(),
                (nameof(ProductionOrderDTO.Manager), SortOrderOptions.DESC) => productionOrders.OrderByDescending(po => po.Employee.FirstName).ThenByDescending(po => po.Employee.LastName).ToList(),
                (nameof(ProductionOrderDTO.ObjectOfLaborId), SortOrderOptions.ASC) => productionOrders.OrderBy(po => po.ObjectOfLaborId).ToList(),
                (nameof(ProductionOrderDTO.ObjectOfLaborId), SortOrderOptions.DESC) => productionOrders.OrderByDescending(po => po.ObjectOfLaborId).ToList(),
                (nameof(ProductionOrderDTO.ObjectOfLaborName), SortOrderOptions.ASC) => productionOrders.OrderBy(po => po.ObjectOfLabor.ObjectOfLaborName).ToList(),
                (nameof(ProductionOrderDTO.ObjectOfLaborName), SortOrderOptions.DESC) => productionOrders.OrderByDescending(po => po.ObjectOfLabor.ObjectOfLaborName).ToList(),
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
            foreach(var productionOrderDto in productionOrdersDto)
            {
                var objectOfLaborTechnologicalProcedures = await _objectOfLaborTechnologicalProcedureRepository.GetObjectOfLaborTechnologicalProceduresByObjectOfLaborId(productionOrderDto.ObjectOfLaborId);
                if (objectOfLaborTechnologicalProcedures.Any())
                {
                    if (productionOrderDto.CurrentTechnologicalProcedureExecuted)
                    {
                        productionOrderDto.CurrentState = (((productionOrderDto.CurrentTechnologicalProcedure - 1) * 1.0) / objectOfLaborTechnologicalProcedures.Count) * 100;
                    }
                    else
                    {
                        productionOrderDto.CurrentState = (productionOrderDto.CurrentTechnologicalProcedure / (double)objectOfLaborTechnologicalProcedures.Count) * 100;
                    }
                }
                else
                {
                    productionOrderDto.CurrentState = 0;
                }
            }
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
        [Authorize(Roles = "Admin,Menadzer")]
        public async Task<ActionResult<ProductionOrderDTO>> CreateProductionOrder(ProductionOrderCreateDTO productionOrder)
        {
            var newProductionOrder = _mapper.Map<ProductionOrder>(productionOrder);
            newProductionOrder.CurrentTechnologicalProcedure = 0;
            newProductionOrder.CurrentTechnologicalProcedureExecuted = false;
            var currentUser = await _userManager.GetUserAsync(User);
            var manager = await _employeeRepository.GetEmployeeByEmail(currentUser.Email);
            if (manager != null)
            {
                newProductionOrder.EmployeeId = manager.EmployeeId;
            }
            var createdProductionOrder = await _productionOrderRepository.AddProductionOrder(newProductionOrder);

            return CreatedAtAction("GetProductionOrder", new { id = createdProductionOrder.ProductionOrderId }, _mapper.Map<ProductionOrderDTO>(createdProductionOrder));
        }

        [HttpPut]
        [Authorize(Roles = "Admin,Menadzer")]
        public async Task<ActionResult<ProductionOrderDTO>> UpdateProductionOrder(ProductionOrderUpdateDTO productionOrder)
        { 
            var matchingProductionOrder = await _productionOrderRepository.GetProductionOrderById(productionOrder.ProductionOrderId);
            if (matchingProductionOrder == null)
            {
                return NotFound();
            }

            var currentUser = await _userManager.GetUserAsync(User);
            var manager = await _employeeRepository.GetEmployeeById(matchingProductionOrder.EmployeeId);
            if (manager == null || currentUser.Email != manager.Email)
            {
                return Unauthorized();
            }

            var editedProductionOrder = _mapper.Map<ProductionOrder>(productionOrder);
            editedProductionOrder.EmployeeId = manager.EmployeeId;
            var updatedProductionOrder = await _productionOrderRepository.UpdateProductionOrder(editedProductionOrder);

            return Ok(_mapper.Map<ProductionOrderDTO>(updatedProductionOrder));
        }

        [HttpPut("startNextTechnologicalProcedure")]
        [Authorize(Roles = "Radnik u proizvodnji")]
        public async Task<ActionResult<ProductionOrderDTO>> StartNextTechnologicalProcedure(ProductionOrderDTO productionOrder)
        {
            var matchingProductionOrder = await _productionOrderRepository.GetProductionOrderById(productionOrder.ProductionOrderId);
            if (matchingProductionOrder == null)
            {
                return NotFound();
            }

            var objectOfLaborTechnologicalProcedures = await _objectOfLaborTechnologicalProcedureRepository.GetObjectOfLaborTechnologicalProceduresByObjectOfLaborId(matchingProductionOrder.ObjectOfLaborId);
            if (objectOfLaborTechnologicalProcedures.Any() && matchingProductionOrder.CurrentTechnologicalProcedure < objectOfLaborTechnologicalProcedures.Count)
            {
                matchingProductionOrder.CurrentTechnologicalProcedure++;
                matchingProductionOrder.CurrentTechnologicalProcedureExecuted = true;
            }

            var updatedProductionOrder = await _productionOrderRepository.UpdateProductionOrder(matchingProductionOrder);

            return Ok(_mapper.Map<ProductionOrderDTO>(updatedProductionOrder));
        }

        [HttpPut("finishCurrentTechnologicalProcedure")]
        [Authorize(Roles = "Radnik u proizvodnji")]
        public async Task<ActionResult<ProductionOrderDTO>> FinishCurrentTechnologicalProcedure(ProductionOrderDTO productionOrder)
        {
            var matchingProductionOrder = await _productionOrderRepository.GetProductionOrderById(productionOrder.ProductionOrderId);
            if (matchingProductionOrder == null)
            {
                return NotFound();
            }

            matchingProductionOrder.CurrentTechnologicalProcedureExecuted = false;

            var updatedProductionOrder = await _productionOrderRepository.UpdateProductionOrder(matchingProductionOrder);

            return Ok(_mapper.Map<ProductionOrderDTO>(updatedProductionOrder));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Menadzer")]
        public async Task<IActionResult> DeleteProductionOrder(Guid id)
        {
            var productionOrder = await _productionOrderRepository.GetProductionOrderById(id);
            if (productionOrder == null)
            {
                return NotFound();
            }

            var manager = await _employeeRepository.GetEmployeeById(productionOrder.EmployeeId);
            var currentUser = await _userManager.GetUserAsync(User);
            if (manager == null || currentUser.Email != manager.Email)
            {
                return Unauthorized();
            }

            await _productionOrderRepository.DeleteProductionOrder(id);

            return NoContent();
        }
    }
}
