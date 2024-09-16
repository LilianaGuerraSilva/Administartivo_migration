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
using Galac.Saw.Ccl.Inventario;

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
            return "Art�culos pr�ximos a Vencer";
        }

        public bool ConfigReport(DataTable valDataSource, Dictionary<string, string> valParameters) {
            if (_UseExternalRpx) {
                string vRpxPath = LibWorkPaths.PathOfRpxFile(_RpxFileName, ReportTitle(), false, LibDefGen.ProgramInfo.ProgramInitials);//ac� se indicar�a si se busca en ULS, por defecto buscar�a en app.path... Tip: Una funci�n con otro nombre.
                if (!LibString.IsNullOrEmpty(vRpxPath, true)) {
                    LibReport.LoadLayout(this, vRpxPath);
                }
            }
            eCantidadAImprimirArticulo vCantidadAImprimir = (eCantidadAImprimirArticulo)LibConvert.DbValueToEnum(valParameters["CantidadAImprimir"]);
            if (LibReport.ConfigDataSource(this, valDataSource)) {
                LibReport.ConfigFieldStr(this, "txtNombreCompania", valParameters["NombreCompania"], string.Empty);
                LibReport.ConfigLabel(this, "lblTituloInforme", ReportTitle());
                LibReport.ConfigLabel(this, "lblFechaYHoraDeEmision", LibReport.PromptEmittedOnDateAtHour);
                LibReport.ConfigHeader(this, "txtNombreCompania", "lblFechaYHoraDeEmision", "lblTituloInforme", "txtNroDePagina", "lblFechaInicialYFinal", LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintPageNumber, LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintEmitDate);
                LibReport.ConfigFieldInt(this, "txtDiasPorVencer", valParameters["DiasPorVencer"], string.Empty);
                LibReport.ConfigFieldStr(this, "txtLineaDeProducto", string.Empty, "LineaDeProducto");
                LibReport.ConfigFieldStr(this, "txtCodigo", string.Empty, "Codigo");
                LibReport.ConfigFieldStr(this, "txtDescripcion", string.Empty, "Descripcion");
                LibReport.ConfigFieldDec(this, "txtExistencia", string.Empty, "Existencia");
                LibReport.ConfigFieldStr(this, "txtLote", string.Empty, "Lote");
                LibReport.ConfigFieldDate(this, "txtFechaVencimiento", string.Empty, "FechaDeVencimiento", "dd/MM/yyyy");
                LibReport.ConfigFieldInt(this, "txtDiasParaVencerse", string.Empty, "DiasPorVencer");
                LibReport.ConfigSummaryField(this, "txtTotalExistenciaporVencer", "Existencia", SummaryFunc.Sum, "GHLineaDeProducto", SummaryRunning.Group, SummaryType.SubTotal);
                LibGraphPrnMargins.SetGeneralMargins(this, DataDynamics.ActiveReports.Document.PageOrientation.Portrait);
                if (vCantidadAImprimir != eCantidadAImprimirArticulo.LineaDeProducto) {
                    LibReport.ChangeControlVisibility(this, "txtLineaDeProducto", false);
                    LibReport.ChangeControlVisibility(this, "lblLineaDeProducto", false);
                } else {
                    LibReport.ConfigGroupHeader(this, "GHLineaDeProducto", "LineaDeProducto", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.None);
                }
                return true;
            }
            return false;
            #endregion //Metodos Generados  
        }
    } //End of class dsrArticulosPorVencer
} //End of namespace Galac.Saw.Rpt.Inventario

