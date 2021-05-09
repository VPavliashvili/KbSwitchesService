using System.Collections;
using System.Collections.Generic;
using ApiProject.Models;

namespace ApiProject.Services.Contexts
{
    public interface IContext
    {
        IEnumerable<MechaSwitch> Switches { get; set; }
    }

}