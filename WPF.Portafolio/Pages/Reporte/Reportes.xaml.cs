using Microsoft.Reporting.WinForms;
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

namespace WPF.Portafolio.Pages.Reporte
{
    /// <summary>
    /// Lógica de interacción para Reportes.xaml
    /// </summary>
    public partial class Reportes : Page
    {
        public Reportes()
        {
            InitializeComponent();

            _reportViewer.Load += ReportViewer_Load;
        }

        private bool _isReportViewerLoaded;

        private void ReportViewer_Load(object sender, EventArgs e)
        {
            try
            {
                ReportDataSource dataSource1 = new ReportDataSource();
                ReportDataSource dataSource2 = new ReportDataSource();
                DALC.Portafolio.DScem dScem = new DALC.Portafolio.DScem();

                dScem.BeginInit();

                dataSource1.Name = "DSProgramas";
                dataSource1.Value = dScem.PROGRAMAS;
                this._reportViewer.LocalReport.DataSources.Add(dataSource1);
                //this.SizeToContent = SizeToContent.WidthAndHeight;

                dataSource2.Name = "DSEstados";
                dataSource2.Value = dScem.V_PROGRAMAS_ESTADO;
                this._reportViewer.LocalReport.DataSources.Add(dataSource2);
                this._reportViewer.LocalReport.ReportEmbeddedResource = "WPF.Portafolio.Programas.rdlc";
                this._reportViewer.LocalReport.EnableExternalImages = true;
                dScem.EndInit();

                DALC.Portafolio.DScemTableAdapters.PROGRAMASTableAdapter tableAdapter = new DALC.Portafolio.DScemTableAdapters.PROGRAMASTableAdapter();
                tableAdapter.ClearBeforeFill = true;
                tableAdapter.Fill(dScem.PROGRAMAS);

                DALC.Portafolio.DScemTableAdapters.V_PROGRAMAS_ESTADOTableAdapter postuladosTable = new DALC.Portafolio.DScemTableAdapters.V_PROGRAMAS_ESTADOTableAdapter();
                postuladosTable.ClearBeforeFill = true;
                postuladosTable.Fill(dScem.V_PROGRAMAS_ESTADO);

                _reportViewer.RefreshReport();
                _isReportViewerLoaded = true;
            }
            catch (Exception asd)
            {
                MessageBox.Show(asd.Message.ToString());
            }
        }
    }
}
