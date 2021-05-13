using System.Collections.Generic;
using ApiProject.Models;

namespace ApiProject.Test.Switches.RepositoryPart
{

    internal static class MechaSwitchRepositoryTests_Data
    {
        public static IEnumerable<object[]> SwitchExistsByObject_Method_UnexistedData
        {
            get
            {
                yield return new object[]
                {
                        new MechaSwitch()
                        {
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
                yield return new object[]
                {
                        new MechaSwitch()
                        {
                            Manufacturer = "Cherry",
                            FullName = "Cherry Blue",
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

        public static IEnumerable<object[]> SwitchExistsByObject_Method_ExistedData
        {
            get
            {
                yield return new object[]
                {
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

        public static IEnumerable<object[]> CreateSwitch_Method_Data
        {
            get
            {
                yield return new object[]
                {
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

        public static IEnumerable<object[]> UpdateSwitch_Method_Data
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

        public static MechaSwitch GetMockSwitchWithCustomId(int id)
            => new MechaSwitch()
            {
                Id = id,
                Manufacturer = "Kailh",
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
