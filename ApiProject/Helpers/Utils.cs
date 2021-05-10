using System;
using ApiProject.Models;

namespace ApiProject
{
    public static class Utils
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

    }

}
