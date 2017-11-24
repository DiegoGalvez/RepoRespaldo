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
    public class EncargadoCemCollection : List<EncargadoCem>
    {
        public EncargadoCemCollection()
        {
        }

        public EncargadoCemCollection(string json)
        {
            //XmlSerializer serializador = new XmlSerializer(typeof(EncargadoCemCollection));
            //StringReader reader = new StringReader(xml);

            EncargadoCemCollection list = JsonConvert.DeserializeObject<EncargadoCemCollection>(json);

            this.AddRange(list);
        }


        //metodo que lee todos los encargados CEM
        public EncargadoCemCollection ReadAll()
        {
            var listaDalc = new EntitiesCEM().ENCARGADO_CEM;

            return GenerarListado(listaDalc.ToList());
        }

        //Metodo que agrega un encargado CEL a la lista y luego retorna la lista de encargados CEL 
        private EncargadoCemCollection GenerarListado(List<DALC.Portafolio.ENCARGADO_CEM> listaDALC)
        {
            EncargadoCemCollection listaBC = new EncargadoCemCollection();

            foreach (var item in listaDALC)
            {
                EncargadoCem encargadoCem  = new EncargadoCem();

                encargadoCem.IdEncargadoCem = item.ID_ENCARGADO_CEM;
                encargadoCem.Identificacion = item.IDENTIFICACION;
                encargadoCem.Nombre = item.NOMBRE;
                encargadoCem.ApePaterno = item.APELL_PATERNO;
                encargadoCem.ApeMaterno = item.APELL_MATERNO;
                encargadoCem.Correo = item.CORREO;

                listaBC.Add(encargadoCem);
            }
            return listaBC;
        }

        //metodo que serializa la coleccion de encargados CEM
        public string Serializar()
        {
            return JsonConvert.SerializeObject(this);
            //XmlSerializer serializar = new XmlSerializer(typeof(EncargadoCemCollection));
            //StringWriter writer = new StringWriter();

            //serializar.Serialize(writer, this);

            //writer.Close();
            //return writer.ToString();
        }

    }
}
