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

namespace ApiProject.Test
{
    public class MechaSwitchRepositoryTests
    {

        private readonly ITestOutputHelper _output;

        public MechaSwitchRepositoryTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Test_MockDbContext_Concrete_Implementation()
        {
            MockDbContext mockDbContext = new();

            IEnumerable<MechaSwitch> switches = mockDbContext.Switches;

            Assert.NotNull(switches);
        }

        [Fact]
        public void Test_MockUnitOfWork_Concrete_Implementation()
        {
            MockUnitOfWork mockUnitOfWork = new(new());

            IMechaSwitchRepository mechaSwitchRepository = mockUnitOfWork.SwitchesRepository;

            Assert.NotNull(mechaSwitchRepository);
        }

        [Theory]
        [ClassData(typeof(Test_ISwitchesRepository_Interface_Data))]
        public void Test_ISwitchesRepository_Interface(IMechaSwitchRepository mechaSwitchRepository)
        {
            Assert.NotNull(mechaSwitchRepository);
        }

        [Theory]
        [ClassData(typeof(IUnitOfWork_Interface_Data))]
        public void Test_IUnitOfWork_Interface(IUnitOfWork unitOfWork)
        {
            Assert.NotNull(unitOfWork);
        }

        [Theory]
        [ClassData(typeof(IUnitOfWork_Interface_Data))]
        public void Test_MechaSwitchRepository_GetSwitches_Method(IUnitOfWork unitOfWork)
        {
            IEnumerable<MechaSwitch> allSwitches = unitOfWork.SwitchesRepository.GetSwitches();

            Assert.NotNull(allSwitches);
        }

        private static void TestRepositoryAndUnitOfWorkPatters()
        {
            IUnitOfWork unitOfWork = new MockUnitOfWork(new Services.Contexts.MockDbContext());

            List<MechaSwitch> allSwitches = unitOfWork.SwitchesRepository.GetSwitches().ToList();
            // allSwitches.ForEach(@switch => @switch.PrintInfo());

            MechaSwitch secondSwitch = unitOfWork.SwitchesRepository.GetSwitch(2);
            // secondSwitch.PrintInfo();

            // int id = 4;
            // if (unitOfWork.Switches.SwtichExists(id))
            // {
            //     unitOfWork.Switches.GetSwitch(id).PrintInfo();
            // }
            // else
            // {
            //     Console.WriteLine($"Record on id {id} does not exist");
            // }
        }

        private class Test_ISwitchesRepository_Interface_Data : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new MockMechaSwitchRepository(new MockDbContext()) };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        private class IUnitOfWork_Interface_Data : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new MockUnitOfWork(new MockDbContext()) };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

    }


}