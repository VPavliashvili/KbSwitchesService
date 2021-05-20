using System.Security.Cryptography.X509Certificates;
using ApiProject.Models;

namespace ApiProject.Test.Helpers
{
    public static class Data
    {

        public static readonly Manufacturer Razer = new()
        {
            Id = 1,
            Name = "Razer"
        };

        public static readonly Manufacturer Gateron = new()
        {
            Id = 2,
            Name = "Gateron"
        };

        public static readonly Manufacturer Cherry = new()
        {
            Id = 3,
            Name = "Cherry"
        };

        public static readonly Manufacturer Kailh = new()
        {
            Id = 4,
            Name = "Kailh"
        };

    }
}