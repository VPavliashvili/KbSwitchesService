using System.Collections.Specialized;
using System;
using ApiProject.Models;
using ApiProject.Dtos;
using ApiProject.SortFilteringSearchAndPaging;

namespace ApiProject
{
    internal static class Utils
    {
        public static void PrintInfo(this MechaSwitch @switch)
        {
            Console.WriteLine(@switch.GetInfo());
        }

        public static string GetInfo(this MechaSwitch @switch)
            => $"Manufacturer: {@switch.Manufacturer}\n"
                + $"Name: {@switch.FullName}\n"
                + $"Type: {@switch.Type}\n"
                + $"Actuation Force: {@switch.ActuationForce} gram\n"
                + $"Actuation Distance: {@switch.ActuationDistance}mm\n"
                + $"BottomOut Force: {@switch.BottomOutForce} gram\n"
                + $"BottomOut Distance: {@switch.BottomOutDistance}mm\n"
                + $"Lifespan: {@switch.Lifespan} keystroke\n";

        public static SwitchDto ToDto(this MechaSwitch @switch)
            => new SwitchDto()
            {
                Id = @switch.Id,
                Manufacturer = @switch.Manufacturer.Name,
                FullName = @switch.FullName,
                SwitchType = @switch.Type.ToString(),
                ActuationForce = @switch.ActuationForce,
                BottomOutForce = @switch.BottomOutForce,
                ActuationDistance = @switch.ActuationDistance,
                BottomOutDistance = @switch.BottomOutDistance,
                Lifespan = @switch.Lifespan
            };

        public static bool IsSameSwitch(this MechaSwitch @switch, MechaSwitch second)
            // => @switch.Manufacturer.Equals(second.Manufacturer)
            => @switch.Manufacturer.IsSameManufacturer(second.Manufacturer)
            && @switch.FullName == second.FullName;

        public static bool IsSameManufacturer(this Manufacturer manufacturer, Manufacturer second)
            => manufacturer.Name.EqualsIgnoreCase(second.Name);

        public static void RemapValuesFromSource(this MechaSwitch @switch, MechaSwitch source)
        {
            @switch.Id = source.Id;
            @switch.Manufacturer = source.Manufacturer;
            @switch.FullName = source.FullName;
            @switch.Type = source.Type;
            @switch.ActuationForce = source.ActuationForce;
            @switch.BottomOutForce = source.BottomOutForce;
            @switch.ActuationDistance = source.ActuationDistance;
            @switch.BottomOutDistance = source.BottomOutDistance;
            @switch.Lifespan = source.Lifespan;
        }

        public static void RemapValuesFromSource(this Manufacturer manufacturer, Manufacturer source)
        {
            manufacturer.Id = source.Id;
            manufacturer.Name = source.Name;
        }

        public static bool EqualsIgnoreCase(this string target, string source)
            => target.ToUpperInvariant() == source.ToUpperInvariant();

        public static Func<MechaSwitch, bool> GetSwitchesFilterExpression(SwitchesParameters parameters)
            => swt => swt.ActuationForce >= parameters.MinActuationForce
                && swt.ActuationForce <= parameters.MaxActuationForce
                && swt.BottomOutForce >= parameters.MinBottomOutForce
                && swt.BottomOutForce <= parameters.MaxBottomOutForce
                && swt.ActuationDistance >= parameters.MinActuationDistance
                && swt.ActuationDistance <= parameters.MaxActuationDistance
                && swt.BottomOutDistance >= parameters.MinBottomOutDistance
                && swt.BottomOutDistance <= parameters.MaxBottomOutDistance
                && swt.Lifespan >= parameters.MinLifespan
                && swt.Lifespan <= parameters.MaxLifespan
                && (parameters.IsFilteringByManufacturer ? swt.Manufacturer.Name.EqualsIgnoreCase(parameters.ManufacturerName) : true)
                && (parameters.IsFilteringBySwitchType ? swt.Type == parameters.SwitchType : true);

    }

}
