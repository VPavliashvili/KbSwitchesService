using System.Linq;
using System;
using System.Collections.Generic;
using ApiProject.Models;

namespace ApiProject.Services.Contexts
{
    public class MockDbContext : IDisposable
    {

        public static int instanceCount;

        public List<MechaSwitch> Switches { get; set; }

        //mocking incremental Db id
        private List<int> _indexes;

        public MockDbContext()
        {
            instanceCount++;
            _indexes = new();

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

            for (int i = 1; i <= Switches.Count; i++)
            {
                _indexes.Add(i);
            }
        }

        public bool AddRecord(MechaSwitch newSwitchRecord)
        {
            if (_indexes.Contains(newSwitchRecord.Id))
                return false;

            Switches.Add(newSwitchRecord);

            _indexes.Add(newSwitchRecord.Id);
            return true;
        }

        public bool ChangeRecord(MechaSwitch sourceSwitchRecord, int id)
        {
            if (!_indexes.Contains(id))
                return false;

            MechaSwitch switchToEdit = Switches.FirstOrDefault(swt => swt.Id == id);
            int indexOfSwitchToEdit = Switches.IndexOf(switchToEdit);
            switchToEdit.RemapValuesFromSource(sourceSwitchRecord);
            switchToEdit.Id = id;
            Switches[indexOfSwitchToEdit] = switchToEdit;

            return true;
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