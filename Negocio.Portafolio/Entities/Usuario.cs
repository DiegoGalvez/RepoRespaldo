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
    public class Usuario : IPersistente
    {
        private int _idUsuario;
        private string _nombreUsuario;
        private string _password;
        private string _rol;
        private Nullable<int> _idAlumno;
        private Nullable<int> _idAdministrativo;
        private Nullable<int> _idFamilia;
        private Nullable<int> _idEncargadoCel;
        private Nullable<int> _idEncargadoCem;

        //Metodo que lee un Usuario
        public bool Read()
        {
            try
            {
                EntitiesCEM ctx = new DALC.Portafolio.EntitiesCEM();
                //Busca si existe el Usuario segun su id y asigna los valores a un obj USUARIO ENTITY
                USUARIO _usuario = ctx.USUARIO.First(u => u.NOMBRE_USUARIO == NomUsuario);
                //Asigna los valores de obj del USUARIO Entity al obj Usuario de la Clase
                this.IdUsuario = _usuario.ID_USUARIO;
                this.NomUsuario = _usuario.NOMBRE_USUARIO;
                this.Password = _usuario.PASSWORD;
                this.Rol = _usuario.ROL;
                this.IdAlumno = _usuario.ID_ALUMNO;
                this.IdAdministrativo = _usuario.ID_ADMINISTRATIVO;
                this.IdFamilia = _usuario.ID_FAMILIA;
                this.IdEncargadoCel = _usuario.ID_ENCARGADO_CEL;
                this.IdEncargadoCem = _usuario.ID_ENCARGADO_CEM;

                ctx = null;

                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        public bool Delete()
        {
            try
            {
                EntitiesCEM ctx = new EntitiesCEM();
                //Busca si existe el Usuario segun su id 
                if (ctx.USUARIO.Any(u => u.ID_USUARIO == IdUsuario))
                {
                    //Llama al procedimiento DELETE en la tabla Usuario
                    ctx.DEL_USUARIO(IdUsuario);
                    ctx.SaveChanges();
                    ctx = null;

                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Update()
        {
            try
            {
                EntitiesCEM ctx = new EntitiesCEM();
                //Busca si existe el USUARIO segun su id
                if (ctx.USUARIO.Any(u => u.ID_USUARIO == IdUsuario))
                {
                    //Llama al procedimiento UPDATE en la tabla USUARIO
                    ctx.UPD_USUARIO(IdUsuario, Password, IdFamilia, IdAdministrativo, IdEncargadoCel, NomUsuario, IdEncargadoCem, IdAlumno, Rol);
                    ctx.SaveChanges();
                    ctx = null;

                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Create()
        {
            try
            {
                EntitiesCEM ctx = new EntitiesCEM();
                //Llama al procedimiento INSERT en la tabla USUARIO
                ctx.INS_USUARIO(IdUsuario, Password, IdFamilia, IdAdministrativo, IdEncargadoCel, NomUsuario, IdEncargadoCem, IdAlumno, Rol);
                ctx.SaveChanges();
                ctx = null;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        //Metodo que valida el usuario al iniciar sesion
        public bool ValidarUsuario(string userPass)
        {
            Usuario usuario = new Usuario(userPass);
            Usuario user = new Usuario(userPass);
            if (usuario.Read())
            {
                if (usuario.Password == user.Password)
                {
                    return true;
                }
            }
            return false;
        }
        //Metodo para seralizar un usuario
        public string Serializar()
        {
            XmlSerializer serializador = new XmlSerializer(typeof(Usuario));
            StringWriter writer = new StringWriter();

            serializador.Serialize(writer, this);
            writer.Close();

            return writer.ToString();
        }
        //Metodo que permiet serializar un usuario
        public Usuario(string xml)
        {
            /* Aplica serializiación para obtener las propiedades*/
            XmlSerializer serializiador = new XmlSerializer(typeof(Usuario));
            StringReader reader = new StringReader(xml);

            Usuario usuario = (Usuario)serializiador.Deserialize(reader);

            this.IdUsuario = usuario.IdUsuario;
            this.NomUsuario = usuario.NomUsuario;
            this.Password = usuario.Password;
            this.Rol = usuario.Rol;
            this.IdAlumno = usuario.IdAlumno;
            this.IdAdministrativo = usuario.IdAdministrativo;
            this.IdFamilia = usuario.IdFamilia;
            this.IdEncargadoCel = usuario.IdEncargadoCel;
            this.IdEncargadoCem = usuario.IdEncargadoCem;
        }
        // Constructor por defecto
        public Usuario()
        {
            this.Init();
        }

        private void Init()
        {
            this.IdUsuario = 0;
            this.NomUsuario = string.Empty;
            this.Password = string.Empty;
            this.Rol = string.Empty;
            this.IdAlumno = 0;
            this.IdAdministrativo = 0;
            this.IdFamilia = 0;
            this.IdEncargadoCel = 0;
            this.IdEncargadoCem = 0;
        }

        public int IdUsuario
        {
            get { return _idUsuario; }
            set { _idUsuario = value; }
        }
        public string NomUsuario
        {
            get { return _nombreUsuario; }
            set { _nombreUsuario = value; }
        }
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
        public Nullable<int> IdAlumno
        {
            get { return _idAlumno; }
            set { _idAlumno = value; }
        }
        public Nullable<int> IdAdministrativo
        {
            get { return _idAdministrativo; }
            set { _idAdministrativo = value; }
        }
        public Nullable<int> IdFamilia
        {
            get { return _idFamilia; }
            set { _idFamilia = value; }
        }
        public Nullable<int> IdEncargadoCel
        {
            get { return _idEncargadoCel; }
            set { _idEncargadoCel = value; }
        }

        public Nullable<int> IdEncargadoCem
        {
            get { return _idEncargadoCem; }
            set { _idEncargadoCem = value; }
        }

        public string Rol
        {
            get { return _rol; }
            set { _rol = value; }
        }
    }
}
