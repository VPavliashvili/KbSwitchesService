using System.Linq;
using System;
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

        public ICollection<Manufacturer> GetAll()
        {
            return _context.Manufacturers.OrderBy(m => m.Name).ToList();
        }

        public Manufacturer GetById(int id)
        {
            return _context.Manufacturers.FirstOrDefault(m => m.Id == id);
        }

        public bool Exists(int id)
        {
            return _context.Manufacturers.Any(m => m.Id == id);
        }

        public bool Exists(Manufacturer manufacturer)
        {
            return _context.Manufacturers.Any(m => m.IsSameManufacturer(manufacturer));
        }

        public bool Exists(string name)
        {
            return _context.Manufacturers.Any(m => m.Name.EqualsIgnoreCase(name));
        }

        public bool Create(Manufacturer manufacturerTocreate)
        {
            return _context.CreateManufacturer(manufacturerTocreate);
        }

        public bool Update(Manufacturer source, int id)
        {
            return _context.UpdateManufacturer(source, id);
        }

        public bool Delete(int id)
        {
            return _context.DeleteManufacturer(id);
        }

    }
}
