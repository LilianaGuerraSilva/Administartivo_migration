using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Document;
using System.Data;
using LibGalac.Aos.Base;
using LibGalac.Aos.DefGen;
using LibGalac.Aos.ARRpt;

namespace Galac.Adm.Rpt.Venta {
    /// <summary>
    /// Summary description for dsrFacturacionPorArticuloDetallado.
    /// </summary>
    public partial class dsrFacturacionPorUsuarioDetallado : ActiveReport {
        #region Variables
        private bool _UseExternalRpx;
        private static string _RpxFileName;
        private decimal totalPorMoneda = 0;
        private int countCantidadDeFacturasPorUsuario = 0;
        #endregion //Variables

        #region Constructores
        public dsrFacturacionPorUsuarioDetallado()
            : this(false, string.Empty) {
        }

        public dsrFacturacionPorUsuarioDetallado(bool initUseExternalRpx, string initRpxFileName) {
            InitializeComponent();
            _UseExternalRpx = initUseExternalRpx;
            if (_UseExternalRpx) {
                _RpxFileName = initRpxFileName;
            }
        }
        #endregion //Constructores

        #region Metodos Generados

        public string ReportTitle() {
            return "Facturación por Usuario - Detallado";
        }

		public bool ConfigReport(DataTable valDataSource, Dictionary<string, string> valParameters) {
            Saw.Lib.eMonedaParaImpresion MonedaDelReporte = (Saw.Lib.eMonedaParaImpresion)LibConvert.DbValueToEnum(valParameters["MonedaDelReporte"]);
            Saw.Lib.eTasaDeCambioParaImpresion TipoTasaDeCambio = (Saw.Lib.eTasaDeCambioParaImpresion)LibConvert.DbValueToEnum(valParameters["TipoTasaDeCambio"]);
            bool UsaContabilidad = LibConvert.SNToBool(valParameters["UsaContabilidad"]);
            bool UsaPrecioSinIva = LibConvert.SNToBool(valParameters["UsaPrecioSinIva"]);
            int vCantDecimales = LibConvert.ToInt(valParameters["CantidadDeDecimalesInt"]);

            Comun.Ccl.TablasGen.IMonedaLocalActual vMonedaLocalActual = new Comun.Brl.TablasGen.clsMonedaLocalActual();
            vMonedaLocalActual.CargarTodasEnMemoriaYAsignarValoresDeLaActual(LibDefGen.ProgramInfo.Country, LibDate.Today());
            string vMonedaLocal = vMonedaLocalActual.NombreMoneda(LibDate.Today());
            bool vIsInMonedaLocal = LibString.S1IsInS2(vMonedaLocal, MonedaDelReporte.GetDescription());
            bool vIsInTasaDelDia = TipoTasaDeCambio == Saw.Lib.eTasaDeCambioParaImpresion.DelDia;

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
                LibReport.ConfigHeader(this, "txtCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "txtNumeroDePagina", "lblFechaInicialYFinal", LibGraphPrnSettings.PrintPageNumber, LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintEmitDate);

                LibReport.ConfigGroupHeader(this, "GHUsuario", "NombreOperador", GroupKeepTogether.FirstDetail, RepeatStyle.OnPageIncludeNoDetail, true, NewPage.None);
                LibReport.ConfigFieldStr(this, "txtUsuario", string.Empty, "NombreOperador");

                LibReport.ConfigGroupHeader(this, "GHMoneda", "Moneda", GroupKeepTogether.FirstDetail, RepeatStyle.OnPageIncludeNoDetail, true, NewPage.None);
                LibReport.ConfigFieldStr(this, "txtMoneda", string.Empty, "Moneda");

                LibReport.ConfigGroupHeader(this, "GHFactura", "Numero", GroupKeepTogether.FirstDetail, RepeatStyle.OnPageIncludeNoDetail, true, NewPage.None);
                LibReport.ConfigFieldDate(this, "txtFecha", string.Empty, "Fecha", LibGalac.Aos.Base.Report.eDateOutputFormat.DateShort);
                LibReport.ConfigFieldStr(this, "txtNumero", string.Empty, "Numero");
                LibReport.ConfigFieldStr(this, "txtNombre", string.Empty, "Nombre");
                LibReport.ConfigFieldDecWithNDecimal(this, "txtCambio", string.Empty, "Cambio", 4);
                LibReport.ConfigFieldDec(this, "txtPorcentajeDescuentoFactura", string.Empty, "PorcentajeDescuentoFactura");
                LibReport.ConfigFieldDec(this, "txtTotalFactura", string.Empty, "TotalFactura");
                
                if (UsaContabilidad) {
                    LibReport.ConfigFieldStr(this, "txtNumeroComprobante", string.Empty, "NumeroComprobante");
                } else {
                    LibReport.ChangeControlVisibility(this, "lblNumeroComprobante", true, false);
                    LibReport.ChangeControlVisibility(this, "txtNumeroComprobante", true, false);
                }

                LibReport.ConfigFieldStr(this, "txtCodigoArticulo", string.Empty, "Articulo");
                LibReport.ConfigFieldStr(this, "txtDescripcion", string.Empty, "Descripcion");
                LibReport.ConfigFieldDecWithNDecimal(this, "txtCantidad", string.Empty, "Cantidad", vCantDecimales);
                LibReport.ConfigFieldDec(this, "txtPorcentajeDescuentoRenglon", string.Empty, "PorcentajeDescuentoRenglon");
                LibReport.ConfigFieldDecWithNDecimal(this, "txtPrecio", string.Empty, "Precio", vCantDecimales);
                LibReport.ConfigFieldDec(this, "txtTotalRenglon", string.Empty, "TotalRenglon");

                string valStrLblNota = "Nota:\tLos precios presentados " + (UsaPrecioSinIva ? "no " : "") + "incluyen el IVA.";
                if (vIsInMonedaLocal) {
                    valStrLblNota += "\n\tLos montos en monedas extranjeras son calculados a " + vMonedaLocal;
                    valStrLblNota += " tomando en cuenta la " + ( vIsInTasaDelDia ? "última tasa de cambio." : "tasa de cambio original." );
                } else {
                    LibReport.ConfigLabel(this, "lblCambio", string.Empty);
                    LibReport.ChangeControlVisibility(this, "txtCambio", true, false);
                }
                LibReport.ConfigLabel(this, "LblNota", valStrLblNota);

                LibGraphPrnMargins.SetGeneralMargins(this, PageOrientation.Portrait);
                return true;
            }
            return false;
        }

        private void GHUsuario_Format(object sender, EventArgs e) {
            countCantidadDeFacturasPorUsuario = 0;
        }

        private void GHMoneda_Format(object sender, EventArgs e) {
            totalPorMoneda = 0;
        }

        private void GHFactura_AfterPrint(object sender, EventArgs e) {
            totalPorMoneda += ( Decimal.TryParse(txtTotalFactura.Text, out Decimal resultado) ? resultado : 0 );
            countCantidadDeFacturasPorUsuario++;
        }

        private void GFMoneda_Format(object sender, EventArgs e) {
            txtTotalMoneda.Text = LibConvert.ToStr(totalPorMoneda);
        }

        private void GFUsuario_Format(object sender, EventArgs e) {
            txtCantidadDeFacturasPorUsuario.Text = countCantidadDeFacturasPorUsuario.ToString();
        }
		#endregion //Metodos Generados

	} //End of class dsrFacturacionPorUsuario

} //End of namespace Galac.Adm.Rpt.Venta

