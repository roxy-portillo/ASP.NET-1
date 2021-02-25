using CRUD_Alumnos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUD_Alumnos.Controllers
{
    public class MateriaController : Controller
    {
        // GET: Materia
        public ActionResult Index()
        {
            AlumnosContext db = new AlumnosContext(); 
            return View(db.Materias.ToList());
        }

        // GET: Materia/Create
        [HttpGet]
        public ActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Agregar(Materias m)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                //To open and close conection
                using (var db = new AlumnosContext())
                {
                    db.Materias.Add(m);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al registrar materia " + ex.Message);
                return View();
            }  
            }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            try
            {
                using (var db = new AlumnosContext())
                {
                    Materias ma = db.Materias.Find(id);
                    return View(ma);
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Materias m)
        {
            try
            {
                using (var db = new AlumnosContext())
                {
                   Materias ma = db.Materias.Find(m.Id);
                    ma.Nombre = m.Nombre;
                    ma.Id_docente = m.Id_docente;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public ActionResult Detalles(int id)
        {
            using (var db = new AlumnosContext())
            {
               Materias ma = db.Materias.Find(id);
                return View(ma);
            }

        }

        public ActionResult Eliminar(int id)
        {
            using (var db = new AlumnosContext())
            {
                Materias ma = db.Materias.Find(id);
                db.Materias.Remove(ma);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}
