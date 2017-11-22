using Negocio.Portafolio;
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
using System.Collections;
using Negocio.Portafolio.ViewClasses;

namespace WPF.Portafolio.Pages.Instituciones
{
    /// <summary>
    /// Lógica de interacción para ListaInstituciones.xaml
    /// </summary>
    public partial class ListaInstituciones : Page
    {
        public ListaInstituciones()
        {
            InitializeComponent();

            Mostrarinstitutos();
        }

        public void Mostrarinstitutos()
        {
            ServiciosWCF.Portafolio.Servicios svc = new ServiciosWCF.Portafolio.Servicios();

            InstitucionCollection list = new InstitucionCollection(svc.LeerTodasInstituciones());

            dgInstituciones.ItemsSource = GenerarVistaInstituciones(list);
        }

        private List<VInstitucion> GenerarVistaInstituciones(InstitucionCollection list)
        {
            List<VInstitucion> instituciones = new List<VInstitucion>();
            
            foreach (var item in list)
            {
                VInstitucion institucion = new VInstitucion(item);
                
                instituciones.Add(institucion);
            }

            return instituciones;
        }

        private void dgInstituciones_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dgInstituciones.SelectedIndex != -1)
                {
                    MantenedorInstitucion mantenedorInstitucion = new MantenedorInstitucion(((VInstitucion)dgInstituciones.SelectedItem).IdInstitucion, this);

                    dgInstituciones.SelectedIndex = -1;

                    mantenedorInstitucion.ShowDialog();
                }
                else
                {

                }
            }
            catch
            {
                Mostrarinstitutos();
            }
        }

        private void btnAgregarInstitucion_Click(object sender, RoutedEventArgs e)
        {
            MantenedorInstitucion mantenedorInstitucion = new MantenedorInstitucion(this);
            
            mantenedorInstitucion.ShowDialog();
        }
    }
}
