using System;
using System.Collections.Generic;
using ApiProject.Models;

namespace ApiProject.Services.Contexts
{
    public interface IDbContext : IDisposable
    {
        IEnumerable<MechaSwitch> Switches { get; }
        bool CreateSwitch(MechaSwitch newRecord);
        bool UpdateSwitch(MechaSwitch sourceRecord, int id);
        bool DeleteSwitch(int id);

        IEnumerable<Manufacturer> Manufacturers { get; }
        bool CreateManufacturer(Manufacturer newRecord);
        bool UpdateManufacturer(Manufacturer sourceRecord, int id);
        bool DeleteManufacturer(int id);

        int SaveChanges();
    }
}