using DALC.Portafolio;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Portafolio.ViewClasses.ProgramasFinalizados
{
    public class VProgramasPostuladosCollection : List<VProgramasPostulados>
    {
        public VProgramasPostuladosCollection() { }

        public VProgramasPostuladosCollection(string json)
        {
            VProgramasPostuladosCollection list = JsonConvert.DeserializeObject<VProgramasPostuladosCollection>(json);

            this.AddRange(list);
        }


        public VProgramasPostuladosCollection LeerProgramasFinalizados()
        {
            var listaVista = new EntitiesCEM().V_PROGRAMAS_POSTULADOS;
            return GenerarListado(listaVista.ToList());
        }


        private VProgramasPostuladosCollection GenerarListado(List<V_PROGRAMAS_POSTULADOS> listaDALC)
        {
            VProgramasPostuladosCollection listaBC = new VProgramasPostuladosCollection();

            foreach (var item in listaDALC)
            {
                VProgramasPostulados programa = new VProgramasPostulados();

                programa.IdPrograma = item.ID_PROGRAMA;
                programa.NombrePrograma = item.NOMBRE_PROGRAMA;
                programa.FechaInicio = item.FECHA_INICIO;
                programa.Institucion = item.Institucion;
                programa.NombreEncargado = item.Encargado_Cel;
                programa.NombreCiudad = item.NOM_CIUDAD;
                programa.NombrePais = item.NOM_PAIS;

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
