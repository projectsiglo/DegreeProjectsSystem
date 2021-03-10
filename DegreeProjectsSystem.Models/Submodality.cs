﻿using System.ComponentModel.DataAnnotations;

namespace DegreeProjectsSystem.Models
{
    public class Submodality
    {
        [Key]
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar el nombre de una Modalidad")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener menos de {1} caracteres")]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Display(Name = "Estado")]
        public bool Active { get; set; }
    }
}