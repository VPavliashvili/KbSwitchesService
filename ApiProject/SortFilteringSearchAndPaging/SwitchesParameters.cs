using System.Text;
using System;
using ApiProject.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ApiProject.SortFilteringSearchAndPaging
{
    public class SwitchesParameters : QueryParameters
    {
        public uint MinActuationForce { get; set; } = 30;
        public uint MaxActuationForce { get; set; } = 200;

        public uint MinBottomOutForce { get; set; } = 30;
        public uint MaxBottomOutForce { get; set; } = 500;

        public float MinActuationDistance { get; set; } = 0.5f;
        public float MaxActuationDistance { get; set; } = 10.0f;

        public float MinBottomOutDistance { get; set; } = 1.0f;
        public float MaxBottomOutDistance { get; set; } = 15.0f;

        public uint MinLifespan { get; set; } = 0;
        public uint MaxLifespan { get; set; } = int.MaxValue;

        public string ManufacturerName { get; set; } = string.Empty;
        public SwitchType? SwitchType { get; set; } = null;

        internal bool IsValidActuationForceRange => MaxActuationForce > MinActuationForce;
        internal bool IsValidBottomOutForceRange => MaxBottomOutForce > MinBottomOutForce;
        internal bool IsValidActuationDistanceRange => MaxActuationDistance > MinActuationDistance;
        internal bool IsValidBottomOutDistanceRange => MaxBottomOutDistance > MinBottomOutDistance;
        internal bool IsValidLifespan => MaxLifespan > MinLifespan;

        internal bool IsFilteringByManufacturer => !string.IsNullOrEmpty(ManufacturerName);
        internal bool IsFilteringBySwitchType => SwitchType != null;

        public bool IsRangesValid()
        {
            return IsValidActuationForceRange && IsValidBottomOutForceRange
                    && IsValidActuationDistanceRange && IsValidBottomOutDistanceRange
                    && IsValidLifespan;
        }

        public void AddErrorMessageToModelState(ModelStateDictionary modelState)
        {
            string errorName = "fileringRangeError";
            if (!IsValidActuationForceRange)
                modelState.AddModelError(errorName, "MaxActuationForce can't be less than MinActuationForce of switch");
            if (!IsValidBottomOutForceRange)
                modelState.AddModelError(errorName, "MaxBottomOutForce can't be less than MinBottomOutForce of switch");
            if (!IsValidActuationDistanceRange)
                modelState.AddModelError(errorName, "MaxActuationDistance can't be less than MinActuationDistance of switch");
            if (!IsValidBottomOutDistanceRange)
                modelState.AddModelError(errorName, "MaxBottomOutDistance can't be less than MinBottomOutDistance of switch");
            if (!IsValidLifespan)
                modelState.AddModelError(errorName, "MaxLifespan can't be less than MinLifespan of switch");
        }
    }
}