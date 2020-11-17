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
    /// Summary description for dsrComisionDeVendedoresPorCobranzaPorMonto.
    /// </summary>
    public partial class dsrComisionDeVendedoresPorCobranzaMonto : DataDynamics.ActiveReports.ActiveReport {
        #region Variables
        private bool _UseExternalRpx;
        private static string _RpxFileName;
        #endregion //Variables
        #region Propiedades
        private Galac.Comun.Ccl.TablasGen.IMonedaLocalActual vMonedaLocal = new Galac.Comun.Brl.TablasGen.clsMonedaLocalActual();
        public decimal TasaDeCambioComisionMonedaExt { get; set; }
        public string SimboloMonedaExtranjera { get; set; }
        private bool UsaAsignacionDeComisionEnCobranza { get; set; }
        private bool EsEnMonedaOriginal { get; set; }
        #endregion
        #region Constructores
        public dsrComisionDeVendedoresPorCobranzaMonto()
            : this(false, string.Empty) {
            vMonedaLocal.CargarTodasEnMemoriaYAsignarValoresDeLaActual(LibDefGen.ProgramInfo.Country, LibDate.Today());
        }
		
        public dsrComisionDeVendedoresPorCobranzaMonto(bool initUseExternalRpx, string initRpxFileName) {
            InitializeComponent();
            _UseExternalRpx = initUseExternalRpx;
            if (_UseExternalRpx) {
                _RpxFileName = initRpxFileName;
            }
        }
        #endregion //Constructores
        #region Metodos Generados
		 public string ReportTitle() {
            return "Comisión de Vendedores por Cobranzas (Detallado)";
        }

        public bool ConfigReport(DataTable valDataSource, Dictionary<string, string> valParameters) {
            Saw.Lib.clsUtilRpt vUtilRpt = new Saw.Lib.clsUtilRpt();
            string vMensajeDeCambio = string.Empty;
            string vNotaFooter = "El cálculo de las comisiones se hace en base a los porcentajes de cobranza por vendedores (Niveles de comisión)";
            EsEnMonedaOriginal = LibConvert.SNToBool(valParameters["EnMonedaOriginal"]);
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
                if (EsEnMonedaOriginal) {
                    #region Ocultar mensaje de tipo de cambio
                    LibReport.ChangeSectionPropertiesVisibleAndHeight(this, "GHSecVendedor", true, (float)0.18);
                    LibReport.ChangeControlLocation(this, "lblNombreVendedor", 0, 0);
                    LibReport.ChangeControlLocation(this, "txtCodigoVendedor", (float)1.71, (float)0);
                    LibReport.ChangeControlLocation(this, "txtNombreVendedor", (float)2.21, (float)0);
                    #endregion
				    LibReport.ConfigFieldDecWithNDecimal(this, "txtMontoTotalCobranza", string.Empty, "MontoAbonado", 2);
				    LibReport.ConfigFieldDecWithNDecimal(this, "txtMontoComisionable", string.Empty, "MontoComisionable", 2);
                    LibReport.ConfigSummaryField(this, "txtTotalComisionable", "MontoComisionable", SummaryFunc.Sum, "GHSecMonedaCobranza", SummaryRunning.Group, SummaryType.SubTotal);
                    LibReport.ConfigSummaryField(this, "txtTotalComisionableEnMonedaLocal", "MontoComisionableEnMonedaLocal", SummaryFunc.Sum, "GHSecMonedaCobranza", SummaryRunning.Group, SummaryType.SubTotal);
                    if(UsaAsignacionDeComisionEnCobranza) {
                        #region Ocultar Totales por Vendedor
                        LibReport.ChangeSectionPropertiesVisibleAndHeight(this, "GFSecVendedor", false, 0);
                        #endregion
                    } else {
                        #region Mostrar Totales Por Moneda
                        LibReport.ChangeSectionPropertiesVisibleAndHeight(this, "GFSecVendedor", false, 0);
                        LibReport.ChangeControlVisibility(this, "lblResumenComisionVendedorPorMonCobranza", true);
                        LibReport.ChangeControlVisibility(this, "lblMontoComisionPorMonedaCobranza", true);
                        LibReport.ChangeControlVisibility(this, "txtMontoComisionPorMonedaCobranza", true);
                        LibReport.ChangeControlVisibility(this, "lblPorcentajeDeComisionPorMonedaCobranza", true);
                        LibReport.ChangeControlVisibility(this, "txtPorcentajeDeComisionPorMonedaCobranza", true);
                        LibReport.ChangeControlVisibility(this, "lblNivelDeComisionPorMonedaCobranza", true);
                        LibReport.ChangeControlVisibility(this, "txtNivelDeComisionPorMonedaCobranza", true);
                        LibReport.ChangeControlVisibility(this, "txtTotalComisionableEnMonedaLocal", false);
                        LibReport.ChangeSectionPropertiesVisibleAndHeight(this, "GFSecMonedaCobranza", true, (float)0.95);
                        #endregion
                    }
                } else {
                    LibReport.ConfigFieldDecWithNDecimal(this, "txtMontoTotalCobranza", string.Empty, "MontoAbonado", 2);
				    LibReport.ConfigFieldDecWithNDecimal(this, "txtMontoComisionable", string.Empty, "MontoComisionableEnMonedaLocal", 2);
				    LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalGeneralComisionable", string.Empty, "TotalGeneralComisionable", 2);
				    LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalGeneralComision", string.Empty, "TotalGeneralComision", 2);
                    LibReport.ConfigSummaryField(this, "txtTotalComisionable", "MontoComisionableEnMonedaLocal", SummaryFunc.Sum, "GHSecMonedaCobranza", SummaryRunning.Group, SummaryType.SubTotal);
                    LibReport.ChangeControlVisibility(this, "txtTipoDeCambioAMonedaLocal", true);
                    if (vEsTasaDeCambioOriginalAMonedaLocal) {
                        vMensajeDeCambio = vUtilRpt.MensajesDeMonedaParaInformes(Saw.Lib.eTasaDeCambioParaImpresion.Original);
                    } else {
                        vMensajeDeCambio = vUtilRpt.MensajesDeMonedaParaInformes(Saw.Lib.eTasaDeCambioParaImpresion.DelDia);
                    }
                    LibReport.ConfigFieldStr(this, "txtTipoDeCambioAMonedaLocal", vMensajeDeCambio, string.Empty);
                    #region Comision en Moneda Ext
                    if (vIncluirComisionEnMonedaExt){
                        string vMontoComisionMonedaExtConSimbolo = "Monto Comisión en " + SimboloMonedaExtranjera + " - (" + SimboloMonedaExtranjera + " 1 = " + LibConvert.ToStr(TasaDeCambioComisionMonedaExt, 2) + " " +vMonedaLocal.GetHoySimboloMoneda() +")";
                        LibReport.ConfigLabel(this, "lblMontoComisionEnMonedaExt", vMontoComisionMonedaExtConSimbolo);
                        LibReport.ChangeControlVisibility(this, "lblMontoComisionEnMonedaExt", true);
                        LibReport.ChangeControlVisibility(this, "txtMontoComisionEnMonedaExt", true);
                        LibReport.ConfigFieldDecWithNDecimal(this, "txtMontoComisionEnMonedaExt", string.Empty, string.Empty, 2);
                    } else {
                        #region Reajuste de la presentacion
                        LibReport.ChangeControlLocation(this, "lblPorcentajeDeComision", (float)3.85, (float)0.37);
                        LibReport.ChangeControlLocation(this, "txtPorcentajeDeComision", (float)6.26, (float)0.37);
                        LibReport.ChangeControlLocation(this, "lblNivelDeComision", (float)3.85, (float)0.53);
                        LibReport.ChangeControlLocation(this, "txtNivelDeComision", (float)6.25, (float)0.53);
                        LibReport.ChangeControlLocation(this, "lblTotalesGenerales", (float)3.85, (float)0.69);
                        LibReport.ChangeControlLocation(this, "lblTotalGeneralComisionable", (float)3.85, (float)0.85);
                        LibReport.ChangeControlLocation(this, "txtTotalGeneralComisionable", (float)6.25, (float)0.85);
                        LibReport.ChangeControlLocation(this, "lblTotalGeneralComision", (float)3.85, (float)1.01);
                        LibReport.ChangeControlLocation(this, "txtTotalGeneralComision", (float)6.25, (float)1.01);
                        #endregion
                    }
                    #endregion
                }
                if (UsaAsignacionDeComisionEnCobranza) {
                    #region Mostrar las columnas y campos
                    LibReport.ChangeControlVisibility(this, "lblPorcentajeDeComisionEnCobranza", true);
                    LibReport.ChangeControlVisibility(this, "txtPorcentajeDeComisionEnCobranza", true);
                    LibReport.ChangeControlVisibility(this, "lblMontoComisionPorCobranza", true);
                    LibReport.ChangeControlVisibility(this, "txtMontoComisionPorCobranza", true);
                    LibReport.ChangeControlVisibility(this, "txtTotalMontoComisionPorCobranza", true);
                    #endregion
                    #region Ocultar las columnas innecesarias si el informe es en moneda local
                    LibReport.ChangeControlVisibility(this, "lblResumenComisionVendedor", false);
                    LibReport.ChangeControlVisibility(this, "lblPorcentajeDeComision", false);
                    LibReport.ChangeControlVisibility(this, "txtPorcentajeDeComision", false);
                    LibReport.ChangeControlVisibility(this, "lblMontoComision", false);
                    LibReport.ChangeControlVisibility(this, "txtMontoComision", false);
                    LibReport.ChangeControlVisibility(this, "lblMontoComisionEnMonedaExt", false);
                    LibReport.ChangeControlVisibility(this, "txtMontoComisionEnMonedaExt", false);
                    LibReport.ChangeControlVisibility(this, "lblNivelDeComision", false);
                    LibReport.ChangeControlVisibility(this, "txtNivelDeComision", false);
                    #endregion
                    if (vIncluirComisionEnMonedaExt) {
                        #region Ajustar la ubicación de los totales
                        LibReport.ChangeControlLocation(this, "lblTotalesGenerales", (float)6.26, (float)0);
                        lblTotalesGenerales.Width = (float)3.66;
                        LibReport.ChangeControlLocation(this, "lblTotalGeneralComisionable", (float)6.26, (float)0.18);
                        LibReport.ChangeControlLocation(this, "txtTotalGeneralComisionable", (float)9.07, (float)0.18);
                        lblTotalGeneralComisionable.Width = (float)2.81;
                        txtTotalGeneralComisionable.Width = (float)0.85;
                        LibReport.ChangeControlLocation(this, "lblTotalGeneralComision", (float)6.26, (float)0.34);
                        LibReport.ChangeControlLocation(this, "txtTotalGeneralComision", (float)9.07, (float)0.34);
                        lblTotalGeneralComision.Width = (float)2.81;
                        txtTotalGeneralComision.Width = (float)0.85;
                        string vMontoComisionMonedaExtConSimbolo = "Monto Comisión en " + SimboloMonedaExtranjera + " - (" + SimboloMonedaExtranjera + " 1 = " + LibConvert.ToStr(TasaDeCambioComisionMonedaExt, 2) + " " + vMonedaLocal.GetHoySimboloMoneda() + ")";
                        LibReport.ConfigLabel(this, "lblTotalGeneralComisionEnMonedaExt", vMontoComisionMonedaExtConSimbolo);
                        LibReport.ChangeControlVisibility(this, "lblTotalGeneralComisionEnMonedaExt", true);
                        LibReport.ChangeControlLocation(this, "lblTotalGeneralComisionEnMonedaExt", (float)6.26, (float)0.5);
                        lblTotalGeneralComisionEnMonedaExt.Width = (float)2.81;
                        LibReport.ChangeControlVisibility(this, "txtTotalGeneralComisionEnMonedaExt", true);
                        LibReport.ChangeControlLocation(this, "txtTotalGeneralComisionEnMonedaExt", (float)9.07, (float)0.5);
                        txtTotalGeneralComisionEnMonedaExt.Width = (float)0.85;
                        #endregion
                    }
                    else {
                        #region Ajustar la ubicación de los totales
                        LibReport.ChangeControlLocation(this, "lblTotalesGenerales", (float)4.96, (float)0);
                        lblTotalesGenerales.Width = (float)4.11;
                        LibReport.ChangeControlLocation(this, "lblTotalGeneralComisionable", (float)4.96, (float)0.18);
                        LibReport.ChangeControlLocation(this, "txtTotalGeneralComisionable", (float)8.14, (float)0.18);
                        lblTotalGeneralComisionable.Width = (float)3.18;
                        txtTotalGeneralComisionable.Width = (float)0.93;
                        LibReport.ChangeControlLocation(this, "lblTotalGeneralComision", (float)4.96, (float)0.34);
                        LibReport.ChangeControlLocation(this, "txtTotalGeneralComision", (float)8.14, (float)0.34);
                        lblTotalGeneralComision.Width = (float)3.18;
                        txtTotalGeneralComision.Width = (float)0.93;
                        #endregion
                    }
                    if (vIncluirComisionEnMonedaExt) {
                        LibReport.ChangeControlVisibility(this, "lblMontoComisionPorCobranzaEnMonedaExt", true);
                        LibReport.ChangeControlVisibility(this, "txtMontoComisionPorCobranzaEnMonedaExt", true);
                        LibReport.ChangeControlVisibility(this, "txtTotalMontoComisionPorCobranzaEnMonedaExt", true);
                        LibReport.ConfigLabel(this, "lblMontoComisionPorCobranzaEnMonedaExt", "Comisión en " + SimboloMonedaExtranjera);
                        LibReport.ConfigFieldDecWithNDecimal(this, "txtTotalMontoComisionPorCobranzaEnMonedaExt", string.Empty, string.Empty, 2);
                    }
                    LibReport.ConfigFieldDecWithNDecimal(this, "txtPorcentajeDeComisionEnCobranza", string.Empty, "PorcentajeDeComision", 2);
                    LibReport.ConfigFieldDecWithNDecimal(this, "txtMontoComisionPorCobranza", string.Empty, "MontoComision", 2);
                    LibReport.ConfigSummaryField(this, "txtTotalMontoComisionPorCobranza", "MontoComision", SummaryFunc.Sum, "GHSecMonedaCobranza", SummaryRunning.Group, SummaryType.SubTotal);
                    vNotaFooter = "El cálculo de la comisión de la cobranza, se hace en base al porcentaje asignado en la misma";
                } else {
				    LibReport.ConfigFieldDecWithNDecimal(this, "txtPorcentajeDeComision", string.Empty, "PorcentajeDeComision", 2);
                }
                LibReport.ConfigFieldDecWithNDecimal(this, "txtMontoComision", string.Empty, "MontoComision", 2);
				LibReport.ConfigFieldStr(this, "txtNivelDeComision", string.Empty, "NivelDeComision");
                LibReport.ConfigFieldStr(this, "txtNotaFooter", vNotaFooter, string.Empty);

                LibReport.ConfigSummaryField(this, "txtTotalCobranza", "MontoAbonado", SummaryFunc.Sum, "GHSecMonedaCobranza", SummaryRunning.Group, SummaryType.SubTotal);
                LibReport.ConfigSummaryField(this, "txtTotalGeneralComisionable", "MontoComisionableEnMonedaLocal", SummaryFunc.Sum, "GHSecVendedor", SummaryRunning.Group, SummaryType.SubTotal);

				LibReport.ConfigGroupHeader(this, "GHSecVendedor", "CodigoDelVendedor", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.After);
				LibReport.ConfigGroupHeader(this, "GHSecMonedaCobranza", "MonedaCobranza", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.None);

                if(UsaAsignacionDeComisionEnCobranza) {
                    LibGraphPrnMargins.SetGeneralMargins(this, DataDynamics.ActiveReports.Document.PageOrientation.Landscape);
                } else {
                    LibGraphPrnMargins.SetGeneralMargins(this, DataDynamics.ActiveReports.Document.PageOrientation.Portrait);
                }
                return true;
            }
            return false;
        }
        #endregion //Metodos Generados
        #region Variables globales para totalizar
        private decimal vTotalMontoComisionEnMonedaExt;
        private decimal vTotalGeneralComisionEnMonedaLocal;
        private decimal vTotalGeneralComisionEnMonedaExt;
        #endregion

        private void GFSecMonedaCobranza_Format(object sender, EventArgs e) {
            IVendedorPdn insVendedor = new clsVendedorNav();
            decimal vMontoComisionableEnMonedaLocal = LibConvert.ToDec(txtTotalComisionableEnMonedaLocal.Text);
            decimal vMontoComisionable = LibConvert.ToDec(txtTotalComisionable.Text);
            string vNivelDeComision = string.Empty;
            decimal vPorcentajeComision = 0;
            decimal vMontoComision = 0;
            insVendedor.CalculaMontoPorcentajeYNivelDeComisionInforme(txtCodigoVendedor.Text, vMontoComisionable ,vMontoComisionableEnMonedaLocal, ref vMontoComision, ref vPorcentajeComision, ref vNivelDeComision);
            txtMontoComisionPorMonedaCobranza.Text = LibConvert.ToStr(vMontoComision);
            txtPorcentajeDeComisionPorMonedaCobranza.Text = LibConvert.ToStr(vPorcentajeComision) + "%";
            txtNivelDeComisionPorMonedaCobranza.Text = vNivelDeComision;
            if(UsaAsignacionDeComisionEnCobranza) {
                txtTotalMontoComisionPorCobranzaEnMonedaExt.Text = LibConvert.ToStr(vTotalMontoComisionEnMonedaExt);
                vTotalMontoComisionEnMonedaExt = 0;
            }
        }

        private void GFSecVendedor_Format(object sender, EventArgs e) {
            IVendedorPdn insVendedor = new clsVendedorNav();
            decimal vMontoComisionableEnMonedaLocal = LibConvert.ToDec(txtTotalGeneralComisionable.Text);
            string vNivelDeComision = string.Empty;
            decimal vPorcentajeComision = 0;
            decimal vMontoComision = 0;
            decimal vMontoComisionEnMonedaExt = 0;
            insVendedor.CalculaMontoPorcentajeYNivelDeComisionInforme(txtCodigoVendedor.Text, vMontoComisionableEnMonedaLocal, vMontoComisionableEnMonedaLocal, ref vMontoComision, ref vPorcentajeComision, ref vNivelDeComision);
            txtMontoComision.Text = LibConvert.ToStr(vMontoComision);
            vMontoComisionEnMonedaExt = vMontoComision / TasaDeCambioComisionMonedaExt;
            txtMontoComisionEnMonedaExt.Text = LibConvert.ToStr(vMontoComisionEnMonedaExt);
            txtPorcentajeDeComision.Text = LibConvert.ToStr(vPorcentajeComision) + "%";
            txtNivelDeComision.Text = vNivelDeComision;
            txtTotalGeneralComision.Text = LibConvert.ToStr(vMontoComision);
            if(!EsEnMonedaOriginal && UsaAsignacionDeComisionEnCobranza) {
                txtTotalGeneralComision.Text = LibConvert.ToStr(vTotalGeneralComisionEnMonedaLocal);
                txtTotalGeneralComisionEnMonedaExt.Text = LibConvert.ToStr(vTotalGeneralComisionEnMonedaExt);
                vTotalGeneralComisionEnMonedaLocal = 0;
                vTotalGeneralComisionEnMonedaExt = 0;
            }
        }

        private void Detail_Format(object sender, EventArgs e) {
            if(UsaAsignacionDeComisionEnCobranza) {
                decimal vMontoComision = LibConvert.ToDec(txtMontoComisionPorCobranza.Text);
                decimal vMontoComisionEnMonedaExt = 0;
                if (vMontoComision > 0) {
                    vMontoComisionEnMonedaExt = vMontoComision / TasaDeCambioComisionMonedaExt;
                }
                txtMontoComisionPorCobranzaEnMonedaExt.Text = LibConvert.ToStr(vMontoComisionEnMonedaExt);
                txtPorcentajeDeComisionEnCobranza.Text += "%";
                vTotalMontoComisionEnMonedaExt += vMontoComisionEnMonedaExt;
                vTotalGeneralComisionEnMonedaLocal += vMontoComision;
                vTotalGeneralComisionEnMonedaExt += vMontoComisionEnMonedaExt;
            }
        }
    } //End of class dsrComisionDeVendedoresPorCobranzaMonto
} //End of namespace Galac.Adm.Rpt.Venta
