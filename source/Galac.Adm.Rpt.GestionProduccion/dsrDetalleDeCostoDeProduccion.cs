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
using Galac.Saw.Ccl.SttDef;

namespace Galac.Adm.Rpt.GestionProduccion {
    /// <summary>
    /// Summary description for DetalleDeCostoDeProduccion.
    /// </summary>
    public partial class dsrDetalleDeCostoDeProduccion : DataDynamics.ActiveReports.ActiveReport {
        #region Variables
        private bool _UseExternalRpx;
        private static string _RpxFileName;
        private string[] _ListaMonedasDelReporte { get; set; }
        private string _MonedaDelInforme { get; set; }
        private decimal _TasaDeCambio { get; set; }

        #endregion //Variables
        #region Propiedades
       
        #endregion
        #region Constructor
        public dsrDetalleDeCostoDeProduccion()
            : this(false, string.Empty) {
        }

        public dsrDetalleDeCostoDeProduccion(bool initUseExternalRpx, string initRpxFileName) {
            InitializeComponent();
            _UseExternalRpx = initUseExternalRpx;
            if (_UseExternalRpx) {
                _RpxFileName = initRpxFileName;
            }
        }
        #endregion
        #region Metodos Generados
        public string ReportTitle() {
            return "Detalle de Costo de Producci�n";
        }

        public bool ConfigReport(DataTable valDataSalida, DataTable valDataInsumos, string[] valListaMonedasDelReporte, string valMonedaDelInforme, decimal valTasaDeCambio, Dictionary<string, string> valParameters) {
            _ListaMonedasDelReporte = valListaMonedasDelReporte;
            _MonedaDelInforme = valMonedaDelInforme;
            _TasaDeCambio = valTasaDeCambio;
            string valNombreDocFiscal = string.Empty;
            if (LibDefGen.ProgramInfo.IsCountryVenezuela()) {
                valNombreDocFiscal = " RIF ";
            } else if (LibDefGen.ProgramInfo.IsCountryEcuador() || LibDefGen.ProgramInfo.IsCountryPeru()) {
                valNombreDocFiscal = " RUC ";
            }
            string vNombreCompania = valParameters["NombreCompania"] + " -" + valNombreDocFiscal + valParameters["RifCompania"];
            if (_UseExternalRpx) {
                string vRpxPath = LibWorkPaths.PathOfRpxFile(_RpxFileName, ReportTitle(), false, LibDefGen.ProgramInfo.ProgramInitials);//ac� se indicar�a si se busca en ULS, por defecto buscar�a en app.path... Tip: Una funci�n con otro nombre.
                if (!LibString.IsNullOrEmpty(vRpxPath, true)) {
                    LibReport.LoadLayout(this, vRpxPath);
                }
            }
            string valEstatus = LibConvert.ToStr(eTipoStatusOrdenProduccion.Cerrada);
            if (LibReport.ConfigDataSource(this, valDataInsumos)) {
                LibReport.ConfigFieldStr(this, "txtNombreCompania", vNombreCompania, string.Empty);
                LibReport.ConfigLabel(this, "lblTituloInforme", ReportTitle());
                LibReport.ConfigLabel(this, "lblFechaInicialYFinal", valParameters["FechaInicialYFinal"]);
                LibReport.ConfigLabel(this, "lblFechaYHoraDeEmision", LibReport.PromptEmittedOnDateAtHour);
                LibReport.ConfigHeader(this, "txtNombreCompania", "lblFechaYHoraDeEmision", "lblTituloInforme", "txtNroDePagina", "lblFechaInicialYFinal", LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintPageNumber, LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintEmitDate);
                LibReport.ConfigFieldStr(this, "txtOrden", string.Empty, "OrdenCodigoDescripcion");
                LibReport.ConfigFieldStr(this, "txtEstatus", valEstatus , string.Empty);
                LibReport.ConfigFieldStr(this, "txtCostoCalculadoAPartirDe", string.Empty, "FormaDeCalculoDelCosto");
                LibReport.ConfigFieldStr(this, "txtMoneda",                     string.Empty, "Moneda");
                LibReport.ConfigFieldDec(this, "txtCambio",                     string.Empty, "Cambio", "n" + 4, true, TextAlignment.Right);
                LibReport.ConfigFieldStr(this, "txtCodigoOrden",                string.Empty, "CodigoOrden");
                LibReport.ConfigFieldDate(this, "txtFechaInicio",               string.Empty, "FechaInicio", LibGalac.Aos.Base.Report.eDateOutputFormat.DateLong);
                LibReport.ConfigFieldDate(this, "txtFechaFinalizacion",         string.Empty, "FechaFinalizacion", LibGalac.Aos.Base.Report.eDateOutputFormat.DateLong);
                LibReport.ConfigFieldStr(this, "txtAlmacenMateriales",          string.Empty, "AlmacenInsumos");
                LibReport.ConfigFieldStr(this, "txtAlmacenProductoTerminado",   string.Empty, "AlmacenSalida");
                LibReport.ConfigFieldStr(this, "txtArticuloServicio",           string.Empty, "ArticuloInventarioInsumo");
				LibReport.ConfigFieldStr(this, "txtUnidades",                   string.Empty, "Unidad");
                LibReport.ConfigFieldDec(this, "txtCantidadEstimada",           string.Empty, "CantidadEstimada", "n" + 8, true, TextAlignment.Right);
                LibReport.ConfigFieldDec(this, "txtCantidadConsumida",          string.Empty, "CantidadConsumida", "n" + 8, true, TextAlignment.Right);
                LibReport.ConfigFieldDec(this, "txtCostoUnitarioMatServ",       string.Empty, "CostoUnitarioMatServ", "n" + 8, true, TextAlignment.Right);
                LibReport.ConfigFieldDec(this, "txtCostoTotalConsumido",        string.Empty, "MontoTotalConsumo", "n" + 8, true, TextAlignment.Right);
                LibReport.ConfigFieldStr(this, "txtObservaciones",              string.Empty, "Observaciones");
                LibReport.ConfigSummaryField(this, "txt_TCostoTotalConsumido", "MontoTotalConsumo", SummaryFunc.Sum, "GHDetalleCostoProduccionInsumos", SummaryRunning.Group, SummaryType.SubTotal, "n" + 8, "");

                LibReport.SetSubReportIfExists(this, SubRptListaDeSalida(valDataSalida), "srptListaDeSalida");
                LibReport.ConfigGroupHeader(this, "GHSecOrdenDeProduccion", "CodigoOrden", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.After);
               
                LibGraphPrnMargins.SetGeneralMargins(this, DataDynamics.ActiveReports.Document.PageOrientation.Landscape);
                LibReport.AddNoDataEvent(this);
                return true;
            }
            return false;
        }

        private ActiveReport SubRptListaDeSalida(DataTable valDataSourceSalida) {
            dsrDetalleCostoProducccionSalida vRpt = new dsrDetalleCostoProducccionSalida();
            vRpt.ConfigReport(valDataSourceSalida);
            return vRpt;
        }
        #endregion

        private void PageFooter_Format(object sender, EventArgs e) {
        }
    }
}

//    this.txt_TCostoTotalConsumido.Value = LibConvert.ToStr(SumaTotalConsumo, 8);
//    //if (LibString.S1IsEqualToS2(_MonedaDelInforme, _ListaMonedasDelReporte[0]) || LibString.S1IsEqualToS2(_MonedaDelInforme, _ListaMonedasDelReporte[1])) {
//    //    this.txtNotaMonedaCambio.Value = "Los montos est�n expresados en " + txtMoneda.Text;
//    //} else if (LibString.S1IsEqualToS2(_MonedaDelInforme, _ListaMonedasDelReporte[2]) || LibString.S1IsEqualToS2(_MonedaDelInforme, _ListaMonedasDelReporte[3])) {
//    //    int vPos = LibString.IndexOf(_MonedaDelInforme, "expresado en");
//    //    if (vPos > 0) {
//    //        string vPrimeraMoneda = LibString.Trim(LibString.SubString(_MonedaDelInforme, 0, vPos));
//    //        string vSegundaMoneda = LibString.SubString(_MonedaDelInforme, vPos + LibString.Len("expresado en "));
//    //        this.txtNotaMonedaCambio.Value = $"Los montos en {vPrimeraMoneda} est�n expresados en {vSegundaMoneda} a la tasa {LibConvert.NumToString(_TasaDeCambio, 4)}";
//    //    }
//    //}
