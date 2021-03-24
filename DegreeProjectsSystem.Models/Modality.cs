using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DegreeProjectsSystem.Models
{
    public class Modality
    {
        [Key]
        public int Id { get; set; }
       
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresaar un nombre de Modalidad")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener menos de {1} caracteres")]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar un Nível Educativo")]
         public int EducationLevelId { get; set; }

        //Foreign key
        [ForeignKey("EducationLevelId")]
        public EducationLevel EducationLevel { get; set; }


        [Display(Name="Estado")]
        public bool Active { get; set; }
    }
}
