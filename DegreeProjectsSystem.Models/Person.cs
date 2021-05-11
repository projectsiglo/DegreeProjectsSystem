using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DegreeProjectsSystem.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar un Número de Documento de Identificación")]
        [MaxLength(20, ErrorMessage = "El campo {0} debe tener menos de {1} caracteres")]
        [Display(Name = "Número de Identificación")]
        public string IdentificationNumber { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar un Tipo de Documento de Identidad")]
        public int IdentityDocumentTypeId { get; set; }

        [Display(Name = "Tipo de Documento")]
        //Foreign key
        [ForeignKey("IdentityDocumentTypeId")]
        public IdentityDocumentType IdentityDocumentType { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar  nombre(s) de la Persona")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener menos de {1} caracteres")]
        [Display(Name = "Nombre(s)")]
        public string Names { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar  apellido(s) de la Persona")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener menos de {1} caracteres")]
        [Display(Name = "Apellido(s)")]
        public string Surnames { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar el Género")]
        public int GenderId { get; set; }

        [Display(Name = "Género")]
        //Foreign key
        [ForeignKey("GenderId")]
        public Gender Gender { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar El Departamento de Residencia")]
        public int DepartmentId { get; set; }

        [Display(Name = "Departamento")]
        //Foreign key
        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe seleccionar La Ciudad de Residencia")]
        public int CityId { get; set; }

        [Display(Name = "Ciudad")]
        //Foreign key
        [ForeignKey("CityId")]
        public City City { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar La Dirección")]
        [MaxLength(300, ErrorMessage = "El campo {0} debe tener menos de {1} caracteres")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Dirección")]
        public string Address { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar un Número Telefónico")]
        [MaxLength(10, ErrorMessage = "El campo {0} debe tener menos de {1} caracteres")]
        [Display(Name = "Teléfono")]
        public string Phone { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar un Número de Celular")]
        [MaxLength(10, ErrorMessage = "El campo {0} debe tener menos de {1} caracteres")]
        [Display(Name = "Celular")]
        public string Mobile { get; set; }

        [Display(Name = "Estado")]
        public bool Active { get; set; }
    }
}
