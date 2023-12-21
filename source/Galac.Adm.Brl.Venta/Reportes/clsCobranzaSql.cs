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
        #region Cobranzas Entre Fechas
        public string SqlCobranzasEntreFechas(int valConsecutivoCompania,Saw.Lib.eMonedaParaImpresion valMonedaReporte,Saw.Lib.eTasaDeCambioParaImpresion valTipoTasaDeCambio,decimal valTasaDeCambio,DateTime valFechaDesde,DateTime valFechaHasta,string valNombreCobrador,string valNombreCliente,string valNombreCuentaBancaria,eFiltrarCobranzasPor valFiltrarCobranzasPor,bool valAgrupado,bool valUsaVentasConIvaDiferidos) {
            string vSQLWhere = "";
            string vSqlTotalCobrado;
            string vSqlCambio;
            string vSqlMonedaDeCobro;
            string vCodigoMoneda;
            string vSqlFechaDeLaCobranza;
            string vSqlCobranzaCodigoMoneda;
            string vMonedaLocal;
            string vFiltroAgrupar;
            StringBuilder vSql = new StringBuilder();
            bool vIsInMonedaLocal;
            bool vCambioOriginal = (valTipoTasaDeCambio == Saw.Lib.eTasaDeCambioParaImpresion.Original);
            bool vUsaModuloContabilidad = LibConvert.SNToBool(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros","UsaModuloDeContabiliad"));
            string vSqlGroupBy;
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
        #endregion //Cobranzas Entre Fechas

        #region Comisiones por Vendedor
        public string SqlComisionesPorCobranza(int valConsecutivoCompania, DateTime valFechaDesde, DateTime valFechaHasta, Saw.Lib.eTipoDeInforme valTipoDeInforme,
                                        Saw.Lib.eMonedaParaImpresion valMonedaDeReporte, Saw.Lib.eTasaDeCambioParaImpresion valTasaDeCambioImpresion, eCantidadAImprimir valCantidadAImprimir,
                                        string valCodigoVendedor, string valCodigoMonedaLocal) {
            bool vParametroAsignarComisionEnCobranza = LibConvert.SNToBool(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "AsignarComisionDeVendedorEnCobranza"));
            QAdvSql insSql = new QAdvSql("");
            StringBuilder vSql = new StringBuilder();
            if (valCantidadAImprimir == eCantidadAImprimir.All) {
                valCodigoVendedor = String.Empty;
            }
            vSql.AppendLine(";WITH");
            vSql.AppendLine(SqlCTECxC(valConsecutivoCompania));
            vSql.AppendLine(",");
            vSql.AppendLine(SqlCTERenglonesFactura(valConsecutivoCompania));
            vSql.AppendLine(",");
            vSql.AppendLine(SqlCTETotalRenglonesFact());
            vSql.AppendLine(",");
            vSql.AppendLine(SqlCTEFact(valConsecutivoCompania, valCodigoMonedaLocal));
            vSql.AppendLine(",");
            vSql.AppendLine(SqlCTECxCVsFactVsCliente(valConsecutivoCompania, valCodigoMonedaLocal));
            vSql.AppendLine(",");
            vSql.AppendLine(SqlCTECxCVsFactVsClienteCalculado());
            vSql.AppendLine(",");
            vSql.AppendLine(SqlCTEDocCobradoCxCVsFactVsCliente(valConsecutivoCompania));
            vSql.AppendLine(",");
            vSql.AppendLine(SqlCTECobranzaVsVendedorVsMoneda(valConsecutivoCompania, valFechaDesde, valFechaHasta, vParametroAsignarComisionEnCobranza, valTipoDeInforme, valMonedaDeReporte));
            vSql.AppendLine(",");
            vSql.AppendLine(SqlCTECobranzaVsVendedorVsMonedaVsDocCobradoCxCVsFactVsCliente(valMonedaDeReporte, valTasaDeCambioImpresion));
            vSql.AppendLine();
            vSql.AppendLine(SqlComisionesPorCobranzaSegunOrigen(valCodigoVendedor, eOrigenFacturacionOManual.Manual));
            vSql.AppendLine("UNION");
            vSql.AppendLine(SqlComisionesPorCobranzaSegunOrigen(valCodigoVendedor, eOrigenFacturacionOManual.Factura));
            vSql.AppendLine(" ORDER BY MonedaCobranza, NombreDelVendedor, FechaDoc, NumeroDelDocumento");
            //LibFile.WriteLineInFile(@"C:\Users\erikson.didik\Desktop\prueba.txt", vSql.ToString());
            return vSql.ToString();
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
                vSql.Append($" WHEN {valCampoTipoCxC} = {vUtilSql.EnumToSqlValue((int)tipoEnCxC)}");
                vSql.AppendLine(" THEN");
                switch (tipoEnCxC) {
                    case eTipoDeTransaccion.FACTURA:
                        vSql.Append($" {vUtilSql.EnumToSqlValue((int)eTipoDocumentoFactura.Factura)}");
                        break;
                    case eTipoDeTransaccion.NOTADECREDITO:
                        vSql.Append($" {vUtilSql.EnumToSqlValue((int)eTipoDocumentoFactura.NotaDeCredito)}");
                        break;
                    case eTipoDeTransaccion.NOTADEDEBITO:
                        vSql.Append($" {vUtilSql.EnumToSqlValue((int)eTipoDocumentoFactura.NotaDeDebito)}");
                        break;
                }
            }
            vSql.AppendLine($" ELSE {vUtilSql.EnumToSqlValue((int)eTipoDocumentoFactura.Factura)}");
            vSql.AppendLine(" END)");
            return vSql.ToString();
        }
        private string SqlPorcentajeDeComision(bool valUsaAsignacionDeComisionEnCobranza) {
            string vResult = "Vendedor.comisionPorCobro";
            if (valUsaAsignacionDeComisionEnCobranza) {
                vResult = "Cobranza.ComisionVendedor";
            }
            return vResult;
        }
        string SqlComisionesPorCobranzaSegunOrigen(string valCodigoVendedor, eOrigenFacturacionOManual valOrigenCxC) {
            QAdvSql insSql = new QAdvSql("");
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("SELECT");
            vSql.AppendLine("   CTE_CobranzaVsVendedorVsMonedaVsDocCobradoCxCVsFactVsCliente.CodigoDelVendedor, ");
            vSql.AppendLine("   CTE_CobranzaVsVendedorVsMonedaVsDocCobradoCxCVsFactVsCliente.NombreDelVendedor, ");
            vSql.AppendLine("   CTE_CobranzaVsVendedorVsMonedaVsDocCobradoCxCVsFactVsCliente.CodigoMonedaCobranza, ");
            vSql.AppendLine("   CTE_CobranzaVsVendedorVsMonedaVsDocCobradoCxCVsFactVsCliente.MonedaCobranza,");
            vSql.AppendLine("   CTE_CobranzaVsVendedorVsMonedaVsDocCobradoCxCVsFactVsCliente.FechaDoc, ");
            vSql.AppendLine("   CTE_CobranzaVsVendedorVsMonedaVsDocCobradoCxCVsFactVsCliente.NumeroDoc, ");
            vSql.AppendLine("   CTE_CobranzaVsVendedorVsMonedaVsDocCobradoCxCVsFactVsCliente.NumeroDelDocumento, ");
            vSql.AppendLine("   CTE_CobranzaVsVendedorVsMonedaVsDocCobradoCxCVsFactVsCliente.NombreCliente, ");
            vSql.AppendLine("   CTE_CobranzaVsVendedorVsMonedaVsDocCobradoCxCVsFactVsCliente.SimboloMonedaDoc, ");
            vSql.AppendLine("   CTE_CobranzaVsVendedorVsMonedaVsDocCobradoCxCVsFactVsCliente.CambioABolivaresDoc, ");
            vSql.AppendLine("   CTE_CobranzaVsVendedorVsMonedaVsDocCobradoCxCVsFactVsCliente.PorcentajeDeComision, ");
            vSql.AppendLine("   CTE_CobranzaVsVendedorVsMonedaVsDocCobradoCxCVsFactVsCliente.MontoAbonadoOriginal, ");
            vSql.AppendLine("   CTE_CobranzaVsVendedorVsMonedaVsDocCobradoCxCVsFactVsCliente.MontoAbonado, ");
            if (valOrigenCxC == eOrigenFacturacionOManual.Manual) {
                vSql.AppendLine("   CTE_CobranzaVsVendedorVsMonedaVsDocCobradoCxCVsFactVsCliente.MontoComisionableCxC AS MontoComisionable, ");
                vSql.AppendLine("   CTE_CobranzaVsVendedorVsMonedaVsDocCobradoCxCVsFactVsCliente.MontoComisionableCxCEnMonedaLocal AS MontoComisionableEnMonedaLocal");
            } else {
                vSql.AppendLine("   CTE_CobranzaVsVendedorVsMonedaVsDocCobradoCxCVsFactVsCliente.MontoComisionableFact AS MontoComisionable, ");
                vSql.AppendLine("   CTE_CobranzaVsVendedorVsMonedaVsDocCobradoCxCVsFactVsCliente.MontoComisionableFactEnMonedaLocal AS MontoComisionableEnMonedaLocal");
            }
            vSql.AppendLine("   FROM CTE_CobranzaVsVendedorVsMonedaVsDocCobradoCxCVsFactVsCliente");
            vSql.AppendLine("   WHERE CTE_CobranzaVsVendedorVsMonedaVsDocCobradoCxCVsFactVsCliente.Origen = " + insSql.EnumToSqlValue((int)valOrigenCxC));
            if (!LibString.IsNullOrEmpty(valCodigoVendedor, true)) {
                vSql.AppendLine("      AND CTE_CobranzaVsVendedorVsMonedaVsDocCobradoCxCVsFactVsCliente.CodigoDelVendedor = " + insSql.ToSqlValue(valCodigoVendedor));
            }
            return vSql.ToString();
        }
        string SqlCTECxC(int valConsecutivoCompania) {
            QAdvSql insSql = new QAdvSql("");
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("CTE_CxC AS (");
            vSql.AppendLine("SELECT");
            vSql.AppendLine("	cxC.ConsecutivoCompania,");
            vSql.AppendLine("	cxC.Numero,");
            vSql.AppendLine("	cxC.TipoCxC,");
            vSql.AppendLine("	cxC.CodigoMoneda,");
            vSql.AppendLine("	cxC.CodigoCliente,");
            vSql.AppendLine("	cxC.NumeroDocumentoOrigen,");
            vSql.AppendLine("	cxC.MontoIVA,");
            vSql.AppendLine("	(cxC.MontoExento + cxC.MontoGravado + cxC.MontoIVA) AS TotalCxC,");
            vSql.AppendLine("	(cxC.MontoExento + cxC.MontoGravado) AS TotalExentoMasGravadoCxC,");
            vSql.AppendLine("	cxC.Origen,");
            vSql.AppendLine("	" + SqlConvertirElTipoDeDocumentoCxCATipodeDocumentoFactura() + " AS TipoCxCTipoFact");
            vSql.AppendLine("FROM cxC");
            vSql.AppendLine("WHERE cxC.ConsecutivoCompania = " + insSql.ToSqlValue(valConsecutivoCompania));
            vSql.AppendLine("	AND (cxC.MontoAbonado + cxC.MontoIVA + cxC.MontoExento) <> 0");
            vSql.AppendLine(")");
            return vSql.ToString();
        }
        string SqlCTERenglonesFactura(int valConsecutivoCompania) {
            QAdvSql insSql = new QAdvSql("");
            StringBuilder vSql = new StringBuilder();
            string vTiposDeDocsFactNCND = insSql.EnumToSqlValue((int)eTipoDocumentoFactura.Factura) + ", " + insSql.EnumToSqlValue((int)eTipoDocumentoFactura.NotaDeCredito) + ", " + insSql.EnumToSqlValue((int)eTipoDocumentoFactura.NotaDeDebito);
            vSql.AppendLine("CTE_RenglonesFactura AS(");
            vSql.AppendLine("SELECT");
            vSql.AppendLine("	F.ConsecutivoCompania,");
            vSql.AppendLine("	F.Numero AS NumeroFactura,");
            vSql.AppendLine("	F.TipoDeDocumento,");
            vSql.AppendLine("	SUM(ROUND((RF.PrecioSinIVA * RF.Cantidad) * (1 - (RF.PorcentajeDescuento / 100)) * (1 - (F.PorcentajeDescuento / 100)), 2) ) AS TotalRenglonEnMonedaOriginal");
            vSql.AppendLine("FROM factura F INNER JOIN renglonFactura RF ON F.ConsecutivoCompania = RF.ConsecutivoCompania AND F.Numero = RF.NumeroFactura AND F.TipoDeDocumento = RF.TipoDeDocumento");
            vSql.AppendLine("	INNER JOIN articuloInventario AI ON RF.ConsecutivoCompania = AI.ConsecutivoCompania AND RF.Articulo = AI.Codigo");
            vSql.AppendLine("WHERE F.ConsecutivoCompania = " + insSql.ToSqlValue(valConsecutivoCompania));
            vSql.AppendLine("AND AI.ExcluirDeComision = " + insSql.ToSqlValue(false));
            vSql.AppendLine("AND F.TipoDeDocumento IN (" + vTiposDeDocsFactNCND + ")");
            vSql.AppendLine("GROUP BY F.ConsecutivoCompania, F.Numero, F.TipoDeDocumento");
            vSql.AppendLine(" UNION ");
            vSql.AppendLine("SELECT");
            vSql.AppendLine("	F.ConsecutivoCompania,");
            vSql.AppendLine("	F.Numero AS NumeroFactura,");
            vSql.AppendLine("	F.TipoDeDocumento,");
            vSql.AppendLine("	SUM(ISNULL(ROC.TotalRenglon, 0)) AS TotalRenglonEnMonedaOriginal");
            vSql.AppendLine("FROM factura F INNER JOIN renglonDetalleDeOtrosCargosFactura ROC ON F.TipoDeDocumento = ROC.TipoDeDocumento AND F.Numero = ROC.NumeroFactura AND F.ConsecutivoCompania = ROC.ConsecutivoCompania");
            vSql.AppendLine("	INNER JOIN otrosCargosDeFactura OCF  ON ROC.CodigoDeCargo = OCF.Codigo AND ROC.ConsecutivoCompania = OCF.ConsecutivoCompania");
            vSql.AppendLine("WHERE F.ConsecutivoCompania = " + insSql.ToSqlValue(valConsecutivoCompania));
            vSql.AppendLine("AND OCF.ExcluirDeComision = " + insSql.ToSqlValue(false));
            vSql.AppendLine("AND OCF.ComoAplicaAlTotalFactura <> '2' ");
            vSql.AppendLine("AND F.TipoDeDocumento IN (" + vTiposDeDocsFactNCND + ")");
            vSql.AppendLine("GROUP BY F.ConsecutivoCompania, F.Numero, F.TipoDeDocumento");
            vSql.AppendLine(")");
            return vSql.ToString();
        }
        string SqlCTETotalRenglonesFact() {
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("CTE_TotalRenglonesFact AS (");
            vSql.AppendLine("SELECT");
            vSql.AppendLine("	CTE_RenglonesFactura.ConsecutivoCompania,");
            vSql.AppendLine("	CTE_RenglonesFactura.NumeroFactura,");
            vSql.AppendLine("	CTE_RenglonesFactura.TipoDeDocumento,");
            vSql.AppendLine("	SUM(CTE_RenglonesFactura.TotalRenglonEnMonedaOriginal) AS TotalRenglonEnMonedaOriginal");
            vSql.AppendLine("FROM CTE_RenglonesFactura");
            vSql.AppendLine("	GROUP BY CTE_RenglonesFactura.ConsecutivoCompania, CTE_RenglonesFactura.NumeroFactura, CTE_RenglonesFactura.TipoDeDocumento");
            vSql.AppendLine(")");
            return vSql.ToString();
        }
        string SqlCTEFact(int valConsecutivoCompania, string valCodigoMonedaLocal) {
            QAdvSql insSql = new QAdvSql("");
            StringBuilder vSql = new StringBuilder();
            string vSqlCodigoMonedaLocal = insSql.ToSqlValue(valCodigoMonedaLocal);
            string vTiposDeDocsFactNCND = insSql.EnumToSqlValue((int)eTipoDocumentoFactura.Factura) + ", " + insSql.EnumToSqlValue((int)eTipoDocumentoFactura.NotaDeCredito) + ", " + insSql.EnumToSqlValue((int)eTipoDocumentoFactura.NotaDeDebito);
            vSql.AppendLine("CTE_Fact AS (");
            vSql.AppendLine("SELECT");
            vSql.AppendLine("	factura.ConsecutivoCompania,");
            vSql.AppendLine("	factura.Numero,");
            vSql.AppendLine("	factura.TipoDeDocumento,");
            vSql.AppendLine("	ISNULL(factura.CodigoMoneda, '') AS CodigoMoneda,");
            vSql.AppendLine("	" + insSql.IIF("ISNULL(factura.CambioMonedaCXC, 1) = 0", "1", "factura.CambioMonedaCXC", true) + " AS CambioMonedaCXC,");
            vSql.AppendLine("	" + insSql.IIF("ISNULL(factura.CodigoMoneda, '') = " + vSqlCodigoMonedaLocal, "ISNULL(factura.IGTFML, 0)", "ISNULL(factura.IGTFME, 0)", true) + " AS TotalIGTF,");
            vSql.AppendLine("	(factura.TotalIVA + " + insSql.IIF("ISNULL(factura.CodigoMoneda, '') = " + vSqlCodigoMonedaLocal, "ISNULL(factura.IGTFML, 0)", "ISNULL(factura.IGTFME, 0)", true) + ") AS TotalIvaMasTotalIGTF,");
            vSql.AppendLine("	CTE_TotalRenglonesFact.TotalRenglonEnMonedaOriginal AS TotalExentoMasGravadoFact");
            vSql.AppendLine("FROM factura INNER JOIN CTE_TotalRenglonesFact ON factura.ConsecutivoCompania = CTE_TotalRenglonesFact.ConsecutivoCompania AND");
            vSql.AppendLine("	factura.Numero = CTE_TotalRenglonesFact.NumeroFactura AND factura.TipoDeDocumento = CTE_TotalRenglonesFact.TipoDeDocumento");
            vSql.AppendLine("WHERE factura.ConsecutivoCompania = " + insSql.ToSqlValue(valConsecutivoCompania));
            vSql.AppendLine("AND factura.TipoDeDocumento IN (" + vTiposDeDocsFactNCND + ")");
            vSql.AppendLine(")");
            return vSql.ToString();
        }
        string SqlCTECxCVsFactVsCliente(int valConsecutivoCompania, string valCodigoMonedaLocal) {
            QAdvSql insSql = new QAdvSql("");
            StringBuilder vSql = new StringBuilder();
            string vSqlCodigoMonedaLocal = insSql.ToSqlValue(valCodigoMonedaLocal);
            vSql.AppendLine("CTE_CxCVsFactVsCliente AS (");
            vSql.AppendLine("SELECT");
            vSql.AppendLine("	CTE_CxC.ConsecutivoCompania,");
            vSql.AppendLine("	CTE_CxC.Numero,");
            vSql.AppendLine("	CTE_CxC.TipoCxC,");
            vSql.AppendLine("	CTE_CxC.CodigoMoneda AS CodigoMonedaCxC,");
            vSql.AppendLine("	CTE_CxC.CodigoCliente,");
            vSql.AppendLine("	CTE_CxC.MontoIVA,");
            vSql.AppendLine("	CTE_CxC.TotalCxC,");
            vSql.AppendLine("	ISNULL(CTE_Fact.CodigoMoneda, '') AS CodigoMonedaFact,");
            vSql.AppendLine("	ISNULL(CTE_Fact.CambioMonedaCXC, 1) AS CambioFactMonedaCXC,");
            vSql.AppendLine("	(CTE_CxC.TotalExentoMasGravadoCxC -  ( CASE WHEN  ISNULL(CTE_Fact.CodigoMoneda, '') = 'VED' THEN  ( CASE WHEN  ISNULL(CTE_Fact.CodigoMoneda, '') = CTE_CxC.CodigoMoneda THEN ISNULL(CTE_Fact.TotalIGTF, 0) ELSE  ROUND(ISNULL(CTE_Fact.TotalIGTF, 0) / ISNULL(CTE_Fact.CambioMonedaCXC, 1), 2)  END )  ELSE ISNULL(CTE_Fact.TotalIGTF, 0) END ) ) AS TotalExentoMasGravadoCxC, "); 
            vSql.AppendLine("	" + insSql.IIF("ISNULL(CTE_Fact.CodigoMoneda, '') = " + vSqlCodigoMonedaLocal, insSql.IIF("ISNULL(CTE_Fact.CodigoMoneda, '') = CTE_CxC.CodigoMoneda", "ISNULL(CTE_Fact.TotalIvaMasTotalIGTF, 0)", insSql.RoundToNDecimals("ISNULL(CTE_Fact.TotalIvaMasTotalIGTF, 0) / ISNULL(CTE_Fact.CambioMonedaCXC, 1)", 2), true), "ISNULL(CTE_Fact.TotalIvaMasTotalIGTF, 0)", true) + " AS TotalIvaMasTotalIGTFFact,");
            vSql.AppendLine("	" + insSql.IIF("ISNULL(CTE_Fact.CodigoMoneda, '') = " + vSqlCodigoMonedaLocal, insSql.IIF("ISNULL(CTE_Fact.CodigoMoneda, '') = CTE_CxC.CodigoMoneda", "ISNULL(CTE_Fact.TotalIGTF, 0)", insSql.RoundToNDecimals("ISNULL(CTE_Fact.TotalIGTF, 0) / ISNULL(CTE_Fact.CambioMonedaCXC, 1)", 2), true), "ISNULL(CTE_Fact.TotalIGTF, 0)", true) + " AS TotalIGTF,");
            vSql.AppendLine("	" + insSql.IIF("ISNULL(CTE_Fact.CodigoMoneda, '') = " + vSqlCodigoMonedaLocal, insSql.IIF("ISNULL(CTE_Fact.CodigoMoneda, '') = CTE_CxC.CodigoMoneda", "ISNULL(CTE_Fact.TotalExentoMasGravadoFact, 0)", insSql.RoundToNDecimals("ISNULL(CTE_Fact.TotalExentoMasGravadoFact, 0) / ISNULL(CTE_Fact.CambioMonedaCXC, 1)", 2), true), "ISNULL(CTE_Fact.TotalExentoMasGravadoFact, 0)", true) + " AS TotalExentoMasGravadoFact,");
            vSql.AppendLine("	CTE_CxC.NumeroDocumentoOrigen,");
            vSql.AppendLine("	CTE_CxC.Origen,");
            vSql.AppendLine("	cliente.Nombre AS NombreCliente ");
            vSql.AppendLine("FROM CTE_CxC INNER JOIN cliente ON CTE_CxC.ConsecutivoCompania = cliente.ConsecutivoCompania AND CTE_CxC.CodigoCliente = cliente.Codigo");
            vSql.AppendLine(" INNER JOIN CTE_Fact ON CTE_CxC.ConsecutivoCompania = CTE_Fact.ConsecutivoCompania AND CTE_CxC.NumeroDocumentoOrigen = CTE_Fact.Numero AND CTE_CxC.TipoCxCTipoFact = CTE_Fact.TipoDeDocumento");
            vSql.AppendLine(" WHERE cliente.ConsecutivoCompania = " + insSql.ToSqlValue(valConsecutivoCompania) + " AND CTE_CXC.Origen = " + insSql.EnumToSqlValue((int)eOrigenFacturacionOManual.Factura));
            vSql.AppendLine(" UNION");
            vSql.AppendLine(" SELECT");
            vSql.AppendLine(" CTE_CxC.ConsecutivoCompania,");
            vSql.AppendLine(" CTE_CxC.Numero,");
            vSql.AppendLine(" CTE_CxC.TipoCxC,");
            vSql.AppendLine(" CTE_CxC.CodigoMoneda AS CodigoMonedaCxC,");
            vSql.AppendLine(" CTE_CxC.CodigoCliente,");
            vSql.AppendLine(" CTE_CxC.MontoIVA,");
            vSql.AppendLine(" CTE_CxC.TotalCxC,");
            vSql.AppendLine(" '' AS CodigoMonedaFact,");
            vSql.AppendLine(" 1 AS CambioFactMonedaCXC,");
            vSql.AppendLine(" 0 AS TotalExentoMasGravadoCxC,");
            vSql.AppendLine(" 0 AS TotalIvaMasTotalIGTFFact,");
            vSql.AppendLine(" 0 AS TotalIGTF,");
            vSql.AppendLine(" 0 TotalExentoMasGravadoFact,");
            vSql.AppendLine(" CTE_CxC.NumeroDocumentoOrigen,");
            vSql.AppendLine(" CTE_CxC.Origen,");
            vSql.AppendLine(" cliente.Nombre AS NombreCliente ");
            vSql.AppendLine(" FROM CTE_CxC INNER JOIN cliente ON CTE_CxC.ConsecutivoCompania = cliente.ConsecutivoCompania AND CTE_CxC.CodigoCliente = cliente.Codigo");
            vSql.AppendLine(" WHERE cliente.ConsecutivoCompania = " + insSql.ToSqlValue(valConsecutivoCompania) + " AND CTE_CxC.Origen = " + insSql.EnumToSqlValue((int)eOrigenFacturacionOManual.Manual));
            vSql.AppendLine(")");
            return vSql.ToString();
        }
        string SqlCTECxCVsFactVsClienteCalculado() {
            QAdvSql insSql = new QAdvSql("");
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("CTE_CxCVsFactVsClienteCalculado AS (");
            vSql.AppendLine("SELECT");
            vSql.AppendLine("	CTE_CxCVsFactVsCliente.ConsecutivoCompania,");
            vSql.AppendLine("	CTE_CxCVsFactVsCliente.Numero,");
            vSql.AppendLine("	CTE_CxCVsFactVsCliente.TipoCxC,");
            vSql.AppendLine("	CTE_CxCVsFactVsCliente.CodigoMonedaCxC,");
            vSql.AppendLine("	CTE_CxCVsFactVsCliente.CodigoCliente,");
            vSql.AppendLine("	CTE_CxCVsFactVsCliente.MontoIVA,");
            vSql.AppendLine("	CTE_CxCVsFactVsCliente.TotalCxC,");
            vSql.AppendLine("	CTE_CxCVsFactVsCliente.TotalExentoMasGravadoCxC,");
            vSql.AppendLine("	CTE_CxCVsFactVsCliente.NumeroDocumentoOrigen,");
            vSql.AppendLine("	CTE_CxCVsFactVsCliente.Origen,");
            vSql.AppendLine("	CTE_CxCVsFactVsCliente.CodigoMonedaFact,");
            vSql.AppendLine("	CTE_CxCVsFactVsCliente.CambioFactMonedaCXC,");
            vSql.AppendLine("	CTE_CxCVsFactVsCliente.TotalIvaMasTotalIGTFFact,");
            vSql.AppendLine("	CTE_CxCVsFactVsCliente.TotalIGTF,");
            vSql.AppendLine("	(CTE_CxCVsFactVsCliente.MontoIVA + CTE_CxCVsFactVsCliente.TotalIGTF) AS MontoIvaCxCMasIGTFFact,");
            vSql.AppendLine("	" + insSql.IIF("(CTE_CxCVsFactVsCliente.MontoIVA + CTE_CxCVsFactVsCliente.TotalIGTF) = 0", "0", "(CTE_CxCVsFactVsCliente.TotalExentoMasGravadoCxC / (CTE_CxCVsFactVsCliente.MontoIVA + CTE_CxCVsFactVsCliente.TotalIGTF))", true) + " AS TotalExentoMasGravadoCxCEntreIvaMasIGTFFact,");
            vSql.AppendLine("	" + insSql.IIF("CTE_CxCVsFactVsCliente.TotalIvaMasTotalIGTFFact = 0", "0", "((CTE_CxCVsFactVsCliente.TotalExentoMasGravadoFact)/ CTE_CxCVsFactVsCliente.TotalIvaMasTotalIGTFFact)", true) + " AS TotalExentoMasGravadoFactEntreIvaMasIgtfFact,");
            vSql.AppendLine("	CTE_CxCVsFactVsCliente.NombreCliente");
            vSql.AppendLine("FROM CTE_CxCVsFactVsCliente");
            vSql.AppendLine(")");
            return vSql.ToString();
        }
        string SqlCTEDocCobradoCxCVsFactVsCliente(int valConsecutivoCompania) {
            QAdvSql insSql = new QAdvSql("");
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("CTE_DocCobradoCxCVsFactVsCliente AS (");
            vSql.AppendLine("SELECT");
            vSql.AppendLine("	DocCob.ConsecutivoCompania,");
            vSql.AppendLine("	DocCob.NumeroCobranza,");
            vSql.AppendLine("	DocCob.MontoAbonado,");
            vSql.AppendLine("	DocCob.NumeroDelDocumentoCobrado,");
            vSql.AppendLine("	CTE_CxCVsFactVsClienteCalculado.Numero AS NumeroDoc,");
            vSql.AppendLine("	DocCob.CambioAMonedaDeCobranza,");
            vSql.AppendLine("	CTE_CxCVsFactVsClienteCalculado.CodigoCliente,");
            vSql.AppendLine("	CTE_CxCVsFactVsClienteCalculado.NombreCliente,");
            vSql.AppendLine("	CTE_CxCVsFactVsClienteCalculado.TotalCxC,");
            vSql.AppendLine("	" + insSql.RoundToNDecimals("DocCob.MontoAbonado - " + insSql.IIF("CTE_CxCVsFactVsClienteCalculado.MontoIvaCxCMasIGTFFact = 0", "0", "DocCob.MontoAbonado/ (1 + CTE_CxCVsFactVsClienteCalculado.TotalExentoMasGravadoCxCEntreIvaMasIGTFFact)", true), 2) + " AS MontoComisionableCxC,");
            vSql.AppendLine("	" + insSql.RoundToNDecimals("DocCob.MontoAbonado - " + insSql.IIF("CTE_CxCVsFactVsClienteCalculado.TotalIvaMasTotalIGTFFact = 0", "0", "DocCob.MontoAbonado/ (1 + CTE_CxCVsFactVsClienteCalculado.TotalExentoMasGravadoFactEntreIvaMasIgtfFact)", true), 2) + " AS MontoComisionableFact,");
            vSql.AppendLine("	CTE_CxCVsFactVsClienteCalculado.Origen");
            vSql.AppendLine("FROM documentoCobrado AS DocCob INNER JOIN CTE_CxCVsFactVsClienteCalculado ON");
            vSql.AppendLine("	DocCob.ConsecutivoCompania = CTE_CxCVsFactVsClienteCalculado.ConsecutivoCompania");
            vSql.AppendLine("	AND DocCob.NumeroDelDocumentoCobrado = CTE_CxCVsFactVsClienteCalculado.Numero");
            vSql.AppendLine("	AND DocCob.ConsecutivoCompania = CTE_CxCVsFactVsClienteCalculado.ConsecutivoCompania");
            vSql.AppendLine("	AND DocCob.TipoDeDocumentoCobrado = CTE_CxCVsFactVsClienteCalculado.TipoCxC");
            vSql.AppendLine("WHERE DocCob.ConsecutivoCompania = " + insSql.ToSqlValue(valConsecutivoCompania));
            vSql.AppendLine(")");
            return vSql.ToString();
        }
        string SqlCTECobranzaVsVendedorVsMoneda(int valConsecutivoCompania, DateTime valFechaDesde, DateTime valFechaHasta, bool valUsaAsignacionDeComisionEnCobranza, Saw.Lib.eTipoDeInforme valTipoDeInforme, Saw.Lib.eMonedaParaImpresion valMonedaDeReporte) {
            QAdvSql insSql = new QAdvSql("");
            StringBuilder vSql = new StringBuilder();
            string vSqlStatusCobranzaVigente = insSql.EnumToSqlValue((int)eStatusCobranza.Vigente);
            string vSqlFechaCobranzaBetween = insSql.SqlDateValueBetween("", "Cobranza.Fecha", valFechaDesde, valFechaHasta);
            string vSqlPorcentajeDeComision = SqlPorcentajeDeComision(valUsaAsignacionDeComisionEnCobranza);
            vSql.AppendLine("CTE_CobranzaVsVendedorVsMoneda AS (");
            vSql.AppendLine("SELECT");
            vSql.AppendLine("	cobranza.ConsecutivoCompania,");
            vSql.AppendLine("	cobranza.Numero,");
            vSql.AppendLine("	vendedor.Codigo AS CodigoDelVendedor,");
            vSql.AppendLine("	vendedor.Nombre AS NombreDelVendedor,");
            vSql.AppendLine("	" + vSqlPorcentajeDeComision + " AS PorcentajeDeComision,");
            if (valTipoDeInforme == Saw.Lib.eTipoDeInforme.Resumido && valMonedaDeReporte == Saw.Lib.eMonedaParaImpresion.EnBolivares) {
                vSql.AppendLine("	'VED' AS CodigoMonedaCobranza,");
                vSql.AppendLine("   'Bolívar' AS MonedaCobranza,");
                vSql.AppendLine("	'Bs.' AS SimboloMonedaDoc,");
            } else {
                vSql.AppendLine("	cobranza.CodigoMoneda AS CodigoMonedaCobranza,");
                vSql.AppendLine("   moneda.Nombre AS MonedaCobranza,");
                vSql.AppendLine("	Moneda.Simbolo AS SimboloMonedaDoc,");
            }
            vSql.AppendLine("	Cobranza.Fecha AS FechaDoc,");
            vSql.AppendLine("	'Cob. ' + Cobranza.Numero + ' / Doc. ' AS NumeroCob,");
            vSql.AppendLine("	Cobranza.CambioABolivares");
            vSql.AppendLine("FROM cobranza INNER JOIN vendedor");
            vSql.AppendLine("	ON cobranza.CodigoCobrador = Vendedor.Codigo");
            vSql.AppendLine("	AND cobranza.ConsecutivoCompania = Vendedor.ConsecutivoCompania");
            vSql.AppendLine(" INNER JOIN Moneda");
            vSql.AppendLine("	ON Moneda.Codigo = Cobranza.CodigoMoneda");
            vSql.AppendLine("WHERE cobranza.ConsecutivoCompania = " + insSql.ToSqlValue(valConsecutivoCompania));
            vSql.AppendLine("	AND " + vSqlFechaCobranzaBetween);
            vSql.AppendLine("	AND Cobranza.StatusCobranza = " + vSqlStatusCobranzaVigente);
            vSql.AppendLine(")");
            return vSql.ToString();
        }
        string SqlCTECobranzaVsVendedorVsMonedaVsDocCobradoCxCVsFactVsCliente(Saw.Lib.eMonedaParaImpresion valMonedaDeReporte, Saw.Lib.eTasaDeCambioParaImpresion valTasaDeCambio) {
            QAdvSql insSql = new QAdvSql("");
            StringBuilder vSql = new StringBuilder();
            string vSqlCambioABolivaresDoc = "CTE_DocCobradoCxCVsFactVsCliente.CambioAMonedaDeCobranza";
            if (valMonedaDeReporte == Saw.Lib.eMonedaParaImpresion.EnBolivares && valTasaDeCambio == Saw.Lib.eTasaDeCambioParaImpresion.DelDia) {
                vSqlCambioABolivaresDoc = _LibSaw.CampoMontoPorTasaDeCambioSegunCodMonedaSql(string.Empty, "CTE_CobranzaVsVendedorVsMoneda.CodigoMonedaCobranza", "1", false, string.Empty);
            }
            vSql.AppendLine("CTE_CobranzaVsVendedorVsMonedaVsDocCobradoCxCVsFactVsCliente AS(");
            vSql.AppendLine("SELECT");
            vSql.AppendLine("   CTE_CobranzaVsVendedorVsMoneda.CodigoDelVendedor, ");
            vSql.AppendLine("   CTE_CobranzaVsVendedorVsMoneda.NombreDelVendedor, ");
            vSql.AppendLine("   CTE_CobranzaVsVendedorVsMoneda.CodigoMonedaCobranza, ");
            vSql.AppendLine("   CTE_CobranzaVsVendedorVsMoneda.MonedaCobranza,");
            vSql.AppendLine("   CTE_CobranzaVsVendedorVsMoneda.FechaDoc, ");
            vSql.AppendLine("   CTE_DocCobradoCxCVsFactVsCliente.Origen,");
            vSql.AppendLine("   CTE_DocCobradoCxCVsFactVsCliente.NumeroDoc, ");
            vSql.AppendLine("   (CTE_CobranzaVsVendedorVsMoneda.NumeroCob + CTE_DocCobradoCxCVsFactVsCliente.NumeroDelDocumentoCobrado) AS NumeroDelDocumento,");
            vSql.AppendLine("   CTE_DocCobradoCxCVsFactVsCliente.NombreCliente, ");
            vSql.AppendLine("   CTE_CobranzaVsVendedorVsMoneda.SimboloMonedaDoc,");
            vSql.AppendLine("   " + vSqlCambioABolivaresDoc + " AS CambioABolivaresDoc, ");
            vSql.AppendLine("   CTE_CobranzaVsVendedorVsMoneda.PorcentajeDeComision, ");
            vSql.AppendLine("   CTE_DocCobradoCxCVsFactVsCliente.MontoAbonado AS MontoAbonadoOriginal, ");
            vSql.AppendLine("   ROUND(CTE_DocCobradoCxCVsFactVsCliente.MontoAbonado * " + vSqlCambioABolivaresDoc + ", 2) AS MontoAbonado,");
            vSql.AppendLine("   CTE_DocCobradoCxCVsFactVsCliente.MontoComisionableCxC AS MontoComisionableCxC, ");
            vSql.AppendLine("   CTE_DocCobradoCxCVsFactVsCliente.MontoComisionableFact AS MontoComisionableFact, ");
            vSql.AppendLine("   (CASE WHEN CTE_CobranzaVsVendedorVsMoneda.MonedaCobranza ='VED' THEN CTE_DocCobradoCxCVsFactVsCliente.MontoComisionableCxC ELSE ROUND(CTE_DocCobradoCxCVsFactVsCliente.MontoComisionableCxC * " + vSqlCambioABolivaresDoc + ", 2) END) AS MontoComisionableCxCEnMonedaLocal,");
            vSql.AppendLine("   (CASE WHEN CTE_CobranzaVsVendedorVsMoneda.MonedaCobranza ='VED' THEN CTE_DocCobradoCxCVsFactVsCliente.MontoComisionableFact ELSE ROUND(CTE_DocCobradoCxCVsFactVsCliente.MontoComisionableFact * " + vSqlCambioABolivaresDoc + ", 2) END) AS MontoComisionableFactEnMonedaLocal");
            vSql.AppendLine("   FROM CTE_CobranzaVsVendedorVsMoneda INNER JOIN CTE_DocCobradoCxCVsFactVsCliente");
            vSql.AppendLine("	ON CTE_DocCobradoCxCVsFactVsCliente.NumeroCobranza = CTE_CobranzaVsVendedorVsMoneda.Numero");
            vSql.AppendLine("	AND CTE_DocCobradoCxCVsFactVsCliente.ConsecutivoCompania = CTE_CobranzaVsVendedorVsMoneda.ConsecutivoCompania");
            vSql.AppendLine("   WHERE CTE_DocCobradoCxCVsFactVsCliente.TotalCxC <> 0 ");
            vSql.AppendLine(")");
            return vSql.ToString();
        }
        #endregion //Comisiones por Vendedor
        #endregion //Metodos Generados
    } //End of class clsCobranzaSql

} //End of namespace Galac.Adm.Brl.Venta

