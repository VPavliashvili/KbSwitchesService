using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiProject.Models
{
    public class MechaSwitch
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Manufacturer { get; set; }
        [Required]
        public string FullName { get; set; }

        [Required]
        public SwitchType Type { get; set; }
        [Required]
        public int ActuationForce { get; set; }
        [Required]
        public int BottomOutForce { get; set; }
        [Required]
        public float ActuationDistance { get; set; }
        [Required]
        public float BottomOutDistance { get; set; }
        [Required]
        public int Lifespan { get; set; }
    }

}