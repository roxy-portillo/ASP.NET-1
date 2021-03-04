using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUD_Alumnos.Models
{
    public class TableNotasViewModel
    {
        public int Id { get; set; }
        public int Id_alumno { get; set; }
        public int Id_materia { get; set; }
        public Nullable<decimal> Lab1 { get; set; }
        public Nullable<decimal> Parcial1 { get; set; }
        public Nullable<decimal> Lab2 { get; set; }
        public Nullable<decimal> Parcial2 { get; set; }
        public Nullable<decimal> Lab3 { get; set; }
        public Nullable<decimal> Parcial3 { get; set; }
        public string NombreMateria { get; set; }

        public virtual Alumno Alumno { get; set; }
        public virtual Materias Materias { get; set; }
    }
}