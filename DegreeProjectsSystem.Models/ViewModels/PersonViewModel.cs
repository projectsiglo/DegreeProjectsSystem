using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace DegreeProjectsSystem.Models.ViewModels
{
    public class PersonViewModel
    {
        public Person Person { get; set; }
        public IEnumerable<SelectListItem> IdentityDocumentTypeList { get; set; }
        public IEnumerable<SelectListItem> GenderList { get; set; }
        public IEnumerable<SelectListItem> DepartmentList { get; set; }
        public IEnumerable<SelectListItem> CityList { get; set; }
    }
}
