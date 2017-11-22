using DALC.Portafolio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
//using DALC.Portafolio;

namespace Negocio.Portafolio
{
    public class Administrativo : IPersistente
    {
        private int _idAdministrativo;
        private string _nombres;
        private string _apePaterno;
        private string _apeMaterno;
        private string _correo;
        private int _idCargo;

        public bool Read()
        {
            try
            {
                EntitiesCEM ctx = new DALC.Portafolio.EntitiesCEM();
                //Busca si existe el Administrativos segun su id y asigna los valores a un obj ADMINISTRATIVO ENTITY
                ADMINISTRATIVO _administrativo = ctx.ADMINISTRATIVO.First(a => a.ID_ADMINISTRATIVO == IdAdministrativo);
                //Asigna los valores de obj del ADMINISTRATIVO Entity al obj Administrativo de la Clase
                this.IdAdministrativo = _administrativo.ID_ADMINISTRATIVO;
                this.Nombres = _administrativo.NOMBRE;
                this.ApePaterno = _administrativo.APELL_PATERNO;
                this.ApeMaterno = _administrativo.APELL_MATERNO;
                this.Correo = _administrativo.CORREO;
                this.IdCargo = _administrativo.ID_CARGO.GetValueOrDefault();
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
                //Busca si existe el ADMINISTRATIVO segun su id 
                if (ctx.ADMINISTRATIVO.Any(a => a.ID_ADMINISTRATIVO == IdAdministrativo))
                {
                    //Llama al procedimiento DELETE en la tabla ACTIVIDAD
                    ctx.DEL_ADMINISTRATIVO(IdAdministrativo);
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
                //Busca si existe la actividad segun su id
                if (ctx.ADMINISTRATIVO.Any(a => a.ID_ADMINISTRATIVO == IdAdministrativo))
                {
                    //Llama al procedimiento UPDATE en la tabla ACTIVIDAD
                    ctx.UPD_ADMINISTRATIVO(ApePaterno, ApeMaterno, IdCargo, Correo, IdAdministrativo, Nombres);
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
                //Llama al procedimiento INSERT en la tabla ACTIVIDAD
                ctx.INS_ADMINISTRATIVO(ApePaterno, ApeMaterno, IdCargo, Correo, IdAdministrativo, Nombres);
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
            XmlSerializer serializador = new XmlSerializer(typeof(Administrativo));
            StringWriter writer = new StringWriter();

            serializador.Serialize(writer, this);
            writer.Close();

            return writer.ToString();
        }
        public Administrativo(string xml)
        {
            /* Aplica serializiación para obtener las propiedades*/
            XmlSerializer serializiador = new XmlSerializer(typeof(Administrativo));
            StringReader reader = new StringReader(xml);

            Administrativo administrativo = (Administrativo)serializiador.Deserialize(reader);

            this.IdAdministrativo = administrativo.IdAdministrativo;
            this.Nombres = administrativo.Nombres;
            this.ApePaterno = administrativo.ApePaterno;
            this.ApeMaterno = administrativo.ApeMaterno;
            this.Correo = administrativo.Correo;
            this.IdCargo = administrativo.IdCargo;
        }
        public Administrativo()
        {
            this.Init();
        }

        private void Init()
        {
            this.IdAdministrativo = 0;
            this.Nombres = string.Empty;
            this.ApePaterno = string.Empty;
            this.ApeMaterno = string.Empty;
            this.Correo = string.Empty;
            this.IdCargo = 0;
        }

        public int IdAdministrativo
        {
            get { return _idAdministrativo; }
            set { _idAdministrativo = value; }
        }
        public string Nombres
        {
            get { return _nombres; }
            set { _nombres = value; }
        }
        public string ApeMaterno
        {
            get { return _apeMaterno; }
            set { _apeMaterno = value; }
        }
        public string ApePaterno
        {
            get { return _apePaterno; }
            set { _apePaterno = value; }
        }
        public string Correo
        {
            get { return _correo; }
            set { _correo = value; }
        }
        public int IdCargo
        {
            get { return _idCargo; }
            set { _idCargo = value; }
        }
    }
}
