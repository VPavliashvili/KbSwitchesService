using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ApiProject.Models;
using ApiProject.Services.Contexts;
using ApiProject.Services.Repositories;
using ApiProject.Services.UnitsOfWork;
using Xunit;
using Xunit.Abstractions;

namespace ApiProject.Test.Switches.RepositoryPart
{
    public partial class MechaSwitchRepositoryTests
    {
        private IUnitOfWork UnitOfWork => new MockUnitOfWork();

        public static IEnumerable<object[]> SwitchExistsByObject_Method_UnexistedData
            => MechaSwitchRepositoryTests_Data.SwitchExistsByObject_Method_UnexistedData;
        public static IEnumerable<object[]> SwitchExistsByObject_Method_ExistedData
            => MechaSwitchRepositoryTests_Data.SwitchExistsByObject_Method_ExistedData;
        public static IEnumerable<object[]> CreateSwitch_Method_Data
            => MechaSwitchRepositoryTests_Data.CreateSwitch_Method_Data;
        public static IEnumerable<object[]> UpdateSwitch_Method_Data
            => MechaSwitchRepositoryTests_Data.UpdateSwitch_Method_Data;

        [Fact]
        public void ShouldNotReturnNull_WhenAccessingSwitchesRepository()
        {
            Assert.NotNull(UnitOfWork.SwitchesRepository);
        }

        [Fact]
        public void ShouldNotReturnNull_WhenGettingAllSwitches()
        {
            IEnumerable<MechaSwitch> allSwitches = UnitOfWork.SwitchesRepository.GetSwitches();

            Assert.NotNull(allSwitches);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void ShouldNotReturnNull_WhenGettingSwitch(int id)
        {
            MechaSwitch certainSwitchById = UnitOfWork.SwitchesRepository.GetSwitch(id);

            Assert.NotNull(certainSwitchById);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void ShouldReturnTrue_WhenCheckingIfExists_WithExistingId(int id)
        {
            bool switchExists = UnitOfWork.SwitchesRepository.SwitchExists(id);

            Assert.True(switchExists, $"Switch record on id {id} does not exists in dbContext");
        }

        [Theory]
        [InlineData(25)]
        [InlineData(15)]
        public void ShouldReturnFalse_WhenCheckingIfExists_WithUnexistedId(int id)
        {
            bool exists = UnitOfWork.SwitchesRepository.SwitchExists(id);

            Assert.False(exists, $"Switch record on id {id} already exists in dbContext");
        }

        [Theory]
        [MemberData(nameof(SwitchExistsByObject_Method_UnexistedData))]
        public void ShouldReturnFalse_WhenCheckingIfExist_WithUnexistingData(MechaSwitch @switch)
        {
            bool switchExists = UnitOfWork.SwitchesRepository.SwitchExists(@switch);

            Assert.False(switchExists, $"Switch {@switch.FullName} already exists in dbContext");
        }

        [Theory]
        [MemberData(nameof(SwitchExistsByObject_Method_ExistedData))]
        public void ShouldReturnTrue_WhenCheckingIfExist_WithExistedData(MechaSwitch @switch)
        {
            bool result = UnitOfWork.SwitchesRepository.SwitchExists(@switch);

            Assert.True(result, $"Switch {@switch.FullName} does not exist in dbContext");
        }

        [Theory]
        [MemberData(nameof(CreateSwitch_Method_Data))]
        public void ShouldReturnTrue_WhenCreatingRecord_WithGoodArgument(MechaSwitch switchToCreate)
        {
            bool switchCreated = UnitOfWork.SwitchesRepository.CreateSwitch(switchToCreate);

            Assert.True(switchCreated, $"Something went wrong in dbContext during Creation of {switchToCreate.FullName} with id {switchToCreate.Id}");
        }

        [Theory]
        [MemberData(nameof(UpdateSwitch_Method_Data))]
        public void ShouldReturnTrue_WhenUpdatingRecord_WithGoodArguments(MechaSwitch sourceSwitch, int id)
        {
            bool switchUpdated = UnitOfWork.SwitchesRepository.UpdateSwitch(sourceSwitch, id);

            Assert.True(switchUpdated, $"Something went wrong in dbContext during update of {sourceSwitch.FullName} with id {sourceSwitch.Id}");
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void ShouldReturnTrue_WhenDeletingRecord_WithExistingIdInDb(int id)
        {
            bool switchDeleted = UnitOfWork.SwitchesRepository.DeleteSwitch(id);

            Assert.True(switchDeleted, $"Something went wrong in dbContext during delete switch record on id {id}");
        }

        [Theory]
        [InlineData(15)]
        public void ShouldReturnFalse_WhenDeletingRecord_WithUnExistedIdInDb(int id)
        {
            bool deleted = UnitOfWork.SwitchesRepository.DeleteSwitch(id);

            Assert.False(deleted, $"There is no record on id {id} in Db");
        }

        [Theory]
        [InlineData(25)]
        public void ShouldReturnTrue_WhenCreatedNewRecordInDb_IsBeingDeleted(int id)
        {
            IMechaSwitchRepository repository = UnitOfWork.SwitchesRepository;

            bool created = repository.CreateSwitch(MechaSwitchRepositoryTests_Data.GetMockSwitchWithCustomId(id));
            Assert.True(created, $"could not create new record");

            bool deleted = repository.DeleteSwitch(id);
            Assert.True(deleted, $"could not delete record on id {id}");
        }

    }

}
