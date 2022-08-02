using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DataDynamics.ActiveReports;
using System.Data;
using LibGalac.Aos.ARRpt;
using LibGalac.Aos.Base;
using LibGalac.Aos.DefGen;

namespace Galac.Adm.Rpt.Venta {
    /// <summary>
    /// Summary description for dsrContratoEntreFechas.
    /// </summary>
    public partial class dsrContratoEntreFechas : DataDynamics.ActiveReports.ActiveReport {
        #region Variables
        private bool _UseExternalRpx;
        private static string _RpxFileName;
        #endregion //Variables
        #region Constructores
        public dsrContratoEntreFechas()	: this(false, string.Empty) {
        }
        public dsrContratoEntreFechas(bool initUseExternalRpx, string initRpxFileName) {

            InitializeComponent();
            _UseExternalRpx = initUseExternalRpx;
            if (_UseExternalRpx) {
                _RpxFileName = initRpxFileName;
            }
        }
        #endregion //Constructores
        #region Metodos Generados

        public string ReportTitle() {
            return "Contrato entre Fechas";
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
                string vRpxPath = LibWorkPaths.PathOfRpxFile(_RpxFileName, ReportTitle(), false, LibDefGen.ProgramInfo.ProgramInitials);//ac� se indicar�a si se busca en ULS, por defecto buscar�a en app.path... Tip: Una funci�n con otro nombre.
                if (!LibString.IsNullOrEmpty(vRpxPath, true)) {
                    LibReport.LoadLayout(this, vRpxPath);
                }
            }
            if (LibReport.ConfigDataSource(this, valDataSource)) {
                LibReport.ConfigFieldStr(this, "txtNombreCompania", vNombreCompania, string.Empty);
                LibReport.ConfigLabel(this, "lblTituloInforme", ReportTitle());
                LibReport.ConfigLabel(this, "lblFechaInicialYFinal", valParameters["FechaInicialYFinal"]);
                LibReport.ConfigLabel(this, "lblFechaYHoraDeEmision", LibReport.PromptEmittedOnDateAtHour);
                LibReport.ConfigHeader(this, "txtNombreCompania", "lblFechaYHoraDeEmision", "lblTituloInforme", "txtNroDePagina", "lblFechaInicialYFinal", LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintPageNumber, LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintEmitDate);

                LibReport.ConfigFieldStr(this, "txtStatus", string.Empty, "Status");
                LibReport.ConfigFieldStr(this, "txtNumeroContrato", string.Empty, "NumeroContrato");
                LibReport.ConfigFieldStr(this, "txtCodigoCliente", string.Empty, "CodigoCliente");
                LibReport.ConfigFieldStr(this, "txtNombreCliente", string.Empty, "NombreCliente");
                LibReport.ConfigFieldStr(this, "txtCodigoClienteAFacturar", string.Empty, "CodigoClienteAFacturar");
                LibReport.ConfigFieldStr(this, "txtNombreClienteAFacturar", string.Empty, "NombreClienteAFacturar");
                LibReport.ConfigFieldStr(this, "txtDuracionDelContrato", string.Empty, "Duracion");
                LibReport.ConfigFieldDate(this, "txtFechaDeInicio", string.Empty, "FechaDeInicio", "dd/MM/yyyy");
				LibReport.ConfigFieldDate(this, "txtFechaFinal", string.Empty, "FechaFinal", "dd/MM/yyyy");
				LibReport.ConfigFieldStr(this, "txtObservaciones", string.Empty, "Observaciones");

                LibReport.ConfigGroupHeader(this, "GHSecContrato", "Status", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.After);

                LibGraphPrnMargins.SetGeneralMargins(this, DataDynamics.ActiveReports.Document.PageOrientation.Landscape);
                return true;
            }
            return false;
        }
        #endregion //Metodos Generados

    } //End of class dsrContratoEntreFechas

} //End of namespace Galac.Dbo.Rpt.Venta
