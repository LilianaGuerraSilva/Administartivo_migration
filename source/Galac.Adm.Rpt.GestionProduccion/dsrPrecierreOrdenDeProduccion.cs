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
    /// Summary description for dsrPrecierreOrdenDeProduccion.
    /// </summary>
    public partial class dsrPrecierreOrdenDeProduccion : DataDynamics.ActiveReports.ActiveReport {
        #region Variables
        private bool _UseExternalRpx;
        private static string _RpxFileName;
        #endregion //Variables
        #region Constructor
        public dsrPrecierreOrdenDeProduccion()
            : this(false, string.Empty) {
        }
        public dsrPrecierreOrdenDeProduccion(bool initUseExternalRpx, string initRpxFileName) {
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

        public bool ConfigReport(DataTable valDataSourceSalidas, DataTable valDataSourceInsumos, Dictionary<string, string> valParameters) {
            if (_UseExternalRpx) {
                string vRpxPath = LibWorkPaths.PathOfRpxFile(_RpxFileName, ReportTitle(), false, LibDefGen.ProgramInfo.ProgramInitials);//acá se indicaría si se busca en ULS, por defecto buscaría en app.path... Tip: Una función con otro nombre.
                if (!LibString.IsNullOrEmpty(vRpxPath, true)) {
                    LibReport.LoadLayout(this, vRpxPath);
                }
            }
            if (LibReport.ConfigDataSource(this, valDataSourceSalidas)) {
                LibReport.ConfigFieldStr(this, "txtNombreCompania", valParameters["NombreCompania"], string.Empty);
                LibReport.ConfigLabel(this, "lblTituloInforme", ReportTitle());
                //LibReport.ConfigLabel(this, "lblFechaInicialYFinal", valParameters["FechaInicialYFinal"]);                
                LibReport.ConfigLabel(this, "lblFechaYHoraDeEmision", LibReport.PromptEmittedOnDateAtHour);
                LibReport.ConfigHeader(this, "txtNombreCompania", "lblFechaYHoraDeEmision", "lblTituloInforme", "txtNroDePagina", "lblFechaInicialYFinal", LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintPageNumber, LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintEmitDate);
                LibReport.ConfigFieldStr(this, "txtCodigoDeOrden", string.Empty, "Codigo");
                LibReport.ConfigFieldDate(this, "txtFechaInicio", string.Empty, "FechaInicio", LibGalac.Aos.Base.Report.eDateOutputFormat.DateLong);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtCantidadAProducirEstimada", string.Empty, "CantidadSolicitada", 8);
                LibReport.ConfigFieldStr(this, "txtListaDeMateriales", string.Empty, "NombreListaDeMateriales");
                LibReport.ConfigFieldStr(this, "txtAlmacenSalida", string.Empty, "AlmacenSalida");
                LibReport.ConfigFieldStr(this, "txtAlmacenEntrada", string.Empty, "AlmacenEntrada");
                LibReport.ConfigFieldStr(this, "txtArticulo", string.Empty, "ArticuloSalida");
                LibReport.ConfigFieldStr(this, "txtUnidad", string.Empty, "Unidad");
                LibReport.ConfigFieldDecWithNDecimal(this, "txtCantidadReservadaDeInventario", string.Empty, "CantidadAProducirEstimada", 8);
                LibReport.ConfigGroupHeader(this, "GHSecOrdenDeProduccion", "Codigo", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.After);
                LibReport.SetSubReportIfExists(this, SubRptListaDeSalidas(valDataSourceInsumos), "subRptSalidas");
                LibGraphPrnMargins.SetGeneralMargins(this, DataDynamics.ActiveReports.Document.PageOrientation.Portrait);
                LibReport.AddNoDataEvent(this);
                return true;
            }
            return false;
        }

        private ActiveReport SubRptListaDeSalidas(DataTable valDataSourceInsumos) {
            dsrPrecierreOrdenDeProduccionSubRptSalidas vRpt = new dsrPrecierreOrdenDeProduccionSubRptSalidas();
            vRpt.ConfigReport(valDataSourceInsumos);
            return vRpt;
        }
        #endregion
    }
}
