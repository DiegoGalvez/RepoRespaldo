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
using WPF.Portafolio.Pages.Programas;

namespace WPF.Portafolio.Pages
{
    /// <summary>
    /// Lógica de interacción para CrearPrograma.xaml
    /// </summary>
    public partial class CrearPrograma : Page
    {
        public CrearPrograma()
        {
            InitializeComponent();
            CargarCbTipo();
            CargarDGActividades();
        }

        private void CargarDGActividades()
        {
            
        }

        private void CargarCbTipo()
        {
            cbTipoPrograma.ItemsSource = Enum.GetValues(typeof(TipoCursos));
            cbTipoPrograma.SelectedItem = TipoCursos.Normal;
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            AgregarActividad addAct = new AgregarActividad();
            addAct.ShowDialog();
            
        }
    }
}
