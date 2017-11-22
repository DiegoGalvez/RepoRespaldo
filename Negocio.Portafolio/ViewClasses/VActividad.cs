using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Portafolio.ViewClasses
{
    public class VActividad
    {
        private string _nombreActividad;
        private string _descripcion;

        public VActividad(Actividad actividad)
        {
            this.NombreActividad = actividad.NombreActividad;
            this.Descripcion = actividad.Descripcion;
        }
        
        public string NombreActividad
        {
            get
            {
                return _nombreActividad;
            }

            set
            {
                _nombreActividad = value;
            }
        }

        public string Descripcion
        {
            get
            {
                return _descripcion;
            }

            set
            {
                _descripcion = value;
            }
        }
    }
}
