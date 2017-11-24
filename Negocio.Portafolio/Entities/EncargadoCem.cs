using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DALC.Portafolio;

using Newtonsoft.Json;


namespace Negocio.Portafolio
{
    public class EncargadoCem : IPersistente
    {
        private int _idEncargadoCem;
        private string _identificacion;
        private string _nombres;
        private string _apePaterno;
        private string _apeMaterno;
        private string _correo;
        private int _idInstitucion;

        public bool Read()
        {
            try
            {
                EntitiesCEM ctx = new DALC.Portafolio.EntitiesCEM();
                //Busca si existe el ENCARGADO_CEM segun su id y asigna los valores a un obj ENCARGADO_CEM ENTITY
                ENCARGADO_CEM _encargado = ctx.ENCARGADO_CEM.First(c => c.ID_ENCARGADO_CEM == IdEncargadoCem);
                //Asigna los valores de obj del ENCARGADO_CEM Entity al obj EncargadoCEM de la Clase
                this.IdEncargadoCem = _encargado.ID_ENCARGADO_CEM;
                this.Identificacion = _encargado.IDENTIFICACION;
                this.Nombre = _encargado.NOMBRE;
                this.ApePaterno = _encargado.APELL_PATERNO;
                this.ApeMaterno = _encargado.APELL_MATERNO;
                this.Correo = _encargado.CORREO;

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
                //Busca si existe el ENCARGADO_CEM segun su id 
                if (ctx.ENCARGADO_CEM.Any(e => e.ID_ENCARGADO_CEM == IdEncargadoCem))
                {
                    //Llama al procedimiento DELETE en la tabla ENCARGADO_CEM
                    ctx.DEL_CIUDAD(IdEncargadoCem);
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
                //Busca si existe el ENCARGADO_CEM segun su id
                if (ctx.ENCARGADO_CEM.Any(c => c.ID_ENCARGADO_CEM == IdEncargadoCem))
                {
                    //Llama al procedimiento UPDATE en la tabla ENCARGADO_CEM
                    ctx.UPD_ENCARGADO_CEM(IdEncargadoCem, Identificacion, Nombre, ApePaterno, ApeMaterno, Correo);
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
                //Llama al procedimiento INSERT en la tabla ENCARGADO_CEM
                ctx.INS_ENCARGADO_CEM(Identificacion, Nombre, ApePaterno, ApeMaterno, Correo);
                ctx.SaveChanges();
                ctx = null;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public EncargadoCem()
        {
            this.Init();
        }
        private void Init()
        {
            this.IdEncargadoCem = 0;
            this.Identificacion = string.Empty;
            this.Nombre = string.Empty;
            this.ApePaterno = string.Empty;
            this.ApeMaterno = string.Empty;
            this.Correo = string.Empty;
        }
        public EncargadoCem(string json)
        {
            //XmlSerializer serializiador = new XmlSerializer(typeof(EncargadoCem));
            //StringReader reader = new StringReader(xml);

            EncargadoCem encargadoCem = JsonConvert.DeserializeObject<EncargadoCem>(json);

            this.IdEncargadoCem = encargadoCem.IdEncargadoCem;
            this.Identificacion = encargadoCem.Identificacion;
            this.Nombre = encargadoCem.Nombre;
            this.ApePaterno = encargadoCem.ApePaterno;
            this.ApeMaterno = encargadoCem.ApeMaterno;
            this.Correo = encargadoCem.Correo;
        }
        public string Serializar()
        {
            return JsonConvert.SerializeObject(this);
            //XmlSerializer serializador = new XmlSerializer(typeof(EncargadoCem));
            //StringWriter writer = new StringWriter();

            //serializador.Serialize(writer, this);
            //writer.Close();

            //return writer.ToString();
        }
        
        public int IdEncargadoCem
        {
            get { return _idEncargadoCem; }
            set { _idEncargadoCem = value; }
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
       
        public string Identificacion
        {
            get { return _identificacion; }
            set { _identificacion = value; }
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
    }
}
