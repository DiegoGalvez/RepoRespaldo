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
    public class ActividadCollection : List<Actividad>
    {
        public ActividadCollection()
        {
        }

        public ActividadCollection(string xml)
        {
            XmlSerializer serializador = new XmlSerializer(typeof(ActividadCollection));
            StringReader reader = new StringReader(xml);

            ActividadCollection list = (ActividadCollection)serializador.Deserialize(reader);

            this.AddRange(list);
        }


        //metodo que lee todas las actividades
        public ActividadCollection ReadAll()
        {
            var listaDalc = new EntitiesCEM().ACTIVIDAD;

            return GenerarListado(listaDalc.ToList());
        }

        //Metodo que agrega una Actividad la lista y luego retorna la lista de Actividades 
        private ActividadCollection GenerarListado(List<DALC.Portafolio.ACTIVIDAD> listaDALC)
        {
            ActividadCollection listaBC = new ActividadCollection();

            foreach (var item in listaDALC)
            {
                Actividad actividad = new Actividad();

                actividad.IdActividad= item.ID_ACTIVIDAD;
                actividad.NombreActividad = item.NOMBREACTIVIDAD;
                actividad.Descripcion = item.DESCRIPCION;
                
                listaBC.Add(actividad);
            }
            return listaBC;
        }

        //metodo que serializa la coleccion de actividades
        public string Serializar()
        {
            XmlSerializer serializar = new XmlSerializer(typeof(ActividadCollection));
            StringWriter writer = new StringWriter();

            serializar.Serialize(writer, this);

            writer.Close();
            return writer.ToString();
        }

    }
}
