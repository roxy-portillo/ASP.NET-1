using CRUD_Alumnos.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using CRUD_Alumnos.Models;

namespace CRUD_Alumnos.Controllers
{
    public class DocenteController : Controller
    {
        // GET: Docente
        public ActionResult Index()
        {
            try
            {

                using (var db = new AlumnosContext())
                {
                    var data = from d in db.Docente
                               join c in db.Ciudad on d.CodCiudad equals c.Id
                               select new DocenteCE()
                               {
                                   Id = d.Id,
                                   Nombres = d.Nombres,
                                   Apellidos = d.Apellidos,
                                   NombreCiudad = c.Nombre              
                               };

                    //List<Alumno> lista = db.Alumno.Where(a => a.Edad > 18).ToList();
                    return View(data.ToList());

                    //return View(db.Database.SqlQuery<AlumnoCE>(sql,
                    // new SqlParameter("@edadAlumno", edad)).ToList());
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
        public ActionResult Agregar(Docente d)
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
                    db.Docente.Add(d);
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
                    Docente doc = db.Docente.Find(id);
                    return View(doc);
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Docente d)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                using (var db = new AlumnosContext())
                {
                    Docente doc = db.Docente.Find(d.Id);
                    doc.Nombres = d.Nombres;
                    doc.Apellidos = d.Apellidos;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public ActionResult DetallesDocente(int id)
        {
            using (var db = new AlumnosContext())
            {
                Docente doc = db.Docente.Find(id);
                return View(doc);
            }

        }

        public ActionResult Eliminar(int id)
        {
            using (var db = new AlumnosContext())
            {
                Docente doc = db.Docente.Find(id);
                db.Docente.Remove(doc);
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