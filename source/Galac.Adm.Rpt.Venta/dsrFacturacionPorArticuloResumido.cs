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

namespace Galac.Adm.Rpt.Venta
{
    /// <summary>
    /// Summary description for dsrFacturacionPorArticuloResumido.
    /// </summary>
    public partial class dsrFacturacionPorArticuloResumido : ActiveReport {
        #region Variables
        private bool _UseExternalRpx;
        private static string _RpxFileName;
        #endregion //Variables

        #region Constructores
        public dsrFacturacionPorArticuloResumido()
            : this(false, string.Empty) {
        }

        public dsrFacturacionPorArticuloResumido(bool initUseExternalRpx, string initRpxFileName) {
            InitializeComponent();
            _UseExternalRpx = initUseExternalRpx;
            if (_UseExternalRpx) {
                _RpxFileName = initRpxFileName;
            }
        }
        #endregion //Constructores

        #region Metodos Generados
        public string ReportTitle() {
            return "Facturación por Artículo - Resumido";
        }

        public bool ConfigReport(DataTable valDataSource, Dictionary<string, string> valParameters) {
            Saw.Lib.eMonedaParaImpresion MonedaDelReporte = (Saw.Lib.eMonedaParaImpresion)LibConvert.DbValueToEnum(valParameters["MonedaDelReporte"]);
            Saw.Lib.eTasaDeCambioParaImpresion TipoTasaDeCambio = (Saw.Lib.eTasaDeCambioParaImpresion)LibConvert.DbValueToEnum(valParameters["TipoTasaDeCambio"]);
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

                LibReport.ConfigGroupHeader(this, "GHArticulo", "Codigo", GroupKeepTogether.FirstDetail, RepeatStyle.OnPageIncludeNoDetail, true, NewPage.None);
                LibReport.ConfigFieldStr(this, "txtCodigo", string.Empty, "Codigo");
                LibReport.ConfigFieldStr(this, "txtDescripcion", string.Empty, "Descripcion");
                LibReport.ConfigFieldStr(this, "txtUnidadDeVenta", string.Empty, "UnidadDeVenta");

                LibReport.ConfigGroupHeader(this, "GHMoneda", "Moneda", GroupKeepTogether.FirstDetail, RepeatStyle.OnPageIncludeNoDetail, true, NewPage.None);
                LibReport.ConfigFieldStr(this, "txtMoneda", string.Empty, "Moneda");

                LibReport.ConfigFieldDate(this, "txtFecha", string.Empty, "Fecha", LibGalac.Aos.Base.Report.eDateOutputFormat.DateShort);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtCantidad", string.Empty, "Cantidad", vCantDecimales);
                LibReport.ConfigFieldDec(this, "txtMonto", string.Empty, "Monto");

                LibReport.ConfigSummaryField(this, "txtTotalCantidad", "Cantidad", SummaryFunc.Sum, "GHMoneda", SummaryRunning.All, SummaryType.SubTotal, "n" + vCantDecimales, "");
                LibReport.ConfigSummaryField(this, "txtTotalMonto", "Monto", SummaryFunc.Sum, "GHMoneda", SummaryRunning.All, SummaryType.SubTotal);

                string valStrLblNota = "Nota:\tLos precios presentados " + (UsaPrecioSinIva? "no " : "") + "incluyen el IVA.";
                if (vIsInMonedaLocal) {
                    valStrLblNota += "\n\tLos montos en monedas extranjeras son calculados a " + vMonedaLocal;
                    valStrLblNota += " tomando en cuenta la " + ( vIsInTasaDelDia ? "última tasa de cambio." : "tasa de cambio original." );
                }
                LibReport.ConfigLabel(this, "LblNota", valStrLblNota);

                LibGraphPrnMargins.SetGeneralMargins(this, PageOrientation.Portrait);
                return true;
            }
            return false;
        }
        #endregion //Metodos Generados

    } //End of class dsrFacturacionPorArticulo

} //End of namespace Galac.Adm.Rpt.Venta

