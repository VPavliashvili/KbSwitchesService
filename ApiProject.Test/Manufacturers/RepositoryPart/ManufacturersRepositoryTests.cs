using System.Runtime.Intrinsics.X86;
using System.Collections.Generic;
using System.Linq;
using ApiProject.Models;
using ApiProject.Services.Repositories;
using ApiProject.Services.UnitsOfWork;
using Xunit;

namespace ApiProject.Test.Manufacturers.RepositoryPart
{
    public class ManufacturersRepositoryTests
    {
        private IManufacturerRepository Repository => new MockUnitOfWork().ManufacturerRepository;

        [Fact]
        public void ShouldNotReturnNull_WhenGettingAllManufacturers()
        {
            ICollection<Manufacturer> result = Repository.GetAll();

            Assert.NotNull(result);
        }

        [Theory]
        [InlineData("Razer")]
        [InlineData("Gateron")]
        [InlineData("Cherry")]
        public void ShouldReturnTrue_WhenLookingForThisNamesInManufacturersDb(string manufacturerName)
        {
            ICollection<Manufacturer> manufacturers = Repository.GetAll();

            Assert.True(manufacturers.Select(m => m.Name).Contains(manufacturerName),
                $"manufacturer with name {manufacturerName} does not exist");
        }

        [Theory]
        [InlineData("InvalidManufacturerName1")]
        [InlineData("InvalidManufacturerName2")]
        [InlineData("InvalidManufacturerName3")]
        public void ShoultReturnFalse_WhenLookingForInvalidManufacturersInDb(string invalidName)
        {
            ICollection<Manufacturer> manufacturers = Repository.GetAll();

            Assert.False(manufacturers.Select(m => m.Name).Contains(invalidName),
                $"manufacturer with name {invalidName} should not be present in mockDb");
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void ShouldNotReturnNull_WhenGettingManufacturerById(int id)
        {
            Manufacturer manufacturer = Repository.GetById(id);

            Assert.NotNull(manufacturer);
        }

        [Theory]
        [InlineData(25)]
        [InlineData(27)]
        public void ShouldReturnNull_WhenGettingManufacturerByInvalidId(int invalidId)
        {
            Manufacturer manufacturer = Repository.GetById(invalidId);

            Assert.Null(manufacturer);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void ShoultReturnTrue_WhenCheckingIfManufacturerExists(int id)
        {
            bool result = Repository.Exists(id);

            Assert.True(result);
        }

        [Theory]
        [InlineData(25)]
        [InlineData(27)]
        public void ShouldReturnFalse_WhenCheckingIfManufacturerExitWithInvalidId(int invalidId)
        {
            bool result = Repository.Exists(invalidId);

            Assert.False(result);
        }

        [Theory]
        [InlineData("Razer")]
        [InlineData("Gateron")]
        [InlineData("Cherry")]
        public void ShouldReturnTrue_WhenCheckingIfManufacturerExistWithObject(string name)
        {
            Manufacturer manufacturer = new() { Name = name };
            bool result = Repository.Exists(manufacturer);

            Assert.True(result);
        }

        [Theory]
        [InlineData("invalidName1")]
        [InlineData("invalidName2")]
        public void ShouldReturnFalse_WhenCheckingIfManufacturerExstWithInvalidObject(string name)
        {
            Manufacturer manufacturer = new() { Name = name };
            bool result = Repository.Exists(manufacturer);

            Assert.False(result);
        }

        [Theory]
        [InlineData(15, "Oetemu")]
        [InlineData(10, "Glorious")]
        public void ShouldReturnTrue_WhenCreatingRecord_WithGoodArgument(int id, string name)
        {
            Manufacturer manufacturer = new() { Id = id, Name = name };
            bool result = Repository.Create(manufacturer);

            Assert.True(result);
        }

        [Theory]
        [InlineData(1, "Oetemu")]
        [InlineData(15, "Razer")]
        public void ShoulReturnFalse_WhenCreatingRecord_WithInvalidArgument(int id, string name)
        {
            Manufacturer manufacturer = new() { Id = id, Name = name };
            bool result = Repository.Create(manufacturer);

            Assert.False(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void ShouldReturnTrue_WhenUpdatingRecord_WithValidId(int id)
        {
            Manufacturer manufacturer = new() { Name = "exampleName" };
            bool result = Repository.Update(manufacturer, id);

            Assert.True(result, $"Record on id {id} does not exist in Db to update");
        }

        [Theory]
        [InlineData(4)]
        [InlineData(10)]
        public void ShouldReturnFalse_WhenUpdatingRecord_WithInvalidId(int id)
        {
            Manufacturer manufacturer = new() { Name = "exampleName" };
            bool result = Repository.Update(manufacturer, id);

            Assert.False(result, $"Record on id {id} exists");
        }

        [Theory]
        [InlineData(1)]
        public void ShouldReturnTrue_WhenDeletingRecord_WithValidId(int id)
        {
            bool result = Repository.Delete(id);

            Assert.True(result, $"Record on id {id} does not exist in Db to delete");
        }

    }
}
