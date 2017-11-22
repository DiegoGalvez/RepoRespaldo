using DALC.Portafolio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Negocio.Portafolio.ViewClasses.NotasProgramaAlumno
{
    public class VAlumnosPostulantesCollection : List<VAlumnosPostulantes>
    {
        public VAlumnosPostulantesCollection()
        {
        }

        public VAlumnosPostulantesCollection(string xml)
        {
            XmlSerializer serializador = new XmlSerializer(typeof(VAlumnosPostulantesCollection));
            StringReader reader = new StringReader(xml);

            VAlumnosPostulantesCollection list = (VAlumnosPostulantesCollection)serializador.Deserialize(reader);

            this.AddRange(list);
        }


        //Metodo que obtiene los programas y notas de un alumno
        public VAlumnosPostulantesCollection LeerTodoVistaPostulantes()
        {
            var listaVista = new EntitiesCEM().VISTA_ALUMNOS_POSTULANTES;
            
            return GenerarListado(listaVista.ToList());
        }
        

        private VAlumnosPostulantesCollection GenerarListado(List<VISTA_ALUMNOS_POSTULANTES> listaDALC)
        {
            VAlumnosPostulantesCollection listaBC = new VAlumnosPostulantesCollection();

            foreach (var item in listaDALC)
            {
                VAlumnosPostulantes alumnosPostulantes = new VAlumnosPostulantes();

                alumnosPostulantes.Id_Alumnos = item.ID_ALUMNO;
                alumnosPostulantes.Nombre_Alumno = item.NOMBRE;
                alumnosPostulantes.Nombre_Programa = item.NOMBRE_PROGRAMA;
                alumnosPostulantes.Nombre_Familia= item.JEFE_FAMILIA_ANFITRIONA;
                alumnosPostulantes.Cupos = item.CUPOS;
                alumnosPostulantes.Fecha_Inicio = item.FECHA_INICIO;
                alumnosPostulantes.Estado = item.ESTADO;

                listaBC.Add(alumnosPostulantes);
            }
            return listaBC;
        }

        //metodo que serializa la coleccion de Notas de Programa 
        public string Serializar()
        {
            XmlSerializer serializar = new XmlSerializer(typeof(VAlumnosPostulantesCollection));
            StringWriter writer = new StringWriter();

            serializar.Serialize(writer, this);

            writer.Close();
            return writer.ToString();
        }

        public VAlumnosPostulantesCollection BuscarPorNombreApellido (string frase)
        {
            frase = frase.ToUpper();

            var listaDalc = new EntitiesCEM().VISTA_ALUMNOS_POSTULANTES;

            return GenerarListado(listaDalc.Where(alumno => alumno.NOMBRE.ToUpper().Contains(frase)).ToList());
        }
    }
}
