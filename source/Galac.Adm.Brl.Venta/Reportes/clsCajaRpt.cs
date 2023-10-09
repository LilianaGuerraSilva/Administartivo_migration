using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Report;
using Galac.Adm.Ccl.Venta;
using System.Xml.Linq;
using LibGalac.Aos.Base;

namespace Galac.Adm.Brl.Venta.Reportes {

    public class clsCajaRpt: ILibReportInfo, ICajaInformes {
        #region Variables
        private Dictionary<string, Dictionary<string, string>> _PropertiesForReportList;
        #endregion //Variables
        #region Propiedades
        Dictionary<string, Dictionary<string, string>> ILibReportInfo.PropertiesForReportList {
            get { return _PropertiesForReportList; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsCajaRpt() {
            _PropertiesForReportList = new Dictionary<string, Dictionary<string, string>>();
            _PropertiesForReportList.Add("Caja", CajaInfo());
        }
        #endregion //Constructores
        #region Metodos Generados

        private Dictionary<string, string> CajaInfo() {
            Dictionary<string, string> vResult = new Dictionary<string, string>();
            vResult.Add("SpSearchName", "Dbo.Gp_CajaSCH");
            vResult.Add("ConfigKeyForDbService", string.Empty);
            return vResult;
        }

        System.Data.DataTable ICajaInformes.BuildCuadreCajaCobroMultimonedaDetallado(int valConsecutivoCompania, DateTime valFechaInicial, DateTime valFechaFinal, Saw.Lib.eCantidadAImprimir valCantidadOperadorDeReporte, string valNombreDelOperador, Saw.Lib.eMonedaParaImpresion valMonedaDeReporte, bool valTotalesTipoCobro) {
            string vSql = "";
            clsCajaSql insCajaSql = new clsCajaSql();
            LibGalac.Aos.Base.ILibDataRpt insCuadreCajaCobroMultimonedaDetallado = new Galac.Adm.Dal.Venta.clsCajaDat();
            vSql = insCajaSql.SqlCuadreCajaCobroMultimonedaDetallado(valConsecutivoCompania, valFechaInicial, valFechaFinal, valCantidadOperadorDeReporte, valNombreDelOperador, valMonedaDeReporte, valTotalesTipoCobro);
            return insCuadreCajaCobroMultimonedaDetallado.GetDt(vSql, 0);
        }

        System.Data.DataTable ICajaInformes.BuildCajasAperturadas(int valConsecutivoCompania) {
            string vSql = "";
            clsCajaSql insCajaSql = new clsCajaSql();
            LibGalac.Aos.Base.ILibDataRpt insCajasAperturadas = new Galac.Adm.Dal.Venta.clsCajaDat();
            vSql = insCajaSql.SqlCajasAperturadas(valConsecutivoCompania);
            return insCajasAperturadas.GetDt(vSql, 0);
        }

        System.Data.DataTable ICajaInformes.BuildCuadreCajaPorTipoCobro(int valConsecutivoCompania, DateTime valFechaInicial, DateTime valFechaFinal, Saw.Lib.eMonedaParaImpresion valMonedaDeReporte, Saw.Lib.eTipoDeInforme valTipoDeInforme) {
            string vSql = "";
            clsCajaSql insCajaSql = new clsCajaSql();
            LibGalac.Aos.Base.ILibDataRpt insCuadreCajaPorTipoCobro = new Galac.Adm.Dal.Venta.clsCajaDat();
            vSql = insCajaSql.SqlCuadreCajaPorTipoCobro(valConsecutivoCompania, valFechaInicial, valFechaFinal, valMonedaDeReporte, valTipoDeInforme);
            return insCuadreCajaPorTipoCobro.GetDt(vSql, 0);
        }

        System.Data.DataTable ICajaInformes.BuildCuadreCajaPorTipoCobroYUsuario(int valConsecutivoCompania, DateTime valFechaInicial, DateTime valFechaFinal, Saw.Lib.eCantidadAImprimir valCantidadOperadorDeReporte, string valNombreDelOperador, Saw.Lib.eMonedaParaImpresion valMonedaDeReporte) {
            string vSql = "";
            clsCajaSql insCajaSql = new clsCajaSql();
            LibGalac.Aos.Base.ILibDataRpt insCuadreCajaPorTipoCobroYUsuario = new Galac.Adm.Dal.Venta.clsCajaDat();
            vSql = insCajaSql.SqlCuadreCajaPorTipoCobroYUsuario(valConsecutivoCompania, valFechaInicial, valFechaFinal, valCantidadOperadorDeReporte, valNombreDelOperador, valMonedaDeReporte);
            return insCuadreCajaPorTipoCobroYUsuario.GetDt(vSql, 0);
        }

        System.Data.DataTable ICajaInformes.BuildCuadreCajaConDetalleFormaPago(int valConsecutivoCompania, DateTime valFechaInicial, DateTime valFechaFinal, Saw.Lib.eMonedaParaImpresion valMonedaDeReporte, Saw.Lib.eTipoDeInforme valTipoDeInforme, bool valTotalesTipoPago) {
            string vSql = "";
            clsCajaSql insCajaSql = new clsCajaSql();
            LibGalac.Aos.Base.ILibDataRpt insCuadreCajaConDetalleFormaPago = new Galac.Adm.Dal.Venta.clsCajaDat();
            if (valTipoDeInforme == Saw.Lib.eTipoDeInforme.Detallado) {
                vSql = insCajaSql.SqlCuadreCajaConDetalleFormaPago(valConsecutivoCompania, valFechaInicial, valFechaFinal, valMonedaDeReporte, valTotalesTipoPago);
            } else {
                vSql = insCajaSql.SqlCuadreCajaConDetalleFormaPagoResumido(valConsecutivoCompania, valFechaInicial, valFechaFinal, valMonedaDeReporte, valTotalesTipoPago);
            }
            return insCuadreCajaConDetalleFormaPago.GetDt(vSql, 0);
        }

        System.Data.DataTable ICajaInformes.BuildCuadreCajaPorUsuario(int valConsecutivoCompania, DateTime valFechaInicial, DateTime valFechaFinal, Saw.Lib.eTipoDeInforme valTipoDeInforme, Saw.Lib.eMonedaParaImpresion valMonedaDeReporte, Saw.Lib.eCantidadAImprimir valCantidadOperadorDeReporte, string valNombreDelOperador) {
            string vSql = "";
            clsCajaSql insCajaSql = new clsCajaSql();
            LibGalac.Aos.Base.ILibDataRpt insCuadreCajaPorUsuario = new Galac.Adm.Dal.Venta.clsCajaDat();
            vSql = insCajaSql.SqlCuadreCajaPorUsuario(valConsecutivoCompania, valFechaInicial, valFechaFinal, valTipoDeInforme, valMonedaDeReporte, valCantidadOperadorDeReporte, valNombreDelOperador);
            return insCuadreCajaPorUsuario.GetDt(vSql, 0);
        }

        System.Data.DataTable ICajaInformes.BuildCajaCerrada(int valConsecutivoCompania, int valConsecutivoCaja, DateTime valFechaDesde, DateTime valFechaHasta, ref decimal refEfectivoEnCaja, ref decimal refEfectivoEnCajaME) {
            string vSql = "";
            clsCajaSql insCajaAperturaSql = new clsCajaSql();
            LibGalac.Aos.Base.ILibDataRpt insCajaCerrada = new Galac.Adm.Dal.Venta.clsCajaDat();
            vSql = insCajaAperturaSql.SqlCajaCerrada(valConsecutivoCompania, valConsecutivoCompania, valFechaDesde, valFechaHasta);
			GetEfectivoEnCaja(valConsecutivoCompania, valConsecutivoCaja, valFechaDesde, valFechaHasta, ref refEfectivoEnCaja, ref refEfectivoEnCajaME);
            return insCajaCerrada.GetDt(vSql, 0);
        }

        private void GetEfectivoEnCaja(int valConsecutivoCompania, int valConsecutivoCaja, DateTime valFechaDesde, DateTime valFechaHasta, ref decimal refEfectivoEnCaja, ref decimal refEfectivoEnCajaME) {
            clsCajaSql insCajaAperturaSql = new clsCajaSql();
            string vSql = insCajaAperturaSql.SqlEfectivoEnCajaCerrada(valConsecutivoCompania, valConsecutivoCaja, valFechaDesde, valFechaHasta);
            XElement vData = LibGalac.Aos.Brl.LibBusiness.ExecuteSelect(vSql, null, "", 0);
            if (vData != null && vData.HasElements) {
                refEfectivoEnCaja = LibConvert.ToDec(LibXml.GetPropertyString(vData, "EfectivoEnCaja"));
                refEfectivoEnCajaME = LibConvert.ToDec(LibXml.GetPropertyString(vData, "EfectivoEnCajaME"));
            }
        }
        #endregion //Metodos Generados
    } //End of class clsCajaRpt
} //End of namespace Galac.Adm.Brl.Venta

