using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DALC.Portafolio;

namespace Negocio.Portafolio
{
    public class UsuarioCollection : List<Usuario>
    {
        public UsuarioCollection()
        {
        }

        public UsuarioCollection(string xml)
        {
            XmlSerializer serializador = new XmlSerializer(typeof(UsuarioCollection));
            StringReader reader = new StringReader(xml);

            UsuarioCollection list = (UsuarioCollection)serializador.Deserialize(reader);

            this.AddRange(list);
        }


        //metodo que lee todos los Usuarios
        public UsuarioCollection ReadAll()
        {
            var listaDalc = new EntitiesCEM().USUARIO;

            return GenerarListado(listaDalc.ToList());
        }

        //Metodo que agrega un Usuario a la lista y luego retorna la lista de Usuarios
        private UsuarioCollection GenerarListado(List<DALC.Portafolio.USUARIO> listaDALC)
        {
            UsuarioCollection listaBC = new UsuarioCollection();

            foreach (var item in listaDALC)
            {
                Usuario usuario = new Usuario();

                usuario.IdUsuario = item.ID_USUARIO;
                usuario.NomUsuario = item.NOMBRE_USUARIO;
                usuario.Password = item.PASSWORD;
                usuario.IdAlumno = item.ID_ALUMNO.GetValueOrDefault();
                usuario.IdAdministrativo = item.ID_ADMINISTRATIVO.GetValueOrDefault();
                usuario.IdFamilia = item.ID_FAMILIA.GetValueOrDefault();                    ;
                usuario.IdEncargadoCel = item.ID_ENCARGADO_CEL.GetValueOrDefault();
                usuario.IdEncargadoCem = item.ID_ENCARGADO_CEM.GetValueOrDefault();

                listaBC.Add(usuario);
            }
            return listaBC;
        }

        //metodo que serializa la coleccion de Usuarios
        public string Serializar()
        {
            XmlSerializer serializar = new XmlSerializer(typeof(UsuarioCollection));
            StringWriter writer = new StringWriter();

            serializar.Serialize(writer, this);

            writer.Close();
            return writer.ToString();
        }

    }
}
