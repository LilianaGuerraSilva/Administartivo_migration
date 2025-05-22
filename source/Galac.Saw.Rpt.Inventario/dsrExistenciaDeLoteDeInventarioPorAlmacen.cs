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
using System.Windows.Forms;
using Galac.Saw.Ccl.Inventario;

namespace Galac.Saw.Rpt.Inventario {
    /// <summary>
    /// Summary description for NewActiveReport1.
    /// </summary>
    public partial class dsrExistenciaDeLoteDeInventarioPorAlmacen : DataDynamics.ActiveReports.ActiveReport {
        #region Variables
        private bool _UseExternalRpx;
        private static string _RpxFileName;
        #endregion //Variables
        #region Constructores
        public dsrExistenciaDeLoteDeInventarioPorAlmacen()
            : this(false, string.Empty) {
        }
            //
        public dsrExistenciaDeLoteDeInventarioPorAlmacen(bool initUseExternalRpx, string initRpxFileName) {
            //
            InitializeComponent();
            _UseExternalRpx = initUseExternalRpx;
            if (_UseExternalRpx) {
                _RpxFileName = initRpxFileName;
            }
        }
        #endregion //Constructores

        #region Metodos Generados
        public string ReportTitle() {
            return "Existencia de Lote de Inventario por Almacén";
        }

        #endregion
        #region Metodos Generados

        public bool ConfigReport(DataTable valDataSource, Dictionary<string, string> valParameters, bool valEsSoloUnLote) {
            var TipoArticuloInv = valDataSource.Rows[0]["TipoArticuloInv"].ToString();
            byte vDecimalDigits = LibConvert.ToByte(valParameters["DecimalesParaReporte"]);
            if (_UseExternalRpx) {
                string vRpxPath = LibWorkPaths.PathOfRpxFile(_RpxFileName, ReportTitle(), false, LibDefGen.ProgramInfo.ProgramInitials);
                if (!LibString.IsNullOrEmpty(vRpxPath, true)) {
                    LibReport.LoadLayout(this, vRpxPath); }
            }
            if (LibReport.ConfigDataSource(this, valDataSource)) {
                LibReport.ConfigFieldStr(this, "txtNombreCompania", valParameters["NombreCompania"], string.Empty);
                LibReport.ConfigLabel(this, "lblTituloInforme", ReportTitle());
                LibReport.ConfigLabel(this, "lblFechaInicialYFinal", "");
                LibReport.ConfigLabel(this, "lblFechaYHoraDeEmision", LibReport.PromptEmittedOnDateAtHour);
                LibReport.ConfigHeader(this, "txtNombreCompania", "lblFechaYHoraDeEmision", "lblTituloInforme", "txtNroDePagina", "lblFechaInicialYFinal", LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintPageNumber, LibGalac.Aos.ARRpt.LibGraphPrnSettings.PrintEmitDate);
                LibReport.ConfigFieldStr(this, "txtArticulo", string.Empty, "Articulo");
                LibReport.ConfigFieldStr(this, "txtCodigoArticulo", string.Empty, "CodigoArticulo");
                LibReport.ConfigFieldStr(this, "txtCodigoAlmacen", string.Empty, "CodigoAlmacen");
                LibReport.ConfigFieldStr(this, "txtNombreAlmacen", string.Empty, "NombreAlmacen");
                LibReport.ConfigFieldDec(this, "txtEntrada", string.Empty, "Existencia");
                LibReport.ConfigFieldDecWithNDecimal(this, "txtEntrada", string.Empty, "Existencia", vDecimalDigits);
                LibReport.ConfigGroupHeader(this, "GHsecArticulo", "", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.None);
                if (valEsSoloUnLote) {
                    LibReport.ConfigFieldStr(this, "txtLote", string.Empty, "Lote");
                    if ((eTipoArticuloInv)LibConvert.DbValueToEnum(TipoArticuloInv) == eTipoArticuloInv.Lote) {
                        LibReport.ChangeControlVisibility(this, "lblFechaElaboracion", false);
                        LibReport.ChangeControlVisibility(this, "txtFechaElaboracion", false);
                        LibReport.ChangeControlVisibility(this, "lblFechaVencimiento", false);
                        LibReport.ChangeControlVisibility(this, "txtFechaVencimiento", false);
                    } else if ((eTipoArticuloInv)LibConvert.DbValueToEnum(TipoArticuloInv) == eTipoArticuloInv.LoteFechadeElaboracion) {
                        LibReport.ChangeControlVisibility(this, "lblFechaVencimiento", false);
                        LibReport.ChangeControlVisibility(this, "txtFechaVencimiento", false);
                        LibReport.ConfigFieldDate(this, "txtFechaElaboracion", string.Empty, "FechaElaboracion", "dd/MM/yyyy");
                    } else if ((eTipoArticuloInv)LibConvert.DbValueToEnum(TipoArticuloInv) == eTipoArticuloInv.LoteFechadeVencimiento) {
                        LibReport.ConfigFieldDate(this, "txtFechaVencimiento", string.Empty, "FechaVencimiento", "dd/MM/yyyy");
                        LibReport.ConfigFieldDate(this, "txtFechaElaboracion", string.Empty, "FechaElaboracion", "dd/MM/yyyy");
                    }
                    lblNombreAlmacen.Width += lblNombreLote.Width + lblFechaElab.Width + lblFechaVenc.Width;
                    txtNombreAlmacen.Width += txtNombreLote.Width + txtFechaElab.Width + txtFechaVenc.Width;
                    LibReport.ChangeControlVisibility(this, "lblNombreLote", false);
                    LibReport.ChangeControlVisibility(this, "lblFechaElab", false);
                    LibReport.ChangeControlVisibility(this, "lblFechaVenc", false);
                    LibReport.ChangeControlVisibility(this, "txtNombreLote", false);
                    LibReport.ChangeControlVisibility(this, "txtFechaElab", false);
                    LibReport.ChangeControlVisibility(this, "txtFechaVenc", false);
                } else {
                    LibReport.ChangeControlVisibility(this, "lblLote", false);
                    LibReport.ChangeControlVisibility(this, "lblFechaElaboracion", false);
                    LibReport.ChangeControlVisibility(this, "lblFechaVencimiento", false);
                    LibReport.ChangeControlVisibility(this, "txtLote", false);
                    LibReport.ChangeControlVisibility(this, "txtFechaElaboracion", false);
                    LibReport.ChangeControlVisibility(this, "txtFechaVencimiento", false);
                    LibReport.ConfigFieldStr(this, "txtNombreLote", string.Empty, "Lote");
                    if ((eTipoArticuloInv)LibConvert.DbValueToEnum(TipoArticuloInv) == eTipoArticuloInv.Lote) {
                        LibReport.ChangeControlVisibility(this, "lblFechaElab", false);
                        LibReport.ChangeControlVisibility(this, "lblFechaVenc", false);
                        LibReport.ChangeControlVisibility(this, "txtFechaElab", false);
                        LibReport.ChangeControlVisibility(this, "txtFechaVenc", false);
                        lblNombreLote.Width += lblFechaElab.Width + lblFechaVenc.Width;
                        txtNombreLote.Width += txtFechaElab.Width + txtFechaVenc.Width;
                    } else if ((eTipoArticuloInv)LibConvert.DbValueToEnum(TipoArticuloInv) == eTipoArticuloInv.LoteFechadeElaboracion) {
                        LibReport.ConfigFieldDate(this, "txtFechaElab", string.Empty, "FechaElaboracion", "dd/MM/yyyy");
                        LibReport.ChangeControlVisibility(this, "lblFechaVenc", false);
                        LibReport.ChangeControlVisibility(this, "txtFechaVenc", false);
                        LibReport.ConfigFieldDate(this, "txtFechaElab", string.Empty, "FechaElaboracion", "dd/MM/yyyy");
                        lblNombreLote.Width += lblFechaElab.Width;
                        lblFechaElab.Left = lblFechaVenc.Left;
                        txtNombreLote.Width += txtFechaElab.Width;
                        txtFechaElab.Left = txtFechaVenc.Left;
                    } else if ((eTipoArticuloInv)LibConvert.DbValueToEnum(TipoArticuloInv) == eTipoArticuloInv.LoteFechadeVencimiento) {
                        LibReport.ConfigFieldDate(this, "txtFechaElab", string.Empty, "FechaElaboracion", "dd/MM/yyyy");
                        LibReport.ConfigFieldDate(this, "txtFechaVenc", string.Empty, "FechaVencimiento", "dd/MM/yyyy");
                    }
                }
                LibReport.ConfigGroupHeader(this, "GHsecLote", "", GroupKeepTogether.FirstDetail, RepeatStyle.OnPage, true, NewPage.After);
                LibReport.ConfigSummaryField(this, "txtExistenciaFinal", "Existencia", SummaryFunc.Sum, "GFsecArticulo", SummaryRunning.Group, SummaryType.GrandTotal);
                LibReport.ConfigFieldDecWithNDecimal(this, "txtExistenciaFinal", string.Empty, "Existencia", vDecimalDigits);
                LibGraphPrnMargins.SetGeneralMargins(this, DataDynamics.ActiveReports.Document.PageOrientation.Portrait);
                return true;
            }
            return false;
        }

        #endregion //Metodos Generados
    } //End of class dsrExistenciaDeLoteDeInventarioPorAlmacen
} //End of namespace Galac.Saw.Rpt.Inventario
