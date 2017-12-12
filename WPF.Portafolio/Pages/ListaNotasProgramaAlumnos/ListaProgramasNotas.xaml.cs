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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Negocio.Portafolio;
using ServiciosWCF.Portafolio;
using WPF.Portafolio.Pages.ListaNotasAlumnos;
using Negocio.Portafolio.ViewClasses;
using System.Collections;

namespace WPF.Portafolio.Pages
{
    /// <summary>
    /// Lógica de interacción para ListaProgramasNotas.xaml
    /// </summary>
    public partial class ListaProgramasNotas : Page
    {
        private Boolean esCEL = false;
        private EncargadoCel eCel = null;

        public ListaProgramasNotas()
        {
            InitializeComponent();

            if(MainMenu.UsuarioActual.Rol != "EncargadoCEL")
            {
                esCEL = false;
                MostrarProgramas();
            }
            else
            {
                EncargadoCel encargadoCel = new EncargadoCel();
                encargadoCel.IdEncargadoCel = MainMenu.UsuarioActual.IdEncargadoCel.GetValueOrDefault();
                encargadoCel.Read();

                esCEL = true;
                eCel = encargadoCel;

                MostrarProgramasPorInstitucion(encargadoCel.IdInstitucion);
            }
            
            txtBuscarNombre.Focus();
        }

        private void MostrarProgramasPorInstitucion(int idInstitucion)
        {
            ServiciosWCF.Portafolio.Servicios svc = new ServiciosWCF.Portafolio.Servicios();
            
            ProgramaCollection list = new ProgramaCollection(svc.ProgramasPublicadosPorInstitucion(idInstitucion));

            dgProgramasNotas.ItemsSource = GenerarVistaProgramas(list);
        }

        private void MostrarProgramas()
        {
            ServiciosWCF.Portafolio.Servicios svc = new ServiciosWCF.Portafolio.Servicios();

            ProgramaCollection list = new ProgramaCollection(svc.ProgramasPublicados());
            
            dgProgramasNotas.ItemsSource = GenerarVistaProgramas(list);
        }

        private List<VPrograma> GenerarVistaProgramas(ProgramaCollection list)
        {
            List<VPrograma> programas = new List<VPrograma>();

            foreach (var item in list)
            {
                VPrograma programa = new VPrograma(item);

                programas.Add(programa);
            }

            return programas;
        }

        private void dgProgramasNotas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgProgramasNotas.SelectedIndex != -1)
            {
                if (!esCEL)
                {
                    NotasProgramaAlumno notasAlumno = new NotasProgramaAlumno(((VPrograma)dgProgramasNotas.SelectedItem).IdPrograma, esCEL);
                    dgProgramasNotas.SelectedIndex = -1;
                    notasAlumno.ShowDialog();
                }
                else
                {
                    NotasProgramaAlumno notasAlumno = new NotasProgramaAlumno(((VPrograma)dgProgramasNotas.SelectedItem).IdPrograma, eCel.IdInstitucion, esCEL);
                    dgProgramasNotas.SelectedIndex = -1;
                    notasAlumno.ShowDialog();
                }
                
            }
        }
        
        private void txtBuscarNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            ServiciosWCF.Portafolio.Servicios svc = new ServiciosWCF.Portafolio.Servicios();

            if (!esCEL)
            {
                ProgramaCollection list = new ProgramaCollection(svc.BuscarProgramasPublicadosPorNombre(txtBuscarNombre.Text));

                dgProgramasNotas.ItemsSource = list;
            }
            else
            {
                ProgramaCollection list = new ProgramaCollection(svc.BuscarProgramasPublicadosPorInstitucionYNombre(eCel.IdInstitucion, txtBuscarNombre.Text));

                dgProgramasNotas.ItemsSource = list;
            }
            
            
        }
    }
}
