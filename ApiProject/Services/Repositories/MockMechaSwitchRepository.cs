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
            return _dbContext.Switches.OrderBy(@switch => @switch.Manufacturer).ToList();
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
            return _dbContext.AddRecord(switchToCreate);
        }

        public bool UpdateSwitch(MechaSwitch sourceSwitch, int id)
        {
            return _dbContext.ChangeRecord(sourceSwitch, id);
        }

        public bool DeleteSwitch(int id)
        {
            return _dbContext.DeleteRecord(id);
        }
    }

}