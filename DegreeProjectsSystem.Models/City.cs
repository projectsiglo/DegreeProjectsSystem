using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DegreeProjectsSystem.Models
{
    public class City
    {
        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar un nombre de Ciudad")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener menos de {1} caracteres")]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar el Departamento al que pertenece")]
        public int DepartmentId { get; set; }

        //Foreign key
        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }


        [Display(Name = "Estado")]
        public bool Active { get; set; }
    }
}