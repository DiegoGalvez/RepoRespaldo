using DALC.Portafolio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Negocio.Portafolio.ViewEntities
{
    public class VNotasProgramaAlumnoCollection : List<VNotasProgramaAlumno>
    {
        public VNotasProgramaAlumnoCollection()
        {
        }

        public VNotasProgramaAlumnoCollection(string xml)
        {
            XmlSerializer serializador = new XmlSerializer(typeof(VNotasProgramaAlumnoCollection));
            StringReader reader = new StringReader(xml);

            VNotasProgramaAlumnoCollection list = (VNotasProgramaAlumnoCollection)serializador.Deserialize(reader);

            this.AddRange(list);
        }


        //Metodo que obtiene los programas y notas de un alumno
        public VNotasProgramaAlumnoCollection ObtenerNotasProgramas(int idAlumno)
        {
            VNotasProgramaAlumnoCollection vNotasProgramaCollection = new VNotasProgramaAlumnoCollection();

            EntitiesCEM ctx = new EntitiesCEM();

            var notasProgramaAlumno = (from a in ctx.ALUMNOS
                          join n in ctx.NOTAS
                          on a.ID_ALUMNO equals n.ID_ALUMNO
                          join p in ctx.PROGRAMAS
                          on n.ID_PROGRAMA equals p.ID_PROGRAMA
                          where a.ID_ALUMNO == idAlumno
                          orderby p.ID_PROGRAMA
                          select new
                          {
                              Programa = p.NOMBRE_PROGRAMA ,
                              Nota = n.NOTA
                          }).ToList();

            foreach (var item in notasProgramaAlumno)
            {
                VNotasProgramaAlumno vNotasPrograma = new VNotasProgramaAlumno();

                
                vNotasPrograma.Nota = (double)item.Nota;
                vNotasPrograma.Programa = item.Programa;

                vNotasProgramaCollection.Add(vNotasPrograma);
            }
            
            return vNotasProgramaCollection;
        }
        
        //metodo que serializa la coleccion de Notas de Programas
        public string Serializar()
        {
            XmlSerializer serializar = new XmlSerializer(typeof(VNotasProgramaAlumnoCollection));
            StringWriter writer = new StringWriter();

            serializar.Serialize(writer, this);

            writer.Close();
            return writer.ToString();
        }
    }
}
