using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DegreeProjectsSystem.Models
{
    public class PersonTypePerson
    {
        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar una Persona")]
        public int PersonId { get; set; }

        [Display(Name = "Nombres y Apellidos")]
        //Foreign key
        [ForeignKey("PersonId")]
        public Person Person { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar un Tipo de Persona")]
        public int TypePersonId { get; set; }

        [Display(Name = "Tipo Persona")]
        //Foreign key
        [ForeignKey("TypePersonId")]
        public TypePerson TypePerson { get; set; }

        [Display(Name = "Estado")]
        public bool Active { get; set; }
    }
}
