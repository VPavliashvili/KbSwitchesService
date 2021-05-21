using System;
using System.Collections.Generic;
using ApiProject.Services.UnitsOfWork;
using ApiProject.Services.Repositories;
using Microsoft.AspNetCore.Mvc;
using ApiProject.Models;
using ApiProject.Dtos;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ApiProject.SortFilteringSearchAndPaging;

namespace ApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SwitchesController : Controller
    {
        private readonly IMechaSwitchRepository _switchesRepository;
        private readonly IManufacturerRepository _manufacturerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SwitchesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _switchesRepository = _unitOfWork.SwitchesRepository;
            _manufacturerRepository = _unitOfWork.ManufacturerRepository;
        }

        // api/switches
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SwitchDto>))]
        public IActionResult GetSwitches([FromQuery] SwitchesParameters switchesParameters)
        {

            PagedList<MechaSwitch> switches = _switchesRepository.GetSwitches(switchesParameters);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var metadata = new
            {
                switches.TotalCount,
                switches.PageSize,
                switches.CurrentPage,
                switches.TotalPages,
                switches.HasNext,
                switches.HasPrevious
            };
            if (Response != null)
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(switches.Select(swt => swt.ToDto()));
        }

        // api/switches/switchId
        [HttpGet("{switchId}", Name = "GetSwitch")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SwitchDto))]
        public IActionResult GetSwitch(int switchId)
        {
            if (!_switchesRepository.SwitchExists(switchId))
            {
                return NotFound();
            }

            MechaSwitch @switch = _switchesRepository.GetSwitch(switchId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SwitchDto switchDto = @switch.ToDto();
            return Ok(switchDto);
        }

        // api/switches
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MechaSwitch))]
        public IActionResult CreateSwitch([FromBody] MechaSwitch switchToCreate)
        {

            if (switchToCreate == null)
            {
                ModelState.AddModelError(string.Empty,
                    $"Passed argument should not be null and typeof{typeof(MechaSwitch)}");

                return BadRequest(ModelState);
            }

            if (_switchesRepository.SwitchExists(switchToCreate))
            {
                return UnprocessableEntity(ModelState);
            }

            if (!_switchesRepository.CreateSwitch(switchToCreate))
            {
                ModelState.AddModelError(string.Empty,
                    $"Something went wrong saving {switchToCreate.FullName}");

                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }

            if (_unitOfWork.Complete() <= 0)
            {
                ModelState.AddModelError(string.Empty,
                    $"Something went wrong completing during cration of {switchToCreate.FullName}");

                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }

            return CreatedAtAction("GetSwitch", new { switchId = switchToCreate.Id }, switchToCreate);
        }

        // api/switches/switchId
        [HttpPatch("{switchId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult UpdateSwitch([FromBody] MechaSwitch source, int switchId)
        {

            if (!_switchesRepository.SwitchExists(switchId))
            {
                return NotFound(ModelState);
            }

            if (source == null)
            {
                ModelState.AddModelError(string.Empty,
                    $"MechaSwitch parameter '{nameof(source)}' is null'");
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_switchesRepository.UpdateSwitch(source, switchId))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }

            if (_unitOfWork.Complete() <= 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }

            return NoContent();
        }

        // api/switches/switchId
        [HttpDelete("{switchId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult DeleteSwitch(int switchId)
        {

            if (!_switchesRepository.SwitchExists(switchId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_switchesRepository.DeleteSwitch(switchId))
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            if (_unitOfWork.Complete() <= 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }

            return NoContent();
        }

        // api/switches/switchId/manufacturer
        [HttpGet("{switchId}/manufacturer")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ManufacturerDto))]
        public IActionResult GetManufacturerOfSwitch(int switchId)
        {
            if (!_switchesRepository.SwitchExists(switchId))
                return NotFound();

            Manufacturer manufacturer = _switchesRepository.GetManufacturerOfSwitch(switchId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            ManufacturerDto asDto = new()
            {
                Id = manufacturer.Id,
                Name = manufacturer.Name
            };

            return Ok(asDto);
        }

        // api/switches/manufacturers/manufacturerId
        [HttpGet("manufacturers/{manufacturerId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SwitchDto>))]
        public IActionResult GetSwitchesOfManufacturer(int manufacturerId)
        {
            if (!_manufacturerRepository.Exists(manufacturerId))
                return NotFound();

            ICollection<MechaSwitch> switches = _switchesRepository.GetSwitchesOfManufacturer(manufacturerId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(switches.Select(swt => swt.ToDto()));
        }

    }

}
