using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ApiProject.Models;
using ApiProject.Services.Contexts;

namespace ApiProject.Services.Repositories
{
    public class MockMechaSwitchRepository : IMechaSwitchRepository
    {

        public bool MethodMockingEnabled { get; private set; }

        private readonly MockDbContext _dbContext;

        public MockMechaSwitchRepository(MockDbContext context)
        {
            _dbContext = context;
        }

        public MechaSwitch GetSwitch(int id)
        {
            return _dbContext.Switches
                    .FirstOrDefault(@switch => @switch.Id == id);
        }

        public ICollection<MechaSwitch> GetSwitches()
        {
            return _dbContext.Switches.OrderBy(@switch => @switch.Manufacturer.Name).ToList();
        }

        public ICollection<MechaSwitch> GetSwitchesOfManufacturer(int manufacturerId)
        {
            return _dbContext.Switches
                .Where(@switch => @switch.Manufacturer.Id == manufacturerId)
                .ToList();
        }

        public Manufacturer GetManufacturerOfSwitch(int switchId)
        {
            return _dbContext.Switches
                .FirstOrDefault(@switch => @switch.Id == switchId)
                ?.Manufacturer;
        }

        public bool SwitchExists(int id)
        {
            return _dbContext.Switches.Any(@switch => @switch.Id == id);
        }

        public bool SwitchExists(MechaSwitch @switch)
        {
            return _dbContext.Switches.Any(swt => swt.IsSameSwitch(@switch));
        }

        public bool CreateSwitch(MechaSwitch switchToCreate)
        {
            return _dbContext.CreateSwitch(switchToCreate) && !MethodMockingEnabled;
        }

        public bool UpdateSwitch(MechaSwitch sourceSwitch, int id)
        {
            return _dbContext.UpdateSwitch(sourceSwitch, id) && !MethodMockingEnabled;
        }

        public bool DeleteSwitch(int id)
        {
            return _dbContext.DeleteSwitch(id) && !MethodMockingEnabled;
        }

        public void EnableMethodMocking() => MethodMockingEnabled = true;
        public void DisableMethodMocking() => MethodMockingEnabled = false;

    }

}