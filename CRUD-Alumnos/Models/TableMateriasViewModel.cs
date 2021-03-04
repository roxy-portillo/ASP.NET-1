using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUD_Alumnos.Models
{
    public class TableMateriasViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Id_docente { get; set; }
       public virtual Docente Docente { get; set; }

        public string NombreMateria{ get; set; }
    }
}