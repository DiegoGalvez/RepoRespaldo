using Negocio.Portafolio;
using Negocio.Portafolio.ViewClasses.ProgramasFinalizados;
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

namespace WPF.Portafolio.Pages.Certificado
{
    /// <summary>
    /// Lógica de interacción para PrograsmasFinalizados.xaml
    /// </summary>
    public partial class ProgramasFinalizados : Page
    {
        public ProgramasFinalizados()
        {
            InitializeComponent();
            CargardgProgramas();
        }

        private void CargardgProgramas()
        {
            ServiciosWCF.Portafolio.Servicios svc = new ServiciosWCF.Portafolio.Servicios();
            dgProgramas.ItemsSource = new VProgramasFinalizadosCollection(svc.BuscarProgramasFinalizados());
            
        }

        private void dgProgramas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dgProgramas.SelectedIndex != -1)
                {
                    VProgramasFinalizados vPrograma = (VProgramasFinalizados)dgProgramas.SelectedItem;

                    Programa programa = new Programa();
                    programa.IdPrograma = vPrograma.IdPrograma;
                    programa.Read();
                    ListasAlumnosAprobados alumnos = new ListasAlumnosAprobados(vPrograma);

                    dgProgramas.SelectedIndex = -1;

                    this.NavigationService.Navigate(alumnos);
                }
            }
            catch
            {
                CargardgProgramas();
            }
        }
    }
}
