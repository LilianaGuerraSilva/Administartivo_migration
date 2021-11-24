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

        public bool ConfigReport(DataTable valDataSource, Dictionary<string, string> valParameters) {
            Saw.Lib.clsUtilRpt UtiltRpt = new Saw.Lib.clsUtilRpt();;
            bool vUsaContabilidad = LibConvert.SNToBool(valParameters["UsaContabilidad"]);
            bool vMostrarContacto = LibConvert.SNToBool(valParameters["MostrarContacto"]);
            Saw.Lib.eMonedaParaImpresion MonedaDelReporte = (Saw.Lib.eMonedaParaImpresion)LibConvert.DbValueToEnum(valParameters["MonedaDelReporte"]);
            Saw.Lib.eTasaDeCambioParaImpresion TipoTasaDeCambio = (Saw.Lib.eTasaDeCambioParaImpresion)LibConvert.DbValueToEnum(valParameters["TipoTasaDeCambio"]);

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
                LibReport.ConfigHeader(this, "txtCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "txtNumeroDePagina", "lblFechaInicialYFinal", LibGraphPrnSettings.PrintPageNumber, LibGraphPrnSettings.PrintEmitDate);

                if (vIsInMonedaLocal) {
                    if (vIsInTasaDelDia) {
                        LibReport.ConfigLabel(this, "lblMensajeDelCambioDeLaMoneda", "Nota: Los montos en monedas extranjeras son calculados a " + vMonedaLocal + " tomando en cuenta la última tasa de cambio.");
                    }
                    else {
                        LibReport.ConfigLabel(this, "lblMensajeDelCambioDeLaMoneda", "Nota: Los montos en monedas extranjeras son calculados a " + vMonedaLocal + " tomando en cuenta la tasa de cambio original.");
                    }
                }
                else {
                    LibReport.ChangeControlVisibility(this, "lblMensajeDelCambioDeLaMoneda", true, false);
                }

                LibReport.ConfigGroupHeader(this, "GHStatus", "StatusStr", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.After);
                LibReport.ConfigFieldStr(this, "txtStatus", string.Empty, "StatusStr");

                if (vIsInMonedaLocal) {
                    LibReport.ConfigFieldStr(this, "txtMonedaPorGrupo", vMonedaLocal, string.Empty);
                }
                else {
                    LibReport.ConfigGroupHeader(this, "GHMoneda", "Moneda", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.None);
                    LibReport.ConfigFieldStr(this, "txtMonedaPorGrupo", string.Empty, "Moneda");
                }

                LibReport.ConfigFieldDate(this, "txtFecha", string.Empty, "Fecha", "dd/MM/yyyy");
				LibReport.ConfigFieldStr(this, "txtNumeroDocumento", string.Empty, "Numero");
				LibReport.ConfigFieldStr(this, "txtCodigoCliente", string.Empty, "Codigo");
				LibReport.ConfigFieldStr(this, "txtNombreCliente", string.Empty, "Nombre");
				LibReport.ConfigFieldDec(this, "txtMonto", string.Empty, "Monto");

                if (vUsaContabilidad) {
                    LibReport.ConfigFieldStr(this, "txtNComprobante", string.Empty, "NumeroComprobante");
                }
                else {
                    LibReport.ChangeControlVisibility(this, "txtNComprobante", true, false);
                    LibReport.ChangeControlVisibility(this, "lbNComprobante", true, false);
                }
                
                if (vMostrarContacto) {
                    LibReport.ConfigFieldStr(this, "txtContacto", string.Empty, "Contacto");
                }
                else {
                    LibReport.ChangeControlVisibility(this, "txtContacto", true, false);
                    LibReport.ChangeControlVisibility(this, "lblContacto", true, false);
                }

                LibReport.ConfigSummaryField(this, "txtTotalMonto", "Monto", SummaryFunc.Sum, "GHMoneda", SummaryRunning.All, SummaryType.SubTotal);

                LibGraphPrnMargins.SetGeneralMargins(this, PageOrientation.Portrait);
                return true;
            }
            return false;
        }
        #endregion //Metodos Generados

    } //End of class dsrCxCPendientesEntreFechas

} //End of namespace Galac.Adm.Rpt.Venta
