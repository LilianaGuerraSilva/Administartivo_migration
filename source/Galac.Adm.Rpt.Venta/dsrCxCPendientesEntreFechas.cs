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
    public partial class dsrCxCPendientesEntreFechas : DataDynamics.ActiveReports.ActiveReport {
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
            return "CxC Pendientes entre Fechas";
        }

        public bool ConfigReport(DataTable valDataSource, Dictionary<string, string> valParameters) {
            Saw.Lib.clsUtilRpt UtiltRpt = new Saw.Lib.clsUtilRpt();
            bool vUsaContabilidad = LibConvert.SNToBool(valParameters["UsaContabilidad"]);
            bool vUsaContacto = LibConvert.SNToBool(valParameters["UsaContacto"]);
            Saw.Lib.eMonedaParaImpresion vMonedaParaImpresion = ((Saw.Lib.eMonedaParaImpresion)LibConvert.DbValueToEnum(valParameters["MonedaParaElReporte"]));
            string vNombreCompania = valParameters["NombreCompania"];
            if (_UseExternalRpx) {
                string vRpxPath = LibWorkPaths.PathOfRpxFile(_RpxFileName, ReportTitle(), false, LibDefGen.ProgramInfo.ProgramInitials);//acá se indicaría si se busca en ULS, por defecto buscaría en app.path... Tip: Una función con otro nombre.
                if (!LibString.IsNullOrEmpty(vRpxPath, true)) {
                    LibReport.LoadLayout(this, vRpxPath);
                }
            }
            if (LibReport.ConfigDataSource(this, valDataSource)) {
                LibReport.ConfigFieldStr(this, "txtCompania", string.Empty, valParameters["NombreCompania"]);
                LibReport.ConfigLabel(this, "lblTituloDelReporte", ReportTitle());
                LibReport.ConfigLabel(this, "lblFechaInicialYFinal", valParameters["FechaInicialYFinal"]);
                LibReport.ConfigLabel(this, "lblFechaYHoraDeEmision", LibReport.PromptEmittedOnDateAtHour);
                LibReport.ConfigHeader(this, "txtCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "txtNumeroDePagina", "lblFechaInicialYFinal", LibGraphPrnSettings.PrintPageNumber, LibGraphPrnSettings.PrintEmitDate);

//				LibReport.ConfigFieldDate(this, "txtFecha", string.Empty, "Fecha", LibGalac.Aos.Base.Report.eDateOutputFormat.DateShort); esta sobrecarga no está en versión 5.0.2.0 de lib, temporalmente pasar formato directo
				LibReport.ConfigFieldDate(this, "txtFecha", string.Empty, "Fecha", "dd/MM/yyyy");
				LibReport.ConfigFieldStr(this, "txtNumeroDocumento", string.Empty, "Numero");
				LibReport.ConfigFieldStr(this, "txtCodigoCliente", string.Empty, "Codigo");
				LibReport.ConfigFieldStr(this, "txtNombreCliente", string.Empty, "Nombre");
				LibReport.ConfigFieldDec(this, "txtMontoOriginal", string.Empty, "Monto");

                if (vUsaContabilidad) {
                    LibReport.ConfigFieldStr(this, "txtNComprobante", string.Empty, "NumeroComprobante");
                }
                else {
                    LibReport.ChangeControlVisibility(this, "txtNComprobante", true, false);
                    LibReport.ChangeControlVisibility(this, "lbNComprobante", true, false);
                }
                
                if (vUsaContacto) {
                    LibReport.ConfigFieldStr(this, "txtContacto", string.Empty, "Contacto");
                }
                else {
                    LibReport.ChangeControlVisibility(this, "txtContacto", true, false);
                    LibReport.ChangeControlVisibility(this, "lblContacto", true, false);
                }

                LibReport.ConfigGroupHeader(this, "GHStatus", "StatusStr", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.None);
				LibReport.ConfigGroupHeader(this, "GHMoneda", "MonedaParaGrupo", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.None);

                LibReport.ConfigFieldStr(this, "txtStatus", string.Empty, "StatusStr");
                LibReport.ConfigFieldStr(this, "txtMonedaDelGrupo", string.Empty, "MonedaParaGrupo");

                LibReport.ConfigSummaryField(this, "txtTotalMontoOriginal", "Monto", SummaryFunc.Sum, "GHMoneda", SummaryRunning.All, SummaryType.SubTotal);

                LibGraphPrnMargins.SetGeneralMargins(this, PageOrientation.Portrait);
                return true;
            }
            return false;
        }
        #endregion //Metodos Generados


    } //End of class dsrCxCPendientesEntreFechas

} //End of namespace Galac.Adm.Rpt.Venta
