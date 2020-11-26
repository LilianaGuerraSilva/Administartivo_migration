using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using Galac.Adm.Ccl.Venta;
using Galac.Saw.Ccl.SttDef;
using LibGalac.Aos.DefGen;

namespace Galac.Adm.Brl.Venta.Reportes {
    public class clsCobranzaSql {
		
		const string NewLine = "\r\n";

        QAdvSql vSqlUtil;
        Saw.Lib.clsLibSaw _LibSaw;
        #region Metodos Generados        

        public clsCobranzaSql() {
            vSqlUtil = new QAdvSql("");
            _LibSaw = new Saw.Lib.clsLibSaw();
        }

        public string SqlCobranzasEntreFechas(int valConsecutivoCompania,Saw.Lib.eMonedaParaImpresion valMonedaReporte,Saw.Lib.eTasaDeCambioParaImpresion valTipoTasaDeCambio,decimal valTasaDeCambio,DateTime valFechaDesde,DateTime valFechaHasta,string valNombreCobrador,string valNombreCliente,string valNombreCuentaBancaria,eFiltrarCobranzasPor valFiltrarCobranzasPor,bool valAgrupado,bool valUsaVentasConIvaDiferidos) {
            string vSQLWhere = "";
            string vSqlTotalCobrado = "";
            string vSqlCambio = "";
            string vSqlMonedaDeCobro = "";
            string vCodigoMoneda = "";
            string vSqlFechaDeLaCobranza = "";
            string vSqlCobranzaCodigoMoneda = "";
            string vMonedaLocal = "";
            string vFiltroAgrupar = "";
            StringBuilder vSql = new StringBuilder();
            bool vIsInMonedaLocal;
            bool vCambioOriginal = (valTipoTasaDeCambio == Saw.Lib.eTasaDeCambioParaImpresion.Original);
            bool vUsaModuloContabilidad = LibConvert.SNToBool(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros","UsaModuloDeContabiliad"));
            string vSqlGroupBy = "";
            Galac.Comun.Ccl.TablasGen.IMonedaLocalActual vMonedaLocalActual = new Galac.Comun.Brl.TablasGen.clsMonedaLocalActual();

            vMonedaLocalActual.CargarTodasEnMemoriaYAsignarValoresDeLaActual(LibDefGen.ProgramInfo.Country,LibDate.Today());
            vCodigoMoneda = vSqlUtil.ToSqlValue(vMonedaLocalActual.CodigoMoneda(LibDate.Today()));
            vMonedaLocal = vMonedaLocalActual.NombreMoneda(LibDate.Today());
            vIsInMonedaLocal = LibString.S1IsInS2(vMonedaLocal,valMonedaReporte.GetDescription());

            if(valUsaVentasConIvaDiferidos) {
                if(valFiltrarCobranzasPor == eFiltrarCobranzasPor.CuentaBancaria) {
                    vSqlTotalCobrado = " SUM(DocumentoCobrado.MontoAbonado - DocumentoCobrado.MontoIvaRetenido) / (cobranza.TotalCobrado - cobranza.RetencionIva) ";
                    vSqlTotalCobrado += " * (cobranza.TotalCobrado - (cobranza.RetencionIslr + cobranza.TotalOtros + cobranza.RetencionIva + cobranza.CobradoAnticipo + cobranza.Vuelto + cobranza.DescProntoPago)) ";
                } else {
                    vSqlTotalCobrado = "SUM (DocumentoCobrado.MontoAbonado)";
                }
                vSqlGroupBy = SqlGroupBy();
            } else {
                if(valFiltrarCobranzasPor == eFiltrarCobranzasPor.CuentaBancaria) {
                    vSqlTotalCobrado = "cobranza.TotalCobrado - (cobranza.RetencionIslr + cobranza.TotalOtros + cobranza.RetencionIva +" +
                                       " cobranza.CobradoAnticipo + cobranza.Vuelto + cobranza.DescProntoPago)";
                } else {
                    vSqlTotalCobrado = "cobranza.TotalCobrado";
                }
            }
            vSqlFechaDeLaCobranza = "Cobranza.Fecha";
            vSqlMonedaDeCobro = "Cobranza.Moneda";
            vSqlCambio = vSqlUtil.IIF("cobranza.CambioABolivares IS NULL OR cobranza.CambioABolivares = 0","1","cobranza.CambioABolivares",true);
            vSqlCobranzaCodigoMoneda = "Cobranza.CodigoMoneda";
            if(vIsInMonedaLocal) {
                if(vCambioOriginal) {
                    vSqlCambio = vMonedaLocalActual.SqlConvierteMontoSiAplica(vCodigoMoneda,vSqlCambio,"Cobranza.Fecha",false);
                } else {
                    vSqlCambio = new Saw.Lib.clsLibSaw().CampoMontoPorTasaDeCambioSql("",vSqlMonedaDeCobro,"1",false,"");
                }
                vSqlTotalCobrado = vSqlUtil.IIF(vSqlCobranzaCodigoMoneda + " = " + vCodigoMoneda,
                                   vMonedaLocalActual.SqlConvierteMontoSiAplica(vCodigoMoneda,vSqlTotalCobrado,vSqlFechaDeLaCobranza),
                                    _LibSaw.CampoMontoPorTasaDeCambioSql(vSqlCambio,vSqlMonedaDeCobro,vSqlTotalCobrado,vIsInMonedaLocal,""),true);
                vSqlMonedaDeCobro = vSqlUtil.ToSqlValue(vMonedaLocal);
            } else {
                vSqlMonedaDeCobro = "Cobranza.moneda";
            }

            vSql.AppendLine(" SET DATEFORMAT dmy ");
            if(vUsaModuloContabilidad) {
                vSql.AppendLine(CteComprobantesSql(valConsecutivoCompania,valFechaDesde,valFechaHasta));
            }
            vSql.AppendLine(" SELECT Cobranza.Fecha,");
            vSql.AppendLine(" Cobranza.Numero,");
            vSql.AppendLine(" Cobranza.CodigoCobrador,");
            vSql.AppendLine(vSqlUtil.Left("Vendedor.Nombre",25) + "AS NombreVendedor,");
            vSql.AppendLine(" Cobranza.CodigoCliente,");
            vSql.AppendLine(vSqlUtil.Left(" Cliente.Nombre",25) + "AS NombreCliente,");
            vSql.AppendLine(" Cobranza.CodigoMoneda,");
            vSql.AppendLine(vSqlMonedaDeCobro + " AS MonedaCobro,"); //Revisar
            vSql.AppendLine(vSqlCambio + " AS Cambio,"); //Revisar           
            vSql.AppendLine(vSqlUtil.IIF("Cobranza.StatusCobranza = " + vSqlUtil.EnumToSqlValue((int)eStatusCobranza.Anulada),"0",vSqlTotalCobrado,true) + "AS TotalCobrado,");
            vSql.AppendLine(vSqlUtil.IIF("Cobranza.StatusCobranza = " + vSqlUtil.EnumToSqlValue((int)eStatusCobranza.Anulada),vSqlUtil.ToSqlValue(eStatusCobranza.Anulada.GetDescription()),vSqlUtil.ToSqlValue(""),true) + " AS Status,");
            vSql.AppendLine(vSqlUtil.Left("CuentaBancaria.NombreCuenta",25)+ "AS NombreCuenta");
            if(vUsaModuloContabilidad) {
                vSql.AppendLine("," + vSqlUtil.IIF("CTE_Comprobante.NumeroComprobante <>" + vSqlUtil.ToSqlValue(""),"CTE_Comprobante.NumeroComprobante",vSqlUtil.ToSqlValue("No Aplica"),true) + " AS NumeroComprobante ");
            }
            vSql.AppendLine(" FROM Cobranza ");
            vSql.AppendLine(" INNER JOIN  cliente ON ");
            vSql.AppendLine(" Cobranza.CodigoCliente = Cliente.Codigo ");
            vSql.AppendLine(" AND Cobranza.ConsecutivoCompania = Cliente.ConsecutivoCompania  ");
            vSql.AppendLine(" INNER JOIN Vendedor ON ");
            vSql.AppendLine(" Cobranza.CodigoCobrador = vendedor.Codigo ");
            vSql.AppendLine(" AND cobranza.ConsecutivoCompania = vendedor.ConsecutivoCompania ");
            if(valFiltrarCobranzasPor == eFiltrarCobranzasPor.CuentaBancaria) {
                vSql.AppendLine(" INNER JOIN CuentaBancaria ON ");
                vSql.AppendLine(" Cobranza.CodigoCuentaBancaria = CuentaBancaria.Codigo ");
                vSql.AppendLine(" AND Cobranza.ConsecutivoCompania = CuentaBancaria.ConsecutivoCompania");
            } else {
                vSql.AppendLine(" LEFT JOIN CuentaBancaria ON ");
                vSql.AppendLine(" Cobranza.CodigoCuentaBancaria = CuentaBancaria.Codigo ");
                vSql.AppendLine(" AND Cobranza.ConsecutivoCompania = CuentaBancaria.ConsecutivoCompania");
            }
            if(vUsaModuloContabilidad) {
                vSql.Append(SqlUsaContabilidad());
            }
            if(valUsaVentasConIvaDiferidos) {
                vSql.Append(SqlUsaVentaIvaDiferido());
            }
            vSQLWhere = vSqlUtil.SqlDateValueBetween(vSQLWhere,"Cobranza.Fecha",valFechaDesde,valFechaHasta) + NewLine;
            vFiltroAgrupar = NombreCampoFiltroInformeCobranzasEntreFecha(valFiltrarCobranzasPor);
            if(!LibString.IsNullOrEmpty(valNombreCliente)) {
                vSQLWhere = vSqlUtil.SqlValueWithAnd(vSQLWhere,vFiltroAgrupar,valNombreCliente) + NewLine;
            } else if(!LibString.IsNullOrEmpty(valNombreCobrador)) {
                vSQLWhere = vSqlUtil.SqlValueWithAnd(vSQLWhere,vFiltroAgrupar,valNombreCobrador) + NewLine;
            } else if(!LibString.IsNullOrEmpty(valNombreCuentaBancaria)) {
                vSQLWhere = vSqlUtil.SqlValueWithAnd(vSQLWhere,vFiltroAgrupar,valNombreCuentaBancaria) + NewLine;
            }
            vSQLWhere = vSqlUtil.SqlIntValueWithAnd(vSQLWhere,"Cobranza.ConsecutivoCompania",valConsecutivoCompania) + NewLine;
            if(valUsaVentasConIvaDiferidos) {
                vSQLWhere += SqlGroupBy();
            }
            if(valAgrupado) {
                vSQLWhere += " ORDER BY MonedaCobro," + vFiltroAgrupar + ", Cobranza.Fecha, Cobranza.Numero";
            } else {
                vSQLWhere += " ORDER BY MonedaCobro, Cobranza.Fecha, " + vFiltroAgrupar + " , Cobranza.Numero";
            }
            vSQLWhere = vSqlUtil.WhereSql(vSQLWhere);
            if(LibString.Len(vSQLWhere) > 0) {
                vSql.AppendLine(vSQLWhere);
            }
            return vSql.ToString();
        }

        private string NombreCampoFiltroInformeCobranzasEntreFecha(eFiltrarCobranzasPor vFieldFiltro) {
            string vResult = "";
            switch(vFieldFiltro) {
            case eFiltrarCobranzasPor.Cobrador:
                vResult = "Vendedor.Nombre";
                break;
            case eFiltrarCobranzasPor.Cliente:
                vResult = "Cliente.Nombre";
                break;
            case eFiltrarCobranzasPor.CuentaBancaria:
                vResult = "CuentaBancaria.NombreCuenta";
                break;
            }
            return vResult;
        }

        private string SqlUsaContabilidad() {
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine(" LEFT JOIN CTE_Comprobante ON");
            vSql.AppendLine(" Cobranza.Numero = CTE_Comprobante.NoDocumentoOrigen AND");
            vSql.AppendLine(" Cobranza.ConsecutivoCompania = CTE_Comprobante.ConsecutivoCompania AND ");
            vSql.AppendLine(" Cobranza.Fecha = CTE_Comprobante.FechaComprobante ");
            return vSql.ToString();
        }

        private string SqlUsaVentaIvaDiferido() {
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine(" INNER JOIN DocumentoCobrado ON ");
            vSql.AppendLine(" DocumentoCobrado.NumeroCobranza = Cobranza.Numero ");
            vSql.AppendLine(" AND DocumentoCobrado.ConsecutivoCompania = Cobranza.ConsecutivoCompania ");
            vSql.AppendLine(" INNER JOIN factura ON ");
            vSql.AppendLine(" factura.Numero = DocumentoCobrado.NumeroDelDocumentoCobrado ");
            vSql.AppendLine(" AND factura.EsOriginalmenteDiferida = " + vSqlUtil.ToSqlValue(true));
            vSql.AppendLine(" AND DocumentoCobrado.ConsecutivoCompania = factura.ConsecutivoCompania ");
            return vSql.ToString();
        }

        private string SqlGroupBy() {
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("GROUP BY ");
            vSql.AppendLine(" Cobranza.Fecha ");
            vSql.AppendLine(" , Cobranza.Numero ");
            vSql.AppendLine(" , Cobranza.CodigoCobrador ");
            vSql.AppendLine(" , Vendedor.Nombre ");
            vSql.AppendLine(" , Cobranza.CodigoCliente ");
            vSql.AppendLine(" , Cliente.Nombre ");
            vSql.AppendLine(" , Cobranza.CodigoMoneda ");
            vSql.AppendLine(" , Cobranza.moneda ");
            vSql.AppendLine(" , cobranza.CambioABolivares ");
            vSql.AppendLine(" , CuentaBancaria.NombreCuenta");
            vSql.AppendLine(" , Cobranza.StatusCobranza ");
            vSql.AppendLine(" , cobranza.TotalCobrado ");
            vSql.AppendLine(" , cobranza.RetencionIslr ");
            vSql.AppendLine(" , cobranza.TotalOtros ");
            vSql.AppendLine(" , cobranza.RetencionIva ");
            vSql.AppendLine(" , cobranza.CobradoAnticipo ");
            vSql.AppendLine(" , cobranza.Vuelto ");
            vSql.AppendLine(" , cobranza.DescProntoPago ");
            vSql.AppendLine(" , CTE_Comprobante.NumeroComprobante ");
            return vSql.ToString();
        }

        private string CteComprobantesSql(int valConsecutivoCompania,DateTime valFechaDesde,DateTime valFechaHasta) {
            string vSqlWhere = "";
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine(";WITH CTE_Comprobante(ConsecutivoCompania,NumeroComprobante,NoDocumentoOrigen,FechaComprobante) ");
            vSql.AppendLine("AS ");
            vSql.AppendLine("(SELECT ");
            vSql.AppendLine("Periodo.ConsecutivoCompania,");
            vSql.AppendLine("Numero,");
            vSql.AppendLine("NoDocumentoOrigen,");
            vSql.AppendLine("Fecha ");
            vSql.AppendLine("FROM ");
            vSql.AppendLine("COMPROBANTE ");
            vSql.AppendLine("INNER JOIN PERIODO ON ");
            vSql.AppendLine("COMPROBANTE.ConsecutivoPeriodo = PERIODO.ConsecutivoPeriodo AND ");
            vSql.AppendLine("COMPROBANTE.GeneradoPor ='<' AND ");
            vSql.AppendLine("COMPROBANTE.Fecha BETWEEN PERIODO.FechaAperturaDelPeriodo AND PERIODO.FechaCierreDelPeriodo ");
            vSqlWhere = vSqlUtil.SqlDateValueBetween(vSqlWhere,"Comprobante.Fecha",valFechaDesde,valFechaHasta);
            vSqlWhere = vSqlUtil.SqlIntValueWithAnd(vSqlWhere,"Periodo.ConsecutivoCompania",valConsecutivoCompania);
            vSqlWhere = vSqlUtil.WhereSql(vSqlWhere);
            vSql.Append(vSqlWhere + ")");
            return vSql.ToString();
        }
		
		public string SqlComisionDeVendedoresPorCobranzaMonto(int valConsecutivoCompania, DateTime valFechaInicial, DateTime valFechaFinal, Saw.Lib.eTipoDeInforme valTipoDeInforme,Saw.Lib.eMonedaParaImpresion valMonedaDeReporte, Saw.Lib.eTasaDeCambioParaImpresion valTasaDeCambioImpresion, Saw.Lib.eCantidadAImprimir valCantidadAImprimir, string valCodigoVendedor, string valCodigoMonedaLocal) {
            StringBuilder vSql = new StringBuilder();
            QAdvSql vUtilSql = new QAdvSql("");
            bool vParametroUsaOtrosCargosDeFactura = LibConvert.SNToBool(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "UsaOtrosCargosDeFactura"));
            bool vParametroAsignarComisionEnCobranza = LibConvert.SNToBool(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "AsignarComisionDeVendedorEnCobranza"));
            string vCxCManual = SqlComisionDeVendedoresDetalladoOrigenCxCManual(valConsecutivoCompania, valFechaInicial, valFechaFinal, valMonedaDeReporte, valTasaDeCambioImpresion, valCantidadAImprimir, valCodigoVendedor, valCodigoMonedaLocal, vParametroAsignarComisionEnCobranza);
            string vCxCDesdeFactura = SqlComisionDeVendedoresDetalladoOrigenFactura(valConsecutivoCompania, valFechaInicial, valFechaFinal, valMonedaDeReporte, valTasaDeCambioImpresion, valCantidadAImprimir, valCodigoVendedor, valCodigoMonedaLocal, vParametroUsaOtrosCargosDeFactura, vParametroAsignarComisionEnCobranza);
            vSql.AppendLine(vUtilSql.SetDateFormat());
            if (vParametroUsaOtrosCargosDeFactura) {
                string vSqlCTETotalRenglonFactura = SqlCTECalcularTotalRenglonFactura(valConsecutivoCompania, valFechaInicial, valFechaFinal, valTasaDeCambioImpresion);
                string vSqlCTETotalRenglonOtrosCargosDeFactura = SqlCTECalcularTotalRenglonOtrosCargosYDescuentos(valConsecutivoCompania, valFechaInicial, valFechaFinal, valTasaDeCambioImpresion);
                vSql.AppendLine($" ;WITH {vSqlCTETotalRenglonFactura}");
                vSql.AppendLine($" , {vSqlCTETotalRenglonOtrosCargosDeFactura}");
            }
            vSql.AppendLine(vCxCManual);
            vSql.AppendLine("   UNION");
            vSql.AppendLine(vCxCDesdeFactura);
            #region Orden de presentacion de para el informe
            vSql.AppendLine("   ORDER BY");
            if (valTipoDeInforme == Saw.Lib.eTipoDeInforme.Detallado) {
                vSql.AppendLine("       Vendedor.Nombre");
                vSql.AppendLine("	    , Cobranza.Moneda");
            } else {
                vSql.AppendLine("	    Cobranza.Moneda");
                vSql.AppendLine("       , Vendedor.Nombre");
            }
            vSql.AppendLine("       , Cobranza.Fecha");
            vSql.AppendLine("       , NumeroDelDocumento");
            #endregion
            return vSql.ToString();
		}

        private string SqlComisionDeVendedoresDetalladoOrigenCxCManual(int valConsecutivoCompania, DateTime valFechaInicial, DateTime valFechaFinal, Saw.Lib.eMonedaParaImpresion valMonedaDeReporte, Saw.Lib.eTasaDeCambioParaImpresion valTasaDeCambioImpresion, Saw.Lib.eCantidadAImprimir valCantidadAImprimir, string valCodigoVendedor, string valCodigoMonedaLocal, bool valUsaAsignacionDeComisionEnCobranza) {
            QAdvSql vUtilSql = new QAdvSql("");
            StringBuilder vSql = new StringBuilder();
            #region Filtrado
            string vSQLWhereCxCManual = string.Empty;
            string vCxCValida = "cxC.MontoAbonado + cxC.MontoIVA + cxC.MontoExento";
            vSQLWhereCxCManual = vUtilSql.SqlExpressionValueWithAnd(vSQLWhereCxCManual, "CxC.Origen", vUtilSql.EnumToSqlValue((int)eOrigenFacturacionOManual.Manual));
            vSQLWhereCxCManual = vUtilSql.SqlDateValueBetween(vSQLWhereCxCManual, "Cobranza.Fecha", valFechaInicial, valFechaFinal);
            vSQLWhereCxCManual = vUtilSql.SqlExpressionValueWithAnd(vSQLWhereCxCManual, "Cobranza.StatusCobranza", vUtilSql.EnumToSqlValue((int)eStatusCobranza.Vigente));
            if (valCantidadAImprimir == Saw.Lib.eCantidadAImprimir.Uno) {
                vSQLWhereCxCManual = vUtilSql.SqlValueWithAnd(vSQLWhereCxCManual, "Vendedor.Codigo", valCodigoVendedor);
            }
            vSQLWhereCxCManual = vUtilSql.SqlIntValueWithOperators(vSQLWhereCxCManual, vCxCValida, 0, true, "AND", "<>");
            vSQLWhereCxCManual = vUtilSql.SqlIntValueWithAnd(vSQLWhereCxCManual, "Cobranza.ConsecutivoCompania", valConsecutivoCompania);
            #endregion
            #region Calculo del monto comisionable
            string vCodMonedaLocal = string.Empty;
            string vMontoAbonadoEnDocumentoCob = "documentoCobrado.MontoAbonado";
            string vMontoTotalDoc = "(cxC.MontoExento + cxC.MontoGravado)";
            string vMontoIVA = "cxC.MontoIva";
            string vProrrateoDelIVA = vMontoAbonadoEnDocumentoCob + "/ (1 + " + vMontoTotalDoc + "/" + vMontoIVA + ")";
            string vEsPosibleProrrateoIVA = vUtilSql.IIF(vMontoIVA + vUtilSql.ComparisonOp("<>") + vUtilSql.ToInt("0"), vProrrateoDelIVA, vUtilSql.ToInt("0"), true);
            string vMontoComisionable = vUtilSql.RoundToNDecimals(vMontoAbonadoEnDocumentoCob + " - " + vEsPosibleProrrateoIVA, 3);
            string vMontoComisionableConvertido = vMontoComisionable + "* Cobranza.CambioABolivares";
            string vMontoComisionableEnMonedaLocal = vUtilSql.IIF("Cobranza.CodigoMoneda =" + vUtilSql.ToSqlValue(valCodigoMonedaLocal), vMontoComisionable, vMontoComisionableConvertido, true);
            #region En moneda local - Tasa del dia
            string vMontoAbonadoEnMonedaLocalTasaDelDia = _LibSaw.CampoMontoPorTasaDeCambioSegunCodMonedaSql(string.Empty, "Cobranza.CodigoMoneda", vMontoAbonadoEnDocumentoCob, false, string.Empty);
            string vMontoComisionableEnMonedaLocalTasaDelDia = _LibSaw.CampoMontoPorTasaDeCambioSegunCodMonedaSql(string.Empty, "Cobranza.CodigoMoneda", vMontoComisionable, false, string.Empty);
            #endregion
            #endregion
            vSql.AppendLine("SELECT");
            vSql.AppendLine("   Vendedor.Codigo AS CodigoDelVendedor");
            vSql.AppendLine("   , Vendedor.Nombre AS NombreDelVendedor");
            vSql.AppendLine("   , cobranza.Moneda AS MonedaCobranza");
            vSql.AppendLine("   , Cobranza.Fecha AS FechaDoc");
            vSql.AppendLine("   , cxC.Numero AS NumeroDoc");
            vSql.AppendLine("   , ('Cob. ' + Cobranza.Numero + '/ Doc. ' + CxC.NumeroDocumentoOrigen) AS NumeroDelDocumento");
            vSql.AppendLine("   , cliente.Nombre AS NombreCliente");
            vSql.AppendLine("   , documentoCobrado.SimboloMonedaDeCxC AS SimboloMonedaDoc");
            vSql.AppendLine("   , documentoCobrado.CambioAMonedaLocal AS CambioABolivaresDoc");
            vSql.AppendLine($"   , {SqlPorcentajeDeComision(valUsaAsignacionDeComisionEnCobranza)} AS PorcentajeDeComision");
            //Prorrateo del IVA
            if (valMonedaDeReporte == Saw.Lib.eMonedaParaImpresion.EnMonedaOriginal) {
                vSql.AppendLine("   , documentoCobrado.MontoAbonado AS MontoAbonado");
                vSql.AppendLine("   ," + vMontoComisionable + " AS MontoComisionable");
                vSql.AppendLine("   ," + vMontoComisionableEnMonedaLocal + " AS MontoComisionableEnMonedaLocal");
                if(valUsaAsignacionDeComisionEnCobranza) {
                    vSql.AppendLine($"  , {SqlCalculoDeComisionEnCobranza(vMontoComisionable)} AS MontoComision");
                }
            } else {
                if (valTasaDeCambioImpresion == Saw.Lib.eTasaDeCambioParaImpresion.Original) {
                    vSql.AppendLine("   , documentoCobrado.MontoAbonado * DocumentoCobrado.CambioAMonedaDeCobranza AS MontoAbonado");
                    vSql.AppendLine("   ," + vMontoComisionable + " AS MontoComisionable");
                    vSql.AppendLine("   ," + vMontoComisionableEnMonedaLocal + " AS MontoComisionableEnMonedaLocal");
                    if(valUsaAsignacionDeComisionEnCobranza) {
                        vSql.AppendLine($"  , {SqlCalculoDeComisionEnCobranza(vMontoComisionableEnMonedaLocal)} AS MontoComision");
                    }
                } else {
                    vSql.AppendLine("   , " + vMontoAbonadoEnMonedaLocalTasaDelDia + " AS MontoAbonado");
                    vSql.AppendLine("   , " + vMontoComisionable + " AS MontoComisionable"); //Obligatorio por el UNION
                    vSql.AppendLine("   , " + vMontoComisionableEnMonedaLocalTasaDelDia + " AS MontoComisionableEnMonedaLocal");
                    if(valUsaAsignacionDeComisionEnCobranza) {
                        vSql.AppendLine($"  , {SqlCalculoDeComisionEnCobranza(vMontoComisionableEnMonedaLocalTasaDelDia)} AS MontoComision");
                    }
                }
            }
            vSql.AppendLine("	FROM Vendedor");
            vSql.AppendLine("	INNER JOIN Cobranza");
            vSql.AppendLine("   ON cobranza.CodigoCobrador = Vendedor.Codigo");
            vSql.AppendLine("       AND cobranza.ConsecutivoCompania = Vendedor.ConsecutivoCompania");
            vSql.AppendLine("   INNER JOIN DocumentoCobrado");
            vSql.AppendLine("   ON DocumentoCobrado.NumeroCobranza = Cobranza.Numero");
            vSql.AppendLine("       AND DocumentoCobrado.ConsecutivoCompania = Cobranza.ConsecutivoCompania");
            vSql.AppendLine("   INNER JOIN CxC");
            vSql.AppendLine("   ON cxC.ConsecutivoCompania = DocumentoCobrado.ConsecutivoCompania");
            vSql.AppendLine("       AND cxC.TipoCxC = DocumentoCobrado.TipoDeDocumentoCobrado");
            vSql.AppendLine("       AND cxC.Numero = DocumentoCobrado.NumeroDelDocumentoCobrado");
            vSql.AppendLine("   INNER JOIN Cliente");
            vSql.AppendLine("   ON Cliente.Codigo = CxC.CodigoCliente");
            vSql.AppendLine("       AND Cliente.ConsecutivoCompania = CxC.ConsecutivoCompania");
            if (LibString.Len(vSQLWhereCxCManual) > 0) {
                vSql.AppendLine(vUtilSql.WhereSql(vSQLWhereCxCManual));
            }
            return vSql.ToString();
        }

        private string SqlComisionDeVendedoresDetalladoOrigenFactura(int valConsecutivoCompania, DateTime valFechaInicial, DateTime valFechaFinal, Saw.Lib.eMonedaParaImpresion valMonedaDeReporte, Saw.Lib.eTasaDeCambioParaImpresion valTasaDeCambioImpresion, Saw.Lib.eCantidadAImprimir valCantidadAImprimir, string valCodigoVendedor, string valCodigoMonedaLocal, bool valUsaOtrosCargosDeFactura, bool valUsaAsignacionDeComisionEnCobranza) {
            QAdvSql vUtilSql = new QAdvSql("");
            StringBuilder vSql = new StringBuilder();
            #region Filtrado
            string vSQLWhereCxCDesdeFactura = string.Empty;
            string vCxCValida = "cxC.MontoAbonado + cxC.MontoIVA + cxC.MontoExento";
            vSQLWhereCxCDesdeFactura = vUtilSql.SqlExpressionValueWithAnd(vSQLWhereCxCDesdeFactura, "CxC.Origen", vUtilSql.EnumToSqlValue((int)eOrigenFacturacionOManual.Factura));
            vSQLWhereCxCDesdeFactura = vUtilSql.SqlDateValueBetween(vSQLWhereCxCDesdeFactura, "Cobranza.Fecha", valFechaInicial, valFechaFinal);
            vSQLWhereCxCDesdeFactura = vUtilSql.SqlExpressionValueWithAnd(vSQLWhereCxCDesdeFactura, "Cobranza.StatusCobranza", vUtilSql.EnumToSqlValue((int)eStatusCobranza.Vigente));
            if (valCantidadAImprimir == Saw.Lib.eCantidadAImprimir.Uno) {
                vSQLWhereCxCDesdeFactura = vUtilSql.SqlValueWithAnd(vSQLWhereCxCDesdeFactura, "Vendedor.Codigo", valCodigoVendedor);
            }
            vSQLWhereCxCDesdeFactura = vUtilSql.SqlIntValueWithOperators(vSQLWhereCxCDesdeFactura, vCxCValida, 0, true, "AND", "<>");
            vSQLWhereCxCDesdeFactura = vUtilSql.SqlIntValueWithAnd(vSQLWhereCxCDesdeFactura, "Cobranza.ConsecutivoCompania", valConsecutivoCompania);
            #endregion
            #region Calculo del monto comisionable
            string vCodMonedaLocal = string.Empty;
            string vMontoAbonadoEnDocumentoCob = "documentoCobrado.MontoAbonado";
            string vMontoTotalDoc = "(CxC.MontoExento + CxC.MontoGravado)";
            string vMontoTotalDocConIva = "(CxC.MontoExento + CxC.MontoGravado + CxC.MontoIVA)";
            string vMontoIVA = "cxC.MontoIva";
            string vTotalRenglonFacturaEnMonedaOriginal = "CTE_TotalRenglonFactura.TotalRenglonEnMonedaOriginal";
            string vTotalRenglonFacturaEnMonedaLocal = "CTE_TotalRenglonFactura.TotalRenglonEnMonedaLocal";
            string vTotalRenglonOtrosCargosDeFacturaEnMonedaOriginal = "CTE_TotalRenglonOtrosCargosFactura.MontoTotalRenglonEnMonedaOriginal";
            string vTotalRenglonOtrosCargosDeFacturaEnMonedaLocal = "CTE_TotalRenglonOtrosCargosFactura.MontoTotalRenglonEnMonedaLocal";
            string vProrrateoDelIVA = vMontoAbonadoEnDocumentoCob + "/ (1 + " + vMontoTotalDoc + "/" + vMontoIVA + ")";
            string vEsPosibleProrrateoIVA = vUtilSql.IIF(vMontoIVA + vUtilSql.ComparisonOp("<>") + vUtilSql.ToInt("0"), vProrrateoDelIVA, vUtilSql.ToInt("0"), true);
            string vMontoComisionable = vUtilSql.RoundToNDecimals(vMontoAbonadoEnDocumentoCob + " - " + vEsPosibleProrrateoIVA, 3);
            string vMontoComisionableConvertido = vMontoComisionable + "* Cobranza.CambioABolivares";
            string vMontoComisionableEnMonedaLocal = vUtilSql.IIF("Cobranza.CodigoMoneda =" + vUtilSql.ToSqlValue(valCodigoMonedaLocal), vMontoComisionable, vMontoComisionableConvertido, true);
            string vMontoAbonadoEnMonedaLocalTasaDelDia = _LibSaw.CampoMontoPorTasaDeCambioSegunCodMonedaSql(string.Empty, "Cobranza.CodigoMoneda", vMontoAbonadoEnDocumentoCob, false, string.Empty);
            string vMontoComisionableEnMonedaLocalTasaDelDia = _LibSaw.CampoMontoPorTasaDeCambioSegunCodMonedaSql(string.Empty, "Cobranza.CodigoMoneda", vMontoComisionable, false, string.Empty);

            string esMontoValidoConRenglonFactura = string.Empty;
            string esMontoValidoConOtrosCargosFactura = string.Empty;
            #endregion
            if (valUsaOtrosCargosDeFactura) {
                if (valMonedaDeReporte == Saw.Lib.eMonedaParaImpresion.EnMonedaOriginal) {
                    esMontoValidoConRenglonFactura = vUtilSql.IsNull($"(({vMontoAbonadoEnDocumentoCob} * {vTotalRenglonFacturaEnMonedaOriginal})/{vMontoTotalDocConIva})", "0");
                    esMontoValidoConOtrosCargosFactura = vUtilSql.IsNull($"(({vMontoAbonadoEnDocumentoCob} * {vTotalRenglonOtrosCargosDeFacturaEnMonedaOriginal})/{vMontoTotalDocConIva})", "0");
                    vMontoComisionable = $"(({vMontoComisionable}) - ({esMontoValidoConRenglonFactura}) - ({esMontoValidoConOtrosCargosFactura}))";
                    vMontoComisionableConvertido = $"{vMontoComisionable} * Cobranza.CambioABolivares";
                    vMontoComisionableEnMonedaLocal = vUtilSql.IIF("Cobranza.CodigoMoneda =" + vUtilSql.ToSqlValue(valCodigoMonedaLocal), vMontoComisionable, vMontoComisionableConvertido, true);
                } else {
                    esMontoValidoConRenglonFactura= vUtilSql.IsNull($"(({vMontoAbonadoEnDocumentoCob} * {vTotalRenglonFacturaEnMonedaLocal})/{vMontoTotalDocConIva})", "0");
                    esMontoValidoConOtrosCargosFactura= vUtilSql.IsNull($"(({vMontoAbonadoEnDocumentoCob} * {vTotalRenglonOtrosCargosDeFacturaEnMonedaLocal})/ {vMontoTotalDocConIva})", "0");
                    vMontoComisionable = $"(({vMontoComisionable}) - ({esMontoValidoConRenglonFactura}) - ({esMontoValidoConOtrosCargosFactura}))";
                    vMontoComisionableEnMonedaLocal = vUtilSql.RoundToNDecimals(_LibSaw.CampoMontoPorTasaDeCambioSegunCodMonedaSql("Cobranza.CambioAbolivares", "Cobranza.CodigoMoneda", vMontoComisionable, true, string.Empty),4);
                    if (valTasaDeCambioImpresion == Saw.Lib.eTasaDeCambioParaImpresion.DelDia) {
                        vMontoComisionableEnMonedaLocalTasaDelDia = vUtilSql.RoundToNDecimals(_LibSaw.CampoMontoPorTasaDeCambioSegunCodMonedaSql("Cobranza.CambioAbolivares", "Cobranza.CodigoMoneda", vMontoComisionable, false, string.Empty), 4);
                    }
                }
            }
            vSql.AppendLine(" SELECT");
            vSql.AppendLine("   Vendedor.Codigo AS CodigoDelVendedor");
            vSql.AppendLine("   , Vendedor.Nombre AS NombreDelVendedor");
            vSql.AppendLine("   , cobranza.Moneda AS MonedaCobranza");
            vSql.AppendLine("   , Cobranza.Fecha AS FechaDoc");
            vSql.AppendLine("   , cxC.Numero AS NumeroDoc");
            vSql.AppendLine("   , ('Cob. ' + Cobranza.Numero + '/ Fact. ' + CxC.NumeroDocumentoOrigen) AS NumeroDelDocumento");
            vSql.AppendLine("   , cliente.Nombre AS NombreCliente");
            vSql.AppendLine("   , documentoCobrado.SimboloMonedaDeCxC AS SimboloMonedaDoc");
            vSql.AppendLine("   , documentoCobrado.CambioAMonedaLocal AS CambioABolivaresDoc");
            vSql.AppendLine($"   , {SqlPorcentajeDeComision(valUsaAsignacionDeComisionEnCobranza)} AS PorcentajeDeComision");
            //Prorrateo del IVA
            if (valMonedaDeReporte == Saw.Lib.eMonedaParaImpresion.EnMonedaOriginal) {
                vSql.AppendLine("   , documentoCobrado.MontoAbonado AS MontoAbonado");
                vSql.AppendLine("   ," + vMontoComisionable + " AS MontoComisionable");
                vSql.AppendLine("   ," + vMontoComisionableEnMonedaLocal + " AS MontoComisionableEnMonedaLocal");
                if(valUsaAsignacionDeComisionEnCobranza) {
                    vSql.AppendLine($"  , {SqlCalculoDeComisionEnCobranza(vMontoComisionable)} AS MontoComision");
                }
            } else {
                if (valTasaDeCambioImpresion == Saw.Lib.eTasaDeCambioParaImpresion.Original) {
                    vSql.AppendLine("   , documentoCobrado.MontoAbonado * DocumentoCobrado.CambioAMonedaDeCobranza AS MontoAbonado");
                    vSql.AppendLine("   ," + vMontoComisionable + " AS MontoComisionable");
                    vSql.AppendLine("   ," + vMontoComisionableEnMonedaLocal + " AS MontoComisionableEnMonedaLocal");
                    if(valUsaAsignacionDeComisionEnCobranza) {
                        vSql.AppendLine($"  , {SqlCalculoDeComisionEnCobranza(vMontoComisionableEnMonedaLocal)} AS MontoComision");
                    }
                } else {
                    vSql.AppendLine("   , " + vMontoAbonadoEnMonedaLocalTasaDelDia + " AS MontoAbonado");
                    vSql.AppendLine("   , " + vMontoComisionable + " AS MontoComisionable"); //Obligatorio por el UNION
                    vSql.AppendLine("   , " + vMontoComisionableEnMonedaLocalTasaDelDia + " AS MontoComisionableEnMonedaLocal");
                    if(valUsaAsignacionDeComisionEnCobranza){
                        vSql.AppendLine($"  , {SqlCalculoDeComisionEnCobranza(vMontoComisionableEnMonedaLocalTasaDelDia)} AS MontoComision");
                    }
                }
            }
            vSql.AppendLine("	FROM Vendedor");
            vSql.AppendLine("	INNER JOIN Cobranza");
            vSql.AppendLine("   ON cobranza.CodigoCobrador = Vendedor.Codigo");
            vSql.AppendLine("       AND cobranza.ConsecutivoCompania = Vendedor.ConsecutivoCompania");
            vSql.AppendLine("   INNER JOIN DocumentoCobrado");
            vSql.AppendLine("   ON DocumentoCobrado.NumeroCobranza = Cobranza.Numero");
            vSql.AppendLine("       AND DocumentoCobrado.ConsecutivoCompania = Cobranza.ConsecutivoCompania");
            vSql.AppendLine("   INNER JOIN CxC");
            vSql.AppendLine("   ON cxC.ConsecutivoCompania = DocumentoCobrado.ConsecutivoCompania");
            vSql.AppendLine("       AND CxC.TipoCxC = DocumentoCobrado.TipoDeDocumentoCobrado");
            vSql.AppendLine("       AND CxC.Numero = DocumentoCobrado.NumeroDelDocumentoCobrado");
            if (valUsaOtrosCargosDeFactura) {
                vSql.AppendLine(SqlJoinConCTECalcularTotalRenglonFactura());
                vSql.AppendLine(SqlJoinConCTECalcularTotalRenglonOtrosCargosYDescuentos());
            }
            vSql.AppendLine("   INNER JOIN Cliente");
            vSql.AppendLine("   ON Cliente.Codigo = CxC.CodigoCliente");
            vSql.AppendLine("       AND Cliente.ConsecutivoCompania = CxC.ConsecutivoCompania");
            if (LibString.Len(vSQLWhereCxCDesdeFactura) > 0) {
                vSql.AppendLine(vUtilSql.WhereSql(vSQLWhereCxCDesdeFactura));
            }

            return vSql.ToString();
        }

        private string SqlCTECalcularTotalRenglonFactura(int valConsecutivoCompania, DateTime valFechaInicial, DateTime valFechaFinal, Saw.Lib.eTasaDeCambioParaImpresion valTasaDeCambioImpresion) {
            StringBuilder vTotalRenglon = new StringBuilder();
            Dictionary<string, string> vSqlTotalDelRenglonFactura = SqlMontoTotalDelRenglonSinIvaConElDescuentoDeFactura(valTasaDeCambioImpresion);
            QAdvSql vUtilSql = new QAdvSql("");
            string vSQLWhere = string.Empty;
            #region Calculos del renglon
            string vConversionTipoDocumentoCxCAFactura = SqlConvertirElTipoDeDocumentoCxCATipodeDocumentoFactura();
            string vTotalRenglonFacturaEnMonedaOriginal = $"SUM({vSqlTotalDelRenglonFactura["MontoEnMonedaOriginal"]})";
            string vTotalRenglonFacturaEnMonedaLocal = $"SUM({vSqlTotalDelRenglonFactura["MontoEnMonedaLocal"]})";
            string vSumaTotalRenglonFacturaEnMonedaOriginal = $"{vUtilSql.IsNull(vTotalRenglonFacturaEnMonedaOriginal, vUtilSql.ToInt("0"))}";
            string vSumaTotalRenglonFacturaEnMonedaLocal = $"{vUtilSql.IsNull(vTotalRenglonFacturaEnMonedaLocal, vUtilSql.ToInt("0"))}";
            #endregion
            #region Filtrado
            vSQLWhere = vUtilSql.SqlBoolValueWithOperators(vSQLWhere, "ArticuloInventario.ExcluirDeComision", true, "=", string.Empty);
            vSQLWhere = vUtilSql.SqlExpressionValueWithAnd(vSQLWhere, "factura.TipoDeDocumento", vConversionTipoDocumentoCxCAFactura);
            vSQLWhere = vUtilSql.SqlDateValueBetween(vSQLWhere, "factura.Fecha", valFechaInicial, valFechaFinal);
            vSQLWhere = vUtilSql.SqlIntValueWithAnd(vSQLWhere, "factura.ConsecutivoCompania", valConsecutivoCompania);
            #endregion
            #region Consulta
            vTotalRenglon.AppendLine(" CTE_TotalRenglonFactura");
            vTotalRenglon.AppendLine(" (ConsecutivoCompania, NumeroFactura, TotalRenglonEnMonedaOriginal, TotalRenglonEnMonedaLocal)");
            vTotalRenglon.AppendLine(" AS (SELECT");
            vTotalRenglon.AppendLine("  factura.ConsecutivoCompania AS ConsecutivoCompania");
            vTotalRenglon.AppendLine(" , factura.Numero AS NumeroFactura");
            vTotalRenglon.AppendLine(" , (" + vSumaTotalRenglonFacturaEnMonedaOriginal + ") AS TotalRenglonEnMonedaOriginal");
            vTotalRenglon.AppendLine(" , (" + vSumaTotalRenglonFacturaEnMonedaLocal + ") AS TotalRenglonEnMonedaLocal");
            vTotalRenglon.AppendLine("	FROM factura");
            vTotalRenglon.AppendLine("	INNER JOIN ArticuloInventario");
            vTotalRenglon.AppendLine("		ON ArticuloInventario.ConsecutivoCompania = factura.ConsecutivoCompania");
            vTotalRenglon.AppendLine("	INNER JOIN renglonFactura");
            vTotalRenglon.AppendLine("		ON renglonFactura.Articulo = ArticuloInventario.Codigo");
            vTotalRenglon.AppendLine("		AND renglonFactura.TipoDeDocumento = factura.TipoDeDocumento");
            vTotalRenglon.AppendLine("		AND renglonFactura.NumeroFactura = factura.Numero");
            vTotalRenglon.AppendLine("		AND renglonFactura.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania");
            vTotalRenglon.AppendLine("		AND renglonFactura.ConsecutivoCompania = factura.ConsecutivoCompania");
            vTotalRenglon.AppendLine("	INNER JOIN CxC");
            vTotalRenglon.AppendLine("		ON CxC.CodigoVendedor = factura.CodigoVendedor");
            vTotalRenglon.AppendLine("		AND CxC.NumeroDocumentoOrigen = factura.Numero");
            vTotalRenglon.AppendLine("		AND CxC.ConsecutivoCompania = factura.ConsecutivoCompania");
            vTotalRenglon.AppendLine(vUtilSql.WhereSql(vSQLWhere));
            vTotalRenglon.AppendLine("  GROUP BY");
            vTotalRenglon.AppendLine("  factura.ConsecutivoCompania");
            vTotalRenglon.AppendLine("  , factura.Numero");
            vTotalRenglon.AppendLine(" )");
            #endregion
            return vTotalRenglon.ToString();
        }

        private string SqlConvertirElTipoDeDocumentoCxCATipodeDocumentoFactura() {
            QAdvSql vUtilSql = new QAdvSql("");
            StringBuilder vSql = new StringBuilder();
            string valCampoTipoCxC = "CxC.TipoCxC";
            List<eTipoDeTransaccion> vListComparative = new List<eTipoDeTransaccion>();
            vListComparative.Add(eTipoDeTransaccion.FACTURA);
            vListComparative.Add(eTipoDeTransaccion.NOTADECREDITO);
            vListComparative.Add(eTipoDeTransaccion.NOTADEDEBITO);
            vSql.AppendLine(" (CASE ");
            foreach (eTipoDeTransaccion tipoEnCxC in vListComparative) {
                vSql.Append($" WHEN {valCampoTipoCxC} = '{vUtilSql.ToSqlValue((int)tipoEnCxC)}'");
                vSql.AppendLine(" THEN");
                switch (tipoEnCxC) {
                    case eTipoDeTransaccion.FACTURA:
                        vSql.Append($" '{vUtilSql.ToSqlValue((int)eTipoDocumentoFactura.Factura)}'");
                        break;
                    case eTipoDeTransaccion.NOTADECREDITO:
                        vSql.Append($" '{vUtilSql.ToSqlValue((int)eTipoDocumentoFactura.NotaDeCredito)}'");
                        break;
                    case eTipoDeTransaccion.NOTADEDEBITO:
                        vSql.Append($" '{vUtilSql.ToSqlValue((int)eTipoDocumentoFactura.NotaDeDebito)}'");
                        break;
                }
            }
            vSql.AppendLine($" ELSE '{vUtilSql.ToSqlValue((int)eTipoDocumentoFactura.Factura)}'");
            vSql.AppendLine(" END)");
            return vSql.ToString(); 
        }

        private Dictionary<string, string> SqlMontoTotalDelRenglonSinIvaConElDescuentoDeFactura(Saw.Lib.eTasaDeCambioParaImpresion valTasaDeCambioImpresion) {
            Dictionary<string, string> vResult = new Dictionary<string, string>();
            string vTotalRenglonConDescuento = string.Empty;
            StringBuilder vSql = new StringBuilder();
            string vTotalRenglon = "(renglonFactura.PrecioSinIVA * renglonFactura.Cantidad)";
            string vMontoPorcentajeDescRenglon = "(1 - (renglonFactura.PorcentajeDescuento / 100))";
            string vMontoPorcentajeDescFactura= "(1 - (factura.PorcentajeDescuento / 100))";
            vTotalRenglonConDescuento = $"(({vTotalRenglon} * {vMontoPorcentajeDescRenglon}) * {vMontoPorcentajeDescFactura})";
            vResult["MontoEnMonedaOriginal"] = vTotalRenglonConDescuento;
            if (valTasaDeCambioImpresion == Saw.Lib.eTasaDeCambioParaImpresion.DelDia) {
                vResult["MontoEnMonedaLocal"] = _LibSaw.CampoMontoPorTasaDeCambioSegunCodMonedaSql("factura.CambioABolivares", "factura.CodigoMoneda", vTotalRenglonConDescuento, false, string.Empty);
            } else {
                vResult["MontoEnMonedaLocal"] = _LibSaw.CampoMontoPorTasaDeCambioSegunCodMonedaSql("factura.CambioABolivares", "factura.CodigoMoneda", vTotalRenglonConDescuento, true, string.Empty);
            }
            return vResult;
        }

        private string SqlJoinConCTECalcularTotalRenglonFactura() {
            StringBuilder vResult = new StringBuilder();
            vResult.AppendLine("    LEFT JOIN CTE_TotalRenglonFactura");
            vResult.AppendLine("        ON CTE_TotalRenglonFactura.ConsecutivoCompania = CxC.ConsecutivoCompania");
            vResult.AppendLine("        AND CTE_TotalRenglonFactura.NumeroFactura = CxC.NumeroDocumentoOrigen");
            return vResult.ToString();
        }

        private string SqlCTECalcularTotalRenglonOtrosCargosYDescuentos(int valConsecutivoCompania, DateTime valFechaInicial, DateTime valFechaFinal, Saw.Lib.eTasaDeCambioParaImpresion valTasaDeCambioImpresion) {
            StringBuilder vTotalRenglon = new StringBuilder();
            QAdvSql vUtilSql = new QAdvSql("");
            Dictionary<string, string> vSqlTotalRenglonOtrosCargos = SqlMontoTotalDelRenglonOtrosCargosDeFactura(valTasaDeCambioImpresion);
            string vSQLWhere = string.Empty;
            string vConversionTipoDocumentoCxCAFactura = SqlConvertirElTipoDeDocumentoCxCATipodeDocumentoFactura();
            string vTotalRenglonOtrosCargosEnMonedaOriginal = $"SUM({vSqlTotalRenglonOtrosCargos["MontoEnMonedaOriginal"]})";
            string vTotalRenglonOtrosCargosEnMonedaLocal = $"SUM({vSqlTotalRenglonOtrosCargos["MontoEnMonedaLocal"]})";
            string vSumaTotalOtrosCargosEnMonedaOriginal = $"{vUtilSql.IsNull(vTotalRenglonOtrosCargosEnMonedaOriginal, vUtilSql.ToInt("0"))}";
            string vSumaTotalOtrosCargosEnMonedaLocal = $"{vUtilSql.IsNull(vTotalRenglonOtrosCargosEnMonedaLocal, vUtilSql.ToInt("0"))}";
            #region Filtrado
            vSQLWhere = vUtilSql.SqlExpressionValueWithAnd(vSQLWhere, "otrosCargosDeFactura.ExcluirDeComision", vUtilSql.ToSqlValue(true), string.Empty, "=");
            vSQLWhere = vUtilSql.SqlExpressionValueWithAnd(vSQLWhere, "otrosCargosDeFactura.ComoAplicaAlTotalFactura", vUtilSql.EnumToSqlValue((int)eComoAplicaOtrosCargosDeFactura.NoAplica_EsInformativo), "AND", "<>");
            vSQLWhere = vUtilSql.SqlExpressionValueWithAnd(vSQLWhere, "factura.TipoDeDocumento", vConversionTipoDocumentoCxCAFactura);
            vSQLWhere = vUtilSql.SqlDateValueBetween(vSQLWhere, "factura.Fecha", valFechaInicial, valFechaFinal);
            vSQLWhere = vUtilSql.SqlIntValueWithAnd(vSQLWhere, "factura.ConsecutivoCompania", valConsecutivoCompania);
            #endregion
            #region Consulta
            vTotalRenglon.AppendLine("  CTE_TotalRenglonOtrosCargosFactura");
            vTotalRenglon.AppendLine("  (ConsecutivoCompania, NumeroFactura, MontoTotalRenglonEnMonedaOriginal, MontoTotalRenglonEnMonedaLocal)");
            vTotalRenglon.AppendLine("  AS (SELECT");
            vTotalRenglon.AppendLine("		factura.ConsecutivoCompania AS ConsecutivoCompania");
            vTotalRenglon.AppendLine("		, factura.Numero AS NumeroFactura");
            vTotalRenglon.AppendLine("		, " + vSumaTotalOtrosCargosEnMonedaOriginal + " AS MontoTotalRenglonEnMonedaOriginal");
            vTotalRenglon.AppendLine("		, " + vSumaTotalOtrosCargosEnMonedaLocal + " AS MontoTotalRenglonEnMonedaLocal");
            vTotalRenglon.AppendLine("  FROM otrosCargosDeFactura");
            vTotalRenglon.AppendLine("  INNER JOIN renglonDetalleDeOtrosCargosFactura");
            vTotalRenglon.AppendLine("		ON renglonDetalleDeOtrosCargosFactura.CodigoDeCargo = otrosCargosDeFactura.Codigo");
            vTotalRenglon.AppendLine("		AND renglonDetalleDeOtrosCargosFactura.ConsecutivoCompania = otrosCargosDeFactura.ConsecutivoCompania");
            vTotalRenglon.AppendLine("  INNER JOIN factura");
            vTotalRenglon.AppendLine("		ON factura.TipoDeDocumento = renglonDetalleDeOtrosCargosFactura.TipoDeDocumento");
            vTotalRenglon.AppendLine("		AND factura.Numero = renglonDetalleDeOtrosCargosFactura.NumeroFactura");
            vTotalRenglon.AppendLine("		AND factura.ConsecutivoCompania = renglonDetalleDeOtrosCargosFactura.ConsecutivoCompania");
            vTotalRenglon.AppendLine("  INNER JOIN CxC");
            vTotalRenglon.AppendLine("      ON CxC.NumeroDocumentoOrigen = factura.Numero");
            vTotalRenglon.AppendLine("		AND CxC.CodigoVendedor = factura.CodigoVendedor");
            vTotalRenglon.AppendLine("		AND CxC.ConsecutivoCompania = factura.ConsecutivoCompania");
            vTotalRenglon.AppendLine(vUtilSql.WhereSql(vSQLWhere));
            vTotalRenglon.AppendLine("  GROUP BY");
            vTotalRenglon.AppendLine("		factura.ConsecutivoCompania");
            vTotalRenglon.AppendLine("		, factura.Numero");
            vTotalRenglon.AppendLine("  )");
            #endregion
            return vTotalRenglon.ToString();
        }

        private Dictionary<string, string> SqlMontoTotalDelRenglonOtrosCargosDeFactura(Saw.Lib.eTasaDeCambioParaImpresion valTasaDeCambioImpresion) {
            Dictionary<string, string> vResult = new Dictionary<string, string>();
            string vSqlTotalRenglonOtrosCargos = "renglonDetalleDeOtrosCargosFactura.TotalRenglon";
            vResult["MontoEnMonedaOriginal"] = vSqlTotalRenglonOtrosCargos;
            if (valTasaDeCambioImpresion == Saw.Lib.eTasaDeCambioParaImpresion.Original) {
                vResult["MontoEnMonedaLocal"] = _LibSaw.CampoMontoPorTasaDeCambioSegunCodMonedaSql("factura.CambioABolivares", "factura.CodigoMoneda", vSqlTotalRenglonOtrosCargos, true, string.Empty);
            } else {
                vResult["MontoEnMonedaLocal"] = _LibSaw.CampoMontoPorTasaDeCambioSegunCodMonedaSql("factura.CambioABolivares", "factura.CodigoMoneda", vSqlTotalRenglonOtrosCargos, false, string.Empty);
            }
            return vResult;
        }

        private string SqlJoinConCTECalcularTotalRenglonOtrosCargosYDescuentos() {
            StringBuilder vResult = new StringBuilder();
            vResult.AppendLine("    LEFT JOIN CTE_TotalRenglonOtrosCargosFactura");
            vResult.AppendLine("        ON CTE_TotalRenglonOtrosCargosFactura.ConsecutivoCompania = CxC.ConsecutivoCompania");
            vResult.AppendLine("        AND CTE_TotalRenglonOtrosCargosFactura.NumeroFactura = CxC.NumeroDocumentoOrigen");
            return vResult.ToString();
        }

        private string SqlPorcentajeDeComision(bool valUsaAsignacionDeComisionEnCobranza) {
            string vResult = "Vendedor.comisionPorCobro";
            if(valUsaAsignacionDeComisionEnCobranza) {
                vResult = "Cobranza.ComisionVendedor";
            }
            return vResult;
        }

        private string SqlCalculoDeComisionEnCobranza(string valSQLMontoComisionableEnMonedaLocal) {
            return $"{valSQLMontoComisionableEnMonedaLocal} * (Cobranza.ComisionVendedor / 100)";
        }
        #endregion //Metodos Generados


    } //End of class clsCobranzaSql

} //End of namespace Galac.Adm.Brl.Venta

