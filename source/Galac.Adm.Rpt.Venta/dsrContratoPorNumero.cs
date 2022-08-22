using System.Collections.Generic;
using DataDynamics.ActiveReports;
using System.Data;
using LibGalac.Aos.ARRpt;
using LibGalac.Aos.Base;
using LibGalac.Aos.DefGen;

namespace Galac.Adm.Rpt.Venta {
    /// <summary>
    /// Summary description for dsrContratoPorNumero.
    /// </summary>
    public partial class dsrContratoPorNumero : ActiveReport {
        #region Variables
        private bool _UseExternalRpx;
        private static string _RpxFileName;
        #endregion //Variables
        #region Constructores
        public dsrContratoPorNumero()
            : this(false, string.Empty) {
        }

        public dsrContratoPorNumero(bool initUseExternalRpx, string initRpxFileName) {
            InitializeComponent();
            _UseExternalRpx = initUseExternalRpx;
            if (_UseExternalRpx) {
                _RpxFileName = initRpxFileName;
            }
        }
        #endregion //Constructores
        #region Metodos Generados

        public string ReportTitle() {
            return "CONTRATO";
        }

        public bool ConfigReport(DataTable valDataSource, Dictionary<string, string> valParameters) {
            string valNombreDocFiscal = string.Empty;
            if (LibDefGen.ProgramInfo.IsCountryVenezuela()) {
                valNombreDocFiscal = " RIF ";
            } else if (LibDefGen.ProgramInfo.IsCountryEcuador() || LibDefGen.ProgramInfo.IsCountryPeru()) {
                valNombreDocFiscal = " RUC ";
            }
            string vNombreCompania = valParameters["NombreCompania"] + " -" + valNombreDocFiscal + valParameters["RifCompania"];
            if (_UseExternalRpx) {
                string vRpxPath = LibWorkPaths.PathOfRpxFile(_RpxFileName, ReportTitle(), false, LibDefGen.ProgramInfo.ProgramInitials);
                if (!LibString.IsNullOrEmpty(vRpxPath, true)) {
                    LibReport.LoadLayout(this, vRpxPath);
                }
            }
            if (LibReport.ConfigDataSource(this, valDataSource)) {
                LibReport.ConfigFieldStr(this, "txtNombreCompania", vNombreCompania, string.Empty);
                LibReport.ConfigLabel(this, "lblTituloInforme", ReportTitle());
                LibReport.ConfigLabel(this, "lblFechaYHoraDeEmision", LibReport.PromptEmittedOnDateAtHour);
                LibReport.ConfigHeader(this, "txtNombreCompania", "lblFechaYHoraDeEmision", "lblTituloInforme", "txtNroDePagina", "lblFechaInicialYFinal", LibGraphPrnSettings.PrintPageNumber, LibGraphPrnSettings.PrintEmitDate);

				LibReport.ConfigFieldStr(this, "txtNumeroContrato", string.Empty, "NumeroContrato");
				LibReport.ConfigFieldStr(this, "txtCodigoCliente", string.Empty, "CodigoCliente");
				LibReport.ConfigFieldStr(this, "txtNombreCliente", string.Empty, "NombreCliente");
                LibReport.ConfigFieldStr(this, "txtCodigoClienteAFacturar", string.Empty, "CodigoClienteAFacturar");
                LibReport.ConfigFieldStr(this, "txtNombreClienteAFacturar", string.Empty, "NombreClienteAFacturar");
				LibReport.ConfigFieldStr(this, "txtDuracionDelContrato", string.Empty, "Duracion");
				LibReport.ConfigFieldStr(this, "txtStatus", string.Empty, "StatusContrato");
                
				LibReport.ConfigFieldDate(this, "txtFechaInicio", string.Empty, "FechaDeInicio", "dd/MM/yyyy");
				LibReport.ConfigFieldDate(this, "txtFechaFinal", string.Empty, "FechaFinal", "dd/MM/yyyy");
                LibReport.ConfigFieldStr(this, "txtObservaciones", string.Empty, "Observaciones");

				LibReport.ConfigFieldStr(this, "txtArticulo", string.Empty, "Articulo");
				LibReport.ConfigFieldStr(this, "txtDescripcion", string.Empty, "Descripcion");
                LibReport.ConfigFieldDec(this, "txtCantidad", string.Empty, "Cantidad");
                LibReport.ConfigFieldDec(this, "txtPrecio", string.Empty, "Imponible");
                LibReport.ConfigFieldDec(this, "txtPorcentajeDescuento", string.Empty, "PorcentajeDescuento");
                LibReport.ConfigFieldDec(this, "txtTotalDetail", string.Empty, "TotalRenglon");

                LibReport.ConfigGroupHeader(this, "GHSecContrato", "", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.None);

                LibReport.ConfigSummaryField(this, "txtTotal", "TotalRenglon", SummaryFunc.Sum, "Detail", SummaryRunning.All, SummaryType.SubTotal);
                LibGraphPrnMargins.SetGeneralMargins(this, DataDynamics.ActiveReports.Document.PageOrientation.Portrait);
                return true;
            }
            return false;
        }
        #endregion //Metodos Generados

    } //End of class dsrContratoPorNumero

} //End of namespace Galac.Adm.Rpt.Venta
