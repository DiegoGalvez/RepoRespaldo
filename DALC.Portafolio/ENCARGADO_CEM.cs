//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DALC.Portafolio
{
    using System;
    using System.Collections.Generic;
    
    public partial class ENCARGADO_CEM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ENCARGADO_CEM()
        {
            this.USUARIO = new HashSet<USUARIO>();
        }
    
        public int ID_ENCARGADO_CEM { get; set; }
        public string IDENTIFICACION { get; set; }
        public string NOMBRE { get; set; }
        public string APELL_PATERNO { get; set; }
        public string APELL_MATERNO { get; set; }
        public int ID_PAIS { get; set; }
        public int ID_CIUDAD { get; set; }
        public string CORREO { get; set; }
    
        public virtual PAIS PAIS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<USUARIO> USUARIO { get; set; }
    }
}
