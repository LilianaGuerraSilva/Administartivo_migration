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
using System.Xml.Linq;
using LibGalac.Aos.Brl.PrnStt;
using LibGalac.Aos.Ccl.PrnStt;
using LibGalac.Aos.Base.Report;
using Galac.Adm.Ccl.Venta;
using Galac.Saw.Ccl.SttDef;
using static System.Net.Mime.MediaTypeNames;

namespace Galac.Adm.Rpt.Venta {
    /// <summary>
    /// Summary description for dsrFacturaBorradorGenrico.
    /// </summary>
    public partial class dsrFacturaBorradorGenerico : DataDynamics.ActiveReports.ActiveReport {
        #region Variables
        private bool _UseExternalRpx;
        private static string _RpxFileName;
        private static eTipoDocumentoFactura _TipoDocumento;
        private string _CodigoMonedaDocumento;
        private string _CodigoMonedaLocal;        
        #endregion //Variables
        #region Constructores
        public dsrFacturaBorradorGenerico()
            : this(false, string.Empty) {

        }

        public dsrFacturaBorradorGenerico(bool initUseExternalRpx, string initRpxFileName) {
            InitializeComponent();
            _UseExternalRpx = initUseExternalRpx;
            if (_UseExternalRpx) {
                _RpxFileName = initRpxFileName;
            }
        }
        #endregion //Constructores
        #region Metodos Generados

        public string ReportTitle() {
            return "Borrador NO FISCAL de Documento";
        }

        public bool ConfigReport(DataTable valDataSource, string valNombreCompania, int valConsecutivoCompania, string valNumeroDocumento, eTipoDocumentoFactura valTipoDocumento, eStatusFactura valStatusDocumento, bool valImprimirFacturaConSubtotalesPorLineaDeProducto, bool valDetalleProdCompFactura) {
            _TipoDocumento = valTipoDocumento;
            if (_UseExternalRpx) {
                string vRpxPath = LibWorkPaths.PathOfRpxFile(_RpxFileName, ReportTitle(), false, LibDefGen.ProgramInfo.ProgramInitials);//acá se indicaría si se busca en ULS, por defecto buscaría en app.path... Tip: Una función con otro nombre.
                if (!LibString.IsNullOrEmpty(vRpxPath, true)) {
                    LibReport.LoadLayout(this, vRpxPath);
                }
            }
            if (LibReport.ConfigDataSource(this, valDataSource)) {
                LibReport.ConfigFieldStr(this, "txtNombreCompania", valNombreCompania, string.Empty);
                LibReport.ConfigLabel(this, "lblTituloInforme", ReportTitle());
                LibReport.ConfigLabel(this, "lblFechaYHoraDeEmision", LibReport.PromptEmittedOnDateAtHour);
                LibReport.ConfigHeader(this, "txtNombreCompania", "lblFechaYHoraDeEmision", "lblTituloInforme", "txtNroDePagina", "lblFechaInicialYFinal", LibGraphPrnSettings.PrintPageNumber, false);

                #region Datos del Documento
                if (valStatusDocumento == eStatusFactura.Emitida) {//TODO:para después
                    LibReport.ConfigFieldStr(this, "txtNumeroFactura", string.Empty, "Numero");
                    LibReport.ConfigFieldDate(this, "txtFecha", string.Empty, "Fecha", eDateOutputFormat.DateLong);
                    LibReport.ConfigLabel(this, "lblNumeroFactura", "Número");
                    LibReport.ConfigFieldDate(this, "txtFechaDeVencimiento", string.Empty, "FechaDeVencimiento", eDateOutputFormat.DateLong);
                    LibReport.ConfigFieldStr(this, "txtTipoDocumento", string.Empty, "TipoDocumentoStr");
                } else {
                    LibReport.ConfigFieldStr(this, "txtNumeroFactura", "* * * * * * * * * *", string.Empty);
                    LibReport.ConfigFieldStr(this, "txtFecha", "dd/MM/aaaa", string.Empty);
                    LibReport.ConfigFieldStr(this, "txtFechaDeVencimiento", "dd/MM/aaaa", string.Empty);
                    LibReport.ConfigFieldStr(this, "txtTipoDocumento", "* * * * * * * * * *", "");
                }
                LibReport.ConfigFieldStr(this, "txtCiudadCia", string.Empty, "CiudadCia");
                LibReport.ConfigFieldStr(this, "txtCodigoMonedaFact", string.Empty, "CodigoMonedaFact");
                LibReport.ConfigFieldStr(this, "txtMonedaDoc", string.Empty, "MonedaDocumento");
                LibReport.ConfigFieldStr(this, "txtCodigoMonedaLocal", string.Empty, "CodigoMonedaLocal");
                LibReport.ConfigFieldStr(this, "txtCodigoMonedaExtranjera", string.Empty, "CodigoMonedaExtranjera");
                LibReport.ConfigFieldStr(this, "txtNroFacturaAfectada", string.Empty, "NumeroFacturaAfectada");
                LibReport.ConfigFieldStr(this, "txtCondicionesDePago", string.Empty, "CondicionesDePago");
                LibReport.ConfigFieldStr(this, "txtFormaDePago", string.Empty, "FormaDePagoStr");
                LibReport.ConfigFieldStr(this, "txtNroPlanillaExportacion", string.Empty, "NumeroPlanillaExportacion");

                LibReport.ConfigFieldDec(this, "txtCambioABolivares", "", "CambioABolivares");
                LibReport.ConfigFieldDec(this, "txtCambioCxC", "", "CambioCxC");
                LibReport.ConfigFieldDec(this, "txtCambioTotalEnDivisas", "", "CambioTotalEnDivisas");
                LibReport.ConfigFieldDec(this, "txtCambioFechaDocumento", "", "CambioFechaDocumento");
                #endregion Datos del Documento

                #region Datos del Cliente
                LibReport.ConfigFieldStr(this, "txtCodigoCliente", string.Empty, "CodigoCliente");
                LibReport.ConfigFieldStr(this, "txtNombreCliente", string.Empty, "NombreCliente");
                LibReport.ConfigFieldStr(this, "txtRIFCliente", string.Empty, "RifCliente");
                LibReport.ConfigFieldStr(this, "txtEmailCliente", string.Empty, "EmailCliente");
                LibReport.ConfigFieldStr(this, "txtDireccionCliente", string.Empty, "DireccionCliente");
                LibReport.ConfigFieldStr(this, "txtCiudadCliente", string.Empty, "CiudadCliente");
                LibReport.ConfigFieldStr(this, "txtTelefonoCliente", string.Empty, "TelefonoCliente");
                LibReport.ConfigFieldStr(this, "txtFaxCliente", string.Empty, "FaxCliente");
                LibReport.ConfigFieldStr(this, "txtContacto", string.Empty, "Contacto");
                LibReport.ConfigFieldStr(this, "txtLblCampoDef1Cliente", string.Empty, "NombreCampoDef1Cliente");
                LibReport.ConfigFieldStr(this, "txtCampoDef1Cliente", string.Empty, "CampoDef1Cliente");
                #endregion Datos del Cliente

                #region Datos de Despacho
                LibReport.ConfigFieldStr(this, "txtDireccionDeDespacho", string.Empty, "DireccionDeDespacho");
                LibReport.ConfigFieldStr(this, "txtCiudadDeDespacho", string.Empty, "CiudadDeDespacho");
                LibReport.ConfigFieldStr(this, "txtZonaPostalDeDespacho", string.Empty, "ZonaPostaDeDespacho");
                LibReport.ConfigFieldStr(this, "txtNombreOperador", string.Empty, "NombreOperador");
                LibReport.ConfigFieldStr(this, "txtContactoDeDespacho", string.Empty, "ContactoDeDespacho");
                LibReport.ConfigFieldStr(this, "txtCodigoVendedor", string.Empty, "CodigoVendedor");
                LibReport.ConfigFieldStr(this, "txtNombreVendedor", string.Empty, "NombreVendedor");
                #endregion Datos de Despacho

                #region Datos Adicionales
                LibReport.ConfigFieldStr(this, "txtLblCampoDefinible1", string.Empty, "NombreCampoDef1");
                LibReport.ConfigFieldStr(this, "txtLblCampoDefinible2", string.Empty, "NombreCampoDef2");
                LibReport.ConfigFieldStr(this, "txtLblCampoDefinible3", string.Empty, "NombreCampoDef3");
                LibReport.ConfigFieldStr(this, "txtLblCampoDefinible4", string.Empty, "NombreCampoDef4");
                LibReport.ConfigFieldStr(this, "txtLblCampoDefinible5", string.Empty, "NombreCampoDef5");
                LibReport.ConfigFieldStr(this, "txtLblCampoDefinible6", string.Empty, "NombreCampoDef6");
                LibReport.ConfigFieldStr(this, "txtLblCampoDefinible7", string.Empty, "NombreCampoDef7");
                LibReport.ConfigFieldStr(this, "txtLblCampoDefinible8", string.Empty, "NombreCampoDef8");
                LibReport.ConfigFieldStr(this, "txtLblCampoDefinible9", string.Empty, "NombreCampoDef9");
                LibReport.ConfigFieldStr(this, "txtLblCampoDefinible10", string.Empty, "NombreCampoDef10");
                LibReport.ConfigFieldStr(this, "txtLblCampoDefinible11", string.Empty, "NombreCampoDef11");
                LibReport.ConfigFieldStr(this, "txtLblCampoDefinible12", string.Empty, "NombreCampoDef12");

                LibReport.ConfigFieldStr(this, "txtCampoDefinible1", string.Empty, "CampoDefinible1");
                LibReport.ConfigFieldStr(this, "txtCampoDefinible2", string.Empty, "CampoDefinible2");
                LibReport.ConfigFieldStr(this, "txtCampoDefinible3", string.Empty, "CampoDefinible3");
                LibReport.ConfigFieldStr(this, "txtCampoDefinible4", string.Empty, "CampoDefinible4");
                LibReport.ConfigFieldStr(this, "txtCampoDefinible5", string.Empty, "CampoDefinible5");
                LibReport.ConfigFieldStr(this, "txtCampoDefinible6", string.Empty, "CampoDefinible6");
                LibReport.ConfigFieldStr(this, "txtCampoDefinible7", string.Empty, "CampoDefinible7");
                LibReport.ConfigFieldStr(this, "txtCampoDefinible8", string.Empty, "CampoDefinible8");
                LibReport.ConfigFieldStr(this, "txtCampoDefinible9", string.Empty, "CampoDefinible9");
                LibReport.ConfigFieldStr(this, "txtCampoDefinible10", string.Empty, "CampoDefinible10");
                LibReport.ConfigFieldStr(this, "txtCampoDefinible11", string.Empty, "CampoDefinible11");
                LibReport.ConfigFieldStr(this, "txtCampoDefinible12", string.Empty, "CampoDefinible12");

                LibReport.ConfigFieldStr(this, "txtPlaca", string.Empty, "");
                LibReport.ConfigFieldStr(this, "txtAno", string.Empty, "");
                LibReport.ConfigFieldStr(this, "txtModelo", string.Empty, "");
                LibReport.ConfigFieldStr(this, "txtNumeroPoliza", string.Empty, "");
                LibReport.ConfigFieldStr(this, "txtSerialVIN", string.Empty, "");
                LibReport.ConfigFieldStr(this, "txtColor", string.Empty, "");
                LibReport.ConfigFieldStr(this, "txtMarca", string.Empty, "");
                LibReport.ConfigFieldStr(this, "txtSerialMotor", string.Empty, "");

                LibReport.ConfigFieldStr(this, "txtLblCampoDefArtInv1", string.Empty, "NombreCampoDefArtInv1");
                LibReport.ConfigFieldStr(this, "txtLblCampoDefArtInv2", string.Empty, "NombreCampoDefArtInv2");
                LibReport.ConfigFieldStr(this, "txtLblCampoDefArtInv3", string.Empty, "NombreCampoDefArtInv3");
                LibReport.ConfigFieldStr(this, "txtLblCampoDefArtInv4", string.Empty, "NombreCampoDefArtInv4");
                LibReport.ConfigFieldStr(this, "txtLblCampoDefArtInv5", string.Empty, "NombreCampoDefArtInv5");
                #endregion Datos Adicionales

                #region GHDetail -> Renglón Factura
                LibReport.ConfigGroupHeader(this, "GHDetail", "ConsecutivoRenglon", GroupKeepTogether.None, RepeatStyle.None, true, NewPage.None);

                LibReport.ConfigFieldInt(this, "txtItem", string.Empty, "");
                LibReport.ConfigSummaryField(this, "txtItem", "", SummaryFunc.Count, "GHDetail", SummaryRunning.None, SummaryType.None);
                LibReport.ConfigFieldStr(this, "txtCodigoArticulo", string.Empty, "Articulo");
                LibReport.ConfigFieldStr(this, "txtDescripcion", string.Empty, "Descripcion");
                LibReport.ConfigFieldDec(this, "txtCantidad", string.Empty, "Cantidad");
                LibReport.ConfigFieldStr(this, "txtUnidadDeVenta", string.Empty, "UnidadDeVenta");
                LibReport.ConfigFieldDec(this, "txtUnidadDeVentaSecundaria", string.Empty, "UnidadDeVentaSecundaria");
                LibReport.ConfigFieldDec(this, "txtPrecioUnitario", string.Empty, "PrecioSinIVA");
                LibReport.ConfigFieldDec(this, "txtPorcentajeDescuento", string.Empty, "PorcentajeDescuento");
                LibReport.ConfigFieldDec(this, "txtAlicuotaIVARenglon", string.Empty, "PorcentajeAlicuota");
                LibReport.ConfigFieldDec(this, "txtTotalRenglon", string.Empty, "TotalRenglon");
                LibReport.ConfigFieldStr(this, "txtTipoArticulo", string.Empty, "TipoDeArticulo");
                LibReport.ConfigFieldStr(this, "txtTipoMercancia", string.Empty, "TipoMercancia");
                LibReport.ConfigFieldStr(this, "txtExento", string.Empty, "Exento");

                LibReport.ConfigFieldStr(this, "txtCampoDefArtInv1", string.Empty, "CampoDefArtInv1");
                LibReport.ConfigFieldStr(this, "txtCampoDefArtInv2", string.Empty, "CampoDefArtInv2");
                LibReport.ConfigFieldStr(this, "txtCampoDefArtInv3", string.Empty, "CampoDefArtInv3");
                LibReport.ConfigFieldStr(this, "txtCampoDefArtInv4", string.Empty, "CampoDefArtInv4");
                LibReport.ConfigFieldStr(this, "txtCampoDefArtInv5", string.Empty, "CampoDefArtInv5");

                LibReport.ConfigFieldStr(this, "txtSerial", string.Empty, "Serial");
                LibReport.ConfigFieldStr(this, "txtRollo", string.Empty, "Rollo");

                LibReport.ConfigFieldStr(this, "txtCampoExtraRenglon1", string.Empty, "CampoExtraEnRenglon1");
                LibReport.ConfigFieldStr(this, "txtCampoExtraRenglon2", string.Empty, "CampoExtraEnRenglon2");

                LibReport.ConfigFieldStr(this, "txtLoteDeInventario", string.Empty, "LoteDeInventario");
                LibReport.ConfigFieldDate(this, "txtFechaElab", string.Empty, "FechaElab", eDateOutputFormat.DateShort);
                LibReport.ConfigFieldDate(this, "txtFechaVenc", string.Empty, "FechaVenc", eDateOutputFormat.DateShort);

                #endregion GHDetail -> Renglón Factura

                #region Detail -> Detalle Producto Compuesto
                LibReport.ConfigFieldStr(this, "txtCodigoArticuloCompuesto", string.Empty, "CodigoArticuloCompuesto");
                LibReport.ConfigFieldStr(this, "txtDescripcionCompuesto", string.Empty, "DescripcionCompuesto");
                LibReport.ConfigFieldDec(this, "txtCantidadCompuesto", string.Empty, "CantidadCompuesto");
                LibReport.ConfigFieldStr(this, "txtUnidadDeVentaCompuesto", string.Empty, "UnidadDeVentaCompuesto");
                LibReport.ConfigFieldStr(this, "txtUnidadDeVentaSecundariaCompuesto", string.Empty, "UnidadDeVentaSecundariaCompuesto");
                LibReport.ConfigFieldDec(this, "txtPrecioUnitarioCompuesto", string.Empty, "PrecioUnitarioCompuesto");

                LibReport.ConfigFieldStr(this, "txtCampoDefArtInvCompuesto1", string.Empty, "CampoDefArtInvCompuesto1");
                LibReport.ConfigFieldStr(this, "txtCampoDefArtInvCompuesto2", string.Empty, "CampoDefArtInvCompuesto2");
                LibReport.ConfigFieldStr(this, "txtCampoDefArtInvCompuesto3", string.Empty, "CampoDefArtInvCompuesto3");
                LibReport.ConfigFieldStr(this, "txtCampoDefArtInvCompuesto4", string.Empty, "CampoDefArtInvCompuesto4");
                LibReport.ConfigFieldStr(this, "txtCampoDefArtInvCompuesto5", string.Empty, "CampoDefArtInvCompuesto5");
                #endregion Detail -> Detalle Producto Compuesto

                #region Subtotal por línea de producto
                if (valImprimirFacturaConSubtotalesPorLineaDeProducto) {
                    LibReport.ConfigGroupHeader(this, "GHLineaDeProducto", "LineaDeProducto", GroupKeepTogether.FirstDetail, RepeatStyle.None, true, NewPage.None);
                    LibReport.ConfigFieldStr(this, "txtLineaDeProducto", string.Empty, "LineaDeProducto");
                    LibReport.ConfigSummaryField(this, "txtSubtotalLineaDeProducto", "TotalRenglon", SummaryFunc.Sum, "GHDetail", SummaryRunning.Group, SummaryType.SubTotal);
                } else {
                    LibReport.ConfigFieldStr(this, "txtLineaDeProducto", string.Empty, "");
                    LibReport.ConfigFieldDec(this, "txtSubtotalLineaDeProducto", "", "");
                    LibReport.ChangeControlVisibility(this, "lblSubTotalLineaDeProducto", false);
                    LibReport.ChangeControlVisibility(this, "txtLineaDeProducto", false);
                    LibReport.ChangeControlVisibility(this, "txtSubtotalLineaDeProducto", false);
                    LibReport.ChangeSectionPropertiesVisibleAndHeight(this, "GHLineaDeProducto", false, 0);
                }
                #endregion Subtotal por línea de producto

                #region GFTotales
                LibReport.ConfigFieldInt(this, "txtTotalItems", string.Empty, "");
                LibReport.ConfigSummaryField(this, "txtTotalItems", "", SummaryFunc.Count, "GHDetail", SummaryRunning.All, SummaryType.GrandTotal);
                LibReport.ConfigSummaryField(this, "txtTotalCantidad", "Cantidad", SummaryFunc.Sum, "GHTotales", SummaryRunning.All, SummaryType.GrandTotal);
                if (valStatusDocumento == eStatusFactura.Emitida) {
                    LibReport.ConfigFieldStr(this, "txtMontoEnLetras", string.Empty, "");
                } else if (valStatusDocumento == eStatusFactura.Anulada) {
                    LibReport.ConfigFieldStr(this, "txtMontoEnLetras", string.Empty, "");
                } else {
                    LibReport.ConfigFieldStr(this, "txtMontoEnLetras", string.Empty, MontoEnLetras(valConsecutivoCompania, valNumeroDocumento, valTipoDocumento));
                }
                LibReport.ConfigFieldStr(this, "txtNotasFinales", string.Empty, "NotasFinales");
                #endregion GFTotales

                #region GFTitulos
                LibReport.ConfigFieldStr(this, "txtObservaciones", string.Empty, "Observaciones");
                #endregion GFTitulos

                #region GFDatosDelDocumento -> Totales
                LibReport.ConfigFieldStr(this, "txtMonedaExtranjera", string.Empty, "MonedaExtranjera");
                LibReport.ConfigFieldStr(this, "txtCambio", string.Empty, "");

                #region ML
                LibReport.ConfigFieldDec(this, "txtSimboloMonedaFact", string.Empty, "SimboloMonedaFact");
                LibReport.ConfigFieldDec(this, "txtSimboloMonedaLocal", string.Empty, "SimboloMonedaLocal");
                LibReport.ConfigFieldDec(this, "txtSimboloMonedaExtranjera", string.Empty, "SimboloMonedaExtranjera");

                LibReport.ConfigFieldDec(this, "txtTotalRenglones", string.Empty, "TotalRenglones");
                LibReport.ConfigFieldDec(this, "txtPorcentajeDesc1", string.Empty, "PorcentajeDesc1");
                LibReport.ConfigFieldDec(this, "txtPorcentajeDesc2", string.Empty, "PorcentajeDesc2");
                LibReport.ConfigFieldDec(this, "txtMontoDesc1", string.Empty, "MontoDescuento1");
                LibReport.ConfigFieldDec(this, "txtMontoDesc2", string.Empty, "MontoDescuento2");
                LibReport.ConfigFieldDec(this, "txtSubTotal", string.Empty, "SubTotal");
                LibReport.ConfigFieldDec(this, "txtTotalMontoExento", string.Empty, "TotalMontoExento");
                LibReport.ConfigFieldDec(this, "txtPorcentajeAlicuota1", string.Empty, "PorcentajeAlicuota1");
                LibReport.ConfigFieldDec(this, "txtPorcentajeAlicuota2", string.Empty, "PorcentajeAlicuota2");
                LibReport.ConfigFieldDec(this, "txtPorcentajeAlicuota3", string.Empty, "PorcentajeAlicuota3");
                LibReport.ConfigFieldDec(this, "txtMontoGravableAlicuota1", string.Empty, "MontoGravableAlicuota1");
                LibReport.ConfigFieldDec(this, "txtMontoGravableAlicuota2", string.Empty, "MontoGravableAlicuota2");
                LibReport.ConfigFieldDec(this, "txtMontoGravableAlicuota3", string.Empty, "MontoGravableAlicuota3");
                LibReport.ConfigFieldDec(this, "txtTotalIvaAlic1", string.Empty, "MontoIVAAlicuota1");
                LibReport.ConfigFieldDec(this, "txtTotalIvaAlic2", string.Empty, "MontoIVAAlicuota2");
                LibReport.ConfigFieldDec(this, "txtTotalIvaAlic3", string.Empty, "MontoIVAAlicuota3");                
                LibReport.ConfigFieldDec(this, "txtTotalFactura", string.Empty, "TotalFactura");
                LibReport.ConfigFieldDec(this, "txtAlicuotaIGTF", string.Empty, "AlicuotaIGTF");
                LibReport.ConfigFieldDec(this, "txtBaseImponibleIGTF", string.Empty, "BaseImponibleIGTF");
                LibReport.ConfigFieldDec(this, "txtIGTF", string.Empty, "IGTF");
                LibReport.ConfigFieldDec(this, "txtTotalFacturaMasIGTF", string.Empty, "TotalFacturaMasIGTF");
                LibReport.ConfigFieldStr(this, "txtMontoDelAbono", string.Empty, "MontoDelAbono");
                LibReport.ConfigFieldStr(this, "txtTotalFacturaMenosAbono", string.Empty, "TotalFacturaMenosAbono");
                #endregion ML

                #region ME
                LibReport.ConfigFieldDec(this, "txtTotalRenglonesME", string.Empty, "");
                LibReport.ConfigFieldDec(this, "txtPorcentajeDesc1ME", string.Empty, "PorcentajeDesc1");
                LibReport.ConfigFieldDec(this, "txtPorcentajeDesc2ME", string.Empty, "PorcentajeDesc2");
                LibReport.ConfigFieldDec(this, "txtMontoDesc1ME", string.Empty, "");
                LibReport.ConfigFieldDec(this, "txtMontoDesc2ME", string.Empty, "");
                LibReport.ConfigFieldDec(this, "txtSubTotalME", string.Empty, "");
                LibReport.ConfigFieldDec(this, "txtTotalMontoExentoME", string.Empty, "");
                LibReport.ConfigFieldDec(this, "txtPorcentajeAlicuota1ME", string.Empty, "PorcentajeAlicuota1");
                LibReport.ConfigFieldDec(this, "txtPorcentajeAlicuota2ME", string.Empty, "PorcentajeAlicuota2");
                LibReport.ConfigFieldDec(this, "txtPorcentajeAlicuota3ME", string.Empty, "PorcentajeAlicuota3");
                LibReport.ConfigFieldDec(this, "txtMontoGravableAlicuota1ME", string.Empty, "");
                LibReport.ConfigFieldDec(this, "txtMontoGravableAlicuota2ME", string.Empty, "");
                LibReport.ConfigFieldDec(this, "txtMontoGravableAlicuota3ME", string.Empty, "");
                LibReport.ConfigFieldDec(this, "txtTotalIvaAlic1ME", string.Empty, "");
                LibReport.ConfigFieldDec(this, "txtTotalIvaAlic2ME", string.Empty, "");
                LibReport.ConfigFieldDec(this, "txtTotalIvaAlic3ME", string.Empty, "");
                LibReport.ConfigFieldDec(this, "txtSimboloMonedaExt", string.Empty, "");
                LibReport.ConfigFieldDec(this, "txtTotalFacturaME", string.Empty, "");
                LibReport.ConfigFieldDec(this, "txtAlicuotaIGTFME", string.Empty, "AlicuotaIGTF");
                LibReport.ConfigFieldDec(this, "txtBaseImponibleIGTFME", string.Empty, "");
                LibReport.ConfigFieldDec(this, "txtIGTFME", string.Empty, "");
                LibReport.ConfigFieldDec(this, "txtTotalFacturaMasIGTFME", string.Empty, "");
                LibReport.ConfigFieldDec(this, "txtMontoAbonado", string.Empty, "");
                LibReport.ConfigFieldDec(this, "txtTotalFacturaMenosAbono", string.Empty, "");
                LibReport.ConfigFieldDec(this, "txtMontoAbonadoME", string.Empty, "");
                LibReport.ConfigFieldDec(this, "txtTotalFacturaMenosAbonoME", string.Empty, "");
                LibReport.ConfigFieldStr(this, "txtMontoDelAbonoME", string.Empty, "");
                LibReport.ConfigFieldStr(this, "txtTotalFacturaMenosAbonoME", string.Empty, "");
                #endregion ME
                #endregion GFDatosDelDocumento -> Totales

                AsignarMargenes(); 
                return true;
            }
            return false;
        }

        private string MontoEnLetras(int valConsecutivoCompania, string valNumeroDocumento, eTipoDocumentoFactura valTipoDocumento) {
            string vResult = string.Empty;
            return vResult;
        }
        #endregion //Metodos Generados
        void AsignarMargenes() {
            LibGraphPrnMargins.SetGeneralMargins(this, PageOrientation.Portrait);
            //XElement vElement = LibPrnSttMargins.GetReportMargins("Borrador Genérico de Factura", true, 0.5M, true, 0.5M, true, 0.5M, true, 0.50M, true, ePageOrientation.Portrait, true, false, 8.5M, false, 11M);
            //LibGraphPrnMargins.SetMargins(this, vElement);
        }

        private void GHDatosDeDespacho_Format(object sender, EventArgs e) {
            try {
                GHDatosDeDespacho.Visible = LibConvert.ToInt(txtNoDirDespachoAimprimir.Value) > 0;
            } catch (Exception) {
                throw;
            }
        }

        private void GFDatosDelDocumento_Format(object sender, EventArgs e) {
            try {
                decimal vCambio = CambioSegunTipoDocumento();
                this.txtCambio.Text = LibConvert.NumToString(vCambio, 4);
                this.txtSimbolo.Text = this.txtSimboloMonedaFact.Text;
                if (LibString.S1IsEqualToS2(_CodigoMonedaDocumento, _CodigoMonedaLocal)) {//Convierte de ML a ME
                    this.txtSimboloME.Text = this.txtSimboloMonedaExtranjera.Text;
                    this.txtTotalRenglonesME.Text = Divide(LibConvert.ToDec(this.txtTotalRenglones.Value), vCambio);
                    this.txtMontoDesc1ME.Text = Divide(LibConvert.ToDec(this.txtMontoDesc1.Value), vCambio);
                    this.txtMontoDesc2ME.Text = Divide(LibConvert.ToDec(this.txtMontoDesc2.Value), vCambio);
                    this.txtSubTotalME.Text = Divide(LibConvert.ToDec(this.txtSubTotal.Value), vCambio);
                    this.txtTotalMontoExentoME.Text = Divide(LibConvert.ToDec(this.txtTotalMontoExento.Value), vCambio);
                    this.txtMontoGravableAlicuota1ME.Text = Divide(LibConvert.ToDec(this.txtMontoGravableAlicuota1.Value), vCambio);
                    this.txtMontoGravableAlicuota2ME.Text = Divide(LibConvert.ToDec(this.txtMontoGravableAlicuota2.Value), vCambio);
                    this.txtMontoGravableAlicuota3ME.Text = Divide(LibConvert.ToDec(this.txtMontoGravableAlicuota3.Value), vCambio);
                    this.txtTotalIvaAlic1ME.Text = Divide(LibConvert.ToDec(this.txtTotalIvaAlic1.Value), vCambio);
                    this.txtTotalIvaAlic2ME.Text = Divide(LibConvert.ToDec(this.txtTotalIvaAlic2.Value), vCambio);
                    this.txtTotalIvaAlic3ME.Text = Divide(LibConvert.ToDec(this.txtTotalIvaAlic3.Value), vCambio);
                    this.txtTotalFacturaME.Text = Divide(LibConvert.ToDec(this.txtTotalFactura.Value), vCambio);
                    this.txtBaseImponibleIGTFME.Text = Divide(LibConvert.ToDec(this.txtBaseImponibleIGTF.Value), vCambio);
                    this.txtIGTFME.Text = Divide(LibConvert.ToDec(this.txtIGTF.Value), vCambio);
                    this.txtTotalFacturaMasIGTFME.Text = Divide(LibConvert.ToDec(this.txtTotalFacturaMasIGTF.Value), vCambio);
                    this.txtMontoDelAbonoME.Text = Divide(LibConvert.ToDec(this.txtMontoDelAbono.Value), vCambio);
                    this.txtTotalFacturaMenosAbonoME.Text = Divide(LibConvert.ToDec(this.txtTotalFacturaMenosAbono.Value), vCambio);
                } else {//Convierte de ME a ML
                    this.txtSimboloME.Text = this.txtSimboloMonedaLocal.Text;
                    this.txtTotalRenglonesME.Text = Multiplica(LibConvert.ToDec(this.txtTotalRenglones.Value), vCambio);
                    this.txtMontoDesc1ME.Text = Multiplica(LibConvert.ToDec(this.txtMontoDesc1.Value), vCambio);
                    this.txtMontoDesc2ME.Text = Multiplica(LibConvert.ToDec(this.txtMontoDesc2.Value), vCambio);
                    this.txtSubTotalME.Text = Multiplica(LibConvert.ToDec(this.txtSubTotal.Value), vCambio);
                    this.txtTotalMontoExentoME.Text = Multiplica(LibConvert.ToDec(this.txtTotalMontoExento.Value), vCambio);
                    this.txtMontoGravableAlicuota1ME.Text = Multiplica(LibConvert.ToDec(this.txtMontoGravableAlicuota1.Value), vCambio);
                    this.txtMontoGravableAlicuota2ME.Text = Multiplica(LibConvert.ToDec(this.txtMontoGravableAlicuota2.Value), vCambio);
                    this.txtMontoGravableAlicuota3ME.Text = Multiplica(LibConvert.ToDec(this.txtMontoGravableAlicuota3.Value), vCambio);
                    this.txtTotalIvaAlic1ME.Text = Multiplica(LibConvert.ToDec(this.txtTotalIvaAlic1.Value), vCambio);
                    this.txtTotalIvaAlic2ME.Text = Multiplica(LibConvert.ToDec(this.txtTotalIvaAlic2.Value), vCambio);
                    this.txtTotalIvaAlic3ME.Text = Multiplica(LibConvert.ToDec(this.txtTotalIvaAlic3.Value), vCambio);
                    this.txtTotalFacturaME.Text = Multiplica(LibConvert.ToDec(this.txtTotalFactura.Value), vCambio);
                    this.txtBaseImponibleIGTFME.Text = Multiplica(LibConvert.ToDec(this.txtBaseImponibleIGTF.Value), vCambio);
                    this.txtIGTFME.Text = Multiplica(LibConvert.ToDec(this.txtIGTF.Value), vCambio);
                    this.txtTotalFacturaMasIGTFME.Text = Multiplica(LibConvert.ToDec(this.txtTotalFacturaMasIGTF.Value), vCambio);
                    this.txtMontoDelAbonoME.Text = Multiplica(LibConvert.ToDec(this.txtMontoDelAbono.Value), vCambio);
                    this.txtTotalFacturaMenosAbonoME.Text = Multiplica(LibConvert.ToDec(this.txtTotalFacturaMenosAbono.Value), vCambio);
                }
            } catch (Exception) {
                throw;
            }
        }

        private string Divide(decimal valMonto, decimal valCambio) {
            decimal vResult = LibMath.RoundToNDecimals(valMonto / valCambio, 2);
            return LibConvert.NumToString(vResult, 2);
        }

        private string Multiplica(decimal valMonto, decimal valCambio) {
            decimal vResult = LibMath.RoundToNDecimals(valMonto * valCambio, 2);
            return LibConvert.NumToString(vResult, 2);
        }

        private decimal CambioSegunTipoDocumento() {
            decimal vResult = 1;
            decimal vCambioTemp = 1;
            if (_TipoDocumento == eTipoDocumentoFactura.NotaDeCredito || _TipoDocumento == eTipoDocumentoFactura.NotaDeCreditoComprobanteFiscal) {
                vCambioTemp = LibConvert.ToDec(this.txtCambioTotalEnDivisas.Value);
                if (vCambioTemp == 1) {
                    vCambioTemp = LibConvert.ToDec(this.txtCambioCxC.Value);
                    if (vCambioTemp == 1) {
                        vCambioTemp = LibConvert.ToDec(this.txtCambioABolivares.Value);
                    }
                }
            } else if (_TipoDocumento == eTipoDocumentoFactura.NotaDeDebito) {
                vCambioTemp = LibConvert.ToDec(this.txtCambioTotalEnDivisas.Value);
                if (vCambioTemp == 1) {
                    vCambioTemp = LibConvert.ToDec(this.txtCambioCxC.Value);
                    if (vCambioTemp == 1) {
                        vCambioTemp = LibConvert.ToDec(this.txtCambioABolivares.Value);
                    }
                }
            } else {
                vCambioTemp = LibConvert.ToDec(this.txtCambioTotalEnDivisas.Value);
                if (vCambioTemp == 1) {
                    vCambioTemp = LibConvert.ToDec(this.txtCambioCxC.Value);
                    if (vCambioTemp == 1) {
                        vCambioTemp = LibConvert.ToDec(this.txtCambioABolivares.Value);
                    }
                }
                if (vCambioTemp == 1) {
                    vCambioTemp = LibConvert.ToDec(this.txtCambioFechaDocumento.Value);
                }
            }
            vResult = vCambioTemp;
            if (vResult == 0) vResult = 1;
            return vResult;

        }

        private void GHDatosDelDocumento_Format(object sender, EventArgs e) {
            _CodigoMonedaDocumento = LibConvert.ToStr(this.txtCodigoMonedaFact.Value);
            _CodigoMonedaLocal = LibConvert.ToStr(this.txtCodigoMonedaLocal.Value);
        }
    } //End of class dsrFacturaBorradorGenerico

} //End of namespace Galac.Adm.Rpt.Venta