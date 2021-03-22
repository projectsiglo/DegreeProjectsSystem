using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DegreeProjectsSystem.Models
{
    public class DepartmentFaculty
    {
        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresaar un nombre de Departamento de Facultad")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener menos de {1} caracteres")]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar la Facultad a la que pertenece")]
        public int FacultyId { get; set; }

        //Foreign key
        [ForeignKey("FacultyId")]
        public Faculty Faculty { get; set; }


        [Display(Name = "Estado")]
        public bool Active { get; set; }
    }
}
