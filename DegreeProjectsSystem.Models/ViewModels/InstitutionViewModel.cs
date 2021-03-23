using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace DegreeProjectsSystem.Models.ViewModels
{
    public class InstitutionViewModel
    {
        public Institution Institution { get; set; }
        public IEnumerable<SelectListItem> InstitutionTypeList { get; set; }
    }
}
