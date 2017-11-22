using DALC.Portafolio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Negocio.Portafolio.ViewClasses.FamiliaPostulantes
{
    public class VFamiliasPostulantes
    {
        public string Nombre { get; set; }
        public string Identificacion { get; set; }
        public long Telefono { get; set; }
        public string Correo { get; set; }
        public string Pais { get; set; }
        public string Ciudad { get; set; }
        public string Estado { get; set; }
                
    }
}
