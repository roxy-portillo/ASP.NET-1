using CRUD_Alumnos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUD_Alumnos.Controllers
{
    public class AlumnosController : Controller
    {
        // GET: Alumnos
        public ActionResult Index()
        {
            try
            {
                using (var db = new AlumnosContext())
                {
                    //List<Alumno> lista = db.Alumno.Where(a => a.Edad > 18).ToList();
                    return View(db.Alumno.ToList());
                }
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        [HttpGet]
        public ActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Agregar(Alumno a)
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
                    a.FechaRegistro = DateTime.Now;
                    db.Alumno.Add(a);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
             ModelState.AddModelError("", "Error al registrar alumno " + ex.Message);
                return View();
            }
            
           
        }

        public ActionResult Agregar2()
        {
            return View();
        }

        public ActionResult ListaCiudades()
        {
            using (var db = new AlumnosContext())
            {             
                return PartialView(db.Ciudad.ToList());
            }
        }


        [HttpGet]
        public ActionResult Editar(int id)
        {
            try
            {
                using (var db = new AlumnosContext())
                {
                    Alumno al = db.Alumno.Find(id);
                    return View(al);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Alumno a)
        {
            try
            {
                using (var db = new AlumnosContext())
                {
                    Alumno al = db.Alumno.Find(a.Id);
                    al.Nombres = a.Nombres;
                    al.Apellidos = a.Apellidos;
                    al.Edad = a.Edad;
                    al.Sexo = a.Sexo;

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
                Alumno al = db.Alumno.Find(id);
                return View(al);
            }

        }

        public ActionResult Eliminar(int id)
        {
            using (var db = new AlumnosContext())
            {
                Alumno al = db.Alumno.Find(id);
                db.Alumno.Remove(al);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public static string NombreCiudad(int CodCiudad)
        {
            using (var db = new AlumnosContext())
            {
                return db.Ciudad.Find(CodCiudad).Nombre;
            }
        }

    }
}