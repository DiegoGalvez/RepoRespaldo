using MahApps.Metro.Controls;
using Negocio.Portafolio;
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

namespace WPF.Portafolio.Pages.Programas
{
    /// <summary>
    /// Lógica de interacción para ModificarPrograma.xaml
    /// </summary>
    public partial class ModificarPrograma : MetroWindow
    {
        AdministrarProgramas administrarProgramas;
        private int idPrograma;

        public ModificarPrograma(AdministrarProgramas administrarProgramas, Programa programa)
        {
            InitializeComponent();

            this.administrarProgramas = administrarProgramas;

            idPrograma = programa.IdPrograma;

            CargarCbTipo();
            CargarCbEstado();
            CargarInstituciones();

            CargarPrograma(programa);

            CargarActividades(programa.IdPrograma);
        }

        private void CargarInstituciones()
        {
            ServiciosWCF.Portafolio.Servicios svc = new ServiciosWCF.Portafolio.Servicios();

            InstitucionCollection institucionCollection = new InstitucionCollection(svc.LeerTodasInstituciones());

            Institucion institucion = new Institucion()
            {
                IdInstitucion = 0,
                Nombres = "Sin institucion"
            };

            institucionCollection.Insert(0, institucion);

            cbInstitucion.ItemsSource = institucionCollection;

            cbInstitucion.DisplayMemberPath = "Nombres";
            cbInstitucion.SelectedValuePath = "IdInstitucion";
        }

        private void CargarCbEstado()
        {
            cbEstado.ItemsSource = Enum.GetValues(typeof(EstadoPrograma));
        }

        private void CargarCbTipo()
        {
            cbTipoPrograma.ItemsSource = Enum.GetValues(typeof(TipoCursos));
        }

        private void CargarPrograma(Programa programa)
        {
            txtNombrePrograma.Text = programa.NombrePrograma;
            txtDescripcion.Text = programa.Descripcion;
            dtInicio.Text = programa.FechaInicio.ToString();
            dtTermino.Text = programa.FechaTermino.ToString();
            cbTipoPrograma.SelectedItem = programa.TipoCurso;
            txtCupos.Text = programa.Cupos.ToString();

            if (programa.IdInstitucion == null)
            {
                cbInstitucion.SelectedIndex = 0;
            }
            else
            {
                cbInstitucion.SelectedValue = programa.IdInstitucion;
            }
            
            cbEstado.SelectedItem = programa.Estado;

        }

        private void CargarActividades(int idPrograma)
        {
            ServiciosWCF.Portafolio.Servicios svc = new ServiciosWCF.Portafolio.Servicios();

            ActividadCollection listaActividades = new ActividadCollection(svc.LeerActividadesPrograma(idPrograma));

            dgActividades.ItemsSource = listaActividades;
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Desea modificar este programa?", "Modificar Programa", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    ServiciosWCF.Portafolio.Servicios svc = new ServiciosWCF.Portafolio.Servicios();

                    Programa programa = new Programa()
                    {
                        IdPrograma = idPrograma,
                        NombrePrograma = txtNombrePrograma.Text,
                        Descripcion = txtDescripcion.Text,
                        Cupos = int.Parse(txtCupos.Text),
                        FechaInicio = DateTime.Parse(dtInicio.Text),
                        FechaTermino = DateTime.Parse(dtTermino.Text),
                        TipoCurso = (TipoCursos)Enum.Parse(typeof(TipoCursos), cbTipoPrograma.Text),
                        Estado = (EstadoPrograma)Enum.Parse(typeof(EstadoPrograma), cbEstado.Text)
                    };

                    if ((int)cbInstitucion.SelectedValue == 0)
                    {
                        programa.IdInstitucion = null;
                    }
                    else
                    {
                        programa.IdInstitucion = (int)cbInstitucion.SelectedValue;
                    }
                    

                    if (svc.ActualizarPrograma(programa.Serializar()))
                    {
                        MessageBox.Show("Programa actualizado!", "Actualizado");
                        this.Close();
                        administrarProgramas.CargarProgramas();
                    }
                    else
                    {
                        throw new Exception("No se pudo actualizar programa");
                    }
                }
                catch (Exception exep)
                {
                    MessageBox.Show(exep.Message, "Error");
                }
            }
        }
    }
}
