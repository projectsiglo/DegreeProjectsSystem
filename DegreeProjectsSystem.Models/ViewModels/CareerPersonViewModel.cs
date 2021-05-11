using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace DegreeProjectsSystem.Models.ViewModels
{
    public class CareerPersonViewModel
    {
        public CareerPerson CareerPerson { get; set; }
        public IEnumerable<SelectListItem> CareerList { get; set; }
        public IEnumerable<SelectListItem> PersonList { get; set; }
    }
}
