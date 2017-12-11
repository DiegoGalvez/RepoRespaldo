using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Portafolio.ViewClasses
{
    public class VPrograma
    {
        private int _idPrograma;
        private string _nombrePrograma;
        private string _descripcion;
        private string _cupos;
        private string institucion;
        private string fechaInicio;
        private string fechaTermino;
        private string tipoCurso;
        private string estado;

        public VPrograma(Programa programa)
        {
            this.IdPrograma = programa.IdPrograma;
            this.NombrePrograma = programa.NombrePrograma;
            this.Descripcion = programa.Descripcion;
            this.Cupos = programa.Cupos.ToString();

            Institucion institucion = new Institucion();

            institucion.IdInstitucion = programa.IdInstitucion.GetValueOrDefault();
            institucion.Read();

            this.Institucion = institucion.Nombres.ToString();

            this.FechaInicio = programa.FechaInicio.ToString();
            this.FechaTermino = programa.FechaTermino.ToString();
            this.TipoCurso = programa.TipoCurso.ToString();
            this.Estado = programa.Estado.ToString();
        }

        public int IdPrograma
        {
            get { return _idPrograma; }
            set { _idPrograma = value; }
        }

        public string NombrePrograma
        {
            get { return _nombrePrograma; }
            set { _nombrePrograma = value; }
        }

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        public string Cupos
        {
            get { return _cupos; }
            set { _cupos = value; }
        }

        public string Institucion
        {
            get { return institucion; }
            set { institucion = value; }
        }

        public string FechaInicio
        {
            get { return fechaInicio; }
            set { fechaInicio = value; }
        }

        public string FechaTermino
        {
            get { return fechaTermino; }
            set { fechaTermino = value; }
        }

        public string TipoCurso
        {
            get { return tipoCurso; }
            set { tipoCurso = value; }
        }

        public string Estado
        {
            get { return estado; }
            set { estado = value; }
        }
    }
}
