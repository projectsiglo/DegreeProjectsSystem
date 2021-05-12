using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace DegreeProjectsSystem.Models.ViewModels
{
    public class RecognitionViewModel
    {
        public Recognition Recognition { get; set; }
        public IEnumerable<SelectListItem> EducationLevelList { get; set; }
    }
}
