using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DegreeProjectsSystem.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace DegreeProjectsSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DepartmentController : Controller
    {
        private readonly IUnitWork _unitWork;

        public DepartmentController(IUnitWork unitWork)
        {
            _unitWork = unitWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region API
        [HttpGet]
        public IActionResult GetAllDepartments() 
        {
            var departments = _unitWork.Department.GetAll();
            return Json(new {  data = departments });
        }
        #endregion
    }
}
