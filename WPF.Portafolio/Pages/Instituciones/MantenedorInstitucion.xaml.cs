using MahApps.Metro.Controls;
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
using System.Windows.Shapes;

namespace WPF.Portafolio.Pages.Instituciones
{
    /// <summary>
    /// Lógica de interacción para MantenedorInstitucion.xaml
    /// </summary>
    public partial class MantenedorInstitucion : MetroWindow
    {
        Institucion institucion = null;
        ListaInstituciones listaInstituciones;

        public MantenedorInstitucion(ListaInstituciones listaInstituciones)
        {
            InitializeComponent();

            this.listaInstituciones = listaInstituciones;

            MostrarPaises();
        }

        public MantenedorInstitucion(int idInstitucion, ListaInstituciones listaInstituciones)
        {
            InitializeComponent();

            this.listaInstituciones = listaInstituciones;

            MostrarPaises();
            
            ServiciosWCF.Portafolio.Servicios svc = new ServiciosWCF.Portafolio.Servicios();

            institucion = new Institucion(svc.LeerInstitucion(idInstitucion));

            CargarComponentes(institucion);

            CargarInstitucion(institucion);

        }

        private void CargarInstitucion(Institucion institucion)
        {
            txtNombres.Text = institucion.Nombres;
            txtCorreo.Text = institucion.Correo;
            txtTelefono.Text = institucion.Telefono.ToString();
            txtPaginaWEB.Text = institucion.PaginaWeb;
            txtDireccion.Text = institucion.Direcion;
            cbPais.SelectedValue = institucion.IdPais;

            MostrarCiudades((int)cbPais.SelectedValue);
            cbCiudad.SelectedValue = institucion.IdCiudad;
        }

        private void CargarComponentes(Institucion institucion)
        {
            lblInstitucion.Content = institucion.Nombres;

            lblIdInstitucion.Visibility = Visibility.Visible;

            lblIdInstitucion.Content = institucion.IdInstitucion;

            btnAccion.Content = "Modificar";

            btnEliminar.Visibility = Visibility.Visible;
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
        
        private void cbPais_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbPais.SelectedIndex != -1)
            {
                MostrarCiudades((int)cbPais.SelectedValue);
            }
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

        private void btnAccion_Click(object sender, RoutedEventArgs e)
        {
            if (institucion == null)
            {
                if (MessageBox.Show("Desea agregar esta institucion?", "Agregar Institucion", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {

                    try
                    {
                        ServiciosWCF.Portafolio.Servicios svc = new ServiciosWCF.Portafolio.Servicios();

                        Institucion institucion = new Institucion()
                        {
                            Nombres = txtNombres.Text,
                            Correo = txtCorreo.Text,
                            Telefono = int.Parse(txtTelefono.Text),
                            PaginaWeb = txtPaginaWEB.Text,
                            Direcion = txtDireccion.Text,
                            IdPais = (int)cbPais.SelectedValue,
                            IdCiudad = (int)cbCiudad.SelectedValue
                        };

                        if (svc.CrearInstitucion(institucion.Serializar()))
                        {
                            MessageBox.Show("Instituto agregado!", "Agregado");
                            this.Close();
                            listaInstituciones.Mostrarinstitutos();
                        }
                        else
                        {
                            throw new Exception("No se pudo agregar instituto");
                        }
                    }
                    catch (Exception exep)
                    {
                        MessageBox.Show(exep.Message, "Error");
                    }
                    
                }
            }else
            {
                if (MessageBox.Show("Desea Modificar esta institucion?", "Modificar Institucion", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    try
                    {
                        ServiciosWCF.Portafolio.Servicios svc = new ServiciosWCF.Portafolio.Servicios();

                        Institucion institucion = new Institucion()
                        {
                            IdInstitucion = int.Parse(lblIdInstitucion.Content.ToString()),
                            Nombres = txtNombres.Text,
                            Correo = txtCorreo.Text,
                            Telefono = int.Parse(txtTelefono.Text),
                            PaginaWeb = txtPaginaWEB.Text,
                            Direcion = txtDireccion.Text,
                            IdPais = (int)cbPais.SelectedValue,
                            IdCiudad = (int)cbCiudad.SelectedValue
                        };

                        if (svc.ActualizarInstitucion(institucion.Serializar()))
                        {
                            MessageBox.Show("Instituto actualizado!", "Actualizado");
                            this.Close();
                            listaInstituciones.Mostrarinstitutos();
                        }
                        else
                        {
                            throw new Exception("No se pudo actualizar instituto");
                        }
                    }
                    catch (Exception exep)
                    {
                        MessageBox.Show(exep.Message, "Error");
                    }
                }
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Desea eliminar esta institucion?", "Eliminar Institucion", MessageBoxButton.YesNo, MessageBoxImage.Error) == MessageBoxResult.Yes)
            {
                try
                {
                    ServiciosWCF.Portafolio.Servicios svc = new ServiciosWCF.Portafolio.Servicios();

                    Institucion institucion = new Institucion()
                    {
                        IdInstitucion = (int)lblIdInstitucion.Content
                    };

                    if (svc.EliminarInstitucion(institucion.Serializar()))
                    {
                        MessageBox.Show("Instituto eliminado!", "Eliminado");
                        this.Close();
                        listaInstituciones.Mostrarinstitutos();
                    }
                    else
                    {
                        throw new Exception("No se pudo eliminar instituto");
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
