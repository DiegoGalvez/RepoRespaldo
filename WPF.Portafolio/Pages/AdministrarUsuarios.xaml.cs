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
using Negocio.Portafolio;
using ServiciosWCF.Portafolio;

namespace WPF.Portafolio.Pages
{
    /// <summary>
    /// Lógica de interacción para AdministrarUsuarios.xaml
    /// </summary>
    public partial class AdministrarUsuarios : Page
    {
        public AdministrarUsuarios()
        {
            InitializeComponent();

            ListarDGUsuario();
            ListarCBTipoRol();
        }

        private void ListarCBTipoRol()
        {
            cbTipoRol.ItemsSource = Enum.GetValues(typeof(RolAsignado));
            cbTipoRol.SelectedItem = RolAsignado.Administrador;
        }

        private void ListarDGUsuario()
        {
            ServiciosWCF.Portafolio.Servicios svc = new ServiciosWCF.Portafolio.Servicios();
            UsuarioCollection list = new UsuarioCollection(svc.LeerTodosUsuarios());

            dgUsuario.ItemsSource = list;
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Usuario _usuario = new Usuario();

                _usuario.NomUsuario = txtUsuario.Text.Trim();
                _usuario.Password = txtPassword.Password.ToString();
                switch (cbTipoRol.SelectedIndex)
                {
                    // Roles 0=Administrador, 1=Alumnos, 2=Familia, 3=EncargadoCEM, 4=EncargadoCEL
                    case 0:
                        _usuario.Rol = "Administrador";
                        _usuario.IdAdministrativo = int.Parse(txtIDRol.Text.Trim());
                        _usuario.IdAlumno = null;
                        _usuario.IdFamilia = null;
                        _usuario.IdEncargadoCel = null;
                        _usuario.IdEncargadoCem = null;
                        break;
                    case 1:
                        _usuario.Rol = "Alumno";
                        _usuario.IdAdministrativo = null;
                        _usuario.IdAlumno = int.Parse(txtIDRol.Text.Trim());
                        _usuario.IdFamilia = null;
                        _usuario.IdEncargadoCel = null;
                        _usuario.IdEncargadoCem = null;

                        break;
                    case 2:
                        _usuario.Rol = "Familia";
                        _usuario.IdAdministrativo = null;
                        _usuario.IdAlumno = null;
                        _usuario.IdFamilia = int.Parse(txtIDRol.Text.Trim());
                        _usuario.IdEncargadoCel = null;
                        _usuario.IdEncargadoCem = null;
                        break;
                    case 3:
                        _usuario.Rol = "EncargadoCEM";
                        _usuario.IdAdministrativo = null;
                        _usuario.IdAlumno = null;
                        _usuario.IdFamilia = null;
                        _usuario.IdEncargadoCel = null;
                        _usuario.IdEncargadoCem = int.Parse(txtIDRol.Text.Trim()); 
                        break;
                    case 4:
                        _usuario.Rol = "EncargadoCEL";
                        _usuario.IdAdministrativo = null;
                        _usuario.IdAlumno = null;
                        _usuario.IdFamilia = null;
                        _usuario.IdEncargadoCem = null;
                        _usuario.IdEncargadoCel = int.Parse(txtIDRol.Text.Trim());
                        break;
                }

                ServiciosWCF.Portafolio.Servicios svc = new ServiciosWCF.Portafolio.Servicios();
                string xml = _usuario.Serializar();

                if (svc.CrearUsuario(xml))
                {
                    MessageBox.Show("Usuario Creado");
                    ListarDGUsuario();
                    LimpiarCampos();
                }
                else {
                    MessageBox.Show("Error al crear usuario");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al crear usuario");
            }
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Usuario _usuario = new Usuario();

                if (txtUsuario.Text != null && txtUsuario.Text != string.Empty)
                {
                    _usuario.NomUsuario = txtUsuario.Text.Trim();

                    ServiciosWCF.Portafolio.Servicios svc = new ServiciosWCF.Portafolio.Servicios();
                    string xml = _usuario.Serializar();

                    _usuario = new Usuario(svc.LeerUsuario(xml));

                    txtIdUsuario.Text = _usuario.IdUsuario.ToString();
                    txtUsuario.Text = _usuario.NomUsuario.ToString();
                    txtPassword.Password = _usuario.Password.ToString();
                    cbTipoRol.SelectedItem = _usuario.Rol;

                    // Roles 0=Administrador, 1=Alumnos, 2=Familia, 3=EncargadoCEM, 4=EncargadoCEL
                    switch (_usuario.Rol)
                    {
                        case "Administrador":
                            txtIDRol.Text = _usuario.IdAdministrativo.ToString();
                            break;
                        case "Alumno":
                            txtIDRol.Text = _usuario.IdAlumno.ToString();
                            break;
                        case "Familia":
                            txtIDRol.Text = _usuario.IdFamilia.ToString();
                            break;
                        case "EncargadoCEM":
                            txtIDRol.Text = _usuario.IdEncargadoCem.ToString();
                            break;
                        case "EncargadoCEL":
                            txtIDRol.Text = _usuario.IdEncargadoCel.ToString();
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("No se encontró alumno");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("ingrese un ID");
            }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Usuario _usuario = new Usuario();

                if (txtUsuario.Text != null)
                {
                    _usuario.NomUsuario = txtUsuario.Text.Trim();

                    if (_usuario.Read())
                    {
                        _usuario.IdUsuario = int.Parse(txtIdUsuario.Text.Trim());
                        _usuario.Password = txtPassword.Password.ToString();
                        switch (cbTipoRol.SelectedIndex)
                        {
                            // Roles 0=Administrador, 1=Alumnos, 2=Familia, 3=EncargadoCEM, 4=EncargadoCEL
                            case 0:
                                _usuario.Rol = "Administrador";
                                _usuario.IdAdministrativo = int.Parse(txtIDRol.Text.Trim());
                                _usuario.IdAlumno = null;
                                _usuario.IdFamilia = null;
                                _usuario.IdEncargadoCel = null;
                                break;
                            case 1:
                                _usuario.Rol = "Alumnos";
                                _usuario.IdAdministrativo = null;
                                _usuario.IdAlumno = int.Parse(txtIDRol.Text.Trim());
                                _usuario.IdFamilia = null;
                                _usuario.IdEncargadoCel = null;

                                break;
                            case 2:
                                _usuario.Rol = "Familia";
                                _usuario.IdAdministrativo = null;
                                _usuario.IdAlumno = null;
                                _usuario.IdFamilia = int.Parse(txtIDRol.Text.Trim());
                                _usuario.IdEncargadoCel = null;
                                break;
                            case 3:
                                _usuario.Rol = "EncargadoCEM";
                                _usuario.IdAdministrativo = null;
                                _usuario.IdAlumno = null;
                                _usuario.IdFamilia = null;
                                _usuario.IdEncargadoCel = int.Parse(txtIDRol.Text.Trim());
                                break;
                            case 4:
                                _usuario.Rol = "EncargadoCEL";
                                _usuario.IdAdministrativo = null;
                                _usuario.IdAlumno = null;
                                _usuario.IdFamilia = null;
                                _usuario.IdEncargadoCel = int.Parse(txtIDRol.Text.Trim());
                                break;
                        }

                        ServiciosWCF.Portafolio.Servicios svc = new ServiciosWCF.Portafolio.Servicios();
                        string xml = _usuario.Serializar();

                        if (svc.ActualizarUsuario(xml))
                        {
                            MessageBox.Show("Usuario Modificado");
                            ListarDGUsuario();
                        }
                        else {
                            MessageBox.Show("Error al modificar usuario");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Usuario no existe");
                    }
                }
                else
                {
                    MessageBox.Show("Nombre Usuario vacio");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            Usuario _usuario = new Usuario();

            _usuario.NomUsuario = txtUsuario.Text.Trim();

            ServiciosWCF.Portafolio.Servicios svc = new ServiciosWCF.Portafolio.Servicios();
            string xml = _usuario.Serializar();

            if (svc.EliminarUsuario(xml))
            {
                MessageBox.Show("Usuario eliminado");
                ListarDGUsuario();
                LimpiarCampos();
            }
            else {
                MessageBox.Show("Error al eliminar usuario");
            }
        }

        private void LimpiarCampos()
        {
            txtIdUsuario.Text = string.Empty;
            txtUsuario.Text = string.Empty;
            txtPassword.Password = string.Empty;
            cbTipoRol.SelectedIndex = 0;
            txtIDRol.Text = string.Empty;
        }

        
    }
}
