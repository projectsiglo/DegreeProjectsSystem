using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace DegreeProjectsSystem.Models.ViewModels
{
    public class CareerViewModel
    {
        public Career Career { get; set; }
        public IEnumerable<SelectListItem> ProgramTypeList { get; set; }
    }
}
