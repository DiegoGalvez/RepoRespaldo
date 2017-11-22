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
    public class ProgramaCollection : List<Programa>
    {
        public ProgramaCollection()
        {
        }

        public ProgramaCollection(string xml)
        {
            XmlSerializer serializador = new XmlSerializer(typeof(ProgramaCollection));
            StringReader reader = new StringReader(xml);

            ProgramaCollection list = (ProgramaCollection)serializador.Deserialize(reader);

            this.AddRange(list);
        }


        //metodo que lee todos los Programas
        public ProgramaCollection ReadAll()
        {
            var listaDalc = new EntitiesCEM().PROGRAMAS;

            return GenerarListado(listaDalc.ToList());
        }

        //Metodo que agrega un Programa a la lista y luego retorna la lista de Programas
        private ProgramaCollection GenerarListado(List<DALC.Portafolio.PROGRAMAS> listaDALC)
        {
            ProgramaCollection listaBC = new ProgramaCollection();

            foreach (var item in listaDALC)
            {
                Programa programa = new Programa();

                programa.IdPrograma= item.ID_PROGRAMA;
                programa.NombrePrograma = item.NOMBRE_PROGRAMA;
                programa.Descripcion = item.DESCRIPCION;
                programa.Cupos= item.CUPOS;
                programa.FechaInicio= item.FECHA_INICIO;
                programa.FechaTermino= item.FECHA_TERMINO;
                programa.TipoCurso = (TipoCursos)Enum.Parse(typeof (TipoCursos), item.TIPO_CURSO);
                
                listaBC.Add(programa);
            }
            return listaBC;
        }

        //metodo que serializa la coleccion de Programas
        public string Serializar()
        {
            XmlSerializer serializar = new XmlSerializer(typeof(ProgramaCollection));
            StringWriter writer = new StringWriter();

            serializar.Serialize(writer, this);

            writer.Close();
            return writer.ToString();
        }

    }
}
