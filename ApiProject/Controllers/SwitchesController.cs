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
        public IActionResult GetSwitch(int switchId)
        {
            if (!_switchesRepository.SwtichExists(switchId))
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

            _unitOfWork.Complete();
            return CreatedAtAction("GetSwitch", new { switchId = switchToCreate.Id }, switchToCreate);
        }

    }
}