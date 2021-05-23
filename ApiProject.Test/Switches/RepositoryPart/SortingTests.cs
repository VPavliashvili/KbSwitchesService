using ApiProject.Models;
using ApiProject.Services.Repositories;
using ApiProject.Services.UnitsOfWork;
using ApiProject.SortFilteringSearchAndPaging;
using ApiProject;
using Xunit;

namespace ApiProject.Test.Switches.RepositoryPart
{
    public class SortingTests
    {

        private readonly IMechaSwitchRepository _repository;

        public SortingTests()
        {
            _repository = new MockUnitOfWork().SwitchesRepository;
        }

        [Fact]
        public void FirstSwitchShouldBeCherryBlue_WhenSortingBy_ActuationForceDesc_And_BottomOutForceAsc()
        {
            string queryParameter = "actuationforce desc,bottomOutForce asc";
            SwitchesParameters parameters = new()
            {
                OrderBy = queryParameter
            };

            MechaSwitch expectedOutput = new()
            {
                Id = 4,
                Manufacturer = new() { Name = "Cherry" },
                FullName = "Cherry Blue",
                Type = SwitchType.Clicky,
                ActuationForce = 50,
                BottomOutForce = 64,
                ActuationDistance = 2.2f,
                BottomOutDistance = 4.0f,
                Lifespan = 100_000_000
            };

            PagedList<MechaSwitch> switches = _repository.GetSwitches(parameters);
            MechaSwitch firstResult = switches[0];

            Assert.True(expectedOutput.Manufacturer.Name == firstResult.Manufacturer.Name
                            && expectedOutput.FullName == firstResult.FullName,
                        $"expected switch was {expectedOutput.FullName} produces by {expectedOutput.Manufacturer.Name}, "
                        + $"but the result is {firstResult.FullName} produced by {firstResult.Manufacturer.Name}");
        }

    }
}