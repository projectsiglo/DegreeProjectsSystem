using AspNetCoreHero.ToastNotification.Abstractions;
using DegreeProjectsSystem.DataAccess.Repository.IRepository;
using DegreeProjectsSystem.Models;
using DegreeProjectsSystem.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DegreeProjectsSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SolicitudeController : Controller
    {
        private readonly IUnitWork _unitWork;
        private readonly INotyfService _notyfService;

        public SolicitudeController(IUnitWork unitWork, INotyfService notyfService)
        {
            _unitWork = unitWork;
            _notyfService = notyfService;
        }
        enum Action
        {
            Create,
            Update,
            None
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult InsertOrUpdateSolicitude(int? id)
        {
            Solicitude solicitude = new Solicitude();
            
            if (id == null)
            {
                solicitude.ActDate = DateTime.Now;
                solicitude.ModalityChange = false;
                solicitude.Active = true;
                // Crea un nuevo registro
                return View(solicitude);
            }

            // Actualiza el registro
            solicitude= _unitWork.Solicitude.Get(id.GetValueOrDefault());
            if (solicitude == null)
            {
                return NotFound();
            }
            return View(solicitude);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult InsertOrUpdateSolicitude(Solicitude solicitude)
        {

            if (ModelState.IsValid)
            {
                Action action = Action.None;
                solicitude.ActDate = DateTime.Parse(Convert.ToString(Request.Form["ActDate"]));

                if (solicitude.Id == 0)
                {
                    action = Action.Create;
                    _unitWork.Solicitude.Add(solicitude);
                }
                else
                {
                    action = Action.Update;
                    _unitWork.Solicitude.Update(solicitude);
                }

                try
                {
                    _unitWork.Save();

                    if (action == Action.Create)
                    {
                        _notyfService.Success("Solicitud trabajo de grado creada correctamente.");
                    }
                    if (action == Action.Update)
                    {
                        _notyfService.Success("Solicitud trabajo de grado actualizada correctamente.");
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
           
            return View(solicitude);
        }

        #region API
        [HttpGet]
        public IActionResult GetAllSolicitudes()
        {
            var solicitudes = _unitWork.Solicitude.GetAll();
            return Json(new { data = solicitudes });
        }

        //Details Institution
        public IActionResult DetailInstitution(int? id)
        {   
            var solicitude = _unitWork.Solicitude.Get(id.GetValueOrDefault());
            
            if (solicitude == null)
            {
                return NotFound();
            }

            return View(solicitude);
        }

        //Details Solicitude
        public IActionResult DetailSolicitude(int? id)
        {
            Solicitude solicitude = new Solicitude();
                
            solicitude = _unitWork.Solicitude.Get(id.GetValueOrDefault());

            if (solicitude == null)
            {
                return NotFound();
            }

            return View(solicitude);
        }


        //Eliminación de registro lógica
        [HttpPost]
        public IActionResult DeleteSolicitude(int id)
        {
            // Actualiza el registro
            var solicitudeDb = _unitWork.Solicitude.Get(id);

            if (solicitudeDb == null)
            {
                return Json(new { succes = false, message = "!!Error al borrar solicitud!!." });
            }

            solicitudeDb.Active = false;
            _unitWork.Solicitude.Update(solicitudeDb);
            _unitWork.Save();

            return Json(new { succes = true, message = "Solicitud borrada exitosamente." });

        }

        #endregion
    }
}