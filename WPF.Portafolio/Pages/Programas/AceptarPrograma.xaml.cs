using Negocio.Portafolio;
using Negocio.Portafolio.ViewClasses.ProgramasFinalizados;
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
    /// Lógica de interacción para AceptarPrograma.xaml
    /// </summary>
    public partial class AceptarPrograma : Page
    {
        public AceptarPrograma()
        {
            InitializeComponent();

            CargardgProgramas();
        }

        private void dgPrograma_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                if (dgPrograma.SelectedIndex != -1)
                {
                    ServiciosWCF.Portafolio.Servicios svc = new ServiciosWCF.Portafolio.Servicios();

                    VProgramasPostulados programa = (VProgramasPostulados)dgPrograma.SelectedItem;
                    
                    ActividadCollection listaActividades = new ActividadCollection(svc.LeerActividadesPrograma(programa.IdPrograma));

                    dgActvidades.ItemsSource = listaActividades;
                }
            }
            catch
            {
                CargardgProgramas();
            }
        }

        private void CargardgProgramas()
        {
            try
            {
                dgPrograma.ItemsSource = null;

                ServiciosWCF.Portafolio.Servicios svc = new ServiciosWCF.Portafolio.Servicios();

                VProgramasPostuladosCollection listaProgramas = new VProgramasPostuladosCollection().LeerProgramasFinalizados();
                
                dgPrograma.ItemsSource = listaProgramas;
                
                dgActvidades.ItemsSource = null;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void btnPostular_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (dgPrograma.SelectedIndex != -1)
                {
                    ServiciosWCF.Portafolio.Servicios svc = new ServiciosWCF.Portafolio.Servicios();

                    VProgramasPostulados vprograma = (VProgramasPostulados)dgPrograma.SelectedItem;

                    Programa programa = new Programa();
                    programa.IdPrograma = vprograma.IdPrograma;
                    if (programa.Read())
                    {
                        programa.Estado = "Publicado";
                        if (programa.Update())
                        {
                            string mensaje = "Programa validado y publicado";
                            MessageBox.Show(mensaje, "Solicitud Aceptada");
                        }
                        else
                        {
                            MessageBox.Show("No se pudo aceptar la postulación");
                        }
                       
                    }
                    else
                    {
                        MessageBox.Show("No se pudo aceptar la postulación");

                    }
                    CargardgProgramas();
                }
            }
            catch
            {
                CargardgProgramas();
            }
        }

        private void btnRechazar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgPrograma.SelectedIndex != -1)
                {
                    ServiciosWCF.Portafolio.Servicios svc = new ServiciosWCF.Portafolio.Servicios();

                    VProgramasPostulados vprograma = (VProgramasPostulados)dgPrograma.SelectedItem;

                    Programa programa = new Programa();
                    programa.IdPrograma = vprograma.IdPrograma;
                    if (programa.Read())
                    {
                        programa.Estado = "Creado";
                        programa.IdInstitucion = null;
                        if (programa.Update())
                        {
                            string mensaje = "El programa estara volverá a estar disponible para que otro CEL pueda postular";
                            MessageBox.Show(mensaje, "Solicitud rechazada");
                        }
                        else
                        {
                            MessageBox.Show("No se pudo rechazar la postulación");
                        }

                    }
                    else
                    {
                        MessageBox.Show("No se pudo rechazar la postulación");

                    }

                    CargardgProgramas();
                }
            }
            catch (Exception)
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
                    VProgramasPostuladosCollection listaProgramas = new VProgramasPostuladosCollection().LeerProgramasFinalizados();

                    dgPrograma.ItemsSource = listaProgramas.Where(p => p.NombrePrograma.ToUpper().Equals(txtNombrePrograma.Text.ToUpper()));
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
