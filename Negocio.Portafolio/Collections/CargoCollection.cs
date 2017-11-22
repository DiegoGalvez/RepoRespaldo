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
    public class CargoCollection : List<Cargo>
    {
        public CargoCollection()
        {
        }

        public CargoCollection(string xml)
        {
            XmlSerializer serializador = new XmlSerializer(typeof(CargoCollection));
            StringReader reader = new StringReader(xml);

            CargoCollection list = (CargoCollection)serializador.Deserialize(reader);

            this.AddRange(list);
        }


        //metodo que lee todos los Cargos
        public CargoCollection ReadAll()
        {
            var listaDalc = new EntitiesCEM().CARGO;

            return GenerarListado(listaDalc.ToList());
        }

        //Metodo que agrega un Cargo a la lista y luego retorna la lista de Cargo
        private CargoCollection GenerarListado(List<DALC.Portafolio.CARGO> listaDALC)
        {
            CargoCollection listaBC = new CargoCollection();

            foreach (var item in listaDALC)
            {
                Cargo cargo = new Cargo();

                cargo.IdCargo = item.ID_CARGO;
                cargo.NombreCargo = item.NOM_CARGO;
                
                listaBC.Add(cargo);
            }
            return listaBC;
        }

        //metodo que serializa la coleccion de cargos
        public string Serializar()
        {
            XmlSerializer serializar = new XmlSerializer(typeof(CargoCollection));
            StringWriter writer = new StringWriter();

            serializar.Serialize(writer, this);

            writer.Close();
            return writer.ToString();
        }

    }
}
