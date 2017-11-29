using MahApps.Metro.Controls;
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
using Negocio.Portafolio.ViewClasses;

namespace WPF.Portafolio.Pages.Programas
{
    /// <summary>
    /// Lógica de interacción para AgregarActividad.xaml
    /// </summary>
    public partial class AgregarActividad : MetroWindow
    {
        private VActividad vactividad = null;
        private CrearPrograma crearPrograma;

        public AgregarActividad(CrearPrograma crearPrograma)
        {
            InitializeComponent();

            this.crearPrograma = crearPrograma;

            txtActividad.Focus();
        }

        public AgregarActividad(CrearPrograma crearPrograma, VActividad actividad)
        {
            InitializeComponent();

            this.crearPrograma = crearPrograma;

            txtActividad.Focus();

            vactividad = actividad;
            CargarActividad(actividad);
        }

        private void CargarActividad(VActividad actividad)
        {
            txtActividad.Text = actividad.NombreActividad;
            txtDescripcion.Text = actividad.Descripcion;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (vactividad == null)
            {
                if (MessageBox.Show("Desea agregar esta actividad?", "Agregar Actividad", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        Actividad actividad = new Actividad()
                        {
                            NombreActividad = txtActividad.Text,
                            Descripcion = txtDescripcion.Text
                        };

                        MessageBox.Show("Actividad agregada!", "Agregada");
                        this.Close();
                        crearPrograma.actividadCollection.Add(new VActividad(actividad));
                    }
                    catch (Exception exep)
                    {
                        MessageBox.Show(exep.Message, "Error");
                    }

                }
            }
            else
            {
                if (MessageBox.Show("Desea Modificar esta actividad?", "Modificar Actividad", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    try
                    {
                        Actividad actividad = new Actividad()
                        {
                            NombreActividad = txtActividad.Text,
                            Descripcion = txtDescripcion.Text
                        };

                        MessageBox.Show("Actividad modificada!", "Modificada");
                        this.Close();
                        crearPrograma.actividadCollection[crearPrograma.dgActividades.SelectedIndex] = new VActividad(actividad);
                    }
                    catch (Exception exep)
                    {
                        MessageBox.Show(exep.Message, "Error");
                    }
                }
            }

        }
    }
}
