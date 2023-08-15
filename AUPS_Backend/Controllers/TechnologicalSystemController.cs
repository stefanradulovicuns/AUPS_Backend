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
    public class TechnologicalSystemController : ControllerBase
    {
        private readonly ITechnologicalSystemRepository _technologicalSystemRepository;
        private readonly IMapper _mapper;

        public TechnologicalSystemController(ITechnologicalSystemRepository technologicalSystemRepository, IMapper mapper)
        {
            _technologicalSystemRepository = technologicalSystemRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<TechnologicalSystemDTO>> GetTechnologicalSystems(string? search, string? sortBy, SortOrderOptions? sortOrder, int page, int count)
        {
            var technologicalSystems = await _technologicalSystemRepository.GetAllTechnologicalSystems();

            if (!string.IsNullOrEmpty(search))
            {
                technologicalSystems = technologicalSystems
                    .Where(e => e.TechnologicalSystemName.Contains(search, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            technologicalSystems = (sortBy, sortOrder) switch
            {
                (nameof(TechnologicalSystemDTO.TechnologicalSystemId), SortOrderOptions.ASC) => technologicalSystems.OrderBy(ts => ts.TechnologicalSystemId).ToList(),
                (nameof(TechnologicalSystemDTO.TechnologicalSystemId), SortOrderOptions.DESC) => technologicalSystems.OrderByDescending(ts => ts.TechnologicalSystemId).ToList(),
                (nameof(TechnologicalSystemDTO.TechnologicalSystemName), SortOrderOptions.ASC) => technologicalSystems.OrderBy(ts => ts.TechnologicalSystemName).ToList(),
                (nameof(TechnologicalSystemDTO.TechnologicalSystemName), SortOrderOptions.DESC) => technologicalSystems.OrderByDescending(ts => ts.TechnologicalSystemName).ToList(),
                _ => technologicalSystems.OrderBy(e => e.TechnologicalSystemName).ToList(),
            };

            int totalCount = technologicalSystems.Count();
            technologicalSystems = technologicalSystems.Skip(page > 0 ? (page - 1) * count : 0)
                .Take(count)
                .ToList();

            if (!technologicalSystems.Any())
            {
                return NoContent();
            }

            var technologicalSystemsDto = _mapper.Map<List<TechnologicalSystemDTO>>(technologicalSystems);
            technologicalSystemsDto[0].TotalCount = totalCount;

            return Ok(technologicalSystemsDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TechnologicalSystemDTO>> GetTechnologicalSystem(Guid id)
        {
            var technologicalSystem = await _technologicalSystemRepository.GetTechnologicalSystemById(id);

            if (technologicalSystem == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<TechnologicalSystemDTO>(technologicalSystem));
        }

        [HttpPost]
        public async Task<ActionResult<TechnologicalSystemDTO>> CreateTechnologicalSystem(TechnologicalSystemCreateDTO technologicalSystem)
        {
            var createdTechnologicalSystem = await _technologicalSystemRepository.AddTechnologicalSystem(_mapper.Map<TechnologicalSystem>(technologicalSystem));

            return CreatedAtAction("GetTechnologicalSystem", new { id = createdTechnologicalSystem.TechnologicalSystemId }, _mapper.Map<TechnologicalSystemDTO>(createdTechnologicalSystem));
        }

        [HttpPut]
        public async Task<ActionResult<TechnologicalSystemDTO>> UpdateTechnologicalSystem(TechnologicalSystemUpdateDTO technologicalSystem)
        {
            var matchingTechnologicalSystem = await _technologicalSystemRepository.GetTechnologicalSystemById(technologicalSystem.TechnologicalSystemId);
            if (matchingTechnologicalSystem == null)
            {
                return NotFound();
            }

            var updatedTechnologicalSystem = await _technologicalSystemRepository.UpdateTechnologicalSystem(_mapper.Map<TechnologicalSystem>(technologicalSystem));

            return Ok(_mapper.Map<TechnologicalSystemDTO>(updatedTechnologicalSystem));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTechnologicalSystem(Guid id)
        {
            bool isDeleted = await _technologicalSystemRepository.DeleteTechnologicalSystem(id);
            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
