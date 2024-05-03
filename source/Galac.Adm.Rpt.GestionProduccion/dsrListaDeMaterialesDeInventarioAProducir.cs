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
    public partial class dsrListaDeMaterialesDeInventarioAProducir : DataDynamics.ActiveReports.ActiveReport {
        #region Variables
        private bool _UseExternalRpx;
        private static string _RpxFileName;
        #endregion //Variables
        #region Constructor
        public dsrListaDeMaterialesDeInventarioAProducir()
            : this(false, string.Empty) {
        }
        public dsrListaDeMaterialesDeInventarioAProducir(bool initUseExternalRpx, string initRpxFileName) {
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
                return "Lista de Materiales de Inventario a Producir";
            }
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
                LibReport.ConfigLabel(this, "lblFechaYHoraDeEmision", LibReport.PromptEmittedOnDateAtHour);
                LibReport.ConfigHeader(this, "txtNombreCompania", "lblFechaYHoraDeEmision", "lblTituloInforme", "txtNroDePagina", "lblFechaInicialYFinal", LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintPageNumber, LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintEmitDate);

                LibReport.ConfigFieldStr(this, "txtCodigoDescripcionArticuloInventario", string.Empty, "InventarioAProducir");
                LibReport.ConfigFieldStr(this, "txtCodigoNombreListaMateriales", string.Empty, "ListaDeMateriales");
                LibReport.ConfigFieldStr(this, "txtArticuloInventario", string.Empty, "Articulo");
                LibReport.ConfigFieldStr(this, "txtTipoDeArticulo", string.Empty, "TipoDeArticulo");

                LibReport.ConfigFieldDec(this, "txtCantidad", string.Empty, "Cantidad", "n" + 8, true, TextAlignment.Right);
                LibReport.ConfigFieldDec(this, "txtCostoUnitario", string.Empty, "CostoUnitario", "n" + 4, true, TextAlignment.Right);
                LibReport.ConfigFieldDec(this, "txtCantidadAProducir", string.Empty, "CantidadAProducir", "n" + 2, true, TextAlignment.Right);
                LibReport.ConfigFieldDec(this, "txtCantidadAReservar", string.Empty, "CantidadAReservarEnInventario", "n" + 2, true, TextAlignment.Right);
                LibReport.ConfigFieldDec(this, "txtCostoTotal", string.Empty, "CostoTotal", "n" + 4, true, TextAlignment.Right);
                LibReport.ConfigFieldDec(this, "txtExistencia", string.Empty, "ExistenciaToStr", "n" + 4, true, TextAlignment.Right);

                LibReport.ConfigGroupHeader(this, "GHSecInventarioAProducir", "CodigoInventarioAProducir", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.None);
                LibReport.ConfigGroupHeader(this, "GHSecListaDeMateriales", "CodigoListaDeMateriales", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.None);
                LibReport.ConfigSummaryField(this, "txtT_CostoTotal", "CostoTotal", SummaryFunc.Sum, "GHSecListaDeMateriales", SummaryRunning.Group, SummaryType.SubTotal, "n" + 4, "");

                LibGraphPrnMargins.SetGeneralMargins(this, DataDynamics.ActiveReports.Document.PageOrientation.Portrait);
                LibReport.AddNoDataEvent(this);
                return true;
            }
            return false;
        }
        #endregion
    }
}
