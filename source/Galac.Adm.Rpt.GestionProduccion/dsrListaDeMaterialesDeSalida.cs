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
using Galac.Saw.Lib;

namespace Galac.Adm.Rpt.GestionProduccion {
    /// <summary>
    /// Summary description for dsrListaDeMaterialesDeInventarioAProducir.
    /// </summary>
    public partial class dsrListaDeMaterialesDeSalida : DataDynamics.ActiveReports.ActiveReport {
        #region Variables
        private bool _UseExternalRpx;
        private static string _RpxFileName;
        private string[] _ListaMonedasDelReporte { get; set; }
        private string _MonedaDelInforme { get; set; }
        private decimal _TasaDeCambio { get; set; }
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

        public bool ConfigReport(DataTable valDataSourceSalidas, DataTable valDataSourceInsumos, string[] valListaMonedasDelReporte, string valMonedaDelInforme, decimal valTasaDeCambio, Dictionary<string, string> valParameters) {
            _ListaMonedasDelReporte = valListaMonedasDelReporte;
            _MonedaDelInforme = valMonedaDelInforme;
            _TasaDeCambio = valTasaDeCambio;
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
                LibReport.ConfigFieldDecWithNDecimal(this, "txtCantidadAProducir", string.Empty, "CantidadAProducir", 8);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtCantidadArticulos", string.Empty, "CantidadArticulos", 8);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtCantidadAProducirDetalle", string.Empty, "CantidadDetalleAProducir", 8);
                LibReport.ConfigFieldStr(this, "txtArticulo", string.Empty, "ArticuloListaSalida");
                LibReport.ConfigFieldStr(this, "txtUnidades", string.Empty, "Unidades");
                LibReport.ConfigFieldDecWithNDecimal(this, "txtPorcentajeCosto", string.Empty, "PorcentajeDeCosto", 8);
                LibReport.ConfigFieldDec(this, "txtCostoUnitario", string.Empty, "CostoUnitario");
                LibReport.ConfigFieldDec(this, "txtCostoCalculado", string.Empty, "CostoTotal");
                LibReport.ConfigFieldStr(this, "txtManejaMerma", string.Empty, "ManejaMerma");
                LibReport.ConfigFieldDecWithNDecimal(this, "txtMermaNormalSalidas", string.Empty, "MermaNormalSalidas", 8);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtPorcMermaNormalSalidas", string.Empty, "PorcentajeMermaNormalSalidas", 8);
                LibReport.ConfigGroupHeader(this, "GHCodigoListaAProducir", "Codigo", GroupKeepTogether.FirstDetail, RepeatStyle.None, true, NewPage.None);
                LibReport.ConfigGroupHeader(this, "GHMoneda", "Moneda", GroupKeepTogether.FirstDetail, RepeatStyle.All, true, NewPage.Before);
                LibReport.ConfigGroupHeader(this, "GHSalidas", "Codigo", GroupKeepTogether.FirstDetail, RepeatStyle.None, true, NewPage.Before);
                LibReport.ConfigSummaryField(this, "txtTotalCostoCalculado", "Costototal", SummaryFunc.Sum, "GHSalidas", SummaryRunning.Group, SummaryType.SubTotal);
                LibReport.SetSubReportIfExists(this, SubRptListaDeInsumos(valDataSourceInsumos), "srptListaDeInsumos");
                LibGraphPrnMargins.SetGeneralMargins(this, DataDynamics.ActiveReports.Document.PageOrientation.Landscape);
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

        private void PageFooter_Format(object sender, EventArgs e) {
            if (LibString.S1IsEqualToS2(_MonedaDelInforme, _ListaMonedasDelReporte[0]) || LibString.S1IsEqualToS2(_MonedaDelInforme, _ListaMonedasDelReporte[1])) {
                this.txtNotaMonedaCambio.Value = "Los montos de los costos están expresados en " + txtMoneda.Text;
            } else if (LibString.S1IsEqualToS2(_MonedaDelInforme, _ListaMonedasDelReporte[2]) || LibString.S1IsEqualToS2(_MonedaDelInforme, _ListaMonedasDelReporte[3])) {
                int vPos = LibString.IndexOf(_MonedaDelInforme, "expresado en");
                if (vPos > 0) {
                    string vPrimeraMoneda = LibString.Trim(LibString.SubString(_MonedaDelInforme, 0, vPos));
                    string vSegundaMoneda = LibString.SubString(_MonedaDelInforme, vPos + LibString.Len("expresado en "));
                    this.txtNotaMonedaCambio.Value = $"Los montos de los costos en {vPrimeraMoneda} están expresados en {vSegundaMoneda} a la tasa {LibConvert.NumToString(_TasaDeCambio, 4)}";
                }
            }
        }

        private void Detail_Format(object sender, EventArgs e) {
            string vManejaMermaStr = "";
            for (int vNumSection = 0; vNumSection < this.Sections.Count; vNumSection++) {
                for (int vNumControl = 0; vNumControl < this.Sections[vNumSection].Controls.Count; vNumControl++) {
                    var name = this.Sections[vNumSection].Controls[vNumControl].Name;
                    if (LibString.S1IsEqualToS2("txtManejaMerma", name)) {
                        var vControl = this.Sections[vNumSection].Controls[vNumControl] as DataDynamics.ActiveReports.TextBox;
                        vManejaMermaStr = vControl.Text;                       
                    }
                }
            }
            
            if (LibConvert.SNToBool(vManejaMermaStr)) {                                               
                LibReport.ChangeControlVisibility(this, "txtMermaNormalSalidas", true);
                LibReport.ChangeControlVisibility(this, "txtPorcMermaNormalSalidas", true);
                LibReport.ChangeControlVisibility(this, "lblMermaNormalSalidas", true);
                LibReport.ChangeControlVisibility(this, "lblPorcMermaNormalSalidas", true);                             
                LibReport.ChangeControlVisibility(this, "lblMermaSalidasHide", false);
                lblMermaNormalSalidas.Visible = true;
                lblPorcMermaNormalSalidas.Visible = true;
                lblMermaSalidasHide.Visible = false;
            } else {                
                LibReport.ChangeControlVisibility(this, "txtMermaNormalSalidas", false);
                LibReport.ChangeControlVisibility(this, "txtPorcMermaNormalSalidas", false);
                LibReport.ChangeControlVisibility(this, "lblMermaNormalSalidas", false);
                LibReport.ChangeControlVisibility(this, "lblPorcMermaNormalSalidas", false);                
                LibReport.ChangeControlVisibility(this, "lblMermaSalidasHide", true);
                lblMermaNormalSalidas.Visible = false;
                lblPorcMermaNormalSalidas.Visible = false;
                lblMermaSalidasHide.Visible = true;
            }
        }
    }
}
