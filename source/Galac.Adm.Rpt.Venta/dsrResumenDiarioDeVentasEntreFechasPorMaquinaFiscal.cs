using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Document;
using System.Data;
using LibGalac.Aos.Base;
using LibGalac.Aos.ARRpt;
using LibGalac.Aos.DefGen;

namespace Galac.Adm.Rpt.Venta
{
    /// <summary>
    /// Summary description for rpxResumenDiarioDeVentasEntreFechasMaquinaFiscal.
    /// </summary>
    public partial class dsrResumenDiarioDeVentasEntreFechasPorMaquinaFiscal : ActiveReport {
        #region Variables
        private bool _UseExternalRpx;
        private static string _RpxFileName;
        #endregion //Variables

        #region Constructores
        public dsrResumenDiarioDeVentasEntreFechasPorMaquinaFiscal()
            : this(false, string.Empty) {
        }

        public dsrResumenDiarioDeVentasEntreFechasPorMaquinaFiscal(bool initUseExternalRpx, string initRpxFileName) {
            InitializeComponent();
            _UseExternalRpx = initUseExternalRpx;
            if (_UseExternalRpx) {
                _RpxFileName = initRpxFileName;
            }
        }
        #endregion //Constructores

        #region Metodos Generados
        public string ReportTitle() {
            return "Resumen Diario de Ventas entre Fechas por Impresora Fiscal";
        }

        public bool ConfigReport(DataTable valDataSource, Dictionary<string, string> valParameters) {
            bool AgruparPorMaquinaFiscal = LibConvert.SNToBool(valParameters["AgruparPorMaquinaFiscal"]);

            if (_UseExternalRpx) {
                string vRpxPath = LibWorkPaths.PathOfRpxFile(_RpxFileName, ReportTitle(), false, LibDefGen.ProgramInfo.ProgramInitials);//acá se indicaría si se busca en ULS, por defecto buscaría en app.path... Tip: Una función con otro nombre.
                if (!LibString.IsNullOrEmpty(vRpxPath, true)) {
                    LibReport.LoadLayout(this, vRpxPath);
                }
            }
            if (LibReport.ConfigDataSource(this, valDataSource)) {
                LibReport.ConfigFieldStr(this, "txtCompania", valParameters["NombreCompania"], string.Empty);
                LibReport.ConfigLabel(this, "lblTituloDelReporte", ReportTitle());
                LibReport.ConfigLabel(this, "lblFechaInicialYFinal", valParameters["FechaInicialYFinal"]);
                LibReport.ConfigLabel(this, "lblFechaYHoraDeEmision", LibReport.PromptEmittedOnDateAtHour);
                LibReport.ConfigHeader(this, "txtNombreCompania", "lblFechaYHoraDeEmision", "lblTituloInforme", "txtNumeroDePagina", "lblFechaInicialYFinal", LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintPageNumber, LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintEmitDate);

                LibReport.ConfigGroupHeader(this, "GHMaquinaFiscal", "ConsecutivoMaquinaFiscal", GroupKeepTogether.FirstDetail, RepeatStyle.OnPageIncludeNoDetail, true, NewPage.None);
                LibReport.ConfigFieldStr(this, "txtDescripcion", string.Empty, "Descripcion");
				LibReport.ConfigFieldStr(this, "txtConsecutivoMaquinaFiscal", string.Empty, "ConsecutivoMaquinaFiscal");
				LibReport.ConfigFieldStr(this, "txtNumeroRegistro", string.Empty, "NumeroRegistro");

                LibReport.ConfigGroupHeader(this, "GHMoneda", "Moneda", GroupKeepTogether.FirstDetail, RepeatStyle.OnPageIncludeNoDetail, true, NewPage.BeforeAfter);
                LibReport.ConfigFieldStr(this, "txtMoneda", string.Empty, "Moneda");

				LibReport.ConfigFieldDate(this, "txtFecha", string.Empty, "Fecha", LibGalac.Aos.Base.Report.eDateOutputFormat.DateShort);
				LibReport.ConfigFieldStr(this, "txtNumeroDesde", string.Empty, "NumeroDesde");
				LibReport.ConfigFieldStr(this, "txtNumeroHasta", string.Empty, "NumeroHasta");
				LibReport.ConfigFieldDec(this, "txtBaseImponible", string.Empty, "TotalBaseImponible");
				LibReport.ConfigFieldDec(this, "txtMontoExento", string.Empty, "TotalMontoExento");
				LibReport.ConfigFieldDec(this, "txtIVA", string.Empty, "TotalIVA");
				LibReport.ConfigFieldDec(this, "txtTotal", string.Empty, "TotalFactura");

                LibReport.ConfigSummaryField(this, "txtTotalBaseImponible", "TotalBaseImponible", SummaryFunc.Sum, "GHMoneda", SummaryRunning.All, SummaryType.SubTotal);
                LibReport.ConfigSummaryField(this, "txtTotalMontoExento", "TotalMontoExento", SummaryFunc.Sum, "GHMoneda", SummaryRunning.All, SummaryType.SubTotal);
                LibReport.ConfigSummaryField(this, "txtTotalIVA", "TotalIVA", SummaryFunc.Sum, "GHMoneda", SummaryRunning.All, SummaryType.SubTotal);
                LibReport.ConfigSummaryField(this, "txtTotales", "TotalFactura", SummaryFunc.Sum, "GHMoneda", SummaryRunning.All, SummaryType.SubTotal);

                LibGraphPrnMargins.SetGeneralMargins(this, PageOrientation.Landscape);
                return true;
            }
            return false;
        }
        #endregion //Metodos Generados

    } //End of class dsrResumenDiarioDeVentasEntreFechas

} //End of namespace Galac.Adm.Rpt.Venta

