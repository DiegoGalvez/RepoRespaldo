using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocio.Portafolio;

namespace WPF.Portafolio.Pages.Programas
{
    public partial class AgregarActividad : Form
    {
        public AgregarActividad()
        {
            InitializeComponent();
        }



        private void Cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void Aceptar_Click(object sender, EventArgs e)
        {
            Actividad act = new Actividad();

            act.NombreActividad = txtActividad.Text;
            act.Descripcion = txtDescripcion.Text;


        }
    }
}
