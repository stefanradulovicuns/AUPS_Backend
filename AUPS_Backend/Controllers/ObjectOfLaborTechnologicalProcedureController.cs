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
    public class ObjectOfLaborTechnologicalProcedureController : ControllerBase
    {
        private readonly IObjectOfLaborTechnologicalProcedureRepository _objectOfLaborTechnologicalProcedureRepository;
        private readonly IMapper _mapper;

        public ObjectOfLaborTechnologicalProcedureController(IObjectOfLaborTechnologicalProcedureRepository objectOfLaborTechnologicalProcedureRepository, IMapper mapper)
        {
            _objectOfLaborTechnologicalProcedureRepository = objectOfLaborTechnologicalProcedureRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ObjectOfLaborTechnologicalProcedureDTO>> GetObjectOfLaborTechnologicalProcedures(string? search, string? sortBy, SortOrderOptions? sortOrder, int page, int count)
        {
            var objectOfLaborTechnologicalProcedures = await _objectOfLaborTechnologicalProcedureRepository.GetAllObjectOfLaborTechnologicalProcedures();
            /*
            if (!string.IsNullOrEmpty(search))
            {
                ObjectOfLaborTechnologicalProcedures = ObjectOfLaborTechnologicalProcedures.Where(e => e.FirstName.Contains(search, StringComparison.OrdinalIgnoreCase)
                                                || e.LastName.Contains(search, StringComparison.OrdinalIgnoreCase)
                                                || e.Email.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            */
            objectOfLaborTechnologicalProcedures = (sortBy, sortOrder) switch
            {
                (nameof(ObjectOfLaborTechnologicalProcedureDTO.ObjectOfLaborTechnologicalProcedureId), SortOrderOptions.ASC) => objectOfLaborTechnologicalProcedures.OrderBy(ooltp => ooltp.ObjectOfLaborTechnologicalProcedureId).ToList(),
                (nameof(ObjectOfLaborTechnologicalProcedureDTO.ObjectOfLaborTechnologicalProcedureId), SortOrderOptions.DESC) => objectOfLaborTechnologicalProcedures.OrderByDescending(ooltp => ooltp.ObjectOfLaborTechnologicalProcedureId).ToList(),
                (nameof(ObjectOfLaborTechnologicalProcedureDTO.ObjectOfLaborId), SortOrderOptions.ASC) => objectOfLaborTechnologicalProcedures.OrderBy(ooltp => ooltp.ObjectOfLaborId).ToList(),
                (nameof(ObjectOfLaborTechnologicalProcedureDTO.ObjectOfLaborId), SortOrderOptions.DESC) => objectOfLaborTechnologicalProcedures.OrderByDescending(ooltp => ooltp.ObjectOfLaborId).ToList(),
                (nameof(ObjectOfLaborTechnologicalProcedureDTO.TechnologicalProcedureId), SortOrderOptions.ASC) => objectOfLaborTechnologicalProcedures.OrderBy(ooltp => ooltp.TechnologicalProcedureId).ToList(),
                (nameof(ObjectOfLaborTechnologicalProcedureDTO.TechnologicalProcedureId), SortOrderOptions.DESC) => objectOfLaborTechnologicalProcedures.OrderByDescending(ooltp => ooltp.TechnologicalProcedureId).ToList(),
                _ => objectOfLaborTechnologicalProcedures,
            };

            int totalCount = objectOfLaborTechnologicalProcedures.Count();
            objectOfLaborTechnologicalProcedures = objectOfLaborTechnologicalProcedures.Skip(page > 0 ? (page - 1) * count : 0)
                .Take(count > 0 ? count : totalCount)
                .ToList();

            if (!objectOfLaborTechnologicalProcedures.Any())
            {
                return NoContent();
            }

            var objectOfLaborTechnologicalProceduresDto = _mapper.Map<List<ObjectOfLaborTechnologicalProcedureDTO>>(objectOfLaborTechnologicalProcedures);
            objectOfLaborTechnologicalProceduresDto[0].TotalCount = totalCount;

            return Ok(objectOfLaborTechnologicalProceduresDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ObjectOfLaborTechnologicalProcedureDTO>> GetObjectOfLaborTechnologicalProcedure(Guid id)
        {
            var objectOfLaborTechnologicalProcedure = await _objectOfLaborTechnologicalProcedureRepository.GetObjectOfLaborTechnologicalProcedureById(id);

            if (objectOfLaborTechnologicalProcedure == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ObjectOfLaborTechnologicalProcedureDTO>(objectOfLaborTechnologicalProcedure));
        }

        [HttpPost]
        public async Task<ActionResult<ObjectOfLaborTechnologicalProcedureDTO>> CreateObjectOfLaborTechnologicalProcedure(ObjectOfLaborTechnologicalProcedureCreateDTO objectOfLaborTechnologicalProcedure)
        {
            var createdObjectOfLaborTechnologicalProcedure = await _objectOfLaborTechnologicalProcedureRepository.AddObjectOfLaborTechnologicalProcedure(_mapper.Map<ObjectOfLaborTechnologicalProcedure>(objectOfLaborTechnologicalProcedure));

            return CreatedAtAction("GetObjectOfLaborTechnologicalProcedure", new { id = createdObjectOfLaborTechnologicalProcedure.ObjectOfLaborTechnologicalProcedureId }, _mapper.Map<ObjectOfLaborTechnologicalProcedureDTO>(createdObjectOfLaborTechnologicalProcedure));
        }

        [HttpPut]
        public async Task<ActionResult<ObjectOfLaborTechnologicalProcedureDTO>> UpdateObjectOfLaborTechnologicalProcedure(ObjectOfLaborTechnologicalProcedureUpdateDTO objectOfLaborTechnologicalProcedure)
        {
            var matchingObjectOfLaborTechnologicalProcedure = await _objectOfLaborTechnologicalProcedureRepository.GetObjectOfLaborTechnologicalProcedureById(objectOfLaborTechnologicalProcedure.ObjectOfLaborTechnologicalProcedureId);
            if (matchingObjectOfLaborTechnologicalProcedure == null)
            {
                return NotFound();
            }

            var updatedObjectOfLaborTechnologicalProcedure = await _objectOfLaborTechnologicalProcedureRepository.UpdateObjectOfLaborTechnologicalProcedure(_mapper.Map<ObjectOfLaborTechnologicalProcedure>(objectOfLaborTechnologicalProcedure));

            return Ok(_mapper.Map<ObjectOfLaborTechnologicalProcedureDTO>(updatedObjectOfLaborTechnologicalProcedure));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteObjectOfLaborTechnologicalProcedure(Guid id)
        {
            bool isDeleted = await _objectOfLaborTechnologicalProcedureRepository.DeleteObjectOfLaborTechnologicalProcedure(id);
            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
