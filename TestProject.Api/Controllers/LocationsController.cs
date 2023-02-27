using Microsoft.AspNetCore.Mvc;
using TestProject.Api.Exceptions;
using TestProject.Application.Contracts;
using TestProject.Application.Models;
using TestProject.Domain;

namespace TestProject.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationRepository _locationsRepository;

        public LocationsController(ILocationRepository locationsRepository)
        {
            _locationsRepository = locationsRepository;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<List<LocationDto>>> GetLocations()
        {
            IReadOnlyList<Location> locations = await _locationsRepository.GetAvailableLocations();

            if (locations == null)
            {
                return NotFound();
            }

            List<LocationDto> locationsToReturn = locations.Select(location => new LocationDto(location.Id, location.Name, location.OpenTime, location.CloseTime)).ToList();
            return Ok(locationsToReturn);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<List<LocationDto>>> GetById(Guid id)
        {
            Location location = await _locationsRepository.GetById(id);

            if (location == null)
            {
                return NotFound();
            }

            LocationDto locationToReturn = new LocationDto(location.Id, location.Name, location.OpenTime, location.CloseTime);
            return Ok(locationToReturn);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<List<LocationDto>>> CreateLocation([FromBody] CreateLocationDto locationDto)
        {
            var validator = new CreateLocationDtoValidator();
            var validationResult = validator.Validate(locationDto);

            if (validationResult.Errors.Any())
                throw new BadRequestException("Invalid Leave Request", validationResult);

            Location entity = new Location
            {
                Name = locationDto.Name,
                OpenTime = locationDto.OpenTime,
                CloseTime = locationDto.CloseTime,
            };

            await _locationsRepository.Create(entity);

            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
        }
    }
}
