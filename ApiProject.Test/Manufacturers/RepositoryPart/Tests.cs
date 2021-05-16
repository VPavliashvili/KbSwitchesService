using System.Collections.Generic;
using ApiProject.Models;
using ApiProject.Services.Repositories;
using ApiProject.Services.UnitsOfWork;
using Xunit;

namespace ApiProject.Test.Manufacturers.RepositoryPart
{
    public class Tests
    {
        private IManufacturerRepository Repository => new MockUnitOfWork().ManufacturerRepository;

        [Fact]
        public void ShouldNotReturnNull_WhenGettingAllSwitches()
        {
            IEnumerable<Manufacturer> result = Repository.GetAll();

            Assert.NotNull(result);
        }

        // [Theory]
        // [InlineData("Razer")]
        // [InlineData("Gateron")]
        // [InlineData("Cherry")]
        // public void ShouldContainPassedManufacturer(string manufacturerName)
        // {
        //     // bool result = Repository.
        // }

    }
}