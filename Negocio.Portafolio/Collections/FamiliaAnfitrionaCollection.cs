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
    public class FamiliaAnfitrionaCollection : List<FamiliaAnfitriona>
    {
        public FamiliaAnfitrionaCollection()
        {
        }

        public FamiliaAnfitrionaCollection(string json)
        {
            //XmlSerializer serializador = new XmlSerializer(typeof(FamiliaAnfitrionaCollection));
            //StringReader reader = new StringReader(xml);

            FamiliaAnfitrionaCollection list = JsonConvert.DeserializeObject<FamiliaAnfitrionaCollection>(json);

            this.AddRange(list);
        }


        //metodo que lee todos las FamiliasAnfitrionas
        public FamiliaAnfitrionaCollection ReadAll()
        {
            var listaDalc = new EntitiesCEM().FAMILIASANFITRIONA;

            return GenerarListado(listaDalc.ToList());
        }

        //Metodo que agrega una FamiliaAnfitriona a la lista y luego retorna la lista de FamiliasAnfitrionas
        private FamiliaAnfitrionaCollection GenerarListado(List<DALC.Portafolio.FAMILIASANFITRIONA> listaDALC)
        {
            FamiliaAnfitrionaCollection listaBC = new FamiliaAnfitrionaCollection();

            foreach (var item in listaDALC)
            {
                FamiliaAnfitriona familiaAnfitriona = new FamiliaAnfitriona();

                familiaAnfitriona.IdFamilia= item.ID_FAMILIA;
                familiaAnfitriona.Nombres= item.NOMBRE;
                familiaAnfitriona.ApePaterno= item.APELL_PATERNO;
                familiaAnfitriona.ApeMaterno = item.APELL_MATERNO;
                familiaAnfitriona.Identificador = item.IDENTIFICACION;
                familiaAnfitriona.Correo= item.CORREO;
                familiaAnfitriona.Telefono = (int)item.TELEFONO_CONTACTO;
                familiaAnfitriona.RutaArchivo = item.RUTA_ARCHIVO;
                familiaAnfitriona.Direccion = item.DIRECCION;
                familiaAnfitriona.IdCiudad = (int)item.ID_CIUDAD;
                familiaAnfitriona.IdPais = item.ID_PAIS;
                familiaAnfitriona.Estado = item.ESTADO;

                listaBC.Add(familiaAnfitriona);
            }
            return listaBC;
        }

        //metodo que serializa la coleccion de FamiliaAnfitrionas
        public string Serializar()
        {
            return JsonConvert.SerializeObject(this);
            //XmlSerializer serializar = new XmlSerializer(typeof(FamiliaAnfitrionaCollection));
            //StringWriter writer = new StringWriter();

            //serializar.Serialize(writer, this);

            //writer.Close();
            //return writer.ToString();
        }

        public FamiliaAnfitrionaCollection BuscarALumnosPorNombreCompleto(string nombreApellido)
        {
            nombreApellido = nombreApellido.ToUpper();

            var listaDalc = new EntitiesCEM().FAMILIASANFITRIONA;

            return GenerarListado(listaDalc.Where(f => f.NOMBRE.ToUpper().Contains(nombreApellido) || f.APELL_PATERNO.ToUpper().Contains(nombreApellido) || f.APELL_MATERNO.ToUpper().Contains(nombreApellido)).ToList());
        }
    }
}
