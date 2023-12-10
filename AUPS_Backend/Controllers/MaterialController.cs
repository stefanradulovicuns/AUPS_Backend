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
    public class MaterialController : ControllerBase
    {
        private readonly IMaterialRepository _materialRepository;
        private readonly IMapper _mapper;

        public MaterialController(IMaterialRepository materialRepository, IMapper mapper)
        {
            _materialRepository = materialRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<MaterialDTO>> GetMaterials(string? search, string? sortBy, SortOrderOptions? sortOrder, int page, int count)
        {
            var materials = await _materialRepository.GetAllMaterials();

            if (!string.IsNullOrEmpty(search))
            {
                materials = materials.Where(e => e.MaterialName.Contains(search, StringComparison.OrdinalIgnoreCase)
                                                || e.StockQuantity.ToString().Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            materials = (sortBy, sortOrder) switch
            {
                (nameof(MaterialDTO.MaterialId), SortOrderOptions.ASC) => materials.OrderBy(m => m.MaterialId).ToList(),
                (nameof(MaterialDTO.MaterialId), SortOrderOptions.DESC) => materials.OrderByDescending(m => m.MaterialId).ToList(),
                (nameof(MaterialDTO.MaterialName), SortOrderOptions.ASC) => materials.OrderBy(m => m.MaterialName).ToList(),
                (nameof(MaterialDTO.MaterialName), SortOrderOptions.DESC) => materials.OrderByDescending(m => m.MaterialName).ToList(),
                (nameof(MaterialDTO.StockQuantity), SortOrderOptions.ASC) => materials.OrderBy(m => m.StockQuantity).ToList(),
                (nameof(MaterialDTO.StockQuantity), SortOrderOptions.DESC) => materials.OrderByDescending(m => m.StockQuantity).ToList(),
                _ => materials.OrderBy(m => m.MaterialName).ToList(),
            };

            int totalCount = materials.Count();
            materials = materials.Skip(page > 0 ? (page - 1) * count : 0)
                .Take(count > 0 ? count : totalCount)
                .ToList();

            if (!materials.Any())
            {
                return NoContent();
            }

            var materialsDto = _mapper.Map<List<MaterialDTO>>(materials);
            materialsDto[0].TotalCount = totalCount;

            return Ok(materialsDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MaterialDTO>> GetMaterial(Guid id)
        {
            var material = await _materialRepository.GetMaterialById(id);

            if (material == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<MaterialDTO>(material));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<MaterialDTO>> CreateMaterial(MaterialCreateDTO Material)
        {
            var createdMaterial = await _materialRepository.AddMaterial(_mapper.Map<Material>(Material));

            return CreatedAtAction("GetMaterial", new { id = createdMaterial.MaterialId }, _mapper.Map<MaterialDTO>(createdMaterial));
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<MaterialDTO>> UpdateMaterial(MaterialUpdateDTO material)
        {
            var matchingMaterial = await _materialRepository.GetMaterialById(material.MaterialId);
            if (matchingMaterial == null)
            {
                return NotFound();
            }

            var updatedMaterial = await _materialRepository.UpdateMaterial(_mapper.Map<Material>(material));

            return Ok(_mapper.Map<MaterialDTO>(updatedMaterial));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteMaterial(Guid id)
        {
            bool isDeleted = await _materialRepository.DeleteMaterial(id);
            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
