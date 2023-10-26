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
    //[Authorize(Roles = nameof(UserTypeOptions.Admin) + "," + nameof(UserTypeOptions.User))]
    [Route("api/[controller]")]
    [ApiController]
    public class ObjectOfLaborTechnologicalProcedureController : ControllerBase
    {
        private readonly IObjectOfLaborTechnologicalProcedureRepository _objectOfLaborTechnologicalProcedureRepository;
        private readonly ITechnologicalSystemRepository _technologicalSystemRepository;
        private readonly IPlantRepository _plantRepository;
        private readonly IOrganizationalUnitRepository _organizationalUnitRepository;
        private readonly IMapper _mapper;

        public ObjectOfLaborTechnologicalProcedureController(IObjectOfLaborTechnologicalProcedureRepository objectOfLaborTechnologicalProcedureRepository, ITechnologicalSystemRepository technologicalSystemRepository, IPlantRepository plantRepository, IOrganizationalUnitRepository organizationalUnitRepository, IMapper mapper)
        {
            _objectOfLaborTechnologicalProcedureRepository = objectOfLaborTechnologicalProcedureRepository;
            _technologicalSystemRepository = technologicalSystemRepository;
            _plantRepository = plantRepository;
            _organizationalUnitRepository = organizationalUnitRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ObjectOfLaborTechnologicalProcedureDTO>> GetObjectOfLaborTechnologicalProcedures(string? search, string? sortBy, SortOrderOptions? sortOrder, int page, int count, Guid? objectOfLaborId)
        {
            var objectOfLaborTechnologicalProcedures = await _objectOfLaborTechnologicalProcedureRepository.GetAllObjectOfLaborTechnologicalProcedures();
            if (objectOfLaborId != null)
            {
                objectOfLaborTechnologicalProcedures = objectOfLaborTechnologicalProcedures.Where(temp => temp.ObjectOfLaborId == objectOfLaborId).ToList();
            }
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
                (nameof(ObjectOfLaborTechnologicalProcedureDTO.OrderOfExecution), SortOrderOptions.ASC) => objectOfLaborTechnologicalProcedures.OrderBy(ooltp => ooltp.OrderOfExecution).ToList(),
                (nameof(ObjectOfLaborTechnologicalProcedureDTO.OrderOfExecution), SortOrderOptions.DESC) => objectOfLaborTechnologicalProcedures.OrderByDescending(ooltp => ooltp.OrderOfExecution).ToList(),
                (nameof(ObjectOfLaborTechnologicalProcedureDTO.ObjectOfLaborId), SortOrderOptions.ASC) => objectOfLaborTechnologicalProcedures.OrderBy(ooltp => ooltp.ObjectOfLaborId).ToList(),
                (nameof(ObjectOfLaborTechnologicalProcedureDTO.ObjectOfLaborId), SortOrderOptions.DESC) => objectOfLaborTechnologicalProcedures.OrderByDescending(ooltp => ooltp.ObjectOfLaborId).ToList(),
                (nameof(ObjectOfLaborTechnologicalProcedureDTO.TechnologicalProcedureId), SortOrderOptions.ASC) => objectOfLaborTechnologicalProcedures.OrderBy(ooltp => ooltp.TechnologicalProcedureId).ToList(),
                (nameof(ObjectOfLaborTechnologicalProcedureDTO.TechnologicalProcedureId), SortOrderOptions.DESC) => objectOfLaborTechnologicalProcedures.OrderByDescending(ooltp => ooltp.TechnologicalProcedureId).ToList(),
                (nameof(ObjectOfLaborTechnologicalProcedureDTO.TechnologicalProcedureName), SortOrderOptions.ASC) => objectOfLaborTechnologicalProcedures.OrderBy(ooltp => ooltp.TechnologicalProcedure.TechnologicalProcedureName).ToList(),
                (nameof(ObjectOfLaborTechnologicalProcedureDTO.TechnologicalProcedureName), SortOrderOptions.DESC) => objectOfLaborTechnologicalProcedures.OrderByDescending(ooltp => ooltp.TechnologicalProcedure.TechnologicalProcedureName).ToList(),
                (nameof(ObjectOfLaborTechnologicalProcedureDTO.TechnologicalSystemName), SortOrderOptions.ASC) => objectOfLaborTechnologicalProcedures.OrderBy(ooltp => ooltp.TechnologicalProcedure.TechnologicalSystem.TechnologicalSystemName).ToList(),
                (nameof(ObjectOfLaborTechnologicalProcedureDTO.TechnologicalSystemName), SortOrderOptions.DESC) => objectOfLaborTechnologicalProcedures.OrderByDescending(ooltp => ooltp.TechnologicalProcedure.TechnologicalSystem.TechnologicalSystemName).ToList(),
                (nameof(ObjectOfLaborTechnologicalProcedureDTO.PlantName), SortOrderOptions.ASC) => objectOfLaborTechnologicalProcedures.OrderBy(ooltp => ooltp.TechnologicalProcedure.Plant.PlantName).ToList(),
                (nameof(ObjectOfLaborTechnologicalProcedureDTO.PlantName), SortOrderOptions.DESC) => objectOfLaborTechnologicalProcedures.OrderByDescending(ooltp => ooltp.TechnologicalProcedure.Plant.PlantName).ToList(),
                (nameof(ObjectOfLaborTechnologicalProcedureDTO.OrganizationalUnitName), SortOrderOptions.ASC) => objectOfLaborTechnologicalProcedures.OrderBy(ooltp => ooltp.TechnologicalProcedure.OrganizationalUnit.OrganizationalUnitName).ToList(),
                (nameof(ObjectOfLaborTechnologicalProcedureDTO.OrganizationalUnitName), SortOrderOptions.DESC) => objectOfLaborTechnologicalProcedures.OrderByDescending(ooltp => ooltp.TechnologicalProcedure.OrganizationalUnit.OrganizationalUnitName).ToList(),
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
