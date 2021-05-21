using System.Collections;
using System.Collections.Generic;
using ApiProject.SortFilteringSearchAndPaging;

namespace ApiProject.Test.Switches.RepositoryPart
{
    public static class FilteringTestsData
    {
        public static IEnumerable<object[]> ForActuationForce_Data
        {
            get
            {
                yield return new object[]
                {
                    2,
                    new SwitchesParameters()
                    {
                        MinActuationForce = 50
                    }
                };
                yield return new object[]
                {
                    4,
                    new SwitchesParameters()
                    {
                        MaxActuationForce = 45
                    }
                };
                yield return new object[]
                {
                    6,
                    new SwitchesParameters() {}
                };
            }
        }

        public static IEnumerable<object[]> ForManufacturer_Data
        {
            get
            {
                yield return new object[]
                {
                    3,
                    new SwitchesParameters()
                    {
                        ManufacturerName = "Razer"
                    }
                };
                yield return new object[]
                {
                    3,
                    new SwitchesParameters()
                    {
                        ManufacturerName = "Cherry"
                    }
                };
                yield return new object[]
                {
                    0,
                    new SwitchesParameters()
                    {
                        ManufacturerName = "Gateron"
                    }
                };
            }
        }

        public static IEnumerable<object[]> ForSwitchType_Data
        {
            get
            {
                yield return new object[]
                {
                    2,
                    new SwitchesParameters()
                    {
                        SwitchType = SwitchType.Linear
                    }
                };
                yield return new object[]
                {
                    2,
                    new SwitchesParameters()
                    {
                        SwitchType = SwitchType.Tactile
                    }
                };
                yield return new object[]
                {
                    2,
                    new SwitchesParameters()
                    {
                        SwitchType = SwitchType.Clicky
                    }
                };
            }
        }

        public static IEnumerable<object[]> ForBottomOutForce_Data
        {
            get
            {
                yield return new object[]
                {
                    5,
                    new SwitchesParameters()
                    {
                        MinBottomOutForce = 60
                    }
                };
                yield return new object[]
                {
                    3,
                    new SwitchesParameters()
                    {
                        MaxBottomOutForce = 60
                    }
                };
                yield return new object[]
                {
                    4,
                    new SwitchesParameters()
                    {
                        MinBottomOutForce = 60,
                        MaxBottomOutForce = 65
                    }
                };
            }
        }

        public static IEnumerable<object[]> ForActuationDistance_Data
        {
            get
            {
                yield return new object[]
                {
                    3,
                    new SwitchesParameters()
                    {
                        MinActuationDistance = 2f,
                        MaxActuationDistance = 3f
                    }
                };
            }
        }

        public static IEnumerable<object[]> ForBottomOutDistance_Data
        {
            get
            {
                yield return new object[]
                {
                    1,
                    new SwitchesParameters()
                    {
                        MaxBottomOutDistance = 3.9f,
                        MinBottomOutDistance = 3.0f
                    }
                };
            }
        }

        public static IEnumerable<object[]> ForLifespan_Data
        {
            get
            {
                yield return new object[]
                {
                    3,
                    new SwitchesParameters()
                    {
                        MinLifespan = 100_000_000
                    }
                };
            }
        }
    }

}