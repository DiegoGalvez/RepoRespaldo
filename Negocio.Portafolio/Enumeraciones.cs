using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Portafolio
{
    public enum TipoCursos
    {
        Normal, Corto
    }

    public enum RolAsignado
    {
        Administrador, Alumno, Familia, EncargadoCEM, EncargadoCEL
    }

    public enum EstadoFamilia
    {
        Registrado, Postulando, Aceptado, Asignado
    }

    public enum EstadoPrograma
    {
        Creado, PorValidar, Validado, Publicado, Rechazado
    }
}
