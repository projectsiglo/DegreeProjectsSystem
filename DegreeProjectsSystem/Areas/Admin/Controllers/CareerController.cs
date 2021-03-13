using DegreeProjectsSystem.DataAccess.Repository.IRepository;
using DegreeProjectsSystem.Models;
using DegreeProjectsSystem.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace DegreeProjectsSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CareerController : Controller
    {
        private readonly IUnitWork _unitWork;

        public CareerController(IUnitWork unitWork)
        {
            _unitWork = unitWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult InsertOrUpdateCareer(int? id)
        {
            CareerViewModel careerViewModel = new CareerViewModel()
            {
                Career= new Career(),
                ProgramTypeList = _unitWork.ProgramType.GetAll().Select(pt => new SelectListItem
                {
                    Text = pt.Name,
                    Value = pt.Id.ToString()
                })
            };

            if (id == null)
            {
                careerViewModel.Career.Active = true;
                // Crea un nuevo registro
                return View(careerViewModel);
            }

            // Actualiza el registro
            careerViewModel.Career = _unitWork.Career.Get(id.GetValueOrDefault());
            if (careerViewModel == null)
            {
                return NotFound();
            }
            return View(careerViewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult InsertOrUpdateCareer(Career career)
        {
            if (ModelState.IsValid)
            {
                if (career.Id == 0)
                {
                    _unitWork.Career.Add(career);
                }
                else
                {
                    _unitWork.Career.Update(career);
                }
                _unitWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(career);
        }

        #region API
        [HttpGet]
        public IActionResult GetAllCareers()
        {
            var careers = _unitWork.Career.GetAll(includeProperties: "ProgramType");
            return Json(new { data = careers });
        }

        //Eliminación de registro lógica
        [HttpPost]
        public IActionResult DeleteCareer(int id)
        {
            Career careerDb = new Career();
            // Actualiza el registro
            careerDb = _unitWork.Career.Get(id);

            if (careerDb == null)
            {
                return Json(new { succes = false, message = "!!Error al borrar el programa!! " });
            }

            careerDb.Active = false;
            _unitWork.Career.Update(careerDb);
            _unitWork.Save();


            return Json(new { succes = true, message = "Programa borrado exitosamente" });

        }


        #endregion
    }
}
