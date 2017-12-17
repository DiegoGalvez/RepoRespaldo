using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF.Portafolio.Pages.Mantenedores;
using WPF.Portafolio.Pages.Certificado;
using WPF.Portafolio.Pages.Programas;
using WPF.Portafolio.Pages.Reporte;
using Negocio.Portafolio;
using WPF.Portafolio.Pages;
using WPF.Portafolio.Pages.Familias;

namespace WPF.Portafolio
{
    /// <summary>
    /// Lógica de interacción para AyudaOnline.xaml
    /// </summary>
    public partial class AyudaOnline : Window
    {
        public AyudaOnline()
        {
            InitializeComponent();
        }

        private void btnAyuda_Click(object sender, RoutedEventArgs e)
        {
            Help.ShowHelp(null, "HelpWeb.chm");
        }
        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.F1)
            {
                Help.ShowHelp(null, "HelpWeb.chm");
            }
        }
    }
}
