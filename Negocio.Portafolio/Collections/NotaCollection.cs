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
    public class NotaCollection : List<Nota>
    {
        public NotaCollection()
        {
        }

        public NotaCollection(string json)
        {
            //XmlSerializer serializador = new XmlSerializer(typeof(NotaCollection));
            //StringReader reader = new StringReader(xml);

            NotaCollection list = JsonConvert.DeserializeObject<NotaCollection>(json);

            this.AddRange(list);
        }

        //metodo que lee todas las notas
        public NotaCollection ReadAll()
        {
            var listaDalc = new EntitiesCEM().NOTAS;

            return GenerarListado(listaDalc.ToList());
        }

        public NotaCollection NotasPorAlumno(int idAlumno)
        {
            var listaDalc = new EntitiesCEM().NOTAS;

            return GenerarListado(listaDalc.Where(nota => nota.ID_ALUMNO == idAlumno).ToList());
        }

        //metodo que lee las notas de un alumno
        public NotaCollection NotasAlumno(string xml)
        {
            EntitiesCEM ctx = new EntitiesCEM();

            Nota _nota = new Nota(xml);
 
            var listaDalc = ctx.NOTAS.Where(n => /*n.ID_PROGRAMA == _nota.IdPrograma &&*/ n.ID_ALUMNO == _nota.IdAlumno);
                        
            return GenerarListado(listaDalc.ToList());
        }

        //Metodo que agrega una Nota a la lista y luego retorna la lista de Notas
        private NotaCollection GenerarListado(List<DALC.Portafolio.NOTAS> listaDALC)
        {
            NotaCollection listaBC = new NotaCollection();

            foreach (var item in listaDALC)
            {
                Nota nota = new Nota();

                nota.IdNota = item.ID_NOTA;
                nota.Evaluacion = item.NOTA;
                nota.IdPrograma = item.ID_PROGRAMA;
                nota.IdAlumno = item.ID_ALUMNO;
                
                listaBC.Add(nota);
            }
            return listaBC;
        }

        //metodo que serializa la coleccion de Notas 
        public string Serializar()
        {
            return JsonConvert.SerializeObject(this);
            //XmlSerializer serializar = new XmlSerializer(typeof(NotaCollection));
            //StringWriter writer = new StringWriter();

            //serializar.Serialize(writer, this);

            //writer.Close();
            //return writer.ToString();
        }
    }
}
