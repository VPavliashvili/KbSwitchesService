using System.Collections.Generic;
using ApiProject.Controllers;
using ApiProject.Models;

namespace ApiProject.Services.Repositories
{
    public interface IMechaSwitchRepository
    {
        PagedList<MechaSwitch> GetSwitches(Controllers.SwitchesParameters switchesParameters);
        ICollection<MechaSwitch> GetSwitchesOfManufacturer(int manufacturerId);
        MechaSwitch GetSwitch(int id);
        Manufacturer GetManufacturerOfSwitch(int switchId);
        bool SwitchExists(int id);
        bool SwitchExists(MechaSwitch @switch);

        bool CreateSwitch(MechaSwitch switchToCreate);
        bool UpdateSwitch(MechaSwitch sourceSwitch, int id);
        bool DeleteSwitch(int id);
    }
}