using System.ComponentModel.DataAnnotations;

namespace DegreeProjectsSystem.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener menos de {1} caracteres")]
        [Display(Name ="Nombre")]
        public string Name { get; set; }
    }
}
