using System;
using System.Collections;
using System.Collections.Generic;
using ApiProject.Controllers;
using ApiProject.Models;
using ApiProject.Services.Contexts;
using ApiProject.Services.Repositories;
using ApiProject.Services.UnitsOfWork;
using ApiProject.Test.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace ApiProject.Test.Switches.ControllerPart
{

    public class SwitchesControllerTests
    {

        private SwitchesController TargetController => new SwitchesController(new MockUnitOfWork());
        private SwitchesParameters SwitchesParameters => new();

        public static IEnumerable<object[]> CreateSwitch_ShouldReturn_UnProcessableEntity_IfObjectIsDuplicating_Data
            => SwitchesControllerTestsData.CreateSwitch_ShouldReturn_UnProcessableEntity_IfObjectIsDuplicating_Data;
        public static IEnumerable<object[]> CreateSwitch_ShouldReturn_CreatedAtAction_IfSuccessful_Data
            => SwitchesControllerTestsData.CreateSwitch_ShouldReturn_CreatedAtAction_IfSuccessful_Data;
        public static IEnumerable<object[]> UpdateSwitch_ShouldReturn_NoContent_WhenSuccessful_Data
            => SwitchesControllerTestsData.UpdateSwitch_ShouldReturn_NoContent_WhenSuccessful_Data;

        [Fact]
        public void SwitchesController_ShouldBe_Controller()
        {
            Assert.True(TargetController is Controller);
        }

        [Fact]
        public void GetSwitches_ShouldReturn_OkObjectResult()
        {
            SwitchesController controller = TargetController;
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            IActionResult result = TargetController.GetSwitches(SwitchesParameters);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetSwitches_ShouldNotReturn_BadRequests()
        {
            IActionResult result = TargetController.GetSwitches(SwitchesParameters);

            TestingUtils.AssertIsNotTypeForTwo<BadRequestResult, BadRequestObjectResult>(result);
        }

        [Theory]
        [InlineData(41)] // at the time of writing this I have 6 objects in inmemory mock repository
        public void GetSwitch_ShouldReturn_NotFount_WhenWrongId(int id)
        {
            IActionResult result = TargetController.GetSwitch(id);

            Assert.IsType<NotFoundResult>(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)] // at the time of writing this I have 6 objects in inmemory mock repository
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

            TestingUtils.AssertIsNotTypeForTwo<BadRequestResult, BadRequestObjectResult>(result);
        }

        [Theory]
        [MemberData(nameof(CreateSwitch_ShouldReturn_UnProcessableEntity_IfObjectIsDuplicating_Data))]
        public void CreateSwitch_ShouldReturn_UnProcessableEntity_IfObjectIsDuplicating(MechaSwitch duplicatingSwitch)
        {
            IActionResult result = TargetController.CreateSwitch(duplicatingSwitch);

            TestingUtils.AssertIsTypeForTwo<UnprocessableEntityResult, UnprocessableEntityObjectResult>(result);
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

            TestingUtils.AssertIsTypeForTwo<BadRequestResult, BadRequestObjectResult>(result);
        }

        [Fact]
        public void UpdateSwitch_ShouldReturn_BadRequest_WhenModelstateIsNotValid()
        {
            string methodName = nameof(TargetController.UpdateSwitch);
            object[] @params = { new MechaSwitch(), 1 };
            IActionResult result = TestingUtils.AddErrorToModelStateAndGetResult<SwitchesController>(methodName, @params);

            TestingUtils.AssertIsTypeForTwo<BadRequestResult, BadRequestObjectResult>(result);
        }

        [Theory]
        [InlineData(null)]
        public void UpdateSwitch_ShouldReturn_BadRequestCode_WhenPassedSwitchIsNull(MechaSwitch source)
        {
            IActionResult result = TargetController.UpdateSwitch(source, 1);
            int? statusCode = (result as ObjectResult).StatusCode;

            Assert.Equal(StatusCodes.Status400BadRequest, statusCode);
        }

        [Theory]
        [InlineData(41)]
        public void UpdateSwitch_ShouldReturn_NotFound_WhenPassedIdIsMissingInDb(int id)
        {
            IActionResult result = TargetController.UpdateSwitch(new(), id);

            TestingUtils.AssertIsTypeForTwo<NotFoundResult, NotFoundObjectResult>(result);
        }

        [Theory]
        [MemberData(nameof(UpdateSwitch_ShouldReturn_NoContent_WhenSuccessful_Data))]
        public void UpdateSwitch_ShouldReturn_NoContent_WhenSuccessful(MechaSwitch source, int id)
        {
            IActionResult result = TargetController.UpdateSwitch(source, id);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void ShouldReturnBadRequest_WhenModelStateIsInvalid_AndDeletingRecord()
        {
            string methodName = nameof(TargetController.DeleteSwitch);
            IActionResult result = TestingUtils.AddErrorToModelStateAndGetResult<SwitchesController>(methodName);

            TestingUtils.AssertIsTypeForTwo<BadRequestResult, BadRequestObjectResult>(result);
        }

        [Theory]
        [InlineData(15)]
        public void ShouldReturnNotFound_WhenPassedIdDoesNotExistIdDatabase(int id)
        {
            IActionResult result = TargetController.DeleteSwitch(id);

            TestingUtils.AssertIsTypeForTwo<NotFoundResult, NotFoundObjectResult>(result);
        }

        [Fact]
        public void ShouldReturnInternalServerError_WhenDbContextRefusesToDeleteRecord()
        {
            IUnitOfWork unitOfWork = new MockUnitOfWork();
            SwitchesController controller = new(unitOfWork);
            (unitOfWork.SwitchesRepository as MockMechaSwitchRepository).EnableMethodMocking();

            bool mockingActivated = (unitOfWork.SwitchesRepository as MockMechaSwitchRepository).MethodMockingEnabled;
            Assert.True(mockingActivated, "method mocking has not activated");

            IActionResult result = controller.DeleteSwitch(1);
            Assert.NotNull(result);

            int? statusCode = ((StatusCodeResult)result).StatusCode;

            Assert.Equal(StatusCodes.Status500InternalServerError, statusCode);
        }

        [Fact]
        public void ShouldReturnInternalServerError_WhenUnitOfWorkCannotComplete()
        {
            IUnitOfWork unitOfWork = new MockUnitOfWork();
            ((MockUnitOfWork)unitOfWork).EnableMethodMocking();
            SwitchesController controller = new(unitOfWork);

            IActionResult result = controller.DeleteSwitch(1);
            int? statuscode = ((ObjectResult)result).StatusCode;

            Assert.Equal(StatusCodes.Status500InternalServerError, statuscode);
        }

        [Fact]
        public void ShoulReturnNoContentResult_WhenRecordDeleted()
        {
            IActionResult result = TargetController.DeleteSwitch(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Theory]
        [InlineData(10)]
        public void ShouldReturnNotFound_WhenGettingManufacturerOfSwitchWithWrongId(int switchId)
        {
            IActionResult result = TargetController.GetManufacturerOfSwitch(switchId);

            TestingUtils.AssertIsTypeForTwo<NotFoundResult, NotFoundObjectResult>(result);
        }

        [Fact]
        public void ShouldReturnBadRequest_WhenModelstateIsInvalidAndGettingManufacturer()
        {
            string methodName = nameof(TargetController.GetManufacturerOfSwitch);
            IActionResult result = TestingUtils.AddErrorToModelStateAndGetResult<SwitchesController>(methodName);

            TestingUtils.AssertIsTypeForTwo<BadRequestResult, BadRequestObjectResult>(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void ShouldReturnOk_WhenGettingManufacturerOfSwitchSuccessfully(int switchId)
        {
            IActionResult result = TargetController.GetManufacturerOfSwitch(switchId);

            TestingUtils.AssertIsTypeForTwo<OkResult, OkObjectResult>(result);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(27)]
        public void ShouldReturnNotFound_WhenGettingAllSwitchesOfManufacturerWithWrongId(int manufacturerId)
        {
            IActionResult result = TargetController.GetSwitchesOfManufacturer(manufacturerId);

            TestingUtils.AssertIsTypeForTwo<NotFoundResult, NotFoundObjectResult>(result);
        }

        [Fact]
        public void ShouldReturnBadRequest_WhenGettingAllSwitchesOfManufacturerAndModelstateIsInvalid()
        {
            string methodName = nameof(TargetController.GetSwitchesOfManufacturer);
            IActionResult result = TestingUtils.AddErrorToModelStateAndGetResult<SwitchesController>(methodName);

            TestingUtils.AssertIsTypeForTwo<BadRequestResult, BadRequestObjectResult>(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void ShouldReturnOk_WhenGettingAllSwitchesOfManufacturerSuccessfully(int manufacturerId)
        {
            IActionResult result = TargetController.GetSwitchesOfManufacturer(manufacturerId);

            TestingUtils.AssertIsTypeForTwo<OkResult, OkObjectResult>(result);
        }

    }

}
