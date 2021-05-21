using System.Collections.Generic;
using ApiProject.Controllers;
using ApiProject.Models;
using ApiProject.SortFilteringSearchAndPaging;

namespace ApiProject.Services.Repositories
{
    public interface IMechaSwitchRepository
    {
        public int RecordsCountInDb { get; }

        PagedList<MechaSwitch> GetSwitches(SwitchesParameters switchesParameters);
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