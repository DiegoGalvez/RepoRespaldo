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
    public class Nota : IPersistente
    {
        private decimal _idNota;
        private Nullable<decimal> _evaluacion;
        private int _idActividad;
        private int _idAlumno;

        public bool Read()
        {
            try
            {
                EntitiesCEM ctx = new DALC.Portafolio.EntitiesCEM();
                //Busca si existe la NOTA segun su id y asigna los valores a un obj NOTAS ENTITY
                NOTAS _nota = ctx.NOTAS.First(i => i.ID_NOTA == _idNota);
                //Asigna los valores de obj del NOTAS Entity al obj Nota de la Clase
                this.Evaluacion = _nota.NOTA;
                this.IdPrograma = _nota.ID_PROGRAMA;
                this.IdAlumno = _nota.ID_ALUMNO;

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
                //Busca si existe el Intercambio segun su id 
                if (ctx.NOTAS.Any(i => i.ID_NOTA == IdNota))
                {
                    //Llama al procedimiento DELETE en la tabla INTERCAMBIO
                    ctx.DEL_NOTAS(IdNota);
                    ctx.SaveChanges();
                    ctx = null;

                    return true;
                }
                return true; //deveria ser false cuando se arregle el tema de las notas
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
                //Busca si existe el INTERCAMBIO segun su id
                if (ctx.NOTAS.Any(i => i.ID_NOTA == IdNota))
                {
                    //Llama al procedimiento UPDATE en la tabla INTERCAMBIO
                    ctx.UPD_NOTAS(Evaluacion,IdAlumno, IdNota, IdPrograma );
                    ctx.SaveChanges();
                    ctx = null;

                    return true;
                }
                return true; //deberia ser false cuando este listo
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
                //Llama al procedimiento INSERT en la tabla INS_INTERCAMBIO
                ctx.INS_NOTAS(Evaluacion, IdAlumno, IdNota, IdPrograma);
                ctx.SaveChanges();
                ctx = null;
                
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public Nota(string json)
        {
            //XmlSerializer serializiador = new XmlSerializer(typeof(Nota));
            //StringReader reader = new StringReader(xml);

            Nota nota = JsonConvert.DeserializeObject<Nota>(json);
            this.IdNota = nota.IdNota;
            this.Evaluacion = nota.Evaluacion;
            this.IdPrograma = nota.IdPrograma;
            this.IdAlumno = nota.IdAlumno;
        }
        public string Serializar()
        {

            return JsonConvert.SerializeObject(this);
            //XmlSerializer serializador = new XmlSerializer(typeof(Nota));
            //StringWriter writer = new StringWriter();

            //serializador.Serialize(writer, this);
            //writer.Close();

            //return writer.ToString();
        }
        public Nota()
        {
            this.Init();
        }
        public void Init()
        {
            this.IdNota = 0;
            this.Evaluacion = 0;
            this.IdPrograma = 0;
            this.IdAlumno = 0;
        }

        public Nullable<decimal> Evaluacion
        {
            get { return _evaluacion; }
            set { _evaluacion = value; }
        } 
        public int IdPrograma
        {
            get { return _idActividad; }
            set { _idActividad = value; }
        }
        public int IdAlumno
        {
            get { return _idAlumno; }
            set { _idAlumno = value; }
        }

        public decimal IdNota
        {
            get { return _idNota; }
            set { _idNota = value; }
        }
    }
}
