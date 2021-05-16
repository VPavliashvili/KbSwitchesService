using System.Collections.Generic;
using ApiProject.Models;

namespace ApiProject.Test.Switches.ControllerPart
{
    internal static class Data
    {
        public static IEnumerable<object[]> CreateSwitch_ShouldReturn_UnProcessableEntity_IfObjectIsDuplicating_Data
        {
            get
            {
                yield return new object[]
                {
                    //this object is already defined in mockdbcontext with same manufacturer and fullname
                    new MechaSwitch()
                    {
                        Manufacturer = "Razer",
                        FullName = "Razer Green",
                        Type = SwitchType.Clicky,
                        ActuationForce = 50,
                        BottomOutForce = 65,
                        ActuationDistance = 1.9f,
                        BottomOutDistance = 4.0f,
                        Lifespan = 80_000_000
                    }
                };
            }
        }

        public static IEnumerable<object[]> CreateSwitch_ShouldReturn_CreatedAtAction_IfSuccessful_Data
        {
            get
            {
                yield return new object[]
                {
                    //this object is not defined in mockdbcontext and is unique with its manufacturer and fullname
                    new MechaSwitch()
                    {
                        Id = 4,
                        Manufacturer = "Gateron",
                        FullName = "Gateron Blue",
                        Type = SwitchType.Clicky,
                        ActuationForce = 50,
                        BottomOutForce = 65,
                        ActuationDistance = 1.9f,
                        BottomOutDistance = 4.0f,
                        Lifespan = 80_000_000
                    }
                };
            }
        }

        public static IEnumerable<object[]> UpdateSwitch_ShouldReturn_NoContent_WhenSuccessful_Data
        {
            get
            {
                yield return new object[]
                {
                    new MechaSwitch()
                    {
                        Id = 2,
                        Manufacturer = "Kailh",
                        FullName = "Box Jade",
                        Type = SwitchType.Clicky,
                        ActuationForce = 50,
                        BottomOutForce = 75,
                        ActuationDistance = 1.8f,
                        BottomOutDistance = 3.6f,
                        Lifespan = 60_000_000
                    },
                    2//id
                };
            }
        }

    }
}