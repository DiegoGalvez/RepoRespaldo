using MahApps.Metro.Controls;
using Negocio.Portafolio;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace WPF.Portafolio.Pages.Familias
{
    /// <summary>
    /// Lógica de interacción para DetallePostulacionFamilia.xaml
    /// </summary>
    public partial class DetallePostulacionFamilia : MetroWindow
    {
        public DetallePostulacionFamilia(FamiliaAnfitriona familia)
        {
            InitializeComponent();
            
            CargaDatos(familia);
        }

        private void CargaDatos(FamiliaAnfitriona familia)
        {   
            lblNombre.Content = string.Format("{0} {1} {2}", familia.Nombres, familia.ApePaterno, familia.ApeMaterno) ;

            txtIdentificacion.Text = familia.Identificador;
            txtTelefono.Text = familia.Telefono.ToString();
            txtCorreo.Text = familia.Correo;
            txtDireccion.Text = familia.Direccion;
            txtCiudad.Text = familia.IdCiudad.ToString();
            txtPais.Text = familia.IdPais.ToString();

            //carImagenes.ItemsSource = new ObservableCollection<string>() { "Item1", "Item2", "Item3", "Item4" };

        }
    }
}
