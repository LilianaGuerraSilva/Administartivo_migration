using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.ARRpt.Reports;
using System.Data;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.ARRpt;
using Galac.Adm.Brl.Venta;
using Galac.Adm.Ccl.Venta;
using LibGalac.Aos.Catching;

namespace Galac.Adm.Rpt.Venta {

    public class clsCuadreCajaCobroMultimonedaDetallado: LibRptBaseMfc {
        #region Propiedades

        protected DataTable Data { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public Saw.Lib.eCantidadAImprimir CantidadOperadorDeReporte { get; set; }
        public string NombreDelOperador { get; set; }
        public Saw.Lib.eMonedaParaImpresion MonedaDeReporte { get; set; }
        public bool TotalesTipoCobro { get; set; }
        public string CodigoMonedaExtranjera { get; set; }
        public string SimboloMonedaExtranjera { get; set; }

        #region Codigo Ejemplo
        /* Codigo de Ejemplo

        public DateTime FechaDesde { get; set; }

        public DateTime FechaHasta { get; set; }
        */
        #endregion //Codigo Ejemplo
        #endregion //Propiedades
        #region Constructores
        public clsCuadreCajaCobroMultimonedaDetallado(ePrintingDevice initPrintingDevice, eExportFileFormat initExportFileFormat, LibXmlMemInfo initAppMemInfo, LibXmlMFC initMfc, DateTime valFechaInicial, DateTime valFechaFinal, Saw.Lib.eCantidadAImprimir valCantidadOperadorDeReporte, string valNombreDelOperador, Saw.Lib.eMonedaParaImpresion valMonedaDeReporte, bool valTotalesTipoCobro, string valCodigoMonedaExtranjera, string valSimboloMonedaExtranjera)
            : base(initPrintingDevice, initExportFileFormat, initAppMemInfo, initMfc) {
            FechaInicial = valFechaInicial;
            FechaFinal = valFechaFinal;
            CantidadOperadorDeReporte = valCantidadOperadorDeReporte;
            NombreDelOperador = valNombreDelOperador;
            MonedaDeReporte = valMonedaDeReporte;
            TotalesTipoCobro = valTotalesTipoCobro;
            CodigoMonedaExtranjera = valCodigoMonedaExtranjera;
            SimboloMonedaExtranjera = valSimboloMonedaExtranjera;
        }
        #endregion //Constructores
        #region Metodos Generados

        public static string ReportName {
            get { return new dsrCuadreCajaCobroMultimonedaDetallado().ReportTitle(); }
        }

        public override Dictionary<string, string> GetConfigReportParameters() {
            string vTitulo = clsCuadreCajaCobroMultimonedaDetallado.ReportName;
            if (UseExternalRpx) {
                vTitulo += " - desde RPX externo.";
            }
            Dictionary<string, string> vParams = new Dictionary<string, string>();
            vParams.Add("NombreCompania", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "Nombre"));
            vParams.Add("RifCompania",LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "NumeroDeRif"));
            vParams.Add("TituloInforme", vTitulo);
            vParams.Add("FechaInicialYFinal", string.Format("desde {0} hasta {1}", LibConvert.ToStr(FechaInicial, "dd/MM/yyyy"), LibConvert.ToStr(FechaFinal, "dd/MM/yyyy")));
            vParams.Add("TotalesTipoDeCobro", LibConvert.BoolToSN(TotalesTipoCobro));
            vParams.Add("EnMonedaOriginal", LibConvert.BoolToSN(LibEnum.Equals(MonedaDeReporte,Saw.Lib.eMonedaParaImpresion.EnMonedaOriginal)));
            return vParams;
        }

        public override void RunReport() {
            if (WorkerCancellPending()) {
                return;
            }
            WorkerReportProgress(30, "Obteniendo datos...");
            ICajaInformes vRpt = new Galac.Adm.Brl.Venta.Reportes.clsCajaRpt() as ICajaInformes;
            Data = vRpt.BuildCuadreCajaCobroMultimonedaDetallado(LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"), FechaInicial, FechaFinal, CantidadOperadorDeReporte, NombreDelOperador, MonedaDeReporte, TotalesTipoCobro);
        }

        public override void SendReportToDevice() {
            WorkerReportProgress(90, "Configurando Informe...");
            Dictionary<string, string> vParams = GetConfigReportParameters();
            dsrCuadreCajaCobroMultimonedaDetallado vRpt = new dsrCuadreCajaCobroMultimonedaDetallado();
            vRpt.CodigoMonedaExtranjera = CodigoMonedaExtranjera;
            vRpt.SimboloMonedaExtranjera = SimboloMonedaExtranjera;
            if (Data.Rows.Count < 1) {
                throw new GalacException("No existen datos para mostrar", eExceptionManagementType.Alert);
            }
            if (vRpt.ConfigReport(Data, vParams)) {
                LibReport.SendReportToDevice(vRpt, 1, PrintingDevice, clsCuadreCajaCobroMultimonedaDetallado.ReportName, true, ExportFileFormat, "", false);
            }
            WorkerReportProgress(100, "Finalizando...");
        }
        #endregion //Metodos Generados


    } //End of class clsCuadreCajaCobroMultimonedaDetallado

} //End of namespace Galac.Adm.Rpt.Venta

