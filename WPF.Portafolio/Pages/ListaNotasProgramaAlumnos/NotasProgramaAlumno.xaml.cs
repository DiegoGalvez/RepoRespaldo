using MahApps.Metro.Controls;
using Negocio.Portafolio;
using Negocio.Portafolio.ViewClasses;
using Negocio.Portafolio.ViewEntities;
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

namespace WPF.Portafolio.Pages.ListaNotasAlumnos
{
    /// <summary>
    /// Lógica de interacción para NotasProgramaAlumno.xaml
    /// </summary>
    public partial class NotasProgramaAlumno : MetroWindow
    {
        private bool cel;
        private Programa programa;

        public NotasProgramaAlumno(int idPrograma, bool esCEL)
        {
            InitializeComponent();
            
            cel = esCEL;
            
            programa = new Programa() { IdPrograma = idPrograma };
            programa.Read();

            lblPrograma.Content = programa.NombrePrograma;

            lblIdNota.Visibility = Visibility.Hidden;
            txtNota.Visibility = Visibility.Hidden;
            btnModificar.Visibility = Visibility.Hidden;
            btnCancelar.Visibility = Visibility.Hidden;
            btnAgregar.Visibility = Visibility.Hidden;
            btnEliminar.Visibility = Visibility.Hidden;

            CargarAlumnos(idPrograma);
        }

        public NotasProgramaAlumno(int idPrograma, int idInstitucion, bool esCEL)
        {
            InitializeComponent();

            cel = esCEL;

            programa = new Programa() { IdPrograma = idPrograma };
            programa.Read();

            lblPrograma.Content = programa.NombrePrograma;

            lblIdNota.Visibility = Visibility.Hidden;
            txtNota.Visibility = Visibility.Hidden;
            btnModificar.Visibility = Visibility.Hidden;
            btnCancelar.Visibility = Visibility.Hidden;
            btnAgregar.Visibility = Visibility.Hidden;
            btnEliminar.Visibility = Visibility.Hidden;

            CargarAlumnos(idPrograma, idInstitucion);
        }

        private void CargarAlumnos(int idPrograma)
        {
            ServiciosWCF.Portafolio.Servicios svc = new ServiciosWCF.Portafolio.Servicios();

            AlumnoCollection list = new AlumnoCollection(svc.AlumnosDePrograma(idPrograma));

            dgAlumnos.ItemsSource = list;
        }
        private void CargarAlumnos(int idPrograma, int idInstitucion)
        {
            ServiciosWCF.Portafolio.Servicios svc = new ServiciosWCF.Portafolio.Servicios();

            AlumnoCollection list = new AlumnoCollection(svc.AlumnosDeProgramaPorInstitucion(idPrograma, idInstitucion));

            dgAlumnos.ItemsSource = list;
        }

        private void CargarNotas(int idALumno)
        {
            ServiciosWCF.Portafolio.Servicios svc = new ServiciosWCF.Portafolio.Servicios();

            NotaCollection notaCollection = new NotaCollection(svc.NotasPorAlumno(idALumno));
            Alumno alumno = new Alumno() { IdAlumno = idALumno };
            alumno.Read();

            lblNombreAlumno.Content = alumno.NombreCompleto();

            if (notaCollection.Count == 0)
            {
                dgNotasProgramas.Visibility = Visibility.Hidden;
                lblMensaje.Content = "No tiene notas Registradas";
                lblMensaje.Visibility = Visibility.Visible;
            }
            else
            {
                dgNotasProgramas.ItemsSource = notaCollection;
                dgNotasProgramas.Visibility = Visibility.Visible;
                lblMensaje.Visibility = Visibility.Hidden;
            }
        }

        private void dgNotasProgramas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgNotasProgramas.SelectedIndex != -1 && cel)
            {
                lblIdNota.Visibility = Visibility.Visible;
                txtNota.Visibility = Visibility.Visible;
                btnModificar.Visibility = Visibility.Visible;
                btnAgregar.Visibility = Visibility.Visible;
                btnCancelar.Visibility = Visibility.Hidden;
                btnEliminar.Visibility = Visibility.Visible;

                lblIdNota.Content = ((Nota)dgNotasProgramas.SelectedItem).IdNota;
                txtNota.Text = ((Nota)dgNotasProgramas.SelectedItem).Evaluacion.ToString();
                
                dgNotasProgramas.SelectedIndex = -1;
            }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            ServiciosWCF.Portafolio.Servicios svc = new ServiciosWCF.Portafolio.Servicios();

            if (lblIdNota.Content.ToString() != "Agregar Nota")
            {
                Nota nota = new Nota()
                {
                    IdNota = (decimal)lblIdNota.Content
                };

                nota = new Nota(svc.LeerNota(nota.Serializar()));

                nota.Evaluacion = decimal.Parse(txtNota.Text);


                svc.ActualizarNota(nota.Serializar());

                CargarNotas(((Alumno)dgAlumnos.SelectedItem).IdAlumno);
            }
            else
            {
                Nota nota = new Nota()
                {
                    Evaluacion = decimal.Parse(txtNota.Text),
                    IdPrograma = programa.IdPrograma,
                    IdAlumno = ((Alumno)dgAlumnos.SelectedItem).IdAlumno
                };

                svc.CrearNota(nota.Serializar());
                CargarNotas(((Alumno)dgAlumnos.SelectedItem).IdAlumno);
            }
        }

        private void dgAlumnos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgAlumnos.SelectedIndex != -1)
            {
                lblIdNota.Visibility = Visibility.Hidden;
                txtNota.Visibility = Visibility.Hidden;
                btnModificar.Visibility = Visibility.Hidden;
                btnAgregar.Visibility = Visibility.Hidden;
                btnEliminar.Visibility = Visibility.Hidden;

                if (cel)
                {
                    btnAgregar.Visibility = Visibility.Visible;
                }

                CargarNotas(((Alumno)dgAlumnos.SelectedItem).IdAlumno);
            }
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            lblIdNota.Content = "Agregar Nota";
            txtNota.Text = "";

            lblIdNota.Visibility = Visibility.Visible;
            txtNota.Visibility = Visibility.Visible;
            btnCancelar.Visibility = Visibility.Visible;
            btnAgregar.Visibility = Visibility.Hidden;
            btnModificar.Visibility = Visibility.Visible;
            btnEliminar.Visibility = Visibility.Hidden;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            lblIdNota.Visibility = Visibility.Hidden;
            lblIdNota.Content = "";
            txtNota.Visibility = Visibility.Hidden;
            txtNota.Text = "";
            btnModificar.Visibility = Visibility.Hidden;
            btnAgregar.Visibility = Visibility.Visible;
            btnCancelar.Visibility = Visibility.Hidden;
            btnEliminar.Visibility = Visibility.Hidden;
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Desea borrar esta nota?", "Eliminar Nota", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {

                try
                {
                    ServiciosWCF.Portafolio.Servicios svc = new ServiciosWCF.Portafolio.Servicios();

                    Nota nota = new Nota()
                    {
                        IdNota = (decimal)lblIdNota.Content
                    };
                    
                    if (svc.EliminarNota(nota.Serializar()))
                    {
                        MessageBox.Show("Nota eliminada!", "Borrado");

                        lblIdNota.Visibility = Visibility.Hidden;
                        lblIdNota.Content = "";
                        txtNota.Visibility = Visibility.Hidden;
                        txtNota.Text = "";
                        btnModificar.Visibility = Visibility.Hidden;
                        btnAgregar.Visibility = Visibility.Visible;
                        btnCancelar.Visibility = Visibility.Hidden;
                        btnEliminar.Visibility = Visibility.Hidden;

                        CargarNotas(((Alumno)dgAlumnos.SelectedItem).IdAlumno);
                    }
                    else
                    {
                        throw new Exception("No se pudo eliminar nota");
                    }
                }
                catch (Exception exep)
                {
                    MessageBox.Show(exep.Message, "Error");
                }

            }
        }

        private void dgNotasProgramas_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            if (this.dgNotasProgramas.Columns != null)
            {
                this.dgNotasProgramas.Columns[1].Visibility = Visibility.Collapsed;
                this.dgNotasProgramas.Columns[2].Visibility = Visibility.Collapsed;
                this.dgNotasProgramas.Columns[3].Visibility = Visibility.Collapsed;
            }
        }
    }
}
