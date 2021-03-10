using DegreeProjectsSystem.DataAccess.Repository.IRepository;
using DegreeProjectsSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace DegreeProjectsSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EducationLevelController : Controller
    {
        private readonly IUnitWork _unitWork;

        public EducationLevelController(IUnitWork unitWork)
        {
            _unitWork = unitWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult InsertOrUpdateEducationLevel(int? id)
        {
            EducationLevel educationLevel = new EducationLevel();
            if (id == null)
            {
                educationLevel.Active = true;
                // Crea un nuevo registro
                return View(educationLevel);
            }
            // Actualiza el registro
            educationLevel = _unitWork.EducationLevel.Get(id.GetValueOrDefault());
            if (educationLevel == null)
            {
                return NotFound();
            }
            return View(educationLevel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult InsertOrUpdateEducationLevel(EducationLevel educationLevel)
        {
            if (ModelState.IsValid)
            {
                if (educationLevel.Id == 0)
                {
                    _unitWork.EducationLevel.Add(educationLevel);
                }
                else
                {
                    _unitWork.EducationLevel.Update(educationLevel);
                }
                _unitWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(educationLevel);
        }

        #region API
        [HttpGet]
        public IActionResult GetAllEducationLevels()
        {
            var educationLevels = _unitWork.EducationLevel.GetAll();
            return Json(new { data = educationLevels });
        }

        //Eliminación de registro lógica
        [HttpPost]
        public IActionResult DeleteEducationLevel(int id)
        {
            EducationLevel educationLevelDb = new EducationLevel();
            // Actualiza el registro
            educationLevelDb = _unitWork.EducationLevel.Get(id);

            if (educationLevelDb == null)
            {
                return Json(new { succes = false, message = "!!Error al borrar nível educativo!! " });
            }

            educationLevelDb.Active = false;
            _unitWork.EducationLevel.Update(educationLevelDb);
            _unitWork.Save();


            return Json(new { succes = true, message = "Nível educativo borrado exitosamente" });

        }


        #endregion
    }
}
