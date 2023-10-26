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
    public class TechnologicalProcedureController : ControllerBase
    {
        private readonly ITechnologicalProcedureRepository _technologicalProcedureRepository;
        private readonly IMapper _mapper;

        public TechnologicalProcedureController(ITechnologicalProcedureRepository technologicalProcedureRepository, IMapper mapper)
        {
            _technologicalProcedureRepository = technologicalProcedureRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<TechnologicalProcedureDTO>> GetTechnologicalProcedures(string? search, string? sortBy, SortOrderOptions? sortOrder, int page, int count)
        {
            var technologicalProcedures = await _technologicalProcedureRepository.GetAllTechnologicalProcedures();

            if (!string.IsNullOrEmpty(search))
            {
                technologicalProcedures = technologicalProcedures
                    .Where(tp => tp.TechnologicalProcedureName.Contains(search, StringComparison.OrdinalIgnoreCase) 
                                || tp.Duration.ToString().Contains(search, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            technologicalProcedures = (sortBy, sortOrder) switch
            {
                (nameof(TechnologicalProcedureDTO.TechnologicalProcedureId), SortOrderOptions.ASC) => technologicalProcedures.OrderBy(tp => tp.TechnologicalProcedureId).ToList(),
                (nameof(TechnologicalProcedureDTO.TechnologicalProcedureId), SortOrderOptions.DESC) => technologicalProcedures.OrderByDescending(tp => tp.TechnologicalProcedureId).ToList(),
                (nameof(TechnologicalProcedureDTO.TechnologicalProcedureName), SortOrderOptions.ASC) => technologicalProcedures.OrderBy(tp => tp.TechnologicalProcedureName).ToList(),
                (nameof(TechnologicalProcedureDTO.TechnologicalProcedureName), SortOrderOptions.DESC) => technologicalProcedures.OrderByDescending(tp => tp.TechnologicalProcedureName).ToList(),
                (nameof(TechnologicalProcedureDTO.Duration), SortOrderOptions.ASC) => technologicalProcedures.OrderBy(tp => tp.Duration).ToList(),
                (nameof(TechnologicalProcedureDTO.Duration), SortOrderOptions.DESC) => technologicalProcedures.OrderByDescending(tp => tp.Duration).ToList(),
                (nameof(TechnologicalProcedureDTO.OrganizationalUnitId), SortOrderOptions.ASC) => technologicalProcedures.OrderBy(tp => tp.OrganizationalUnitId).ToList(),
                (nameof(TechnologicalProcedureDTO.OrganizationalUnitId), SortOrderOptions.DESC) => technologicalProcedures.OrderByDescending(tp => tp.OrganizationalUnitId).ToList(),
                (nameof(TechnologicalProcedureDTO.OrganizationalUnitName), SortOrderOptions.ASC) => technologicalProcedures.OrderBy(tp => tp.OrganizationalUnit.OrganizationalUnitName).ToList(),
                (nameof(TechnologicalProcedureDTO.OrganizationalUnitName), SortOrderOptions.DESC) => technologicalProcedures.OrderByDescending(tp => tp.OrganizationalUnit.OrganizationalUnitName).ToList(),
                (nameof(TechnologicalProcedureDTO.PlantId), SortOrderOptions.ASC) => technologicalProcedures.OrderBy(tp => tp.PlantId).ToList(),
                (nameof(TechnologicalProcedureDTO.PlantId), SortOrderOptions.DESC) => technologicalProcedures.OrderByDescending(tp => tp.PlantId).ToList(),
                (nameof(TechnologicalProcedureDTO.PlantName), SortOrderOptions.ASC) => technologicalProcedures.OrderBy(tp => tp.Plant.PlantName).ToList(),
                (nameof(TechnologicalProcedureDTO.PlantName), SortOrderOptions.DESC) => technologicalProcedures.OrderByDescending(tp => tp.Plant.PlantName).ToList(),
                (nameof(TechnologicalProcedureDTO.TechnologicalSystemId), SortOrderOptions.ASC) => technologicalProcedures.OrderBy(tp => tp.TechnologicalSystemId).ToList(),
                (nameof(TechnologicalProcedureDTO.TechnologicalSystemId), SortOrderOptions.DESC) => technologicalProcedures.OrderByDescending(tp => tp.TechnologicalSystemId).ToList(),
                (nameof(TechnologicalProcedureDTO.TechnologicalSystemName), SortOrderOptions.ASC) => technologicalProcedures.OrderBy(tp => tp.TechnologicalSystem.TechnologicalSystemName).ToList(),
                (nameof(TechnologicalProcedureDTO.TechnologicalSystemName), SortOrderOptions.DESC) => technologicalProcedures.OrderByDescending(tp => tp.TechnologicalSystem.TechnologicalSystemName).ToList(),
                _ => technologicalProcedures.OrderBy(tp => tp.TechnologicalProcedureName).ToList(),
            };

            int totalCount = technologicalProcedures.Count();
            technologicalProcedures = technologicalProcedures.Skip(page > 0 ? (page - 1) * count : 0)
                .Take(count > 0 ? count : totalCount)
                .ToList();

            if (!technologicalProcedures.Any())
            {
                return NoContent();
            }

            var technologicalProceduresDto = _mapper.Map<List<TechnologicalProcedureDTO>>(technologicalProcedures);
            technologicalProceduresDto[0].TotalCount = totalCount;

            return Ok(technologicalProceduresDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TechnologicalProcedureDTO>> GetTechnologicalProcedure(Guid id)
        {
            var technologicalProcedure = await _technologicalProcedureRepository.GetTechnologicalProcedureById(id);

            if (technologicalProcedure == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<TechnologicalProcedureDTO>(technologicalProcedure));
        }

        [HttpPost]
        public async Task<ActionResult<TechnologicalProcedureDTO>> CreateTechnologicalProcedure(TechnologicalProcedureCreateDTO technologicalProcedure)
        {
            var createdTechnologicalProcedure = await _technologicalProcedureRepository.AddTechnologicalProcedure(_mapper.Map<TechnologicalProcedure>(technologicalProcedure));

            return CreatedAtAction("GetTechnologicalProcedure", new { id = createdTechnologicalProcedure.TechnologicalProcedureId }, _mapper.Map<TechnologicalProcedureDTO>(createdTechnologicalProcedure));
        }

        [HttpPut]
        public async Task<ActionResult<TechnologicalProcedureDTO>> UpdateTechnologicalProcedure(TechnologicalProcedureUpdateDTO technologicalProcedure)
        {
            var matchingTechnologicalProcedure = await _technologicalProcedureRepository.GetTechnologicalProcedureById(technologicalProcedure.TechnologicalProcedureId);
            if (matchingTechnologicalProcedure == null)
            {
                return NotFound();
            }

            var updatedTechnologicalProcedure = await _technologicalProcedureRepository.UpdateTechnologicalProcedure(_mapper.Map<TechnologicalProcedure>(technologicalProcedure));

            return Ok(_mapper.Map<TechnologicalProcedureDTO>(updatedTechnologicalProcedure));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTechnologicalProcedure(Guid id)
        {
            bool isDeleted = await _technologicalProcedureRepository.DeleteTechnologicalProcedure(id);
            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
