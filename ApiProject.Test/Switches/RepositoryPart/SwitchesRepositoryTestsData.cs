using System.Collections.Generic;
using ApiProject.Models;

namespace ApiProject.Test.Switches.RepositoryPart
{

    internal static class SwitchesRepositoryTestsData
    {
        public static IEnumerable<object[]> SwitchExistsByObject_Method_UnexistedData
        {
            get
            {
                yield return new object[]
                {
                        new MechaSwitch()
                        {
                            Manufacturer = Helpers.Data.Gateron,
                            FullName = "Gateron Blue",
                            Type = SwitchType.Clicky,
                            ActuationForce = 50,
                            BottomOutForce = 65,
                            ActuationDistance = 1.9f,
                            BottomOutDistance = 4.0f,
                            Lifespan = 80_000_000
                        }
                };
                yield return new object[]
                {
                        new MechaSwitch()
                        {
                            Manufacturer = Helpers.Data.Cherry,
                            FullName = "Cherry Green",
                            Type = SwitchType.Clicky,
                            ActuationForce = 82,
                            BottomOutForce = 87,
                            ActuationDistance = 1.9f,
                            BottomOutDistance = 4.0f,
                            Lifespan = 80_000_000
                        }
                };
            }
        }

        public static IEnumerable<object[]> SwitchExistsByObject_Method_ExistedData
        {
            get
            {
                yield return new object[]
                {
                        new MechaSwitch()
                        {
                            Manufacturer = Helpers.Data.Razer,
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

        public static IEnumerable<object[]> CreateSwitch_Method_Data
        {
            get
            {
                yield return new object[]
                {
                        new MechaSwitch()
                        {
                            Id = 14,
                            Manufacturer = Helpers.Data.Gateron,
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

        public static IEnumerable<object[]> UpdateSwitch_Method_Data
        {
            get
            {
                yield return new object[]
                {
                    new MechaSwitch()
                    {
                        Id = 2,
                        Manufacturer = Helpers.Data.Gateron,
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

        public static IEnumerable<object[]> TargetMethodShouldReturnFalse_WhenMethodMockingIsEnabled_Data
        {
            get
            {
                yield return new object[] { "CreateSwitch", new object[] { GetMockSwitchWithCustomId(1) } };
                yield return new object[] { "CreateSwitch", new object[] { GetMockSwitchWithCustomId(15) } };
                yield return new object[] { "UpdateSwitch", new object[] { GetMockSwitchWithCustomId(1), 1 } };
                yield return new object[] { "UpdateSwitch", new object[] { GetMockSwitchWithCustomId(15), 15 } };
                yield return new object[] { "DeleteSwitch", new object[] { 1 } };
                yield return new object[] { "DeleteSwitch", new object[] { 15 } };
            }
        }

        public static MechaSwitch GetMockSwitchWithCustomId(int id)
            => new MechaSwitch()
            {
                Id = id,
                Manufacturer = Helpers.Data.Kailh,
                FullName = "Box Jade",
                Type = SwitchType.Clicky,
                ActuationForce = 50,
                BottomOutForce = 75,
                ActuationDistance = 1.8f,
                BottomOutDistance = 3.6f,
                Lifespan = 60_000_000
            };

    }


}
