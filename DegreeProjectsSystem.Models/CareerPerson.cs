using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DegreeProjectsSystem.Models
{
    public class CareerPerson
    {
        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar un Programa")]
        public int CareerId { get; set; }

        [Display(Name = "Programa")]
        //Foreign key
        [ForeignKey("CareerId")]
        public Career Career { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar una Persona que Pertenezca al Programa")]
        public int PersonId { get; set; }

        [Display(Name = "Nombres y Apellidos")]
        //Foreign key
        [ForeignKey("PersonId")]
        public Person Person { get; set; }

        [Display(Name = "Estado")]
        public bool Active { get; set; }
    }
}
