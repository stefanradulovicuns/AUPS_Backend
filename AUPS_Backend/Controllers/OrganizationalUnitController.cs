using AUPS_Backend.DTO;
using AUPS_Backend.Entities;
using AUPS_Backend.Enums;
using AUPS_Backend.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AUPS_Backend.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationalUnitController : ControllerBase
    {
        private readonly IOrganizationalUnitRepository _organizationalUnitRepository;
        private readonly IMapper _mapper;

        public OrganizationalUnitController(IOrganizationalUnitRepository organizationalUnitRepository, IMapper mapper)
        {
            _organizationalUnitRepository = organizationalUnitRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<OrganizationalUnitDTO>> GetOrganiaztionalUnits(string? search, string? sortBy, SortOrderOptions? sortOrder, int page, int count)
        {
            var organizationalUnits = await _organizationalUnitRepository.GetAllOrganizationalUnits();

            if (!string.IsNullOrEmpty(search))
            {
                organizationalUnits = organizationalUnits
                    .Where(ou => ou.OrganizationalUnitName.Contains(search, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            organizationalUnits = (sortBy, sortOrder) switch
            {
                (nameof(OrganizationalUnitDTO.OrganizationalUnitId), SortOrderOptions.ASC) => organizationalUnits.OrderBy(ou => ou.OrganizationalUnitId).ToList(),
                (nameof(OrganizationalUnitDTO.OrganizationalUnitId), SortOrderOptions.DESC) => organizationalUnits.OrderByDescending(ou => ou.OrganizationalUnitId).ToList(),
                (nameof(OrganizationalUnitDTO.OrganizationalUnitName), SortOrderOptions.ASC) => organizationalUnits.OrderBy(ou => ou.OrganizationalUnitName).ToList(),
                (nameof(OrganizationalUnitDTO.OrganizationalUnitName), SortOrderOptions.DESC) => organizationalUnits.OrderByDescending(ou => ou.OrganizationalUnitName).ToList(),
                _ => organizationalUnits.OrderBy(ou => ou.OrganizationalUnitName).ThenBy(ou => ou.OrganizationalUnitName).ToList(),
            };

            int totalCount = organizationalUnits.Count();
            organizationalUnits = organizationalUnits.Skip(page > 0 ? (page - 1) * count : 0)
                .Take(count > 0 ? count : totalCount)
                .ToList();

            if (!organizationalUnits.Any())
            {
                return NoContent();
            }

            var organizationalUnitsDto = _mapper.Map<List<OrganizationalUnitDTO>>(organizationalUnits);
            organizationalUnitsDto[0].TotalCount = totalCount;

            return Ok(organizationalUnitsDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrganizationalUnitDTO>> GetOrganizationalUnit(Guid id)
        {
            var organizationalUnit = await _organizationalUnitRepository.GetOrganizationalUnitById(id);

            if (organizationalUnit == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<OrganizationalUnitDTO>(organizationalUnit));
        }

        [HttpPost]
        public async Task<ActionResult<OrganizationalUnitDTO>> CreateOrganizationalUnit(OrganizationalUnitCreateDTO organizationalUnit)
        {
            var createdOrganizationalUnit = await _organizationalUnitRepository.AddOrganizationalUnit(_mapper.Map<OrganizationalUnit>(organizationalUnit));

            return CreatedAtAction("GetOrganizationalUnit", new { id = createdOrganizationalUnit.OrganizationalUnitId }, _mapper.Map<OrganizationalUnitDTO>(createdOrganizationalUnit));
        }

        [HttpPut]
        public async Task<ActionResult<OrganizationalUnitDTO>> UpdateOrganizationalUnit(OrganizationalUnitUpdateDTO organizationalUnit)
        {
            var matchingOrganizationalUnit = await _organizationalUnitRepository.GetOrganizationalUnitById(organizationalUnit.OrganizationalUnitId);
            if (matchingOrganizationalUnit == null)
            {
                return NotFound();
            }

            var updatedOrganizationalUnit = await _organizationalUnitRepository.UpdateOrganizationalUnit(_mapper.Map<OrganizationalUnit>(organizationalUnit));

            return Ok(_mapper.Map<OrganizationalUnitDTO>(updatedOrganizationalUnit));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrganizationalUnit(Guid id)
        {
            bool isDeleted = await _organizationalUnitRepository.DeleteOrganizationalUnit(id);
            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
