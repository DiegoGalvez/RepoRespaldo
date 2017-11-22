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
    public class Intercambio : IPersistente
    {
        private int _idIntercambio;
        private string _estado;
        private int? _idAdministrativo;
        private int _idFamilia;
        private int _idAlumno;
        private int _idPrograma;

        public bool Read()
        {
            try
            {
                EntitiesCEM ctx = new DALC.Portafolio.EntitiesCEM();
                //Busca si existe el Intercambio segun su id y asigna los valores a un obj INTERCAMBIO ENTITY
                INTERCAMBIO _intercambio = ctx.INTERCAMBIO.First(i => i.ID_INTERCAMBIO == IdIntercambio);
                //Asigna los valores de obj del INTERCAMBIO Entity al obj Intercambio de la Clase
                this.IdIntercambio = _intercambio.ID_INTERCAMBIO;
                this.Estado = _intercambio.ESTADO;
                this.IdAdministrativo = _intercambio.ID_ADMINISTRATIVO.GetValueOrDefault();
                this.IdFamilia = _intercambio.ID_FAMILIA.GetValueOrDefault();
                this.IdAlumno = _intercambio.ID_ALUMNO.GetValueOrDefault();
                this.IdPrograma = _intercambio.ID_PROGRAMA.GetValueOrDefault();
                
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
                //Busca si existe el Intercambio segun su id 
                if (ctx.INTERCAMBIO.Any(i => i.ID_INTERCAMBIO == IdIntercambio))
                {
                    //Llama al procedimiento DELETE en la tabla INTERCAMBIO
                    ctx.DEL_INTERCAMBIO(IdIntercambio);
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
                //Busca si existe el INTERCAMBIO segun su id
                if (ctx.INTERCAMBIO.Any(i => i.ID_INTERCAMBIO == IdIntercambio))
                {
                    //Llama al procedimiento UPDATE en la tabla INTERCAMBIO
                    ctx.UPD_INTERCAMBIO1(IdIntercambio, Estado, IdFamilia, IdAdministrativo, IdPrograma, IdAlumno);
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
                //Llama al procedimiento INSERT en la tabla INS_INTERCAMBIO
                ctx.INS_INTERCAMBIO(IdIntercambio, Estado, IdFamilia, IdAdministrativo, IdPrograma, IdAlumno);
                ctx.SaveChanges();
                ctx = null;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public Intercambio(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Intercambio));
            StringReader reader = new StringReader(xml);

            Intercambio intercambio = (Intercambio)serializer.Deserialize(reader);

            this.IdIntercambio = intercambio.IdIntercambio;
            this.Estado = intercambio.Estado;
            this.IdAdministrativo = intercambio.IdAdministrativo;
            this.IdFamilia= intercambio.IdFamilia;
            this.IdAlumno = intercambio.IdAlumno;
            this.IdPrograma = intercambio.IdPrograma;
        }
        public string Serializar()
        {
            XmlSerializer serializador = new XmlSerializer(typeof(Intercambio));
            StringWriter writer = new StringWriter();

            serializador.Serialize(writer, this);
            writer.Close();

            return writer.ToString();
        }
        public Intercambio()
        {
            this.Init();
        }
        private void Init()
        {
            this.IdIntercambio = 0;
            this.Estado = string.Empty;
            this.IdAdministrativo = 0;
            this.IdFamilia = 0;
            this.IdAlumno = 0;
            this.IdPrograma = 0;
        }

        public int IdIntercambio
        {
            get { return _idIntercambio; }
            set { _idIntercambio = value; }
        } 
        public string Estado
        {
            get { return _estado; }
            set { _estado = value; }
        } 
        public int? IdAdministrativo
        {
            get { return _idAdministrativo; }
            set { _idAdministrativo = value; }
        } 
        public int IdFamilia
        {
            get { return _idFamilia; }
            set { _idFamilia = value; }
        } 
        public int IdAlumno
        {
            get { return _idAlumno; } 
            set { _idAlumno = value; }
        } 
        public int IdPrograma
        {
            get { return _idPrograma; }
            set { _idPrograma = value; }
        }
    }
}
