using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Negocio.Portafolio;

namespace ServiciosWCF.Portafolio
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IServicios
    {
        //Actividad
        [OperationContract]
        bool CrearActividad(string xml);
        [OperationContract]
        string LeerActividad(string xml);
        [OperationContract]
        bool ActualizarActividad(string xml);
        [OperationContract]
        bool EliminarActividad(string xml);
        [OperationContract]
        string LeerTodasActividades();
        [OperationContract]
        bool EnlazarPrograma(int idPrograma, int idActividad);
        [OperationContract]
        bool DesenlazarPrograma();
        [OperationContract]
        int IdActualEntidadActividad();

        //Administrativo
        [OperationContract]
        bool CrearAdministrativo(string xml);
        [OperationContract]
        string LeerAdministrativo(string xml);
        [OperationContract]
        bool ActualizarAdministrativo(string xml);
        [OperationContract]
        bool EliminarAdministrativo(string xml);
        [OperationContract]
        string LeerTodosAdministrativos();

        //Alumno
        [OperationContract]
        bool CrearAlumno(string xml);
        [OperationContract]
        string LeerAlumno(string xml);
        [OperationContract]
        bool ActualizarAlumno(string xml);
        [OperationContract]
        bool EliminarAlumno(string xml);
        [OperationContract]
        string LeerTodosAlumnos();
        [OperationContract]
        string BuscarALumnosPorNombreCompleto(string frase);
        
        //Ciudad
        [OperationContract]
        bool CrearCiudad(string xml);
        [OperationContract]
        string LeerCiudad(string xml);
        [OperationContract]
        bool ActualizarCiudad(string xml);
        [OperationContract]
        bool EliminarCiudad(string xml);
        [OperationContract]
        string LeerTodasCiudades();
        [OperationContract]
        string LeerCiudades(int pais);

        //EncargadoCel
        [OperationContract]
        bool CrearEncargadoCel(string xml);
        [OperationContract]
        string LeerEncargadoCel(string xml);
        [OperationContract]
        bool ActualizarEncargadoCel(string xml);
        [OperationContract]
        bool EliminarEncargadoCel(string xml);
        [OperationContract]
        string LeerTodosEncargadosCel();

        //FamiliaAnfitriona
        [OperationContract]
        bool CrearFamiliaAnfitriona(string xml);
        [OperationContract]
        string LeerFamiliaAnfitriona(string xml);
        [OperationContract]
        bool ActualizarFamiliaAnfitriona(string xml);
        [OperationContract]
        bool EliminarFamiliaAnfitriona(string xml);
        [OperationContract]
        string LeerTodasFamiliasAnfitrionas();
        [OperationContract]
        string LeerFamiliaIdentificador(string xml);
        [OperationContract]
        string BuscarFamiliaPorNombreCompleto(string nombreApellido);

        //Institucion
        [OperationContract]
        bool CrearInstitucion(string xml);
        [OperationContract]
        string LeerInstitucionXML(string xml);
        [OperationContract]
        bool ActualizarInstitucion(string xml);
        [OperationContract]
        bool EliminarInstitucion(string xml);
        [OperationContract]
        string LeerTodasInstituciones();
        [OperationContract]
        string LeerInstitucion(int idInstitucion);
        [OperationContract]
        string LeerTodosPostulantes();
        
        //Intercambio
        [OperationContract]
        bool CrearIntercambio(string xml);
        [OperationContract]
        string LeerIntercambio(string xml);
        [OperationContract]
        bool ActualizarIntercambio(string xml);
        [OperationContract]
        bool EliminarIntercambio(string xml);
        [OperationContract]
        string LeerTodosIntercambios();
        [OperationContract]
        string LeerDetallePostulacion(int idPostulante, string nomPrograma);

        //Nota
        [OperationContract]
        bool CrearNota(string xml);
        [OperationContract]
        string LeerNota(string xml);
        [OperationContract]
        bool ActualizarNota(string xml);
        [OperationContract]
        bool EliminarNota(string xml);
        [OperationContract]
        string LeerTodasNotas();
        [OperationContract]
        string NotasAlumno(string xml);

        //Pais
        [OperationContract]
        bool CrearPais(string xml);
        [OperationContract]
        string LeerPais(string xml);
        [OperationContract]
        bool ActualizarPais(string xml);
        [OperationContract]
        bool EliminarPais(string xml);
        [OperationContract]
        string LeerTodosPaises();
        [OperationContract]
        int IdActualEntidadPrograma();

        //Programa
        [OperationContract]
        bool CrearPrograma(string xml);
        [OperationContract]
        string LeerPrograma(string xml);
        [OperationContract]
        bool ActualizarPrograma(string xml);
        [OperationContract]
        bool EliminarPrograma(string xml);
        [OperationContract]
        string LeerTodosProgramas();
        
        //Usuario
        [OperationContract]
        bool validarUsuario(string userPass);
        [OperationContract]
        bool CrearUsuario(string xml);
        [OperationContract]
        string LeerUsuario(string xml);
        [OperationContract]
        bool ActualizarUsuario(string xml);
        [OperationContract]
        bool EliminarUsuario(string xml);
        [OperationContract]
        string LeerTodosUsuarios();
        [OperationContract]
        string ReadAllVNotasPrograma(int idALumno);

        //Vista alumnos postulantes
        [OperationContract]
        string BuscarPorNombreApellido(string frase);
        //Vista familia postulantes
        [OperationContract]
        string BuscarFamiliaNombreApellido(string frase);
    }
}
