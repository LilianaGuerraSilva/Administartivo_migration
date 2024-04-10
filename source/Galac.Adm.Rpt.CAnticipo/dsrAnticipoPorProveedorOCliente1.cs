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
namespace Galac.Dbo.Rpt.CAnticipo {
    /// <summary>
    /// ESTE ARCHIVO NO ES PARA SER AGREGADO DIRECTO AL PROYECTO
    /// </summary>

    public partial class dsrAnticipoPorProveedorOCliente : DataDynamics.ActiveReports.ActiveReport {
        #region Variables
        private bool _UseExternalRpx;
        private static string _RpxFileName;
        #endregion //Variables
        #region Constructores
 LEE PROGRAMADOR: Deja que ActiveReports cree el designer correctamente, 
 LEE PROGRAMADOR: este archivo es solo para pasar lineas AFTER, no para tomarlo en su totalidad 
        public dsrAnticipoPorProveedorOCliente()
            : this(false, string.Empty) {
        }

        public dsrAnticipoPorProveedorOCliente(bool initUseExternalRpx, string initRpxFileName) {
            InitializeComponent();
            _UseExternalRpx = initUseExternalRpx;
            if (_UseExternalRpx) {
                _RpxFileName = initRpxFileName;
            }
        }
        #endregion //Constructores
        #region Metodos Generados

        public string ReportTitle() {
            return "Anticipos";
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

				LibReport.ConfigFieldStr(this, "txtNombreDeLaMoneda", string.Empty, "NombreDeLaMoneda");
				LibReport.ConfigFieldStr(this, "txtClienteProveedor", string.Empty, "ClienteProveedor");
				LibReport.ConfigFieldStr(this, "txtEstatus", string.Empty, "Estatus");
//				LibReport.ConfigFieldDate(this, "txtFecha", string.Empty, "Fecha", LibGalac.Aos.Base.Report.eDateOutputFormat.DateShort); esta sobrecarga no está en versión 5.0.2.0 de lib, temporalmente pasar formato directo
				LibReport.ConfigFieldDate(this, "txtFecha", string.Empty, "Fecha", "dd/MM/yyyy");
				LibReport.ConfigFieldStr(this, "txtNumero", string.Empty, "Numero");
				LibReport.ConfigFieldStr(this, "txtNumeroCheque", string.Empty, "NumeroCheque");
				LibReport.ConfigFieldDec(this, "txtMontoAnulado", string.Empty, "MontoAnulado");
				LibReport.ConfigFieldDec(this, "txtMontoTotal", string.Empty, "MontoTotal");
				LibReport.ConfigFieldDec(this, "txtMontoUsado", string.Empty, "MontoUsado");
				LibReport.ConfigFieldDec(this, "txtMontoDevuelto", string.Empty, "MontoDevuelto");
				LibReport.ConfigFieldDec(this, "txtDifDevolucion", string.Empty, "DifDevolucion");


				LibReport.ConfigGroupHeader(this, "GHMoneda", "", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.None);
				LibReport.ConfigGroupHeader(this, "GHCodigoClienteProveedor", "", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.None);

                LibGraphPrnMargins.SetGeneralMargins(this, DataDynamics.ActiveReports.Document.PageOrientation.Portrait);
                return true;
            }
            return false;
        }
        #endregion //Metodos Generados


    } //End of class dsrAnticipoPorProveedorOCliente

} //End of namespace Galac.Dbo.Rpt.CAnticipo

