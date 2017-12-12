using Negocio.Portafolio;
using Negocio.Portafolio.ViewClasses.FamiliaPostulantes;
using System;
using System.Linq;
using System.Windows.Controls;

namespace WPF.Portafolio.Pages.Familias
{
    /// <summary>
    /// Lógica de interacción para ListasFamiliasPostulantes.xaml
    /// </summary>
    public partial class ListasFamiliasPostulantes : Page
    {
        public ListasFamiliasPostulantes()
        {
            InitializeComponent();

            CargacbEstado();

            CargardgPostulantes();
        }

        private void CargacbEstado()
        {
            if (cbEstado.HasItems == false)
            {
                cbEstado.Items.Add("Mostrar todos");
                foreach (var estado in Enum.GetValues(typeof(EstadoFamilia)))
                {
                    cbEstado.Items.Add(estado);
                }
                cbEstado.SelectedIndex = 0;
            }
        }

        private void CargardgPostulantes()
        {
            ServiciosWCF.Portafolio.Servicios svc = new ServiciosWCF.Portafolio.Servicios();

            VFamiliasPostulantesCollection listaPostulantes = new VFamiliasPostulantesCollection(svc.BuscarFamiliaNombreApellido(txtNombreApellido.Text));
            
            dgPostulantes.ItemsSource = listaPostulantes;
        }


        private void txtNombreApellido_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                CargarPorNombreEstado(txtNombreApellido.Text, cbEstado.SelectedValue);
            }
            catch (Exception)
            {
                CargardgPostulantes();
            }
        }

        private void cbEstado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                CargarPorNombreEstado(txtNombreApellido.Text, cbEstado.SelectedValue);
            }
            catch (Exception)
            {
                CargardgPostulantes();
            }
        }

        private void CargarPorNombreEstado(string txtbox, object cbox)
        {
            ServiciosWCF.Portafolio.Servicios svc = new ServiciosWCF.Portafolio.Servicios();

            VFamiliasPostulantesCollection list = new VFamiliasPostulantesCollection(svc.BuscarFamiliaNombreApellido(txtbox));

            if (cbEstado.SelectedIndex != 0)
            {
                dgPostulantes.ItemsSource = list.Where(f => f.Estado.Equals(cbox.ToString()));
            }
            else
            {
                dgPostulantes.ItemsSource = list;
            }
        }


        private void dgPostulantes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dgPostulantes.SelectedIndex != -1)
                {
                    VFamiliasPostulantes vfam = (VFamiliasPostulantes)dgPostulantes.SelectedItem;
                    FamiliaAnfitriona fam = new FamiliaAnfitriona();
                    fam.Identificador = vfam.Identificacion;
                    fam.LeerPorIdentificador();
                    DetallePostulacionFamilia detallefamilia = new DetallePostulacionFamilia(fam);

                    dgPostulantes.SelectedIndex = -1;

                    detallefamilia.ShowDialog();
                }
            }
            catch
            {
                CargardgPostulantes();
            }
        }
    }
}
