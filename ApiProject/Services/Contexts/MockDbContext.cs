using System.Linq;
using System;
using System.Collections.Generic;
using ApiProject.Models;

namespace ApiProject.Services.Contexts
{
    public class MockDbContext : IDbContext
    {
        public IEnumerable<MechaSwitch> Switches => _switches.data;
        public IEnumerable<Manufacturer> Manufacturers => _manufacturers.data;

        private readonly MockSet<MechaSwitch> _switches;
        private readonly MockSet<Manufacturer> _manufacturers;

        public MockDbContext()
        {
            MockDbData data = new();
            _manufacturers = data.GetManufacturersMockData();
            _switches = data.GetSwitchesMockData();
        }

        public bool CreateSwitch(MechaSwitch newSwitchRecord)
        {
            if (_switches.indexes.Contains(newSwitchRecord.Id))
                return false;

            _switches.data.Add(newSwitchRecord);

            _switches.indexes.Add(newSwitchRecord.Id);
            return true;
        }

        public bool UpdateSwitch(MechaSwitch sourceSwitchRecord, int id)
        {
            if (!_switches.indexes.Contains(id))
                return false;

            MechaSwitch switchToEdit = _switches.data.FirstOrDefault(swt => swt.Id == id);
            int indexOfSwitchToEdit = _switches.data.IndexOf(switchToEdit);
            switchToEdit.RemapValuesFromSource(sourceSwitchRecord);
            switchToEdit.Id = id;
            _switches.data[indexOfSwitchToEdit] = switchToEdit;

            return true;
        }

        public bool DeleteSwitch(int id)
        {
            if (!_switches.indexes.Contains(id))
                return false;

            MechaSwitch switchToDelete = _switches.data.FirstOrDefault(swt => swt.Id == id);
            int indexOfSwitchToDelete = _switches.data.IndexOf(switchToDelete);
            _switches.data.RemoveAt(indexOfSwitchToDelete);

            return true;
        }

        public bool CreateManufacturer(Manufacturer newRecord)
        {
            if (_manufacturers.indexes.Contains(newRecord.Id)
            || _manufacturers.data.FirstOrDefault(m => m.Name == newRecord.Name) != null)
                return false;

            _manufacturers.data.Add(newRecord);

            _manufacturers.indexes.Add(newRecord.Id);
            return true;
        }

        public bool UpdateManufacturer(Manufacturer sourceRecord, int id)
        {
            if (!_manufacturers.indexes.Contains(id))
                return false;

            Manufacturer manufacturerToEdit = _manufacturers.data.FirstOrDefault(m => m.Id == id);
            int indexOfManufacturerToEdit = _manufacturers.data.IndexOf(manufacturerToEdit);
            manufacturerToEdit.RemapValuesFromSource(sourceRecord);
            manufacturerToEdit.Id = id;
            _manufacturers.data[indexOfManufacturerToEdit] = manufacturerToEdit;

            return true;
        }

        public bool DeleteManufacturer(int id)
        {
            if (!_manufacturers.indexes.Contains(id))
                return false;

            Manufacturer manufacturerToDelete = _manufacturers.data.FirstOrDefault(m => m.Id == id);
            int indexOfManufacturerToDelete = _manufacturers.data.IndexOf(manufacturerToDelete);
            _manufacturers.data.RemoveAt(indexOfManufacturerToDelete);

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

