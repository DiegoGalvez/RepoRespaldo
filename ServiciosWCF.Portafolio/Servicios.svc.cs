using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Negocio.Portafolio;
using Negocio.Portafolio.ViewEntities;
using Negocio.Portafolio.ViewClasses.NotasProgramaAlumno;
using Negocio.Portafolio.ViewClasses.AlumnosPostulantes;
using Negocio.Portafolio.ViewClasses.FamiliaPostulantes;
using Negocio.Portafolio.ViewClasses.ProgramasFinalizados;

namespace ServiciosWCF.Portafolio
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Servicios" en el código, en svc y en el archivo de configuración.
    // NOTE: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Servicios.svc o Servicios.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class Servicios : IServicios
    {
        //Actividad
        public bool CrearActividad(string xml)
        {
            Actividad actividad = new Actividad(xml);

            if (actividad.Create())
            {
                return true;
            }
            return false;
        }

        public string LeerActividad(string xml)
        {
            Actividad actividad = new Actividad(xml);

            if (actividad.Read())
            {
                return actividad.Serializar();
            }
            return null;
        }


        public bool ActualizarActividad(string xml)
        {
            Actividad actividad = new Actividad();

            if (actividad.Update())
            {
                return true;
            }
            return false;
        }

        public bool EliminarActividad(string xml)
        {
            Actividad actividad = new Actividad();

            if (actividad.Delete())
            {
                return true;
            }
            return false;
        }

        public bool EnlazarPrograma(int idPrograma, int idActividad)
        {
            Actividad actividad = new Actividad();

            if (actividad.LinkPrograma(idPrograma, idActividad))
            {
                return true;
            }
            return false;
        }

        public bool DesenlazarPrograma()
        {
            Actividad actividad = new Actividad();

            if (actividad.UnlinkPrograma())
            {
                return true;
            }
            return false;
        }

        public string LeerTodasActividades()
        {
            ActividadCollection actividadCollection = new ActividadCollection();

            return actividadCollection.ReadAll().Serializar();
        }

        public int IdActualEntidadActividad()
        {
            Actividad actividad = new Actividad();

            return actividad.CurrentActividadEntityID();
        }

        public string LeerActividadesPrograma(int idPrograma)
        {
            ActividadCollection listaActividades = new ActividadCollection().LeerActividadesPrograma(idPrograma);

            return listaActividades.Serializar();
        }

        //Administrativo
        public bool CrearAdministrativo(string xml)
        {
            Administrativo administrativo = new Administrativo(xml);

            if (administrativo.Create())
            {
                return true;
            }
            return false;
        }

        public string LeerAdministrativo(string xml)
        {
            throw new NotImplementedException();
        }

        public bool ActualizarAdministrativo(string xml)
        {
            Administrativo administrativo = new Administrativo();

            if (administrativo.Update())
            {
                return true;
            }
            return false;
        }

        public bool EliminarAdministrativo(string xml)
        {
            Administrativo administrativo = new Administrativo();

            if (administrativo.Delete())
            {
                return true;
            }
            return false;
        }
        public string LeerTodosAdministrativos()
        {
            AdministrativoCollection administrativoCollection = new AdministrativoCollection();

            return administrativoCollection.ReadAll().Serializar();
        }

        //Alumno
        public bool CrearAlumno(string xml)
        {
            Alumno alumno = new Alumno(xml);

            if (alumno.Create())
            {
                return true;
            }
            return false;
        }

        public string LeerAlumno(string xml)
        {
            Alumno alumno = new Alumno(xml);

            if (alumno.Read())
            {
                return alumno.Serializar();
            }
            return null;
        }

        public bool ActualizarAlumno(string xml)
        {
            Alumno alumno = new Alumno(xml);

            if (alumno.Update())
            {
                return true;
            }
            return false;
        }

        public bool EliminarAlumno(string xml)
        {
            Alumno alumno = new Alumno(xml);

            if (alumno.Delete())
            {
                return true;
            }
            return false;
        }
        public string LeerTodosAlumnos()
        {
            AlumnoCollection alumnoCollection = new AlumnoCollection();

            return alumnoCollection.ReadAll().Serializar();
        }

        public string BuscarALumnosPorNombreCompleto(string frase)
        {
            AlumnoCollection alumnoCollection = new AlumnoCollection();

            return alumnoCollection.BuscarALumnosPorNombreCompleto(frase).Serializar();
        }
        

        //Ciudad
        public bool CrearCiudad(string xml)
        {
            Ciudad ciudad = new Ciudad(xml);

            if (ciudad.Create())
            {
                return true;
            }
            return false;
        }

        public string LeerCiudad(string xml)
        {
            throw new NotImplementedException();
        }

        public bool ActualizarCiudad(string xml)
        {
            Ciudad ciudad = new Ciudad();

            if (ciudad.Update())
            {
                return true;
            }
            return false; ;
        }

        public bool EliminarCiudad(string xml)
        {
            Ciudad ciudad = new Ciudad();

            if (ciudad.Delete())
            {
                return true;
            }
            return false;
        }
        public string LeerTodasCiudades()
        {
            CiudadCollection ciudadCollection = new CiudadCollection();

            return ciudadCollection.ReadAll().Serializar();
        }

        public string LeerCiudades(int pais)
        {
            CiudadCollection ciudadCollection = new CiudadCollection();

            return ciudadCollection.LeerCiudades(pais).Serializar();
        }

        //EncargadoCel
        public bool CrearEncargadoCel(string xml)
        {
            EncargadoCel encargadoCel = new EncargadoCel(xml);

            if (encargadoCel.Create())
            {
                return true;
            }
            return false;
        }

        public string LeerEncargadoCel(string xml)
        {
            throw new NotImplementedException();
        }

        public bool ActualizarEncargadoCel(string xml)
        {
            EncargadoCel encargadoCel = new EncargadoCel();

            if (encargadoCel.Update())
            {
                return true;
            }
            return false;
        }

        public bool EliminarEncargadoCel(string xml)
        {
            EncargadoCel encargadoCel = new EncargadoCel();

            if (encargadoCel.Delete())
            {
                return true;
            }
            return false;
        }
        public string LeerTodosEncargadosCel()
        {
            EncargadoCelCollection encargadoCelCollection = new EncargadoCelCollection();

            return encargadoCelCollection.ReadAll().Serializar();
        }

        //FamiliaAnfitriona
        public bool CrearFamiliaAnfitriona(string xml)
        {
            FamiliaAnfitriona familiaAnfitriona = new FamiliaAnfitriona(xml);

            if (familiaAnfitriona.Create())
            {
                return true;
            }
            return false;
        }

        public string LeerFamiliaAnfitriona(string xml)
        {
            FamiliaAnfitriona familia = new FamiliaAnfitriona(xml);

            if (familia.Read())
            {
                return familia.Serializar();
            }
            return null;
        }

        public string LeerFamiliaIdentificador(string xml)
        {
            FamiliaAnfitriona familia = new FamiliaAnfitriona(xml);

            if (familia.LeerPorIdentificador())
            {
                return familia.Serializar();
            }
            return null;
        }

        public bool ActualizarFamiliaAnfitriona(string xml)
        {
            FamiliaAnfitriona familiaAnfitriona = new FamiliaAnfitriona();

            if (familiaAnfitriona.Update())
            {
                return true;
            }
            return false;
        }

        public bool EliminarFamiliaAnfitriona(string xml)
        {
            FamiliaAnfitriona familiaAnfitriona = new FamiliaAnfitriona();

            if (familiaAnfitriona.Delete())
            {
                return true;
            }
            return false;
        }

        public string LeerTodasFamiliasAnfitrionas()
        {
            FamiliaAnfitrionaCollection familiaAnfitrionaCollection = new FamiliaAnfitrionaCollection();

            return familiaAnfitrionaCollection.ReadAll().Serializar();
        }

        public string BuscarFamiliaPorNombreCompleto(string nombreApellido)
        {
            FamiliaAnfitrionaCollection familiaCollection = new FamiliaAnfitrionaCollection();

            return familiaCollection.BuscarALumnosPorNombreCompleto(nombreApellido).Serializar();
        }

        //Institucion
        public bool CrearInstitucion(string xml)
        {
            Institucion institucion = new Institucion(xml);

            if (institucion.Create())
            {
                return true;
            }
            return false;
        }

        public string LeerInstitucionXML(string xml)
        {
            Institucion institucion = new Institucion(xml);

            if (institucion.Read())
            {
                return institucion.Serializar();
            }
            return null;
        }

        public bool ActualizarInstitucion(string xml)
        {
            Institucion institucion = new Institucion(xml);

            if (institucion.Update())
            {
                return true;
            }
            return false;
        }

        public bool EliminarInstitucion(string xml)
        {
            Institucion institucion = new Institucion();

            if (institucion.Delete())
            {
                return true;
            }
            return false;
        }

        public string LeerTodasInstituciones()
        {
            InstitucionCollection institucionCollection = new InstitucionCollection();

            return institucionCollection.ReadAll().Serializar();
        }

        public string LeerInstitucion(int idInstitucion)
        {
            Institucion institucion = new Institucion() { IdInstitucion = idInstitucion};

            if (institucion.Read())
            {
                return institucion.Serializar();
            }
            return null;
        }

        //Intercambio
        public bool CrearIntercambio(string xml)
        {
            Intercambio intercambio = new Intercambio(xml);

            if (intercambio.Create())
            {
                return true;
            }
            return false;
        }

        public string LeerIntercambio(string xml)
        {
            Intercambio intercambio = new Intercambio(xml);

            if (intercambio.Read())
            {
                return intercambio.Serializar();
            }
            return null;
        }

        public bool ActualizarIntercambio(string xml)
        {
            Intercambio intercambio = new Intercambio(xml);
            if (intercambio.Update())
            {
                return true;
            }
            return false;
        }

        public bool EliminarIntercambio(string xml)
        {
            Intercambio intercambio = new Intercambio();

            if (intercambio.Delete())
            {
                return true;
            }
            return false;
        }

        public string LeerTodosIntercambios()
        {
            IntercambioCollection intercambioCollecion = new IntercambioCollection();

            return intercambioCollecion.ReadAll().Serializar();
        }

        //Nota
        public bool CrearNota(string xml)
        {
            Nota nota = new Nota(xml);

            if (nota.Create())
            {
                return true;
            }
            return false;
        }

        public string LeerNota(string xml)
        {
            throw new NotImplementedException();
        }

        public bool ActualizarNota(string xml)
        {
            Nota nota = new Nota();

            if (nota.Update())
            {
                return true;
            }
            return false;
        }

        public bool EliminarNota(string xml)
        {
            Nota nota = new Nota();

            if (nota.Delete())
            {
                return true;
            }
            return false;
        }

        public string LeerTodasNotas()
        {
            NotaCollection notaCollection = new NotaCollection();

            return notaCollection.ReadAll().Serializar();
        }

        //Pais
        public bool CrearPais(string xml)
        {
            Pais pais = new Pais(xml);

            if (pais.Create())
            {
                return true;
            }
            return false;
        }

        public string LeerPais(string xml)
        {
            Pais pais = new Pais(xml);

            if (pais.Read())
            {
                return pais.Serializar();
            }
            return null;
        }

        public bool ActualizarPais(string xml)
        {
            Pais pais = new Pais();

            if (pais.Update())
            {
                return true;
            }
            return false;
        }

        public bool EliminarPais(string xml)
        {
            Pais pais = new Pais();

            if (pais.Delete())
            {
                return true;
            }
            return false;
        }

        public string LeerTodosPaises()
        {
            PaisCollection paisCollection = new PaisCollection();

            return paisCollection.ReadAll().Serializar();
        }

        //Programa
        public bool CrearPrograma(string xml)
        {
            Programa programa = new Programa(xml);

            if (programa.Create())
            {
                return true;
            }
            return false;
        }

        public string LeerPrograma(string xml)
        {
            Programa programa = new Programa(xml);

            if (programa.Read())
            {
                return programa.Serializar();
            }
            return null;
        }


        public bool ActualizarPrograma(string xml)
        {
            Programa programa = new Programa();

            if (programa.Update())
            {
                return true;
            }
            return false;
        }

        public bool EliminarPrograma(string xml)
        {
            Programa programa = new Programa();

            if (programa.Delete())
            {
                return true;
            }
            return false;
        }

        public string LeerTodosProgramas()
        {
            ProgramaCollection programaCollection = new ProgramaCollection();

            return programaCollection.ReadAll().Serializar();
        }

        public int IdActualEntidadPrograma()
        {
            Programa programa = new Programa();

            return programa.CurrentProgramaEntityID();
        }

        public string BuscarProgramasFinalizados()
        {
            VProgramasFinalizadosCollection programaCollection = new VProgramasFinalizadosCollection().LeerProgramasFinalizados();
            
            return programaCollection.Serializar();
        }


        public string BuscarProgramasFinalizadosCEL(int idCEL)
        {
            ProgramaCollection programaCollection = new ProgramaCollection();

            return programaCollection.BuscarProgramasFinalizadosCEL(idCEL).Serializar();
        }

        //Usuario
        public bool validarUsuario(string userPass)
        {
            Usuario usuario = new Usuario();

            if (usuario.ValidarUsuario(userPass))
            {
                return true;
            }
            return false;
        }

        public bool CrearUsuario(string xml)
        {
            Usuario usuario = new Usuario(xml);

            if (usuario.Create())
            {
                return true;
            }
            return false;
        }

        public string LeerUsuario(string xml)
        {
            Usuario usuario = new Usuario(xml);

            if (usuario.Read())
            {
                return usuario.Serializar();
            }
            return null;
        }

        public bool ActualizarUsuario(string xml)
        {
            Usuario usuario = new Usuario(xml);

            if (usuario.Update())
            {
                return true;
            }
            return false;
        }

        public bool EliminarUsuario(string xml)
        {
            Usuario usuario = new Usuario(xml);

            if (usuario.Delete())
            {
                return true;
            }
            return false;
        }

        public string LeerTodosUsuarios()
        {
            UsuarioCollection usuarioCollection = new UsuarioCollection();

            return usuarioCollection.ReadAll().Serializar();
        }

        public string NotasAlumno(string xml)
        {
            NotaCollection _nota = new NotaCollection();

            return _nota.NotasAlumno(xml).Serializar();
        }

        //OBJETOS VISTA
        
        //Entities

        //Collection
        public string ReadAllVNotasPrograma(int idALumno)
        {
            try
            {
                VNotasProgramaAlumnoCollection lista = new VNotasProgramaAlumnoCollection();

                return lista.ObtenerNotasProgramas(idALumno).Serializar();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string LeerTodosPostulantes()
        {
            try
            {
                VAlumnosPostulantesCollection lista = new VAlumnosPostulantesCollection();

                return lista.LeerTodoVistaPostulantes().Serializar();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string LeerDetallePostulacion(int IDAlumno, string NomPrograma)
        {
            string detalle = new VDetallePostulacion().LeerDetallePostulacion(IDAlumno, NomPrograma);

            return detalle;
        }

        public string BuscarPorNombreApellido(string frase)
        {
            try
            {
                VAlumnosPostulantesCollection list = new VAlumnosPostulantesCollection().BuscarPorNombreApellido(frase);

                return list.Serializar();
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        public string BuscarFamiliaNombreApellido(string frase)
        {
            try
            {
                VFamiliasPostulantesCollection list = new VFamiliasPostulantesCollection().BuscarFamiliaNombreApellido(frase);

                return list.Serializar();
            }
            catch (Exception)
            {
                return null;
            }
        }

        
    }
}
