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
    public class WorkplaceController : ControllerBase
    {
        private readonly IWorkplaceRepository _workplaceRepository;
        private readonly IMapper _mapper;

        public WorkplaceController(IWorkplaceRepository workplaceRepository, IMapper mapper)
        {
            _workplaceRepository = workplaceRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<WorkplaceDTO>> GetWorkplaces(string? search, string? sortBy, SortOrderOptions? sortOrder, int page, int count)
        {
            var workplaces = await _workplaceRepository.GetAllWorkplaces();

            if (!string.IsNullOrEmpty(search))
            {
                workplaces = workplaces.Where(w => w.WorkplaceName.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            workplaces = (sortBy, sortOrder) switch
            {
                (nameof(WorkplaceDTO.WorkplaceId), SortOrderOptions.ASC) => workplaces.OrderBy(w => w.WorkplaceId).ToList(),
                (nameof(WorkplaceDTO.WorkplaceId), SortOrderOptions.DESC) => workplaces.OrderByDescending(w => w.WorkplaceId).ToList(),
                (nameof(WorkplaceDTO.WorkplaceName), SortOrderOptions.ASC) => workplaces.OrderBy(w => w.WorkplaceName).ToList(),
                (nameof(WorkplaceDTO.WorkplaceName), SortOrderOptions.DESC) => workplaces.OrderByDescending(w => w.WorkplaceName).ToList(),
                _ => workplaces.OrderBy(w => w.WorkplaceName).ToList()
            };

            int totalCount = workplaces.Count();
            workplaces = workplaces.Skip(page > 0 ? (page - 1) * count : 0)
                  .Take(count)
                  .ToList();

            if (!workplaces.Any())
            {
                return NoContent();
            }

            var workplacesDto = _mapper.Map<List<WorkplaceDTO>>(workplaces);
            workplacesDto[0].TotalCount = totalCount;

            return Ok(workplacesDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WorkplaceDTO>> GetWorkplace(Guid id)
        {
            var workplace = await _workplaceRepository.GetWorkplaceById(id);

            if (workplace == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<WorkplaceDTO>(workplace));
        }

        [HttpPost]
        public async Task<ActionResult<WorkplaceDTO>> CreateWorkplace(WorkplaceCreateDTO workplace)
        {
            var createdWorkplace = await _workplaceRepository.AddWorkplace(_mapper.Map<Workplace>(workplace));

            return CreatedAtAction("GetWorkplace", new {id = createdWorkplace.WorkplaceId}, _mapper.Map<WorkplaceDTO>(createdWorkplace));
        }

        [HttpPut]
        public async Task<ActionResult<WorkplaceDTO>> UpdateWorkplace(WorkplaceUpdateDTO workpalce)
        {
            var matchingWorkplace = await _workplaceRepository.GetWorkplaceById(workpalce.WorkplaceId);

            if (matchingWorkplace == null)
            {
                return NotFound();
            }

            var updatedWorkplace = await _workplaceRepository.UpdateWorkplace(_mapper.Map<Workplace>(workpalce));

            return Ok(_mapper.Map<WorkplaceDTO>(updatedWorkplace));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkplace(Guid id)
        {
            bool isDeleted = await _workplaceRepository.DeleteWorkplace(id);
            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
