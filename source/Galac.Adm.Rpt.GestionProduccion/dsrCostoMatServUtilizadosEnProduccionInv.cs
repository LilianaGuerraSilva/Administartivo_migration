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
    /// Summary description for dsrCostoMatServUtilizadosEnProduccionInv.
    /// </summary>
    public partial class dsrCostoMatServUtilizadosEnProduccionInv : DataDynamics.ActiveReports.ActiveReport {
        
        #region Variables

        private bool _UseExternalRpx;
        private static string _RpxFileName;

        #endregion //Variables

        #region Propiedades

        public string ReportTitle() {
            return "Costos de Materiales o Servicios utilizados en Producción de Inventario";
        }

        public int CantidadDeTiposInventarioProducidos { get; set; }

        public decimal MayorCostoConsumo { get; set; }

        public decimal MenorCostoConsumo { get; set; }

        public string OrdenMayorCosto { get; set; }

        public string OrdenMenorCosto { get; set; }

        #endregion

        #region Constructor
        public dsrCostoMatServUtilizadosEnProduccionInv()
            : this(false, string.Empty) {
        }

        public dsrCostoMatServUtilizadosEnProduccionInv(bool initUseExternalRpx, string initRpxFileName) {
            InitializeComponent();
            _UseExternalRpx = initUseExternalRpx;
            if (_UseExternalRpx) {
                _RpxFileName = initRpxFileName;
            }
        }

        #endregion

        #region Metodos Generados

        public bool ConfigReport(DataTable valDataSource, eMonedaDelInformeMM valMonedaDelInforme, eTasaDeCambioParaImpresion valTasaDeCambio, string valMoneda, Dictionary<string, string> valParameters) {
            if (_UseExternalRpx) {
                string vRpxPath = LibWorkPaths.PathOfRpxFile(_RpxFileName, ReportTitle(), false, LibDefGen.ProgramInfo.ProgramInitials);//acá se indicaría si se busca en ULS, por defecto buscaría en app.path... Tip: Una función con otro nombre.
                if (!LibString.IsNullOrEmpty(vRpxPath, true)) {
                    LibReport.LoadLayout(this, vRpxPath);
                }
            }
            MayorCostoConsumo = LibConvert.ToDec(valDataSource.Compute("MAX(MontoSubTotal)", ""), 2);
            MenorCostoConsumo = LibConvert.ToDec(valDataSource.Compute("MIN(MontoSubTotal)", ""), 2);
            if (MayorCostoConsumo != MenorCostoConsumo) {
                OrdenMayorCosto = OrdenSegunCosto(valDataSource, MayorCostoConsumo);
                OrdenMenorCosto = OrdenSegunCosto(valDataSource, MenorCostoConsumo);
            }
            if (LibReport.ConfigDataSource(this, valDataSource)) {
                LibReport.ConfigFieldStr(this, "txtNombreCompania", valParameters["NombreCompania"], string.Empty);
                LibReport.ConfigLabel(this, "lblTituloInforme", ReportTitle());
                LibReport.ConfigLabel(this, "lblFechaInicialYFinal", valParameters["FechaInicialYFinal"]);
                LibReport.ConfigLabel(this, "lblFechaYHoraDeEmision", LibReport.PromptEmittedOnDateAtHour);
                LibReport.ConfigHeader(this, "txtNombreCompania", "lblFechaYHoraDeEmision", "lblTituloInforme", "txtNroDePagina", "lblFechaInicialYFinal", LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintPageNumber, LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintEmitDate);
                LibReport.ConfigFieldStr(this, "txtInventarioProducido", string.Empty, "InventarioProducido");
                LibReport.ConfigFieldStr(this, "txtMoneda", string.Empty, "Moneda");
                LibReport.ConfigFieldDec(this, "txtCambio", string.Empty, "Cambio", "n" + 4, true, TextAlignment.Right);
                LibReport.ConfigFieldStr(this, "txtArticuloUtilizado", string.Empty, "ArticuloServicioUtilizado");
                LibReport.ConfigFieldStr(this, "txtCodigoOrden", string.Empty, "Orden");
                LibReport.ConfigFieldDate(this, "txtFechaFinalizacion", string.Empty, "FechaFinalizacion", LibGalac.Aos.Base.Report.eDateOutputFormat.DateLong);
                LibReport.ConfigFieldDec(this, "txtCantidadConsumida", string.Empty, "CantidadConsumida", "n" + 8, true, TextAlignment.Right);
                LibReport.ConfigFieldDec(this, "txtCostoUnitario", string.Empty, "CostoUnitarioArticuloInventario", "n" + 8, true, TextAlignment.Right);
                LibReport.ConfigFieldDec(this, "txtCostoTotal", string.Empty, "MontoSubtotal", "n" + 8, true, TextAlignment.Right);

                LibReport.ConfigGroupHeader(this, "GHSecInventarioProducido", "InventarioProducido", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.None);
                LibReport.ConfigGroupHeader(this, "GHSecArticuloUtilizado", "ArticuloServicioUtilizado", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.None);

                LibReport.ConfigSummaryField(this, "txt_TCantidadConsumida", "CantidadConsumida", SummaryFunc.Sum, "GHSecArticuloUtilizado", SummaryRunning.Group, SummaryType.SubTotal, "n" + 8, "");
                LibReport.ConfigSummaryField(this, "txt_TCostoTotal", "MontoSubtotal", SummaryFunc.Sum, "GHSecArticuloUtilizado", SummaryRunning.Group, SummaryType.SubTotal, "n" + 8, "");
                LibReport.ConfigSummaryField(this, "txt_TTCostoConsumo", "MontoSubTotal", SummaryFunc.Sum, "PageHeader", SummaryRunning.Group, SummaryType.GrandTotal, "n" + 8, "");

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
            string vArtServ = string.Empty;
            string vFilter = new QAdvSql("").SqlDecValueWithAnd("", "MontoSubTotal", valCostoProduccion);
            DataRow[] vDrs = valDataSource.Select(vFilter);
            if (vDrs.Length > 0) {
                for (int i = 0; i < vDrs.Length; i++) {
                    if (!LibText.IsNullOrEmpty(vResult.ToString())) {
                        vResult.Append("/");
                    }
                    vResult.Append(LibConvert.ToStr(vDrs[i]["ArticuloServicioUtilizado"]) + "- Orden: " + LibConvert.ToStr(vDrs[i]["Orden"]));
                }
            }
            return vResult.ToString();
        }

        #endregion

        #region Eventos del dsr

        private void Detail_Format(object sender, EventArgs e) {

        }

        private void PageFooter_Format(object sender, EventArgs e) {
            this.txt_TTMayorCostoConsumo.Value = LibConvert.ToStr(MayorCostoConsumo, 8);
            this.txt_TTMenorCostoConsumo.Value = LibConvert.ToStr(MenorCostoConsumo, 8);
            this.txtOrdenConMayorCosto.Value = LibConvert.ToStr(OrdenMayorCosto);
            this.txtOrdenConMenorCosto.Value = LibConvert.ToStr(OrdenMenorCosto);
        }

        #endregion
        
    }
}
