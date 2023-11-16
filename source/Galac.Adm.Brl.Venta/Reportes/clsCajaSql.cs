using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using Galac.Saw.Ccl.SttDef;
using Galac.Adm.Ccl.Venta;
using Galac.Adm.Ccl.CAnticipo;
using Galac.Comun.Ccl.TablasGen;
using LibGalac.Aos.Dal;
using Galac.Saw.Ccl.Tablas;

namespace Galac.Adm.Brl.Venta.Reportes {
    public class clsCajaSql {
        QAdvSql insSql;

        public clsCajaSql() {
            insSql = new QAdvSql("");
        }
        #region Metodos Generados

        public string SqlCuadreCajaCobroMultimonedaDetallado(int valConsecutivoCompania, DateTime valFechaInicial, DateTime valFechaFinal, Saw.Lib.eCantidadAImprimir valCantidadOperadorDeReporte, string valNombreDelOperador, Saw.Lib.eMonedaParaImpresion valMonedaDeReporte, bool valTotalesTipoCobro) {
            StringBuilder vSql = new StringBuilder();
            int vConsecutivoCajaGenerica = 0;
            string vSQLWhere = "";
            vSQLWhere = insSql.SqlExpressionValueWithAnd(vSQLWhere, "factura.StatusFactura", insSql.EnumToSqlValue((int)eStatusFactura.Emitida));
            vSQLWhere = vSQLWhere + " AND factura.ConsecutivoCaja" + " <> " + LibConvert.ToStr(vConsecutivoCajaGenerica);
            vSQLWhere = vSQLWhere + " AND factura.TipoDeDocumento IN ( " + insSql.EnumToSqlValue((int)eTipoDocumentoFactura.Factura) + "," + insSql.EnumToSqlValue((int)eTipoDocumentoFactura.ComprobanteFiscal) + "," + insSql.EnumToSqlValue((int)eTipoDocumentoFactura.NotaDeCredito) + "," + insSql.EnumToSqlValue((int)eTipoDocumentoFactura.NotaDeCreditoComprobanteFiscal) + ")";
            vSQLWhere = vSQLWhere + " AND (SAW.FormaDelCobro.TipoDePago  <> " + insSql.EnumToSqlValue((int)eTipoDeFormaDePago.VueltoEfectivo) + " AND SAW.FormaDelCobro.TipoDePago  <> " + insSql.EnumToSqlValue((int)eTipoDeFormaDePago.VueltoC2P) + ") ";
            vSQLWhere = insSql.SqlDateValueBetween(vSQLWhere, "factura.fecha", valFechaInicial, valFechaFinal);
            vSQLWhere = insSql.SqlExpressionValueWithAnd(vSQLWhere, "factura.GeneraCobroDirecto", insSql.ToSqlValue("S"));
            if (valCantidadOperadorDeReporte == Saw.Lib.eCantidadAImprimir.Uno) {
                vSQLWhere = insSql.SqlValueWithAnd(vSQLWhere, "factura.NombreOperador", valNombreDelOperador);
            }
            vSQLWhere = insSql.SqlIntValueWithAnd(vSQLWhere, "factura.ConsecutivoCompania", valConsecutivoCompania);
            string esFacturaValida = insSql.IIF("renglonCobroDeFactura.Monto IS NULL AND (factura.TotalFactura) > 0", "(factura.TotalFactura)", insSql.ToInt("0"), true);
            string esNotaDeCredito = insSql.IIF("factura.TipoDeDocumento IN (" + insSql.EnumToSqlValue((int)eTipoDocumentoFactura.NotaDeCredito) + "," + insSql.EnumToSqlValue((int)eTipoDocumentoFactura.NotaDeCreditoComprobanteFiscal) + ")", insSql.ToSqlValue("NOTAS DE CRÉDITO/DEVOLUCIONES"), insSql.ToSqlValue(string.Empty), false);
            string esFacturaCobrada = insSql.IIF("factura.TipoDeDocumento IN (" + insSql.EnumToSqlValue((int)eTipoDocumentoFactura.Factura) + "," + insSql.EnumToSqlValue((int)eTipoDocumentoFactura.ComprobanteFiscal) + ")", insSql.ToSqlValue("FACTURAS COBRADAS"), esNotaDeCredito, false);
            string esFacturaNoCobrada = insSql.IIF("SUM" + esFacturaValida + insSql.ComparisonOp(">") + insSql.ToInt("0") + "AND factura.TipoDeDocumento IN (" + insSql.EnumToSqlValue((int)eTipoDocumentoFactura.Factura) + "," + insSql.EnumToSqlValue((int)eTipoDocumentoFactura.ComprobanteFiscal) + ")", insSql.ToSqlValue("FACTURAS NO COBRADAS"), esFacturaCobrada, true);
            vSql.AppendLine(insSql.SetDateFormat());
            vSql.AppendLine(" ;WITH CTE_MonedasActivas(CodMoneda, NombreMoneda, SimboloMoneda)");
            vSql.AppendLine("	AS (SELECT");
            vSql.AppendLine("		Codigo AS CodMoneda,");
            vSql.AppendLine("		Nombre AS NombreMoneda,");
            vSql.AppendLine("		Simbolo AS SimboloMoneda");
            vSql.AppendLine("	    FROM dbo.Moneda");
            vSql.AppendLine("	    WHERE Activa = " + insSql.ToSqlValue(LibConvert.BoolToSN(true)));
            vSql.AppendLine("       AND TipoDeMoneda = " + LibConvert.ToInt(eTipoDeMoneda.Fisica));
            vSql.AppendLine("       )");
            vSql.AppendLine(" SELECT");
            vSql.AppendLine("	Caja.Consecutivo AS ConsecutivoCaja");
            vSql.AppendLine("	, Caja.NombreCaja");
            vSql.AppendLine("	, factura.Fecha");
            vSql.AppendLine("	, factura.NombreOperador AS NombreDelOperador");
            vSql.AppendLine("   , MonedaDoc.NombreMoneda AS NombreMoneda");
            vSql.AppendLine("   , " + esFacturaNoCobrada + " AS TipoDeDocumento");
            vSql.AppendLine("	, factura.Numero");
            vSql.AppendLine("	, factura.NumeroComprobanteFiscal");
            if (valMonedaDeReporte == Saw.Lib.eMonedaParaImpresion.EnMonedaOriginal) {
                vSql.AppendLine("	, MAX(factura.TotalFactura) AS TotalFactura");
                vSql.AppendLine(", " + insSql.IsNull("SUM(renglonCobroDeFactura.Monto)", insSql.ToSqlValue(string.Empty)) + " AS Monto");
                vSql.AppendLine("   , MAX(factura.CambioMostrarTotalEnDivisas) AS TasaOperacion");
            } else {
                vSql.AppendLine("	, MAX(factura.TotalFactura * factura.CambioABolivares) AS TotalFactura");
                vSql.AppendLine(", " + insSql.IsNull("SUM(renglonCobroDeFactura.Monto * renglonCobroDeFactura.CambioAMonedaLocal)", insSql.ToSqlValue(string.Empty)) + " AS Monto");
                vSql.AppendLine(", " + insSql.IsNull("MonedaRenglon.SimboloMoneda", insSql.ToSqlValue(string.Empty)) + " AS SimboloFormaDeCobro");
                vSql.AppendLine(", " + insSql.IsNull("MAX(renglonCobroDeFactura.CambioAMonedaLocal)", insSql.ToSqlValue(string.Empty)) + " AS CambioABolivares");
            }
            vSql.AppendLine(", " + insSql.IsNull("SAW.FormaDelCobro.TipoDePago", insSql.ToSqlValue(string.Empty)) + " AS TipoDeCobro");
            vSql.AppendLine("	, MonedaDoc.SimboloMoneda AS SimboloMonedaDoc");
            vSql.AppendLine(", " + insSql.IsNull("MonedaRenglon.CodMoneda", insSql.ToSqlValue(string.Empty)) + "AS CodMonedaFormaDelCobro");
            vSql.AppendLine(", " + insSql.IsNull("MonedaRenglon.NombreMoneda", insSql.ToSqlValue(string.Empty)) + "AS NombreMonedaFormaDelCobro");
            vSql.AppendLine(" FROM Adm.Caja");
            vSql.AppendLine(" LEFT JOIN factura");
            vSql.AppendLine("	ON factura.ConsecutivoCaja = Caja.Consecutivo");
            vSql.AppendLine("	AND factura.ConsecutivoCompania = Caja.ConsecutivoCompania");
            vSql.AppendLine(" LEFT JOIN dbo.renglonCobroDeFactura");
            vSql.AppendLine("	ON renglonCobroDeFactura.NumeroFactura = factura.Numero");
            vSql.AppendLine("	AND renglonCobroDeFactura.ConsecutivoCompania = factura.ConsecutivoCompania");
            vSql.AppendLine("   AND renglonCobroDeFactura.TipoDeDocumento = factura.TipoDeDocumento");

            vSql.AppendLine(" INNER JOIN SAW.FormaDelCobro");
            vSql.AppendLine("   ON SAW.FormaDelCobro.Codigo = renglonCobroDeFactura.CodigoFormaDelCobro");
            vSql.AppendLine(" INNER JOIN CTE_MonedasActivas AS MonedaDoc");
            vSql.AppendLine("	ON MonedaDoc.CodMoneda = factura.CodigoMoneda");
            vSql.AppendLine(" LEFT JOIN CTE_MonedasActivas AS MonedaRenglon");
            vSql.AppendLine("   ON MonedaRenglon.CodMoneda = renglonCobroDeFactura.CodigoMoneda");
            if (LibString.Len(vSQLWhere) > 0) {
                vSql.AppendLine(insSql.WhereSql(vSQLWhere));
            }
            vSql.AppendLine(" GROUP BY");
            vSql.AppendLine("	Caja.Consecutivo");
            vSql.AppendLine("	, Caja.NombreCaja");
            vSql.AppendLine("	, factura.TipoDeDocumento");
            vSql.AppendLine("	, factura.Fecha");
            vSql.AppendLine("	, factura.NombreOperador");
            vSql.AppendLine("   , factura.Moneda");
            vSql.AppendLine("	, factura.Numero");
            vSql.AppendLine("	, factura.NumeroComprobanteFiscal");
            vSql.AppendLine("	, factura.CodigoVendedor");
            vSql.AppendLine("	, TotalFactura");
            if (valMonedaDeReporte != Saw.Lib.eMonedaParaImpresion.EnMonedaOriginal) {
                vSql.AppendLine("	, MonedaRenglon.SimboloMoneda");
            }
            vSql.AppendLine("	, MonedaDoc.NombreMoneda");
            vSql.AppendLine("	, SAW.FormaDelCobro.TipoDePago");
            vSql.AppendLine("	, MonedaDoc.SimboloMoneda");
            vSql.AppendLine("	, MonedaRenglon.CodMoneda");
            vSql.AppendLine("	, MonedaRenglon.NombreMoneda");
            vSql.AppendLine(" ORDER BY");
            vSql.AppendLine("   factura.NombreOperador");
            vSql.AppendLine("   , Caja.NombreCaja");
            vSql.AppendLine("	, factura.Fecha");
            vSql.AppendLine("   , factura.TipoDeDocumento DESC");
            vSql.AppendLine("	, factura.Numero");
            vSql.AppendLine("	, MonedaRenglon.CodMoneda DESC");
            return vSql.ToString();
        }

        public string SqlCajasAperturadas(int valConsecutivoCompania) {
            int vConsecutivoCajaGenerica = 0;
            StringBuilder vSql = new StringBuilder();
            string vSQLWhere = "";
            vSQLWhere = insSql.SqlExpressionValueWithAnd(vSQLWhere, "CajaApertura.CajaCerrada", insSql.ToSqlValue("N"));
            vSQLWhere = vSQLWhere + " AND Caja.Consecutivo" + " <> " + LibConvert.ToStr(vConsecutivoCajaGenerica);
            vSQLWhere = insSql.SqlIntValueWithAnd(vSQLWhere, "Caja.ConsecutivoCompania", valConsecutivoCompania);
            vSql.AppendLine(" SELECT");
            vSql.AppendLine("   CajaApertura.ConsecutivoCaja AS ConsecutivoCaja");
            vSql.AppendLine("   ,  CajaApertura.NombreDelUsuario AS NombreDelUsuario");
            vSql.AppendLine("   ,  CajaApertura.HoraApertura AS HoraApertura");
            vSql.AppendLine("   ,  CajaApertura.Fecha AS FechaDeApertura");
            vSql.AppendLine("   ,  Caja.NombreCaja AS NombreCaja");
            vSql.AppendLine("   ,  Caja.Consecutivo AS ConsecutivoCaja");
            vSql.AppendLine("   ,  CajaApertura.MontoApertura AS MontoApertura");
            vSql.AppendLine(" FROM Adm.Caja");
            vSql.AppendLine(" INNER JOIN Adm.CajaApertura");
            vSql.AppendLine("   ON CajaApertura.ConsecutivoCompania = Caja.ConsecutivoCompania");
            vSql.AppendLine("   AND CajaApertura.ConsecutivoCaja = Caja.Consecutivo");
            if (LibString.Len(vSQLWhere) > 0) {
                vSql.AppendLine(" WHERE " + vSQLWhere);
            }
            vSql.AppendLine(" ORDER BY");
            vSql.AppendLine("   CajaApertura.ConsecutivoCaja,");
            vSql.AppendLine("   CajaApertura.Fecha DESC");
            return vSql.ToString();
        }

        public string SqlCuadreCajaPorTipoCobro(int valConsecutivoCompania, DateTime valFechaInicial, DateTime valFechaFinal, Saw.Lib.eMonedaParaImpresion valMonedaDeReporte, Saw.Lib.eTipoDeInforme valTipoDeInforme) {
            int vConsecutivoCajaGenerica = 0;
            string vSQLWhere = "";
            StringBuilder vSql = new StringBuilder();
            vSQLWhere = insSql.SqlExpressionValueWithAnd(vSQLWhere, "factura.StatusFactura", insSql.EnumToSqlValue((int)eStatusFactura.Emitida));
            vSQLWhere = vSQLWhere + " AND factura.ConsecutivoCaja" + " <> " + LibConvert.ToStr(vConsecutivoCajaGenerica);
            vSQLWhere = vSQLWhere + " AND factura.TipoDeDocumento IN ( " + insSql.EnumToSqlValue((int)eTipoDocumentoFactura.Factura) + "," + insSql.EnumToSqlValue((int)eTipoDocumentoFactura.ComprobanteFiscal) + "," + insSql.EnumToSqlValue((int)eTipoDocumentoFactura.NotaDeCredito) + "," + insSql.EnumToSqlValue((int)eTipoDocumentoFactura.NotaDeCreditoComprobanteFiscal) + ")";
            vSQLWhere = vSQLWhere + " AND (SAW.FormaDelCobro.TipoDePago  <> " + insSql.EnumToSqlValue((int)eTipoDeFormaDePago.VueltoEfectivo) + " AND SAW.FormaDelCobro.TipoDePago  <> " + insSql.EnumToSqlValue((int)eTipoDeFormaDePago.VueltoC2P) + ") ";
            vSQLWhere = insSql.SqlDateValueBetween(vSQLWhere, "factura.fecha", valFechaInicial, valFechaFinal);
            vSQLWhere = insSql.SqlExpressionValueWithAnd(vSQLWhere, "factura.GeneraCobroDirecto", insSql.ToSqlValue("S"));
            vSQLWhere = insSql.SqlIntValueWithAnd(vSQLWhere, "factura.ConsecutivoCompania", valConsecutivoCompania);
            string menorA = insSql.ComparisonOp("<");
            string mayorA = insSql.ComparisonOp(">");
            string montoRenglon = insSql.IsNull("renglonCobroDeFactura.Monto", insSql.ToInt("0"));
            string cambioDelRenglon = insSql.IsNull("renglonCobroDeFactura.CambioAMonedaLocal", insSql.ToInt("1"));
            string esEfectivo = "SAW.FormaDelCobro.TipoDePago" + insSql.ComparisonOp("=") + insSql.EnumToSqlValue((int)eFormaDeCobro.Efectivo);
            string esTarjeta = "SAW.FormaDelCobro.TipoDePago" + insSql.ComparisonOp("=") + insSql.EnumToSqlValue((int)eFormaDeCobro.Tarjeta);
            string esCheque = "SAW.FormaDelCobro.TipoDePago" + insSql.ComparisonOp("=") + insSql.EnumToSqlValue((int)eFormaDeCobro.Cheque);
            string monedaCobro = insSql.IsNull("MonedaRenglon.Nombre", "MonedaDoc.Nombre");
            string esDepTransferencia = "SAW.FormaDelCobro.TipoDePago" + insSql.ComparisonOp("=") + insSql.EnumToSqlValue((int)eFormaDeCobro.Deposito);
            string esNotaDeCredito = montoRenglon + menorA + insSql.ToInt("0") + insSql.LogicalOp("OR") + " factura.TotalFactura" + menorA + insSql.ToInt("0");
            string existeVuelto = "SUM(" + montoRenglon + ")" + mayorA + "factura.TotalFactura " + insSql.LogicalOp("AND") + "factura.TotalFactura" + mayorA + insSql.ToInt("0");
            string montoEfectivo = "SUM" + insSql.IIF(esEfectivo, montoRenglon, insSql.ToInt("0"), true);
            string montoTarjeta = "SUM" + insSql.IIF(esTarjeta, montoRenglon, insSql.ToInt("0"), true);
            string montoDepTranferencia = "SUM" + insSql.IIF(esDepTransferencia, montoRenglon, insSql.ToInt("0"), true);
            string montoCheque = "SUM" + insSql.IIF(esCheque, montoRenglon, insSql.ToInt("0"), true);
            string montoNotaDeCredito = "SUM" + insSql.IIF(esNotaDeCredito, montoRenglon, insSql.ToInt("0"), true);
            string monedasIguales = "renglonCobroDeFactura.CodigoMoneda" + insSql.ComparisonOp("=") + "factura.CodigoMoneda";
            string calculoMontoVuelto = insSql.IIF(existeVuelto, "SUM(" + montoRenglon + ") - factura.TotalFactura", insSql.ToInt("0"), true);
            string montoVuelto = insSql.IIF(monedasIguales, calculoMontoVuelto, insSql.ToInt("0"), true);
            string montoPagadoSinConvertir = "SUM" + insSql.IIF(montoRenglon + menorA + insSql.ToInt("0"), insSql.ToInt("0"), montoRenglon, true);
            string convEnRenglon = montoRenglon + " * " + insSql.IsNull("renglonCobroDeFactura.CambioAMonedaLocal", insSql.ToInt("1"));
            string montoPagadoConvertido = "SUM" + insSql.IIF(convEnRenglon + " IS NULL " + insSql.LogicalOp("OR") + convEnRenglon + insSql.ComparisonOp("<") + insSql.ToInt("0"), insSql.ToInt("0"), convEnRenglon, true);
            string montoPagado = insSql.IIF(monedasIguales, montoPagadoSinConvertir, montoPagadoConvertido, true);
            string esCredito = "MonedaRenglon.Nombre IS NULL" + insSql.LogicalOp("AND") + "factura.TotalFactura" + insSql.ComparisonOp("<>") + insSql.ToInt("0");
            string montoEfectivoEnBs = "SUM" + insSql.IIF(esEfectivo, montoRenglon + " * " + cambioDelRenglon, insSql.ToInt("0"), true);
            string montoTarjetaEnBs = "SUM" + insSql.IIF(esTarjeta, montoRenglon + " * " + cambioDelRenglon, insSql.ToInt("0"), true);
            string montoDepTranferenciaEnBs = "SUM" + insSql.IIF(esDepTransferencia, montoRenglon + " * " + cambioDelRenglon, insSql.ToInt("0"), true);
            string montoChequeEnBs = "SUM" + insSql.IIF(esCheque, montoRenglon + " * " + cambioDelRenglon, insSql.ToInt("0"), true);
            string montoNotaDeCreditoEnBs = "SUM" + insSql.IIF(esNotaDeCredito, montoRenglon + " * " + cambioDelRenglon, insSql.ToInt("0"), true);
            string montoCredito = insSql.IIF(esCredito, "factura.TotalFactura", insSql.ToInt("0"), true);
            vSql.AppendLine(insSql.SetDateFormat());
            vSql.AppendLine("   SELECT");
            vSql.AppendLine("   MonedaDoc.Nombre AS MonedaDoc");
            vSql.AppendLine("   , " + monedaCobro + " AS MonedaCobro");
            vSql.AppendLine("   , factura.ConsecutivoCaja AS ConsecutivoCaja");
            vSql.AppendLine("   , caja.NombreCaja AS NombreCaja");
            vSql.AppendLine("   , factura.Numero AS NumeroDoc");
            vSql.AppendLine("   , factura.NumeroComprobanteFiscal AS NumFiscal");
            vSql.AppendLine("   , factura.Fecha AS Fecha");
            vSql.AppendLine("   , cliente.Nombre AS NombreCliente");
            vSql.AppendLine("   , factura.TotalFactura AS MontoDoc");
            if (valMonedaDeReporte == Saw.Lib.eMonedaParaImpresion.EnMonedaOriginal) {
                vSql.AppendLine("   , " + montoEfectivo + " AS Efectivo");
                vSql.AppendLine("   , " + montoTarjeta + " AS Tarjeta");
                vSql.AppendLine("   , " + montoDepTranferencia + " AS Deposito");
                vSql.AppendLine("   , " + montoCheque + " AS Cheque");
                vSql.AppendLine("   , " + montoNotaDeCredito + " AS NotaDeCredito");
                vSql.AppendLine("   , " + montoPagadoSinConvertir + " AS MontoPagado");
            } else {
                vSql.AppendLine("   , " + montoEfectivoEnBs + " AS Efectivo");
                vSql.AppendLine("   , " + montoTarjetaEnBs + " AS Tarjeta");
                vSql.AppendLine("   , " + montoDepTranferenciaEnBs + " AS Deposito");
                vSql.AppendLine("   , " + montoChequeEnBs + " AS Cheque");
                vSql.AppendLine("   , " + montoNotaDeCreditoEnBs + " AS NotaDeCredito");
                vSql.AppendLine("   , " + montoPagado + " AS MontoPagado");
            }
            vSql.AppendLine("   , " + montoVuelto + " AS Vuelto");
            vSql.AppendLine("   , " + montoCredito + " AS VentaCredito");
            vSql.AppendLine("	, factura.CambioMostrarTotalEnDivisas AS CambioDeOperacion");
            vSql.AppendLine("   FROM Adm.Caja");
            vSql.AppendLine("   LEFT JOIN dbo.factura");
            vSql.AppendLine("       ON factura.ConsecutivoCaja = Caja.Consecutivo");
            vSql.AppendLine("	    AND factura.ConsecutivoCompania = Caja.ConsecutivoCompania");
            vSql.AppendLine("   LEFT JOIN dbo.Cliente");
            vSql.AppendLine("       ON Cliente.Codigo = factura.CodigoCliente");
            vSql.AppendLine("	    AND Cliente.ConsecutivoCompania = factura.ConsecutivoCompania");
            vSql.AppendLine("   LEFT JOIN dbo.renglonCobroDeFactura");
            vSql.AppendLine("       ON renglonCobroDeFactura.NumeroFactura = factura.Numero");
            vSql.AppendLine("	    AND renglonCobroDeFactura.ConsecutivoCompania = factura.ConsecutivoCompania");
            vSql.AppendLine("	    AND renglonCobroDeFactura.TipoDeDocumento = factura.TipoDeDocumento");
            vSql.AppendLine("   LEFT JOIN SAW.FormaDelCobro");
            vSql.AppendLine("       ON SAW.FormaDelCobro.Codigo = renglonCobroDeFactura.CodigoFormaDelCobro");
            vSql.AppendLine("   INNER JOIN dbo.Moneda AS MonedaDoc");
            vSql.AppendLine("       ON MonedaDoc.Codigo = factura.CodigoMoneda");
            vSql.AppendLine("   LEFT JOIN dbo.Moneda AS MonedaRenglon");
            vSql.AppendLine("       ON MonedaRenglon.Codigo = renglonCobroDeFactura.CodigoMoneda");
            if (LibString.Len(vSQLWhere) > 0) {
                vSql.AppendLine(insSql.WhereSql(vSQLWhere));
            }
            vSql.AppendLine("   GROUP BY");
            vSql.AppendLine("	    factura.Moneda");
            vSql.AppendLine("	    , factura.ConsecutivoCaja");
            vSql.AppendLine("	    , factura.CodigoMoneda");
            vSql.AppendLine("	    , renglonCobroDeFactura.CodigoMoneda");
            vSql.AppendLine("	    , caja.NombreCaja");
            vSql.AppendLine("	    , factura.Numero");
            vSql.AppendLine("	    , factura.NumeroComprobanteFiscal");
            vSql.AppendLine("	    , factura.Fecha");
            vSql.AppendLine("	    , cliente.Nombre");
            vSql.AppendLine("	    , factura.TotalFactura");
            vSql.AppendLine("	    , factura.CambioMostrarTotalEnDivisas");
            vSql.AppendLine("       , MonedaDoc.Nombre");
            vSql.AppendLine("       , MonedaRenglon.Nombre");
            vSql.AppendLine("   ORDER BY");
            vSql.AppendLine("       MonedaDoc.Nombre");
            vSql.AppendLine("       , factura.ConsecutivoCaja");
            vSql.AppendLine("       , MonedaRenglon.Nombre");
            vSql.AppendLine("       , caja.NombreCaja");
            vSql.AppendLine("       , factura.Fecha");
            vSql.AppendLine("       , factura.Numero");
            vSql.AppendLine("       , cliente.Nombre");
            return vSql.ToString();
        }

        public string SqlCuadreCajaPorTipoCobroYUsuario(int valConsecutivoCompania, DateTime valFechaInicial, DateTime valFechaFinal, Saw.Lib.eCantidadAImprimir valCantidadOperadorDeReporte, string valNombreDelOperador, Saw.Lib.eMonedaParaImpresion valMonedaDeReporte) {
            int vConsecutivoCajaGenerica = 0;
            string vSQLWhereFactura = string.Empty;
            string vSQLWhereAnticipo = string.Empty;
            StringBuilder vSql = new StringBuilder();
            //ANTICIPO
            vSQLWhereAnticipo = insSql.SqlEnumValueWithAnd(vSQLWhereAnticipo, "anticipo.Tipo", (int)eTipoDeAnticipo.Cobrado);
            vSQLWhereAnticipo = vSQLWhereAnticipo + " AND anticipo.Status IN ( " + insSql.EnumToSqlValue((int)eStatusAnticipo.Vigente) + "," + insSql.EnumToSqlValue((int)eStatusAnticipo.ParcialmenteUsado) + ")";
            vSQLWhereAnticipo = insSql.SqlDateValueBetween(vSQLWhereAnticipo, "anticipo.Fecha", valFechaInicial, valFechaFinal);
            vSQLWhereAnticipo = insSql.SqlExpressionValueWithAnd(vSQLWhereAnticipo, "anticipo.AsociarAnticipoACaja", insSql.ToSqlValue(LibConvert.BoolToSN(true)));
            if (valCantidadOperadorDeReporte == Saw.Lib.eCantidadAImprimir.Uno && !LibString.IsNullOrEmpty(valNombreDelOperador)) {
                vSQLWhereAnticipo = insSql.SqlValueWithAnd(vSQLWhereAnticipo, "anticipo.NombreOperador", valNombreDelOperador);
            }
            vSQLWhereAnticipo = insSql.SqlIntValueWithAnd(vSQLWhereAnticipo, "anticipo.ConsecutivoCompania", valConsecutivoCompania);
            //FACTURA
            vSQLWhereFactura = insSql.SqlExpressionValueWithAnd(vSQLWhereFactura, "factura.StatusFactura", insSql.EnumToSqlValue((int)eStatusFactura.Emitida));
            vSQLWhereFactura = vSQLWhereFactura + " AND factura.ConsecutivoCaja" + " <> " + LibConvert.ToStr(vConsecutivoCajaGenerica);
            vSQLWhereFactura = vSQLWhereFactura + " AND factura.TipoDeDocumento IN ( " + insSql.EnumToSqlValue((int)eTipoDocumentoFactura.Factura) + "," + insSql.EnumToSqlValue((int)eTipoDocumentoFactura.ComprobanteFiscal) + "," + insSql.EnumToSqlValue((int)eTipoDocumentoFactura.NotaDeCredito) + "," + insSql.EnumToSqlValue((int)eTipoDocumentoFactura.NotaDeCreditoComprobanteFiscal) + ")";
            vSQLWhereFactura = vSQLWhereFactura + " AND (SAW.FormaDelCobro.TipoDePago  <> " + insSql.EnumToSqlValue((int)eTipoDeFormaDePago.VueltoEfectivo) + " AND SAW.FormaDelCobro.TipoDePago  <> " + insSql.EnumToSqlValue((int)eTipoDeFormaDePago.VueltoC2P) + ") ";
            vSQLWhereFactura = insSql.SqlDateValueBetween(vSQLWhereFactura, "factura.fecha", valFechaInicial, valFechaFinal);
            vSQLWhereFactura = insSql.SqlExpressionValueWithAnd(vSQLWhereFactura, "factura.GeneraCobroDirecto", insSql.ToSqlValue(LibConvert.BoolToSN(true)));
            if (valCantidadOperadorDeReporte == Saw.Lib.eCantidadAImprimir.Uno && !LibString.IsNullOrEmpty(valNombreDelOperador)) {
                vSQLWhereFactura = insSql.SqlValueWithAnd(vSQLWhereFactura, "factura.NombreOperador", valNombreDelOperador);
            }
            vSQLWhereFactura = insSql.SqlIntValueWithAnd(vSQLWhereFactura, "factura.ConsecutivoCompania", valConsecutivoCompania);
            string prefijoNroDocFactura = insSql.ToSqlValue("Fact: ");
            string prefijoNroDocAnticipo = insSql.ToSqlValue("Ant: ");
            string menorA = insSql.ComparisonOp("<");
            string mayorA = insSql.ComparisonOp(">");
            #region CondicionalesAnticipo
            string montoTotalAnticipo = "anticipo.MontoTotal";
            string montoUsadoAnticipo = "anticipo.MontoUsado";
            string cambioAnticipo = "anticipo.Cambio";
            string montoTotalAnticipoBs = montoTotalAnticipo + " * " + cambioAnticipo;
            string montoUsadoAnticipoBs = montoUsadoAnticipo + " * " + cambioAnticipo;
            #endregion CondicionalesAnticipo

            #region CondicionalesFactura
            string montoRenglon = insSql.IsNull("renglonCobroDeFactura.Monto", insSql.ToInt("0"));
            string cambioDelRenglon = insSql.IsNull("renglonCobroDeFactura.CambioAMonedaLocal", insSql.ToInt("1"));
            string esEfectivo = "SAW.FormaDelCobro.TipoDePago" + insSql.ComparisonOp("=") + insSql.EnumToSqlValue((int)eFormaDeCobro.Efectivo);
            string esTarjeta = "SAW.FormaDelCobro.TipoDePago" + insSql.ComparisonOp("=") + insSql.EnumToSqlValue((int)eFormaDeCobro.Tarjeta);
            string esCheque = "SAW.FormaDelCobro.TipoDePago" + insSql.ComparisonOp("=") + insSql.EnumToSqlValue((int)eFormaDeCobro.Cheque);
            string monedaCobro = insSql.IsNull("MonedaRenglon.Nombre", "MonedaDoc.Nombre");
            string esDepTransferencia = "SAW.FormaDelCobro.TipoDePago" + insSql.ComparisonOp("=") + insSql.EnumToSqlValue((int)eFormaDeCobro.Deposito);
            string esNotaDeCredito = montoRenglon + menorA + insSql.ToInt("0") + insSql.LogicalOp("OR") + " factura.TotalFactura" + menorA + insSql.ToInt("0");
            string esAnticipoUsado = "SAW.FormaDelCobro.TipoDePago" + insSql.ComparisonOp("=") + insSql.EnumToSqlValue((int)eFormaDeCobro.Anticipo);
            string existeVuelto = "SUM(" + montoRenglon + ")" + mayorA + "factura.TotalFactura " + insSql.LogicalOp("AND") + "factura.TotalFactura" + mayorA + insSql.ToInt("0");
            string montoEfectivo = "SUM" + insSql.IIF(esEfectivo, montoRenglon, insSql.ToInt("0"), true);
            string montoTarjeta = "SUM" + insSql.IIF(esTarjeta, montoRenglon, insSql.ToInt("0"), true);
            string montoDepTranferencia = "SUM" + insSql.IIF(esDepTransferencia, montoRenglon, insSql.ToInt("0"), true);
            string montoCheque = "SUM" + insSql.IIF(esCheque, montoRenglon, insSql.ToInt("0"), true);
            string montoAnticipoUsado = "SUM" + insSql.IIF(esAnticipoUsado, montoRenglon, insSql.ToInt("0"), true);
            string montoNotaDeCredito = "SUM" + insSql.IIF(esNotaDeCredito, montoRenglon, insSql.ToInt("0"), true);
            string monedasIguales = "renglonCobroDeFactura.CodigoMoneda" + insSql.ComparisonOp("=") + "factura.CodigoMoneda";
            string calculoMontoVuelto = insSql.IIF(existeVuelto, "SUM(" + montoRenglon + ") - factura.TotalFactura", insSql.ToInt("0"), true);
            string montoVuelto = insSql.IIF(monedasIguales, calculoMontoVuelto, insSql.ToInt("0"), true);
            string montoPagadoSinConvertir = "SUM" + insSql.IIF(montoRenglon + menorA + insSql.ToInt("0"), insSql.ToInt("0"), montoRenglon, true);
            string convEnRenglon = montoRenglon + " * " + insSql.IsNull("renglonCobroDeFactura.CambioAMonedaLocal", insSql.ToInt("1"));
            string montoPagadoConvertido = "SUM" + insSql.IIF(convEnRenglon + " IS NULL " + insSql.LogicalOp("OR") + convEnRenglon + menorA + insSql.ToInt("0"), insSql.ToInt("0"), convEnRenglon, true);

            string montoPagado = insSql.IIF(monedasIguales, montoPagadoSinConvertir, montoPagadoConvertido, true);
            string esCredito = "MonedaRenglon.Nombre IS NULL" + insSql.LogicalOp("AND") + "factura.TotalFactura" + insSql.ComparisonOp("<>") + insSql.ToInt("0");
            string montoEfectivoEnBs = "SUM" + insSql.IIF(esEfectivo, montoRenglon + " * " + cambioDelRenglon, insSql.ToInt("0"), true);
            string montoTarjetaEnBs = "SUM" + insSql.IIF(esTarjeta, montoRenglon + " * " + cambioDelRenglon, insSql.ToInt("0"), true);
            string montoDepTranferenciaEnBs = "SUM" + insSql.IIF(esDepTransferencia, montoRenglon + " * " + cambioDelRenglon, insSql.ToInt("0"), true);
            string montoChequeEnBs = "SUM" + insSql.IIF(esCheque, montoRenglon + " * " + cambioDelRenglon, insSql.ToInt("0"), true);
            string montoAnticipoUsadoEnBs = "SUM" + insSql.IIF(esAnticipoUsado, montoRenglon + " * " + cambioDelRenglon, insSql.ToInt("0"), true);
            string montoNotaDeCreditoEnBs = "SUM" + insSql.IIF(esNotaDeCredito, montoRenglon + " * " + cambioDelRenglon, insSql.ToInt("0"), true);
            string montoCredito = insSql.IIF(esCredito, "factura.TotalFactura", insSql.ToInt("0"), true);
            #endregion CondicionalesFactura

            vSql.AppendLine(insSql.SetDateFormat());
            vSql.AppendLine("SELECT");
            vSql.AppendLine("    anticipo.NombreOperador AS NombreDelOperador");
            vSql.AppendLine("    , anticipo.Moneda AS MonedaDoc");
            vSql.AppendLine("    , anticipo.Moneda AS MonedaCobro");
            vSql.AppendLine("    , " + insSql.IsNull("anticipo.CodigoMoneda", insSql.ToSqlValue(string.Empty)) + " AS CodMoneda");
            vSql.AppendLine("    , anticipo.ConsecutivoCaja AS ConsecutivoCaja");
            vSql.AppendLine("    , caja.NombreCaja AS NombreCaja");
            vSql.AppendLine("    ," + prefijoNroDocAnticipo + insSql.CharConcat() + "anticipo.Numero AS NumeroDoc");
            vSql.AppendLine("    ," + insSql.ToSqlValue(string.Empty) + " AS NumFiscal");
            vSql.AppendLine("    , anticipo.Fecha AS Fecha");
            vSql.AppendLine("    , cliente.Nombre AS NombreCliente");
            if (valMonedaDeReporte == Saw.Lib.eMonedaParaImpresion.EnMonedaOriginal) {
                vSql.AppendLine("    ," + montoTotalAnticipo + " AS MontoDoc");
                vSql.AppendLine("    ," + LibConvert.ToStr(0) + " AS Efectivo");
                vSql.AppendLine("    ," + LibConvert.ToStr(0) + " AS Tarjeta");
                vSql.AppendLine("    ," + LibConvert.ToStr(0) + " AS Deposito");
                vSql.AppendLine("    ," + LibConvert.ToStr(0) + " AS Cheque");
                vSql.AppendLine("    ," + LibConvert.ToStr(0) + " AS NotaDeCredito");
                vSql.AppendLine("    ," + montoTotalAnticipo + " AS MontoPagado");
                vSql.AppendLine("    ," + montoUsadoAnticipo + " AS AnticipoUsado");
                vSql.AppendLine("    , (" + montoTotalAnticipo + " - " + montoUsadoAnticipo + ") AS AnticipoRestante");
            } else {
                vSql.AppendLine("    ," + montoTotalAnticipoBs + " AS MontoDoc");
                vSql.AppendLine("    ," + LibConvert.ToStr(0) + " AS Efectivo");
                vSql.AppendLine("    ," + LibConvert.ToStr(0) + " AS Tarjeta");
                vSql.AppendLine("    ," + LibConvert.ToStr(0) + " AS Deposito");
                vSql.AppendLine("    ," + LibConvert.ToStr(0) + " AS Cheque");
                vSql.AppendLine("    ," + LibConvert.ToStr(0) + " AS NotaDeCredito");
                vSql.AppendLine("    ," + montoTotalAnticipoBs + " AS MontoPagado");
                vSql.AppendLine("    ," + montoUsadoAnticipoBs + " AS AnticipoUsado");
                vSql.AppendLine("    , (" + montoTotalAnticipoBs + " - " + montoUsadoAnticipoBs + ") AS AnticipoRestante");
            }
            vSql.AppendLine("    ," + LibConvert.ToStr(0) + " AS Vuelto");
            vSql.AppendLine("    ," + LibConvert.ToStr(0) + " AS VentaCredito");
            vSql.AppendLine("   FROM anticipo");
            vSql.AppendLine("   INNER JOIN Adm.Caja");
            vSql.AppendLine("       ON Caja.ConsecutivoCompania = anticipo.ConsecutivoCompania");
            vSql.AppendLine("       AND Caja.Consecutivo = anticipo.ConsecutivoCaja");
            vSql.AppendLine("   INNER JOIN Cliente");
            vSql.AppendLine("       ON Cliente.ConsecutivoCompania = anticipo.ConsecutivoCompania");
            vSql.AppendLine("       AND Cliente.Codigo = anticipo.CodigoCliente");
            if (LibString.Len(vSQLWhereAnticipo) > 0) {
                vSql.AppendLine(insSql.WhereSql(vSQLWhereAnticipo));
            }
            vSql.AppendLine("   UNION");
            vSql.AppendLine("   SELECT");
            vSql.AppendLine("   factura.NombreOperador AS NombreDelOperador");
            vSql.AppendLine("   , MonedaDoc.Nombre AS MonedaDoc");
            vSql.AppendLine("   , " + monedaCobro + " AS MonedaCobro");
            vSql.AppendLine("    , " + insSql.IsNull("MonedaRenglon.Codigo", insSql.ToSqlValue(string.Empty)) + " AS CodMoneda");
            vSql.AppendLine("   , factura.ConsecutivoCaja AS ConsecutivoCaja");
            vSql.AppendLine("   , caja.NombreCaja AS NombreCaja");
            vSql.AppendLine("   , " + prefijoNroDocFactura + insSql.CharConcat() + "factura.Numero AS NumeroDoc");
            vSql.AppendLine("   , factura.NumeroComprobanteFiscal AS NumFiscal");
            vSql.AppendLine("   , factura.Fecha AS Fecha");
            vSql.AppendLine("   , cliente.Nombre AS NombreCliente");
            vSql.AppendLine("   , factura.TotalFactura AS MontoDoc");
            if (valMonedaDeReporte == Saw.Lib.eMonedaParaImpresion.EnMonedaOriginal) {
                vSql.AppendLine("   , " + montoEfectivo + " AS Efectivo");
                vSql.AppendLine("   , " + montoTarjeta + " AS Tarjeta");
                vSql.AppendLine("   , " + montoDepTranferencia + " AS Deposito");
                vSql.AppendLine("   , " + montoCheque + " AS Cheque");
                vSql.AppendLine("   , " + montoNotaDeCredito + " AS NotaDeCredito");
                vSql.AppendLine("   , " + montoPagadoSinConvertir + " AS MontoPagado");
                vSql.AppendLine("   , " + montoAnticipoUsado + " AS AnticipoUsado");
            } else {
                vSql.AppendLine("   , " + montoEfectivoEnBs + " AS Efectivo");
                vSql.AppendLine("   , " + montoTarjetaEnBs + " AS Tarjeta");
                vSql.AppendLine("   , " + montoDepTranferenciaEnBs + " AS Deposito");
                vSql.AppendLine("   , " + montoChequeEnBs + " AS Cheque");
                vSql.AppendLine("   , " + montoNotaDeCreditoEnBs + " AS NotaDeCredito");
                vSql.AppendLine("   , " + montoPagado + " AS MontoPagado");
                vSql.AppendLine("    ," + montoAnticipoUsadoEnBs + " AS AnticipoUsado");
            }
            vSql.AppendLine("    ," + LibConvert.ToStr(0) + " AS AnticipoRestante");
            vSql.AppendLine("   , " + montoVuelto + " AS Vuelto");
            vSql.AppendLine("   , " + montoCredito + " AS VentaCredito");
            vSql.AppendLine("   FROM Adm.Caja");
            vSql.AppendLine("   LEFT JOIN dbo.factura");
            vSql.AppendLine("       ON factura.ConsecutivoCaja = Caja.Consecutivo");
            vSql.AppendLine("	    AND factura.ConsecutivoCompania = Caja.ConsecutivoCompania");
            vSql.AppendLine("   LEFT JOIN dbo.Cliente");
            vSql.AppendLine("       ON Cliente.Codigo = factura.CodigoCliente");
            vSql.AppendLine("	    AND Cliente.ConsecutivoCompania = factura.ConsecutivoCompania");
            vSql.AppendLine("   LEFT JOIN dbo.renglonCobroDeFactura");
            vSql.AppendLine("       ON renglonCobroDeFactura.NumeroFactura = factura.Numero");
            vSql.AppendLine("	    AND renglonCobroDeFactura.ConsecutivoCompania = factura.ConsecutivoCompania");
            vSql.AppendLine("	    AND renglonCobroDeFactura.TipoDeDocumento = factura.TipoDeDocumento");
            vSql.AppendLine("   LEFT JOIN SAW.FormaDelCobro");
            vSql.AppendLine("       ON SAW.FormaDelCobro.Codigo = renglonCobroDeFactura.CodigoFormaDelCobro");
            vSql.AppendLine("   INNER JOIN dbo.Moneda AS MonedaDoc");
            vSql.AppendLine("       ON MonedaDoc.Codigo = factura.CodigoMoneda");
            vSql.AppendLine("   LEFT JOIN dbo.Moneda AS MonedaRenglon");
            vSql.AppendLine("       ON MonedaRenglon.Codigo = renglonCobroDeFactura.CodigoMoneda");
            if (LibString.Len(vSQLWhereFactura) > 0) {
                vSql.AppendLine(insSql.WhereSql(vSQLWhereFactura));
            }
            vSql.AppendLine("   GROUP BY");
            vSql.AppendLine("	    factura.Moneda");
            vSql.AppendLine("	    , factura.ConsecutivoCaja");
            vSql.AppendLine("	    , factura.CodigoMoneda");
            vSql.AppendLine("	    , renglonCobroDeFactura.CodigoMoneda");
            vSql.AppendLine("       , factura.NombreOperador");
            vSql.AppendLine("	    , caja.NombreCaja");
            vSql.AppendLine("	    , factura.Numero");
            vSql.AppendLine("	    , factura.NumeroComprobanteFiscal");
            vSql.AppendLine("	    , factura.Fecha");
            vSql.AppendLine("	    , cliente.Nombre");
            vSql.AppendLine("	    , factura.TotalFactura");
            vSql.AppendLine("	    , factura.CambioMostrarTotalEnDivisas");
            vSql.AppendLine("       , MonedaDoc.Nombre");
            vSql.AppendLine("       , MonedaRenglon.Nombre");
            vSql.AppendLine("       , MonedaRenglon.Codigo");
            vSql.AppendLine("   ORDER BY");
            vSql.AppendLine("       NombreDelOperador");
            vSql.AppendLine("       , MonedaDoc");
            vSql.AppendLine("       , ConsecutivoCaja");
            vSql.AppendLine("       , MonedaCobro");
            vSql.AppendLine("       , NombreCaja");
            vSql.AppendLine("       , Fecha");
            vSql.AppendLine("       , NumeroDoc");
            vSql.AppendLine("       , NombreCliente");
            return vSql.ToString();
        }

        public string SqlCuadreCajaConDetalleFormaPago(int valConsecutivoCompania, DateTime valFechaInicial, DateTime valFechaFinal, Saw.Lib.eMonedaParaImpresion valMonedaDeReporte, bool valTotalesTipoPago) {
            StringBuilder vSql = new StringBuilder();
            int vConsecutivoCajaGenerica = 0;
            string vSQLWhere = "";
            vSQLWhere = insSql.SqlExpressionValueWithAnd(vSQLWhere, "factura.StatusFactura", insSql.EnumToSqlValue((int)eStatusFactura.Emitida));
            vSQLWhere = vSQLWhere + " AND factura.ConsecutivoCaja" + " <> " + LibConvert.ToStr(vConsecutivoCajaGenerica);
            vSQLWhere = vSQLWhere + " AND factura.TipoDeDocumento IN ( " + insSql.EnumToSqlValue((int)eTipoDocumentoFactura.Factura) + "," + insSql.EnumToSqlValue((int)eTipoDocumentoFactura.ComprobanteFiscal) + "," + insSql.EnumToSqlValue((int)eTipoDocumentoFactura.NotaDeCredito) + "," + insSql.EnumToSqlValue((int)eTipoDocumentoFactura.NotaDeCreditoComprobanteFiscal) + ")";
            vSQLWhere = vSQLWhere + " AND (SAW.FormaDelCobro.TipoDePago  <> " + insSql.EnumToSqlValue((int)eTipoDeFormaDePago.VueltoEfectivo) + " AND SAW.FormaDelCobro.TipoDePago  <> " + insSql.EnumToSqlValue((int)eTipoDeFormaDePago.VueltoC2P) + ") ";
            vSQLWhere = insSql.SqlDateValueBetween(vSQLWhere, "factura.fecha", valFechaInicial, valFechaFinal);
            vSQLWhere = insSql.SqlExpressionValueWithAnd(vSQLWhere, "factura.GeneraCobroDirecto", insSql.ToSqlValue("S"));
            vSQLWhere = insSql.SqlIntValueWithAnd(vSQLWhere, "factura.ConsecutivoCompania", valConsecutivoCompania);

            vSql.AppendLine(insSql.SetDateFormat());
            vSql.AppendLine(" SELECT");
            vSql.AppendLine("	Caja.Consecutivo");
            vSql.AppendLine("	, Caja.NombreCaja");
            vSql.AppendLine("   , factura.Moneda AS MonedaDoc");
            vSql.AppendLine("	, factura.Fecha");
            vSql.AppendLine("	, factura.Numero AS NumeroDoc");
            vSql.AppendLine("	, factura.NumeroComprobanteFiscal As NumFiscal");
            vSql.AppendLine("   , Cliente.Nombre AS NombreCliente");
            if (valMonedaDeReporte == Saw.Lib.eMonedaParaImpresion.EnMonedaOriginal) {
                vSql.AppendLine("	, MAX(factura.TotalFactura) AS MontoDoc");
                vSql.AppendLine("   , " + insSql.IsNull("SUM(renglonCobroDeFactura.Monto)", insSql.ToCur("0")) + " AS MontoCobro");
                vSql.AppendLine("   , MAX(factura.CambioMostrarTotalEnDivisas) AS TasaOperacion");
            } else {
                vSql.AppendLine("	, MAX(factura.TotalFactura * factura.CambioABolivares) AS MontoDoc");
                vSql.AppendLine(", " + insSql.IsNull("SUM(renglonCobroDeFactura.Monto * renglonCobroDeFactura.CambioAMonedaLocal)", insSql.ToCur("0")) + " AS MontoCobro");
                vSql.AppendLine(", " + insSql.IsNull("MAX(renglonCobroDeFactura.CambioAMonedaLocal)", insSql.ToCur("0")) + " AS CambioABolivares");
            }
            vSql.AppendLine(", " + insSql.IsNull("SAW.FormaDelCobro.TipoDePago", insSql.ToSqlValue(string.Empty)) + " AS TipoDeCobro");
            vSql.AppendLine(", " + insSql.IsNull("MonedaRenglon.Nombre", insSql.ToSqlValue(string.Empty)) + "AS NombreMonedaFormaDelCobro");
            if (valTotalesTipoPago) {
                vSql.AppendLine("   , MonedaRenglon.Codigo AS CodMonedaCobro");
            }
            vSql.AppendLine(" FROM Adm.Caja");
            vSql.AppendLine(" INNER JOIN factura");
            vSql.AppendLine("	ON factura.ConsecutivoCaja = Caja.Consecutivo");
            vSql.AppendLine("	AND factura.ConsecutivoCompania = Caja.ConsecutivoCompania");
            vSql.AppendLine(" INNER JOIN Cliente");
            vSql.AppendLine(" ON Cliente.Codigo = factura.CodigoCliente");
            vSql.AppendLine(" AND Cliente.ConsecutivoCompania = factura.ConsecutivoCompania");
            vSql.AppendLine(" INNER JOIN dbo.renglonCobroDeFactura");
            vSql.AppendLine("	ON renglonCobroDeFactura.NumeroFactura = factura.Numero");
            vSql.AppendLine("	AND renglonCobroDeFactura.ConsecutivoCompania = factura.ConsecutivoCompania");
            vSql.AppendLine("   AND renglonCobroDeFactura.TipoDeDocumento = factura.TipoDeDocumento");
            vSql.AppendLine(" INNER JOIN SAW.FormaDelCobro");
            vSql.AppendLine("   ON SAW.FormaDelCobro.Codigo = renglonCobroDeFactura.CodigoFormaDelCobro");
            vSql.AppendLine(" INNER JOIN dbo.Moneda AS MonedaRenglon");
            vSql.AppendLine("   ON MonedaRenglon.Codigo = renglonCobroDeFactura.CodigoMoneda");
            if (LibString.Len(vSQLWhere) > 0) {
                vSql.AppendLine(insSql.WhereSql(vSQLWhere));
            }
            vSql.AppendLine(" GROUP BY");
            vSql.AppendLine("	Caja.Consecutivo");
            vSql.AppendLine("	, Caja.NombreCaja");
            vSql.AppendLine("	, factura.TipoDeDocumento");
            vSql.AppendLine("	, factura.Fecha");
            vSql.AppendLine("	, factura.NombreOperador");
            vSql.AppendLine("   , factura.Moneda");
            vSql.AppendLine("	, factura.Numero");
            vSql.AppendLine("	, factura.NumeroComprobanteFiscal");
            vSql.AppendLine("	, factura.CodigoVendedor");
            vSql.AppendLine("	, Cliente.Nombre");
            vSql.AppendLine("	, factura.TotalFactura");
            vSql.AppendLine("	, SAW.FormaDelCobro.TipoDePago");
            vSql.AppendLine("	, MonedaRenglon.Codigo");
            vSql.AppendLine("	, MonedaRenglon.Nombre");
            vSql.AppendLine(" ORDER BY");
            vSql.AppendLine("   factura.NombreOperador");
            vSql.AppendLine("   , Caja.NombreCaja");
            vSql.AppendLine("	, factura.Fecha");
            vSql.AppendLine("	, factura.Numero");
            vSql.AppendLine("	, MonedaRenglon.Codigo DESC");
            return vSql.ToString();
        }

        public string SqlCuadreCajaConDetalleFormaPagoResumido(int valConsecutivoCompania, DateTime valFechaInicial, DateTime valFechaFinal, Saw.Lib.eMonedaParaImpresion valMonedaDeReporte, bool valTotalesTipoPago) {
            StringBuilder vSql = new StringBuilder();
            int vConsecutivoCajaGenerica = 0;
            string vSQLWhere = "";
            vSQLWhere = insSql.SqlExpressionValueWithAnd(vSQLWhere, "factura.StatusFactura", insSql.EnumToSqlValue((int)eStatusFactura.Emitida));
            vSQLWhere = vSQLWhere + " AND factura.ConsecutivoCaja" + " <> " + LibConvert.ToStr(vConsecutivoCajaGenerica);
            vSQLWhere = vSQLWhere + " AND factura.TipoDeDocumento IN ( " + insSql.EnumToSqlValue((int)eTipoDocumentoFactura.Factura) + "," + insSql.EnumToSqlValue((int)eTipoDocumentoFactura.ComprobanteFiscal) + "," + insSql.EnumToSqlValue((int)eTipoDocumentoFactura.NotaDeCredito) + "," + insSql.EnumToSqlValue((int)eTipoDocumentoFactura.NotaDeCreditoComprobanteFiscal) + ")";
            vSQLWhere = insSql.SqlDateValueBetween(vSQLWhere, "factura.fecha", valFechaInicial, valFechaFinal);
            vSQLWhere = insSql.SqlExpressionValueWithAnd(vSQLWhere, "factura.GeneraCobroDirecto", insSql.ToSqlValue("S"));
            vSQLWhere = insSql.SqlIntValueWithAnd(vSQLWhere, "factura.ConsecutivoCompania", valConsecutivoCompania);
            vSql.AppendLine(insSql.SetDateFormat());
            vSql.AppendLine(" ;WITH CTE_TotalCaja(ConsecutivoCompañia, ConsecutivoCaja, MontoTotalCaja)");
            vSql.AppendLine(" AS");
            vSql.AppendLine("(SELECT");
            vSql.AppendLine("	factura.ConsecutivoCompania");
            vSql.AppendLine("	, factura.ConsecutivoCaja AS ConsecutivoCaja");
            vSql.AppendLine("	, SUM(factura.TotalFactura) AS MontoTotalCaja");
            vSql.AppendLine(" FROM dbo.factura");
            if (LibString.Len(vSQLWhere) > 0) {
                vSql.AppendLine(insSql.WhereSql(vSQLWhere));
            }
            vSql.AppendLine(" GROUP BY");
            vSql.AppendLine("	factura.ConsecutivoCompania");
            vSql.AppendLine("	, factura.ConsecutivoCaja");
            vSql.AppendLine(" )");
            vSql.AppendLine(" SELECT");
            vSql.AppendLine("    Caja.Consecutivo AS ConsecutivoCaja");
            vSql.AppendLine("    , Caja.NombreCaja AS NombreCaja");
            vSql.AppendLine("    , factura.Moneda AS MonedaDoc");
            vSql.AppendLine("    , MonedaRenglon.Codigo AS CodMonedaDeCobro");
            vSql.AppendLine("    , MonedaRenglon.Nombre AS MonedaPago");
            vSql.AppendLine("    , CTE_TotalCaja.MontoTotalCaja AS MontoFacturas");
            if (valMonedaDeReporte == Saw.Lib.eMonedaParaImpresion.EnMonedaOriginal) {
                vSql.AppendLine("    , " + insSql.IsNull("SUM(renglonCobroDeFactura.Monto)", insSql.ToInt("0")) + " AS MontoCobro");
            } else {
                vSql.AppendLine("    , " + insSql.IsNull("SUM(renglonCobroDeFactura.Monto * renglonCobroDeFactura.CambioAMonedaLocal)", insSql.ToInt("0")) + " AS MontoCobro");
            }
            vSql.AppendLine("    , " + insSql.IsNull("SAW.FormaDelCobro.TipoDePago", insSql.ToSqlValue(string.Empty)) + " AS TipoDeCobro");
            vSql.AppendLine(" FROM Adm.Caja");
            vSql.AppendLine("    INNER JOIN factura");
            vSql.AppendLine("       ON factura.ConsecutivoCaja = Caja.Consecutivo");
            vSql.AppendLine("       AND factura.ConsecutivoCompania = Caja.ConsecutivoCompania");
            vSql.AppendLine("    INNER JOIN dbo.renglonCobroDeFactura");
            vSql.AppendLine("       ON renglonCobroDeFactura.NumeroFactura = factura.Numero");
            vSql.AppendLine("       AND renglonCobroDeFactura.ConsecutivoCompania = factura.ConsecutivoCompania");
            vSql.AppendLine("       AND renglonCobroDeFactura.TipoDeDocumento = factura.TipoDeDocumento");
            vSql.AppendLine("    INNER JOIN SAW.FormaDelCobro");
            vSql.AppendLine("       ON SAW.FormaDelCobro.Codigo = renglonCobroDeFactura.CodigoFormaDelCobro");
            vSql.AppendLine("    INNER JOIN dbo.Moneda AS MonedaRenglon");
            vSql.AppendLine("       ON MonedaRenglon.Codigo = renglonCobroDeFactura.CodigoMoneda");
            vSql.AppendLine("	INNER JOIN CTE_TotalCaja");
            vSql.AppendLine("	    ON CTE_TotalCaja.ConsecutivoCaja = Caja.Consecutivo");
            vSql.AppendLine("	    AND CTE_TotalCaja.ConsecutivoCompañia = Caja.ConsecutivoCompania");
            if (LibString.Len(vSQLWhere) > 0) {
                vSQLWhere = vSQLWhere + " AND (SAW.FormaDelCobro.TipoDePago  <> " + insSql.EnumToSqlValue((int)eTipoDeFormaDePago.VueltoEfectivo) + " AND SAW.FormaDelCobro.TipoDePago  <> " + insSql.EnumToSqlValue((int)eTipoDeFormaDePago.VueltoC2P) + ") ";
                vSql.AppendLine(insSql.WhereSql(vSQLWhere));
            }
            vSql.AppendLine(" GROUP BY Caja.Consecutivo,");
            vSql.AppendLine("    Caja.NombreCaja,");
            vSql.AppendLine("    factura.Moneda,");
            vSql.AppendLine("    MonedaRenglon.Nombre,");
            vSql.AppendLine("    SAW.FormaDelCobro.TipoDePago,");
            vSql.AppendLine("    MonedaRenglon.Codigo,");
            vSql.AppendLine("	 CTE_TotalCaja.MontoTotalCaja");
            vSql.AppendLine(" ORDER BY MonedaRenglon.Nombre,");
            vSql.AppendLine("    Caja.Consecutivo,");
            vSql.AppendLine("    Caja.NombreCaja");
            return vSql.ToString();
        }

        public string SqlCuadreCajaPorUsuario(int valConsecutivoCompania, DateTime valFechaInicial, DateTime valFechaFinal, Saw.Lib.eTipoDeInforme valTipoDeInforme, Saw.Lib.eMonedaParaImpresion valMonedaDeReporte, Saw.Lib.eCantidadAImprimir valCantidadOperadorDeReporte, string valNombreDelOperador) {
            const int vConsecutivoCajaGenerica = 0;
            StringBuilder vSql = new StringBuilder();
            #region Filtro de consulta
            string vSQLWhere = "";
            vSQLWhere = insSql.SqlExpressionValueWithAnd(vSQLWhere, "factura.StatusFactura", insSql.EnumToSqlValue((int)eStatusFactura.Emitida));
            vSQLWhere = vSQLWhere + " AND factura.ConsecutivoCaja" + " <> " + LibConvert.ToStr(vConsecutivoCajaGenerica);
            vSQLWhere = vSQLWhere + " AND factura.TipoDeDocumento IN ( " + insSql.EnumToSqlValue((int)eTipoDocumentoFactura.Factura) + "," + insSql.EnumToSqlValue((int)eTipoDocumentoFactura.ComprobanteFiscal) + "," + insSql.EnumToSqlValue((int)eTipoDocumentoFactura.NotaDeCredito) + "," + insSql.EnumToSqlValue((int)eTipoDocumentoFactura.NotaDeCreditoComprobanteFiscal) + ")";
            vSQLWhere = vSQLWhere + " AND (SAW.FormaDelCobro.TipoDePago  <> " + insSql.EnumToSqlValue((int)eTipoDeFormaDePago.VueltoEfectivo) + " AND SAW.FormaDelCobro.TipoDePago  <> " + insSql.EnumToSqlValue((int)eTipoDeFormaDePago.VueltoC2P) + ") ";
            vSQLWhere = insSql.SqlDateValueBetween(vSQLWhere, "factura.fecha", valFechaInicial, valFechaFinal);
            vSQLWhere = insSql.SqlExpressionValueWithAnd(vSQLWhere, "factura.GeneraCobroDirecto", insSql.ToSqlValue("S"));
            #region Manejo para Fechas de Apertura y Cierre de Caja            
            string horaCierre = " AND factura.HoraModificacion BETWEEN  CajaApertura.HoraApertura AND " + insSql.IIF("CajaApertura.HoraCierre <> " + insSql.ToSqlValue(string.Empty), "CajaApertura.HoraCierre", insSql.ToSqlValue("23:59"), true);
            string fechaCierre = " AND Factura.Fecha <= " + insSql.IIF("CajaApertura.CajaCerrada = " + insSql.ToSqlValue(false), "CajaApertura.Fecha", "GetDate()", true);
            #endregion Manejo para Fechas de Apertura y Cierre de Caja
            vSQLWhere = vSQLWhere + fechaCierre + horaCierre;
            if (valCantidadOperadorDeReporte == Saw.Lib.eCantidadAImprimir.Uno && !LibString.IsNullOrEmpty(valNombreDelOperador)) {
                vSQLWhere = insSql.SqlValueWithAnd(vSQLWhere, "Factura.NombreOperador", valNombreDelOperador);
            }
            vSQLWhere = insSql.SqlIntValueWithAnd(vSQLWhere, "factura.ConsecutivoCompania", valConsecutivoCompania);
            #endregion Filtro de consulta
            #region Calculo Vuelto
            string mayorA = insSql.ComparisonOp(">");
            string monedasIguales = "MonedaRenglon.Codigo" + insSql.ComparisonOp("=") + "factura.CodigoMoneda";
            string montoRenglon = insSql.IsNull("renglonCobroDeFactura.Monto", insSql.ToInt("0"));
            string existeVuelto = "SUM(" + montoRenglon + ")" + mayorA + "factura.TotalFactura " + insSql.LogicalOp("AND") + "factura.TotalFactura" + mayorA + insSql.ToInt("0");
            string calculoMontoVuelto = insSql.IIF(existeVuelto, "SUM(" + montoRenglon + ") - factura.TotalFactura", insSql.ToInt("0"), true);
            string montoVuelto = insSql.IIF(monedasIguales, calculoMontoVuelto, insSql.ToInt("0"), true);
            #endregion            
            vSql.AppendLine(" SELECT");
            vSql.AppendLine("    Factura.NombreOperador AS NombreUsuario");
            vSql.AppendLine("   , caja.NombreCaja AS NombreCaja");
            vSql.AppendLine("   , factura.Moneda AS MonedaDoc");
            vSql.AppendLine("	, MonedaRenglon.Nombre AS MonedaCobro");
            vSql.AppendLine("   , MonedaRenglon.Codigo AS CodMonedaCobro");
            vSql.AppendLine("   , factura.Fecha AS Fecha");
            vSql.AppendLine("   , factura.Numero AS NumeroDoc");
            vSql.AppendLine("   , factura.NumeroComprobanteFiscal AS NumFiscal");
            vSql.AppendLine("   , Cliente.Nombre AS NombreCliente");
            vSql.AppendLine("   , factura.TotalFactura AS MontoDoc");
            if (valMonedaDeReporte != Saw.Lib.eMonedaParaImpresion.EnMonedaOriginal) {
                vSql.AppendLine("	, SUM(renglonCobroDeFactura.Monto * renglonCobroDeFactura.CambioAMonedaLocal) AS MontoPagado");
            } else {
                vSql.AppendLine("	, SUM(renglonCobroDeFactura.Monto) AS MontoPagado");
            }
            vSql.AppendLine("   , " + montoVuelto + " AS Vuelto");
            vSql.AppendLine(" FROM Adm.Caja");
            vSql.AppendLine("	INNER JOIN dbo.factura");
            vSql.AppendLine("		ON factura.ConsecutivoCaja = Caja.Consecutivo");
            vSql.AppendLine("		AND factura.ConsecutivoCompania = Caja.ConsecutivoCompania");
            vSql.AppendLine("	INNER JOIN dbo.renglonCobroDeFactura");
            vSql.AppendLine("		ON renglonCobroDeFactura.NumeroFactura = factura.Numero");
            vSql.AppendLine("		AND renglonCobroDeFactura.ConsecutivoCompania = factura.ConsecutivoCompania");
            vSql.AppendLine("		AND renglonCobroDeFactura.TipoDeDocumento = factura.TipoDeDocumento");
            vSql.AppendLine("	INNER JOIN dbo.Moneda AS MonedaRenglon");
            vSql.AppendLine("		ON MonedaRenglon.Codigo = renglonCobroDeFactura.CodigoMoneda");
            vSql.AppendLine("	INNER JOIN dbo.Cliente");
            vSql.AppendLine("		ON Cliente.Codigo = factura.CodigoCliente");
            vSql.AppendLine("		AND Cliente.ConsecutivoCompania = factura.ConsecutivoCompania");
            vSql.AppendLine("	INNER JOIN Adm.CajaApertura");
            vSql.AppendLine("		ON CajaApertura.ConsecutivoCaja = Factura.ConsecutivoCaja");
            vSql.AppendLine("		AND CajaApertura.ConsecutivoCompania = Factura.ConsecutivoCompania");
            vSql.AppendLine("   INNER JOIN SAW.FormaDelCobro");
            vSql.AppendLine("      ON SAW.FormaDelCobro.Codigo = renglonCobroDeFactura.CodigoFormaDelCobro");
            if (LibString.Len(vSQLWhere) > 0) {
                vSql.AppendLine(insSql.WhereSql(vSQLWhere));
            }
            vSql.AppendLine(" GROUP BY");
            vSql.AppendLine("    Factura.NombreOperador");
            vSql.AppendLine("   , factura.Moneda");
            vSql.AppendLine("   , caja.NombreCaja");
            vSql.AppendLine("   , factura.Numero");
            vSql.AppendLine("   , factura.NumeroComprobanteFiscal");
            vSql.AppendLine("   , factura.Fecha");
            vSql.AppendLine("   , Cliente.Nombre");
            vSql.AppendLine("   , factura.TotalFactura");
            vSql.AppendLine("   , factura.TipoDeDocumento");
            vSql.AppendLine("   , MonedaRenglon.Nombre");
            vSql.AppendLine("	, MonedaRenglon.Codigo");
            vSql.AppendLine("	, factura.CodigoMoneda");
            vSql.AppendLine(" ORDER BY");
            vSql.AppendLine("    Factura.NombreOperador");
            vSql.AppendLine("   , caja.NombreCaja");
            vSql.AppendLine("   , factura.Moneda");
            vSql.AppendLine("	, MonedaRenglon.Nombre");
            vSql.AppendLine("   , factura.Fecha");
            vSql.AppendLine("   , factura.Numero");
            vSql.AppendLine("   , Cliente.Nombre");
            return vSql.ToString();
        }

        internal string SqlCajaCerrada(int valConsecutivoCompania, DateTime valFechaDesde, DateTime valFechaHasta) {
            StringBuilder vSql = new StringBuilder();
            vSql.Append(SqlCTECajaAperturadaPorOperador(valConsecutivoCompania, valFechaDesde, valFechaHasta));
            vSql.AppendLine("SELECT ");
            vSql.AppendLine("	CAST(Consecutivo AS varchar(10)) + '-' + CAST(ConsecutivoCaja AS varchar(10)) AS ConsecutivoConsecutivoCaja, ");
            vSql.AppendLine("	Consecutivo, ");
            vSql.AppendLine("	ConsecutivoCaja, ");
            vSql.AppendLine("	NombreCaja, ");
            vSql.AppendLine("	Fecha, ");
            vSql.AppendLine("	HoraApertura, ");
            vSql.AppendLine("	HoraCierre, ");
            vSql.AppendLine("	Operador, ");
            vSql.AppendLine("	Movimiento,");
            vSql.AppendLine("	MontoApertura, MontoAperturaME,");
            vSql.AppendLine("	MontoCierre, MontoCierreME,");
            vSql.AppendLine("	(CASE CodigoMoneda WHEN 'VED' THEN 'Monto M.E.' ELSE 'MONTO ' + CodigoMoneda END) AS EtiquetaMontoME,");
            vSql.AppendLine("	SUM(MontoML) MontoML,");
            vSql.AppendLine("	SUM(MontoME) MontoME ");
            vSql.AppendLine("FROM CTE_CajaAperturadaPorOperador");
            vSql.AppendLine("GROUP BY ");
            vSql.AppendLine("	Consecutivo, ");
            vSql.AppendLine("	ConsecutivoCaja, ");
            vSql.AppendLine("	NombreCaja, ");
            vSql.AppendLine("	Fecha, ");
            vSql.AppendLine("	HoraApertura, ");
            vSql.AppendLine("	HoraCierre, ");
            vSql.AppendLine("	Operador, ");
            vSql.AppendLine("	MontoApertura, MontoAperturaME,");
            vSql.AppendLine("	MontoCierre, MontoCierreME,");
            vSql.AppendLine("	Movimiento,");
            vSql.AppendLine("	CodigoMoneda");
            vSql.AppendLine("ORDER BY ");
            vSql.AppendLine("	Consecutivo, ");
            vSql.AppendLine("	ConsecutivoCaja, ");
            vSql.AppendLine("	Fecha,");
            vSql.AppendLine("	Movimiento");
            return vSql.ToString();
        }

        string SqlCTECajaAperturadaPorOperador(int valConsecutivoCompania, DateTime valFechaDesde, DateTime valFechaHasta) {
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("; WITH CTE_CajaAperturadaPorOperador AS(");
            vSql.AppendLine(SqlUNPIVOTCajaCerrada(true, valConsecutivoCompania, valFechaDesde, valFechaHasta));
            vSql.AppendLine(" UNION ");
            vSql.AppendLine(SqlUNPIVOTCajaCerrada(false, valConsecutivoCompania, valFechaDesde, valFechaHasta));
            vSql.AppendLine(")");
            return vSql.ToString();
        }

        string SqlUNPIVOTCajaCerrada(bool valEsPAraMonedaLocal, int valConsecutivoCompania, DateTime valFechaDesde, DateTime valFechaHasta) {
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("SELECT ");
            vSql.AppendLine("	Consecutivo, ");
            vSql.AppendLine("	ConsecutivoCaja, ");
            vSql.AppendLine("	NombreCaja, ");
            vSql.AppendLine("	Fecha, ");
            vSql.AppendLine("	HoraApertura, ");
            vSql.AppendLine("	HoraCierre, ");
            vSql.AppendLine("	Operador,");
            vSql.AppendLine("	MontoApertura, MontoAperturaME,");
            vSql.AppendLine("	MontoCierre, MontoCierreME,");
            vSql.AppendLine("	NombreUsuario,");            
            vSql.AppendLine("	Movimiento,");
            if (valEsPAraMonedaLocal) {
                vSql.AppendLine("	MontoML,");
                vSql.AppendLine("	0 AS MontoME, CodigoMoneda");
            } else {
                vSql.AppendLine("	0 AS MontoML,");
                vSql.AppendLine("	MontoME, CodigoMoneda");
            }
            vSql.AppendLine("FROM (SELECT ");
            vSql.AppendLine("		CA.Consecutivo, ");
            vSql.AppendLine("		CA.ConsecutivoCaja, ");
            vSql.AppendLine("		C.NombreCaja, ");
            vSql.AppendLine("		'(' + NombreDelUsuario + ') ' + Usr.FirstAndLastName AS Operador,");
            vSql.AppendLine("		MontoApertura, MontoAperturaME,");
            vSql.AppendLine("		MontoCierre, MontoCierreME,");
            vSql.AppendLine("		CA.NombreDelUsuario AS NombreUsuario,");           
            if (valEsPAraMonedaLocal) {
                vSql.AppendLine("		MontoEfectivo [01 - Efectivo], MontoTarjeta [02 - Tarjeta], MontoCheque [03 - Cheque], MontoDeposito [04 - Depósito], MontoAnticipo [05 - Anticipo], MontoVuelto [06 - Vuelto Efectivo], MontoVueltoPM [07 - Vuelto Pago Móvil], MontoTarjetaMS [08 - Tarjeta Medios Electrónicos],MontoC2P [09 - C2P],MontoTransferenciaMS [10 - Transferencia Medios Electrónicos],MontoPagoMovil [11 - Pago Móvil],MontoDepositoMS [12 - Depósito Medios Electrónicos], ");
            } else {
                vSql.AppendLine("		MontoEfectivoME [01 - Efectivo], MontoTarjetaME [02 - Tarjeta], MontoChequeME [03 - Cheque], MontoDepositoME [04 - Depósito], MontoAnticipoME [05 - Anticipo], MontoVueltoME [06 - Vuelto Efectivo], MontoZelle [13 - Zelle], ");
            }
            vSql.AppendLine("		Fecha, ");
            vSql.AppendLine("		HoraApertura, ");
            vSql.AppendLine("		HoraCierre, ");
            vSql.AppendLine("		CajaCerrada, CA.CodigoMoneda ");
            vSql.AppendLine("	FROM Adm.CajaApertura AS CA INNER JOIN Lib.GUser Usr");
            vSql.AppendLine("		ON CA.NombreDelUsuario = Usr.UserName");
            vSql.AppendLine("		INNER JOIN Adm.Caja C");
            vSql.AppendLine("		ON CA.ConsecutivoCaja = c.Consecutivo");
            vSql.AppendLine("		AND CA.ConsecutivoCompania = C.ConsecutivoCompania");
            vSql.AppendLine("	WHERE CA.ConsecutivoCompania = " + insSql.ToSqlValue(valConsecutivoCompania));
            vSql.AppendLine("		AND " + insSql.SqlDateValueBetween("", "CA.Fecha", valFechaDesde, valFechaHasta));
            vSql.AppendLine("		AND CA.CajaCerrada = " + insSql.ToSqlValue(true) + ") BaseUnpivot");
            if (valEsPAraMonedaLocal) {
                vSql.AppendLine("	UNPIVOT (MontoML FOR Movimiento IN ([01 - Efectivo], [02 - Tarjeta], [03 - Cheque], [04 - Depósito], [05 - Anticipo], [06 - Vuelto Efectivo], [07 - Vuelto Pago Móvil], [08 - Tarjeta Medios Electrónicos], [09 - C2P], [10 - Transferencia Medios Electrónicos], [11 - Pago Móvil], [12 - Depósito Medios Electrónicos]))AS CajaAperturaPorOperadorML");
            } else {
                vSql.AppendLine("	UNPIVOT (MontoME FOR Movimiento IN ([01 - Efectivo], [02 - Tarjeta], [03 - Cheque], [04 - Depósito], [05 - Anticipo], [06 - Vuelto Efectivo] ,[13 - Zelle]))AS CajaAperturaPorOperadorME");
            }
            return vSql.ToString();
        }
        #endregion //Metodos Generados
    } //End of class clsCajaSql
} //End of namespace Galac.Adm.Brl.Venta
