using System.Windows;
using System.Windows.Forms;

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
