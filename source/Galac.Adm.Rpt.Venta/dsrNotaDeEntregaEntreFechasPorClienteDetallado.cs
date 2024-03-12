using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Document;

using LibGalac.Aos.ARRpt;
using LibGalac.Aos.Base;
using LibGalac.Aos.DefGen;
using System.Data;
using Galac.Saw.Lib;

namespace Galac.Adm.Rpt.Venta
{
    /// <summary>
    /// Summary description for dsrNotaDeEntregaEntreFechasPorClienteDetallado.
    /// </summary>
    public partial class dsrNotaDeEntregaEntreFechasPorClienteDetallado : DataDynamics.ActiveReports.ActiveReport { 
        #region Variables
        private bool _UseExternalRpx;
        private static string _RpxFileName;
        #endregion //Variables
        #region Constructores
        public dsrNotaDeEntregaEntreFechasPorClienteDetallado()
            : this(false, string.Empty) {
        }
            //
        public dsrNotaDeEntregaEntreFechasPorClienteDetallado(bool initUseExternalRpx, string initRpxFileName) {
            InitializeComponent();
            _UseExternalRpx = initUseExternalRpx;
            if (_UseExternalRpx) {
                _RpxFileName = initRpxFileName;
            }
        }
        #endregion //Constructores
        #region Metodos Generados

        public string ReportTitle() {
            return "Notas de Entrega por Cliente";
        }

        public bool ConfigReport(DataTable valDataSource, Dictionary<string, string> valParameters, eMonedaDelInformeMM valMonedaDelInforme, string valMoneda, eTasaDeCambioParaImpresion valTasaDeCambio) {
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

				LibReport.ConfigFieldStr(this, "txtCodigo", string.Empty, "CodigoCliente");
				LibReport.ConfigFieldStr(this, "txtNombre", string.Empty, "Cliente");
				LibReport.ConfigFieldStr(this, "txtMoneda", string.Empty, "Moneda");
				LibReport.ConfigFieldDate(this, "txtFecha", string.Empty, "Fecha", "dd/MM/yy");
				LibReport.ConfigFieldStr(this, "txtNroDocumento", string.Empty, "Numero");
				LibReport.ConfigFieldStr(this, "txtMonedaDoc", string.Empty, "MonedaDoc");
				LibReport.ConfigFieldDec(this, "txtCambio", string.Empty, "Cambio");
				LibReport.ConfigFieldDec(this, "txtAnulada", string.Empty, "EsAnulada");
				LibReport.ConfigFieldDec(this, "txtMontoTotal", string.Empty, "TotalFactura");

                //LibReport.ConfigSummaryField(this, "txtT_SumMontoTotal_SecMoneda", "SumMontoTotal", SummaryFunc.Sum, "GHSecMoneda", SummaryRunning.Group, SummaryType.SubTotal);
                if (LibConvert.SNToBool(valParameters["IncluirDetalleNotasDeEntregas"])) {
                    LibReport.ChangeSectionPropertiesVisibleAndHeight(this, "GHSecDetalle", true, (float)0.15);
                    LibReport.ChangeSectionPropertiesVisibleAndHeight(this, "Detail", true, (float)0.156);
                    LibReport.ChangeControlVisibility(this, "lblCodigoArticulo", true);
                    LibReport.ChangeControlVisibility(this, "lblDescripcionArticulo", true);
                    LibReport.ChangeControlVisibility(this, "lblCantidad", true);
                    LibReport.ChangeControlVisibility(this, "lblPrecio", true);
                    LibReport.ChangeControlVisibility(this, "lblTotalRenglon", true);
                    LibReport.ChangeControlVisibility(this, "txtCodigoArticulo", true);
                    LibReport.ChangeControlVisibility(this, "txtDescripcionArticulo", true);
                    LibReport.ChangeControlVisibility(this, "txtCantidad", true);
                    LibReport.ChangeControlVisibility(this, "txtPrecio", true);
                    LibReport.ChangeControlVisibility(this, "txtTotalRenglon", true);
                    LibReport.ConfigFieldStr(this, "txtCodigoArticulo", string.Empty, "CodigoArticulo");
                    LibReport.ConfigFieldStr(this, "txtDescripcionArticulo", string.Empty, "Descripcion");
                    LibReport.ConfigFieldDec(this, "txtCantidad", string.Empty, "Cantidad");
                    LibReport.ConfigFieldDec(this, "txtPrecio", String.Empty, "Precio");
                    LibReport.ConfigFieldDec(this, "txtTotalRenglon", String.Empty, "TotalRenglon");
                }
                LibReport.ConfigFieldStr(this, "txtMonedaReporte", string.Empty, "Moneda");
                LibReport.ConfigSummaryField(this, "txtSumMontoTotal", "TotalRenglon", SummaryFunc.Sum, "GHSecMoneda", SummaryRunning.Group, SummaryType.SubTotal);
                LibReport.ConfigGroupHeader(this, "GHSecDocumento", "Numero", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.None);
				LibReport.ConfigGroupHeader(this, "GHSecMoneda", "Moneda", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.None);

                string vNotaMonedaCambio = new clsLibSaw().NotaMonedaCambioParaInformes(valMonedaDelInforme, valTasaDeCambio, valMoneda, "Notas de Entrega");
                LibReport.ConfigFieldStr(this, "txtNotaMonedaCambio", vNotaMonedaCambio, "");
                LibReport.ChangeControlVisibility(this, "txtNotaMonedaCambio", true);

                LibGraphPrnMargins.SetGeneralMargins(this, DataDynamics.ActiveReports.Document.PageOrientation.Portrait);
                return true;
            }
            return false;
        }
        #endregion //Metodos Generados

        private void dsrNotaDeEntregaEntreFechasPorClienteDetallado_ReportStart(object sender, EventArgs e)
        {

        }
    } //End of class dsrNotaDeEntregaEntreFechasPorClienteDetallado

} //End of namespace Galac.Dbo.Rpt.ComponenteNoEspecificado
