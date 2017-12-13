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

namespace WPF.Portafolio.Pages.Programas
{
    /// <summary>
    /// Lógica de interacción para AdministrarProgramas.xaml
    /// </summary>
    public partial class AdministrarProgramas : Page
    {
        public AdministrarProgramas()
        {
            InitializeComponent();
            CargarProgramas();
            txtBuscarNombre.Focus();
        }

        public void CargarProgramas()
        {
            ServiciosWCF.Portafolio.Servicios svc = new ServiciosWCF.Portafolio.Servicios();

            ProgramaCollection programaCollection = new ProgramaCollection(svc.LeerTodosProgramas());

            dgProgramas.ItemsSource = programaCollection;
        }

        private void dgProgramas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgProgramas.SelectedIndex != -1)
            {
                btnModificar.IsEnabled = true;
            }
            else
            {
                btnModificar.IsEnabled = false;
            }
        }

        private void txtBuscarNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            ServiciosWCF.Portafolio.Servicios svc = new ServiciosWCF.Portafolio.Servicios();

            ProgramaCollection list = new ProgramaCollection(svc.BuscarProgramasPorNombre(txtBuscarNombre.Text));

            dgProgramas.ItemsSource = list;
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            ModificarPrograma modificarPrograma = new ModificarPrograma(this,(Programa)dgProgramas.SelectedItem);

            dgProgramas.SelectedIndex = -1;

            modificarPrograma.ShowDialog();
        }
    }
}
