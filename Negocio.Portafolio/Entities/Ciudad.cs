using DALC.Portafolio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

using Newtonsoft.Json;

namespace Negocio.Portafolio
{
    public class Ciudad : IPersistente
    {
        private int _idCiudad;
        private string _nombreCiudad;
        private Nullable <int>_idPais;

        public bool Read()
        {
            try
            {
                EntitiesCEM ctx = new DALC.Portafolio.EntitiesCEM();
                //Busca si existe el ciudad segun su id y asigna los valores a un obj CIUDAD ENTITY
                CIUDAD _ciudad = ctx.CIUDAD.First(c => c.ID_CIUDAD == IdCiudad);
                //Asigna los valores de obj del CIUDAD Entity al obj Ciudad de la Clase
                this.IdCiudad = _ciudad.ID_CIUDAD;
                this.NombreCiudad = _ciudad.NOM_CIUDAD;
                this.IdPais = _ciudad.ID_PAIS;

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
                //Busca si existe el CIUDAD segun su id 
                if (ctx.CIUDAD.Any(c => c.ID_CIUDAD == IdCiudad))
                {
                    //Llama al procedimiento DELETE en la tabla CIUDAD
                    ctx.DEL_CIUDAD(IdCiudad);
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
                //Busca si existe el ciudad segun su id
                if (ctx.CIUDAD.Any(c => c.ID_CIUDAD == IdCiudad))
                {
                    //Llama al procedimiento UPDATE en la tabla CIUDAD
                    ctx.UPD_CIUDAD(IdCiudad, NombreCiudad, IdPais);
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
                //Llama al procedimiento INSERT en la tabla CIUDAD
                ctx.INS_CIUDAD(NombreCiudad, IdPais);
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
            return JsonConvert.SerializeObject(this);
            //XmlSerializer serializador = new XmlSerializer(typeof(Ciudad));
            //StringWriter writer = new StringWriter();

            //serializador.Serialize(writer, this);
            //writer.Close();

            //return writer.ToString();
        }
        public Ciudad(string json)
        {
            //XmlSerializer serializer = new XmlSerializer(typeof(Ciudad));
            //StringReader reader = new StringReader(xml);

            Ciudad ciudad = JsonConvert.DeserializeObject<Ciudad>(json);

            this.IdCiudad = ciudad.IdCiudad;
            this.NombreCiudad = ciudad.NombreCiudad;
            this.IdPais = ciudad.IdPais;
        }
        public Ciudad()
        {
            this.Init();
        }
        private void Init()
        {
            this.IdCiudad = 0;
            this.NombreCiudad = string.Empty;
            this.IdPais = 0;
        }

        public int IdCiudad
        {
            get { return _idCiudad; }
            set { _idCiudad = value; }
        }
        public string NombreCiudad
        {
            get { return _nombreCiudad; }
            set { _nombreCiudad = value; }
        }
        public Nullable <int> IdPais
        {
            get { return _idPais; }
            set { _idPais = value; }
        }

        
    }
}
