using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.ARRpt.Reports;
using System.Data;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.ARRpt;
using Galac.Adm.Ccl.Venta;
using LibGalac.Aos.Catching;

namespace Galac.Adm.Rpt.Venta {

    public class clsCuadreCajaConDetalleFormaPagoResumido: LibRptBaseMfc {
        #region Propiedades

        protected DataTable Data { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public Saw.Lib.eMonedaParaImpresion MonedaDeReporte { get; set; }
        public Saw.Lib.eTipoDeInforme TipoDeInforme { get; set; }
        public bool TotalesTipoPago { get; set; }
        #endregion //Propiedades
        #region Constructores
        public clsCuadreCajaConDetalleFormaPagoResumido(ePrintingDevice initPrintingDevice, eExportFileFormat initExportFileFormat, LibXmlMemInfo initAppMemInfo, LibXmlMFC initMfc)
            : base(initPrintingDevice, initExportFileFormat, initAppMemInfo, initMfc) {

        }
        #endregion //Constructores
        #region Metodos Generados

        public static string ReportName {
            get { return new dsrCuadreCajaConDetalleFormaPagoResumido().ReportTitle(); }
        }

        public override Dictionary<string, string> GetConfigReportParameters() {
            string vTitulo = clsCuadreCajaConDetalleFormaPagoResumido.ReportName;
            if (UseExternalRpx) {
                vTitulo += " - desde RPX externo.";
            }
            Dictionary<string, string> vParams = new Dictionary<string, string>();
            vParams.Add("NombreCompania", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "Nombre"));
            vParams.Add("RifCompania", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "NumeroDeRif"));
            vParams.Add("TituloInforme", vTitulo);
            vParams.Add("FechaInicialYFinal", string.Format("desde {0} hasta {1}", LibConvert.ToStr(FechaInicial, "dd/MM/yyyy"), LibConvert.ToStr(FechaFinal, "dd/MM/yyyy")));
            vParams.Add("EnMonedaOriginal", LibConvert.BoolToSN(LibEnum.Equals(MonedaDeReporte, Saw.Lib.eMonedaParaImpresion.EnMonedaOriginal)));
            vParams.Add("TotalesTipoPago", LibConvert.BoolToSN(TotalesTipoPago));
            return vParams;
        }

        public override void RunReport() {
            if (WorkerCancellPending()) {
                return;
            }
            WorkerReportProgress(30, "Obteniendo datos...");
            ICajaInformes vRpt = new Galac.Adm.Brl.Venta.Reportes.clsCajaRpt() as ICajaInformes;
            Data = vRpt.BuildCuadreCajaConDetalleFormaPago(LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"), FechaInicial, FechaFinal, MonedaDeReporte, TipoDeInforme, TotalesTipoPago);
        }

        public override void SendReportToDevice() {
            WorkerReportProgress(90, "Configurando Informe...");
            Dictionary<string, string> vParams = GetConfigReportParameters();
            dsrCuadreCajaConDetalleFormaPagoResumido vRpt = new dsrCuadreCajaConDetalleFormaPagoResumido();
            if (Data.Rows.Count < 1) {
                throw new GalacException("No existen datos para mostrar", eExceptionManagementType.Alert);
            }
            if (vRpt.ConfigReport(Data, vParams)) {
                LibReport.SendReportToDevice(vRpt, 1, PrintingDevice, clsCuadreCajaConDetalleFormaPagoResumido.ReportName, true, ExportFileFormat, "", false);
            }
            WorkerReportProgress(100, "Finalizando...");
        }
        #endregion //Metodos Generados


    } //End of class clsCuadreCajaConDetalleFormaPago

} //End of namespace Galac.Dbo.Rp