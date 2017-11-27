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
    public class AlumnoCollection : List<Alumno>
    {
        public AlumnoCollection()
        {
        }

        public AlumnoCollection(string json)
        {
            //XmlSerializer serializador = new XmlSerializer(typeof(AlumnoCollection));
            //StringReader reader = new StringReader(xml);

            AlumnoCollection list = JsonConvert.DeserializeObject<AlumnoCollection>(json);

            this.AddRange(list);
        }


        //metodo que lee todos los alumnos
        public AlumnoCollection AlumnosProgramaFinalizado(int idPrograma)
        {
            EntitiesCEM ctx = new EntitiesCEM();
            var listaDalc = from a in ctx.ALUMNOS
                            join i in ctx.INTERCAMBIO on a.ID_ALUMNO equals i.ID_ALUMNO
                            join p in ctx.PROGRAMAS on i.ID_PROGRAMA equals p.ID_PROGRAMA
                            where p.ESTADO == "Finalizado" && p.ID_PROGRAMA == idPrograma && i.ESTADO == "Aprobado"
                            select a ;
                             

            return GenerarListado(listaDalc.ToList());
        }

        //metodo que lee todos los alumnos
        public AlumnoCollection ReadAll()
        {
            var listaDalc = new EntitiesCEM().ALUMNOS;

            return GenerarListado(listaDalc.ToList());
        }

        //metodo que lee todos los alumnos que contengan la FRASE en su nombre, apellido paterno o apellido materno
        public AlumnoCollection BuscarALumnosPorNombreCompleto(string frase)
        {
            frase = frase.ToUpper();

            var listaDalc = new EntitiesCEM().ALUMNOS;

            return GenerarListado(listaDalc.Where(alumno => alumno.NOMBRE.ToUpper().Contains(frase) || alumno.APELL_PATERNO.ToUpper().Contains(frase) || alumno.APELL_MATERNO.ToUpper().Contains(frase)).ToList());
        }

        //Metodo que agrega un alumno a la lista y luego retorna la lista de alumnos 
        private AlumnoCollection GenerarListado(List<DALC.Portafolio.ALUMNOS> listaDALC)
        {
            AlumnoCollection listaBC = new AlumnoCollection();

            foreach (var item in listaDALC)
            {
                Alumno alumno = new Alumno();

                alumno.IdAlumno = item.ID_ALUMNO;
                alumno.Dv = item.DV;
                alumno.Nombre= item.NOMBRE;
                alumno.ApePaterno = item.APELL_PATERNO;
                alumno.ApeMaterno = item.APELL_MATERNO;
                alumno.Correo = item.CORREO;
                alumno.Reserva = item.RESERVA;
                alumno.Telefono = (int)item.TELEFONO;
                alumno.EstadoMora = item.ESTADO_MORA;

                listaBC.Add(alumno);
            }
            return listaBC;
        }

        //metodo que serializa la coleccion de alumnos 
        public string Serializar()
        {
            return JsonConvert.SerializeObject(this);
            //XmlSerializer serializar = new XmlSerializer(typeof(AlumnoCollection));
            //StringWriter writer = new StringWriter();

            //serializar.Serialize(writer, this);

            //writer.Close();
            //return writer.ToString();
        }

    }
}
