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
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IMapper _mapper;

        public WarehouseController(IWarehouseRepository warehouseRepository, IMapper mapper)
        {
            _warehouseRepository = warehouseRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<WarehouseDTO>> GetWarehouses(string? search, string? sortBy, SortOrderOptions? sortOrder, int page, int count)
        {
            var warehouses = await _warehouseRepository.GetAllWarehouses();

            if (!string.IsNullOrEmpty(search))
            {
                warehouses = warehouses.Where(e => e.Address.Contains(search, StringComparison.OrdinalIgnoreCase)
                                                || e.City.Contains(search, StringComparison.OrdinalIgnoreCase)
                                                || e.Capacity.ToString().Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            warehouses = (sortBy, sortOrder) switch
            {
                (nameof(WarehouseDTO.WarehouseId), SortOrderOptions.ASC) => warehouses.OrderBy(w => w.WarehouseId).ToList(),
                (nameof(WarehouseDTO.WarehouseId), SortOrderOptions.DESC) => warehouses.OrderByDescending(w => w.WarehouseId).ToList(),
                (nameof(WarehouseDTO.Address), SortOrderOptions.ASC) => warehouses.OrderBy(w => w.Address).ToList(),
                (nameof(WarehouseDTO.Address), SortOrderOptions.DESC) => warehouses.OrderByDescending(w => w.Address).ToList(),
                (nameof(WarehouseDTO.City), SortOrderOptions.ASC) => warehouses.OrderBy(w => w.City).ToList(),
                (nameof(WarehouseDTO.City), SortOrderOptions.DESC) => warehouses.OrderByDescending(w => w.City).ToList(),
                (nameof(WarehouseDTO.Capacity), SortOrderOptions.ASC) => warehouses.OrderBy(w => w.Capacity).ToList(),
                (nameof(WarehouseDTO.Capacity), SortOrderOptions.DESC) => warehouses.OrderByDescending(w => w.Capacity).ToList(),
                _ => warehouses.OrderBy(e => e.City).ThenBy(e => e.Address).ToList(),
            };

            int totalCount = warehouses.Count();
            warehouses = warehouses.Skip(page > 0 ? (page - 1) * count : 0)
                .Take(count)
                .ToList();

            if (!warehouses.Any())
            {
                return NoContent();
            }

            var warehousesDto = _mapper.Map<List<WarehouseDTO>>(warehouses);
            warehousesDto[0].TotalCount = totalCount;

            return Ok(warehousesDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WarehouseDTO>> GetWarehouse(Guid id)
        {
            var warehouse = await _warehouseRepository.GetWarehouseById(id);

            if (warehouse == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<WarehouseDTO>(warehouse));
        }

        [HttpPost]
        public async Task<ActionResult<WarehouseDTO>> CreateWarehouse(WarehouseCreateDTO warehouse)
        {
            var createdWarehouse = await _warehouseRepository.AddWarehouse(_mapper.Map<Warehouse>(warehouse));

            return CreatedAtAction("GetWarehouse", new { id = createdWarehouse.WarehouseId }, _mapper.Map<WarehouseDTO>(createdWarehouse));
        }

        [HttpPut]
        public async Task<ActionResult<WarehouseDTO>> Updatewarehouse(WarehouseUpdateDTO warehouse)
        {
            var matchingWarehouse = await _warehouseRepository.GetWarehouseById(warehouse.WarehouseId);
            if (matchingWarehouse == null)
            {
                return NotFound();
            }

            var updatedWarehouse = await _warehouseRepository.UpdateWarehouse(_mapper.Map<Warehouse>(warehouse));

            return Ok(_mapper.Map<WarehouseDTO>(updatedWarehouse));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletewarehouse(Guid id)
        {
            bool isDeleted = await _warehouseRepository.DeleteWarehouse(id);
            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
