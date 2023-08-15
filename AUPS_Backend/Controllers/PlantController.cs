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
    public class PlantController : ControllerBase
    {
        private readonly IPlantRepository _plantRepository;
        private readonly IMapper _mapper;

        public PlantController(IPlantRepository plantRepository, IMapper mapper)
        {
            _plantRepository = plantRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<PlantDTO>> GetPlants(string? search, string? sortBy, SortOrderOptions? sortOrder, int page, int count)
        {
            var plants = await _plantRepository.GetAllPlants();

            if (!string.IsNullOrEmpty(search))
            {
                plants = plants.Where(p => p.PlantName.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            plants = (sortBy, sortOrder) switch
            {
                (nameof(PlantDTO.PlantId), SortOrderOptions.ASC) => plants.OrderBy(p => p.PlantId).ToList(),
                (nameof(PlantDTO.PlantId), SortOrderOptions.DESC) => plants.OrderByDescending(p => p.PlantId).ToList(),
                (nameof(PlantDTO.PlantName), SortOrderOptions.ASC) => plants.OrderBy(p => p.PlantName).ToList(),
                (nameof(PlantDTO.PlantName), SortOrderOptions.DESC) => plants.OrderByDescending(p => p.PlantName).ToList(),
                _ => plants.OrderBy(p => p.PlantName).ToList(),
            };

            int totalCount = plants.Count();
            plants = plants.Skip(page > 0 ? (page - 1) * count : 0)
                .Take(count)
                .ToList();

            if (!plants.Any())
            {
                return NoContent();
            }

            var plantsDto = _mapper.Map<List<PlantDTO>>(plants);
            plantsDto[0].TotalCount = totalCount;

            return Ok(plantsDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PlantDTO>> GetPlant(Guid id)
        {
            var plant = await _plantRepository.GetPlantById(id);

            if (plant == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PlantDTO>(plant));
        }

        [HttpPost]
        public async Task<ActionResult<PlantDTO>> CreatePlant(PlantCreateDTO plant)
        {
            var createdPlant = await _plantRepository.AddPlant(_mapper.Map<Plant>(plant));

            return CreatedAtAction("GetPlant", new { id = createdPlant.PlantId }, _mapper.Map<PlantDTO>(createdPlant));
        }

        [HttpPut]
        public async Task<ActionResult<PlantDTO>> UpdatePlant(PlantUpdateDTO plant)
        {
            var matchingPlant = await _plantRepository.GetPlantById(plant.PlantId);
            if (matchingPlant == null)
            {
                return NotFound();
            }

            var updatedPlant = await _plantRepository.UpdatePlant(_mapper.Map<Plant>(plant));

            return Ok(_mapper.Map<PlantDTO>(updatedPlant));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlant(Guid id)
        {
            bool isDeleted = await _plantRepository.DeletePlant(id);
            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
