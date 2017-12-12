using MahApps.Metro.Controls;
using Negocio.Portafolio;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Forms;

namespace WPF.Portafolio.Pages.Familias
{
    /// <summary>
    /// Lógica de interacción para DetallePostulacionFamilia.xaml
    /// </summary>
    public partial class DetallePostulacionFamilia : MetroWindow
    {
        public DetallePostulacionFamilia(FamiliaAnfitriona familia)
        {
            InitializeComponent();

            CargaDatos(familia);
        }

        private void CargaDatos(FamiliaAnfitriona familia)
        {
            lblNombre.Content = string.Format("{0} {1} {2}", familia.Nombres, familia.ApePaterno, familia.ApeMaterno);

            txtIdentificacion.Text = familia.Identificador;
            txtTelefono.Text = familia.Telefono.ToString();
            txtCorreo.Text = familia.Correo;
            txtDireccion.Text = familia.Direccion;
            txtCiudad.Text = familia.IdCiudad.ToString();
            txtPais.Text = familia.IdPais.ToString();

            descImgs(familia.Identificador);
        }

        private void descImgs(string identifiacionFamilia)
        {
            string directorio = string.Format("ftp://190.46.53.32/Familias/{0}", identifiacionFamilia);
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(directorio);
            request.Method = WebRequestMethods.Ftp.ListDirectory;

            request.Credentials = new NetworkCredential("cem", "nick6831");

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);

            List<string> paths = new List<string>();
            string line = reader.ReadLine();

            while (!string.IsNullOrEmpty(line))
            {
                paths.Add(line);
                line = reader.ReadLine();
            }

            using (WebClient ftpClient = new WebClient())
            {
                ftpClient.Credentials = new System.Net.NetworkCredential("cem", "nick6831");

                for (int i = 0; i <= paths.Count - 1; i++)
                {
                    if (paths[i].Contains(".jpg") || paths[i].Contains(".png"))
                    {
                        string result = Path.GetTempPath();
                        string path = "ftp://190.46.53.32/Familias/" + paths[i].ToString();

                        string[] DirNombre = paths[i].Split('/');
                        string dir = DirNombre[0];
                        string nombreFile = DirNombre[1];
                        string trnsfrpth = result + nombreFile;

                        ftpClient.DownloadFile(path, trnsfrpth);
                        images.Items.Add(trnsfrpth);

                    }
                }
            }

            reader.Close();
            response.Close();
        }

    }
}
