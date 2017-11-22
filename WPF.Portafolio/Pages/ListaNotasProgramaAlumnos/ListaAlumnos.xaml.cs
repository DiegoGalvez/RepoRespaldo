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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Negocio.Portafolio;
using ServiciosWCF.Portafolio;
using WPF.Portafolio.Pages.ListaNotasAlumnos;
using WPF.Portafolio.Pages.ListaNotasProgramaAlumnos;

namespace WPF.Portafolio.Pages
{
    /// <summary>
    /// Lógica de interacción para ListaNotas.xaml
    /// </summary>
    public partial class ListaNotas : Page
    {
        public ListaNotas()
        {
            InitializeComponent();

            MostrarAlumnos();

            txtBuscarNombreOApellido.Focus();
        }

        private void MostrarAlumnos()
        {
            ServiciosWCF.Portafolio.Servicios svc = new ServiciosWCF.Portafolio.Servicios();

            AlumnoCollection list = new AlumnoCollection(svc.LeerTodosAlumnos());
            
            dgNotasAlumno.ItemsSource = list;
        }
        
        private void dgNotasAlumno_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                if (dgNotasAlumno.SelectedIndex != -1)
                {
                    if (MainMenu.UsuarioActual.Rol == "Administrador"/*RolAsignado.EncargadoCEL.ToString()*/)
                    {
                        NotaAlumnoAdmin notasAlumno = new NotaAlumnoAdmin(((Alumno)dgNotasAlumno.SelectedItem));
                        dgNotasAlumno.SelectedIndex = -1;
                        notasAlumno.ShowDialog();
                    }
                    else
                    {
                        NotasProgramaAlumno notasAlumno = new NotasProgramaAlumno(((Alumno)dgNotasAlumno.SelectedItem));
                        dgNotasAlumno.SelectedIndex = -1;
                        notasAlumno.ShowDialog();
                    }
                }
            }
            catch
            {
                MostrarAlumnos();
            }
        }
        
        private void txtBuscarNombreOApellido_TextChanged(object sender, TextChangedEventArgs e)
        {
            ServiciosWCF.Portafolio.Servicios svc = new ServiciosWCF.Portafolio.Servicios();

            AlumnoCollection list = new AlumnoCollection(svc.BuscarALumnosPorNombreCompleto(txtBuscarNombreOApellido.Text));

            dgNotasAlumno.ItemsSource = list;
        }
    }
}
