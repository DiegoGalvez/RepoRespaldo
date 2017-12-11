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
using System.Windows.Shapes;
using Negocio.Portafolio;
using ServiciosWCF.Portafolio;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Behaviours;

namespace WPF.Portafolio
{
    /// <summary>
    /// Lógica de interacción para InicioSesion.xaml
    /// </summary>
    public partial class InicioSesion : MetroWindow
    {
        public bool isAceptado = false;

        public InicioSesion()
        {
            InitializeComponent();

            txtUser.Focus();
        }

        private async void btnIniciar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string _mensaje = "Bienvenido!";
                
                Usuario user = new Usuario();
                user.NomUsuario = txtUser.Text.ToString();
                user.Password = txtPassword.Password;
                
                ServiciosWCF.Portafolio.Servicios svc = new ServiciosWCF.Portafolio.Servicios();

                string xml = user.Serializar();

                if (svc.validarUsuario(xml))
                {
                    user = new Usuario(svc.LeerUsuario(xml));
                    if (user.Rol != null && user.Rol != "Alumno" && user.Rol != "Familia")
                    {
                        _mensaje = string.Format("Bienvenido {0}", user.NomUsuario);
                        await this.ShowMessageAsync("Exito", _mensaje);
                        MainMenu menu = new MainMenu(user);
                        menu.Show();
                        Close();
                    }
                    else
                    {
                        string mensaje = string.Format("Aplicación no disponible para rol: {0}", user.Rol);
                        await this.ShowMessageAsync("Error de permisos", mensaje);
                    }
                    
                }
                else
                {
                    _mensaje = "Usuario o Contraseña incorrectos";
                    await this.ShowMessageAsync("Error", _mensaje);
                   
                }
                
            }
            catch (Exception)
            {
                throw;
            }

        }

        private void txtUser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key==Key.Enter)
            {
                btnIniciar_Click(sender, e);
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnIniciar_Click(sender, e);
            }
        }
        
    }
}
