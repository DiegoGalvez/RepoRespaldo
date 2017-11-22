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
    public class AdministrativoCollection : List<Administrativo>
    {
        public AdministrativoCollection()
        {
        }

        public AdministrativoCollection(string json)
        {
            //XmlSerializer serializador = new XmlSerializer(typeof(AdministrativoCollection));
            //StringReader reader = new StringReader(xml);

            AdministrativoCollection list = JsonConvert.DeserializeObject<AdministrativoCollection>(json);

            this.AddRange(list);
        }


        //metodo que lee todos los administrativo
        public AdministrativoCollection ReadAll()
        {
            var listaDalc = new EntitiesCEM().ADMINISTRATIVO;

            return GenerarListado(listaDalc.ToList());
        }

        //Metodo que agrega un Administrativo a la lista y luego retorna la lista de administrativo 
        private AdministrativoCollection GenerarListado(List<DALC.Portafolio.ADMINISTRATIVO> listaDALC)
        {
            AdministrativoCollection listaBC = new AdministrativoCollection();

            foreach (var item in listaDALC)
            {
                Administrativo administrativo = new Administrativo();

                administrativo.IdAdministrativo = item.ID_ADMINISTRATIVO;
                administrativo.Nombres = item.NOMBRE;
                administrativo.ApePaterno = item.APELL_PATERNO;
                administrativo.ApeMaterno = item.APELL_MATERNO;
                administrativo.Correo = item.CORREO;
                administrativo.IdCargo = (int)item.ID_CARGO;
                
                listaBC.Add(administrativo);
            }
            return listaBC;
        }

        //metodo que serializa la coleccion de administrativos 
        public string Serializar()
        {
            return JsonConvert.SerializeObject(this);
            //XmlSerializer serializar = new XmlSerializer(typeof(AdministrativoCollection));
            //StringWriter writer = new StringWriter();

            //serializar.Serialize(writer, this);

            //writer.Close();
            //return writer.ToString();
        }

    }
}
