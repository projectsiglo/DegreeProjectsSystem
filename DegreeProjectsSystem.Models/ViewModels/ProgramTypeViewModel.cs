using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace DegreeProjectsSystem.Models.ViewModels
{
    public class ProgramTypeViewModel
    {
        public ProgramType ProgramType { get; set; }
        public IEnumerable<SelectListItem> EducationLevelList { get; set; }
    }
}
