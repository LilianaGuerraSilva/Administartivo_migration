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
using Galac.Saw.Ccl.Vendedor;
using Galac.Saw.Brl.Vendedor;

namespace Galac.Adm.Rpt.Venta {
    /// <summary>
    /// Summary description for dsrComisionDeVendedoresPorCobranzaMontoResumido.
    /// </summary>
    public partial class dsrComisionDeVendedoresPorCobranzaMontoResumido : DataDynamics.ActiveReports.ActiveReport {
        #region Variables
        private bool _UseExternalRpx;
        private static string _RpxFileName;
        #endregion //Variables
        #region Propiedades
        private Galac.Comun.Ccl.TablasGen.IMonedaLocalActual vMonedaLocal = new Galac.Comun.Brl.TablasGen.clsMonedaLocalActual();
        public decimal TasaDeCambioComisionMonedaExt { get; set; }
        public string SimboloMonedaExtranjera { get; set; }
        private bool UsaAsignacionDeComisionEnCobranza { get; set; }
        #endregion
        #region Constructores
        public dsrComisionDeVendedoresPorCobranzaMontoResumido()
            : this(false, string.Empty) {
            vMonedaLocal.CargarTodasEnMemoriaYAsignarValoresDeLaActual(LibDefGen.ProgramInfo.Country, LibDate.Today());
        }

        public dsrComisionDeVendedoresPorCobranzaMontoResumido(bool initUseExternalRpx, string initRpxFileName) {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
            _UseExternalRpx = initUseExternalRpx;
            if (_UseExternalRpx) {
                _RpxFileName = initRpxFileName;
            }
        }

		#endregion //Constructores
        #region Metodos Generados
		 public string ReportTitle() {
            return "Comisión de Vendedores por Cobranzas (Resumido)";
        }

        public bool ConfigReport(DataTable valDataSource, Dictionary<string, string> valParameters) {
            Saw.Lib.clsUtilRpt vUtilRpt = new Saw.Lib.clsUtilRpt();
            string vMensajeDeCambio = string.Empty;
            const string vNotaFooter = "El cálculo de las comisiones se hace en base a los porcentajes de cobranza por vendedores (Niveles de comisión)";
            bool vEsEnMonedaOriginal = LibConvert.SNToBool(valParameters["EnMonedaOriginal"]);
            bool vIncluirComisionEnMonedaExt = LibConvert.SNToBool(valParameters["IncluirComisionEnMonedaExt"]);
            bool vEsTasaDeCambioOriginalAMonedaLocal = LibConvert.SNToBool(valParameters["TasaDeCambioOriginalAMonedaLocal"]);
            UsaAsignacionDeComisionEnCobranza = LibConvert.SNToBool(valParameters["UsaAsignacionDeComisionEnCobranza"]);
            if (_UseExternalRpx) {
                string vRpxPath = LibWorkPaths.PathOfRpxFile(_RpxFileName, ReportTitle(), false, LibDefGen.ProgramInfo.ProgramInitials);//acá se indicaría si se busca en ULS, por defecto buscaría en app.path... Tip: Una función con otro nombre.
                if (!LibString.IsNullOrEmpty(vRpxPath, true)) {
                    LibReport.LoadLayout(this, vRpxPath);
                }
            }
            if (LibReport.ConfigDataSource(this, valDataSource)) {
                LibReport.ConfigFieldStr(this, "txtNombreCompania", valParameters["NombreCompania"], string.Empty);
                LibReport.ConfigLabel(this, "lblTituloInforme", ReportTitle());
                LibReport.ConfigLabel(this, "lblFechaInicialYFinal", valParameters["FechaInicialYFinal"]);
                LibReport.ConfigLabel(this, "lblFechaYHoraDeEmision", LibReport.PromptEmittedOnDateAtHour);
                LibReport.ConfigHeader(this, "txtNombreCompania", "lblFechaYHoraDeEmision", "lblTituloInforme", "txtNroDePagina", "lblFechaInicialYFinal", LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintPageNumber, LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintEmitDate);

				LibReport.ConfigFieldStr(this, "txtCodigoVendedor", string.Empty, "CodigoDelVendedor");
				LibReport.ConfigFieldStr(this, "txtNombreVendedor", string.Empty, "NombreDelVendedor");
				LibReport.ConfigFieldStr(this, "txtMonedaCobranza", string.Empty, "MonedaCobranza");
				LibReport.ConfigFieldDate(this, "txtFecha", string.Empty, "FechaDoc", "dd/MM/yyyy");
				LibReport.ConfigFieldStr(this, "txtNumeroDoc", string.Empty, "NumeroDelDocumento");
				LibReport.ConfigFieldStr(this, "txtNombreCliente", string.Empty, "NombreCliente");
				LibReport.ConfigFieldStr(this, "txtMonedaDoc", string.Empty, "SimboloMonedaDoc");
				LibReport.ConfigFieldDecWithNDecimal(this, "txtCambioABolivares", string.Empty, "CambioABolivaresDoc", 2);
				LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalComisionable", string.Empty, "", 2);
                if (vEsEnMonedaOriginal) {
				    LibReport.ConfigFieldDecWithNDecimal(this, "txtMontoTotalCobranza", string.Empty, "MontoAbonado", 2);
				    LibReport.ConfigFieldDecWithNDecimal(this, "txtMontoComisionable", string.Empty, "MontoComisionable", 2);
                    LibReport.ConfigSummaryField(this, "txtTotalComisionable", "MontoComisionable", SummaryFunc.Sum, "GHSecVendedor", SummaryRunning.Group, SummaryType.SubTotal);
                    LibReport.ConfigSummaryField(this, "txtTotalGeneralComisionable", "MontoComisionable", SummaryFunc.Sum, "GHSecMonedaCobranza", SummaryRunning.Group, SummaryType.SubTotal);
                    LibReport.ConfigSummaryField(this, "txtTotalComisionableEnMonedaLocal", "MontoComisionableEnMonedaLocal", SummaryFunc.Sum, "GHSecVendedor", SummaryRunning.Group, SummaryType.SubTotal);
                    LibReport.ChangeSectionPropertiesVisibleAndHeight(this, "GHSecNotaDeCambio", false, (float)0);
                } else {
                    LibReport.ConfigFieldDecWithNDecimal(this, "txtMontoTotalCobranza", string.Empty, "MontoAbonado", 2);
				    LibReport.ConfigFieldDecWithNDecimal(this, "txtMontoComisionable", string.Empty, "MontoComisionableEnMonedaLocal", 2);
                    LibReport.ConfigLabel(this, "lblMonedaCobranza", "Montos en:");
                    LibReport.ConfigSummaryField(this, "txtTotalComisionable", "MontoComisionableEnMonedaLocal", SummaryFunc.Sum, "GHSecVendedor", SummaryRunning.Group, SummaryType.SubTotal);
                    LibReport.ConfigSummaryField(this, "txtTotalComisionableEnMonedaLocal", "MontoComisionableEnMonedaLocal", SummaryFunc.Sum, "GHSecVendedor", SummaryRunning.Group, SummaryType.SubTotal); LibReport.ConfigSummaryField(this, "txtTotalGeneralComisionable", "MontoComisionableEnMonedaLocal", SummaryFunc.Sum, "GHSecMonedaCobranza", SummaryRunning.Group, SummaryType.SubTotal);
                    LibReport.ChangeControlVisibility(this, "txtTipoDeCambioAMonedaLocal", true);
                    if (vEsTasaDeCambioOriginalAMonedaLocal) {
                        vMensajeDeCambio = vUtilRpt.MensajesDeMonedaParaInformes(Saw.Lib.eTasaDeCambioParaImpresion.Original);
                    } else {
                        vMensajeDeCambio = vUtilRpt.MensajesDeMonedaParaInformes(Saw.Lib.eTasaDeCambioParaImpresion.DelDia);
                    }
                    LibReport.ConfigFieldStr(this, "txtTipoDeCambioAMonedaLocal", vMensajeDeCambio, string.Empty);
                    #region Comision en Moneda Ext
                    if (vIncluirComisionEnMonedaExt){
                        string vMontoComisionMonedaExtConSimbolo = "Monto Comisión en " + SimboloMonedaExtranjera;
                        LibReport.ConfigLabel(this, "lblMontoComisionEnMonedaExt", vMontoComisionMonedaExtConSimbolo);
                        LibReport.ChangeControlVisibility(this, "lblMontoComisionEnMonedaExt", true);
                        LibReport.ChangeControlVisibility(this, "txtMontoComisionEnMonedaExt", true);
                        LibReport.ChangeControlVisibility(this, "txtTotalGeneralComisionEnMonedaExt", true);
                        LibReport.ConfigFieldDecWithNDecimal(this, "txtMontoComisionEnMonedaExt", string.Empty, string.Empty, 2);
                    } 
                    #endregion
                }
                if (UsaAsignacionDeComisionEnCobranza) {
                    #region Ocultar columnas innecesarias
                    LibReport.ChangeControlVisibility(this, "lblPorcentajeDeComision", false);
                    LibReport.ChangeControlVisibility(this, "txtPorcenta" +
                        "jeDeComision", false);
                    LibReport.ChangeControlVisibility(this, "lblNivelDeComision", false);
                    LibReport.ChangeControlVisibility(this, "txtNivelDeComision", false);
                    #endregion
                    if (vIncluirComisionEnMonedaExt) {
                        #region Reubicar la columna de comision en moneda extranjera
                        LibReport.ChangeControlLocation(this, "lblMontoComisionEnMonedaExt", (float)7.15, (float)0.23);
                        LibReport.ChangeControlLocation(this, "txtMontoComisionEnMonedaExt", (float)7.15, (float)0);
                        txtTotalGeneralComisionEnMonedaExt.Width = (float)1.2;
                        #endregion
                    }
                    LibReport.ConfigSummaryField(this, "txtMontoComision", "MontoComision", SummaryFunc.Sum, "GHSecVendedor", SummaryRunning.Group, SummaryType.SubTotal);
                } else {
                    LibReport.ConfigFieldDecWithNDecimal(this, "txtMontoComision", string.Empty, "MontoComision", 2);
                }
				LibReport.ConfigFieldDecWithNDecimal(this, "txtPorcentajeDeComision", string.Empty, "PorcentajeDeComision", 2);
				LibReport.ConfigFieldStr(this, "txtNivelDeComision", string.Empty, "NivelDeComision");
                LibReport.ConfigFieldStr(this, "txtNotaFooter", vNotaFooter, string.Empty);

                LibReport.ConfigSummaryField(this, "txtTotalCobranza", "MontoAbonado", SummaryFunc.Sum, "GHSecVendedor", SummaryRunning.Group, SummaryType.SubTotal);
                LibReport.ConfigSummaryField(this, "txtTotalGeneralCobranzas", "MontoAbonado", SummaryFunc.Sum, "GHSecMonedaCobranza", SummaryRunning.Group, SummaryType.SubTotal);

				LibReport.ConfigGroupHeader(this, "GHSecMonedaCobranza", "MonedaCobranza", GroupKeepTogether.All, RepeatStyle.OnPage, true, NewPage.None);
				LibReport.ConfigGroupHeader(this, "GHSecVendedor", "CodigoDelVendedor", GroupKeepTogether.All, RepeatStyle.OnPage, true, NewPage.None);
                LibReport.ChangeSectionPropertiesVisibleAndHeight(this, "Detail", false, 0);
                LibGraphPrnMargins.SetGeneralMargins(this, DataDynamics.ActiveReports.Document.PageOrientation.Landscape);
                return true;
            }
            return false;
        }
        #endregion //Metodos Generados
        #region Variables globales para totales generales
        private decimal vTotalGeneralComision;
        private decimal vTotalGeneralComisionEnMonedaExt;
        #endregion

        private void GHSecMonedaCobranza_Format(object sender, EventArgs e) {
            vTotalGeneralComision = 0;
            vTotalGeneralComisionEnMonedaExt = 0;
        }

        private void GFSecVendedor_Format(object sender, EventArgs e) {
            decimal vMontoComision = 0;
            decimal vMontoComisionEnMonedaExt = 0;
            if (UsaAsignacionDeComisionEnCobranza) {
                vMontoComision = LibConvert.ToDec(txtMontoComision.Text);
                vMontoComisionEnMonedaExt = vMontoComision / TasaDeCambioComisionMonedaExt;
            } else {
                IVendedorPdn insVendedor = new clsVendedorNav();
                decimal vMontoComisionableEnMonedaLocal = LibConvert.ToDec(txtTotalComisionableEnMonedaLocal.Text);
                decimal vMontoComisionable = LibConvert.ToDec(txtTotalComisionable.Text);
                string vNivelDeComision = string.Empty;
                decimal vPorcentajeComision = 0;
                insVendedor.CalculaMontoPorcentajeYNivelDeComisionInforme(txtCodigoVendedor.Text, vMontoComisionable, vMontoComisionableEnMonedaLocal, ref vMontoComision, ref vPorcentajeComision, ref vNivelDeComision);
                txtMontoComision.Text = LibConvert.ToStr(vMontoComision);
                vMontoComisionEnMonedaExt = vMontoComision / TasaDeCambioComisionMonedaExt;
                txtPorcentajeDeComision.Text = LibConvert.ToStr(vPorcentajeComision) + "%";
                txtNivelDeComision.Text = vNivelDeComision;
            }
            vTotalGeneralComision += vMontoComision;
            vTotalGeneralComisionEnMonedaExt += vMontoComisionEnMonedaExt;
            txtMontoComisionEnMonedaExt.Text = LibConvert.ToStr(vMontoComisionEnMonedaExt);
        }

        private void GFSecMonedaCobranza_Format(object sender, EventArgs e) {
            txtTotalGeneralComision.Text = LibConvert.ToStr(vTotalGeneralComision);
            txtTotalGeneralComisionEnMonedaExt.Text = LibConvert.ToStr(vTotalGeneralComisionEnMonedaExt);
        }

    } //End of class dsrComisionDeVendedoresPorCobranzaMontoResumido

} //End of namespace Galac.Adm.Rpt.Venta
