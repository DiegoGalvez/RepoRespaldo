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
    public class IntercambioCollection : List<Intercambio>
    {
        public IntercambioCollection()
        {
        }

        public IntercambioCollection(string json)
        {
            //XmlSerializer serializador = new XmlSerializer(typeof(IntercambioCollection));
            //StringReader reader = new StringReader(xml);

            IntercambioCollection list = JsonConvert.DeserializeObject<IntercambioCollection>(json);

            this.AddRange(list);
        }


        //metodo que lee todos los Intercambios
        public IntercambioCollection ReadAll()
        {
            var listaDalc = new EntitiesCEM().INTERCAMBIO;

            return GenerarListado(listaDalc.ToList());
        }

        //Metodo que agrega unos Intercambios a la lista y luego retorna la lista de Intercambios
        private IntercambioCollection GenerarListado(List<DALC.Portafolio.INTERCAMBIO> listaDALC)
        {
            IntercambioCollection listaBC = new IntercambioCollection();

            foreach (var item in listaDALC)
            {
                Intercambio intercambio = new Intercambio();

                intercambio.IdIntercambio = item.ID_INTERCAMBIO;
                intercambio.Estado= item.ESTADO;
                intercambio.IdAdministrativo = item.ID_ADMINISTRATIVO.GetValueOrDefault();
                intercambio.IdFamilia = item.ID_FAMILIA.GetValueOrDefault();
                intercambio.IdAlumno= item.ID_ALUMNO.GetValueOrDefault();
                intercambio.IdPrograma = item.ID_PROGRAMA.GetValueOrDefault();
                
                listaBC.Add(intercambio);
            }
            return listaBC;
        }

        //metodo que serializa la coleccion de Intercambios 
        public string Serializar()
        {
            return JsonConvert.SerializeObject(this);
            //XmlSerializer serializar = new XmlSerializer(typeof(IntercambioCollection));
            //StringWriter writer = new StringWriter();

            //serializar.Serialize(writer, this);

            //writer.Close();
            //return writer.ToString();
        }

    }
}
