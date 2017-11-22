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
    public class CiudadCollection : List<Ciudad>
    {
        public CiudadCollection()
        {
        }

        public CiudadCollection(string json)
        {
            //XmlSerializer serializador = new XmlSerializer(typeof(CiudadCollection));
            //StringReader reader = new StringReader(xml);

            CiudadCollection list = JsonConvert.DeserializeObject<CiudadCollection>(json);

            this.AddRange(list);
        }


        //metodo que lee todos las Ciudades
        public CiudadCollection ReadAll()
        {
            var listaDalc = new EntitiesCEM().CIUDAD;

            return GenerarListado(listaDalc.ToList());
        }

        //metodo que lee todos las Ciudades de un Pais
        public CiudadCollection LeerCiudades(int pais)
        {
            var listaDalc = new EntitiesCEM().CIUDAD;

            return GenerarListado(listaDalc.Where(ciudad => ciudad.ID_PAIS == pais).ToList());
        }
        //Metodo que agrega un alumno a la lista y luego retorna la lista de alumnos 
        private CiudadCollection GenerarListado(List<DALC.Portafolio.CIUDAD> listaDALC)
        {
            CiudadCollection listaBC = new CiudadCollection();

            foreach (var item in listaDALC)
            {
                Ciudad ciudad = new Ciudad();

                ciudad.IdCiudad = item.ID_CIUDAD;
                ciudad.NombreCiudad = item.NOM_CIUDAD;
                ciudad.IdPais = item.ID_PAIS.GetValueOrDefault();

                listaBC.Add(ciudad);
            }
            return listaBC;
        }

        //metodo que serializa la coleccion de Ciudades
        public string Serializar()
        {

            return JsonConvert.SerializeObject(this);
            //XmlSerializer serializar = new XmlSerializer(typeof(CiudadCollection));
            //StringWriter writer = new StringWriter();

            //serializar.Serialize(writer, this);

            //writer.Close();
            //return writer.ToString();
        }

    }
}
