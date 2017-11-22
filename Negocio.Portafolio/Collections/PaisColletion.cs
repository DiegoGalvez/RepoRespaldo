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
    public class PaisCollection : List<Pais>
    {
        public PaisCollection()
        {
        }

        public PaisCollection(string xml)
        {
            XmlSerializer serializador = new XmlSerializer(typeof(PaisCollection));
            StringReader reader = new StringReader(xml);

            PaisCollection list = (PaisCollection)serializador.Deserialize(reader);

            this.AddRange(list);
        }


        //metodo que lee todos los Paises
        public PaisCollection ReadAll()
        {
            var listaDalc = new EntitiesCEM().PAIS;

            return GenerarListado(listaDalc.ToList());
        }

        //Metodo que agrega un Pais a la lista y luego retorna la lista de Paises
        private PaisCollection GenerarListado(List<DALC.Portafolio.PAIS> listaDALC)
        {
            PaisCollection listaBC = new PaisCollection();

            foreach (var item in listaDALC)
            {
                Pais pais = new Pais();

                pais.IdPais = item.ID_PAIS;
                pais.NombrePais = item.NOM_PAIS;
                pais.Sigla= item.SIGLA;
                
                listaBC.Add(pais);
            }
            return listaBC;
        }

        //metodo que serializa la coleccion de Paises
        public string Serializar()
        {
            XmlSerializer serializar = new XmlSerializer(typeof(PaisCollection));
            StringWriter writer = new StringWriter();

            serializar.Serialize(writer, this);

            writer.Close();
            return writer.ToString();
        }

    }
}
