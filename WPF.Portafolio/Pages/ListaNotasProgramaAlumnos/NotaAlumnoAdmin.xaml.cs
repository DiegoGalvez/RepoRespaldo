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
using Negocio.Portafolio;
using Negocio.Portafolio.ViewEntities;

namespace WPF.Portafolio.Pages.ListaNotasProgramaAlumnos
{
    /// <summary>
    /// Lógica de interacción para NotaAlumnoAdmin.xaml
    /// </summary>
    public partial class NotaAlumnoAdmin : Window
    {
        public NotaAlumnoAdmin(Alumno alumno)
        {
            InitializeComponent();

            lblNombreAlumno.Content = alumno.NombreCompleto();

            CargarNotas(alumno.IdAlumno);
        }
        


        private void CargarNotas(int idALumno)
        {
            ServiciosWCF.Portafolio.Servicios svc = new ServiciosWCF.Portafolio.Servicios();

            VNotasProgramaAlumnoCollection vNotasProgramaAlumnoCollection = new VNotasProgramaAlumnoCollection(svc.ReadAllVNotasPrograma(idALumno));

            if (vNotasProgramaAlumnoCollection.Count == 0)
            {
                lblMensaje.Content = "No tiene notas Registradas";
                
            }
            dgNotasProgramas.ItemsSource = vNotasProgramaAlumnoCollection;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}

