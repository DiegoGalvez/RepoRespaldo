using DALC.Portafolio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

using Newtonsoft.Json;

namespace Negocio.Portafolio.ViewEntities
{
    public class VNotasModificableCollection : List<VNotasModificable>
    {
        public VNotasModificableCollection()
        {
        }

        public VNotasModificableCollection(string json)
        {
            //XmlSerializer serializador = new XmlSerializer(typeof(VNotasProgramaAlumnoCollection));
            //StringReader reader = new StringReader(xml);

            VNotasModificableCollection list = JsonConvert.DeserializeObject<VNotasModificableCollection>(json);

            this.AddRange(list);
        }


        //Metodo que obtiene los programas y notas de un alumno
        public VNotasModificableCollection ObtenerNotasModificable(int idAlumno)
        {
            VNotasModificableCollection vNotasModificableCollection  = new VNotasModificableCollection();

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
                              IdNota = n.ID_NOTA,
                              Nota = n.NOTA,
                              Programa = p.NOMBRE_PROGRAMA
                          }).ToList();

            foreach (var item in notasProgramaAlumno)
            {
                VNotasModificable vNotasModificable = new VNotasModificable();

                vNotasModificable.IdNota= (int)item.IdNota;
                vNotasModificable.Nota = (double)item.Nota;
                vNotasModificable.Programa = item.Programa;

                vNotasModificableCollection.Add(vNotasModificable);
            }
            
            return vNotasModificableCollection;
        }
        
        //metodo que serializa la coleccion de Notas de Programas
        public string Serializar()
        {
            return JsonConvert.SerializeObject(this);
            //XmlSerializer serializar = new XmlSerializer(typeof(VNotasProgramaAlumnoCollection));
            //StringWriter writer = new StringWriter();

            //serializar.Serialize(writer, this);

            //writer.Close();
            //return writer.ToString();
        }
    }
}
