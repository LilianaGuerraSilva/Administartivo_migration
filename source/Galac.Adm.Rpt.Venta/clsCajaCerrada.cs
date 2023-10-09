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

    public class clsCajaCerrada: LibRptBaseMfc {
        #region Propiedades

        protected DataTable Data { get; set; }
        int ConsecutivoCaja { get; set; }
        DateTime FechaDesde { get; set; }
        DateTime FechaHasta { get; set; }
        decimal EfectivoEnCaja { get; set; }
        decimal EfectivoEnCajaME { get; set; }
        #endregion //Propiedades
        #region Constructores
        public clsCajaCerrada(ePrintingDevice initPrintingDevice, eExportFileFormat initExportFileFormat, LibXmlMemInfo initAppMemInfo, LibXmlMFC initMfc, int initConsecutivoCaja, DateTime initFechaDesde, DateTime initFechaHasta) : base(initPrintingDevice, initExportFileFormat, initAppMemInfo, initMfc) {
            FechaDesde = initFechaDesde;
            FechaHasta = initFechaHasta;
            ConsecutivoCaja = initConsecutivoCaja;
        }
        #endregion //Constructores
        #region Metodos Generados

        public static string ReportName {
            get { return new dsrCajaCerrada().ReportTitle(); }
        }

        public override Dictionary<string, string> GetConfigReportParameters() {
            string vTitulo = clsCajaCerrada.ReportName;
            if (UseExternalRpx) {
                vTitulo += " - desde RPX externo.";
            }
            Dictionary<string, string> vParams = new Dictionary<string, string>();
            vParams.Add("NombreCompania", AppMemoryInfo.GlobalValuesGetString("Compania", "Nombre"));
            vParams.Add("TituloInforme", vTitulo);
            #region Codigo Ejemplo
            /* Codigo de Ejemplo
                vParams.Add("FechaInicialYFinal", string.Format("{0} al {1}", LibConvert.ToStr(FechaDesde, "dd/MM/yyyy"), LibConvert.ToStr(FechaHasta, "dd/MM/yyyy")));
            */
            #endregion //Codigo Ejemplo
            return vParams;
        }

        public override void RunReport() {
            decimal vEfectivoEnCaja = 0;
            decimal vEfectivoEnCajaME = 0;
            if (WorkerCancellPending()) {
                return;
            }
            WorkerReportProgress(30, "Obteniendo datos...");
            ICajaInformes vRpt = new Galac.Adm.Brl.Venta.Reportes.clsCajaRpt() as ICajaInformes;
            Data = vRpt.BuildCajaCerrada(Mfc.GetInt("Compania"), ConsecutivoCaja, FechaDesde, FechaHasta, ref vEfectivoEnCaja, ref vEfectivoEnCajaME);
            EfectivoEnCaja = vEfectivoEnCaja;
            EfectivoEnCajaME = vEfectivoEnCajaME;
        }

        public override void SendReportToDevice() {
            WorkerReportProgress(90, "Configurando Informe...");
            Dictionary<string, string> vParams = GetConfigReportParameters();
            dsrCajaCerrada vRpt = new dsrCajaCerrada();
            if (Data == null || Data.Rows == null || Data.Rows.Count < 1) {
                throw new GalacException("No existen datos para mostrar", eExceptionManagementType.Alert);
            }
            if (vRpt.ConfigReport(Data, FechaDesde, FechaHasta, EfectivoEnCaja, EfectivoEnCajaME)) {
                LibReport.SendReportToDevice(vRpt, 1, PrintingDevice, clsCajaCerrada.ReportName, true, ExportFileFormat, "", false);
            }
            WorkerReportProgress(100, "Finalizando...");
        }
        #endregion //Metodos Generados
    } //End of class clsCajaCerrada
} //End of namespace Galac.Adm.Rpt.Venta

