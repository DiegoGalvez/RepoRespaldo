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
using DALC.Portafolio;
using Negocio.Portafolio.ViewClasses.NotasProgramaAlumno;
using WPF.Portafolio.Pages.Alumnos;
using WPF.Portafolio.Pages.ListaNotasAlumnos;
using Negocio.Portafolio.ViewClasses.AlumnosPostulantes;

namespace WPF.Portafolio.Pages.Familias
{
    /// <summary>
    /// Lógica de interacción para PostulacionAlumno.xaml
    /// </summary>
    public partial class PostulacionAlumno : Page
    {
        public PostulacionAlumno()
        {
            InitializeComponent();
            ListardgPostulaciones();
        }

        private void ListardgPostulaciones()
        {
            EntitiesCEM ctx = new EntitiesCEM();
            ServiciosWCF.Portafolio.Servicios svc = new ServiciosWCF.Portafolio.Servicios();

            VAlumnosPostulantesCollection listaPostulantes = new VAlumnosPostulantesCollection(svc.LeerTodosPostulantes());
            dgPostulantes.ItemsSource = listaPostulantes.Where(p=> p.Estado.Equals("Postulando"));
        }

       
        private void dgPostulantes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dgPostulantes.SelectedIndex != -1)
                {
                    DetallePostulacion detallePostulacion = new DetallePostulacion(((VAlumnosPostulantes)dgPostulantes.SelectedItem));

                    detallePostulacion.ShowDialog();

                    ListardgPostulaciones();
                }
            }
            catch (Exception)
            {
                ListardgPostulaciones();
            }
        }

        private void txtIdAlumno_TextChanged(object sender, TextChangedEventArgs e)
        {
            ServiciosWCF.Portafolio.Servicios svc = new ServiciosWCF.Portafolio.Servicios();

            VAlumnosPostulantesCollection listaPostulantes = new VAlumnosPostulantesCollection().BuscarPorNombreApellido(txtNombreApellido.Text);
            
            dgPostulantes.ItemsSource = listaPostulantes;
        }
    }
}
