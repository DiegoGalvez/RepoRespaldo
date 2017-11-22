using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DALC.Portafolio;


namespace Negocio.Portafolio
{
    public class EncargadoCel : IPersistente
    {
        private int _idEncargadoCel;
        private string _identificacion;
        private string _nombres;
        private string _apePaterno;
        private string _apeMaterno;
        private string _correo;
        private int _idPais;
        private int _idCiudad;

        public bool Read()
        {
            try
            {
                EntitiesCEM ctx = new DALC.Portafolio.EntitiesCEM();
                //Busca si existe el ENCARGADO_CEL segun su id y asigna los valores a un obj ENCARGADO_CEL ENTITY
                ENCARGADO_CEL _encargado = ctx.ENCARGADO_CEL.First(c => c.ID_ENCARGADO_CEL == IdEncargadoCel);
                //Asigna los valores de obj del ENCARGADO_CEL Entity al obj EncargadoCEL de la Clase
                this.IdEncargadoCel = _encargado.ID_ENCARGADO_CEL;
                this.Identificacion = _encargado.IDENTIFICACION;
                this.Nombre = _encargado.NOMBRE;
                this.ApePaterno = _encargado.APELL_PATERNO;
                this.ApeMaterno = _encargado.APELL_MATERNO;
                this.Correo = _encargado.CORREO;
                this.IdPais = _encargado.ID_PAIS;
                this.IdCiudad = _encargado.ID_CIUDAD;

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
                //Busca si existe el ENCARGADO_CEL segun su id 
                if (ctx.ENCARGADO_CEL.Any(e => e.ID_ENCARGADO_CEL == IdEncargadoCel))
                {
                    //Llama al procedimiento DELETE en la tabla ENCARGADO_CEL
                    ctx.DEL_CIUDAD(IdEncargadoCel);
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
                //Busca si existe el ENCARGADO_CEL segun su id
                if (ctx.ENCARGADO_CEL.Any(c => c.ID_ENCARGADO_CEL == IdEncargadoCel))
                {
                    //Llama al procedimiento UPDATE en la tabla ENCARGADO_CEL
                    ctx.UPD_ENCARGADO_CEL1(ApePaterno, ApeMaterno, Correo, IdEncargadoCel, Identificacion, IdCiudad, Nombre, IdPais);
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
                //Llama al procedimiento INSERT en la tabla ENCARGADO_CEL
                ctx.INS_ENCARGADO_CEL1(ApePaterno, ApeMaterno, Correo, IdEncargadoCel, Identificacion, IdCiudad, Nombre, IdPais);
                ctx.SaveChanges();
                ctx = null;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public EncargadoCel()
        {
            this.Init();
        }
        private void Init()
        {
            this.IdEncargadoCel = 0;
            this.Identificacion = string.Empty;
            this.Nombre = string.Empty;
            this.ApePaterno = string.Empty;
            this.ApeMaterno = string.Empty;
            this.Correo = string.Empty;
            this.IdPais = 0;
            this.IdCiudad = 0;
        }
        public EncargadoCel(string xml)
        {
            XmlSerializer serializiador = new XmlSerializer(typeof(EncargadoCel));
            StringReader reader = new StringReader(xml);

            EncargadoCel encargadoCel = (EncargadoCel)serializiador.Deserialize(reader);

            this.IdEncargadoCel = encargadoCel.IdEncargadoCel;
            this.Identificacion = encargadoCel.Identificacion;
            this.Nombre = encargadoCel.Nombre;
            this.ApePaterno = encargadoCel.ApePaterno;
            this.ApeMaterno = encargadoCel.ApeMaterno;
            this.Correo = encargadoCel.Correo;
            this.IdPais = encargadoCel.IdPais;
            this.IdCiudad = encargadoCel.IdCiudad;
        }
        public string Serializar()
        {
            XmlSerializer serializador = new XmlSerializer(typeof(EncargadoCel));
            StringWriter writer = new StringWriter();

            serializador.Serialize(writer, this);
            writer.Close();

            return writer.ToString();
        }
        
        public int IdEncargadoCel
        {
            get { return _idEncargadoCel; }
            set { _idEncargadoCel = value; }
        }
        public string Nombre
        {
            get { return _nombres; }
            set { _nombres = value; }
        }
        public string ApePaterno
        {
            get { return _apePaterno; }
            set { _apePaterno = value; }
        }
        public string ApeMaterno
        {
            get { return _apeMaterno; } 
            set { _apeMaterno = value; }
        }
        public string Correo
        {
            get { return _correo; } 
            set { _correo = value; }
        }
        public int IdPais
        {
            get { return _idPais; } 
            set { _idPais = value; }
        }
        public int IdCiudad
        {
            get { return _idCiudad; } 
            set { _idCiudad = value; }
        }

        public string Identificacion
        {
            get { return _identificacion; }
            set { _identificacion = value; }
        }
    }
}
