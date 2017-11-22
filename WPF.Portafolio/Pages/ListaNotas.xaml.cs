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

namespace WPF.Portafolio.Pages
{
    /// <summary>
    /// Lógica de interacción para ListaNotas.xaml
    /// </summary>
    public partial class ListaNotas : Page
    {
        public ListaNotas()
        {
            InitializeComponent();
        }
        

        private void button_Click(object sender, RoutedEventArgs e)
        {
            ServiciosWCF.Portafolio.Servicios svc = new ServiciosWCF.Portafolio.Servicios();
            Nota nota = new Nota();
            nota.IdAlumno = int.Parse(txtBuscar.Text);
            NotaCollection list = new NotaCollection(svc.NotasAlumno(nota.Serializar()));
            dgListaNotas.ItemsSource = list;
            
        }
    }
}
