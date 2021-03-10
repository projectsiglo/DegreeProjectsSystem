using DegreeProjectsSystem.DataAccess.Repository.IRepository;
using DegreeProjectsSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace DegreeProjectsSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubmodalityController : Controller
    {
        private readonly IUnitWork _unitWork;

        public SubmodalityController(IUnitWork unitWork)
        {
            _unitWork = unitWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult InsertOrUpdateSubmodality(int? id)
        {
            Submodality submodality = new Submodality();
            if (id == null)
            {
                submodality.Active = true;
                // Crea un nuevo registro
                return View(submodality);
            }
            // Actualiza el registro
            //institutionType = _unitWork
            submodality = _unitWork.Submodality.Get(id.GetValueOrDefault());
            if (submodality == null)
            {
                return NotFound();
            }
            return View(submodality);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult InsertOrUpdateSubmodality(Submodality submodality)
        {
            if (ModelState.IsValid)
            {
                if (submodality.Id == 0)
                {
                    _unitWork.Submodality.Add(submodality);
                }
                else
                {
                    _unitWork.Submodality.Update(submodality);
                }
                _unitWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(submodality);
        }

        #region API
        [HttpGet]
        public IActionResult GetAllSubmodalities()
        {
            var subbmodalities = _unitWork.Submodality.GetAll();
            return Json(new { data = subbmodalities });
        }

        //Eliminación de registro lógica
        [HttpPost]
        public IActionResult DeleteSubmodality(int id)
        {
            Submodality submodalityDb = new Submodality();
            // Actualiza el registro
            submodalityDb = _unitWork.Submodality.Get(id);

            if (submodalityDb == null)
            {
                return Json(new { succes = false, message = "!!Error al borrar submodalidad!! " });
            }

            submodalityDb.Active = false;
            _unitWork.Submodality.Update(submodalityDb);
            _unitWork.Save();

            return Json(new { succes = true, message = "Submodalidad borrada exitosamente" });

        }


        #endregion
    }
}
