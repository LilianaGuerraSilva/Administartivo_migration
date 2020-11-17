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
namespace Galac.Adm.Rpt.GestionCompras {
    /// <summary>
    /// Summary description for NewActiveReport1.
    /// </summary>
    public partial class dsrImprimirCotizacionOrdenDeCompra : DataDynamics.ActiveReports.ActiveReport {
        #region Variables
        private bool _UseExternalRpx;
        private static string _RpxFileName;
        decimal vUtilidadML = 0;
        decimal vUtilidadME = 0;
        decimal vVentaCotizacionML = 0;
        decimal vVentaCotizacionME = 0;
        decimal vCostoOrdenDeCompraML = 0;
        decimal vCostoOrdenDeCompraME = 0;
        #endregion //Variables
        #region Constructores
        public dsrImprimirCotizacionOrdenDeCompra()
            : this(false, string.Empty) {
        }

        public dsrImprimirCotizacionOrdenDeCompra(bool initUseExternalRpx, string initRpxFileName) {
            InitializeComponent();
            _UseExternalRpx = initUseExternalRpx;
            if (_UseExternalRpx) {
                _RpxFileName = initRpxFileName;
            }
        }
        #endregion //Constructores
        #region Metodos Generados

        public string ReportTitle() {
            return "Ordenes de Compra de Cotización";
        }

        public bool ConfigReport(DataTable valDataSource, Dictionary<string, string> valParameters) {
            if (_UseExternalRpx) {
                string vRpxPath = LibWorkPaths.PathOfRpxFile(_RpxFileName, ReportTitle(), false, LibDefGen.ProgramInfo.ProgramInitials);//acá se indicaría si se busca en ULS, por defecto buscaría en app.path... Tip: Una función con otro nombre.
                if (!LibString.IsNullOrEmpty(vRpxPath, true)) {
                    LibReport.LoadLayout(this, vRpxPath);
                }
            }
            if (LibReport.ConfigDataSource(this, valDataSource)) {
                LibReport.ConfigFieldStr(this, "txtNombreCompania", valParameters["NombreCompania"], string.Empty);
                LibReport.ConfigLabel(this, "lblTituloInforme", ReportTitle());
                LibReport.ConfigLabel(this, "lblFechaInicialYFinal", "");
                LibReport.ConfigLabel(this, "lblFechaYHoraDeEmision", LibReport.PromptEmittedOnDateAtHour);
                LibReport.ConfigHeader(this, "txtNombreCompania", "lblFechaYHoraDeEmision", "lblTituloInforme", "txtNroDePagina", "lblFechaInicialYFinal", LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintPageNumber, LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintEmitDate);

                LibReport.ConfigFieldStr(this, "txtOrden", "", "Orden");
                LibReport.ConfigFieldStr(this, "txtNombreCliente", "", "NombreCliente");
                LibReport.ConfigFieldStr(this, "txtCambio","","TasaDeCambio");
                LibReport.ConfigFieldDec(this, "txtPorcentaje", "", "PorcentajeVenta");
                LibReport.ConfigFieldStr(this, "txtNombreArticulo", "", "DescripcionArticulo");
                LibReport.ConfigFieldDec(this, "txtSoles", "", "MontoArticuloEnML","0.00", false, TextAlignment.Right);
                LibReport.ConfigFieldDec(this, "txtDolares", "", "MontoArticuloEnME", "0.00", false, TextAlignment.Right);
                LibReport.ConfigFieldStr(this, "txtNumeroOC", "", "NumeroDocumento");
                LibReport.ConfigFieldStr(this, "txtNumeroCot", "", "NumeroCotizacionParaAgrupamiento");

                LibReport.ConfigGroupHeader(this, "GHDetail", "NumeroCotizacionParaAgrupamiento", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.None);
                LibReport.ConfigGroupHeader(this, "GHOrdenDeCompra", "Orden", GroupKeepTogether.All, RepeatStyle.All, true, NewPage.None);
                LibReport.ConfigSummaryField(this, "txtTotalML", "MontoArticuloEnML", SummaryFunc.Sum, "GHOrdenDeCompra", SummaryRunning.Group , SummaryType.SubTotal );
                LibReport.ConfigSummaryField(this, "txtTotalME", "MontoArticuloEnME", SummaryFunc.Sum, "GHOrdenDeCompra", SummaryRunning.Group, SummaryType.SubTotal);
                LibReport.AddNoDataEvent(this);
                LibGraphPrnMargins.SetGeneralMargins(this, DataDynamics.ActiveReports.Document.PageOrientation.Portrait);
                return true;
            }
            return false;
        }


        #endregion //Metodos Generados

        private void GFDetail_Format(object sender, EventArgs e) {
            decimal utilidadBrutaSoles = vVentaCotizacionML - vCostoOrdenDeCompraML;
            decimal utilidadBrutaDolares = vVentaCotizacionME - vCostoOrdenDeCompraME;
            decimal porcentaje = (utilidadBrutaSoles / vVentaCotizacionML) * 100;

            LibReport.ConfigFieldDec(this, "txtUtilidadSoles", LibConvert.ToStr(utilidadBrutaSoles), "");
            LibReport.ConfigFieldDec(this, "txtUtilidadDolares", LibConvert.ToStr(utilidadBrutaDolares), "");

            LibReport.ConfigFieldStr(this, "txtPorcentajeUtilidad", LibConvert.ToStr(porcentaje,2) + " %", "");
            vVentaCotizacionML = 0;
            vCostoOrdenDeCompraML = 0;
            vVentaCotizacionME = 0;
            vCostoOrdenDeCompraME = 0;
        }

        private void Detail_BeforePrint(object sender, EventArgs e) {
            if (txtOrden.Text == "0") {
                vVentaCotizacionML += LibConvert.ToDec(txtSoles.Text);
                vVentaCotizacionME += LibConvert.ToDec(txtDolares.Text);
            } else {
                vCostoOrdenDeCompraML  += LibConvert.ToDec(txtSoles.Text);
                vCostoOrdenDeCompraME += LibConvert.ToDec(txtDolares.Text);
            }
        }

        private void Detail_AfterPrint(object sender, EventArgs e) {
        }

        private void GFOrdenDeCompra_Format(object sender, EventArgs e) {
            //costoTotalSoles = solesOCDetail;
            //costoTotalDolares = dolaresOCDetail;

            //LibReport.ConfigFieldDec(this, "txtCostoTotalSoles", LibConvert.ToStr(costoTotalSoles), "");
            //LibReport.ConfigFieldDec(this, "txtCostoTotalDolares", LibConvert.ToStr(costoTotalDolares), "");

            //LibReport.ConfigFieldDec(this, "txtVentaTotalSoles", LibConvert.ToStr(solesCotDetail), "");
            //LibReport.ConfigFieldDec(this, "txtVentaTotalDolares", LibConvert.ToStr(dolaresCotDetail), "");
            
        }

        private void GFOrdenDeCompra_BeforePrint(object sender, EventArgs e) {
            
        }

        private void Detail_Format(object sender, EventArgs e) {
            if (txtOrden.Text == "0") {
                lblTotales.Text =  "VENTA TOTAL";
                txtNumeroOC.Text = "";
                txtPorcentaje.Text  = "";
            } else {
                lblTotales.Text = "COSTO TOTAL";
            }
        }

        private void GHOrdenDeCompra_Format(object sender, EventArgs e) {

        }

        private void GHOrdenDeCompra_BeforePrint(object sender, EventArgs e) {
            if (txtOrden.Text == "0") {
                lblOC.Text = "";
                lblVenta.Text = "";
            } else {
                lblOC.Text = "Nro. O/C";
                lblVenta.Text = "% de Venta";
            }
        }
    } //End of class dsrImprimirCostoDeCompraEntreFechas

} //End of namespace Galac.Dbo.Rpt.ComponenteNoEspecificado

