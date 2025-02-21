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
using LibGalac.Aos.Base.Report;
using Galac.Saw.Ccl.Inventario;

namespace Galac.Saw.Rpt.Inventario
{
    /// <summary>
    /// Summary description for dsrNotaDeEntradaSalidaDeInventario.
    /// </summary>
    public partial class dsrNotaDeEntradaSalidaDeInventario : DataDynamics.ActiveReports.ActiveReport {
        #region Variables
        private bool _UseExternalRpx;
        private static string _RpxFileName;
        #endregion //Variables
        #region Constructores
        public dsrNotaDeEntradaSalidaDeInventario()
            : this(false, string.Empty) {
        }

        public dsrNotaDeEntradaSalidaDeInventario(bool initUseExternalRpx, string initRpxFileName) {
            InitializeComponent();
            _UseExternalRpx = initUseExternalRpx;
            if (_UseExternalRpx) {
                _RpxFileName = initRpxFileName;
            }
        }
        #endregion //Constructores
        #region Metodos Generados
        public string ReportTitle() {
            return "Nota de Entrada/Salida de Inventario";
        }

        public bool ConfigReport(DataTable valDataSource) {
            if (_UseExternalRpx) {
                string vRpxPath = LibWorkPaths.PathOfRpxFile(_RpxFileName, ReportTitle(), false, LibDefGen.ProgramInfo.ProgramInitials);//acá se indicaría si se busca en ULS, por defecto buscaría en app.path... Tip: Una función con otro nombre.
                if (!LibString.IsNullOrEmpty(vRpxPath, true)) {
                    LibReport.LoadLayout(this, vRpxPath);
                }
            }
            if (LibReport.ConfigDataSource(this, valDataSource)) {
                LibReport.ConfigFieldStr(this, "txtNombreCompania", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "Nombre"), string.Empty);
                LibReport.ConfigLabel(this, "lblTituloInforme", ReportTitle());
                //LibReport.ConfigLabel(this, "lblFechaInicialYFinal", valParameters["FechaInicialYFinal"]);
                LibReport.ConfigLabel(this, "lblFechaYHoraDeEmision", LibReport.PromptEmittedOnDateAtHour);
                LibReport.ConfigHeader(this, "txtNombreCompania", "lblFechaYHoraDeEmision", "lblTituloInforme", "txtNroDePagina", "lblFechaInicialYFinal", LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintPageNumber, LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintEmitDate);
                LibReport.ChangeControlVisibility(this, "lblFechaInicialYFinal", false);

				LibReport.ConfigFieldStr(this, "txtNumeroDocumento", string.Empty, "NumeroDocumento");
                LibReport.ConfigFieldDate(this, "txtFecha", string.Empty, "Fecha", eDateOutputFormat.DateLong);
                LibReport.ConfigFieldStr(this, "txtTipodeOperacion", string.Empty, "TipodeOperacionStr");
                LibReport.ConfigFieldStr(this, "txtStatusNotaEntradaSalida", string.Empty, "StatusNotaEntradaSalidaStr");
				LibReport.ConfigFieldStr(this, "txtAlmacen", string.Empty, "CodigoNombreAlmacen");
				LibReport.ConfigFieldStr(this, "txtComentarios", string.Empty, "Comentarios");
				LibReport.ConfigFieldStr(this, "txtCodigoArticulo", string.Empty, "CodigoArticulo");
				LibReport.ConfigFieldStr(this, "txtDescripcionArticulo", string.Empty, "DescripcionArticulo");
				LibReport.ConfigFieldDec(this, "txtCantidad", string.Empty, "Cantidad");
                LibReport.ConfigFieldStr(this, "txtTipoArticuloInv", string.Empty, "TipoArticuloInv");
                LibReport.ConfigFieldStr(this, "txtLoteDeInventario", string.Empty, "LoteDeInventario");
				LibReport.ConfigFieldDate(this, "txtFechaDeElaboracion", string.Empty, "FechaDeElaboracion", eDateOutputFormat.DateShort); 
				LibReport.ConfigFieldDate(this, "txtFechaDeVencimiento", string.Empty, "FechaDeVencimiento", eDateOutputFormat.DateShort);

				LibReport.ConfigGroupHeader(this, "GHSecNota", "NumeroDocumento", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.None);
				//LibReport.ConfigGroupHeader(this, "GHSecDetalle", "", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.None);

                LibGraphPrnMargins.SetGeneralMargins(this, PageOrientation.Portrait);
                return true;
            }
            return false;
        }

        #endregion //Metodos Generados

        private void Detail_Format(object sender, EventArgs e) {
            try {
                eTipoArticuloInv vTipoArtInv = (eTipoArticuloInv)LibConvert.DbValueToEnum(LibConvert.ToStr(this.txtTipoArticuloInv.Value));
                txtLoteDeInventario.Visible = vTipoArtInv == eTipoArticuloInv.Lote || vTipoArtInv == eTipoArticuloInv.LoteFechadeVencimiento;
                txtFechaDeElaboracion.Visible = vTipoArtInv == eTipoArticuloInv.LoteFechadeVencimiento;
                txtFechaDeVencimiento.Visible = vTipoArtInv == eTipoArticuloInv.LoteFechadeVencimiento;
            } catch (Exception) {
                throw;
            }
        }
    } //End of class dsrNotaDeEntradaSalidaDeInventario

} //End of namespace Galac.Dbo.Rpt.Inventario
