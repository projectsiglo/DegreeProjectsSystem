using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DegreeProjectsSystem.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Display(Name = "Nombres")]
        public  string Names { get; set; }
        [Display(Name = "Apellidos")]
        [Required]
        public string Surnames { get; set; }
        
        [Required]
        [Display(Name = "Dependencia")]
        public string Dependence { get; set; }
        
        [NotMapped]
        public string Role { get; set; }
    }
}
