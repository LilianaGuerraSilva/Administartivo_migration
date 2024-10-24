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
namespace Galac.Adm.Rpt.GestionProduccion {

    public partial class dsrListaDeMaterialesDeInsumos : DataDynamics.ActiveReports.ActiveReport {
        #region Variables
        private bool _UseExternalRpx;
        private static string _RpxFileName;
        #endregion //Variables
        #region Constructores
        public dsrListaDeMaterialesDeInsumos()
            : this(false, string.Empty) {
        }

        public dsrListaDeMaterialesDeInsumos(bool initUseExternalRpx, string initRpxFileName) {
            InitializeComponent();
            _UseExternalRpx = initUseExternalRpx;
            if(_UseExternalRpx) {
                _RpxFileName = initRpxFileName;
            }
        }
        #endregion //Constructores
        #region Metodos Generados

        public string ReportTitle() {
            return "Lista De Materiales Insumos";
        }

        public bool ConfigReport(DataTable valDataSource) {
            if(_UseExternalRpx) {
                string vRpxPath = LibWorkPaths.PathOfRpxFile(_RpxFileName, ReportTitle(), false, LibDefGen.ProgramInfo.ProgramInitials);//acá se indicaría si se busca en ULS, por defecto buscaría en app.path... Tip: Una función con otro nombre.
                if(!LibString.IsNullOrEmpty(vRpxPath, true)) {
                    LibReport.LoadLayout(this, vRpxPath);
                }
            }
            if (LibReport.ConfigDataSource(this, valDataSource)) {
                LibReport.ConfigGroupHeader(this, "GHListaInsumos", "Codigo", GroupKeepTogether.None, RepeatStyle.None, false, NewPage.None);
                LibReport.ConfigFieldStr(this, "txtArticulo", string.Empty, "ListaArticuloInsumos");
                LibReport.ConfigFieldStr(this, "txtUnidades", string.Empty, "Unidades");
                LibReport.ConfigFieldStr(this, "txtCodigo", string.Empty, "Codigo");
                LibReport.ConfigFieldDecWithNDecimal(this, "txtCantidad", string.Empty, "CantidadInsumos", 8);
                LibReport.ConfigFieldDec(this, "txtCosto", string.Empty, "CostoUnitario");
                LibReport.ConfigFieldDecWithNDecimal(this, "txtCantidadAReservar", string.Empty, "CantidadAReservar", 8);
                LibReport.ConfigFieldDec(this, "txtCostoTotal", string.Empty, "CostoTotal");
                LibReport.ConfigFieldDec(this, "txtExistencia", string.Empty, "Existencia");
                LibReport.ConfigFieldDecWithNDecimal(this, "txtMermaNormalInsumos", string.Empty, "MermaNormalInsumos", 8);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtPorcMermaNormalInsumos", string.Empty, "PorcentajeMermaNormalInsumos ", 8);
                LibReport.ConfigSummaryField(this, "txtTotalCostoCalculado", "CostoTotal", SummaryFunc.Sum, "GHListaInsumos", SummaryRunning.Group, SummaryType.SubTotal);
                return true;
            }
            return false;
        }
        #endregion //Metodos Generados
    } //End of class dsrListaDeMaterialesDeInsumos

} //End of namespace Galac.Adm.Rpt.GestionProduccion

