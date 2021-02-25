using CRUD_Alumnos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUD_Alumnos.Controllers
{
    public class NotaController : Controller
    {
        // GET: Nota
        public ActionResult Index()
        {
            AlumnosContext db = new AlumnosContext();
            return View(db.Notas.ToList());
        }

        // GET: Nota/Create
        [HttpGet]
        public ActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Agregar(Notas n)
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
                    db.Notas.Add(n);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al registrar docente " + ex.Message);
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
                    Notas n = db.Notas.Find(id);
                    return View(n);
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Notas n)
        {
            try
            {
                using (var db = new AlumnosContext())
                {
                    Notas no = db.Notas.Find(n.Id);
                    no.Id_alumno = n.Id_alumno;
                    no.Id_materia = n.Id_materia;
                    no.Lab1 = n.Lab1;
                    no.Parcial1 = n.Parcial1;
                    no.Lab2 = n.Lab2;
                    no.Parcial2 = n.Parcial2;
                    no.Lab3 = n.Lab3;
                    no.Parcial3 = n.Parcial3;
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
                Notas no = db.Notas.Find(id);
                return View(no);
            }

        }

        public ActionResult Eliminar(int id)
        {
            using (var db = new AlumnosContext())
            {
               Notas no = db.Notas.Find(id);
                db.Notas.Remove(no);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

    }
}
