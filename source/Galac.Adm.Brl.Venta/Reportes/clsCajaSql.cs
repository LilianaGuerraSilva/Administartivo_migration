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

namespace Galac.Adm.Brl.Venta.Reportes {
    public class clsCajaSql {
        #region Metodos Generados

        public string SqlCuadreCajaCobroMultimonedaDetallado(int valConsecutivoCompania, DateTime valFechaInicial, DateTime valFechaFinal,Saw.Lib.eCantidadAImprimir valCantidadOperadorDeReporte, string valNombreDelOperador, Saw.Lib.eMonedaParaImpresion valMonedaDeReporte, bool valTotalesTipoCobro) {
            QAdvSql vUtilSql = new QAdvSql("");
            StringBuilder vSql = new StringBuilder();
            int vConsecutivoCajaGenerica = 0;
            string vSQLWhere = "";
            vSQLWhere = vUtilSql.SqlExpressionValueWithAnd(vSQLWhere, "factura.StatusFactura", vUtilSql.EnumToSqlValue((int) eStatusFactura.Emitida));
            vSQLWhere = vSQLWhere + " AND factura.ConsecutivoCaja" + " <> " + LibConvert.ToStr(vConsecutivoCajaGenerica);
            vSQLWhere = vSQLWhere + " AND factura.TipoDeDocumento IN ( " + vUtilSql.EnumToSqlValue((int)eTipoDocumentoFactura.Factura) + "," + vUtilSql.EnumToSqlValue((int)eTipoDocumentoFactura.ComprobanteFiscal) + "," + vUtilSql.EnumToSqlValue((int)eTipoDocumentoFactura.NotaDeCredito) + "," + vUtilSql.EnumToSqlValue((int)eTipoDocumentoFactura.NotaDeCreditoComprobanteFiscal) + ")";
            vSQLWhere = vUtilSql.SqlDateValueBetween(vSQLWhere, "factura.fecha", valFechaInicial, valFechaFinal);
            vSQLWhere = vUtilSql.SqlExpressionValueWithAnd(vSQLWhere, "factura.GeneraCobroDirecto",vUtilSql.ToSqlValue("S"));
            if (valCantidadOperadorDeReporte == Saw.Lib.eCantidadAImprimir.Uno) {
                vSQLWhere = vUtilSql.SqlValueWithAnd(vSQLWhere, "factura.NombreOperador", valNombreDelOperador);
            }
            vSQLWhere = vUtilSql.SqlIntValueWithAnd(vSQLWhere, "factura.ConsecutivoCompania", valConsecutivoCompania);
            string esFacturaValida = vUtilSql.IIF("renglonCobroDeFactura.Monto IS NULL AND (factura.TotalFactura) > 0", "(factura.TotalFactura)", vUtilSql.ToInt("0"), true);
            string esNotaDeCredito = vUtilSql.IIF("factura.TipoDeDocumento IN (" + vUtilSql.EnumToSqlValue((int)eTipoDocumentoFactura.NotaDeCredito) + "," + vUtilSql.EnumToSqlValue((int)eTipoDocumentoFactura.NotaDeCreditoComprobanteFiscal) + ")", vUtilSql.ToSqlValue("NOTAS DE CRÉDITO/DEVOLUCIONES"), vUtilSql.ToSqlValue(string.Empty), false);
            string esFacturaCobrada = vUtilSql.IIF("factura.TipoDeDocumento IN (" + vUtilSql.EnumToSqlValue((int)eTipoDocumentoFactura.Factura) + "," + vUtilSql.EnumToSqlValue((int)eTipoDocumentoFactura.ComprobanteFiscal) + ")", vUtilSql.ToSqlValue("FACTURAS COBRADAS"), esNotaDeCredito, false);
            string esFacturaNoCobrada = vUtilSql.IIF("SUM" + esFacturaValida + vUtilSql.ComparisonOp(">") + vUtilSql.ToInt("0") + "AND factura.TipoDeDocumento IN (" + vUtilSql.EnumToSqlValue((int)eTipoDocumentoFactura.Factura) + "," + vUtilSql.EnumToSqlValue((int)eTipoDocumentoFactura.ComprobanteFiscal) + ")", vUtilSql.ToSqlValue("FACTURAS NO COBRADAS"), esFacturaCobrada, true);
            vSql.AppendLine(vUtilSql.SetDateFormat());
            vSql.AppendLine(" ;WITH CTE_MonedasActivas(CodMoneda, NombreMoneda, SimboloMoneda)");
            vSql.AppendLine("	AS (SELECT");
            vSql.AppendLine("		Codigo AS CodMoneda,");
            vSql.AppendLine("		Nombre AS NombreMoneda,");
            vSql.AppendLine("		Simbolo AS SimboloMoneda");
            vSql.AppendLine("	    FROM dbo.Moneda");
            vSql.AppendLine("	    WHERE Activa = " + vUtilSql.ToSqlValue(LibConvert.BoolToSN(true)));
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
            if (valMonedaDeReporte == Saw.Lib.eMonedaParaImpresion.EnMonedaOriginal){
                vSql.AppendLine("	, MAX(factura.TotalFactura) AS TotalFactura");
                vSql.AppendLine(", " + vUtilSql.IsNull("SUM(renglonCobroDeFactura.Monto)", vUtilSql.ToSqlValue(string.Empty)) + " AS Monto");
                vSql.AppendLine("   , MAX(factura.CambioMostrarTotalEnDivisas) AS TasaOperacion");
            } else {
                vSql.AppendLine("	, MAX(factura.TotalFactura * factura.CambioABolivares) AS TotalFactura");
                vSql.AppendLine(", " + vUtilSql.IsNull("SUM(renglonCobroDeFactura.Monto * renglonCobroDeFactura.CambioAMonedaLocal)", vUtilSql.ToSqlValue(string.Empty)) + " AS Monto");
                vSql.AppendLine(", " + vUtilSql.IsNull("MonedaRenglon.SimboloMoneda", vUtilSql.ToSqlValue(string.Empty)) + " AS SimboloFormaDeCobro");
                vSql.AppendLine(", " + vUtilSql.IsNull("MAX(renglonCobroDeFactura.CambioAMonedaLocal)", vUtilSql.ToSqlValue(string.Empty)) + " AS CambioABolivares");
            }
            vSql.AppendLine(", " + vUtilSql.IsNull("FormaDelCobro.TipoDePago", vUtilSql.ToSqlValue(string.Empty)) + " AS TipoDeCobro");
            vSql.AppendLine("	, MonedaDoc.SimboloMoneda AS SimboloMonedaDoc");
            vSql.AppendLine(", " + vUtilSql.IsNull("MonedaRenglon.CodMoneda", vUtilSql.ToSqlValue(string.Empty)) + "AS CodMonedaFormaDelCobro");
            vSql.AppendLine(", " + vUtilSql.IsNull("MonedaRenglon.NombreMoneda", vUtilSql.ToSqlValue(string.Empty)) + "AS NombreMonedaFormaDelCobro");
            vSql.AppendLine(" FROM Adm.Caja");
            vSql.AppendLine(" LEFT JOIN factura");
            vSql.AppendLine("	ON factura.ConsecutivoCaja = Caja.Consecutivo");
            vSql.AppendLine("	AND factura.ConsecutivoCompania = Caja.ConsecutivoCompania");
            vSql.AppendLine(" LEFT JOIN dbo.renglonCobroDeFactura");
            vSql.AppendLine("	ON renglonCobroDeFactura.NumeroFactura = factura.Numero");
            vSql.AppendLine("	AND renglonCobroDeFactura.ConsecutivoCompania = factura.ConsecutivoCompania");
            vSql.AppendLine("   AND renglonCobroDeFactura.TipoDeDocumento = factura.TipoDeDocumento");

            vSql.AppendLine(" LEFT JOIN Saw.FormaDelCobro");
            vSql.AppendLine("   ON FormaDelCobro.Codigo = renglonCobroDeFactura.CodigoFormaDelCobro");
            vSql.AppendLine(" INNER JOIN CTE_MonedasActivas AS MonedaDoc");
            vSql.AppendLine("	ON MonedaDoc.CodMoneda = factura.CodigoMoneda");
            vSql.AppendLine(" LEFT JOIN CTE_MonedasActivas AS MonedaRenglon");
            vSql.AppendLine("   ON MonedaRenglon.CodMoneda = renglonCobroDeFactura.CodigoMoneda");
            if (LibString.Len(vSQLWhere) > 0) {
                vSql.AppendLine(vUtilSql.WhereSql(vSQLWhere));
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
            //vSql.AppendLine("   , renglonCobroDeFactura.CodigoFormaDelCobro");
            //vSql.AppendLine("	, renglonCobroDeFactura.CodigoMoneda");
            if (valMonedaDeReporte != Saw.Lib.eMonedaParaImpresion.EnMonedaOriginal) {
                vSql.AppendLine("	, MonedaRenglon.SimboloMoneda");
            }
            vSql.AppendLine("	, MonedaDoc.NombreMoneda");
            vSql.AppendLine("	, FormaDelCobro.TipoDePago");
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
		public string SqlCajasAperturadas(int valConsecutivoCompania){
            int vConsecutivoCajaGenerica = 0;
            QAdvSql vUtilSql = new QAdvSql("");
            StringBuilder vSql = new StringBuilder();
			string vSQLWhere = "";
            vSQLWhere = vUtilSql.SqlExpressionValueWithAnd(vSQLWhere, "CajaApertura.CajaCerrada", vUtilSql.ToSqlValue("N"));
            vSQLWhere = vSQLWhere + " AND Caja.Consecutivo" + " <> " + LibConvert.ToStr(vConsecutivoCajaGenerica);
            vSQLWhere = vUtilSql.SqlIntValueWithAnd(vSQLWhere, "Caja.ConsecutivoCompania", valConsecutivoCompania);
            vSql.AppendLine(" SELECT");
            vSql.AppendLine("   CajaApertura.ConsecutivoCaja AS ConsecutivoCaja");
            vSql.AppendLine("   ,  CajaApertura.NombreDelUsuario AS NombreDelUsuario");
            vSql.AppendLine("   ,  CajaApertura.HoraApertura AS HoraApertura");
            vSql.AppendLine("   ,  CajaApertura.Fecha AS FechaDeApertura");
            vSql.AppendLine("   ,  Caja.NombreCaja AS NombreCaja");
            vSql.AppendLine("   ,  Caja.Consecutivo AS ConsecutivoCaja");
            vSql.AppendLine("   ,  CajaApertura.MontoApertura AS MontoApertura");
            vSql.AppendLine(" FROM Adm.Caja");
            vSql.AppendLine(" INNER JOIN CajaApertura");
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
            QAdvSql vUtilSql = new QAdvSql("");
            string vSQLWhere = "";
            StringBuilder vSql = new StringBuilder();
            vSQLWhere = vUtilSql.SqlExpressionValueWithAnd(vSQLWhere, "factura.StatusFactura", vUtilSql.EnumToSqlValue((int)eStatusFactura.Emitida));
            vSQLWhere = vSQLWhere + " AND factura.ConsecutivoCaja" + " <> " + LibConvert.ToStr(vConsecutivoCajaGenerica);
            vSQLWhere = vSQLWhere + " AND factura.TipoDeDocumento IN ( " + vUtilSql.EnumToSqlValue((int)eTipoDocumentoFactura.Factura) + "," + vUtilSql.EnumToSqlValue((int)eTipoDocumentoFactura.ComprobanteFiscal) + "," + vUtilSql.EnumToSqlValue((int)eTipoDocumentoFactura.NotaDeCredito) + "," + vUtilSql.EnumToSqlValue((int)eTipoDocumentoFactura.NotaDeCreditoComprobanteFiscal) + ")";
            vSQLWhere = vUtilSql.SqlDateValueBetween(vSQLWhere, "factura.fecha", valFechaInicial, valFechaFinal);
            vSQLWhere = vUtilSql.SqlExpressionValueWithAnd(vSQLWhere, "factura.GeneraCobroDirecto", vUtilSql.ToSqlValue("S"));
            vSQLWhere = vUtilSql.SqlIntValueWithAnd(vSQLWhere, "factura.ConsecutivoCompania", valConsecutivoCompania);
            string menorA = vUtilSql.ComparisonOp("<");
            string mayorA = vUtilSql.ComparisonOp(">");
            string montoRenglon = vUtilSql.IsNull("renglonCobroDeFactura.Monto", vUtilSql.ToInt("0"));
            string cambioDelRenglon = vUtilSql.IsNull("renglonCobroDeFactura.CambioAMonedaLocal", vUtilSql.ToInt("1"));
            string esEfectivo = "FormaDelCobro.TipoDePago" + vUtilSql.ComparisonOp("=") + vUtilSql.EnumToSqlValue((int)eFormaDeCobro.Efectivo);
            string esTarjeta = "FormaDelCobro.TipoDePago" + vUtilSql.ComparisonOp("=") + vUtilSql.EnumToSqlValue((int)eFormaDeCobro.Tarjeta);
            string esCheque = "FormaDelCobro.TipoDePago" + vUtilSql.ComparisonOp("=") + vUtilSql.EnumToSqlValue((int)eFormaDeCobro.Cheque);
            string monedaCobro = vUtilSql.IsNull("MonedaRenglon.Nombre", "MonedaDoc.Nombre");
            string esDepTransferencia = "FormaDelCobro.TipoDePago" + vUtilSql.ComparisonOp("=") + vUtilSql.EnumToSqlValue((int)eFormaDeCobro.Deposito);
            string esNotaDeCredito = montoRenglon + menorA + vUtilSql.ToInt("0") + vUtilSql.LogicalOp("OR") + " factura.TotalFactura" + menorA + vUtilSql.ToInt("0");
            string existeVuelto = "SUM(" + montoRenglon +")" + mayorA + "factura.TotalFactura " + vUtilSql.LogicalOp("AND") + "factura.TotalFactura" + mayorA + vUtilSql.ToInt("0");
            string montoEfectivo = "SUM" + vUtilSql.IIF(esEfectivo, montoRenglon, vUtilSql.ToInt("0"),true);
            string montoTarjeta = "SUM" + vUtilSql.IIF(esTarjeta, montoRenglon, vUtilSql.ToInt("0"), true);
            string montoDepTranferencia = "SUM" + vUtilSql.IIF(esDepTransferencia, montoRenglon, vUtilSql.ToInt("0"), true);
            string montoCheque = "SUM" + vUtilSql.IIF(esCheque, montoRenglon, vUtilSql.ToInt("0"), true);
            string montoNotaDeCredito = "SUM" + vUtilSql.IIF(esNotaDeCredito, montoRenglon, vUtilSql.ToInt("0"), true);
            string monedasIguales = "renglonCobroDeFactura.CodigoMoneda" + vUtilSql.ComparisonOp("=") + "factura.CodigoMoneda";
            string calculoMontoVuelto = vUtilSql.IIF(existeVuelto, "SUM("+ montoRenglon + ") - factura.TotalFactura", vUtilSql.ToInt("0"), true);
            string montoVuelto = vUtilSql.IIF(monedasIguales, calculoMontoVuelto, vUtilSql.ToInt("0"), true);
            string montoPagadoSinConvertir = "SUM" + vUtilSql.IIF(montoRenglon + menorA + vUtilSql.ToInt("0"), vUtilSql.ToInt("0"), montoRenglon, true);
            string convEnRenglon = montoRenglon + " * " + vUtilSql.IsNull("renglonCobroDeFactura.CambioAMonedaLocal", vUtilSql.ToInt("1"));
            string montoPagadoConvertido = "SUM" + vUtilSql.IIF(convEnRenglon + " IS NULL " + vUtilSql.LogicalOp("OR") + convEnRenglon + vUtilSql.ComparisonOp("<") + vUtilSql.ToInt("0"), vUtilSql.ToInt("0"), convEnRenglon, true);
            string montoPagado = vUtilSql.IIF(monedasIguales,montoPagadoSinConvertir,montoPagadoConvertido,true);
            string esCredito = "MonedaRenglon.Nombre IS NULL" + vUtilSql.LogicalOp("AND") + "factura.TotalFactura" + vUtilSql.ComparisonOp("<>") + vUtilSql.ToInt("0");
            string montoEfectivoEnBs = "SUM" + vUtilSql.IIF(esEfectivo, montoRenglon + " * " + cambioDelRenglon, vUtilSql.ToInt("0"), true);
            string montoTarjetaEnBs = "SUM" + vUtilSql.IIF(esTarjeta, montoRenglon + " * " + cambioDelRenglon , vUtilSql.ToInt("0"), true);
            string montoDepTranferenciaEnBs = "SUM" + vUtilSql.IIF(esDepTransferencia, montoRenglon + " * " + cambioDelRenglon, vUtilSql.ToInt("0"), true);
            string montoChequeEnBs = "SUM" + vUtilSql.IIF(esCheque, montoRenglon + " * " + cambioDelRenglon, vUtilSql.ToInt("0"), true);
            string montoNotaDeCreditoEnBs = "SUM" + vUtilSql.IIF(esNotaDeCredito, montoRenglon + " * " + cambioDelRenglon, vUtilSql.ToInt("0"), true);
            string montoCredito = vUtilSql.IIF(esCredito, "factura.TotalFactura", vUtilSql.ToInt("0"), true);
            vSql.AppendLine(vUtilSql.SetDateFormat());
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
            vSql.AppendLine("   LEFT JOIN Saw.FormaDelCobro");
            vSql.AppendLine("       ON FormaDelCobro.Codigo = renglonCobroDeFactura.CodigoFormaDelCobro");
            vSql.AppendLine("   INNER JOIN dbo.Moneda AS MonedaDoc");
            vSql.AppendLine("       ON MonedaDoc.Codigo = factura.CodigoMoneda");
            vSql.AppendLine("   LEFT JOIN dbo.Moneda AS MonedaRenglon");
            vSql.AppendLine("       ON MonedaRenglon.Codigo = renglonCobroDeFactura.CodigoMoneda");
            if (LibString.Len(vSQLWhere) > 0) {
				vSql.AppendLine(vUtilSql.WhereSql(vSQLWhere));
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
            QAdvSql vUtilSql = new QAdvSql("");
            string vSQLWhereFactura = string.Empty;
            string vSQLWhereAnticipo = string.Empty;
            StringBuilder vSql = new StringBuilder();
            //ANTICIPO
            vSQLWhereAnticipo = vUtilSql.SqlEnumValueWithAnd(vSQLWhereAnticipo, "anticipo.Tipo", (int)eTipoDeAnticipo.Cobrado);
            vSQLWhereAnticipo = vSQLWhereAnticipo + " AND anticipo.Status IN ( " + vUtilSql.EnumToSqlValue((int)eStatusAnticipo.Vigente) + "," + vUtilSql.EnumToSqlValue((int) eStatusAnticipo.ParcialmenteUsado) + ")";
            vSQLWhereAnticipo = vUtilSql.SqlDateValueBetween(vSQLWhereAnticipo, "anticipo.Fecha", valFechaInicial, valFechaFinal);
            vSQLWhereAnticipo = vUtilSql.SqlExpressionValueWithAnd(vSQLWhereAnticipo, "anticipo.AsociarAnticipoACaja", vUtilSql.ToSqlValue(LibConvert.BoolToSN(true)));
            if (valCantidadOperadorDeReporte == Saw.Lib.eCantidadAImprimir.Uno && !LibString.IsNullOrEmpty(valNombreDelOperador)) {
                vSQLWhereAnticipo = vUtilSql.SqlValueWithAnd(vSQLWhereAnticipo, "anticipo.NombreOperador", valNombreDelOperador);
            }
            vSQLWhereAnticipo = vUtilSql.SqlIntValueWithAnd(vSQLWhereAnticipo, "anticipo.ConsecutivoCompania", valConsecutivoCompania);
            //FACTURA
            vSQLWhereFactura = vUtilSql.SqlExpressionValueWithAnd(vSQLWhereFactura, "factura.StatusFactura", vUtilSql.EnumToSqlValue((int)eStatusFactura.Emitida));
            vSQLWhereFactura = vSQLWhereFactura + " AND factura.ConsecutivoCaja" + " <> " + LibConvert.ToStr(vConsecutivoCajaGenerica);
            vSQLWhereFactura = vSQLWhereFactura + " AND factura.TipoDeDocumento IN ( " + vUtilSql.EnumToSqlValue((int)eTipoDocumentoFactura.Factura) + "," + vUtilSql.EnumToSqlValue((int)eTipoDocumentoFactura.ComprobanteFiscal) + "," + vUtilSql.EnumToSqlValue((int)eTipoDocumentoFactura.NotaDeCredito) + "," + vUtilSql.EnumToSqlValue((int)eTipoDocumentoFactura.NotaDeCreditoComprobanteFiscal) + ")";
            vSQLWhereFactura = vUtilSql.SqlDateValueBetween(vSQLWhereFactura, "factura.fecha", valFechaInicial, valFechaFinal);
            vSQLWhereFactura = vUtilSql.SqlExpressionValueWithAnd(vSQLWhereFactura, "factura.GeneraCobroDirecto", vUtilSql.ToSqlValue(LibConvert.BoolToSN(true)));
            if (valCantidadOperadorDeReporte == Saw.Lib.eCantidadAImprimir.Uno && !LibString.IsNullOrEmpty(valNombreDelOperador)) {
                vSQLWhereFactura = vUtilSql.SqlValueWithAnd(vSQLWhereFactura, "factura.NombreOperador", valNombreDelOperador);
            }
            vSQLWhereFactura = vUtilSql.SqlIntValueWithAnd(vSQLWhereFactura, "factura.ConsecutivoCompania", valConsecutivoCompania);
            string prefijoNroDocFactura = vUtilSql.ToSqlValue("Fact: ");
            string prefijoNroDocAnticipo = vUtilSql.ToSqlValue("Ant: ");
            string menorA = vUtilSql.ComparisonOp("<");
            string mayorA = vUtilSql.ComparisonOp(">");
            #region CondicionalesAnticipo
            string montoTotalAnticipo = "anticipo.MontoTotal";
            string montoUsadoAnticipo = "anticipo.MontoUsado";
            string cambioAnticipo = "anticipo.Cambio";
            string montoTotalAnticipoBs = montoTotalAnticipo + " * " + cambioAnticipo;
            string montoUsadoAnticipoBs = montoUsadoAnticipo + " * " + cambioAnticipo;
            #endregion CondicionalesAnticipo


            #region CondicionalesFactura
            string montoRenglon = vUtilSql.IsNull("renglonCobroDeFactura.Monto", vUtilSql.ToInt("0"));
            string cambioDelRenglon = vUtilSql.IsNull("renglonCobroDeFactura.CambioAMonedaLocal", vUtilSql.ToInt("1"));
            string esEfectivo = "FormaDelCobro.TipoDePago" + vUtilSql.ComparisonOp("=") + vUtilSql.EnumToSqlValue((int)eFormaDeCobro.Efectivo);
            string esTarjeta = "FormaDelCobro.TipoDePago" + vUtilSql.ComparisonOp("=") + vUtilSql.EnumToSqlValue((int)eFormaDeCobro.Tarjeta);
            string esCheque = "FormaDelCobro.TipoDePago" + vUtilSql.ComparisonOp("=") + vUtilSql.EnumToSqlValue((int)eFormaDeCobro.Cheque);
            string monedaCobro = vUtilSql.IsNull("MonedaRenglon.Nombre", "MonedaDoc.Nombre");
            string esDepTransferencia = "FormaDelCobro.TipoDePago" + vUtilSql.ComparisonOp("=") + vUtilSql.EnumToSqlValue((int)eFormaDeCobro.Deposito);
            string esNotaDeCredito = montoRenglon + menorA + vUtilSql.ToInt("0") + vUtilSql.LogicalOp("OR") + " factura.TotalFactura" + menorA + vUtilSql.ToInt("0");
            string esAnticipoUsado = "FormaDelCobro.TipoDePago" + vUtilSql.ComparisonOp("=") + vUtilSql.EnumToSqlValue((int) eFormaDeCobro.Anticipo);
            string existeVuelto = "SUM(" + montoRenglon +")" + mayorA + "factura.TotalFactura " + vUtilSql.LogicalOp("AND") + "factura.TotalFactura" + mayorA + vUtilSql.ToInt("0");
            string montoEfectivo = "SUM" + vUtilSql.IIF(esEfectivo, montoRenglon, vUtilSql.ToInt("0"),true);
            string montoTarjeta = "SUM" + vUtilSql.IIF(esTarjeta, montoRenglon, vUtilSql.ToInt("0"), true);
            string montoDepTranferencia = "SUM" + vUtilSql.IIF(esDepTransferencia, montoRenglon, vUtilSql.ToInt("0"), true);
            string montoCheque = "SUM" + vUtilSql.IIF(esCheque, montoRenglon, vUtilSql.ToInt("0"), true);
            string montoAnticipoUsado = "SUM" + vUtilSql.IIF(esAnticipoUsado, montoRenglon, vUtilSql.ToInt("0"), true);
            string montoNotaDeCredito = "SUM" + vUtilSql.IIF(esNotaDeCredito, montoRenglon, vUtilSql.ToInt("0"), true);
            string monedasIguales = "renglonCobroDeFactura.CodigoMoneda" + vUtilSql.ComparisonOp("=") + "factura.CodigoMoneda";
            string calculoMontoVuelto = vUtilSql.IIF(existeVuelto, "SUM("+ montoRenglon + ") - factura.TotalFactura", vUtilSql.ToInt("0"), true);
            string montoVuelto = vUtilSql.IIF(monedasIguales, calculoMontoVuelto, vUtilSql.ToInt("0"), true);
            string montoPagadoSinConvertir = "SUM" + vUtilSql.IIF(montoRenglon + menorA + vUtilSql.ToInt("0"), vUtilSql.ToInt("0"), montoRenglon, true);
            string convEnRenglon = montoRenglon + " * " + vUtilSql.IsNull("renglonCobroDeFactura.CambioAMonedaLocal", vUtilSql.ToInt("1"));
            string montoPagadoConvertido = "SUM" + vUtilSql.IIF(convEnRenglon + " IS NULL " + vUtilSql.LogicalOp("OR") + convEnRenglon + menorA + vUtilSql.ToInt("0"), vUtilSql.ToInt("0"), convEnRenglon, true);

            string montoPagado = vUtilSql.IIF(monedasIguales,montoPagadoSinConvertir,montoPagadoConvertido,true);
            string esCredito = "MonedaRenglon.Nombre IS NULL" + vUtilSql.LogicalOp("AND") + "factura.TotalFactura" + vUtilSql.ComparisonOp("<>") + vUtilSql.ToInt("0");
            string montoEfectivoEnBs = "SUM" + vUtilSql.IIF(esEfectivo, montoRenglon + " * " + cambioDelRenglon, vUtilSql.ToInt("0"), true);
            string montoTarjetaEnBs = "SUM" + vUtilSql.IIF(esTarjeta, montoRenglon + " * " + cambioDelRenglon , vUtilSql.ToInt("0"), true);
            string montoDepTranferenciaEnBs = "SUM" + vUtilSql.IIF(esDepTransferencia, montoRenglon + " * " + cambioDelRenglon, vUtilSql.ToInt("0"), true);
            string montoChequeEnBs = "SUM" + vUtilSql.IIF(esCheque, montoRenglon + " * " + cambioDelRenglon, vUtilSql.ToInt("0"), true);
            string montoAnticipoUsadoEnBs = "SUM" + vUtilSql.IIF(esAnticipoUsado, montoRenglon + " * " + cambioDelRenglon, vUtilSql.ToInt("0"), true);
            string montoNotaDeCreditoEnBs = "SUM" + vUtilSql.IIF(esNotaDeCredito, montoRenglon + " * " + cambioDelRenglon, vUtilSql.ToInt("0"), true);
            string montoCredito = vUtilSql.IIF(esCredito, "factura.TotalFactura", vUtilSql.ToInt("0"), true);
            #endregion CondicionalesFactura
			
            vSql.AppendLine(vUtilSql.SetDateFormat());
            vSql.AppendLine("SELECT");
            vSql.AppendLine("    anticipo.NombreOperador AS NombreDelOperador");
            vSql.AppendLine("    , anticipo.Moneda AS MonedaDoc");
            vSql.AppendLine("    , anticipo.Moneda AS MonedaCobro");
            vSql.AppendLine("    , " + vUtilSql.IsNull("anticipo.CodigoMoneda", vUtilSql.ToSqlValue(string.Empty)) + " AS CodMoneda");
            vSql.AppendLine("    , anticipo.ConsecutivoCaja AS ConsecutivoCaja");
            vSql.AppendLine("    , caja.NombreCaja AS NombreCaja");
            vSql.AppendLine("    ," + prefijoNroDocAnticipo + vUtilSql.CharConcat() + "anticipo.Numero AS NumeroDoc");
            vSql.AppendLine("    ," + vUtilSql.ToSqlValue(string.Empty) + " AS NumFiscal");
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
                vSql.AppendLine(vUtilSql.WhereSql(vSQLWhereAnticipo));
            }
            vSql.AppendLine("   UNION");
            vSql.AppendLine("   SELECT");
            vSql.AppendLine("   factura.NombreOperador AS NombreDelOperador");
            vSql.AppendLine("   , MonedaDoc.Nombre AS MonedaDoc");
            vSql.AppendLine("   , " + monedaCobro + " AS MonedaCobro");
            vSql.AppendLine("    , " + vUtilSql.IsNull("MonedaRenglon.Codigo", vUtilSql.ToSqlValue(string.Empty)) + " AS CodMoneda");
            vSql.AppendLine("   , factura.ConsecutivoCaja AS ConsecutivoCaja");
            vSql.AppendLine("   , caja.NombreCaja AS NombreCaja");
            vSql.AppendLine("   , " + prefijoNroDocFactura + vUtilSql.CharConcat() + "factura.Numero AS NumeroDoc");
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
            vSql.AppendLine("   LEFT JOIN Saw.FormaDelCobro");
            vSql.AppendLine("       ON FormaDelCobro.Codigo = renglonCobroDeFactura.CodigoFormaDelCobro");
            vSql.AppendLine("   INNER JOIN dbo.Moneda AS MonedaDoc");
            vSql.AppendLine("       ON MonedaDoc.Codigo = factura.CodigoMoneda");
            vSql.AppendLine("   LEFT JOIN dbo.Moneda AS MonedaRenglon");
            vSql.AppendLine("       ON MonedaRenglon.Codigo = renglonCobroDeFactura.CodigoMoneda");
            if (LibString.Len(vSQLWhereFactura) > 0) {
                vSql.AppendLine(vUtilSql.WhereSql(vSQLWhereFactura));
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
            QAdvSql vUtilSql = new QAdvSql("");
            StringBuilder vSql = new StringBuilder();
            int vConsecutivoCajaGenerica = 0;
            string vSQLWhere = "";
            vSQLWhere = vUtilSql.SqlExpressionValueWithAnd(vSQLWhere, "factura.StatusFactura", vUtilSql.EnumToSqlValue((int) eStatusFactura.Emitida));
            vSQLWhere = vSQLWhere + " AND factura.ConsecutivoCaja" + " <> " + LibConvert.ToStr(vConsecutivoCajaGenerica);
            vSQLWhere = vSQLWhere + " AND factura.TipoDeDocumento IN ( " + vUtilSql.EnumToSqlValue((int)eTipoDocumentoFactura.Factura) + "," + vUtilSql.EnumToSqlValue((int)eTipoDocumentoFactura.ComprobanteFiscal) + "," + vUtilSql.EnumToSqlValue((int)eTipoDocumentoFactura.NotaDeCredito) + "," + vUtilSql.EnumToSqlValue((int)eTipoDocumentoFactura.NotaDeCreditoComprobanteFiscal) + ")";
            vSQLWhere = vUtilSql.SqlDateValueBetween(vSQLWhere, "factura.fecha", valFechaInicial, valFechaFinal);
            vSQLWhere = vUtilSql.SqlExpressionValueWithAnd(vSQLWhere, "factura.GeneraCobroDirecto",vUtilSql.ToSqlValue("S"));
            vSQLWhere = vUtilSql.SqlIntValueWithAnd(vSQLWhere, "factura.ConsecutivoCompania", valConsecutivoCompania);

            vSql.AppendLine(vUtilSql.SetDateFormat());
            vSql.AppendLine(" SELECT");
            vSql.AppendLine("	Caja.Consecutivo");
            vSql.AppendLine("	, Caja.NombreCaja");
            vSql.AppendLine("   , factura.Moneda AS MonedaDoc");
            vSql.AppendLine("	, factura.Fecha");
            vSql.AppendLine("	, factura.Numero AS NumeroDoc");
            vSql.AppendLine("	, factura.NumeroComprobanteFiscal As NumFiscal");
            vSql.AppendLine("   , Cliente.Nombre AS NombreCliente");
            if (valMonedaDeReporte == Saw.Lib.eMonedaParaImpresion.EnMonedaOriginal){
                vSql.AppendLine("	, MAX(factura.TotalFactura) AS MontoDoc");
                vSql.AppendLine("   , " + vUtilSql.IsNull("SUM(renglonCobroDeFactura.Monto)", vUtilSql.ToCur("0")) + " AS MontoCobro");
                vSql.AppendLine("   , MAX(factura.CambioMostrarTotalEnDivisas) AS TasaOperacion");
            } else {
                vSql.AppendLine("	, MAX(factura.TotalFactura * factura.CambioABolivares) AS MontoDoc");
                vSql.AppendLine(", " + vUtilSql.IsNull("SUM(renglonCobroDeFactura.Monto * renglonCobroDeFactura.CambioAMonedaLocal)", vUtilSql.ToCur("0")) + " AS MontoCobro");
                vSql.AppendLine(", " + vUtilSql.IsNull("MAX(renglonCobroDeFactura.CambioAMonedaLocal)", vUtilSql.ToCur("0")) + " AS CambioABolivares");
            }
            vSql.AppendLine(", " + vUtilSql.IsNull("FormaDelCobro.TipoDePago", vUtilSql.ToSqlValue(string.Empty)) + " AS TipoDeCobro");
            vSql.AppendLine(", " + vUtilSql.IsNull("MonedaRenglon.Nombre", vUtilSql.ToSqlValue(string.Empty)) + "AS NombreMonedaFormaDelCobro");
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
            vSql.AppendLine(" INNER JOIN Saw.FormaDelCobro");
            vSql.AppendLine("   ON FormaDelCobro.Codigo = renglonCobroDeFactura.CodigoFormaDelCobro");
            vSql.AppendLine(" INNER JOIN dbo.Moneda AS MonedaRenglon");
            vSql.AppendLine("   ON MonedaRenglon.Codigo = renglonCobroDeFactura.CodigoMoneda");
            if (LibString.Len(vSQLWhere) > 0) {
                vSql.AppendLine(vUtilSql.WhereSql(vSQLWhere));
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
            vSql.AppendLine("	, FormaDelCobro.TipoDePago");
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
            QAdvSql vUtilSql = new QAdvSql("");
            StringBuilder vSql = new StringBuilder();
            int vConsecutivoCajaGenerica = 0;
            string vSQLWhere = "";
            vSQLWhere = vUtilSql.SqlExpressionValueWithAnd(vSQLWhere, "factura.StatusFactura", vUtilSql.EnumToSqlValue((int) eStatusFactura.Emitida));
            vSQLWhere = vSQLWhere + " AND factura.ConsecutivoCaja" + " <> " + LibConvert.ToStr(vConsecutivoCajaGenerica);
            vSQLWhere = vSQLWhere + " AND factura.TipoDeDocumento IN ( " + vUtilSql.EnumToSqlValue((int)eTipoDocumentoFactura.Factura) + "," + vUtilSql.EnumToSqlValue((int)eTipoDocumentoFactura.ComprobanteFiscal) + "," + vUtilSql.EnumToSqlValue((int)eTipoDocumentoFactura.NotaDeCredito) + "," + vUtilSql.EnumToSqlValue((int)eTipoDocumentoFactura.NotaDeCreditoComprobanteFiscal) + ")";
            vSQLWhere = vUtilSql.SqlDateValueBetween(vSQLWhere, "factura.fecha", valFechaInicial, valFechaFinal);
            vSQLWhere = vUtilSql.SqlExpressionValueWithAnd(vSQLWhere, "factura.GeneraCobroDirecto",vUtilSql.ToSqlValue("S"));
            vSQLWhere = vUtilSql.SqlIntValueWithAnd(vSQLWhere, "factura.ConsecutivoCompania", valConsecutivoCompania);

            vSql.AppendLine(vUtilSql.SetDateFormat());
            vSql.AppendLine(" ;WITH CTE_TotalCaja(ConsecutivoCompañia, ConsecutivoCaja, MontoTotalCaja)");
            vSql.AppendLine(" AS");
            vSql.AppendLine("(SELECT");
            vSql.AppendLine("	factura.ConsecutivoCompania");
            vSql.AppendLine("	, factura.ConsecutivoCaja AS ConsecutivoCaja");
            vSql.AppendLine("	, SUM(factura.TotalFactura) AS MontoTotalCaja");
            vSql.AppendLine(" FROM dbo.factura");
            if (LibString.Len(vSQLWhere) > 0) {
                vSql.AppendLine(vUtilSql.WhereSql(vSQLWhere));
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
                vSql.AppendLine("    , " + vUtilSql.IsNull("SUM(renglonCobroDeFactura.Monto)", vUtilSql.ToInt("0")) + " AS MontoCobro");
            } else {
                vSql.AppendLine("    , " + vUtilSql.IsNull("SUM(renglonCobroDeFactura.Monto * renglonCobroDeFactura.CambioAMonedaLocal)", vUtilSql.ToInt("0")) + " AS MontoCobro");
            }
            vSql.AppendLine("    , " + vUtilSql.IsNull("FormaDelCobro.TipoDePago", vUtilSql.ToSqlValue(string.Empty)) + " AS TipoDeCobro");
            vSql.AppendLine(" FROM Adm.Caja");
            vSql.AppendLine("    INNER JOIN factura");
            vSql.AppendLine("       ON factura.ConsecutivoCaja = Caja.Consecutivo");
            vSql.AppendLine("       AND factura.ConsecutivoCompania = Caja.ConsecutivoCompania");
            vSql.AppendLine("    INNER JOIN dbo.renglonCobroDeFactura");
            vSql.AppendLine("       ON renglonCobroDeFactura.NumeroFactura = factura.Numero");
            vSql.AppendLine("       AND renglonCobroDeFactura.ConsecutivoCompania = factura.ConsecutivoCompania");
            vSql.AppendLine("       AND renglonCobroDeFactura.TipoDeDocumento = factura.TipoDeDocumento");
            vSql.AppendLine("    INNER JOIN Saw.FormaDelCobro");
            vSql.AppendLine("       ON FormaDelCobro.Codigo = renglonCobroDeFactura.CodigoFormaDelCobro");
            vSql.AppendLine("    INNER JOIN dbo.Moneda AS MonedaRenglon");
            vSql.AppendLine("       ON MonedaRenglon.Codigo = renglonCobroDeFactura.CodigoMoneda");
            vSql.AppendLine("	INNER JOIN CTE_TotalCaja");
            vSql.AppendLine("	    ON CTE_TotalCaja.ConsecutivoCaja = Caja.Consecutivo");
            vSql.AppendLine("	    AND CTE_TotalCaja.ConsecutivoCompañia = Caja.ConsecutivoCompania");
            if (LibString.Len(vSQLWhere) > 0) {
                vSql.AppendLine(vUtilSql.WhereSql(vSQLWhere));
            }
            vSql.AppendLine(" GROUP BY Caja.Consecutivo,");
            vSql.AppendLine("    Caja.NombreCaja,");
            vSql.AppendLine("    factura.Moneda,");
            vSql.AppendLine("    MonedaRenglon.Nombre,");
            vSql.AppendLine("    FormaDelCobro.TipoDePago,");
            vSql.AppendLine("    MonedaRenglon.Codigo,");
            vSql.AppendLine("	 CTE_TotalCaja.MontoTotalCaja");
            vSql.AppendLine(" ORDER BY MonedaRenglon.Nombre,");
            vSql.AppendLine("    Caja.Consecutivo,");
            vSql.AppendLine("    Caja.NombreCaja");
            return vSql.ToString();
		}
		
		public string SqlCuadreCajaPorUsuario(int valConsecutivoCompania, DateTime valFechaInicial, DateTime valFechaFinal, Saw.Lib.eTipoDeInforme valTipoDeInforme, Saw.Lib.eMonedaParaImpresion valMonedaDeReporte, Saw.Lib.eCantidadAImprimir valCantidadOperadorDeReporte, string valNombreDelOperador) {
            const int vConsecutivoCajaGenerica = 0;
            QAdvSql vUtilSql = new QAdvSql("");
            StringBuilder vSql = new StringBuilder();
            #region Filtro de consulta
            string vSQLWhere = "";
            vSQLWhere = vUtilSql.SqlExpressionValueWithAnd(vSQLWhere, "factura.StatusFactura", vUtilSql.EnumToSqlValue((int)eStatusFactura.Emitida));
            vSQLWhere = vSQLWhere + " AND factura.ConsecutivoCaja" + " <> " + LibConvert.ToStr(vConsecutivoCajaGenerica);
            vSQLWhere = vSQLWhere + " AND factura.TipoDeDocumento IN ( " + vUtilSql.EnumToSqlValue((int)eTipoDocumentoFactura.Factura) + "," + vUtilSql.EnumToSqlValue((int)eTipoDocumentoFactura.ComprobanteFiscal) + "," + vUtilSql.EnumToSqlValue((int)eTipoDocumentoFactura.NotaDeCredito) + "," + vUtilSql.EnumToSqlValue((int)eTipoDocumentoFactura.NotaDeCreditoComprobanteFiscal) + ")";
            vSQLWhere = vUtilSql.SqlDateValueBetween(vSQLWhere, "factura.fecha", valFechaInicial, valFechaFinal);
            vSQLWhere = vUtilSql.SqlExpressionValueWithAnd(vSQLWhere, "factura.GeneraCobroDirecto", vUtilSql.ToSqlValue("S"));
            #region Manejo para Fechas de Apertura y Cierre de Caja            
            string horaCierre = " AND factura.HoraModificacion BETWEEN  CajaApertura.HoraApertura AND " + vUtilSql.IIF("CajaApertura.HoraCierre <> " + vUtilSql.ToSqlValue(string.Empty), "CajaApertura.HoraCierre", vUtilSql.ToSqlValue("23:59"), true);
            string fechaCierre = " AND Factura.Fecha = CajaApertura.Fecha";
            #endregion Manejo para Fechas de Apertura y Cierre de Caja
            vSQLWhere = vSQLWhere + fechaCierre + horaCierre;            
            if (valCantidadOperadorDeReporte == Saw.Lib.eCantidadAImprimir.Uno && !LibString.IsNullOrEmpty(valNombreDelOperador)) {
                vSQLWhere = vUtilSql.SqlValueWithAnd(vSQLWhere, "Factura.NombreOperador", valNombreDelOperador);
            }
            vSQLWhere = vUtilSql.SqlIntValueWithAnd(vSQLWhere, "factura.ConsecutivoCompania", valConsecutivoCompania);
            #endregion Filtro de consulta
            #region Calculo Vuelto
            string mayorA = vUtilSql.ComparisonOp(">");
            string monedasIguales = "MonedaRenglon.Codigo" + vUtilSql.ComparisonOp("=") + "factura.CodigoMoneda";
            string montoRenglon = vUtilSql.IsNull("renglonCobroDeFactura.Monto", vUtilSql.ToInt("0"));
            string existeVuelto = "SUM(" + montoRenglon + ")" + mayorA + "factura.TotalFactura " + vUtilSql.LogicalOp("AND") + "factura.TotalFactura" + mayorA + vUtilSql.ToInt("0");
            string calculoMontoVuelto = vUtilSql.IIF(existeVuelto, "SUM(" + montoRenglon + ") - factura.TotalFactura", vUtilSql.ToInt("0"), true);
            string montoVuelto = vUtilSql.IIF(monedasIguales, calculoMontoVuelto, vUtilSql.ToInt("0"), true);
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
            if(LibString.Len(vSQLWhere) > 0) {
                vSql.AppendLine(vUtilSql.WhereSql(vSQLWhere));
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
        #endregion //Metodos Generados


    } //End of class clsCajaSql

} //End of namespace Galac.Adm.Brl.Venta

