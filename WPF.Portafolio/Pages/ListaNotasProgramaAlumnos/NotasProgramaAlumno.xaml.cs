using MahApps.Metro.Controls;
using Negocio.Portafolio;
using Negocio.Portafolio.ViewEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPF.Portafolio.Pages.ListaNotasAlumnos
{
    /// <summary>
    /// Lógica de interacción para NotasProgramaAlumno.xaml
    /// </summary>
    public partial class NotasProgramaAlumno : MetroWindow
    {
        public NotasProgramaAlumno(Alumno alumno)
        {
            InitializeComponent();

            lblNombreAlumno.Content = alumno.NombreCompleto();

            CargarNotas(alumno.IdAlumno);
        }
        

        private void CargarNotas(int idALumno)
        {
            ServiciosWCF.Portafolio.Servicios svc = new ServiciosWCF.Portafolio.Servicios();

            VNotasProgramaAlumnoCollection vNotasProgramaAlumnoCollection = new VNotasProgramaAlumnoCollection(svc.ReadAllVNotasPrograma(idALumno));
            
            if(vNotasProgramaAlumnoCollection.Count==0)
            {
                dgNotasProgramas.Visibility = Visibility.Hidden;
                lblMensaje.Content = "No tiene notas Registradas";
            }
            else
            {
                dgNotasProgramas.ItemsSource = vNotasProgramaAlumnoCollection;
            }
            
        }

    }
}
