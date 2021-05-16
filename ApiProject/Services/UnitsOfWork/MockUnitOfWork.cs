using System;
using ApiProject.Services.Repositories;
using ApiProject.Services.Contexts;
using ApiProject.Models;
using System.Collections.Generic;

namespace ApiProject.Services.UnitsOfWork
{
    public class MockUnitOfWork : IUnitOfWork
    {
        public readonly MockDbContext _context;
        private bool _methodMockingEnabled;

        public IMechaSwitchRepository SwitchesRepository { get; }
        public IManufacturerRepository ManufacturerRepository { get; }

        public MockUnitOfWork()
        {
            _context = new();

            SwitchesRepository = new MockMechaSwitchRepository(_context);
            ManufacturerRepository = new MockManufacturerRepository(_context);
        }

        public int Complete()
        {
            if (_methodMockingEnabled)
                return int.MinValue;

            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void EnableMethodMocking() => _methodMockingEnabled = true;
        public void DisableMethodMocking() => _methodMockingEnabled = false;

    }

}