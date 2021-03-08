using DegreeProjectsSystem.DataAccess.Repository.IRepository;
using DegreeProjectsSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace DegreeProjectsSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class InstitutionTypeController : Controller
    {
        private readonly IUnitWork _unitWork;

        public InstitutionTypeController(IUnitWork unitWork)
        {
            _unitWork = unitWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult InsertOrUpdateInstitutionType(int? id)
        {
            InstitutionType institutionType = new InstitutionType();
            if (id == null)
            {
                // Crea un nuevo registro
                return View(institutionType);
            }
            // Actualiza el registro
            //institutionType = _unitWork
            institutionType = _unitWork.InstitutionType.Get(id.GetValueOrDefault());
            if (institutionType == null)
            {
                return NotFound();
            }
            return View(institutionType);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult InsertOrUpdateInstitutionType(InstitutionType institutionType)
        {
            if (ModelState.IsValid)
            {
                if (institutionType.Id == 0)
                {
                    _unitWork.InstitutionType.Add(institutionType);
                }
                else
                {
                    _unitWork.InstitutionType.Update(institutionType);
                }
                _unitWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(institutionType);
        }

        #region API
        [HttpGet]
        public IActionResult GetAllInstitutionTypes()
        {
            var institutionTypes = _unitWork.InstitutionType.GetAll();
            return Json(new { data = institutionTypes });
        }

        //Eliminación de registro lógica
        [HttpPost]
        public IActionResult DeleteInstitutionType(int id)
        {
            InstitutionType institutionTypeDb = new InstitutionType();
            // Actualiza el registro
            institutionTypeDb = _unitWork.InstitutionType.Get(id);

            if (institutionTypeDb == null)
            {
                return Json(new { succes = false, message = "!!Error al borrar tipo de institución!! " });
            }

            institutionTypeDb.Active = false;
            _unitWork.InstitutionType.Update(institutionTypeDb);
            _unitWork.Save();


            return Json(new { succes = true, message = "Tipo de institución borrado exitosamente" });

        }


        #endregion
    }
}
