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
using Galac.Saw.Lib;

namespace Galac.Adm.Rpt.Venta {
    /// <summary>
    /// Summary description for dsrCxCPendientesEntreFechas.
    /// </summary>
    public partial class dsrCxCPendientesEntreFechas : ActiveReport {
        #region Variables
        private bool _UseExternalRpx;
        private static string _RpxFileName;
        #endregion //Variables

        #region Constructores
        public dsrCxCPendientesEntreFechas()
            : this(false, string.Empty) {
        }

        public dsrCxCPendientesEntreFechas(bool initUseExternalRpx, string initRpxFileName) {
            InitializeComponent();
            _UseExternalRpx = initUseExternalRpx;
            if (_UseExternalRpx) {
                _RpxFileName = initRpxFileName;
            }
        }
        #endregion //Constructores

        #region Metodos Generados
        public string ReportTitle() {
            return "Cuentas por Cobrar Pendientes entre Fechas";
        }

        public bool ConfigReport(DataTable valDataSource, Dictionary<string, string> valParameters, bool valMostrarContacto, bool valMostrarNroComprobanteContable, eMonedaDelInformeMM valMonedaDelInforme, string valMoneda, eTasaDeCambioParaImpresion valTasaDeCambio) {
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

                LibReport.ConfigFieldStr(this, "txtMoneda", string.Empty, "MonedaTotales");
                LibReport.ConfigFieldStr(this, "txtStatus", string.Empty, "StatusStr");
                LibReport.ConfigFieldDate(this, "txtFecha", string.Empty, "Fecha", "dd/MM/yy");
                LibReport.ConfigFieldStr(this, "txtNumeroDocumento", string.Empty, "Numero");
                LibReport.ConfigFieldStr(this, "txtCodigoCliente", string.Empty, "CodigoCliente");
                LibReport.ConfigFieldStr(this, "txtNombreCliente", string.Empty, "NombreCliente");
                LibReport.ConfigFieldDec(this, "txtCambio", string.Empty, "Cambio", "#,###.0000", false, TextAlignment.Right);
                LibReport.ConfigFieldDec(this, "txtMontoRestante", string.Empty, "MontoRestante");

                if (valMostrarNroComprobanteContable && LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "CaracteristicaDeContabilidadActiva")) {
                    LibReport.ConfigFieldStr(this, "txtNComprobante", string.Empty, "NroComprobanteContable");
                } else {
                    LibReport.ChangeControlVisibility(this, "lblNComprobante", false);
                    LibReport.ChangeControlVisibility(this, "txtNComprobante", false);
                    float vWidth = lblNComprobante.Width;
                    lblNombreCliente.Width += vWidth;
                    txtNombreCliente.Width += vWidth;
                    lblCambio.Left += vWidth;
                    txtCambio.Left += vWidth;
                    lblMontoRestante.Left += vWidth;
                    txtMontoRestante.Left += vWidth;

                    txtTotalMontoRestantePorStatus.Left = lblMontoRestante.Left;
                    txtTotalMontoRestantePorMoneda.Left = lblMontoRestante.Left;

                    txtGFStatusCxC.Width += vWidth;
                    txtGFMoneda.Width += vWidth;
                }
                if (valMostrarContacto) {
                    LibReport.ConfigFieldStr(this, "txtContacto", string.Empty, "Contacto");
                } else {
                    LibReport.ChangeControlVisibility(this, "txtContacto", false);
                    LibReport.ChangeControlVisibility(this, "lblContacto", false);
                    txtContacto.Height = 0;
                    lblContacto.Height = 0;
                }
                LibReport.ConfigFieldStr(this, "txtGFStatusCxC", string.Empty, "StatusStr");
                LibReport.ConfigGroupHeader(this, "GHStatus", "Status", GroupKeepTogether.FirstDetail, RepeatStyle.All, true, NewPage.None);
                LibReport.ConfigSummaryField(this, "txtTotalMontoOriginalPorStatus", "MontoOriginal", SummaryFunc.Sum, "GHStatus", SummaryRunning.Group, SummaryType.SubTotal);
                LibReport.ConfigSummaryField(this, "txtTotalMontoRestantePorStatus", "MontoRestante", SummaryFunc.Sum, "GHStatus", SummaryRunning.Group, SummaryType.SubTotal);

                LibReport.ConfigFieldStr(this, "txtGFMoneda", string.Empty, "MonedaTotales");
                LibReport.ConfigGroupHeader(this, "GHMoneda", "Moneda", GroupKeepTogether.FirstDetail, RepeatStyle.All, true, NewPage.None);
                LibReport.ConfigSummaryField(this, "txtTotalMontoOriginalPorMoneda", "MontoOriginal", SummaryFunc.Sum, "GHMoneda", SummaryRunning.Group, SummaryType.SubTotal);
                LibReport.ConfigSummaryField(this, "txtTotalMontoRestantePorMoneda", "MontoRestante", SummaryFunc.Sum, "GHMoneda", SummaryRunning.Group, SummaryType.SubTotal);

                string vNotaMonedaCambio = new clsLibSaw().NotaMonedaCambioParaInformes(valMonedaDelInforme, valTasaDeCambio, valMoneda, "cuenta por cobrar");
                LibReport.ConfigFieldStr(this, "txtNotaMonedaCambio", vNotaMonedaCambio, "");

                LibGraphPrnMargins.SetGeneralMargins(this, DataDynamics.ActiveReports.Document.PageOrientation.Portrait);
                return true;
            }
            return false;
        }
        #endregion //Metodos Generados

        private void Detail_Format(object sender, EventArgs e) {
            try {
                if (LibString.IsNullOrEmpty(txtNComprobante.Text, true)) {
                    txtNComprobante.Text = "No aplica";
                }
            } catch (Exception) {
                throw;
            }

        }
    } //End of class dsrCxCPendientesEntreFechas

} //End of namespace Galac.Adm.Rpt.Venta
