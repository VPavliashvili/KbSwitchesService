using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ApiProject.Models;
using ApiProject.Services.Contexts;
using ApiProject.Controllers;
using ApiProject.SortFilteringSearchAndPaging;

namespace ApiProject.Services.Repositories
{
    public class MockMechaSwitchRepository : IMechaSwitchRepository
    {

        public bool MethodMockingEnabled { get; private set; }
        public int RecordsCountInDb => _dbContext.Switches.Count();

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

        public PagedList<MechaSwitch> GetSwitches(SwitchesParameters parameters)
        {
            Func<MechaSwitch, bool> filterExpression = Utils.GetSwitchesFilterExpression(parameters);

            IQueryable<MechaSwitch> filteredSwitches =
                _dbContext.Switches
                .Where(filterExpression)
                .OrderBy(swt => swt.Manufacturer.Id)
                .AsQueryable();

            SearchByName(ref filteredSwitches, parameters.Name);

            return PagedList<MechaSwitch>.ToPagedList
            (
                filteredSwitches,
                parameters.PageNumber,
                parameters.PageSize
            );
        }

        private void SearchByName(ref IQueryable<MechaSwitch> switches, string name)
        {
            if (!switches.Any() || string.IsNullOrWhiteSpace(name))
                return;
            switches = switches.Where(stw => stw.FullName.ToUpperInvariant().Contains(name.Trim().ToUpperInvariant()));
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