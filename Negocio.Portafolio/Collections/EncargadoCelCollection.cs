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
    public class EncargadoCelCollection : List<EncargadoCel>
    {
        public EncargadoCelCollection()
        {
        }

        public EncargadoCelCollection(string xml)
        {
            XmlSerializer serializador = new XmlSerializer(typeof(EncargadoCelCollection));
            StringReader reader = new StringReader(xml);

            EncargadoCelCollection list = (EncargadoCelCollection)serializador.Deserialize(reader);

            this.AddRange(list);
        }


        //metodo que lee todos los encargados CEL
        public EncargadoCelCollection ReadAll()
        {
            var listaDalc = new EntitiesCEM().ENCARGADO_CEL;

            return GenerarListado(listaDalc.ToList());
        }

        //Metodo que agrega un encargado CEL a la lista y luego retorna la lista de encargados CEL 
        private EncargadoCelCollection GenerarListado(List<DALC.Portafolio.ENCARGADO_CEL> listaDALC)
        {
            EncargadoCelCollection listaBC = new EncargadoCelCollection();

            foreach (var item in listaDALC)
            {
                EncargadoCel encargadoCel  = new EncargadoCel();

                encargadoCel.IdEncargadoCel = item.ID_ENCARGADO_CEL;
                encargadoCel.Identificacion = item.IDENTIFICACION;
                encargadoCel.Nombre = item.NOMBRE;
                encargadoCel.ApePaterno = item.APELL_PATERNO;
                encargadoCel.ApeMaterno = item.APELL_MATERNO;
                encargadoCel.Correo = item.CORREO;
                encargadoCel.IdPais = item.ID_PAIS;
                encargadoCel.IdCiudad = item.ID_CIUDAD;

                listaBC.Add(encargadoCel);
            }
            return listaBC;
        }

        //metodo que serializa la coleccion de encargados CEL 
        public string Serializar()
        {
            XmlSerializer serializar = new XmlSerializer(typeof(EncargadoCelCollection));
            StringWriter writer = new StringWriter();

            serializar.Serialize(writer, this);

            writer.Close();
            return writer.ToString();
        }

    }
}
