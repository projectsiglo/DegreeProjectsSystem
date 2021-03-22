using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace DegreeProjectsSystem.Models.ViewModels
{
    public class DepartmentFacultyViewModel
    {
        public DepartmentFaculty DepartmentFaculty { get; set; }
        public IEnumerable<SelectListItem> FacultyList { get; set; }
    }
}
