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
    public class Pais : IPersistente
    {
        private int _idPais;
        private string _nombrePais;
        private string _sigla;

        public bool Read()
        {
            try
            {
                EntitiesCEM ctx = new DALC.Portafolio.EntitiesCEM();
                //Busca si existe el Pais segun su id y asigna los valores a un obj PAIS ENTITY
                PAIS _pais = ctx.PAIS.First(p => p.ID_PAIS == IdPais);
                //Asigna los valores de obj del PAIS Entity al obj Pais de la Clase
                this.IdPais = _pais.ID_PAIS;
                this.Sigla = _pais.SIGLA;
                this.NombrePais = _pais.NOM_PAIS;
                
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
                //Busca si existe el Pais segun su id 
                if (ctx.PAIS.Any(p => p.ID_PAIS == IdPais))
                {
                    //Llama al procedimiento DELETE en la tabla PAIS
                    ctx.DEL_PAIS(IdPais);
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
                //Busca si existe el PAIS segun su id
                if (ctx.PAIS.Any(p => p.ID_PAIS == IdPais))
                {
                    //Llama al procedimiento UPDATE en la tabla PAIS
                    ctx.UPD_PAIS(IdPais, NombrePais, Sigla);
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
                //Llama al procedimiento INSERT en la tabla PAIS
                ctx.INS_PAIS(NombrePais, Sigla);
                ctx.SaveChanges();
                ctx = null;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public Pais(string json)
        {
            //XmlSerializer serializer = new XmlSerializer(typeof(Pais));
            //StringReader reader = new StringReader(xml);

            Pais pais = JsonConvert.DeserializeObject<Pais>(json);

            this.IdPais = pais.IdPais;
            this.NombrePais = pais.NombrePais;
            this.Sigla = pais.Sigla;
        }
        public string Serializar()
        {
            return JsonConvert.SerializeObject(this);
            //XmlSerializer serializador = new XmlSerializer(typeof(Pais));
            //StringWriter writer = new StringWriter();

            //serializador.Serialize(writer, this);
            //writer.Close();

            //return writer.ToString();
        }
        public Pais()
        {
            this.Init();
        }
        private void Init()
        {
            this.IdPais = 0;
            this.NombrePais = string.Empty;
            this.Sigla = string.Empty;
        }

        public int IdPais
        {
            get { return _idPais; } 
            set { _idPais = value; }
        } 
        public string NombrePais
        {
            get { return _nombrePais; } 
            set { _nombrePais = value; }
        } 
        public string Sigla
        {
            get { return _sigla; } 
            set { _sigla = value; }
        }
    }
}
