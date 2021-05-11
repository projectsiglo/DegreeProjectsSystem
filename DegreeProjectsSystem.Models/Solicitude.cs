using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DegreeProjectsSystem.Models
{
    public class Solicitude
    {
        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar el título del trabajo de grado")]
        [MaxLength(200, ErrorMessage = "El campo {0} debe tener menos de {1} caracteres")]
        [Display(Name = "Titulo")]
        public string TitleDegreeWork { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar el número de acta")]
        [MaxLength(20, ErrorMessage = "El campo {0} debe tener menos de {1} caracteres")]
        [Display(Name = "Acta Número")]
        public string ActNumber { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar la fecha del acta")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date, ErrorMessage = "Fecha inválida")]
        [Display(Name = "Fecha Acta")]
        public DateTime ActDate { get; set; }
        
        [Required]
        [Display(Name = "Cambio Modalidad")]
        public bool ModalityChange { get; set; }

        [MaxLength(200, ErrorMessage = "El campo {0} debe tener menos de {1} caracteres")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Observaciones")]
        public string Observations { get; set; }
        
        [Required]
        [Display(Name = "Estado")]
        public bool Active { get; set; }
    }
}
