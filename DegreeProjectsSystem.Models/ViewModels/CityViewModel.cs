using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace DegreeProjectsSystem.Models.ViewModels
{
    public class CityViewModel
    {
        public City City { get; set; }
        public IEnumerable<SelectListItem> DepartmentList { get; set; }
    }
}
