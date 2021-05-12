using System.Collections;
using System.Collections.Generic;
using ApiProject.Controllers;
using ApiProject.Models;
using ApiProject.Services.Contexts;
using ApiProject.Services.UnitsOfWork;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace ApiProject.Test.Switches.ControllerPart
{

    public class SwitchesControllerTests
    {

        private SwitchesController TargetController => new SwitchesController(new MockUnitOfWork());

        public static IEnumerable<object[]> CreateSwitch_ShouldReturn_UnProcessableEntity_IfObjectIsDuplicating_Data
            => SwitchesControllerTests_Data.CreateSwitch_ShouldReturn_UnProcessableEntity_IfObjectIsDuplicating_Data;
        public static IEnumerable<object[]> CreateSwitch_ShouldReturn_CreatedAtAction_IfSuccessful_Data
            => SwitchesControllerTests_Data.CreateSwitch_ShouldReturn_CreatedAtAction_IfSuccessful_Data;

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

        [Fact]
        public void GetSwitches_ShouldNotReturn_BadRequests()
        {
            IActionResult result = TargetController.GetSwitches();

            SwitchesControllerTests_Utils.AssertIsNotTypeForTwo<BadRequestResult, BadRequestObjectResult>(result);
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
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void GetSwitch_ShouldNotReturn_TheseRequestObjects(int id)
        {
            IActionResult result = TargetController.GetSwitch(id);

            SwitchesControllerTests_Utils.AssertIsNotTypeForTwo<BadRequestResult, BadRequestObjectResult>(result);
        }

        [Theory]
        [MemberData(nameof(CreateSwitch_ShouldReturn_UnProcessableEntity_IfObjectIsDuplicating_Data))]
        public void CreateSwitch_ShouldReturn_UnProcessableEntity_IfObjectIsDuplicating(MechaSwitch duplicatingSwitch)
        {
            IActionResult result = TargetController.CreateSwitch(duplicatingSwitch);

            SwitchesControllerTests_Utils.AssertIsTypeForTwo<UnprocessableEntityResult, UnprocessableEntityObjectResult>(result);
        }

        [Theory]
        [MemberData(nameof(CreateSwitch_ShouldReturn_CreatedAtAction_IfSuccessful_Data))]
        public void CreateSwitch_ShouldReturn_CreatedAtAction_IfSuccessful(MechaSwitch switchToCreate)
        {
            IActionResult result = TargetController.CreateSwitch(switchToCreate);

            Assert.IsType<CreatedAtActionResult>(result);
        }

        [Theory]
        [InlineData(null)]
        public void CreateSwitch_ShouldReturnBadRequest_IfPassedObjectIsNull(MechaSwitch switchToCreate)
        {
            IActionResult result = TargetController.CreateSwitch(switchToCreate);

            SwitchesControllerTests_Utils.AssertIsTypeForTwo<BadRequestResult, BadRequestObjectResult>(result);
        }

    }

}
