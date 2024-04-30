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
    /// Summary description for dsrListaDeMaterialesDeInventarioAProducir.
    /// </summary>
    public partial class dsrListaDeMaterialesDeSalida : DataDynamics.ActiveReports.ActiveReport {
        #region Variables
        private bool _UseExternalRpx;
        private static string _RpxFileName;
        #endregion //Variables
        #region Constructor
        public dsrListaDeMaterialesDeSalida()
            : this(false, string.Empty) {
        }
        public dsrListaDeMaterialesDeSalida(bool initUseExternalRpx, string initRpxFileName) {
            InitializeComponent();
            _UseExternalRpx = initUseExternalRpx;
            if (_UseExternalRpx) {
                _RpxFileName = initRpxFileName;
            }
        }
        #endregion
        #region Metodos Generados
        public string ReportTitle() {
            IListaDeMaterialesPdn insListaMateriales = new clsListaDeMaterialesNav();
            if (!LibString.IsNullOrEmpty(insListaMateriales.NombreParaMostrarListaDeMateriales())) {
                return insListaMateriales.NombreParaMostrarListaDeMateriales() + " de Inventario a Producir";
            } else {
                return "Lista De Materiales de Salida";
            }
        }

        public bool ConfigReport(DataTable valDataSourceSalidas, DataTable valDataSourceInsumos, Dictionary<string, string> valParameters) {
            if (_UseExternalRpx) {
                string vRpxPath = LibWorkPaths.PathOfRpxFile(_RpxFileName, ReportTitle(), false, LibDefGen.ProgramInfo.ProgramInitials);//acá se indicaría si se busca en ULS, por defecto buscaría en app.path... Tip: Una función con otro nombre.
                if (!LibString.IsNullOrEmpty(vRpxPath, true)) {
                    LibReport.LoadLayout(this, vRpxPath);
                }
            }
            if (LibReport.ConfigDataSource(this, valDataSourceSalidas)) {
                LibReport.ConfigFieldStr(this, "txtNombreCompania", valParameters["NombreCompania"], "");
                LibReport.ConfigFieldStr(this, "txtMoneda", valParameters["NombreMoneda"], string.Empty);
                LibReport.ConfigLabel(this, "lblTituloInforme", ReportTitle());
                LibReport.ConfigLabel(this, "lblFechaYHoraDeEmision", LibReport.PromptEmittedOnDateAtHour);
                LibReport.ConfigHeader(this, "txtNombreCompania", "lblFechaYHoraDeEmision", "lblTituloInforme", "txtNroDePagina", "lblFechaInicialYFinal", LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintPageNumber, LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintEmitDate);
                LibReport.ConfigFieldStr(this, "txtListaDeMateriales", string.Empty, "ListaDeMateriales");
                LibReport.ConfigFieldDec(this, "txtCantidadAProducir", string.Empty, "CantidadAProducir");
                LibReport.ConfigFieldDec(this, "txtCantidadAProducirDetalle", string.Empty, "CantidadAProducirDetalle");
                LibReport.ConfigFieldStr(this, "txtArticulo", string.Empty, "ArticuloListaSalida");
                LibReport.ConfigFieldStr(this, "txtUnidades", string.Empty, "Unidades");
                LibReport.ConfigFieldDec(this, "txtPorcentajeCosto", string.Empty, "PorcentajeDeCosto");
                LibReport.ConfigFieldDec(this, "txtCostoCalculado", string.Empty, "CostoCalculado");
                LibReport.ConfigGroupHeader(this, "GHCodigoListaAProducir", "", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.None);                
                LibReport.ConfigSummaryField(this, "txtTotalCostoCalculado", "CostoCalculado", SummaryFunc.Sum, "GHCodigoListaAProducir", SummaryRunning.Group, SummaryType.SubTotal);                
                LibReport.SetSubReportIfExists(this, SubRptListaDeInsumos(valDataSourceInsumos), "srptListaDeInsumos");
                LibGraphPrnMargins.SetGeneralMargins(this, DataDynamics.ActiveReports.Document.PageOrientation.Portrait);
                return true;
            }
            return false;
        }

        private ActiveReport SubRptListaDeInsumos(DataTable valDataSourceInsumos) {
            dsrListaDeMaterialesDeInsumos vRpt = new dsrListaDeMaterialesDeInsumos();
            vRpt.ConfigReport(valDataSourceInsumos);
            return vRpt;
        }
        #endregion
    }
}
