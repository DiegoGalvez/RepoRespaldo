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
    public class Alumno : IPersistente
    {
        private int _idAlumno;
        private string _dv;
        private string _nombres;
        private string _apePaterno; 
        private string _apeMaterno; 
        private string _correo;
        private Nullable<int> _reserva;
        private int _telefono;
        private string _estadoMora;

        public bool Read()
        {
            try
            {
                EntitiesCEM ctx = new DALC.Portafolio.EntitiesCEM();
                //Busca si existe el alumnos segun su id y asigna los valores a un obj ALUMNO ENTITY
                ALUMNOS _alumno = ctx.ALUMNOS.First(a => a.ID_ALUMNO == IdAlumno);
                //Asigna los valores de obj del Alumno Entity al obj Alumno de la Clase
                this.IdAlumno = _alumno.ID_ALUMNO;
                this.Dv = _alumno.DV;
                this.Nombre = _alumno.NOMBRE;
                this.ApePaterno = _alumno.APELL_PATERNO;
                this.ApeMaterno = _alumno.APELL_MATERNO;
                this.Correo = _alumno.CORREO;
                this.Reserva = _alumno.RESERVA;
                this.Telefono = (int)_alumno.TELEFONO;
                this.EstadoMora = _alumno.ESTADO_MORA;
                ctx = null;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Delete()
        {
            try
            {
                EntitiesCEM ctx = new EntitiesCEM();
                //Busca si existe el alumnos segun su id
                if (ctx.ALUMNOS.Any(a => a.ID_ALUMNO == IdAlumno))
                {
                    //Llama al procedimiento DELETE de la tabla ALUMNOS
                    ctx.DEL_ALUMNOS(IdAlumno);
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
                //Busca si existe el alumnos segun su id
                if (ctx.ALUMNOS.Any(a => a.ID_ALUMNO == IdAlumno))
                {
                    //Llama al procedimiento UPDATE de la tabla ALUMNOS
                    ctx.UPD_ALUMNOS(IdAlumno, Dv, Nombre, ApePaterno, ApeMaterno, Correo, Reserva, Telefono, EstadoMora);
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
                if (ctx.ALUMNOS.Any(a => a.ID_ALUMNO == IdAlumno))
                {
                    return false;
                }
                else
                {
                    //Llama al procedimiento CREATE de la tabla ALUMNOS
                    ctx.INS_ALUMNOS(Dv, Nombre, ApePaterno, ApeMaterno, Correo, Reserva, Telefono, EstadoMora);
                    ctx.SaveChanges();
                    ctx = null;
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public string Serializar()
        {

            return JsonConvert.SerializeObject(this);
            //XmlSerializer serializador = new XmlSerializer(typeof(Alumno));
            //StringWriter writer = new StringWriter();

            //serializador.Serialize(writer, this);
            //writer.Close();

            //return writer.ToString();
        }
        public Alumno(string json)
        {
            /* Aplica serializiación para obtener las propiedades*/
            //XmlSerializer serializer = new XmlSerializer(typeof(Alumno));
            //StringReader reader = new StringReader(xml);

            Alumno _alumno = JsonConvert.DeserializeObject<Alumno>(json);

            this.IdAlumno = _alumno.IdAlumno;
            this.Dv = _alumno.Dv;
            this.Nombre = _alumno.Nombre;
            this.ApePaterno = _alumno.ApePaterno;
            this.ApeMaterno = _alumno.ApeMaterno;
            this.Correo = _alumno.Correo;
            this.Reserva = _alumno.Reserva;
            this.Telefono = _alumno.Telefono;
            this.EstadoMora = _alumno.EstadoMora;
        }
        public Alumno()
        {
            this.Init();
        }

        private void Init()
        {
            this.IdAlumno = 0;
            this.Dv = string.Empty;
            this.Nombre = string.Empty;
            this.ApePaterno = string.Empty;
            this.ApeMaterno = string.Empty;
            this.Correo = string.Empty;
            this.Reserva = 0;
            this.Telefono = 0;
            this.EstadoMora = string.Empty;
        }

        public int IdAlumno
        {
            get { return _idAlumno; }
            set { _idAlumno = value; }
        }
        public string Dv
        {
            get { return _dv; }
            set { _dv = value; }
        }
        public string Nombre
        {
            get { return _nombres; }
            set { _nombres = value; }
        }
        public string ApePaterno
        {
            get { return _apePaterno; }
            set { _apePaterno = value; }
        }
        public string ApeMaterno
        {
            get { return _apeMaterno;  }
            set { _apeMaterno = value;  }
        }
        public string Correo
        {
            get {return _correo; }
            set { _correo = value; }
        }
        public Nullable<int> Reserva
        {
            get { return _reserva; }
            set { _reserva = value; }
        }
        public int Telefono
        {
            get { return _telefono; }
            set { _telefono = value; }
        }
        public string EstadoMora
        {
            get { return _estadoMora; }
            set { _estadoMora = value; }
        }

        public string NombreCompleto()
        {
            return string.Format("{0} {1} {2}",Nombre, ApePaterno, ApeMaterno);
        }
    }
}
