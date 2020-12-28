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

    public class clsCuadreCajaPorUsuario: LibRptBaseMfc {
        #region Propiedades

        protected DataTable Data { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public Saw.Lib.eTipoDeInforme TipoDeInforme { get; set; }
        public Saw.Lib.eMonedaParaImpresion MonedaDeReporte { get; set; }
        public Saw.Lib.eCantidadAImprimir CantidadOperadorDeReporte { get; set; }
        public string NombreDelOperador { get; set; }
        public string CodigoMonedaExtrajera { get; set; }
        public string SimboloMonedaExtranjera { get; set; }
        #endregion //Propiedades
        #region Constructores
        public clsCuadreCajaPorUsuario(ePrintingDevice initPrintingDevice, eExportFileFormat initExportFileFormat, LibXmlMemInfo initAppMemInfo, LibXmlMFC initMfc, DateTime valFechaInicial, DateTime valFechaFinal, Saw.Lib.eTipoDeInforme valTipoDeInforme, Saw.Lib.eMonedaParaImpresion valMonedaDeReporte, Saw.Lib.eCantidadAImprimir valCantidadAImprimir, string valNombreDelOperador, string valCodigoMonedaExtranjera,string valSimboloMonedaExtranjera)
            : base(initPrintingDevice, initExportFileFormat, initAppMemInfo, initMfc) {
            FechaInicial = valFechaInicial;
            FechaFinal = valFechaFinal;
            TipoDeInforme = valTipoDeInforme;
            MonedaDeReporte = valMonedaDeReporte;
            CantidadOperadorDeReporte = valCantidadAImprimir;
            NombreDelOperador = valNombreDelOperador;
            CodigoMonedaExtrajera = valCodigoMonedaExtranjera;
            SimboloMonedaExtranjera = valSimboloMonedaExtranjera;
        }
        #endregion //Constructores
        #region Metodos Generados

        public static string ReportName {
            get { return new dsrCuadreCajaPorUsuario().ReportTitle(); }
        }

        public override Dictionary<string, string> GetConfigReportParameters() {
            string vTitulo = clsCuadreCajaPorUsuario.ReportName;
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
            Data = vRpt.BuildCuadreCajaPorUsuario(LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"), FechaInicial, FechaFinal, TipoDeInforme, MonedaDeReporte, CantidadOperadorDeReporte, NombreDelOperador);
        }

        public override void SendReportToDevice() {
            WorkerReportProgress(90, "Configurando Informe...");
            Dictionary<string, string> vParams = GetConfigReportParameters();
            dsrCuadreCajaPorUsuario vRpt = new dsrCuadreCajaPorUsuario();
            vRpt.CodigoMonedaExtranjera = CodigoMonedaExtrajera;
            vRpt.SimboloMonedaExtranjera = SimboloMonedaExtranjera;
            if (Data.Rows.Count < 1) {
                throw new GalacException("No existen datos para mostrar", eExceptionManagementType.Alert);
            }
            if (vRpt.ConfigReport(Data, vParams)) {
                LibReport.SendReportToDevice(vRpt, 1, PrintingDevice, clsCuadreCajaPorUsuario.ReportName, true, ExportFileFormat, "", false);
            }
            WorkerReportProgress(100, "Finalizando...");
        }
        #endregion //Metodos Generados


    } //End of class clsCuadreCajaPorUsuario

} //End of namespace Galac.Dbo.Rpt.Venta

