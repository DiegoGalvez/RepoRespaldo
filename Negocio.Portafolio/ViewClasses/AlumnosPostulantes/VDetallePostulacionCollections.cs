using DALC.Portafolio;
using Negocio.Portafolio.ViewClasses.NotasProgramaAlumno;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Negocio.Portafolio.ViewClasses.AlumnosPostulantes
{
    public class VDetallePostulacionCollections : List<VDetallePostulacion>
    {
        public VDetallePostulacionCollections()
        {
        }

        public VDetallePostulacionCollections(string xml)
        {
            XmlSerializer serializador = new XmlSerializer(typeof(VAlumnosPostulantesCollection));
            StringReader reader = new StringReader(xml);

            VDetallePostulacionCollections list = (VDetallePostulacionCollections)serializador.Deserialize(reader);

            this.AddRange(list);
        }
        
        //Metodo que obtiene los programas y notas de un alumno
        public VDetallePostulacionCollections LeerDetallePostulacion(int idPostulante, string NomPrograma)
        {
            var detalle = new EntitiesCEM().VISTA_DETALLE_POSTULACION;

            return GenerarListado(detalle.ToList());
        }
        

        private VDetallePostulacionCollections GenerarListado(List<VISTA_DETALLE_POSTULACION> listaDALC)
        {
            VDetallePostulacionCollections listaBC = new VDetallePostulacionCollections();

            foreach (var item in listaDALC)
            {
                VDetallePostulacion detallePostulacion = new VDetallePostulacion();

                detallePostulacion.IdPostulante = item.ID_ALUMNO;
                detallePostulacion.NombrePostulante = item.NOMBRE_POSTULANTE;
                detallePostulacion.CorreoPostulante = item.CORREO_POSTULANTE;
                detallePostulacion.TelefonoPostulante = item.TELEFONO_POSTULANTE;
                detallePostulacion.EstadoMoraPostulante = item.ESTADO_MORA;
                detallePostulacion.IdFamilia = item.ID_FAMILIA;
                detallePostulacion.NombreFamilia = item.NOMBRE_FAMILIA_ANFITRIONA;
                detallePostulacion.DireccionFamilia = item.DIRECCION_FAMILIA;
                detallePostulacion.CorreoFamilia = item.CORREO_FAMILIA;
                detallePostulacion.TelefonoFamilia = item.TELEFONO_FAMILIA;
                detallePostulacion.IdPrograma = item.ID_PROGRAMA;
                detallePostulacion.NombrePrograma = item.NOMBRE_PROGRAMA;
                detallePostulacion.DescripcionPrograma = item.DESCRIPCION;
                detallePostulacion.CuposPrograma = item.CUPOS;
                detallePostulacion.FechaInicioPrograma = item.FECHA_INICIO;
                detallePostulacion.FechaTerminoPrograma = item.FECHA_TERMINO;
                detallePostulacion.TipoPrograma = item.TIPO_CURSO;
                detallePostulacion.CiudadFamilia = item.CIUDAD_FAMILIA;
                detallePostulacion.PaisFamilia = item.NOMBRE_PAIS;
                detallePostulacion.NombreInstitucion = item.NOMBRE_INSTITUCION;

                listaBC.Add(detallePostulacion);
            }
            return listaBC;
        }

        //metodo que serializa la coleccion de Notas de Programa 
        public string Serializar()
        {
            XmlSerializer serializar = new XmlSerializer(typeof(VDetallePostulacionCollections));
            StringWriter writer = new StringWriter();

            serializar.Serialize(writer, this);

            writer.Close();
            return writer.ToString();
        }
    }
}
