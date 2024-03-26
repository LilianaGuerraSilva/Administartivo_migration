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
using Galac.Saw.Lib;
using LibGalac.Aos.Catching;

namespace Galac.Adm.Rpt.Venta {

    public class clsNotaDeEntregaEntreFechasPorClienteDetallado : LibRptBaseMfc {

        #region Propiedades

        protected DataTable Data { get; set; }
        private DateTime FechaDesde { get; set; }
        private DateTime FechaHasta { get; set; }
        private bool IncluirNotasDeEntregasAnuladas { get; set; }
        private eCantidadAImprimir CantidadAImprimir { get; set; }
        private eMonedaParaImpresion MonedaDelReporte { get; set; }
        private string CodigoCliente { get; set; }
        private bool IncluirDetalleNotasDeEntregas { get; set; }

        #endregion //Propiedades

        #region Constructores

        public clsNotaDeEntregaEntreFechasPorClienteDetallado(ePrintingDevice initPrintingDevice, eExportFileFormat initExportFileFormat, LibXmlMemInfo initAppMemInfo, LibXmlMFC initMfc, DateTime initFechaDesde, DateTime initFechaHasta, bool initIncluirNotasDeEntregasAnuladas, eCantidadAImprimir initCantidadAImprimir, string initCodigoCliente, bool initIncluirDetalleNotasDeEntregas)
            : base(initPrintingDevice, initExportFileFormat, initAppMemInfo, initMfc) {

            FechaDesde = initFechaDesde;
            FechaHasta = initFechaHasta;
            IncluirNotasDeEntregasAnuladas = initIncluirNotasDeEntregasAnuladas;
            CantidadAImprimir = initCantidadAImprimir;
            CodigoCliente = initCodigoCliente;
            IncluirDetalleNotasDeEntregas = initIncluirDetalleNotasDeEntregas;
        }

        #endregion //Constructores

        #region Metodos Generados

        public static string ReportName {
            get { return new dsrNotaDeEntregaEntreFechasPorClienteDetallado().ReportTitle(); }
        }

        public override Dictionary<string, string> GetConfigReportParameters() {
            string vTitulo = clsNotaDeEntregaEntreFechasPorClienteDetallado.ReportName;
            if (UseExternalRpx) {
                vTitulo += " - desde RPX externo.";
            }
            Dictionary<string, string> vParams = new Dictionary<string, string>();
            vParams.Add("NombreCompania", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "Nombre"));
            vParams.Add("RifCompania", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "NumeroDeRif"));
            vParams.Add("TituloInforme", vTitulo);
            vParams.Add("FechaInicialYFinal", string.Format("desde {0} hasta {1}", LibConvert.ToStr(FechaDesde, "dd/MM/yyyy"), LibConvert.ToStr(FechaHasta, "dd/MM/yyyy")));
            vParams.Add("IncluirDetalleNotasDeEntregas", LibConvert.BoolToSN(IncluirDetalleNotasDeEntregas));
            return vParams;
        }

        public override void RunReport() {
            if (WorkerCancellPending()) {
                return;
            }
            WorkerReportProgress(30, "Obteniendo datos...");
            INotaDeEntregaInformes vRpt = new Galac.Adm.Brl.Venta.Reportes.clsNotaDeEntregaRpt() as INotaDeEntregaInformes;
            Data = vRpt.BuildNotaDeEntregaEntreFechasPorClienteDetallado(LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"), FechaDesde, FechaHasta,  CantidadAImprimir, CodigoCliente);
        }

        public override void SendReportToDevice() {
            WorkerReportProgress(90, "Configurando Informe...");
            Dictionary<string, string> vParams = GetConfigReportParameters();
            dsrNotaDeEntregaEntreFechasPorClienteDetallado vRpt = new dsrNotaDeEntregaEntreFechasPorClienteDetallado();
            if (Data.Rows.Count >= 1) {
                if (vRpt.ConfigReport(Data, vParams)) {
                    LibReport.SendReportToDevice(vRpt, 1, PrintingDevice, clsNotaDeEntregaEntreFechasPorClienteDetallado.ReportName, true, ExportFileFormat, "", false);
                }
                WorkerReportProgress(100, "Finalizando...");
            } else {
                throw new GalacException("No se encontró información para imprimir", eExceptionManagementType.Alert);
            }
        }
        #endregion //Metodos Generados


    } //End of class clsNotaDeEntregaEntreFechasPorClienteDetallado

} //End of namespace Galac.Dbo.Rpt.ComponenteNoEspecificado

