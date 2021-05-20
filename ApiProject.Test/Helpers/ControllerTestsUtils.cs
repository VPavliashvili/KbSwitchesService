using System;
using ApiProject.Controllers;
using ApiProject.Services.UnitsOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Xunit;

namespace ApiProject.Test.Helpers
{
    internal static class ControllerTestsUtils
    {
        public static void AssertIsTypeForTwo<TResult, TObjResult>(IActionResult result) where TResult : StatusCodeResult where TObjResult : ObjectResult
        {
            if (result is StatusCodeResult)
                Assert.IsType<TResult>(result);
            else if (result is ObjectResult)
                Assert.IsType<TObjResult>(result);
            else if (result == null)
                throw new NullReferenceException("controlleridan dabrunebuli result aris null, most likely controlleridan vabrunebineb null-s");
            else throw new NotImplementedException("Am momentshi rac vici is aris rom IActionResult-ma mxolod am 2 saxis sheidzleba daabrunos");
        }

        public static void AssertIsNotTypeForTwo<TResult, TObjResult>(IActionResult result) where TResult : StatusCodeResult where TObjResult : ObjectResult
        {
            if (result is StatusCodeResult)
                Assert.IsNotType<TResult>(result);
            else if (result is ObjectResult)
                Assert.IsNotType<TObjResult>(result);
            else if (result == null)
                throw new NullReferenceException("controlleridan dabrunebuli result aris null, most likely controlleridan vabrunebineb null-s");
            else throw new NotImplementedException($"Am momentshi rac vici is aris rom IActionResult-ma mxolod am 2 saxis sheidzleba daabrunos");
        }

        public static void AddSampleErrorToModelState(this Controller controller)
        {
            controller.ModelState.AddModelError("Test Key", "Test error description");
        }

        public static IActionResult AddErrorToModelStateAndGetResult(string methodName)
        {
            int existingSwitchId = 1;
            SwitchesController controller = new SwitchesController(new MockUnitOfWork());
            controller.AddSampleErrorToModelState();
            return (IActionResult)controller.GetType().GetMethod(methodName).Invoke(controller, new object[] { existingSwitchId });
        }

        public static IActionResult AddErrorToModelStateAndGetResult(string methodName, object[] @params)
        {
            SwitchesController controller = new SwitchesController(new MockUnitOfWork());
            controller.AddSampleErrorToModelState();
            return (IActionResult)controller.GetType().GetMethod(methodName).Invoke(controller, @params);
        }

    }
}