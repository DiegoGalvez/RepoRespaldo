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
    public class Actividad : IPersistente
    {
        private int _idActividad;
        private string _nombreActividad;
        private string _descripcion;
        
        public bool Read()
        {
            try
            {
                EntitiesCEM ctx = new DALC.Portafolio.EntitiesCEM();
                ACTIVIDAD _actividad = ctx.ACTIVIDAD.First(a => a.ID_ACTIVIDAD == IdActividad);

                this.IdActividad = _actividad.ID_ACTIVIDAD;
                this.NombreActividad = _actividad.NOMBREACTIVIDAD;
                this.Descripcion = _actividad.DESCRIPCION;

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
                //Busca si existe la actividad segun su id 
                if (ctx.ACTIVIDAD.Any(a => a.ID_ACTIVIDAD == IdActividad))
                {
                    //Llama al procedimiento DELETE en la tabla ACTIVIDAD
                    ctx.DEL_ACTIVIDAD(IdActividad);
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
                if (ctx.ACTIVIDAD.Any(a => a.ID_ACTIVIDAD == IdActividad))
                {
                    //Llama al procedimiento UPDATE en la tabla ACTIVIDAD
                    ctx.UPD_ACTIVIDAD(IdActividad, NombreActividad,Descripcion);
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
                ctx.INS_ACTIVIDAD(IdActividad, NombreActividad, Descripcion);
                ctx.SaveChanges();
                ctx = null;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int CurrentActividadEntityID()
        {
            int id;
            try
            {
                EntitiesCEM ctx = new EntitiesCEM();

                id = ctx.Database.SqlQuery<int>("SELECT ACTIVIDAD_ID_ACTIVIDAD_SEQ.CURRVAL FROM dual").FirstOrDefault();

                ctx = null;
                return id;
            }
            catch (Exception)
            {
                throw new Exception("No se pudo obtener id actual");
            }
        }

        public bool LinkPrograma(int idPrograma, int idActividad)
        {
            try
            {
                EntitiesCEM ctx = new EntitiesCEM();

                //Llama al procedimiento INSERT en la tabla PROGRAMA_ACTIVIDAD
                ctx.INS_PROGRAMA_ACTIVIDAD(idPrograma, idActividad);
                ctx.SaveChanges();
                ctx = null;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UnlinkPrograma()
        {
            try
            {
                EntitiesCEM ctx = new EntitiesCEM();
                //Busca si existe la actividad segun su id 
                if (ctx.ACTIVIDAD.Any(a => a.ID_ACTIVIDAD == IdActividad))
                {
                    //Llama al procedimiento DELETE en la tabla ACTIVIDAD
                    ctx.DEL_PROGRAMA_ACTIVIDAD(IdActividad);
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
        
        public string Serializar()
        {
            XmlSerializer serializador = new XmlSerializer(typeof(Actividad));
            StringWriter writer = new StringWriter();

            serializador.Serialize(writer, this);
            writer.Close();

            return writer.ToString();
        }
        public Actividad(string xml)
        {
            XmlSerializer serializiador = new XmlSerializer(typeof(Actividad));
            StringReader reader = new StringReader(xml);

            Actividad actividad = (Actividad)serializiador.Deserialize(reader);

            this.IdActividad = actividad.IdActividad;
            this.NombreActividad = actividad.NombreActividad;
            this.Descripcion = actividad.Descripcion;
        }
        public Actividad()
        {
            this.init();
        }
        private void init()
        {
            this.IdActividad = 0;
            this.NombreActividad = string.Empty;
            this.Descripcion = string.Empty;
        }
        public int IdActividad
        {
            get { return _idActividad; }
            set { _idActividad = value; }
        }
        public string NombreActividad
        {
            get { return _nombreActividad; }
            set { _nombreActividad = value; }
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
