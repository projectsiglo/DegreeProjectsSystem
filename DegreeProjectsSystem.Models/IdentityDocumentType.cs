using System.ComponentModel.DataAnnotations;

namespace DegreeProjectsSystem.Models
{
    public class IdentityDocumentType
    {
        [Key]
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar un tipo de documento de Identidad")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener menos de {1} caracteres")]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Display(Name = "Estado")]
        public bool Active { get; set; }
    }
}
