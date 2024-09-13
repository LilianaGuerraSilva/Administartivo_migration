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
using LibGalac.Aos.Base.Report;

namespace Galac.Saw.Rpt.Inventario {
    
    public partial class dsrArticulosPorVencer : DataDynamics.ActiveReports.ActiveReport {
        #region Variables
        private bool _UseExternalRpx;
        private static string _RpxFileName;
        #endregion //Variables
        #region Constructores
        public dsrArticulosPorVencer()
            : this(false, string.Empty) {
        }

        public dsrArticulosPorVencer(bool initUseExternalRpx, string initRpxFileName) {
            InitializeComponent();
            _UseExternalRpx = initUseExternalRpx;
            if (_UseExternalRpx) {
                _RpxFileName = initRpxFileName;
            }
        }
        #endregion //Constructores
        #region Metodos Generados

        public string ReportTitle() {
            return "Lote de Inventario";
        }

        public bool ConfigReport(DataTable valDataSource, Dictionary<string, string> valParameters) {
            if (_UseExternalRpx) {
                string vRpxPath = LibWorkPaths.PathOfRpxFile(_RpxFileName, ReportTitle(), false, LibDefGen.ProgramInfo.ProgramInitials);//acá se indicaría si se busca en ULS, por defecto buscaría en app.path... Tip: Una función con otro nombre.
                if (!LibString.IsNullOrEmpty(vRpxPath, true)) {
                    LibReport.LoadLayout(this, vRpxPath);
                }
            }
            eCantidadAImprimir vCantidadAImprimir = (eCantidadAImprimir)LibConvert.DbValueToEnum(valParameters["CantidadAImprimir"]);
            if (LibReport.ConfigDataSource(this, valDataSource)) {
                LibReport.ConfigFieldStr(this, "txtNombreCompania", valParameters["NombreCompania"], string.Empty);
                LibReport.ConfigLabel(this, "lblTituloInforme", ReportTitle());
                LibReport.ConfigLabel(this, "lblFechaInicialYFinal", valParameters["FechaInicialYFinal"]);
                LibReport.ConfigLabel(this, "lblFechaYHoraDeEmision", LibReport.PromptEmittedOnDateAtHour);
                LibReport.ConfigHeader(this, "txtNombreCompania", "lblFechaYHoraDeEmision", "lblTituloInforme", "txtNroDePagina", "lblFechaInicialYFinal", LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintPageNumber, LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintEmitDate);
				LibReport.ConfigFieldStr(this, "txtCodigo", string.Empty, "Codigo");
				LibReport.ConfigFieldStr(this, "txtDescripcion", string.Empty, "Descripcion");
				LibReport.ConfigFieldDec(this, "txtExistencia", string.Empty, "Existencia");
				LibReport.ConfigFieldStr(this, "txtLote", string.Empty, "Lote");
				LibReport.ConfigFieldDate(this, "txtFechaVencimiento", string.Empty, "FechaVencimiento", "dd/MM/yyyy");
                LibReport.ConfigFieldInt(this, "txtDiasParaVencerse", string.Empty, "DiasParaVencerse");				
				LibReport.ConfigGroupHeader(this, "GHLineaDeProducto", "", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.None);
                LibGraphPrnMargins.SetGeneralMargins(this, DataDynamics.ActiveReports.Document.PageOrientation.Portrait);
                return true;
            }
            return false;
        }
        #endregion //Metodos Generados


    } //End of class dsrArticulosPorVencer

} //End of namespace Galac.Saw.Rpt.Inventario

