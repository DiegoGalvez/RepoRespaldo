using DALC.Portafolio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

using Newtonsoft.Json;

namespace Negocio.Portafolio
{
    public class Programa : IPersistente
    {
        private int _idPrograma;
        private string _nombrePrograma;
        private string _descripcion;
        private Nullable<int> _cupos;
        private Nullable<int> idInstitucion;
        private Nullable<DateTime> fechaInicio;
        private Nullable<DateTime> fechaTermino;
        private TipoCursos tipoCurso;
        private EstadoPrograma estado;
        //private DateTime fechaCreacionCurso;

        public int CurrentProgramaEntityID()
        {
            int id;
            try
            {
                EntitiesCEM ctx = new EntitiesCEM();

                id = ctx.Database.SqlQuery<int>("SELECT PROGRAMAS_ID_PROGRAMA_SEQ.CURRVAL FROM dual").FirstOrDefault();

                ctx = null;
                return id;
            }
            catch (Exception)
            {
                throw new Exception("No se pudo obtener id actual");
            }
        }

        public bool Read()
        {
            try
            {
                EntitiesCEM ctx = new DALC.Portafolio.EntitiesCEM();
                //Busca si existe el Programa segun su id y asigna los valores a un obj PROGRAMAS ENTITY
                PROGRAMAS _pais = ctx.PROGRAMAS.First(p =>p.ID_PROGRAMA == IdPrograma);
                //Asigna los valores de obj del PROGRAMAS Entity al obj Programa de la Clase
                this.IdPrograma = _pais.ID_PROGRAMA;
                this.NombrePrograma = _pais.NOMBRE_PROGRAMA;
                this.Descripcion= _pais.DESCRIPCION;
                this.Cupos = _pais.CUPOS;
                this.fechaInicio = _pais.FECHA_INICIO;
                this.fechaTermino = _pais.FECHA_TERMINO;
                this.tipoCurso = (TipoCursos)Enum.Parse(typeof (TipoCursos),_pais.TIPO_CURSO);
                this.estado = (EstadoPrograma)Enum.Parse(typeof (EstadoPrograma), _pais.ESTADO);
                this.idInstitucion = _pais.ID_INSTITUCION;

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
                //Busca si existe el Programa segun su id 
                if (ctx.PROGRAMAS.Any(p => p.ID_PROGRAMA == IdPrograma))
                {
                    //Llama al procedimiento DELETE en la tabla PROGRAMAS
                    ctx.DEL_PROGRAMAS(IdPrograma);
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
                //Busca si existe el PROGRAMAS segun su id
                if (ctx.PROGRAMAS.Any(p => p.ID_PROGRAMA == IdPrograma))
                {
                    //Llama al procedimiento UPDATE en la tabla PROGRAMAS
                    ctx.UPD_PROGRAMAS(IdPrograma, NombrePrograma, Descripcion, Cupos, idInstitucion, FechaInicio, FechaTermino, TipoCurso.ToString(), estado.ToString());
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
                //Llama al procedimiento INSERT en la tabla PROGRAMAS
                //FechaTermino, IdPrograma, Cupos, TipoCurso.ToString(), Descripcion, FechaInicio, NombrePrograma);
                ctx.INS_PROGRAMAS(NombrePrograma, Descripcion, Cupos, idInstitucion, fechaInicio, fechaTermino, tipoCurso.ToString(), estado.ToString());
                ctx.SaveChanges();
                ctx = null;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public Programa(string json)
        {
            //XmlSerializer serializer = new XmlSerializer(typeof(Programa));
            //StringReader reader = new StringReader(xml);

            Programa programa = JsonConvert.DeserializeObject<Programa>(json);

            this.IdPrograma = programa.IdPrograma;
            this.NombrePrograma = programa.NombrePrograma;
            this.Descripcion = programa.Descripcion;
            this.Cupos = programa.Cupos;
            this.idInstitucion = programa.IdInstitucion;
            this.FechaInicio = programa.FechaInicio;
            this.FechaTermino = programa.FechaTermino;
            this.TipoCurso = programa.TipoCurso;
            this.estado = programa.estado;
        }
        public string Serializar()
        {
            return JsonConvert.SerializeObject(this);
            //XmlSerializer serializador = new XmlSerializer(typeof(Programa));
            //StringWriter writer = new StringWriter();

            //serializador.Serialize(writer, this);
            //writer.Close();

            //return writer.ToString();
        }
        public Programa()
        {
            this.Init();
        }
        private void Init()
        {
            this.IdPrograma = 0;
            this.NombrePrograma = string.Empty;
            this.Cupos = 0;
            this.IdInstitucion = 0;
            this.fechaInicio = DateTime.Now;
            this.FechaTermino = DateTime.Now;
            this.tipoCurso = TipoCursos.Normal;
            this.estado = EstadoPrograma.Creado;
        }

        public int IdPrograma
        {
            get { return _idPrograma; } 
            set { _idPrograma = value; }
        }
        public string NombrePrograma
        {
            get { return _nombrePrograma; }
            set { _nombrePrograma = value; }
        }
        public Nullable<int> Cupos
        {
            get { return _cupos; }
            set { _cupos = value;}
        }
        
        public Nullable<int> IdInstitucion
        {
            get { return idInstitucion; }
            set { idInstitucion = value; }
        }
        public Nullable<DateTime> FechaInicio
        {
            get { return fechaInicio; } 
            set { fechaInicio = value; }
        }
        public Nullable<DateTime> FechaTermino
        {
            get { return fechaTermino; } 
            set { fechaTermino = value; }
        }
        public TipoCursos TipoCurso
        {
            get { return tipoCurso; } 
            set { tipoCurso = value; }
        }

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        public EstadoPrograma Estado
        {
            get { return estado; }
            set { estado = value; }
        }
    }
}
