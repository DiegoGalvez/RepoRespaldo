using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Portafolio.ViewEntities
{
    public class VNotasProgramaAlumno
    {
        private string _programa;
        private double _nota;

        public string Programa
        {
            get
            {
                return _programa;
            }

            set
            {
                _programa = value;
            }
        }

        public double Nota
        {
            get
            {
                return _nota;
            }

            set
            {
                _nota = value;
            }
        }
    }
}
