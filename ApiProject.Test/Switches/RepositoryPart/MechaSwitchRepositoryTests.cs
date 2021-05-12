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

        public static IEnumerable<object[]> MechaSwitchRepository_SwitchExistsByObject_Method_Data
            => MechaSwitchRepositoryTests_Data.MechaSwitchRepository_SwitchExistsByObject_Method_Data;
        public static IEnumerable<object[]> Test_MechaSwitchRepository_CreateSwitch_Method_Data
            => MechaSwitchRepositoryTests_Data.Test_MechaSwitchRepository_CreateSwitch_Method_Data;
        public static IEnumerable<object[]> Test_MechaSwitchRepository_UpdateSwitch_Method_Data
            => MechaSwitchRepositoryTests_Data.Test_MechaSwitchRepository_UpdateSwitch_Method_Data;

        [Fact]
        public void Test_MockDbContext_Concrete_Implementation()
        {
            MockDbContext mockDbContext = new();

            var switches = mockDbContext.Switches;

            Assert.NotNull(switches);
        }

        [Fact]
        public void Test_IMechaSwitchRepository_Concrete_Implementation_With_MockUnitOfWork()
        {
            MockUnitOfWork mockUnitOfWork = new();

            IMechaSwitchRepository mechaSwitchRepository = mockUnitOfWork.SwitchesRepository;

            Assert.NotNull(mechaSwitchRepository);
        }

        [Fact]
        public void Test_ISwitchesRepository_Interface()
        {
            Assert.NotNull(UnitOfWork.SwitchesRepository);
        }

        [Fact]
        public void Test_MechaSwitchRepository_GetSwitches_Method()
        {
            IEnumerable<MechaSwitch> allSwitches = UnitOfWork.SwitchesRepository.GetSwitches();

            Assert.NotNull(allSwitches);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Test_MechaSwitchRepository_GetSwitch_Method(int id)
        {
            MechaSwitch certainSwitchById = UnitOfWork.SwitchesRepository.GetSwitch(id);

            Assert.NotNull(certainSwitchById);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Test_MechaSwitchRepository_SwitchExists_Method(int id)
        {
            bool switchExists = UnitOfWork.SwitchesRepository.SwtichExists(id);

            //i want switch to exist, because wanna get one with given id from db
            Assert.True(switchExists, $"Switch record on id {id} does not exists in dbContext");
        }

        [Theory]
        [MemberData(nameof(MechaSwitchRepository_SwitchExistsByObject_Method_Data))]
        public void Test_MechaSwitchRepository_SwitchAlreadyExists_Method(MechaSwitch @switch)
        {
            bool switchExists = UnitOfWork.SwitchesRepository.SwitchExists(@switch);

            //i dont want @switch to exist, because wanna create new in db
            Assert.False(switchExists, $"Switch {@switch.FullName} already exists in dbContext");
        }

        [Theory]
        [MemberData(nameof(Test_MechaSwitchRepository_CreateSwitch_Method_Data))]
        public void Test_MechaSwitchRepository_CreateSwitch_Method(MechaSwitch switchToCreate)
        {
            bool switchCreated = UnitOfWork.SwitchesRepository.CreateSwitch(switchToCreate);

            Assert.True(switchCreated, $"Something went wrong in dbContext during Creation of {switchToCreate.FullName} with id {switchToCreate.Id}");
        }

        [Theory]
        [MemberData(nameof(Test_MechaSwitchRepository_UpdateSwitch_Method_Data))]
        public void Test_MechaSwitchRepository_UpdateSwitch_Method(MechaSwitch sourceSwitch, int id)
        {
            bool switchUpdated = UnitOfWork.SwitchesRepository.UpdateSwitch(sourceSwitch, id);

            Assert.True(switchUpdated, $"Something went wrong in dbContext during update of {sourceSwitch.FullName} with id {sourceSwitch.Id}");
        }


    }

}
