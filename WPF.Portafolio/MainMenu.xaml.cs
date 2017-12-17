using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Negocio.Portafolio;
using WPF.Portafolio.Pages;
using WPF.Portafolio.Pages.Familias;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Windows.Media.Animation;
using WPF.Portafolio.Pages.Mantenedores;
using WPF.Portafolio.Pages.Certificado;
using WPF.Portafolio.Pages.Programas;
using WPF.Portafolio.Pages.Reporte;
using System.Windows.Forms;

namespace WPF.Portafolio
{
    /// <summary>
    /// Lógica de interacción para MainMenu.xaml
    /// </summary>
    public partial class MainMenu : MetroWindow
    {
        public static Usuario UsuarioActual;

        public MainMenu()
        {
            InitializeComponent();
        }

        public MainMenu(Usuario user)
        {
            InitializeComponent();
            UsuarioActual = user;
            LBNombreUsuario.Content = UsuarioActual.NomUsuario;
            SideMenuRol();
            Main.Content = new AcercaDe();

        }


        


        private void SideMenuRol()
        {
            //ROLES  Administrador, Alumnos, Familia, EncargadoCEM, EncargadoCEL

            switch (UsuarioActual.Rol)
            {
                case "Administrador":
                    //Menu para Administrador (Todas las opciones disponibles)
                    filaPostulacionesA.Height = new GridLength(0);
                    btnPostulacionesAlumnos.Visibility = Visibility.Collapsed;
                    imgPostulacionesAlumnos.Visibility = Visibility.Collapsed;

                    filaPostularaPrograma.Height = new GridLength(0);
                    btnPostularAPrograma.Visibility = Visibility.Collapsed;
                    imgListaProgramas.Visibility = Visibility.Collapsed;
                    break;
                case "Alumno":
                    //Menu para alumno
                    this.ShowMessageAsync("Error de permisos", "Aplicacion no disponible para rol: Alumnos");
                    this.Close();
                    break;
                case "Familia":
                    //Menu para familia
                    this.ShowMessageAsync("Error de permisos", "Aplicacion no disponible para rol: Familia Anfitriona");
                    this.Close();
                    break;
                case "EncargadoCEM":
                    //Menu para Encargado CEM
                    filaPostularaPrograma.Height = new GridLength(0);
                    btnPostularAPrograma.Visibility = Visibility.Collapsed;
                    imgListaProgramas.Visibility = Visibility.Collapsed;
                    break;

                case "EncargadoCEL":
                    //Menu para Encargado CEL
                    filaPostulacionesA.Height = new GridLength(0);
                    btnPostulacionesAlumnos.Visibility = Visibility.Collapsed;
                    imgPostulacionesAlumnos.Visibility = Visibility.Collapsed;

                    filaReporte.Height = new GridLength(0);
                    btnReporte.Visibility = Visibility.Collapsed;
                    imgReporte.Visibility = Visibility.Collapsed;

                    filaCertificado.Height = new GridLength(0);
                    btnCertificado.Visibility = Visibility.Collapsed;
                    imgCertificado.Visibility = Visibility.Collapsed;

                    filaValidarPrograma.Height = new GridLength(0);
                    btnValidarPrograma.Visibility = Visibility.Collapsed;
                    imgValidarProgramas.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void CargarNombreUsuario(string nomUsario)
        {
            Usuario user = new Usuario();
            user.NomUsuario = nomUsario;
            user.Read();
            UsuarioActual = user;

            string tx = UsuarioActual.NomUsuario;
            LBNombreUsuario.Content = tx;
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            if (stackMenuFondo.Width < 80)
            {
                DoubleAnimation animation = new DoubleAnimation();
                animation.From = 0;
                animation.To = 240;
                animation.Duration = TimeSpan.FromMilliseconds(500);
                stackMenuFondo.BeginAnimation(WidthProperty, animation);
            }
            if (stackMenuFondo.Width > 250)
            {
                DoubleAnimation animation = new DoubleAnimation();
                animation.From = 240;
                animation.To = 0;
                animation.Duration = TimeSpan.FromMilliseconds(500);
                stackMenuFondo.BeginAnimation(WidthProperty, animation);
            }

        }

        private void btnMenu_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Thickness margenMinimizado = new Thickness(-240, 0, 0, 0);
            Thickness margenMaximizado = new Thickness(0, 0, 0, 0);
            if (stackMenuFondo.Margin == margenMinimizado)
            {
                ThicknessAnimation thick = new ThicknessAnimation();
                thick.From = new Thickness(-240, 0, 0, 0);
                thick.To = new Thickness(0, 0, 0, 0);
                thick.Duration = TimeSpan.FromMilliseconds(500);
                stackMenuFondo.BeginAnimation(MarginProperty, thick);
            }
            if (stackMenuFondo.Margin == margenMaximizado)
            {
                ThicknessAnimation thick = new ThicknessAnimation();
                thick.From = new Thickness(0, 0, 0, 0);
                thick.To = new Thickness(-240, 0, 0, 0);
                thick.Duration = TimeSpan.FromMilliseconds(500);
                stackMenuFondo.BeginAnimation(MarginProperty, thick);
            }
        }

        private void btnPostulacionesAlumnos_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            CleanBackgroundbtn();
            btnPostulacionesAlumnos.Background = new SolidColorBrush(Colors.DarkRed);
            Main.Content = new PostulacionAlumno();
        }


        private void btnNotas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            CleanBackgroundbtn();
            btnNotas.Background = new SolidColorBrush(Colors.DarkRed);
            Main.Content = new ListaProgramasNotas();
        }

        private void btnPostulacionesFamilias_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            CleanBackgroundbtn();
            btnPostulacionesFamilias.Background = new SolidColorBrush(Colors.DarkRed);
            Main.Content = new ListasFamiliasPostulantes();
        }

        private void btnMantendor_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            CleanBackgroundbtn();
            btnMantendor.Background = new SolidColorBrush(Colors.DarkRed);
            Main.Content = new TabsMantenedor();
        }

        private void btnReporte_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            CleanBackgroundbtn();
            btnReporte.Background = new SolidColorBrush(Colors.DarkRed);
            Main.Content = new Reportes();
        }

        private void btnCertificado_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            CleanBackgroundbtn();
            btnCertificado.Background = new SolidColorBrush(Colors.DarkRed);
            Main.Content = new ProgramasFinalizados();
        }

        private void btnPostularAPrograma_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            CleanBackgroundbtn();
            btnPostularAPrograma.Background = new SolidColorBrush(Colors.DarkRed);
            Main.Content = new UnirAPrograma();
        }

        private void btnValidarPrograma_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            CleanBackgroundbtn();
            btnValidarPrograma.Background = new SolidColorBrush(Colors.DarkRed);
            Main.Content = new AceptarPrograma();
        }


        private void btnCerrarSesion_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            InicioSesion ini = new InicioSesion();
            ini.Show();
            Close();
        }

        private void CleanBackgroundbtn()
        {
            btnCertificado.Background = null;
            btnMantendor.Background = null;
            btnNotas.Background = null;
            btnPostulacionesAlumnos.Background = null;
            btnPostulacionesFamilias.Background = null;
            btnPostularAPrograma.Background = null;
            btnReporte.Background = null;
            btnValidarPrograma.Background = null;
        }



    }
}

