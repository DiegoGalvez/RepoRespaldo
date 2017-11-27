using DALC.Portafolio;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Portafolio.ViewClasses.ProgramasFinalizados
{
    public class VProgramasFinalizadosCollection : List<VProgramasFinalizados>
    {
        public VProgramasFinalizadosCollection() { }

        public VProgramasFinalizadosCollection(string json)
        {
            VProgramasFinalizadosCollection list = JsonConvert.DeserializeObject<VProgramasFinalizadosCollection>(json);

            this.AddRange(list);
        }


        public VProgramasFinalizadosCollection LeerProgramasFinalizados()
        {
            var listaVista = new EntitiesCEM().VISTA_PROGRAMAS_FINALIZADOS;
            return GenerarListado(listaVista.ToList());
        }


        private VProgramasFinalizadosCollection GenerarListado(List<VISTA_PROGRAMAS_FINALIZADOS> listaDALC)
        {
            VProgramasFinalizadosCollection listaBC = new VProgramasFinalizadosCollection();

            foreach (var item in listaDALC)
            {
                VProgramasFinalizados programa = new VProgramasFinalizados();

                programa.IdPrograma = item.ID_PROGRAMA;
                programa.NombrePrograma = item.NOMBRE_PROGRAMA;
                programa.NombreCEL= item.NOMBRE_CEL;
                programa.NombreEncargadoCEL = item.NOMBRE_ENCARGADO_CEL;
                programa.Pais = item.NOM_PAIS;
                programa.Ciudad = item.NOM_CIUDAD;
                programa.FechaInicio = item.FECHA_INICIO;
                programa.FechaTermino = item.FECHA_TERMINO;

                listaBC.Add(programa);
            }
            return listaBC;
        }

        //metodo que serializa la coleccion de Notas de Programa 
        public string Serializar()
        {
            return JsonConvert.SerializeObject(this);
        }

    }
}
