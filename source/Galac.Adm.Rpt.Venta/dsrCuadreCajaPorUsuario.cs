using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Document;
using System.Data;
using LibGalac.Aos.ARRpt;
using LibGalac.Aos.Base;
using LibGalac.Aos.DefGen;
namespace Galac.Adm.Rpt.Venta {
    /// <summary>
    /// Summary description for dsrCuadreCajaPorUsuario.
    /// </summary>
    public partial class dsrCuadreCajaPorUsuario: DataDynamics.ActiveReports.ActiveReport {
        #region Variables
        private bool _UseExternalRpx;
        private static string _RpxFileName;
        #endregion //Variables
        #region Propiedades
        private Galac.Comun.Ccl.TablasGen.IMonedaLocalActual vMonedaLocal = new Galac.Comun.Brl.TablasGen.clsMonedaLocalActual();
        public string CodigoMonedaExtranjera { get; set; }
        public string SimboloMonedaExtranjera { get; set; }
        #endregion
        #region Constructores
        public dsrCuadreCajaPorUsuario()
            : this(false, string.Empty) {
            vMonedaLocal.CargarTodasEnMemoriaYAsignarValoresDeLaActual(LibDefGen.ProgramInfo.Country, LibDate.Today());
        }
        public dsrCuadreCajaPorUsuario(bool initUseExternalRpx, string initRpxFileName) {
            InitializeComponent();
            _UseExternalRpx = initUseExternalRpx;
            if (_UseExternalRpx) {
                _RpxFileName = initRpxFileName;
            }
        }
        #endregion //Constructores
        #region Metodos Generados

        public string ReportTitle() {
            return "Cuadre de Caja Por Usuario";
        }

        public bool ConfigReport(DataTable valDataSource, Dictionary<string, string> valParameters) {
            string vNombreCompania = valParameters["NombreCompania"] + " - " + valParameters["RifCompania"];
            if (_UseExternalRpx) {
                string vRpxPath = LibWorkPaths.PathOfRpxFile(_RpxFileName, ReportTitle(), false, LibDefGen.ProgramInfo.ProgramInitials);//acá se indicaría si se busca en ULS, por defecto buscaría en app.path... Tip: Una función con otro nombre.
                if (!LibString.IsNullOrEmpty(vRpxPath, true)) {
                    LibReport.LoadLayout(this, vRpxPath);
                }
            }
            if (LibReport.ConfigDataSource(this, valDataSource)) {
                LibReport.ConfigFieldStr(this, "txtNombreCompania", vNombreCompania, string.Empty);
                LibReport.ConfigLabel(this, "lblTituloInforme", ReportTitle());
                LibReport.ConfigLabel(this, "lblFechaInicialYFinal", valParameters["FechaInicialYFinal"]);
                LibReport.ConfigLabel(this, "lblFechaYHoraDeEmision", LibReport.PromptEmittedOnDateAtHour);
                LibReport.ConfigHeader(this, "txtNombreCompania", "lblFechaYHoraDeEmision", "lblTituloInforme", "txtNroDePagina", "lblFechaInicialYFinal", LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintPageNumber, LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintEmitDate);
                #region Columnas (Cabeceras del informe)
                LibReport.ConfigFieldStr(this, "txtNombreUsuario", string.Empty, "NombreUsuario");
                LibReport.ConfigFieldStr(this, "txtNombreCaja", string.Empty, "NombreCaja");
                LibReport.ConfigFieldStr(this, "txtMonedaDoc", string.Empty, "MonedaDoc");
                LibReport.ConfigFieldStr(this, "txtMonedaCobro", string.Empty, "MonedaCobro");
                LibReport.ConfigFieldStr(this, "txtCodMonedaCobro", string.Empty, "CodMonedaCobro");
                LibReport.ConfigFieldDate(this, "txtFecha", string.Empty, "Fecha", "dd/MM/yyyy");
                LibReport.ConfigFieldStr(this, "txtNumeroDoc", string.Empty, "NumeroDoc");
                LibReport.ConfigFieldStr(this, "txtNumeroCompFiscal", string.Empty, "NumFiscal");
                LibReport.ConfigFieldStr(this, "txtNombreCliente", string.Empty, "NombreCliente");
                LibReport.ConfigFieldDecWithNDecimal(this, "txtMontoDoc", string.Empty, "MontoDoc", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtMontoPagado", string.Empty, "MontoPagado", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtMontoVuelto", string.Empty, "Vuelto", 2);
                #endregion
                #region Totales
                // Por Moneda de Cobro
                LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalMontoDoc", LibConvert.ToStr((decimal)0), "", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalMontoPagado", LibConvert.ToStr((decimal)0), "", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalMontoVuelto", LibConvert.ToStr((decimal)0), "", 2);
                LibReport.ConfigSummaryField(this, "txtTotalMontoDoc", "MontoDoc", SummaryFunc.Sum, "GHSecMonedaCobro", SummaryRunning.Group, SummaryType.SubTotal);
                LibReport.ConfigSummaryField(this, "txtTotalMontoPagado", "MontoPagado", SummaryFunc.Sum, "GHSecMonedaCobro", SummaryRunning.Group, SummaryType.SubTotal);
                LibReport.ConfigSummaryField(this, "txtTotalMontoVuelto", "Vuelto", SummaryFunc.Sum, "GHSecMonedaCobro", SummaryRunning.Group, SummaryType.SubTotal);
                // Por Caja
                LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalMontoPagadoCajaMonedaLocal", LibConvert.ToStr((decimal)0), "", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalMontoVueltoCajaMonedaLocal", LibConvert.ToStr((decimal)0), "", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalMontoPagadoCajaMonedaExt", LibConvert.ToStr((decimal)0), "", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalMontoVueltoCajaMonedaExt", LibConvert.ToStr((decimal)0), "", 2);
                // Por Operador (Usuario)
                LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalMontoPagadoOperadorMonedaLocal", LibConvert.ToStr((decimal)0), "", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalMontoVueltoOperadorMonedaLocal", LibConvert.ToStr((decimal)0), "", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalMontoPagadoOperadorMonedaExt", LibConvert.ToStr((decimal)0), "", 2);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalMontoVueltoOperadorMonedaExt", LibConvert.ToStr((decimal)0), "", 2);
                #endregion

                #region Simbolos
                LibReport.ConfigLabel(this, "lblTotalesPorCajaMonedaLocal", "Total Caja en " + vMonedaLocal.GetHoySimboloMoneda());
                LibReport.ConfigLabel(this, "lblTotalesPorOperadorMonedaLocal", "Total Operador en " + vMonedaLocal.GetHoySimboloMoneda());
                LibReport.ConfigLabel(this, "lblTotalesPorCajaMonedaExt", "Total Caja en " + SimboloMonedaExtranjera);
                LibReport.ConfigLabel(this, "lblTotalesPorOperadorMonedaExt", "Total Operador en " + SimboloMonedaExtranjera);
                #endregion

                #region Campos condicionados a la moneda extranjera
                if (LibString.IsNullOrEmpty(CodigoMonedaExtranjera)) {
                    LibReport.ChangeControlVisibility(this, "txtTotalMontoPagadoCajaMonedaExt", false);
                    LibReport.ChangeControlVisibility(this, "txtTotalMontoVueltoCajaMonedaExt", false);
                    LibReport.ChangeControlVisibility(this, "txtTotalMontoPagadoOperadorMonedaExt", false);
                    LibReport.ChangeControlVisibility(this, "txtTotalMontoVueltoOperadorMonedaExt", false);
                    LibReport.ChangeControlVisibility(this, "lblTotalesPorCajaMonedaExt", false);
                    LibReport.ChangeControlVisibility(this, "lblTotalesPorOperadorMonedaExt", false);
                }
                #endregion
                LibReport.ConfigGroupHeader(this, "GHSecOperador", "NombreUsuario", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.None);
                LibReport.ConfigGroupHeader(this, "GHSecCaja", "NombreCaja", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.None);
                LibReport.ConfigGroupHeader(this, "GHSecMonedaDoc", "MonedaDoc", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.None);
                LibReport.ConfigGroupHeader(this, "GHSecMonedaCobro", "MonedaCobro", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.None);
                LibGraphPrnMargins.SetGeneralMargins(this, DataDynamics.ActiveReports.Document.PageOrientation.Portrait);
                return true;
            }
            return false;
        }

        #region Totales de Caja
        private decimal totalMontoPagadoCajaMonedaLocal;
        private decimal totalVueltoCajaMonedaLocal;
        private decimal totalMontoPagadoCajaMonedaExt;
        private decimal totalVueltoCajaMonedaExt;
        #endregion

        #region Totales de Operador
        private decimal totalMontoPagadoOperadorMonedaLocal;
        private decimal totalVueltoOperadorMonedaLocal;
        private decimal totalMontoPagadoOperadorMonedaExt;
        private decimal totalVueltoOperadorMonedaExt;
        #endregion

        #endregion //Metodos Generados

        private void GHSecOperador_Format(object sender, EventArgs e) {
            totalMontoPagadoOperadorMonedaLocal = 0;
            totalVueltoOperadorMonedaLocal = 0;
            totalMontoPagadoOperadorMonedaExt = 0;
            totalVueltoOperadorMonedaExt = 0;
        }

        private void GHSecCaja_Format(object sender, EventArgs e) {
            totalMontoPagadoCajaMonedaLocal = 0;
            totalVueltoCajaMonedaLocal = 0;
            totalMontoPagadoCajaMonedaExt = 0;
            totalVueltoCajaMonedaExt = 0;
        }

        private void Detail_Format(object sender, EventArgs e) {
            string vCodMonedaCobroRenglon = string.Empty;
            vCodMonedaCobroRenglon = txtCodMonedaCobro.Text;
            string vCodigoMonedaLocal = vMonedaLocal.GetHoyCodigoMoneda();
            if (LibString.S1IsEqualToS2(vCodMonedaCobroRenglon, vCodigoMonedaLocal)) {
                totalMontoPagadoCajaMonedaLocal += LibConvert.ToDec(txtMontoPagado.Text);
                totalVueltoCajaMonedaLocal += LibConvert.ToDec(txtMontoVuelto.Text);
                totalMontoPagadoOperadorMonedaLocal += LibConvert.ToDec(txtMontoPagado.Text);
                totalVueltoOperadorMonedaLocal += LibConvert.ToDec(txtMontoVuelto.Text);
            } else if (!LibString.IsNullOrEmpty(CodigoMonedaExtranjera) && LibString.S1IsEqualToS2(vCodMonedaCobroRenglon, CodigoMonedaExtranjera)) {
                totalMontoPagadoCajaMonedaExt += LibConvert.ToDec(txtMontoPagado.Text);
                totalVueltoCajaMonedaExt += LibConvert.ToDec(txtMontoVuelto.Text);
                totalMontoPagadoOperadorMonedaExt += LibConvert.ToDec(txtMontoPagado.Text);
                totalVueltoOperadorMonedaExt += LibConvert.ToDec(txtMontoVuelto.Text);
            }
        }

        private void GFSecCaja_Format(object sender, EventArgs e) {
            txtTotalMontoPagadoCajaMonedaLocal.Text = LibConvert.ToStr(totalMontoPagadoCajaMonedaLocal);
            txtTotalMontoVueltoCajaMonedaLocal.Text = LibConvert.ToStr(totalVueltoCajaMonedaLocal);
            txtTotalMontoPagadoCajaMonedaExt.Text = LibConvert.ToStr(totalMontoPagadoCajaMonedaExt);
            txtTotalMontoVueltoCajaMonedaExt.Text = LibConvert.ToStr(totalVueltoCajaMonedaExt);
        }

        private void GFSecOperador_Format(object sender, EventArgs e) {
            txtTotalMontoPagadoOperadorMonedaLocal.Text = LibConvert.ToStr(totalMontoPagadoOperadorMonedaLocal);
            txtTotalMontoVueltoOperadorMonedaLocal.Text = LibConvert.ToStr(totalVueltoOperadorMonedaLocal);
            txtTotalMontoPagadoOperadorMonedaExt.Text = LibConvert.ToStr(totalMontoPagadoOperadorMonedaExt);
            txtTotalMontoVueltoOperadorMonedaExt.Text = LibConvert.ToStr(totalVueltoOperadorMonedaExt);
        }
    }//End of class dsrCuadreCajaPorUsuario
}//End of namespace Galac.Adm.Rpt.Venta
