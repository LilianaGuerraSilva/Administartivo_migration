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
namespace Galac.Adm.Rpt.CajaChica {
    /// <summary>
    /// Summary description for dsrReposicionesEntreFechas.
    /// </summary>
    public partial class dsrReposicionesEntreFechas : DataDynamics.ActiveReports.ActiveReport {
        #region Variables
        private bool _UseExternalRpx;
        private static string _RpxFileName;
        #endregion //Variables
        #region Constructores
        public dsrReposicionesEntreFechas()
            : this(false, string.Empty) {
        }

        public dsrReposicionesEntreFechas(bool initUseExternalRpx, string initRpxFileName) {
            InitializeComponent();
            _UseExternalRpx = initUseExternalRpx;
            if (_UseExternalRpx) {
                _RpxFileName = initRpxFileName;
            }
        }
        #endregion //Constructores
        #region Metodos Generados

        public string ReportTitle() {
            return "Reposiciones de Caja Chica Entre Fechas";
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
                LibReport.ConfigLabel(this, "lblFechaInicialYFinal", valParameters["FechaInicialYFinal"]);
                LibReport.ConfigLabel(this, "lblFechaYHoraDeEmision", LibReport.PromptEmittedOnDateAtHour);
                LibReport.ConfigHeader(this, "txtNombreCompania", "lblFechaYHoraDeEmision", "lblTituloInforme", "txtNroDePagina", "lblFechaInicialYFinal", LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintPageNumber, LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintEmitDate);

				LibReport.ConfigFieldStr(this, "txtCodigoCtaBancariaCajaChica", string.Empty, "CodigoCtaBancariaCajaChica");
				LibReport.ConfigFieldStr(this, "txtNombreCuentaBancariaCajaChica", string.Empty, "NombreCuentaBancariaCajaChica");
				LibReport.ConfigFieldDate(this, "txtFechaApertura", string.Empty, "FechaApertura", LibGalac.Aos.Base.Report.eDateOutputFormat.DateShort); //esta sobrecarga no está en versión 5.0.2.0 de lib, temporalmente pasar formato directo
				LibReport.ConfigFieldDate(this, "txtFechaCierre", string.Empty, "FechaCierre", LibGalac.Aos.Base.Report.eDateOutputFormat.DateShort); //esta sobrecarga no está en versión 5.0.2.0 de lib, temporalmente pasar formato directo
				LibReport.ConfigFieldStr(this, "txtDescripcion", string.Empty, "Descripcion");
				LibReport.ConfigFieldStr(this, "txtNumero", string.Empty, "Numero");
                LibReport.ConfigFieldStr(this, "txtObservaciones", string.Empty, "Observaciones");
				LibReport.ConfigFieldStr(this, "txtStatusRendicion", string.Empty, "StatusRendicionStr");
				LibReport.ConfigFieldDec(this, "txtTotalGastos", string.Empty, "TotalGastos");
				LibReport.ConfigFieldStr(this, "txtNumeroDocumento", string.Empty, "NumeroDocumento");

                LibReport.ConfigSummaryField(this, "txtMontoTotalGastos", "TotalGastos", SummaryFunc.Sum, "GHSecCajaChica", SummaryRunning.Group, SummaryType.SubTotal);
                if (LibConvert.SNToBool(valParameters["UnaPaginaPorCajaChica"])) {
                    LibReport.ConfigGroupHeader(this, "GHSecCajaChica", "CodigoCtaBancariaCajaChica", GroupKeepTogether.FirstDetail, RepeatStyle.All, true, NewPage.After);
                } else {
                    LibReport.ConfigGroupHeader(this, "GHSecCajaChica", "CodigoCtaBancariaCajaChica", GroupKeepTogether.FirstDetail, RepeatStyle.All, true, NewPage.None);
                }
                LibReport.AddNoDataEvent(this);
                LibGraphPrnMargins.SetGeneralMargins(this, DataDynamics.ActiveReports.Document.PageOrientation.Landscape);                
                return true;
            }
            return false;
        }
        #endregion //Metodos Generados
    }
}
