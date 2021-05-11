using System.Collections.Generic;
using ApiProject.Services.UnitsOfWork;
using ApiProject.Services.Repositories;
using Microsoft.AspNetCore.Mvc;
using ApiProject.Models;
using ApiProject.Dtos;
using System.Linq;

namespace ApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SwitchesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMechaSwitchRepository _switchesRepository => _unitOfWork.SwitchesRepository;

        public SwitchesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
        [HttpGet("{switchId}")]
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

    }
}