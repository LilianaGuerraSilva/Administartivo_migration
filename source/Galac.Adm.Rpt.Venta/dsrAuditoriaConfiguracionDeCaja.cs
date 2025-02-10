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
    /// Summary description for dsrAuditoriaConfiguracionDeCaja.
    /// </summary>
    public partial class dsrAuditoriaConfiguracionDeCaja : DataDynamics.ActiveReports.ActiveReport {
        #region Variables
        private bool _UseExternalRpx;
        private static string _RpxFileName;
        #endregion //Variables
        #region Constructores
        public dsrAuditoriaConfiguracionDeCaja()
            : this(false, string.Empty) {
        }

        public dsrAuditoriaConfiguracionDeCaja(bool initUseExternalRpx, string initRpxFileName) {
            InitializeComponent();
            _UseExternalRpx = initUseExternalRpx;
            if (_UseExternalRpx) {
                _RpxFileName = initRpxFileName;
            }
        }
        #endregion //Constructores
        #region Metodos Generados

        public string ReportTitle() {
            return "Auditoria Configuracion de Caja";
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

                //				LibReport.ConfigFieldDate(this, "txtFechaYHora", string.Empty, "FechaYHora", LibGalac.Aos.Base.Report.eDateOutputFormat.DateShort); esta sobrecarga no está en versión 5.0.2.0 de lib, temporalmente pasar formato directo
                LibReport.ConfigFieldDate(this, "txtFechaYHora", string.Empty, "FechaYHora", "dd/MM/yyyy hh:mm tt");
                LibReport.ConfigFieldStr(this, "txtVersionPrograma", string.Empty, "VersionPrograma");
                LibReport.ConfigFieldStr(this, "txtAccion", string.Empty, "Accion");
                LibReport.ConfigFieldStr(this, "txtMotivo", string.Empty, "Motivo");
                LibReport.ConfigFieldStr(this, "txtConfiguracionOriginal", string.Empty, "ConfiguracionOriginal");
                LibReport.ConfigFieldStr(this, "txtConfiguracionNueva", string.Empty, "ConfiguracionNueva");
                LibReport.ConfigFieldStr(this, "txtNombreOperador", string.Empty, "NombreOperador");
                LibGraphPrnMargins.SetGeneralMargins(this, DataDynamics.ActiveReports.Document.PageOrientation.Portrait);
                return true;
            }
            return false;
        }
        #endregion //Metodos Generados


    } //End of class dsrAuditoriaConfiguracionDeCaja

} //End of namespace Galac.Adm.Rpt.Venta

