using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DataDynamics.ActiveReports;
using DataDynamics.ActiveReports.Document;

using LibGalac.Aos.ARRpt;
using LibGalac.Aos.Base;
using LibGalac.Aos.DefGen;
using System.Data;
using Galac.Saw.Lib;

namespace Galac.Adm.Rpt.Venta {
    /// <summary>
    /// Summary description for dsrNotaDeEntregaEntreFechasPorClienteDetallado.
    /// </summary>
    public partial class dsrNotaDeEntregaEntreFechasPorClienteDetallado : DataDynamics.ActiveReports.ActiveReport {
        #region Variables
        private bool _UseExternalRpx;
        private static string _RpxFileName;
        decimal mTotalMontoTotal;
        decimal mTotalMontoPorCliente;
        #endregion //Variables
        #region Constructores
        public dsrNotaDeEntregaEntreFechasPorClienteDetallado()
            : this(false, string.Empty) {
        }
        //
        public dsrNotaDeEntregaEntreFechasPorClienteDetallado(bool initUseExternalRpx, string initRpxFileName) {
            InitializeComponent();
            _UseExternalRpx = initUseExternalRpx;
            if (_UseExternalRpx) {
                _RpxFileName = initRpxFileName;
            }
        }
        #endregion //Constructores
        #region Metodos Generados

        public string ReportTitle() {
            return "Notas de Entrega por Cliente";
        }

        public bool ConfigReport(DataTable valDataSource, Dictionary<string, string> valParameters) {
            mTotalMontoTotal = 0;
            mTotalMontoPorCliente = 0;
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

                LibReport.ConfigFieldStr(this, "txtMoneda", string.Empty, "Moneda");
                LibReport.ConfigFieldStr(this, "txtCodigo", string.Empty, "CodigoCliente");
                LibReport.ConfigFieldStr(this, "txtNombre", string.Empty, "Cliente");
                LibReport.ConfigFieldDate(this, "txtFecha", string.Empty, "Fecha", "dd/MM/yy");
                LibReport.ConfigFieldStr(this, "txtNroDocumento", string.Empty, "Numero");
                LibReport.ConfigFieldStr(this, "txtMonedaDoc", string.Empty, "MonedaDoc");
                LibReport.ConfigFieldDec(this, "txtCambio", string.Empty, "Cambio", "#,###.0000", false, TextAlignment.Right);
                LibReport.ConfigFieldDec(this, "txtMontoTotal", string.Empty, "TotalFactura");

                LibReport.ConfigFieldStr(this, "txtCodigoArticulo", string.Empty, "CodigoArticulo");
                LibReport.ConfigFieldStr(this, "txtDescripcionArticulo", string.Empty, "Descripcion");
                LibReport.ConfigFieldDec(this, "txtCantidad", string.Empty, "Cantidad");
                LibReport.ConfigFieldDec(this, "txtPrecio", String.Empty, "Precio");
                LibReport.ConfigFieldDec(this, "txtTotalRenglon", String.Empty, "TotalRenglon");


                LibReport.ConfigFieldStr(this, "txtTotalCodigoCliente", string.Empty, "CodigoCliente");
                LibReport.ConfigFieldStr(this, "txtMonedaReporte", string.Empty, "Moneda");
                LibReport.ConfigGroupHeader(this, "GHSecMoneda", "Moneda", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.None);
                LibReport.ConfigGroupHeader(this, "GHCliente", "CodigoCliente", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.None);
                LibReport.ConfigGroupHeader(this, "GHSecDocumento", "Numero", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.None);

                LibReport.ConfigFieldStr(this, "txtNotaMonedaCambio", "", "");
                LibReport.ChangeControlVisibility(this, "txtNotaMonedaCambio", false);

                LibGraphPrnMargins.SetGeneralMargins(this, DataDynamics.ActiveReports.Document.PageOrientation.Portrait);
                return true;
            }
            return false;
        }
        #endregion //Metodos Generados

        private void GHSecDocumento_Format(object sender, EventArgs e) {
            try {
                mTotalMontoTotal += LibConvert.ToDec(txtMontoTotal.Value);
                mTotalMontoPorCliente += LibConvert.ToDec(txtMontoTotal.Value);
            } catch (Exception) {
                throw;
            }
        }

        private void GFSecMoneda_Format(object sender, EventArgs e) {
            try {
                txtTotalMontoTotal.Text = LibConvert.NumToString(mTotalMontoTotal, 2);
            } catch (Exception) {
                throw;
            }
        }

        private void GHSecMoneda_Format(object sender, EventArgs e) {
            try {
                mTotalMontoTotal = 0;
            } catch (Exception) {
                throw;
            }
        }

        private void GHCliente_Format(object sender, EventArgs e) {
            try {
                mTotalMontoPorCliente = 0;
            } catch (Exception) {
                throw;
            }
        }

        private void GFCliente_Format(object sender, EventArgs e) {
            try {
                txtTotalMontoCliente.Text = LibConvert.NumToString(mTotalMontoPorCliente, 2);
            } catch (Exception) {
                throw;
            }
        }
    } //End of class dsrNotaDeEntregaEntreFechasPorClienteDetallado

} //End of namespace Galac.Dbo.Rpt.ComponenteNoEspecificado
