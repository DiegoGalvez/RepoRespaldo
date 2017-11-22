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
    public partial class AdministrarAlumnos : Page
    {
        public AdministrarAlumnos()
        {
            InitializeComponent();

            ListarDGUsuario();
        }

        private void ListarDGUsuario()
        {
            ServiciosWCF.Portafolio.Servicios svc = new ServiciosWCF.Portafolio.Servicios();
            AlumnoCollection list = new AlumnoCollection(svc.LeerTodosAlumnos());

            dgUsuario.ItemsSource = list;
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Alumno _alumno = new Alumno();

                _alumno.IdAlumno = int.Parse(txtIdAlumno.Text.Trim());
                _alumno.Dv = txtDV.Text.Trim();
                _alumno.Nombre = txtNombres.Text.Trim();
                _alumno.ApePaterno = txtApePaterno.Text.Trim();
                _alumno.ApeMaterno = txtApeMaterno.Text.Trim();
                _alumno.Correo = txtCorreo.Text.Trim();
                _alumno.Telefono = int.Parse(txtTelefono.Text.Trim());
                _alumno.Reserva = int.Parse(txtReserva.Text.Trim());
                _alumno.EstadoMora = txtMora.Text.Trim();

                ServiciosWCF.Portafolio.Servicios svc = new ServiciosWCF.Portafolio.Servicios();
                string xml = _alumno.Serializar();

                if (svc.CrearAlumno(xml))
                {
                    MessageBox.Show("Alumno Creado");
                    ListarDGUsuario();
                }
                else {
                    MessageBox.Show("Error al crear alumno");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al crear alumno");
            }
            
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Alumno _alumno = new Alumno();

                if (txtIdAlumno.Text != null)
                {
                    _alumno.IdAlumno = int.Parse(txtIdAlumno.Text.Trim());

                    ServiciosWCF.Portafolio.Servicios svc = new ServiciosWCF.Portafolio.Servicios();
                    string xml = _alumno.Serializar();

                    _alumno = new Alumno(svc.LeerAlumno(xml));
                    if (_alumno != null)
                    {

                        txtIdAlumno.Text = _alumno.IdAlumno.ToString();
                        txtDV.Text = _alumno.Dv;
                        txtNombres.Text = _alumno.Nombre;
                        txtApePaterno.Text = _alumno.ApePaterno;
                        txtApeMaterno.Text = _alumno.ApeMaterno;
                        txtCorreo.Text = _alumno.Correo;
                        txtTelefono.Text = _alumno.Telefono.ToString();
                        txtReserva.Text = _alumno.Reserva.ToString();
                        txtMora.Text = _alumno.EstadoMora;
                    }
                    else
                    {
                        MessageBox.Show("No se encuentró alumno");
                    }
                }
                else
                {
                    MessageBox.Show("ID Alumno vacio");
                }
                
            }
            catch (Exception)
            {
                MessageBox.Show("Campos Nombre vacio");
            }
            
            
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            Alumno _alumno = new Alumno();

            _alumno.IdAlumno = int.Parse(txtIdAlumno.Text.Trim());
            _alumno.Dv = txtDV.Text.Trim();
            _alumno.Nombre = txtNombres.Text.Trim();
            _alumno.ApePaterno = txtApePaterno.Text.Trim();
            _alumno.ApeMaterno = txtApeMaterno.Text.Trim();
            _alumno.Correo = txtCorreo.Text.Trim();
            _alumno.Telefono = int.Parse(txtTelefono.Text.Trim());
            _alumno.Reserva = int.Parse(txtReserva.Text.Trim());
            _alumno.EstadoMora = txtMora.Text.Trim();

            ServiciosWCF.Portafolio.Servicios svc = new ServiciosWCF.Portafolio.Servicios();
            string xml = _alumno.Serializar();

            if (svc.ActualizarAlumno(xml))
            {
                MessageBox.Show("Alumno editado");
                ListarDGUsuario();
            }
            else {
                MessageBox.Show("Error al editar alumno");
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            Alumno _alumno = new Alumno();

            _alumno.IdAlumno = int.Parse(txtIdAlumno.Text.Trim());

            ServiciosWCF.Portafolio.Servicios svc = new ServiciosWCF.Portafolio.Servicios();
            string xml = _alumno.Serializar();

            if (svc.EliminarAlumno(xml))
            {
                MessageBox.Show("Alumno eliminado");
                ListarDGUsuario();
                LimpiarCampos();
            }
            else {
                MessageBox.Show("Error al eliminar alumno");
            }
        }


        private void LimpiarCampos()
        {
            txtIdAlumno.Text = string.Empty;
            txtDV.Text = string.Empty;
            txtNombres.Text = string.Empty;
            txtApePaterno.Text = string.Empty;
            txtApeMaterno.Text = string.Empty;
            txtCorreo.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtReserva.Text = string.Empty;
            txtMora.Text = string.Empty;
        }
    }
}
