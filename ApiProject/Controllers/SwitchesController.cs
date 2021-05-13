using System.Collections.Generic;
using ApiProject.Services.UnitsOfWork;
using ApiProject.Services.Repositories;
using Microsoft.AspNetCore.Mvc;
using ApiProject.Models;
using ApiProject.Dtos;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace ApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SwitchesController : Controller
    {
        private readonly IMechaSwitchRepository _switchesRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SwitchesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _switchesRepository = _unitOfWork.SwitchesRepository;
        }

        // api/switches
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SwitchDto>))]
        public IActionResult GetSwitches()
        {

            ICollection<MechaSwitch> switches = _switchesRepository.GetSwitches();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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

        // api/switches
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

    }
}