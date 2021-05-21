using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ApiProject.Controllers;
using ApiProject.Dtos;
using ApiProject.Models;
using ApiProject.Services.Repositories;
using ApiProject.Services.UnitsOfWork;
using ApiProject.SortFilteringSearchAndPaging;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace ApiProject.Test.Switches.RepositoryPart
{
    public class PagingTests
    {

        private readonly IMechaSwitchRepository _repository;

        public PagingTests()
        {
            _repository = new MockUnitOfWork().SwitchesRepository;
        }

        [Fact]
        public void ShouldReturnTwoRecord_WhenPagesizeIsTwo()
        {
            SwitchesParameters switchesParameters = new();
            switchesParameters.PageSize = 2;
            PagedList<MechaSwitch> switches = _repository.GetSwitches(switchesParameters);

            Assert.True(switches.Count() == 2);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public void ShouldReturnNRecords_WhenPageSizeIsN(int pageSize)
        {
            SwitchesParameters switchesParameters = new()
            {
                PageSize = pageSize
            };
            PagedList<MechaSwitch> switches = _repository.GetSwitches(switchesParameters);

            Assert.True(switches.Count() == pageSize);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void ShouldReturnHasNext_IsTrue(int currentPage)
        {
            SwitchesParameters parameters = new()
            {
                PageSize = 2,
                PageNumber = currentPage
            };
            PagedList<MechaSwitch> switches = _repository.GetSwitches(parameters);

            Assert.True(switches.HasNext);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        public void ShouldReturnHasPrevious_IsTrue(int currentPage)
        {
            SwitchesParameters parameters = new()
            {
                PageSize = 2,
                PageNumber = currentPage
            };
            PagedList<MechaSwitch> switches = _repository.GetSwitches(parameters);

            Assert.True(switches.HasPrevious);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(100)]
        public void ShouldReturnTrue_WhenCheckingTotalPagesCount(int pageSize)
        {
            int itemsCount = _repository.RecordsCountInDb;
            SwitchesParameters parameters = new()
            {
                PageSize = pageSize
            };
            PagedList<MechaSwitch> switches = _repository.GetSwitches(parameters);

            Assert.True(switches.TotalPages == (int)Math.Ceiling(itemsCount / (double)pageSize));
        }

    }
}