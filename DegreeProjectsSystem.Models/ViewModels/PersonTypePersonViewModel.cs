using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace DegreeProjectsSystem.Models.ViewModels
{
    public class PersonTypePersonViewModel
    {
        public PersonTypePerson PersonTypePerson { get; set; }
        public IEnumerable<SelectListItem> PersonList { get; set; }
        public IEnumerable<SelectListItem> TypePersonList { get; set; }
    }
}
