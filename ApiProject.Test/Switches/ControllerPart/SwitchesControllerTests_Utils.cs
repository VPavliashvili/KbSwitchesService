using System;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace ApiProject.Test.Switches.ControllerPart
{
    internal static class SwitchesControllerTests_Utils
    {
        public static void AssertIsTypeForTwo<TResult, TObjResult>(IActionResult result) where TResult : StatusCodeResult where TObjResult : ObjectResult
        {
            if (result is StatusCodeResult)
                Assert.IsType<TResult>(result);
            else if (result is ObjectResult)
                Assert.IsType<TObjResult>(result);
            else throw new NotImplementedException("Am momentshi rac vici is aris rom IActionResult-ma mxolod am 2 saxis sheidzleba daabrunos");
        }

        public static void AssertIsNotTypeForTwo<TResult, TObjResult>(IActionResult result) where TResult : StatusCodeResult where TObjResult : ObjectResult
        {
            if (result is StatusCodeResult)
                Assert.IsNotType<TResult>(result);
            else if (result is ObjectResult)
                Assert.IsNotType<TObjResult>(result);
            else throw new NotImplementedException("Am momentshi rac vici is aris rom IActionResult-ma mxolod am 2 saxis sheidzleba daabrunos");
        }
    }
}