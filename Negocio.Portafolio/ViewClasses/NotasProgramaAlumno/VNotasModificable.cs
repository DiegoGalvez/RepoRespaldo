using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Portafolio.ViewEntities
{
    public class VNotasModificable
    {
        private int _idNota;
        private double _nota;
        private string _programa;

        public int IdNota
        {
            get { return _idNota; }
            set { _idNota = value; }
        }

        public double Nota
        {
            get { return _nota; }
            set { _nota = value; }
        }

        public string Programa
        {
            get { return _programa; }
            set { _programa = value; }
        }
    }
}
