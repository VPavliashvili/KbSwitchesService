using System.Collections.Generic;
using ApiProject.Models;

namespace ApiProject.Services.Repositories
{
    public interface IManufacturerRepository
    {
        ICollection<Manufacturer> GetAll();
        Manufacturer GetById(int id);
        bool Exists(int id);
        bool Exists(Manufacturer manufacturer);

        bool Create(Manufacturer manufacturerTocreate);
        bool Update(Manufacturer source, int id);
        bool Delete(int id);
    }
}