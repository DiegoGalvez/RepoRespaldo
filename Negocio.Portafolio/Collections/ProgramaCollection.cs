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
    public class ProgramaCollection : List<Programa>
    {
        public ProgramaCollection()
        {
        }

        public ProgramaCollection(string json)
        {
            //XmlSerializer serializador = new XmlSerializer(typeof(ProgramaCollection));
            //StringReader reader = new StringReader(xml);

            ProgramaCollection list = JsonConvert.DeserializeObject<ProgramaCollection>(json);

            this.AddRange(list);
        }

        public ProgramaCollection ProgramasPublicados()
        {
            var listaDalc = new EntitiesCEM().PROGRAMAS;

            return GenerarListado(listaDalc.Where(programa => programa.ESTADO == "Publicado").ToList());
        }

        public ProgramaCollection BuscarProgramasPublicadosPorNombre(string frase)
        {
            frase = frase.ToUpper();

            var listaDalc = new EntitiesCEM().PROGRAMAS;

            return GenerarListado(listaDalc.Where(programa => programa.ESTADO == "Publicado" && programa.NOMBRE_PROGRAMA.ToUpper().Contains(frase)).ToList());
        }

        public ProgramaCollection BuscarProgramasPorNombre(string frase)
        {
            frase = frase.ToUpper();

            var listaDalc = new EntitiesCEM().PROGRAMAS;

            return GenerarListado(listaDalc.Where(programa => programa.NOMBRE_PROGRAMA.ToUpper().Contains(frase)).ToList());
        }

        public ProgramaCollection ProgramasPublicadosPorInstitucion(int idInstitucion)
        {
            var listaDalc = new EntitiesCEM().PROGRAMAS;

            return GenerarListado(listaDalc.Where(programa => programa.ESTADO == "Publicado" && programa.ID_INSTITUCION == idInstitucion).ToList());
        }

        public ProgramaCollection BuscarProgramasPublicadosPorInstitucionYNombre(int idInstitucion, string frase)
        {
            frase = frase.ToUpper();

            var listaDalc = new EntitiesCEM().PROGRAMAS;

            return GenerarListado(listaDalc.Where(programa => programa.ESTADO == "Publicado" && programa.ID_INSTITUCION == idInstitucion && programa.NOMBRE_PROGRAMA.ToUpper().Contains(frase)).ToList());
        }

        //Metodo que busca los programas correspondientes al CEL que esta asociado el Usuario
        public ProgramaCollection BuscarProgramasFinalizadosCEL(int idCEL)
        {
            EntitiesCEM ctx = new EntitiesCEM();
            var listaDalc = from p in ctx.PROGRAMAS where p.ID_INSTITUCION == idCEL select p;
            return GenerarListado(listaDalc.ToList());
        }

        //Metodo que busca todos los programas finalizados
        public ProgramaCollection BuscarProgramasFinalizados()
        {
            EntitiesCEM ctx = new EntitiesCEM();
            var listaDalc = from p in ctx.PROGRAMAS where p.ESTADO.Equals("Finalizado") select p;
            return GenerarListado(listaDalc.ToList());
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
                programa.IdInstitucion = item.ID_INSTITUCION;
                programa.FechaInicio= item.FECHA_INICIO;
                programa.FechaTermino= item.FECHA_TERMINO;
                programa.TipoCurso = (TipoCursos)Enum.Parse(typeof (TipoCursos), item.TIPO_CURSO);
                programa.Estado = (EstadoPrograma)Enum.Parse(typeof(EstadoPrograma), item.ESTADO);

                listaBC.Add(programa);
            }
            return listaBC;
        }

        //metodo que serializa la coleccion de Programas
        public string Serializar()
        {
            return JsonConvert.SerializeObject(this);
            //XmlSerializer serializar = new XmlSerializer(typeof(ProgramaCollection));
            //StringWriter writer = new StringWriter();

            //serializar.Serialize(writer, this);

            //writer.Close();
            //return writer.ToString();
        }
        
    }
}
