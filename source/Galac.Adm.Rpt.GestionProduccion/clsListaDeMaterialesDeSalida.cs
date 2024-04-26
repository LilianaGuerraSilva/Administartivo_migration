using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.ARRpt.Reports;
using System.Data;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.ARRpt;
using Galac.Adm.Ccl.GestionProduccion;
using Galac.Saw.Lib;

namespace Galac.Adm.Rpt.GestionProduccion {

    public class clsListaDeMaterialesDeSalida : LibRptBaseMfc {
        #region Propiedades

        protected DataTable Data { get; set; }
        private int ConsecutivoCompania { get; set; }       
        private string CodigoListaAProducir { get; set; }       
        private eCantidadAImprimir CantidadAImprimir { get; set; }       
        private decimal CantidadAProducir { get; set; }       
        private string MonedaDelInformeMM { get; set; }       
        private eTasaDeCambioParaImpresion TipoTasaDeCambio { get; set; }     
        private string Moneda { get; set; }
        #endregion //Propiedades
        #region Constructores
        public clsListaDeMaterialesDeSalida(ePrintingDevice initPrintingDevice, eExportFileFormat initExportFileFormat, LibXmlMemInfo initAppMemInfo, LibXmlMFC initMfc, eCantidadAImprimir initCantidadAImprimir, decimal initCantidadAProducir, string initCodigoListaAProducir, string initMonedaDelInformeMM, eTasaDeCambioParaImpresion initTipoTasaDeCambio, string initMoneda)
            : base(initPrintingDevice, initExportFileFormat, initAppMemInfo, initMfc) {
            ConsecutivoCompania = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Compania", "ConsecutivoCompania");
            CodigoListaAProducir = initCodigoListaAProducir;
            CantidadAImprimir = initCantidadAImprimir;
            CantidadAProducir = initCantidadAProducir;
            MonedaDelInformeMM = initMonedaDelInformeMM;
            TipoTasaDeCambio = initTipoTasaDeCambio;
            Moneda = initMoneda;            
        }
        #endregion //Constructores
        #region Metodos Generados

        public static string ReportName {
            get {
                return new dsrListaDeMaterialesDeSalida().ReportTitle();
            }
        }

        public override Dictionary<string, string> GetConfigReportParameters() {
            string vTitulo = clsListaDeMaterialesDeSalida.ReportName;
            if (UseExternalRpx) {
                vTitulo += " - desde RPX externo.";
            }
            Dictionary<string, string> vParams = new Dictionary<string, string>();
            vParams.Add("NombreCompania", AppMemoryInfo.GlobalValuesGetString("Compania", "Nombre"));
            vParams.Add("TituloInforme", vTitulo);            
            return vParams;
        }

        public override void RunReport() {
            if (WorkerCancellPending()) {
                return;
            }
            WorkerReportProgress(30, "Obteniendo datos...");
            IListaDeMaterialesInformes vRpt = new Galac.Adm.Brl.GestionProduccion.Reportes.clsListaDeMaterialesRpt() as IListaDeMaterialesInformes;
            string vCodigoMoneda = LibString.Trim(LibString.Mid(Moneda, 1, LibString.InStr(Moneda, ")") - 1));
            vCodigoMoneda = LibString.IsNullOrEmpty(vCodigoMoneda, true) ? LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoMonedaExtranjera") : vCodigoMoneda;
            string vNombreMoneda = LibString.Trim(LibString.Mid(Moneda, 1 + LibString.InStr(Moneda, ")")));
            Data = vRpt.BuildListaDeMateriales(ConsecutivoCompania, CodigoListaAProducir, CantidadAImprimir, CantidadAProducir, MonedaDelInformeMM, TipoTasaDeCambio, vNombreMoneda, vCodigoMoneda);
        }

        public override void SendReportToDevice() {
            WorkerReportProgress(90, "Configurando Informe...");
            Dictionary<string, string> vParams = GetConfigReportParameters();
            dsrListaDeMaterialesDeSalida vRpt = new dsrListaDeMaterialesDeSalida();
            if (vRpt.ConfigReport(Data, vParams)) {
                LibReport.SendReportToDevice(vRpt, 1, PrintingDevice, clsListaDeMaterialesDeSalida.ReportName, true, ExportFileFormat, "", false);
            }
            WorkerReportProgress(100, "Finalizando...");
        }
        #endregion //Metodos Generados


    } //End of class clsListaDeMaterialesDeInventarioAProducir

} //End of namespace Galac.Adm.Rpt.GestionProduccion

