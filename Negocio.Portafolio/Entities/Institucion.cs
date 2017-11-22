using DALC.Portafolio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Negocio.Portafolio
{
    public class Institucion : IPersistente
    {
        private int _idInstitucion;
        private string _nombres;
        private string _correo;
        private Nullable<long> _telefono;
        private string _paginaWeb;
        private string _direcion;
        private Nullable<int> _idCiudad;
        private Nullable<int> _idPais;

        public bool Read()
        {
            try
            {
                EntitiesCEM ctx = new DALC.Portafolio.EntitiesCEM();
                //Busca si existe el Institucion segun su id y asigna los valores a un obj INSTITUCION ENTITY
                INSTITUCION _institucion = ctx.INSTITUCION.First(i => i.ID_INSTITUCION == IdInstitucion);
                //Asigna los valores de obj del INSTITUCION Entity al obj Institucion de la Clase
                this.IdInstitucion = _institucion.ID_INSTITUCION;
                this.Nombres = _institucion.NOMBRE;
                this.Correo = _institucion.CORREO;
                this.Telefono = (long)_institucion.TELEFONO;
                this.PaginaWeb = _institucion.PAGINA_WEB;
                this.Direcion = _institucion.DIRECCION;
                this.IdCiudad = _institucion.ID_CIUDAD;
                this.IdPais = _institucion.ID_PAIS;
                
                ctx = null;

                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        public bool Delete()
        {
            try
            {
                EntitiesCEM ctx = new EntitiesCEM();
                //Busca si existe el Institucion segun su id 
                if (ctx.INSTITUCION.Any(f => f.ID_INSTITUCION == IdInstitucion))
                {
                    //Llama al procedimiento DELETE en la tabla INSTITUCION
                    ctx.DEL_INSTITUCION(IdInstitucion);
                    ctx.SaveChanges();
                    ctx = null;

                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Update()
        {
            try
            {
                EntitiesCEM ctx = new EntitiesCEM();
                //Busca si existe el INSTITUCION segun su id
                if (ctx.INSTITUCION.Any(i => i.ID_INSTITUCION == IdInstitucion))
                {
                    //Llama al procedimiento UPDATE en la tabla INSTITUCION
                    ctx.UPD_INSTITUCION(Direcion, Correo, PaginaWeb, IdCiudad, Telefono, IdInstitucion, Nombres, IdPais);
                    ctx.SaveChanges();
                    ctx = null;

                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Create()
        {
            try
            {
                EntitiesCEM ctx = new EntitiesCEM();
                //Llama al procedimiento INSERT en la tabla INSTITUCION
                ctx.INS_INSTITUCION(Direcion, Correo, PaginaWeb, IdCiudad, Telefono, IdInstitucion, Nombres, IdPais);
                ctx.SaveChanges();
                ctx = null;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public string Serializar()
        {
            XmlSerializer serializador = new XmlSerializer(typeof(Institucion));
            StringWriter writer = new StringWriter();

            serializador.Serialize(writer, this);
            writer.Close();

            return writer.ToString();
        }
        public Institucion(string xml)
        {
            XmlSerializer serializiador = new XmlSerializer(typeof(Institucion));
            StringReader reader = new StringReader(xml);

            Institucion institucion = (Institucion)serializiador.Deserialize(reader);

            this.IdInstitucion = institucion.IdInstitucion;
            this.Nombres = institucion.Nombres;
            this.Correo = institucion.Correo;
            this.Telefono = institucion.Telefono;
            this.PaginaWeb = institucion.PaginaWeb;
            this.Direcion = institucion.Direcion;
            this.IdCiudad = institucion.IdCiudad;
            this.IdPais = institucion.IdPais;
        }
        public Institucion()
        {
            this.Init();
        }
        private void Init()
        {
            this.IdInstitucion = 0;
            this.Nombres = string.Empty;
            this.Correo = string.Empty;
            this.Telefono = 0;
            this.PaginaWeb = string.Empty;
            this.Direcion = string.Empty;
            this.IdCiudad = 0;
            this.IdPais = 0;
        }

        public int IdInstitucion
        {
            get { return _idInstitucion; }
            set { _idInstitucion = value; }
        }
        public string Nombres
        {
            get { return _nombres; }
            set { _nombres = value; }
        }
        public string Correo
        {
            get { return _correo; }
            set { _correo = value; }
        }
        public Nullable<long> Telefono
        {
            get { return _telefono; }
            set { _telefono = value; }
        }
        public string PaginaWeb
        {
            get { return _paginaWeb; }
            set { _paginaWeb = value; }
        }
        public string Direcion
        {
            get { return _direcion;}
            set { _direcion = value; }
        }
        public Nullable<int> IdCiudad
        {
            get { return _idCiudad; }
            set { _idCiudad = value; }
        }
        public Nullable<int> IdPais
        {
            get { return _idPais; }
            set { _idPais = value; }
        }
    }
}
