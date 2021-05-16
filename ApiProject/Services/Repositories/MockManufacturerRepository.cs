using System.Collections.Generic;
using ApiProject.Models;
using ApiProject.Services.Contexts;

namespace ApiProject.Services.Repositories
{
    public class MockManufacturerRepository : IManufacturerRepository
    {
        private readonly MockDbContext _context;

        public MockManufacturerRepository(MockDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Manufacturer> GetAll()
        {
            return _context.

        }

    }
}