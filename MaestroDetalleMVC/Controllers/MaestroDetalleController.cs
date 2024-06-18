using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MaestroDetalleMVC.Models;
using MaestroDetalleMVC.Models.ViewModels;

namespace MaestroDetalleMVC.Controllers
{
    public class MaestroDetalleController : Controller
    {
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        public ActionResult PersonaDetalleView()
        {
            PersonaViewModel model = new PersonaViewModel();
            using (MaestroDetalleMVCEntities db = new MaestroDetalleMVCEntities())
            {
                model.personas = db.Personas.Select(p => new MaestroDetalleMVC.Models.ViewModels.Persona
                {
                    Cedula = p.cedula,
                    Nombres = p.nombres,
                    Apellidos = p.apellidos,
                    Direccion = p.direccion
                }).ToList();
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult PersonaDetalle(PersonaViewModel model)
        {
            if (model == null)
            {
                model = new PersonaViewModel();
            }

            if (model.personas == null)
            {
                model.personas = new List<MaestroDetalleMVC.Models.ViewModels.Persona>();
            }

            // Agregar la persona al listado
            var nuevaPersona = new MaestroDetalleMVC.Models.ViewModels.Persona
            {
                Cedula = model.Cedula,
                Nombres = model.Nombres,
                Apellidos = model.Apellidos,
                Direccion = model.Direccion
            };
            model.personas.Add(nuevaPersona);

            try
            {
                using (MaestroDetalleMVCEntities db = new MaestroDetalleMVCEntities())
                {
                    Models.Persona oPersona = new Models.Persona
                    {
                        cedula = nuevaPersona.Cedula,
                        nombres = nuevaPersona.Nombres,
                        apellidos = nuevaPersona.Apellidos,
                        direccion = nuevaPersona.Direccion
                    };
                    db.Personas.Add(oPersona);
                    db.SaveChanges();
                }
                ViewBag.Message = "Registro Insertado";
                return RedirectToAction("PersonaDetalleView");
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error: " + ex.Message;
                return View("PersonaDetalleView", model);
            }
        }

        public ActionResult Edit(int id)
        {
            PersonaViewModel model = new PersonaViewModel();
            using (MaestroDetalleMVCEntities db = new MaestroDetalleMVCEntities())
            {
                var persona = db.Personas.Find(id);
                if (persona != null)
                {
                    model.Cedula = persona.cedula;
                    model.Nombres = persona.nombres;
                    model.Apellidos = persona.apellidos;
                    model.Direccion = persona.direccion;
                }
            }
            return View(model);
        }

        [HttpPost]
        
        public ActionResult Edit(PersonaViewModel model)
        {
            try
            {
                using (MaestroDetalleMVCEntities db = new MaestroDetalleMVCEntities())
                {
                    var persona = db.Personas.Find(model.Cedula);
                    if (persona != null)
                    {
                        persona.nombres = model.Nombres;
                        persona.apellidos = model.Apellidos;
                        persona.direccion = model.Direccion;
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("PersonaDetalleView");
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error: " + ex.Message;
                return View(model);
            }
        }


        [HttpPost]
     
        public ActionResult Delete(int id)
        {
            try
            {
                using (MaestroDetalleMVCEntities db = new MaestroDetalleMVCEntities())
                {
                    var persona = db.Personas.Find(id);
                    if (persona != null)
                    {
                        db.Personas.Remove(persona);
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("PersonaDetalleView");
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error: " + ex.Message;
                return RedirectToAction("PersonaDetalleView");
            }
        }

        
       

    }
}
