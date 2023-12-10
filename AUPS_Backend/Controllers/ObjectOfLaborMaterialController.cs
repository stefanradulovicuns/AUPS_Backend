using AUPS_Backend.DTO;
using AUPS_Backend.Entities;
using AUPS_Backend.Enums;
using AUPS_Backend.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AUPS_Backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ObjectOfLaborMaterialController : ControllerBase
    {
        private readonly IObjectOfLaborMaterialRepository _objectOfLaborMaterialRepository;
        private readonly IMapper _mapper;

        public ObjectOfLaborMaterialController(IObjectOfLaborMaterialRepository objectOfLaborMaterialRepository, IMapper mapper)
        {
            _objectOfLaborMaterialRepository = objectOfLaborMaterialRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ObjectOfLaborMaterialDTO>> GetObjectOfLaborMaterials(string? search, string? sortBy, SortOrderOptions? sortOrder, int page, int count, Guid? objectOfLaborId)
        {
            var objectOfLaborMaterials = await _objectOfLaborMaterialRepository.GetAllObjectOfLaborMaterials();
            if (objectOfLaborId != null)
            {
                objectOfLaborMaterials = objectOfLaborMaterials.Where(temp => temp.ObjectOfLaborId == objectOfLaborId).ToList();
            }

            if (!string.IsNullOrEmpty(search))
            {
                objectOfLaborMaterials = objectOfLaborMaterials
                    .Where(temp => temp.Quantity.ToString().Contains(search, StringComparison.OrdinalIgnoreCase)
                                || temp.Material.MaterialName.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            objectOfLaborMaterials = (sortBy, sortOrder) switch
            {
                (nameof(ObjectOfLaborMaterialDTO.ObjectOfLaborMaterialId), SortOrderOptions.ASC) => objectOfLaborMaterials.OrderBy(oolm => oolm.ObjectOfLaborMaterialId).ToList(),
                (nameof(ObjectOfLaborMaterialDTO.ObjectOfLaborMaterialId), SortOrderOptions.DESC) => objectOfLaborMaterials.OrderByDescending(oolm => oolm.ObjectOfLaborMaterialId).ToList(),
                (nameof(ObjectOfLaborMaterialDTO.Quantity), SortOrderOptions.ASC) => objectOfLaborMaterials.OrderBy(oolm => oolm.Quantity).ToList(),
                (nameof(ObjectOfLaborMaterialDTO.Quantity), SortOrderOptions.DESC) => objectOfLaborMaterials.OrderByDescending(oolm => oolm.Quantity).ToList(),
                (nameof(ObjectOfLaborMaterialDTO.ObjectOfLaborId), SortOrderOptions.ASC) => objectOfLaborMaterials.OrderBy(oolm => oolm.ObjectOfLaborId).ToList(),
                (nameof(ObjectOfLaborMaterialDTO.ObjectOfLaborId), SortOrderOptions.DESC) => objectOfLaborMaterials.OrderByDescending(oolm => oolm.ObjectOfLaborId).ToList(),
                (nameof(ObjectOfLaborMaterialDTO.MaterialId), SortOrderOptions.ASC) => objectOfLaborMaterials.OrderBy(oolm => oolm.MaterialId).ToList(),
                (nameof(ObjectOfLaborMaterialDTO.MaterialId), SortOrderOptions.DESC) => objectOfLaborMaterials.OrderByDescending(oolm => oolm.MaterialId).ToList(),
                (nameof(ObjectOfLaborMaterialDTO.MaterialName), SortOrderOptions.ASC) => objectOfLaborMaterials.OrderBy(oolm => oolm.Material.MaterialName).ToList(),
                (nameof(ObjectOfLaborMaterialDTO.MaterialName), SortOrderOptions.DESC) => objectOfLaborMaterials.OrderByDescending(oolm => oolm.Material.MaterialName).ToList(),
                _ => objectOfLaborMaterials,
            };

            int totalCount = objectOfLaborMaterials.Count();
            objectOfLaborMaterials = objectOfLaborMaterials.Skip(page > 0 ? (page - 1) * count : 0)
                .Take(count > 0 ? count : totalCount)
                .ToList();

            if (!objectOfLaborMaterials.Any())
            {
                return NoContent();
            }

            var objectOfLaborMaterialsDto = _mapper.Map<List<ObjectOfLaborMaterialDTO>>(objectOfLaborMaterials);
            objectOfLaborMaterialsDto[0].TotalCount = totalCount;

            return Ok(objectOfLaborMaterialsDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ObjectOfLaborMaterialDTO>> GetObjectOfLaborMaterial(Guid id)
        {
            var objectOfLaborMaterial = await _objectOfLaborMaterialRepository.GetObjectOfLaborMaterialById(id);

            if (objectOfLaborMaterial == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ObjectOfLaborMaterialDTO>(objectOfLaborMaterial));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ObjectOfLaborMaterialDTO>> CreateObjectOfLaborMaterial(ObjectOfLaborMaterialCreateDTO objectOfLaborMaterial)
        {
            var createdObjectOfLaborMaterial = await _objectOfLaborMaterialRepository.AddObjectOfLaborMaterial(_mapper.Map<ObjectOfLaborMaterial>(objectOfLaborMaterial));

            return CreatedAtAction("GetObjectOfLaborMaterial", new { id = createdObjectOfLaborMaterial.ObjectOfLaborMaterialId }, _mapper.Map<ObjectOfLaborMaterialDTO>(createdObjectOfLaborMaterial));
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ObjectOfLaborMaterialDTO>> UpdateObjectOfLaborMaterial(ObjectOfLaborMaterialUpdateDTO objectOfLaborMaterial)
        {
            var matchingObjectOfLaborMaterial = await _objectOfLaborMaterialRepository.GetObjectOfLaborMaterialById(objectOfLaborMaterial.ObjectOfLaborMaterialId);
            if (matchingObjectOfLaborMaterial == null)
            {
                return NotFound();
            }

            var updatedObjectOfLaborMaterial = await _objectOfLaborMaterialRepository.UpdateObjectOfLaborMaterial(_mapper.Map<ObjectOfLaborMaterial>(objectOfLaborMaterial));

            return Ok(_mapper.Map<ObjectOfLaborMaterialDTO>(updatedObjectOfLaborMaterial));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteObjectOfLaborMaterial(Guid id)
        {
            bool isDeleted = await _objectOfLaborMaterialRepository.DeleteObjectOfLaborMaterial(id);
            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
