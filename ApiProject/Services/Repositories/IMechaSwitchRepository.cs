using System.Collections.Generic;
using ApiProject.Models;

namespace ApiProject.Services.Repositories
{
    public interface IMechaSwitchRepository
    {
        ICollection<MechaSwitch> GetSwitches();
        MechaSwitch GetSwitch(int id);
        bool SwitchExists(int id);
        bool SwitchExists(MechaSwitch @switch);

        //TO DO create, update, delete
        bool CreateSwitch(MechaSwitch switchToCreate);
        bool UpdateSwitch(MechaSwitch sourceSwitch, int id);
        bool DeleteSwitch(int id);
    }
}