using CRUD_Alumnos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;

namespace CRUD_Alumnos.Controllers
{
    public class ConsultaController : Controller
    {
        public string draw = "";
        public string start = "";
        public string length = "";
        public string sortColumn = "";
        public string sortColumnDir = "";
        public string searchValue = "";
        public int pageSize, skip, recordsTotal;

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

        public static string NombreMateria(int Id_materia)
        {
            using (var db = new AlumnosContext())
            {
                return db.Materias.Find(Id_materia).Nombre;
            }
        }

        [HttpPost]
        public ActionResult Json()
        {
            List<TableNotasViewModel> lst = new List<TableNotasViewModel>();
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();


            //Paging Size (10,20,50,100)    
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;


            using (AlumnosContext db = new AlumnosContext())
            {
                IQueryable<TableNotasViewModel> query = (from n in db.Notas
                                                         select new TableNotasViewModel
                                                         {
                                                             Id_alumno = n.Id_alumno,
                                                             Id_materia = n.Id_materia,
                                                             Lab1 = n.Lab1,
                                                             Parcial1 = n.Parcial1,
                                                             Lab2 = n.Lab2,
                                                             Parcial2 = n.Parcial2,
                                                             Lab3 = n.Lab3,
                                                             Parcial3 = n.Parcial3,
                                                             NombreMateria = n.Materias.Nombre                                                           
                                                         });
                if (searchValue != "")  
                    query = query.Where(n => n.Id_alumno.ToString() == searchValue);

                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                    query = query.OrderBy(sortColumn + " " + sortColumnDir);
                }

                recordsTotal = query.Count();

                lst = query.Skip(skip).Take(pageSize).ToList();
            }

            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = lst });
        }

    }
}