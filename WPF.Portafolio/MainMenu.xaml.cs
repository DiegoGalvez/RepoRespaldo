﻿using System;
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
using System.Windows.Shapes;
using Negocio.Portafolio;
using ServiciosWCF.Portafolio;
using WPF.Portafolio.Pages;
using WPF.Portafolio.Pages.Familias;
using WPF.Portafolio.Pages.Instituciones;
using MahApps.Metro.Controls;
using System.Windows.Media.Effects;
using MahApps.Metro.Controls.Dialogs;
using System.Windows.Media.Animation;
using WPF.Portafolio.Pages.Mantenedores;
using WPF.Portafolio.Pages.Certificado;

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
            Main.Content = new AcercaDe();
            if (!LlamarLogin())
            {
                Application.Current.Shutdown(-1);
            }
            else
            {
                SideMenuRol();
            }
        }

        private void SideMenuRol()
        {
            //ROLES  Administrador, Alumnos, Familia, EncargadoCEM, EncargadoCEL

            switch (UsuarioActual.Rol)
            {
                case "Adminstrador":
                    //Menu para Administrador (Todas las opciones disponibles)
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
                    filaUnirAPrograma.Height = new GridLength(0);
                    btnPostulacionesAlumnos.Visibility = Visibility.Collapsed;
                    imgPostulacionesAlumnos.Visibility = Visibility.Collapsed;
                    break;
                case "EncargadoCEL":
                    //Menu para Encargado CEL
                    filaPostulacionesA.Height = new GridLength(0);
                    btnPostulacionesAlumnos.Visibility = Visibility.Collapsed;
                    imgPostulacionesAlumnos.Visibility = Visibility.Collapsed;

                    filaReporte.Height = new GridLength(0);
                    btnReporte.Visibility = Visibility.Collapsed;
                    imgReporte.Visibility = Visibility.Collapsed;

                    filaCertficado.Height = new GridLength(0);
                    btnCertificado.Visibility = Visibility.Collapsed;
                    imgCertificado.Visibility = Visibility.Collapsed;
                    break;
            }
        }
        
        private bool LlamarLogin()
        {
            InicioSesion login = new InicioSesion();
            login.ShowDialog();

            if (login.isAceptado)
            {
                CargarNombreUsuario(login.txtUser.Text);
            }
            return login.isAceptado;
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

        private void CerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            UsuarioActual = null;
            MainMenu menu = new MainMenu();
            //login.Aceptado = false;
            menu.Show();
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
            Main.Content = new PostulacionAlumno();
        }
        
        private void btnNotas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new ListaNotas();
        }

        private void btnPostulacionesFamilias_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new ListasFamiliasPostulantes();
        }
        
        private void btnMantendor_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new TabsMantenedor();
        }

        private void btnReporte_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void btnCertificado_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new ProgramasFinalizados();
        }

        private void btnPostularAPrograma_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void btnCerrarSesion_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        
    }
}

