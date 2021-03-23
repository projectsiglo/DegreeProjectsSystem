using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DegreeProjectsSystem.Models
{
    public class Institution
    {
        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar el nombre de la Institución")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener menos de {1} caracteres")]
        [Display(Name = "Nombre")]
        public string Name { get; set; }
        
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar el nit de la Institución")]
        [MaxLength(15, ErrorMessage = "El campo {0} debe tener menos de {1} caracteres")]
        [Display(Name = "Nit")]
        public string Nit { get; set; }
        
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar el número de teléfono de la Institución")]
        [MaxLength(15, ErrorMessage = "El campo {0} debe tener menos de {1} caracteres")]
        [Display(Name = "Teléfono")]
        public string Phone { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar el correo de la Institución")]
        [EmailAddress(ErrorMessage = "El formato de correo ingresado es inválido")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener menos de {1} caracteres")]
        [Display(Name = "Correo")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar un Tipo de Institución")]
        public int InstitutionTypeId { get; set; }

        //Foreign key
        [ForeignKey("InstitutionTypeId")]
        public InstitutionType InstitutionType { get; set; }

        [MaxLength(200, ErrorMessage = "El campo {0} debe tener menos de {1} caracteres")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Observaciones")]
        public string Observations { get; set; }
        
        [Required]
        [Display(Name = "Estado")]
        public bool Active { get; set; }
    }
}
