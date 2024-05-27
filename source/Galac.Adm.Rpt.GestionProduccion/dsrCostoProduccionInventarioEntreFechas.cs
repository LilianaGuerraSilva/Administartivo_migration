using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Document;
using LibGalac.Aos.Base;
using Galac.Adm.Brl.GestionProduccion;
using Galac.Adm.Ccl.GestionProduccion;
using LibGalac.Aos.ARRpt;
using System.Data;
using LibGalac.Aos.DefGen;
using LibGalac.Aos.Base.Dal;
using System.Text;
using Galac.Saw.Lib;

namespace Galac.Adm.Rpt.GestionProduccion {
    /// <summary>
    /// Summary description for CostoProduccionInventarioEntreFechas.
    /// </summary>
    public partial class dsrCostoProduccionInventarioEntreFechas : DataDynamics.ActiveReports.ActiveReport {
        #region Variables
        private bool _UseExternalRpx;
        private static string _RpxFileName;
        #endregion //Variables
        #region propiedades
        public int CantidadDeTiposInventarioProducidos { get; set; }

        public decimal MayorCostoProduccion { get; set; }

        public decimal MenorCostoDeProduccion { get; set; }

        public string OrdenMayorCosto { get; set; }

        public string OrdenMenorCosto { get; set; }

        #endregion
        #region Constructor
        public dsrCostoProduccionInventarioEntreFechas()
            : this(false, string.Empty) {
        }

        public dsrCostoProduccionInventarioEntreFechas(bool initUseExternalRpx, string initRpxFileName) {
            InitializeComponent();
            _UseExternalRpx = initUseExternalRpx;
            if (_UseExternalRpx) {
                _RpxFileName = initRpxFileName;
            }
        }
        #endregion
        #region Metodos Generados
        public string ReportTitle() {
            return "Costo Producción de Inventario Entre Fechas";
        }

        public bool ConfigReport(DataTable valDataSource, eMonedaDelInformeMM valMonedaDelInforme, eTasaDeCambioParaImpresion valTasaDeCambio, string valMoneda, Dictionary<string, string> valParameters) {
            if (_UseExternalRpx) {
                string vRpxPath = LibWorkPaths.PathOfRpxFile(_RpxFileName, ReportTitle(), false, LibDefGen.ProgramInfo.ProgramInitials);//acá se indicaría si se busca en ULS, por defecto buscaría en app.path... Tip: Una función con otro nombre.
                if (!LibString.IsNullOrEmpty(vRpxPath, true)) {
                    LibReport.LoadLayout(this, vRpxPath);
                }
            }
            MayorCostoProduccion = LibConvert.ToDec(valDataSource.Compute("MAX(MontoSubTotal)", ""), 8);
            MenorCostoDeProduccion = LibConvert.ToDec(valDataSource.Compute("MIN(MontoSubTotal)", ""), 8);
            if (MayorCostoProduccion != MenorCostoDeProduccion) {
                OrdenMayorCosto = OrdenSegunCosto(valDataSource, MayorCostoProduccion);
                OrdenMenorCosto = OrdenSegunCosto(valDataSource, MenorCostoDeProduccion);
            }
            if (LibReport.ConfigDataSource(this, valDataSource)) {
                LibReport.ConfigFieldStr(this, "txtNombreCompania", valParameters["NombreCompania"], string.Empty);
                LibReport.ConfigLabel(this, "lblTituloInforme", ReportTitle());
                LibReport.ConfigLabel(this, "lblFechaInicialYFinal", valParameters["FechaInicialYFinal"]);
                LibReport.ConfigLabel(this, "lblFechaYHoraDeEmision", LibReport.PromptEmittedOnDateAtHour);
                LibReport.ConfigHeader(this, "txtNombreCompania", "lblFechaYHoraDeEmision", "lblTituloInforme", "txtNroDePagina", "lblFechaInicialYFinal", LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintPageNumber, LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintEmitDate);
                LibReport.ConfigFieldStr(this, "txtCodigoOrden", string.Empty, "Codigo");
                LibReport.ConfigFieldStr(this, "txtMoneda", string.Empty, "Moneda");
                LibReport.ConfigFieldDec(this, "txtCambio", string.Empty, "Cambio", "n" + 4, true, TextAlignment.Right);
                LibReport.ConfigFieldDate(this, "txtFechaFinalizacion", string.Empty, "FechaFinalizacion", LibGalac.Aos.Base.Report.eDateOutputFormat.DateLong);
                LibReport.ConfigFieldStr(this, "txtInventarioProducido", string.Empty, "ArticuloInventario");
                LibReport.ConfigFieldDec(this, "txtCantidadProducida", string.Empty, "CantidadProducida", "n" + 8, true, TextAlignment.Right);
                LibReport.ConfigFieldDec(this, "txtCostoUnitario", string.Empty, "CostoUnitario", "n" + 8, true, TextAlignment.Right);
                LibReport.ConfigFieldDec(this, "txtCostoTotalOrden", string.Empty, "MontoSubTotal", "n" + 8, true, TextAlignment.Right);

                LibReport.ConfigGroupHeader(this, "GHSecInventarioProducido", "ArticuloInventario", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.None);

                LibReport.ConfigSummaryField(this, "txt_TCantidadTotalProducida", "CantidadProducida", SummaryFunc.Sum, "GHSecInventarioProducido", SummaryRunning.Group, SummaryType.SubTotal);
                LibReport.ConfigSummaryField(this, "txt_TCostoTotalProduccion", "MontoSubTotal", SummaryFunc.Sum, "GHSecInventarioProducido", SummaryRunning.Group, SummaryType.SubTotal, "n" + 8, "");

                LibReport.ConfigSummaryField(this, "txt_TTCantidadProducidas", "CantidadProducida", SummaryFunc.Sum, "PageHeader", SummaryRunning.Group, SummaryType.GrandTotal);
                LibReport.ConfigSummaryField(this, "txt_TTCostoProduccion", "MontoSubTotal", SummaryFunc.Sum, "PageHeader", SummaryRunning.Group, SummaryType.GrandTotal, "n" + 8, "");

                string vNotaMonedaCambio = new clsLibSaw().NotaMonedaCambioParaInformes(valMonedaDelInforme, valTasaDeCambio, valMoneda, "Orden de Producción");
                LibReport.ConfigFieldStr(this, "txtNotaMonedaCambio", vNotaMonedaCambio, "");

                LibGraphPrnMargins.SetGeneralMargins(this, DataDynamics.ActiveReports.Document.PageOrientation.Portrait);
                LibReport.AddNoDataEvent(this);
                return true;
            }
            return false;
        }
        #endregion
        #region Código Programador
        string OrdenSegunCosto(DataTable valDataSource, decimal valCostoProduccion) {
            StringBuilder vResult = new StringBuilder();
            string vFilter = new QAdvSql("").SqlDecValueWithAnd("", "MontoSubTotal", valCostoProduccion);
            DataRow[] vDrs = valDataSource.Select(vFilter);
            if (vDrs.Length > 0) {
                for (int i = 0; i < vDrs.Length; i++) {
                    if (!LibText.IsNullOrEmpty(vResult.ToString())) {
                        vResult.Append("/");
                    }
                    vResult.Append(LibConvert.ToStr(vDrs[i]["Codigo"]));
                }

            }
            return vResult.ToString();
        }

        #region Eventos del dsr
        private void Detail_Format(object sender, EventArgs e) {

        }

        private void GHSecInventarioProducido_Format(object sender, EventArgs e) {

        }

        private void GFSecInventarioProducido_Format(object sender, EventArgs e) {
            CalculosParaElPeriodo();
            CantidadDeTiposInventarioProducidos = CantidadDeTiposInventarioProducidos + 1;
        }

        private void PageFooter_Format(object sender, EventArgs e) {
            this.txt_TTProductosServicios.Value = CantidadDeTiposInventarioProducidos;
            this.txt_TTMayorCostoDeProduccion.Value = LibConvert.ToStr(MayorCostoProduccion, 8);
            this.txt_TTMenorCostoDeProduccion.Value = LibConvert.ToStr(MenorCostoDeProduccion, 8);
            this.txtOrdenConMayorCosto.Value = "Orden(es):" + LibConvert.ToStr(OrdenMayorCosto);
            this.txtOrdenConMenorCosto.Value = "Orden(es):" + LibConvert.ToStr(OrdenMenorCosto);
        }

        private void CalculosParaElPeriodo() {
            decimal vCostoPromedio = 0;
            decimal vCostoTotalProduccion = LibConvert.ToDec(this.txt_TCostoTotalProduccion.Text, 8);
            decimal vCantidadTotalProducida = LibConvert.ToDec(this.txt_TCantidadTotalProducida.Text, 2);
            if (vCantidadTotalProducida > 0) {
                vCostoPromedio = vCostoTotalProduccion / vCantidadTotalProducida;
            }
            this.txt_TCostoUnitarioPromedio.Value = LibConvert.ToStr(vCostoPromedio, 8);
        }
        #endregion
        #endregion
    }
}
