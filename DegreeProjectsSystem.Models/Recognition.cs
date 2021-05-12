using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DegreeProjectsSystem.Models
{
    public class Recognition
    {
        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar la descripción de un Reconocimiento")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener menos de {1} caracteres")]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar un Nível Educativo")]
        public int EducationLevelId { get; set; }

        //Foreign key
        [ForeignKey("EducationLevelId")]
        public EducationLevel EducationLevel { get; set; }

        [Required]
        [Display(Name = "Estado")]
        public bool Active { get; set; }
    }
}