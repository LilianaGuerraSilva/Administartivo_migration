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
using LibGalac.Aos.Catching;

namespace Galac.Adm.Rpt.GestionProduccion {

    public class clsListaDeMaterialesDeSalida : LibRptBaseMfc {
        #region Propiedades

        protected DataTable DataListaSalida { get; set; }
        protected DataTable DataListaInsumos { get; set; }    
        private string CodigoListaAProducir { get; set; }       
        private eCantidadAImprimir CantidadAImprimir { get; set; }       
        private decimal CantidadAProducir { get; set; }              
        private decimal TasaDeCambio { get; set; }     
        private string MonedaDelInforme { get; set; }
        private string[] ListaMonedas { get; set; }
        #endregion //Propiedades
        #region Constructores
        public clsListaDeMaterialesDeSalida(ePrintingDevice initPrintingDevice, eExportFileFormat initExportFileFormat, LibXmlMemInfo initAppMemInfo, LibXmlMFC initMfc, eCantidadAImprimir initCantidadAImprimir, decimal initCantidadAProducir, string initCodigoListaAProducir, decimal initTasaDeCambio, string initMonedaDelInforme, string[] initListaMonedas)
            : base(initPrintingDevice, initExportFileFormat, initAppMemInfo, initMfc) {
            CodigoListaAProducir = initCodigoListaAProducir;
            CantidadAImprimir = initCantidadAImprimir;
            CantidadAProducir = initCantidadAProducir;
            TasaDeCambio = initTasaDeCambio;
            MonedaDelInforme = initMonedaDelInforme;
            ListaMonedas = initListaMonedas;
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
            if(UseExternalRpx) {
                vTitulo += " - desde RPX externo.";
            }
            int vPos = LibString.IndexOf(MonedaDelInforme, " ") > 0 ? LibString.IndexOf(MonedaDelInforme, "expresado") - 1 : LibString.Len(MonedaDelInforme);
            string vNombreMoneda = LibString.SubString(MonedaDelInforme, 0, vPos);
            Dictionary<string, string> vParams = new Dictionary<string, string>();
            vParams.Add("NombreCompania", AppMemoryInfo.GlobalValuesGetString("Compania", "Nombre"));
            vParams.Add("TituloInforme", vTitulo);
            vParams.Add("NombreMoneda", vNombreMoneda);
            return vParams;
        }

        public override void RunReport() {
            if (WorkerCancellPending()) {
                return;
            }
            WorkerReportProgress(30, "Obteniendo datos...");
            int vConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");            
            IListaDeMaterialesInformes vRpt = new Galac.Adm.Brl.GestionProduccion.Reportes.clsListaDeMaterialesRpt() as IListaDeMaterialesInformes;
            DataListaSalida = vRpt.BuildListaDeMaterialesSalida(vConsecutivoCompania, CodigoListaAProducir, CantidadAImprimir, CantidadAProducir, MonedaDelInforme, TasaDeCambio, ListaMonedas);
            DataListaInsumos = vRpt.BuildListaDeMaterialesInsumos(vConsecutivoCompania, CodigoListaAProducir, CantidadAImprimir, CantidadAProducir, MonedaDelInforme, TasaDeCambio, ListaMonedas);
        }

        public override void SendReportToDevice() {
            WorkerReportProgress(90, "Configurando Informe...");
            Dictionary<string, string> vParams = GetConfigReportParameters();
            dsrListaDeMaterialesDeSalida vRpt = new dsrListaDeMaterialesDeSalida();
            if(DataListaSalida.Rows.Count > 0) {
                if(vRpt.ConfigReport(DataListaSalida, DataListaInsumos, vParams)) {
                    LibReport.SendReportToDevice(vRpt, 1, PrintingDevice, clsListaDeMaterialesDeSalida.ReportName, true, ExportFileFormat, "", false);
                }
                WorkerReportProgress(100, "Finalizando...");
            } else {
                throw new GalacException("No existen registros para mostrar", eExceptionManagementType.Alert);
            }
        }
        #endregion //Metodos Generados


    } //End of class clsListaDeMaterialesDeInventarioAProducir

} //End of namespace Galac.Adm.Rpt.GestionProduccion

