using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ApiProject.Models
{
    public class Manufacturer
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        // navigationals
        public ICollection<MechaSwitch> Switches { get; set; }

        public override bool Equals(object obj)
        {
            return ((Manufacturer)obj).Name == Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() * 17;
        }
    }
}