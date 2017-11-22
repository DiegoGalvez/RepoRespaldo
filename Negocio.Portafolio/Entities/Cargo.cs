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
    public class Cargo : IPersistente
    {
        private int _idCargo;
        private string _nombreCargo;

        public bool Read()
        {
            try
            {
                EntitiesCEM ctx = new DALC.Portafolio.EntitiesCEM();
                //Busca si existe el cargo segun su id y asigna los valores a un obj CARGO ENTITY
                CARGO _cargo = ctx.CARGO.First(c => c.ID_CARGO == IdCargo);
                //Asigna los valores de obj del CARGO Entity al obj Cargo de la Clase
                this.IdCargo= _cargo.ID_CARGO;
                this.NombreCargo = _cargo.NOM_CARGO;
                
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
                //Busca si existe el CARGO segun su id 
                if (ctx.CARGO.Any(c => c.ID_CARGO == IdCargo))
                {
                    //Llama al procedimiento DELETE en la tabla CARGO
                    ctx.DEL_CARGO(IdCargo);
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
                //Busca si existe el cargo segun su id
                if (ctx.CARGO.Any(c => c.ID_CARGO == IdCargo))
                {
                    //Llama al procedimiento UPDATE en la tabla CARGO
                    ctx.UPD_CARGO(IdCargo, NombreCargo);
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
                //Llama al procedimiento INSERT en la tabla CARGO
                ctx.INS_CARGO(IdCargo, NombreCargo);
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
            XmlSerializer serializador = new XmlSerializer(typeof(Cargo));
            StringWriter writer = new StringWriter();

            serializador.Serialize(writer, this);
            writer.Close();

            return writer.ToString();
        }

        public Cargo(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Cargo));
            StringReader reader = new StringReader(xml);

            Cargo car = (Cargo)serializer.Deserialize(reader);

            this.IdCargo = car.IdCargo;
            this.NombreCargo = car.NombreCargo;
        }

        public Cargo()
        {
            this.Init();
        }

        private void Init()
        {
            this.IdCargo = 0;
            this.NombreCargo = string.Empty;
        }

        public int IdCargo
        {
            get { return _idCargo; }
            set { _idCargo = value; }
        }
        public string NombreCargo
        {
            get { return _nombreCargo; }
            set { _nombreCargo = value; }
        }
    }
}
