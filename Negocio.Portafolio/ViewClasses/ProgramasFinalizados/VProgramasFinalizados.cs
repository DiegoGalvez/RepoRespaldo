using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Portafolio.ViewClasses.ProgramasFinalizados
{
    public class VProgramasFinalizados
    {
        public int IdPrograma { get; set; }
        public string NombrePrograma { get; set; }
        public string NombreCEL { get; set; }
        public string NombreEncargadoCEL { get; set; }
        public string Pais { get; set; }
        public string Ciudad { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaTermino { get; set; }
    }
}
