using ApiProject.Models;
using ApiProject.Services.Repositories;
using ApiProject.Services.UnitsOfWork;
using ApiProject.SortFilteringSearchAndPaging;
using Xunit;

namespace ApiProject.Test.Switches.RepositoryPart
{
    public class SearchingTests
    {

        private readonly IMechaSwitchRepository _repository;

        public SearchingTests()
        {
            _repository = new MockUnitOfWork().SwitchesRepository;
        }

        [Theory]
        [InlineData("Razer", 3)]
        [InlineData("Cherry", 3)]
        [InlineData("Blue", 1)]
        [InlineData("Green", 1)]
        public void SearchWithFollowingTerm_ShouldReturnFollowingNumberOfResult(string searchTerm, int expectedResultNum)
        {
            SwitchesParameters parameters = new()
            {
                Name = searchTerm
            };
            PagedList<MechaSwitch> switches = _repository.GetSwitches(parameters);

            Assert.True(switches.Count == expectedResultNum);
        }

    }
}