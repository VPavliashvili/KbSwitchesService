using System.Collections;
using System.Collections.Generic;
using ApiProject.Models;
using ApiProject.Services.Repositories;
using ApiProject.Services.UnitsOfWork;
using ApiProject.SortFilteringSearchAndPaging;
using Xunit;

namespace ApiProject.Test.Switches.RepositoryPart
{
    public class FilteringTests
    {
        private readonly IMechaSwitchRepository _repository;

        public static IEnumerable<object[]> ForActuationForce_Data
            => FilteringTestsData.ForActuationForce_Data;
        public static IEnumerable<object[]> ForManufacturer_Data
            => FilteringTestsData.ForManufacturer_Data;
        public static IEnumerable<object[]> ForSwitchType_Data
            => FilteringTestsData.ForSwitchType_Data;
        public static IEnumerable<object[]> ForBottomOutForce_Data
            => FilteringTestsData.ForBottomOutForce_Data;
        public static IEnumerable<object[]> ForActuationDistance_Data
            => FilteringTestsData.ForActuationDistance_Data;
        public static IEnumerable<object[]> ForBottomOutDistance_Data
            => FilteringTestsData.ForBottomOutDistance_Data;
        public static IEnumerable<object[]> ForLifespan_Data
            => FilteringTestsData.ForLifespan_Data;




        public FilteringTests()
        {
            _repository = new MockUnitOfWork().SwitchesRepository;
        }

        [Theory]
        [MemberData(nameof(ForActuationForce_Data))]
        [MemberData(nameof(ForManufacturer_Data))]
        [MemberData(nameof(ForSwitchType_Data))]
        [MemberData(nameof(ForBottomOutForce_Data))]
        [MemberData(nameof(ForActuationDistance_Data))]
        [MemberData(nameof(ForBottomOutDistance_Data))]
        [MemberData(nameof(ForLifespan_Data))]
        public void ShouldReturnTrue_WhenPassingFollowingArguments_ForActuationForceFiltering(int expectedResultNum, SwitchesParameters parameters)
        {
            PagedList<MechaSwitch> switches = _repository.GetSwitches(parameters);

            Assert.True(expectedResultNum == switches.Count,
                $"Expected {expectedResultNum} and {nameof(switches)} result returned {switches.Count} records");
        }

    }

}