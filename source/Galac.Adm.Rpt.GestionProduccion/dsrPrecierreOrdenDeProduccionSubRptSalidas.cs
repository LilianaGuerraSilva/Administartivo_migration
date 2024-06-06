using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Document;
using LibGalac.Aos.Base;
using Galac.Adm.Brl.GestionProduccion;
using Galac.Adm.Ccl.GestionProduccion;
using LibGalac.Aos.ARRpt;
using System.Data;
using LibGalac.Aos.DefGen;

namespace Galac.Adm.Rpt.GestionProduccion {
    /// <summary>
    /// Summary description for dsrPrecierreOrdenDeProduccionSubRpt.
    /// </summary>
    public partial class dsrPrecierreOrdenDeProduccionSubRptSalidas : DataDynamics.ActiveReports.ActiveReport {
        #region Variables
        private bool _UseExternalRpx;
        private static string _RpxFileName;
        #endregion //Variables
        #region Constructor
        public dsrPrecierreOrdenDeProduccionSubRptSalidas()
            : this(false, string.Empty) {
        }
        public dsrPrecierreOrdenDeProduccionSubRptSalidas(bool initUseExternalRpx, string initRpxFileName) {
            InitializeComponent();
            _UseExternalRpx = initUseExternalRpx;
            if (_UseExternalRpx) {
                _RpxFileName = initRpxFileName;
            }
        }
        #endregion
        #region Metodos Generados
        public string ReportTitle() {            
            return "Precierre Orden de Producción";
        }

        public bool ConfigReport(DataTable valData) {
            if (_UseExternalRpx) {
                string vRpxPath = LibWorkPaths.PathOfRpxFile(_RpxFileName, ReportTitle(), false, LibDefGen.ProgramInfo.ProgramInitials);//acá se indicaría si se busca en ULS, por defecto buscaría en app.path... Tip: Una función con otro nombre.
                if (!LibString.IsNullOrEmpty(vRpxPath, true)) {
                    LibReport.LoadLayout(this, vRpxPath);
                }
            }
            if (LibReport.ConfigDataSource(this, valData)) {
                //LibReport.ConfigFieldStr(this, "txtNombreCompania", valParameters["NombreCompania"], string.Empty);
                //LibReport.ConfigLabel(this, "lblTituloInforme", ReportTitle());
                //LibReport.ConfigLabel(this, "lblFechaInicialYFinal", valParameters["FechaInicialYFinal"]);
                //LibReport.ConfigLabel(this, "lblFechaYHoraDeEmision", LibReport.PromptEmittedOnDateAtHour);
                //LibReport.ConfigHeader(this, "txtNombreCompania", "lblFechaYHoraDeEmision", "lblTituloInforme", "txtNroDePagina", "lblFechaInicialYFinal", LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintPageNumber, LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintEmitDate);                                
                LibReport.ConfigFieldStr(this, "txtArticulo", string.Empty, "ArticuloSalida");
                LibReport.ConfigFieldStr(this, "txtUnidad", string.Empty, "Unidad");
                LibReport.ConfigFieldDecWithNDecimal(this, "txtCantidadSolicitada", string.Empty, "CantidadSolicitada", 8);               
                LibReport.ConfigGroupHeader(this, "GHSecOrdenDeProduccion", "Codigo", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.After);
                LibGraphPrnMargins.SetGeneralMargins(this, DataDynamics.ActiveReports.Document.PageOrientation.Portrait);
                LibReport.AddNoDataEvent(this);
                return true;
            }
            return false;
        }
        #endregion
    }
}
