using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DegreeProjectsSystem.Models
{
    public class Career
    {
        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar el nombre de un Programa")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener menos de {1} caracteres")]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        public int NumberCredits { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar un Tipo de Programa")]
        public int ProgramTypeId { get; set; }

        //Foreign key
        [ForeignKey("ProgramTypeId")]
        public ProgramType ProgramType { get; set; }

        [Required]
        [Display(Name = "Estado")]
        public bool Active { get; set; }
    }
}