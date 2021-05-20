using System.Linq;
using ApiProject.Models;

namespace ApiProject.Services.Contexts
{
    internal class MockDbData
    {

        private readonly MockSet<Manufacturer> _manufacturers;
        private readonly MockSet<MechaSwitch> _switches;

        public MockDbData()
        {
            _manufacturers = new MockSet<Manufacturer>(new Manufacturer[]
            {
                new()
                {
                    Id = 1,
                    Name = "Razer"
                },
                new()
                {
                    Id = 2,
                    Name = "Gateron"
                },
                new()
                {
                    Id = 3,
                    Name = "Cherry"
                }
            });

            _switches = new MockSet<MechaSwitch>(new MechaSwitch[]
            {
                new()
                {
                    Id = 1,
                    Manufacturer = _manufacturers.data.FirstOrDefault(m => m.Name == "Razer"),
                    FullName = "Razer Green",
                    Type = SwitchType.Clicky,
                    ActuationForce = 50,
                    BottomOutForce = 65,
                    ActuationDistance = 1.9f,
                    BottomOutDistance = 4.0f,
                    Lifespan = 80_000_000,
                },
                new()
                {
                    Id = 2,
                    Manufacturer = _manufacturers.data.FirstOrDefault(m => m.Name == "Razer"),
                    FullName = "Razer Orange",
                    Type = SwitchType.Tactile,
                    ActuationForce = 45,
                    BottomOutForce = 55,
                    ActuationDistance = 1.9f,
                    BottomOutDistance = 4.0f,
                    Lifespan = 80_000_000
                },
                new()
                {
                    Id = 3,
                    Manufacturer = _manufacturers.data.FirstOrDefault(m => m.Name == "Razer"),
                    FullName = "Razer Yellow",
                    Type = SwitchType.Linear,
                    ActuationForce = 45,
                    BottomOutForce = 75,
                    ActuationDistance = 1.2f,
                    BottomOutDistance = 3.5f,
                    Lifespan = 80_000_000,
                },
                new()
                {
                    Id = 4,
                    Manufacturer = _manufacturers.data.FirstOrDefault(m => m.Name == "Cherry"),
                    FullName = "Cherry Blue",
                    Type = SwitchType.Clicky,
                    ActuationForce = 50,
                    BottomOutForce = 64,
                    ActuationDistance = 2.2f,
                    BottomOutDistance = 4.0f,
                    Lifespan = 100_000_000
                },
                new()
                {
                    Id = 5,
                    Manufacturer = _manufacturers.data.FirstOrDefault(m => m.Name == "Cherry"),
                    FullName = "Cherry Brown",
                    Type = SwitchType.Tactile,
                    ActuationForce = 45,
                    BottomOutForce = 60,
                    ActuationDistance = 2.0f,
                    BottomOutDistance = 4.0f,
                    Lifespan = 100_000_000
                },
                new()
                {
                    Id = 6,
                    Manufacturer = _manufacturers.data.FirstOrDefault(m => m.Name == "Cherry"),
                    FullName = "Cherry Red",
                    Type = SwitchType.Linear,
                    ActuationForce = 45,
                    BottomOutForce = 60,
                    ActuationDistance = 2.0f,
                    BottomOutDistance = 4.0f,
                    Lifespan = 100_000_000
                }
            });

        }

        public MockSet<Manufacturer> GetManufacturersMockData() => _manufacturers;
        public MockSet<MechaSwitch> GetSwitchesMockData() => _switches;
    }

}

