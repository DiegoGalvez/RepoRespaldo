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
    public class InstitucionCollection : List<Institucion>
    {
        public InstitucionCollection()
        {
        }

        public InstitucionCollection(string json)
        {
            //XmlSerializer serializador = new XmlSerializer(typeof(InstitucionCollection));
            //StringReader reader = new StringReader(xml);

            InstitucionCollection list = JsonConvert.DeserializeObject<InstitucionCollection>(json);

            this.AddRange(list);
        }


        //metodo que lee todas las instituciones
        public InstitucionCollection ReadAll()
        {
            var listaDalc = new EntitiesCEM().INSTITUCION;

            return GenerarListado(listaDalc.ToList());
        }

        //Metodo que agrega una Institucion a la lista y luego retorna la lista de Instituciones
        private InstitucionCollection GenerarListado(List<DALC.Portafolio.INSTITUCION> listaDALC)
        {
            InstitucionCollection lista = new InstitucionCollection();

            foreach (var item in listaDALC)
            {
                Institucion institucion = new Institucion();

                institucion.IdInstitucion = item.ID_INSTITUCION;
                institucion.Nombres= item.NOMBRE;
                institucion.Correo = item.CORREO;
                institucion.Telefono = (int)item.TELEFONO;
                institucion.PaginaWeb = item.PAGINA_WEB;
                institucion.Direcion = item.DIRECCION;
                institucion.IdCiudad = (int)item.ID_CIUDAD;
                institucion.IdPais = (int)item.ID_PAIS;
                
                lista.Add(institucion);
            }
            return lista;
        }

        //metodo que serializa la coleccion de Instituciones
        public string Serializar()
        {
            return JsonConvert.SerializeObject(this);
            //XmlSerializer serializar = new XmlSerializer(typeof(InstitucionCollection));
            //StringWriter writer = new StringWriter();

            //serializar.Serialize(writer, this);

            //writer.Close();
            //return writer.ToString();
        }

    }
}
