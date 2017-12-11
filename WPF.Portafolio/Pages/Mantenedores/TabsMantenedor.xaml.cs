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

namespace WPF.Portafolio.Pages.Mantenedores
{
    /// <summary>
    /// Lógica de interacción para TabsMantenedor.xaml
    /// </summary>
    public partial class TabsMantenedor : Page
    {
        public TabsMantenedor()
        {
            InitializeComponent();

            CargarTabs();
        }

        private void CargarTabs()
        {
            switch (MainMenu.UsuarioActual.Rol)
            {
                case "Administrador":
                    
                    break;
                case "EncargadoCEM":
                    tabAlumnos.Visibility = Visibility.Collapsed;
                    tabFamilia.Visibility = Visibility.Collapsed;
                    tabUsuarios.Visibility = Visibility.Collapsed;
                    break;
                case "EncargadoCEL":
                    tabPrograma.Visibility = Visibility.Collapsed;
                    tabCEL.Visibility = Visibility.Collapsed;
                    break;
            }
        }
    }
}
