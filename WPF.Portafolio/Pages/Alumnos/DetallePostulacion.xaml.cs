using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Negocio.Portafolio;
using Negocio.Portafolio.ViewClasses.AlumnosPostulantes;
using Negocio.Portafolio.ViewClasses.NotasProgramaAlumno;
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

namespace WPF.Portafolio.Pages.Alumnos
{
    /// <summary>
    /// Lógica de interacción para DetallePostulacion.xaml
    /// </summary>
    public partial class DetallePostulacion : MetroWindow
    {
        public DetallePostulacion(VAlumnosPostulantes alumnoPostulante)
        {
            InitializeComponent();
            CargarDatos(alumnoPostulante);
        }

        private void CargarDatos(VAlumnosPostulantes alumnoPostulante)
        {
            ServiciosWCF.Portafolio.Servicios svc = new ServiciosWCF.Portafolio.Servicios();

            VDetallePostulacion detallePostulacion = new VDetallePostulacion(svc.LeerDetallePostulacion(alumnoPostulante.Id_Alumnos, alumnoPostulante.Nombre_Programa));
            txtIdAPostulante.Text = detallePostulacion.IdPostulante.ToString();
            txtNombrePostulante.Text = detallePostulacion.NombrePostulante;
            txtCorreoPostulante.Text = detallePostulacion.CorreoPostulante;
            txtTelefonoPostulante.Text = detallePostulacion.TelefonoPostulante.ToString();
            txtMoraPostulante.Text = detallePostulacion.EstadoMoraPostulante;

            txtIdFamilia.Text = detallePostulacion.IdFamilia.ToString();
            txtNombreFamilia.Text = detallePostulacion.NombreFamilia;
            txtDireccionFamilia.Text = detallePostulacion.DireccionFamilia;
            txtCiudadFamilia.Text = detallePostulacion.CiudadFamilia;
            txtPaisFamilia.Text = detallePostulacion.PaisFamilia;
            txtCorreoFamilia.Text = detallePostulacion.CorreoFamilia;
            txtTelefonoFamilia.Text = detallePostulacion.TelefonoFamilia.ToString();

            txtIdPrograma.Text = detallePostulacion.IdPrograma.ToString();
            txtNombrePrograma.Text = detallePostulacion.NombrePrograma;
            txtCEL.Text = detallePostulacion.NombreInstitucion;
            txtCupos.Text = detallePostulacion.CuposPrograma.ToString();
            txtInicio.Text = detallePostulacion.FechaInicioPrograma.ToString();
            txtTermino.Text = detallePostulacion.FechaTerminoPrograma.ToString();
            txtTipoPrograma.Text = detallePostulacion.TipoPrograma;
            tbDescripcion.Text = detallePostulacion.DescripcionPrograma;

            lblIdIntercambio.Content = detallePostulacion.IdIntercambio;

        }
        private async void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ServiciosWCF.Portafolio.Servicios svc = new ServiciosWCF.Portafolio.Servicios();

                if (svc.ValidarMora(txtIdAPostulante.Text))
                {
                    
                    Intercambio intercambio = new Intercambio();
                    intercambio.IdIntercambio = int.Parse(lblIdIntercambio.Content.ToString());
                    intercambio = new Intercambio(svc.LeerIntercambio(intercambio.Serializar()));
                    intercambio.Estado = "Aceptada";
                    if (svc.ActualizarIntercambio(intercambio.Serializar()))
                    {
                        Programa programa = new Programa();
                        programa.IdPrograma = intercambio.IdPrograma;
                        programa = new Programa(svc.LeerPrograma(programa.Serializar()));
                        --programa.Cupos;
                        svc.ActualizarPrograma(programa.Serializar());
                        Correo mail = new Correo();
                        string _titulo = string.Format("Resultados de postulacion al programa {0}", txtNombrePrograma.Text);
                        string _descripcion = string.Format("Junto con saludar, el Centro de estudio Montreal informa que la solicitud para postular al programa {0} en {1} fue ACEPTADA, a medida que vaya acercando la fecha de comienzo del programa postuado estaremos en contacto para solicitar cierta informacion necesaria. Por el momento en caso de no tener pasaporte, te recomendamos que comiences con el trámite, ya que es necesario para el intercambio", txtNombrePrograma.Text, txtPaisFamilia.Text);
                        mail.body = mail.PopulateBody(txtNombrePostulante.Text, _titulo, _descripcion);
                        mail.SendHtmlFormattedEmail(txtCorreoPostulante.Text, _titulo, mail.body);

                        string mensaje = string.Format("Se emitió un correo al alumnos postulante");
                        await this.ShowMessageAsync("Solicitud aceptada", mensaje);

                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("El alumno se encuentra con mora","Advertencia");
                }
               
            }
            catch (Exception)
            {
                
            }
            
        }
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private async void btnRechazar_Click(object sender, RoutedEventArgs e)
        {
            ServiciosWCF.Portafolio.Servicios svc = new ServiciosWCF.Portafolio.Servicios();
            Intercambio intercambio = new Intercambio();
            intercambio.IdIntercambio = int.Parse(lblIdIntercambio.Content.ToString());
            intercambio = new Intercambio(svc.LeerIntercambio(intercambio.Serializar()));
            intercambio.Estado = "Rechazada";
            if (svc.ActualizarIntercambio(intercambio.Serializar()))
            {
                Correo mail = new Correo();
                string _titulo = string.Format("Resultados de postulacion al programa {0}", txtNombrePrograma.Text);
                string _descripcion = string.Format("Junto con saludar, el Centro de estudio Montreal informa que la solicitud para postular al programa {0} en {1} fue rechazada, lamentamos entregar esta información, de todas maneras te invitamos a volver a realizar la postulacion en el siguiente periodo donde podras tener una nueva oportinidad, para informacion puede contactarse con el que emisor de este correo", txtNombrePrograma.Text, txtPaisFamilia.Text);
                mail.body = mail.PopulateBody(txtNombrePostulante.Text, _titulo, _descripcion);
                mail.SendHtmlFormattedEmail(txtCorreoPostulante.Text, _titulo, mail.body);

                string mensaje = string.Format("Se emitió un correo al alumnos postulante");
                await this.ShowMessageAsync("Solicitud rechazada", mensaje);
                this.Close();
            }
        }

        private void btnImagenes_Click(object sender, RoutedEventArgs e)
        {
            // Debe abrir la pagina con las imagenes de la familia 
        }
    }
}
