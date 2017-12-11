using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Portafolio.ViewClasses.ProgramasFinalizados
{
    public class VProgramasPostulados
    {
        public int IdPrograma { get; set; }
        public string NombrePrograma { get; set; }
        public DateTime FechaInicio { get; set; }
        public string NombreEncargado { get; set; }
        public string Institucion { get; set; }
        public string NombreCiudad { get; set; }
        public string NombrePais { get; set; }
        
    }
}
