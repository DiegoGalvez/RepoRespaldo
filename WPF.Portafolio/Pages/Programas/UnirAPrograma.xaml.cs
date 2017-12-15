using Negocio.Portafolio;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WPF.Portafolio.Pages.Programas
{
    /// <summary>
    /// Lógica de interacción para UnirAPrograma.xaml
    /// </summary>
    public partial class UnirAPrograma : Page
    {
        public UnirAPrograma()
        {
            InitializeComponent();

            CargardgProgramas();
        }

        private void CargardgProgramas()
        {
            try
            {
                dgPrograma.ItemsSource = null;

                ServiciosWCF.Portafolio.Servicios svc = new ServiciosWCF.Portafolio.Servicios();

                ProgramaCollection ProgramasPostulados = new ProgramaCollection(svc.LeerTodosProgramas());

                dgPrograma.ItemsSource = ProgramasPostulados.Where(p=>p.Estado.Equals("Creado"));

                dgActvidades.ItemsSource = null;
                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        private void dgPrograma_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                if (dgPrograma.SelectedIndex != -1)
                {
                    Programa programa = (Programa)dgPrograma.SelectedItem;

                    ServiciosWCF.Portafolio.Servicios svc = new ServiciosWCF.Portafolio.Servicios();

                    ActividadCollection listaActividades = new ActividadCollection(svc.LeerActividadesPrograma(programa.IdPrograma));

                    dgActvidades.ItemsSource = listaActividades;
                }
            }
            catch
            {
                CargardgProgramas();
            }
        }

        private void btnPostular_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (dgPrograma.SelectedIndex != -1)
                {
                    ServiciosWCF.Portafolio.Servicios svc = new ServiciosWCF.Portafolio.Servicios();

                    EncargadoCel encargado = new EncargadoCel();
                    encargado.IdEncargadoCel = MainMenu.UsuarioActual.IdEncargadoCel.GetValueOrDefault();
                    encargado = new EncargadoCel(svc.LeerEncargadoCel(encargado.Serializar()));

                    Programa programa = (Programa)dgPrograma.SelectedItem;
                    programa.IdInstitucion = encargado.IdInstitucion;
                    programa.Estado = EstadoPrograma.Postulado;
                    if (svc.ActualizarPrograma(programa.Serializar()))
                    {
                        string mensaje = "A la espera de un CEM que apruebe la solicitud ";
                        MessageBox.Show(mensaje, "Postulacion enviada");
                        
                    }
                    else
                    {
                        MessageBox.Show("No se pudo enviar la postulación");
                        
                    }
                    CargardgProgramas();
                }
            }
            catch
            {
                CargardgProgramas();
            }
        }

        private void txtNombrePrograma_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtNombrePrograma.Text == null || txtNombrePrograma.Text == string.Empty || txtNombrePrograma.Text == "")
                {
                    CargardgProgramas();
                    dgActvidades.ItemsSource = null;
                }
                else
                {
                    ServiciosWCF.Portafolio.Servicios svc = new ServiciosWCF.Portafolio.Servicios();
                    ProgramaCollection programas = new ProgramaCollection(svc.LeerTodosProgramas());
                    dgPrograma.ItemsSource = programas.Where(p => p.NombrePrograma.ToUpper().Contains(txtNombrePrograma.Text.ToUpper()) && p.Estado.Equals("Creado"));
                    if (dgPrograma.Items.Count == 0)
                    {
                        dgActvidades.ItemsSource = null;
                    }
                }

            }
            catch (Exception)
            {
                CargardgProgramas();
            }
        }
    }
}
