using DALC.Portafolio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Negocio.Portafolio.ViewClasses.AlumnosPostulantes
{
    public class VDetallePostulacion
    {
        public string NombrePostulante { get; set; }
        public int IdPostulante { get; set; }
        public string CorreoPostulante { get; set; }
        public long TelefonoPostulante { get; set; }
        public string EstadoMoraPostulante { get; set; }
        public string NombreFamilia { get; set; }
        public int IdFamilia { get; set; }
        public string DireccionFamilia { get; set; }
        public string CorreoFamilia { get; set; }
        public long TelefonoFamilia { get; set; }
        public int IdPrograma { get; set; }
        public string NombrePrograma { get; set; }
        public string DescripcionPrograma { get; set; }
        public Nullable<int> CuposPrograma { get; set; }
        public DateTime FechaInicioPrograma { get; set; }
        public DateTime FechaTerminoPrograma { get; set; }
        public string TipoPrograma { get; set; }
        public string CiudadFamilia { get; set; }
        public string PaisFamilia { get; set; }
        public string NombreInstitucion { get; set; }
        public int IdIntercambio { get; set; }

        public VDetallePostulacion() { }

        public VDetallePostulacion(string xml)
        {
            XmlSerializer serializiador = new XmlSerializer(typeof(VDetallePostulacion));
            StringReader reader = new StringReader(xml);

            VDetallePostulacion detallexml = (VDetallePostulacion)serializiador.Deserialize(reader);

            this.IdPostulante = detallexml.IdPostulante;
            this.NombrePostulante = detallexml.NombrePostulante;
            this.CorreoPostulante = detallexml.CorreoPostulante;
            this.TelefonoPostulante = detallexml.TelefonoPostulante;
            this.EstadoMoraPostulante = detallexml.EstadoMoraPostulante;
            this.IdFamilia = detallexml.IdFamilia;
            this.NombreFamilia = detallexml.NombreFamilia;
            this.DireccionFamilia = detallexml.DireccionFamilia;
            this.CorreoFamilia = detallexml.CorreoFamilia;
            this.TelefonoFamilia = detallexml.TelefonoFamilia;
            this.IdPrograma = detallexml.IdPrograma;
            this.NombrePrograma = detallexml.NombrePrograma;
            this.DescripcionPrograma = detallexml.DescripcionPrograma;
            this.CuposPrograma = detallexml.CuposPrograma;
            this.FechaInicioPrograma = detallexml.FechaInicioPrograma;
            this.FechaTerminoPrograma = detallexml.FechaTerminoPrograma;
            this.TipoPrograma = detallexml.TipoPrograma;
            this.CiudadFamilia = detallexml.CiudadFamilia;
            this.PaisFamilia = detallexml.PaisFamilia;
            this.NombreInstitucion = detallexml.NombreInstitucion;
            this.IdIntercambio = detallexml.IdIntercambio;
        }

        public string LeerDetallePostulacion(int idPostulante, string NomPrograma)
        {
            VISTA_DETALLE_POSTULACION detalle = new EntitiesCEM().VISTA_DETALLE_POSTULACION.First(d => d.ID_ALUMNO == idPostulante && d.NOMBRE_PROGRAMA == NomPrograma);

            VDetallePostulacion detallePostulacion = new VDetallePostulacion();

            detallePostulacion.IdPostulante = detalle.ID_ALUMNO;
            detallePostulacion.NombrePostulante = detalle.NOMBRE_POSTULANTE;
            detallePostulacion.CorreoPostulante = detalle.CORREO_POSTULANTE;
            detallePostulacion.TelefonoPostulante = detalle.TELEFONO_POSTULANTE;
            detallePostulacion.EstadoMoraPostulante = detalle.ESTADO_MORA;
            detallePostulacion.IdFamilia = detalle.ID_FAMILIA;
            detallePostulacion.NombreFamilia = detalle.NOMBRE_FAMILIA_ANFITRIONA;
            detallePostulacion.DireccionFamilia = detalle.DIRECCION_FAMILIA;
            detallePostulacion.CorreoFamilia = detalle.CORREO_FAMILIA;
            detallePostulacion.TelefonoFamilia = detalle.TELEFONO_FAMILIA;
            detallePostulacion.IdPrograma = detalle.ID_PROGRAMA;
            detallePostulacion.NombrePrograma = detalle.NOMBRE_PROGRAMA;
            detallePostulacion.DescripcionPrograma = detalle.DESCRIPCION;
            detallePostulacion.CuposPrograma = detalle.CUPOS;
            detallePostulacion.FechaInicioPrograma = detalle.FECHA_INICIO;
            detallePostulacion.FechaTerminoPrograma = detalle.FECHA_TERMINO;
            detallePostulacion.TipoPrograma = detalle.TIPO_CURSO;
            detallePostulacion.CiudadFamilia = detalle.CIUDAD_FAMILIA;
            detallePostulacion.PaisFamilia = detalle.NOMBRE_PAIS;
            detallePostulacion.NombreInstitucion = detalle.NOMBRE_INSTITUCION;
            detallePostulacion.IdIntercambio = detalle.ID_INTERCAMBIO;

            return detallePostulacion.Serializar();
        }

        public string Serializar()
        {
            XmlSerializer serializador = new XmlSerializer(typeof(VDetallePostulacion));
            StringWriter writer = new StringWriter();

            serializador.Serialize(writer, this);
            writer.Close();

            return writer.ToString();
        }
    }


}
