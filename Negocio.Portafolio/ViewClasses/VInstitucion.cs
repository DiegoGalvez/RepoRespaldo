using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Portafolio.ViewClasses
{
    public class VInstitucion
    {
        private int _idInstitucion;
        private string _nombres;
        private string _correo;
        private Nullable<long> _telefono;
        private string _paginaWeb;
        private string _direcion;
        private string _ciudad;
        private string _pais;

        public VInstitucion(Institucion institucion)
        {
            this.Init();


            this._idInstitucion = institucion.IdInstitucion;
            this._nombres = institucion.Nombres;
            this._correo = institucion.Correo;
            this._telefono = institucion.Telefono;
            this._paginaWeb = institucion.PaginaWeb;
            this._direcion = institucion.Direcion;

            Ciudad ciudad = new Ciudad();

            ciudad.IdCiudad = institucion.IdCiudad.GetValueOrDefault();
            ciudad.Read();

            this._ciudad = ciudad.NombreCiudad;

            Pais pais= new Pais();

            pais.IdPais = institucion.IdPais.GetValueOrDefault();
            pais.Read();

            this._pais = pais.NombrePais;
        }

        private void Init()
        {
            this.IdInstitucion = 0;
            this.Nombres = string.Empty;
            this.Correo = string.Empty;
            this.Telefono = 0;
            this.PaginaWeb = string.Empty;
            this.Direcion = string.Empty;
            this.Ciudad = string.Empty;
            this.Pais = string.Empty;
        }

        public int IdInstitucion
        {
            get
            {
                return _idInstitucion;
            }

            set
            {
                _idInstitucion = value;
            }
        }

        public string Nombres
        {
            get
            {
                return _nombres;
            }

            set
            {
                _nombres = value;
            }
        }

        public string Correo
        {
            get
            {
                return _correo;
            }

            set
            {
                _correo = value;
            }
        }

        public Nullable<long> Telefono
        {
            get
            {
                return _telefono;
            }

            set
            {
                _telefono = value;
            }
        }

        public string PaginaWeb
        {
            get
            {
                return _paginaWeb;
            }

            set
            {
                _paginaWeb = value;
            }
        }

        public string Direcion
        {
            get
            {
                return _direcion;
            }

            set
            {
                _direcion = value;
            }
        }

        public string Ciudad
        {
            get
            {
                return _ciudad;
            }

            set
            {
                _ciudad = value;
            }
        }

        public string Pais
        {
            get
            {
                return _pais;
            }

            set
            {
                _pais = value;
            }
        }
    }
}
