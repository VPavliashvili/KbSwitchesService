using System.Collections.Generic;
using ApiProject.Models;

namespace ApiProject.Services.Repositories
{
    public interface IManufacturerRepository
    {
        IEnumerable<Manufacturer> GetAll();
    }
}