//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CRUD_Alumnos.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Notas
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
    
        public virtual Alumno Alumno { get; set; }
        public virtual Materias Materias { get; set; }
    }
}
