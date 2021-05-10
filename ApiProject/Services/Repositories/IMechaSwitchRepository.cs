using System.Collections.Generic;
using ApiProject.Models;

namespace ApiProject.Services.Repositories
{
    public interface IMechaSwitchRepository
    {
        ICollection<MechaSwitch> GetSwitches();
        MechaSwitch GetSwitch(int id);
        bool SwtichExists(int id);

        //TO DO create, update, delete
    }
}