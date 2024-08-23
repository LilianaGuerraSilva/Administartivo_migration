using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.ARRpt.Reports;
using System.Data;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.ARRpt;
using Galac.Saw.Ccl.Inventario;
using Galac.Saw.Brl.Inventario.Reportes;

namespace Galac.Saw.Rpt.Inventario {
    public class clsNotaDeEntradaSalidaDeInventario: LibRptBaseMfc {
        #region Propiedades
        protected DataTable Data { get; set; }
        public string NumeroDocumento { get; set; }
        #endregion //Propiedades
        #region Constructores
        public clsNotaDeEntradaSalidaDeInventario(ePrintingDevice initPrintingDevice, eExportFileFormat initExportFileFormat, LibXmlMemInfo initAppMemInfo, LibXmlMFC initMfc)
            : base(initPrintingDevice, initExportFileFormat, initAppMemInfo, initMfc) {
        }
        #endregion //Constructores
        #region Metodos Generados
        public static string ReportName {
            get { return new dsrNotaDeEntradaSalidaDeInventario().ReportTitle(); }
        }

        public override Dictionary<string, string> GetConfigReportParameters() {
            string vTitulo = clsNotaDeEntradaSalidaDeInventario.ReportName;
            if (UseExternalRpx) {
                vTitulo += " - desde RPX externo.";
            }
            Dictionary<string, string> vParams = new Dictionary<string, string>();
            vParams.Add("NombreCompania", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "Nombre"));
            vParams.Add("TituloInforme", vTitulo);
            return vParams;
        }

        public override void RunReport() {
            if (WorkerCancellPending()) {
                return;
            }
            WorkerReportProgress(30, "Obteniendo datos...");
            INotaDeEntradaSalidaInformes vRpt = new clsNotaDeEntradaSalidaRpt() as INotaDeEntradaSalidaInformes;
            int vConsecutivoCompania = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Compania", "ConsecutivoCompania");
            Data = vRpt.BuildNotaDeEntradaSalidaDeInventario(vConsecutivoCompania, NumeroDocumento);
        }

        public override void SendReportToDevice() {
            WorkerReportProgress(90, "Configurando Informe...");            
            dsrNotaDeEntradaSalidaDeInventario vRpt = new dsrNotaDeEntradaSalidaDeInventario();
            if (vRpt.ConfigReport(Data)) {
                LibReport.SendReportToDevice(vRpt, 1, ePrintingDevice.Screen, clsNotaDeEntradaSalidaDeInventario.ReportName, true, ExportFileFormat, "", false);
            }
            WorkerReportProgress(100, "Finalizando...");
        }
        #endregion //Metodos Generados


    } //End of class clsNotaDeEntradaSalidaDeInventario

} //End of namespace Galac.Dbo.Rpt.Inventario

