using System.ComponentModel.DataAnnotations;

namespace ApiProject.Models
{
    public class Manufacturer
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string CountryName { get; set; }

    }
}