using System;
using System.Collections;
using System.Collections.Generic;
using ApiProject.Controllers;
using ApiProject.Services.Contexts;
using ApiProject.Services.UnitsOfWork;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace ApiProject.Test
{
    public class SwitchesControllerTester
    {

        public static IEnumerable<object[]> UnwantedObjectReturnTypes_GetAllSwitches
        {
            get
            {
                yield return new object[] { typeof(BadRequestObjectResult) };
                yield return new object[] { typeof(BadRequestResult) };
            }
        }
        public static IEnumerable<object[]> UnwantedObjectReturnTypes_GetSwitch
        {
            get
            {
                yield return new object[] { typeof(BadRequestObjectResult) };
                yield return new object[] { typeof(BadRequestResult) };
            }
        }

        private SwitchesController TargetController => new SwitchesController(new MockUnitOfWork(new()));


        [Fact]
        public void SwitchesController_ShouldBe_Controller()
        {
            Assert.True(TargetController is Controller);
        }

        [Fact]
        public void GetSwitches_ShouldReturn_OkObjectResult()
        {
            IActionResult result = TargetController.GetSwitches();

            Assert.IsType<OkObjectResult>(result);
        }

        [Theory]
        [MemberData(nameof(UnwantedObjectReturnTypes_GetAllSwitches))]
        public void GetSwitches_ShouldNotReturn_TheseRequestObjects(Type unwanted)
        {
            IActionResult result = TargetController.GetSwitches();

            Assert.IsNotType(unwanted, result);
        }

        [Theory]
        [InlineData(4)] // at the time of writing this I have 3 objects in inmemory mock repository
        public void GetSwitch_ShouldReturn_NotFount_WhenWrongId(int id)
        {
            IActionResult result = TargetController.GetSwitch(id);

            Assert.IsType<NotFoundResult>(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)] // at the time of writing this I have 3 objects in inmemory mock repository
        public void GetSwitch_ShouldReturn_OkObjectResult_WhenCorrectId(int id)
        {
            IActionResult result = TargetController.GetSwitch(id);

            Assert.IsType<OkObjectResult>(result);
        }

        [Theory]
        [MemberData(nameof(UnwantedObjectReturnTypes_GetSwitch))]
        public void GetSwitch_ShouldNotReturn_TheseRequestObjects(Type unwanted)
        {
            for (int i = 1; i <= 3; i++)
            {
                int id = i;
                IActionResult result = TargetController.GetSwitch(id);

                Assert.IsNotType(unwanted, result);
            }
        }

    }
}