using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Portafolio.ViewClasses.NotasProgramaAlumno
{
    public class VAlumnosPostulantes
    {
        public int Id_Alumnos { get; set; }
        public string Nombre_Alumno { get; set; }
        public string Nombre_Familia { get; set; }
        public Nullable<int> Cupos { get; set; }
        public string Estado { get; set; }
        public string Nombre_Programa { get; set; }
        public DateTime Fecha_Inicio { get; set; }
    }
}
