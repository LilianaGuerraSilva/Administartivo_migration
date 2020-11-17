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

namespace Galac.Adm.Rpt.Venta {
    /// <summary>
    /// Summary description for dsrCajasAperturadas.
    /// </summary>
    public partial class dsrCajasAperturadas : DataDynamics.ActiveReports.ActiveReport {
        #region Variables
        private bool _UseExternalRpx;
        private static string _RpxFileName;
        #endregion //Variables
        #region Constructores
 //LEE PROGRAMADOR: Deja que ActiveReports cree el designer correctamente, 
 //LEE PROGRAMADOR: este archivo es solo para pasar lineas AFTER, no para tomarlo en su totalidad 
        public dsrCajasAperturadas()
            : this(false, string.Empty) {
        }

        public dsrCajasAperturadas(bool initUseExternalRpx, string initRpxFileName) {
            InitializeComponent();
            _UseExternalRpx = initUseExternalRpx;
            if (_UseExternalRpx) {
                _RpxFileName = initRpxFileName;
            }
        }
        #endregion //Constructores
        #region Metodos Generados

        public string ReportTitle() {
            return "Informe de cajas abiertas";
        }

        public bool ConfigReport(DataTable valDataSource, Dictionary<string, string> valParameters) {
            string vNombreCompania = valParameters["NombreCompania"] + " - " + valParameters["RifCompania"];
            if (_UseExternalRpx) {
                string vRpxPath = LibWorkPaths.PathOfRpxFile(_RpxFileName, ReportTitle(), false, LibDefGen.ProgramInfo.ProgramInitials);//acá se indicaría si se busca en ULS, por defecto buscaría en app.path... Tip: Una función con otro nombre.
                if (!LibString.IsNullOrEmpty(vRpxPath, true)) {
                    LibReport.LoadLayout(this, vRpxPath);
                }
            }
            if (LibReport.ConfigDataSource(this, valDataSource)) {
                LibReport.ConfigFieldStr(this, "txtNombreCompania", vNombreCompania, string.Empty);
                LibReport.ConfigLabel(this, "lblTituloInforme", ReportTitle());
                //LibReport.ConfigLabel(this, "lblFechaInicialYFinal", valParameters["FechaInicialYFinal"]);
                LibReport.ConfigLabel(this, "lblFechaInicialYFinal", string.Empty);
                LibReport.ConfigLabel(this, "lblFechaYHoraDeEmision", LibReport.PromptEmittedOnDateAtHour);
                LibReport.ConfigHeader(this, "txtNombreCompania", "lblFechaYHoraDeEmision", "lblTituloInforme", "txtNroDePagina", "lblFechaInicialYFinal", LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintPageNumber, LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintEmitDate);
				LibReport.ConfigFieldDate(this, "txtFechaDeApertura", string.Empty, "FechaDeApertura", "dd/MM/yyyy");
                LibReport.ConfigFieldStr(this, "txtHoraApertura", string.Empty, "HoraApertura");
                LibReport.ConfigFieldInt(this, "txtConsecutivoCaja", string.Empty, "ConsecutivoCaja");
				LibReport.ConfigFieldStr(this, "txtNombreDelUsuario", string.Empty, "NombreDelUsuario");
                LibReport.ConfigFieldStr(this, "txtNombreCaja", string.Empty, "NombreCaja");
                LibReport.ConfigFieldDecWithNDecimal(this, "txtMontoApertura", string.Empty, "MontoApertura", 2);
                LibGraphPrnMargins.SetGeneralMargins(this, DataDynamics.ActiveReports.Document.PageOrientation.Landscape);
                return true;
            }
            return false;
        }
        #endregion //Metodos Generados


    } //End of class dsrCajasAperturadas

} //End of namespace Galac.Adm.Rpt.Venta
