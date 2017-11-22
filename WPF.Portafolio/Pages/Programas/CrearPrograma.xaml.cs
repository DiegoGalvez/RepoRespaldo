using Negocio.Portafolio;
using Negocio.Portafolio.ViewClasses;
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
        public List<VActividad> actividadCollection = new List<VActividad>();

        public CrearPrograma()
        {
            InitializeComponent();
            CargarCbTipo();
            CargarDgActividades();

            txtNombrePrograma.Focus();
        }

        public void CargarDgActividades()
        {
            dgActividades.ItemsSource = actividadCollection.ToList();
        }

        private void CargarCbTipo()
        {
            cbTipoPrograma.ItemsSource = Enum.GetValues(typeof(TipoCursos));
            cbTipoPrograma.SelectedItem = TipoCursos.Normal;
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            AgregarActividad agregarActividad = new AgregarActividad(this);
            agregarActividad.ShowDialog();

            CargarDgActividades();
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            txtNombrePrograma.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            dtInicio.Text = string.Empty;
            dtTermino.Text = string.Empty;
            cbTipoPrograma.SelectedIndex = 0;
            txtCupos.Text = string.Empty;
        }

        private void dgActividades_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgActividades.SelectedIndex != -1)
            {
                btnModificar.IsEnabled = true;
                btnBorrar.IsEnabled = true;
            }
            else
            {
                btnModificar.IsEnabled = false;
                btnBorrar.IsEnabled = false;
            }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            AgregarActividad agregarActividad = new AgregarActividad(this, (VActividad)dgActividades.SelectedItem);
            agregarActividad.ShowDialog();

            CargarDgActividades();
        }

        private void btnBorrar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Desea eliminar esta actividad?", "Eliminar actividad", MessageBoxButton.YesNo, MessageBoxImage.Error) == MessageBoxResult.Yes)
            {
                actividadCollection.RemoveAt(dgActividades.SelectedIndex);

                CargarDgActividades();

                MessageBox.Show("Actividad eliminada!", "Eliminada");
            }
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Desea agregar este programa?", "Agregar Programa", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {

                try
                {
                    ServiciosWCF.Portafolio.Servicios svc = new ServiciosWCF.Portafolio.Servicios();

                    Programa programa = new Programa()
                    {
                        NombrePrograma = txtNombrePrograma.Text,
                        Descripcion = txtDescripcion.Text,
                        Cupos = int.Parse(txtCupos.Text),
                        FechaInicio = DateTime.Parse(dtInicio.Text),
                        FechaTermino = DateTime.Parse(dtTermino.Text),
                        TipoCurso = (TipoCursos)Enum.Parse(typeof(TipoCursos), cbTipoPrograma.Text),
                        Estado = "Postulado"
                    };

                    if (svc.CrearPrograma(programa.Serializar()))
                    {
                        MessageBox.Show("Programa agregado!", "Agregado");

                        int id_programa_actual = svc.IdActualEntidadPrograma();

                        ConfirmarActividades(svc, id_programa_actual);

                        btnLimpiar_Click(sender, e);

                        actividadCollection = new List<VActividad>();

                        CargarDgActividades();
                    }
                    else
                    {
                        throw new Exception("No se pudo crear programa");
                    }
                }
                catch (Exception exep)
                {
                    MessageBox.Show(exep.Message, "Error");
                }

            }
        }

        private void ConfirmarActividades(ServiciosWCF.Portafolio.Servicios svc, int id_programa_actual)
        {
            foreach (VActividad item in actividadCollection)
            {
                Actividad actividad = new Actividad()
                {
                    NombreActividad = item.NombreActividad,
                    Descripcion = item.Descripcion
                };

                if (svc.CrearActividad(actividad.Serializar()))
                {
                    svc.EnlazarPrograma(id_programa_actual, svc.IdActualEntidadActividad());
                }
                else
                {
                    throw new Exception("No se pudo crear actividad");
                }
            }

            actividadCollection.Clear();
        }
    }
}
