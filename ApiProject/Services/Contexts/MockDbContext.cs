using System;
using System.Collections.Generic;
using ApiProject.Models;

namespace ApiProject.Services.Contexts
{
    public class MockDbContext : IDisposable
    {
        public virtual IEnumerable<MechaSwitch> Switches { get; set; }

        public MockDbContext()
        {
            Switches = new List<MechaSwitch>
            {
                new MechaSwitch()
                {
                    Id = 1,
                    Manufacturer = "Razer",
                    FullName = "Razer Green",
                    Type = SwitchType.Clicky,
                    ActuationForce = 50,
                    BottomOutForce = 65,
                    ActuationDistance = 1.9f,
                    BottomOutDistance = 4.0f,
                    Lifespan = 80_000_000,
                },
                new MechaSwitch()
                {
                    Id = 2,
                    Manufacturer = "Razer",
                    FullName = "Razer Orange",
                    Type = SwitchType.Tactile,
                    ActuationForce = 45,
                    BottomOutForce = 55,
                    ActuationDistance = 1.9f,
                    BottomOutDistance = 4.0f,
                    Lifespan = 80_000_000
                },
                new MechaSwitch()
                {
                    Id = 3,
                    Manufacturer = "Razer",
                    FullName = "Razer Yellow",
                    Type = SwitchType.Linear,
                    ActuationForce = 45,
                    BottomOutForce = 75,
                    ActuationDistance = 1.2f,
                    BottomOutDistance = 3.5f,
                    Lifespan = 80_000_000,
                }
            };
        }

        public int SaveChanges()
        {
            return 1;
        }

        public void Dispose()
        {
            return;
        }

    }

}