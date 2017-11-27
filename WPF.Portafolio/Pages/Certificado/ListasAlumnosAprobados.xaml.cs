using Negocio.Portafolio;
using Negocio.Portafolio.ViewClasses.ProgramasFinalizados;
using Spire.Pdf;
using Spire.Pdf.HtmlConverter;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF.Portafolio.Pages.Certificado
{
    /// <summary>
    /// Lógica de interacción para ListasAlumnosAprobados.xaml
    /// </summary>
    public partial class ListasAlumnosAprobados : Page
    {
        public static VProgramasFinalizados programaActual;
        public ListasAlumnosAprobados(VProgramasFinalizados programa)
        {
            InitializeComponent();
            programaActual = programa;
            lblNombrePrograma.Content = programaActual.NombrePrograma;
            cargardgAlumnos();
        }

        private void cargardgAlumnos()
        {
            AlumnoCollection listaAlumnos = new AlumnoCollection().AlumnosProgramaFinalizado(programaActual.IdPrograma);
            
            dgAlumnos.ItemsSource = listaAlumnos;
        }

        private void descargar_Click(object sender, RoutedEventArgs e)
        {
            
            Alumno alu = (Alumno)dgAlumnos.SelectedItem;
            // Falta agregar el codigo que ejecuta la creacion del certificado y darle parametros
            //MessageBox.Show(string.Format("{0}{1}{2}",alu.IdAlumno.ToString(), alu.Nombre, alu.Correo));

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "PDF file |*.pdf";
            sfd.ValidateNames = true;
            sfd.FileName = string.Format("Certificado {0}", alu.NombreCompleto());//NombreCompleto);

            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                PdfDocument doc = new PdfDocument();
                try
                {
                    string cuerpo = File.ReadAllText(@"..\\..\\..\\Negocio.Portafolio\\Certificado.html");

                    string htmlstring = string.Format(cuerpo, alu.NombreCompleto(), programaActual.NombrePrograma, programaActual.Pais, programaActual.FechaTermino.ToString("Y", CultureInfo.CreateSpecificCulture("es-ES")));
                    string outputFile = string.Format("Certificado {0}", alu.NombreCompleto());

                    PdfHtmlLayoutFormat htmlLayoutFormat = new PdfHtmlLayoutFormat();
                    htmlLayoutFormat.IsWaiting = false;
                    PdfPageSettings setting = new PdfPageSettings();
                    setting.Orientation = PdfPageOrientation.Landscape;

                    doc.LoadFromHTML(htmlstring, false, setting, htmlLayoutFormat);

                    doc.SaveToFile(sfd.FileName);
                    System.Diagnostics.Process.Start(sfd.FileName);

                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message, "Mensaje", MessageBoxButton.OK);
                    //await this.ShowMessageAsync(ex.Message, "Mensaje", MessageBoxButton.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    doc.Close();
                }
            }
        }
        
    }
}
