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

    public class clsCuadreCajaPorTipoCobroYUsuario: LibRptBaseMfc {
        #region Propiedades

        protected DataTable Data { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public eCantidadAImprimir CantidadOperadorDeReporte { get; set; }
        public string NombreDelOperador { get; set; }
        public Saw.Lib.eMonedaParaImpresion MonedaDeReporte { get; set; }
        #endregion //Propiedades
        #region Constructores
        public clsCuadreCajaPorTipoCobroYUsuario(ePrintingDevice initPrintingDevice, eExportFileFormat initExportFileFormat, LibXmlMemInfo initAppMemInfo, LibXmlMFC initMfc)
            : base(initPrintingDevice, initExportFileFormat, initAppMemInfo, initMfc) {

        }
        #endregion //Constructores
        #region Metodos Generados

        public static string ReportName {
            get { return new dsrCuadreCajaPorTipoCobroYUsuario().ReportTitle(); }
        }

        public override Dictionary<string, string> GetConfigReportParameters() {
            string vTitulo = clsCuadreCajaPorTipoCobroYUsuario.ReportName;
            if (UseExternalRpx) {
                vTitulo += " - desde RPX externo.";
            }
            Dictionary<string, string> vParams = new Dictionary<string, string>();
            vParams.Add("NombreCompania", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "Nombre"));
            vParams.Add("RifCompania", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "NumeroDeRif"));
            vParams.Add("TituloInforme", vTitulo);
            vParams.Add("FechaInicialYFinal", string.Format("desde {0} hasta {1}", LibConvert.ToStr(FechaInicial, "dd/MM/yyyy"), LibConvert.ToStr(FechaFinal, "dd/MM/yyyy")));
            vParams.Add("EnMonedaOriginal", LibConvert.BoolToSN(LibEnum.Equals(MonedaDeReporte, Saw.Lib.eMonedaParaImpresion.EnMonedaOriginal)));
            return vParams;
        }

        public override void RunReport() {
            if (WorkerCancellPending()) {
                return;
            }
            WorkerReportProgress(30, "Obteniendo datos...");
            ICajaInformes vRpt = new Galac.Adm.Brl.Venta.Reportes.clsCajaRpt() as ICajaInformes;
            Data = vRpt.BuildCuadreCajaPorTipoCobroYUsuario(LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"), FechaInicial, FechaFinal, CantidadOperadorDeReporte, NombreDelOperador, MonedaDeReporte);
        }

        public override void SendReportToDevice() {
            WorkerReportProgress(90, "Configurando Informe...");
            Dictionary<string, string> vParams = GetConfigReportParameters();
            dsrCuadreCajaPorTipoCobroYUsuario vRpt = new dsrCuadreCajaPorTipoCobroYUsuario();
            if (Data.Rows.Count < 1) {
                throw new GalacException("No existen datos para mostrar", eExceptionManagementType.Alert);
            }
            if (vRpt.ConfigReport(Data, vParams)) {
                LibReport.SendReportToDevice(vRpt, 1, PrintingDevice, clsCuadreCajaPorTipoCobroYUsuario.ReportName, true, ExportFileFormat, "", false);
            }
            WorkerReportProgress(100, "Finalizando...");
        }
        #endregion //Metodos Generados


    } //End of class clsCuadreCajaPorTipoCobroYUsuario

} //End of namespace Galac.Adm.Rpt.Venta

