using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace DegreeProjectsSystem.Models.ViewModels
{
    public class ModalityViewModel
    {
        public Modality Modality { get; set; }
        public IEnumerable<SelectListItem> EducationLevelList { get; set; }
    }
}
