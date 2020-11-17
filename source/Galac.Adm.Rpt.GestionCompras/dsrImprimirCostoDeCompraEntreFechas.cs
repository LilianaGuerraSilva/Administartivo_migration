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
namespace Galac.Adm.Rpt.GestionCompras {
    /// <summary>
    /// Summary description for NewActiveReport1.
    /// </summary>
    public partial class dsrImprimirCostoDeCompraEntreFechas : DataDynamics.ActiveReports.ActiveReport {
        #region Variables
        private bool _UseExternalRpx;
        private static string _RpxFileName;
        #endregion //Variables
        #region Constructores
        public dsrImprimirCostoDeCompraEntreFechas()
            : this(false, string.Empty) {
        }

        public dsrImprimirCostoDeCompraEntreFechas(bool initUseExternalRpx, string initRpxFileName) {
            InitializeComponent();
            _UseExternalRpx = initUseExternalRpx;
            if (_UseExternalRpx) {
                _RpxFileName = initRpxFileName;
            }
        }
        #endregion //Constructores
        #region Metodos Generados

        public string ReportTitle() {
            return "Costo de Compra Entre Fechas";
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
                LibReport.ConfigFieldStr(this, "txtCodigoArticulo", string.Empty, "CodigoArticulo");
                LibReport.ConfigFieldStr(this, "txtDescripcionDelArticulo", string.Empty, "Descripcion");
                LibReport.ConfigFieldStr(this, "txtLineaDeProducto", string.Empty, "LineaDeProducto");
                LibReport.ConfigFieldDec(this, "txtCantidadTotalDelArticulo", string.Empty, "CantArtComp","0.00#",false,TextAlignment.Right);
                LibReport.ConfigFieldStr(this, "txtVecesComp", string.Empty, "VecesComprados");
                LibReport.ConfigFieldDec(this, "txtPromedioPonderadoCompEntreFeha", string.Empty, "PromPonDeComp", "0.00#", false, TextAlignment.Right);
                LibReport.ConfigFieldStr(this, "txtMoneda", string.Empty, "Moneda");
                LibReport.ConfigFieldDec(this, "txtMaximoCostoDeCompra", string.Empty, "CostoMaximo","0.00#", false, TextAlignment.Center);
                LibReport.ConfigFieldDec(this, "txtMinimoCostoDeCompra", string.Empty, "CostoMinimo","0.00#", false, TextAlignment.Right);
                LibReport.ConfigFieldDec(this, "txtMontoTotal", string.Empty, "TotalCompXArt", "0.00#", false, TextAlignment.Right);
                LibReport.ConfigFieldDate(this, "txtFechaDeLaUltimaCompra", string.Empty, "FechaUltCompra", "dd/MM/yyyy");
                LibReport.ConfigGroupHeader(this, "GHLineaDeProducto", "LineaDeProducto", GroupKeepTogether.All, RepeatStyle.All, true, NewPage.None);
                LibReport.ConfigGroupHeader(this, "GHMoneda", "Moneda", GroupKeepTogether.All, RepeatStyle.All, true, NewPage.None);
                LibReport.ConfigSummaryField(this, "txtMontoTotalPorGrupo", "TotalCompXArt", SummaryFunc.Sum, "GHLineaDeProducto", SummaryRunning.All, SummaryType.SubTotal);
                LibReport.ConfigSummaryField(this, "txtMontoTotalDelReporte", "TotalCompXArt", SummaryFunc.Sum, "GHMoneda", SummaryRunning.All, SummaryType.SubTotal);
                LibReport.AddNoDataEvent(this);
                LibGraphPrnMargins.SetGeneralMargins(this, DataDynamics.ActiveReports.Document.PageOrientation.Portrait);
                return true;
            }
            return false;
        }
        #endregion //Metodos Generados


    } //End of class dsrImprimirCostoDeCompraEntreFechas

} //End of namespace Galac.Dbo.Rpt.ComponenteNoEspecificado
