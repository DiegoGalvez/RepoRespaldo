using DALC.Portafolio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Negocio.Portafolio.ViewClasses.FamiliaPostulantes
{
    public class VFamiliasPostulantesCollection : List<VFamiliasPostulantes>
    {
        public VFamiliasPostulantesCollection() { }
        public VFamiliasPostulantesCollection(string xml)
        {
            XmlSerializer serializador = new XmlSerializer(typeof(VFamiliasPostulantesCollection));
            StringReader reader = new StringReader(xml);

            VFamiliasPostulantesCollection list = (VFamiliasPostulantesCollection)serializador.Deserialize(reader);

            this.AddRange(list);
        }


        //Metodo que obtiene los programas y notas de un alumno
        public VFamiliasPostulantesCollection LeerTodoVistaPostulantes()
        {
            var listaVista = new EntitiesCEM().V_FAMILIA_ANFITRIONA;

            return GenerarListado(listaVista.ToList());
        }


        private VFamiliasPostulantesCollection GenerarListado(List<V_FAMILIA_ANFITRIONA> listaDALC)
        {
            VFamiliasPostulantesCollection listaBC = new VFamiliasPostulantesCollection();

            foreach (var item in listaDALC)
            {
                VFamiliasPostulantes familiaPostulante = new VFamiliasPostulantes();

                familiaPostulante.Nombre = item.Nombre;
                familiaPostulante.Identificacion = item.IDENTIFICACION;
                familiaPostulante.Telefono = item.TELEFONO_CONTACTO;
                familiaPostulante.Correo = item.CORREO;
                familiaPostulante.Pais = item.NOM_PAIS;
                familiaPostulante.Ciudad = item.NOM_CIUDAD;
                familiaPostulante.Estado = item.ESTADO;

                listaBC.Add(familiaPostulante);
            }
            return listaBC;
        }

        //metodo que serializa la coleccion de Notas de Programa 
        public string Serializar()
        {
            XmlSerializer serializar = new XmlSerializer(typeof(VFamiliasPostulantesCollection));
            StringWriter writer = new StringWriter();

            serializar.Serialize(writer, this);

            writer.Close();
            return writer.ToString();
        }

        public VFamiliasPostulantesCollection BuscarFamiliaNombreApellido(string frase)
        {
            frase = frase.ToUpper();

            var listaDalc = new EntitiesCEM().V_FAMILIA_ANFITRIONA;

            if (frase.Equals("") || frase.Equals(string.Empty))
            {
                return GenerarListado(listaDalc.ToList());
            }
            else
            {
                return GenerarListado(listaDalc.Where(familia => familia.Nombre.ToUpper().Contains(frase)).ToList());
            }

        }
    }


   
}
