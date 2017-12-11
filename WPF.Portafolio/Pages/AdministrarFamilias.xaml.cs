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
    public partial class AdministrarFamilias : Page
    {
        public AdministrarFamilias()
        {
            InitializeComponent();

            ListarDGFamilias();
            ListarCBEstado();
            MostrarPaises();
        }

        private void MostrarPaises()
        {
            ServiciosWCF.Portafolio.Servicios svc = new ServiciosWCF.Portafolio.Servicios();

            PaisCollection paisCollection = new PaisCollection(svc.LeerTodosPaises());

            cbPais.ItemsSource = paisCollection;

            cbPais.DisplayMemberPath = "NombrePais";
            cbPais.SelectedValuePath = "IdPais";

            cbPais.SelectedIndex = 0;
        }
        private void MostrarCiudades(int pais)
        {
            ServiciosWCF.Portafolio.Servicios svc = new ServiciosWCF.Portafolio.Servicios();

            CiudadCollection ciudadCollection = new CiudadCollection(svc.LeerCiudades(pais));

            cbCiudad.ItemsSource = ciudadCollection;

            cbCiudad.DisplayMemberPath = "NombreCiudad";
            cbCiudad.SelectedValuePath = "IdCiudad";

            cbCiudad.SelectedIndex = 0;
        }
        private void ListarCBEstado()
        {
            cbEstado.ItemsSource = Enum.GetValues(typeof(EstadoFamilia));
            cbEstado.SelectedItem = EstadoFamilia.Registrado;
        }

        private void ListarDGFamilias()
        {
            ServiciosWCF.Portafolio.Servicios svc = new ServiciosWCF.Portafolio.Servicios();
            FamiliaAnfitrionaCollection list = new FamiliaAnfitrionaCollection(svc.LeerTodasFamiliasAnfitrionas());

            dgFamilias.ItemsSource = list;
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FamiliaAnfitriona familiaAnfitriona = new FamiliaAnfitriona();

                familiaAnfitriona.IdFamilia= int.Parse(txtIdFamilia.Text.Trim());

                familiaAnfitriona.Nombres= txtNombres.Text.Trim();
                familiaAnfitriona.ApePaterno= txtApePaterno.Text.Trim();
                familiaAnfitriona.ApeMaterno= txtApeMaterno.Text.Trim();
                familiaAnfitriona.Identificador= txtIdentificador.Text.Trim();
                familiaAnfitriona.Correo= txtCorreo.Text.Trim();
                familiaAnfitriona.Telefono= int.Parse(txtTelefono.Text.Trim());
                familiaAnfitriona.Direccion= txtDireccion.Text.Trim();
                familiaAnfitriona.IdPais = (int)cbPais.SelectedValue;
                familiaAnfitriona.IdCiudad = (int)cbCiudad.SelectedValue;
                familiaAnfitriona.Estado = (EstadoFamilia)cbEstado.SelectedValue;
                

                ServiciosWCF.Portafolio.Servicios svc = new ServiciosWCF.Portafolio.Servicios();
                string xml = familiaAnfitriona.Serializar();

                if (svc.CrearFamiliaAnfitriona(xml))
                {
                    MessageBox.Show("Familia Creada");
                    ListarDGFamilias();
                    LimpiarCampos();
                }
                else {
                    throw new Exception("Error al crear familia");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FamiliaAnfitriona familiaAnfitriona = new FamiliaAnfitriona();

                if (txtIdentificador.Text != string.Empty)
                {
                    familiaAnfitriona.Identificador= txtIdentificador.Text.Trim();

                    ServiciosWCF.Portafolio.Servicios svc = new ServiciosWCF.Portafolio.Servicios();
                    string xml = familiaAnfitriona.Serializar();

                    familiaAnfitriona = new FamiliaAnfitriona(svc.LeerFamiliaIdentificador(xml));

                    txtIdFamilia.Text = familiaAnfitriona.IdFamilia.ToString();
                    txtNombres.Text = familiaAnfitriona.Nombres;
                    txtApePaterno.Text = familiaAnfitriona.ApePaterno;
                    txtApeMaterno.Text = familiaAnfitriona.ApeMaterno;
                    txtCorreo.Text = familiaAnfitriona.Correo;
                    txtTelefono.Text = familiaAnfitriona.Telefono.ToString();
                    txtDireccion.Text = familiaAnfitriona.Direccion;
                    cbPais.SelectedValue = familiaAnfitriona.IdPais;
                    cbCiudad.SelectedValue = familiaAnfitriona.IdCiudad;
                    cbEstado.SelectedValue = familiaAnfitriona.Estado;
                }
                else
                {
                    throw new Exception("No se encontró Familia");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FamiliaAnfitriona familiaAnfitriona = new FamiliaAnfitriona();

                familiaAnfitriona.IdFamilia = int.Parse(txtIdFamilia.Text.Trim());

                familiaAnfitriona.Nombres = txtNombres.Text.Trim();
                familiaAnfitriona.ApePaterno = txtApePaterno.Text.Trim();
                familiaAnfitriona.ApeMaterno = txtApeMaterno.Text.Trim();
                familiaAnfitriona.Identificador = txtIdentificador.Text.Trim();
                familiaAnfitriona.Correo = txtCorreo.Text.Trim();
                familiaAnfitriona.Telefono = int.Parse(txtTelefono.Text.Trim());
                familiaAnfitriona.Direccion = txtDireccion.Text.Trim();
                familiaAnfitriona.IdPais = (int)cbPais.SelectedValue;
                familiaAnfitriona.IdCiudad = (int)cbCiudad.SelectedValue;
                familiaAnfitriona.Estado = (EstadoFamilia)cbEstado.SelectedValue;

                ServiciosWCF.Portafolio.Servicios svc = new ServiciosWCF.Portafolio.Servicios();
                string xml = familiaAnfitriona.Serializar();

                if (svc.ActualizarFamiliaAnfitriona(xml))
                {
                    MessageBox.Show("Familia editada");
                    ListarDGFamilias();
                }
                else
                {
                    throw new Exception("Error al actualizar familia");
                }
            }
            catch(Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FamiliaAnfitriona familiaAnfitriona = new FamiliaAnfitriona();

                familiaAnfitriona.IdFamilia = int.Parse(txtIdFamilia.Text.Trim());

                ServiciosWCF.Portafolio.Servicios svc = new ServiciosWCF.Portafolio.Servicios();
                string xml = familiaAnfitriona.Serializar();

                if (svc.EliminarFamiliaAnfitriona(xml))
                {
                    MessageBox.Show("Familia eliminada");
                    ListarDGFamilias();
                    LimpiarCampos();
                }
                else
                {
                    throw new Exception("Error al eliminar familia");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            
        }

        private void LimpiarCampos()
        {
            txtIdFamilia.Text = string.Empty;
            txtNombres.Text = string.Empty;
            txtApePaterno.Text = string.Empty;
            txtApeMaterno.Text = string.Empty;
            txtIdentificador.Text = string.Empty;
            txtCorreo.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            cbPais.SelectedIndex = 0;
            cbCiudad.SelectedIndex = 0;
            cbEstado.SelectedIndex = 0;
        }

        private void cbPais_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbPais.SelectedIndex != -1)
            {
                MostrarCiudades((int)cbPais.SelectedValue);
            }
        }
    }
}
