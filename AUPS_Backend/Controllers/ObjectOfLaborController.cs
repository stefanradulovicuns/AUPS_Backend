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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ObjectOfLaborController : ControllerBase
    {
        private readonly IObjectOfLaborRepository _objectOfLaborRepository;
        private readonly IMapper _mapper;

        public ObjectOfLaborController(IObjectOfLaborRepository objectOfLaborRepository, IMapper mapper)
        {
            _objectOfLaborRepository = objectOfLaborRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ObjectOfLaborDTO>> GetObjectOfLabors(string? search, string? sortBy, SortOrderOptions? sortOrder, int page, int count)
        {
            var objectOfLabors = await _objectOfLaborRepository.GetAllObjectOfLabors();

            if (!string.IsNullOrEmpty(search))
            {
                objectOfLabors = objectOfLabors.Where(ool => ool.ObjectOfLaborName.Contains(search, StringComparison.OrdinalIgnoreCase)
                                                || ool.Price.ToString().Contains(search, StringComparison.OrdinalIgnoreCase)
                                                || ool.StockQuantity.ToString().Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            objectOfLabors = (sortBy, sortOrder) switch
            {
                (nameof(ObjectOfLaborDTO.ObjectOfLaborId), SortOrderOptions.ASC) => objectOfLabors.OrderBy(ool => ool.ObjectOfLaborId).ToList(),
                (nameof(ObjectOfLaborDTO.ObjectOfLaborId), SortOrderOptions.DESC) => objectOfLabors.OrderByDescending(ool => ool.ObjectOfLaborId).ToList(),
                (nameof(ObjectOfLaborDTO.ObjectOfLaborName), SortOrderOptions.ASC) => objectOfLabors.OrderBy(ool => ool.ObjectOfLaborName).ToList(),
                (nameof(ObjectOfLaborDTO.ObjectOfLaborName), SortOrderOptions.DESC) => objectOfLabors.OrderByDescending(ool => ool.ObjectOfLaborName).ToList(),
                (nameof(ObjectOfLaborDTO.Description), SortOrderOptions.ASC) => objectOfLabors.OrderBy(ool => ool.Description).ToList(),
                (nameof(ObjectOfLaborDTO.Description), SortOrderOptions.DESC) => objectOfLabors.OrderByDescending(ool => ool.Description).ToList(),
                (nameof(ObjectOfLaborDTO.Price), SortOrderOptions.ASC) => objectOfLabors.OrderBy(ool => ool.Price).ToList(),
                (nameof(ObjectOfLaborDTO.Price), SortOrderOptions.DESC) => objectOfLabors.OrderByDescending(ool => ool.Price).ToList(),
                (nameof(ObjectOfLaborDTO.StockQuantity), SortOrderOptions.ASC) => objectOfLabors.OrderBy(ool => ool.StockQuantity).ToList(),
                (nameof(ObjectOfLaborDTO.StockQuantity), SortOrderOptions.DESC) => objectOfLabors.OrderByDescending(ool => ool.StockQuantity).ToList(),
                (nameof(ObjectOfLaborDTO.WarehouseId), SortOrderOptions.ASC) => objectOfLabors.OrderBy(ool => ool.WarehouseId).ToList(),
                (nameof(ObjectOfLaborDTO.WarehouseId), SortOrderOptions.DESC) => objectOfLabors.OrderByDescending(ool => ool.WarehouseId).ToList(),
                (nameof(ObjectOfLaborDTO.WarehouseFullAddress), SortOrderOptions.ASC) => objectOfLabors.OrderBy(ool => ool.Warehouse.City).ThenBy(ool => ool.Warehouse.Address).ToList(),
                (nameof(ObjectOfLaborDTO.WarehouseFullAddress), SortOrderOptions.DESC) => objectOfLabors.OrderByDescending(ool => ool.Warehouse.City).ThenByDescending(ool => ool.Warehouse.Address).ToList(),
                _ => objectOfLabors.OrderBy(ool => ool.ObjectOfLaborName).ToList(),
            };

            int totalCount = objectOfLabors.Count();
            objectOfLabors = objectOfLabors.Skip(page > 0 ? (page - 1) * count : 0)
                .Take(count > 0 ? count : totalCount)
                .ToList();

            if (!objectOfLabors.Any())
            {
                return NoContent();
            }

            var objectOfLaborsDto = _mapper.Map<List<ObjectOfLaborDTO>>(objectOfLabors);
            objectOfLaborsDto[0].TotalCount = totalCount;

            return Ok(objectOfLaborsDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ObjectOfLaborDTO>> GetObjectOfLabor(Guid id)
        {
            var objectOfLabor = await _objectOfLaborRepository.GetObjectOfLaborById(id);

            if (objectOfLabor == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ObjectOfLaborDTO>(objectOfLabor));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ObjectOfLaborDTO>> CreateObjectOfLabor(ObjectOfLaborCreateDTO objectOfLabor)
        {
            var createdObjectOfLabor = await _objectOfLaborRepository.AddObjectOfLabor(_mapper.Map<ObjectOfLabor>(objectOfLabor));

            return CreatedAtAction("GetObjectOfLabor", new { id = createdObjectOfLabor.ObjectOfLaborId }, _mapper.Map<ObjectOfLaborDTO>(createdObjectOfLabor));
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ObjectOfLaborDTO>> UpdateObjectOfLabor(ObjectOfLaborUpdateDTO objectOfLabor)
        {
            var matchingObjectOfLabor = await _objectOfLaborRepository.GetObjectOfLaborById(objectOfLabor.ObjectOfLaborId);
            if (matchingObjectOfLabor == null)
            {
                return NotFound();
            }

            var updatedObjectOfLabor = await _objectOfLaborRepository.UpdateObjectOfLabor(_mapper.Map<ObjectOfLabor>(objectOfLabor));

            return Ok(_mapper.Map<ObjectOfLaborDTO>(updatedObjectOfLabor));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteObjectOfLabor(Guid id)
        {
            bool isDeleted = await _objectOfLaborRepository.DeleteObjectOfLabor(id);
            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
