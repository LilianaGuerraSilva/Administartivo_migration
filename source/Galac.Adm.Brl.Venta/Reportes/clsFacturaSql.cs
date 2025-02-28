using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.DefGen;
using Galac.Adm.Ccl.Venta;
using Galac.Saw.Ccl.SttDef;
using Galac.Comun.Ccl.TablasGen;
using Galac.Saw.Ccl.Cliente;
using Galac.Saw.Ccl.Tablas;

namespace Galac.Adm.Brl.Venta.Reportes {
    public class clsFacturaSql {
        private QAdvSql insSql = new QAdvSql("");

        #region Metodos Generados
        public string SqlFacturacionPorArticulo(int valConsecutivoCompania, DateTime valFechaDesde, DateTime valFechaHasta, string valCodigoDelArticulo, Saw.Lib.eMonedaParaImpresion valMonedaDelReporte, Saw.Lib.eTasaDeCambioParaImpresion valTipoTasaDeCambio, bool valIsInformeDetallado) {
            StringBuilder vSql = new StringBuilder();
            string vSQLWhere = "";

            Comun.Ccl.TablasGen.IMonedaLocalActual vMonedaLocalActual = new Comun.Brl.TablasGen.clsMonedaLocalActual();
            vMonedaLocalActual.CargarTodasEnMemoriaYAsignarValoresDeLaActual(LibDefGen.ProgramInfo.Country, LibDate.Today());
            string vMonedaLocal = vMonedaLocalActual.NombreMoneda(LibDate.Today());
            bool vIsInMonedaLocal = LibString.S1IsInS2(vMonedaLocal, valMonedaDelReporte.GetDescription());
            bool vIsInTasaDelDia = valTipoTasaDeCambio == Saw.Lib.eTasaDeCambioParaImpresion.DelDia;

            string vSqlCambio = SqlCambioParaReportesDeFactura(vIsInMonedaLocal, vIsInTasaDelDia);

            bool vUsaPrecioSinIva = LibConvert.SNToBool(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "UsaPrecioSinIva"));

            string vSqlPrecioUnitario = vSqlCambio + " * RenglonFactura." + (vUsaPrecioSinIva ? "PrecioSinIVA" : "PrecioConIVA");
            string vSqlMonto = vSqlCambio + " * (RenglonFactura.TotalRenglon * (1 - Factura.PorcentajeDescuento / 100))";
            vSqlMonto = insSql.RoundToNDecimals(vSqlMonto, 2);

            vSql.AppendLine(CteArticuloInventarioSql());
            vSql.AppendLine("SELECT");
            vSql.AppendLine("CTE_ArticuloInventario.Codigo,");
            vSql.AppendLine("CTE_ArticuloInventario.Descripcion,");
            vSql.AppendLine("CTE_ArticuloInventario.UnidadDeVenta,");
            vSql.AppendLine((vIsInMonedaLocal ? insSql.ToSqlValue(vMonedaLocal) : "Factura.Moneda") + " AS Moneda,");
            vSql.AppendLine("Factura.Fecha,");

            if (valIsInformeDetallado) {
                vSql.AppendLine("Factura.CodigoCliente,");
                vSql.AppendLine("Cliente.Nombre,");
                vSql.AppendLine("Factura.Numero,");
                vSql.AppendLine("RenglonFactura.Cantidad,");
                vSql.AppendLine(vSqlPrecioUnitario + " AS PrecioUnitario,");
                vSql.AppendLine(vSqlMonto + " AS Monto");

                vSql.AppendLine("FROM");
                vSql.AppendLine("Cliente");
                vSql.AppendLine("INNER JOIN (");
            } else {
                vSql.AppendLine("SUM(RenglonFactura.Cantidad) AS Cantidad,");
                vSql.AppendLine("SUM(" + vSqlPrecioUnitario + ") AS PrecioUnitario,");
                vSql.AppendLine("SUM(" + vSqlMonto + ") AS Monto");

                vSql.AppendLine("FROM");
            }

            vSql.AppendLine("Factura");
            vSql.AppendLine("INNER JOIN (");
            vSql.AppendLine("CTE_ArticuloInventario");
            vSql.AppendLine("INNER JOIN RenglonFactura");
            vSql.AppendLine("ON CTE_ArticuloInventario.ConsecutivoCompania = RenglonFactura.ConsecutivoCompania");
            vSql.AppendLine("AND CTE_ArticuloInventario.Codigo = RenglonFactura.Articulo");
            vSql.AppendLine("AND CTE_ArticuloInventario.Serial = RenglonFactura.Serial");
            vSql.AppendLine("AND CTE_ArticuloInventario.Rollo = RenglonFactura.Rollo");
            vSql.AppendLine(")");
            vSql.AppendLine("ON Factura.ConsecutivoCompania = RenglonFactura.ConsecutivoCompania");
            vSql.AppendLine("AND Factura.Numero = RenglonFactura.NumeroFactura");
            vSql.AppendLine("AND Factura.TipoDeDocumento = RenglonFactura.TipoDeDocumento");

            if (valIsInformeDetallado) {
                vSql.AppendLine(")");
                vSql.AppendLine("ON Cliente.ConsecutivoCompania = Factura.ConsecutivoCompania");
                vSql.AppendLine("AND Cliente.Codigo = Factura.CodigoCliente");
            }

            vSQLWhere = insSql.SqlIntValueWithAnd(vSQLWhere, "Factura.ConsecutivoCompania", valConsecutivoCompania);
            vSQLWhere = insSql.SqlDateValueBetween(vSQLWhere, "Factura.Fecha", valFechaDesde, valFechaHasta);
            vSQLWhere = insSql.SqlEnumValueWithAnd(vSQLWhere, "Factura.StatusFactura", (int)Ccl.Venta.eStatusFactura.Emitida);
            vSQLWhere = insSql.SqlEnumValueWithOperators(vSQLWhere, "Factura.TipoDeDocumento", (int)Saw.Ccl.SttDef.eTipoDocumentoFactura.ResumenDiarioDeVentas, "AND", "<>");
            vSQLWhere = insSql.SqlEnumValueWithOperators(vSQLWhere, "Factura.TipoDeDocumento", (int)Saw.Ccl.SttDef.eTipoDocumentoFactura.NotaEntrega, "AND", "<>");

            if (!LibString.IsNullOrEmpty(valCodigoDelArticulo)) {
                vSQLWhere = insSql.SqlValueWithAnd(vSQLWhere, "CTE_ArticuloInventario.Codigo", valCodigoDelArticulo);
            }

            vSQLWhere = insSql.WhereSql(vSQLWhere);
            vSql.AppendLine(vSQLWhere);

            if (!valIsInformeDetallado) {
                vSql.AppendLine("GROUP BY");
                vSql.AppendLine("CTE_ArticuloInventario.Codigo,");
                vSql.AppendLine("CTE_ArticuloInventario.Descripcion,");
                vSql.AppendLine("CTE_ArticuloInventario.UnidadDeVenta,");
                if (!vIsInMonedaLocal) {
                    vSql.AppendLine("Moneda,");
                }
                vSql.AppendLine("Factura.Fecha");
            }

            vSql.AppendLine("ORDER BY");
            vSql.AppendLine("CTE_ArticuloInventario.Codigo,");
            vSql.AppendLine("Moneda,");
            vSql.AppendLine("Factura.Fecha");

            if (valIsInformeDetallado) {
                vSql.AppendLine(", Factura.TipoDeDocumento, Factura.Numero");
            }

            return vSql.ToString();
        }

        public string SqlFacturacionPorUsuario(int valConsecutivoCompania, DateTime valFechaDesde, DateTime valFechaHasta, string valNombreOperador, Saw.Lib.eMonedaParaImpresion valMonedaDelReporte, Saw.Lib.eTasaDeCambioParaImpresion valTipoTasaDeCambio, bool valIsInformeDetallado) {
            StringBuilder vSql = new StringBuilder();
            string vSQLWhere = "";

            Comun.Ccl.TablasGen.IMonedaLocalActual vMonedaLocalActual = new Comun.Brl.TablasGen.clsMonedaLocalActual();
            vMonedaLocalActual.CargarTodasEnMemoriaYAsignarValoresDeLaActual(LibDefGen.ProgramInfo.Country, LibDate.Today());
            string vMonedaLocal = vMonedaLocalActual.NombreMoneda(LibDate.Today());
            bool vIsInMonedaLocal = LibString.S1IsInS2(vMonedaLocal, valMonedaDelReporte.GetDescription());
            bool vIsInTasaDelDia = valTipoTasaDeCambio == Saw.Lib.eTasaDeCambioParaImpresion.DelDia;

            string vSqlCambio = SqlCambioParaReportesDeFactura(vIsInMonedaLocal, vIsInTasaDelDia);

            bool vUsaModuloContabilidad = LibConvert.SNToBool(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "UsaModuloDeContabilidad"));
            bool vUsaPrecioSinIva = LibConvert.SNToBool(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "UsaPrecioSinIva"));

            if (vUsaModuloContabilidad) {
                vSql.AppendLine(CteComprobantesSql(valConsecutivoCompania, valFechaDesde, valFechaHasta));
            }
            vSql.AppendLine("SELECT");
            vSql.AppendLine("Factura.NombreOperador,");
            vSql.AppendLine("Factura.Fecha,");
            vSql.AppendLine("Factura.Numero,");
            vSql.AppendLine("Factura.PorcentajeDescuento AS PorcentajeDescuentoFactura,");
            vSql.AppendLine("Factura.TipoDeDocumento,");
            vSql.AppendLine("Cliente.Nombre,");
            vSql.AppendLine((vIsInMonedaLocal ? insSql.ToSqlValue(vMonedaLocal) : "Factura.Moneda") + " AS Moneda,");
            vSql.AppendLine(vSqlCambio + " As Cambio,");
            vSql.AppendLine(vSqlCambio + " * Factura.TotalFactura As TotalFactura");

            if (vUsaModuloContabilidad) {
                vSql.AppendLine("," + insSql.IIF("CTE_Comprobante.NumeroComprobante <>" + insSql.ToSqlValue(""), "CTE_Comprobante.NumeroComprobante", insSql.ToSqlValue("No Aplica"), true) + "AS NumeroComprobante");
            }

            if (valIsInformeDetallado) {
                vSql.AppendLine(", RenglonFactura.Articulo, RenglonFactura.Descripcion, RenglonFactura.PorcentajeDescuento AS PorcentajeDescuentoRenglon, RenglonFactura.Cantidad,");
                vSql.AppendLine(vSqlCambio + " * RenglonFactura." + (vUsaPrecioSinIva ? "PrecioSinIVA" : "PrecioConIVA") + " As Precio,");
                vSql.AppendLine(vSqlCambio + " * RenglonFactura.TotalRenglon As TotalRenglon");
            }

            vSql.AppendLine("FROM Cliente INNER JOIN Factura ON (Cliente.Codigo = Factura.CodigoCliente AND Cliente.ConsecutivoCompania = Factura.ConsecutivoCompania)");
            if (valIsInformeDetallado) {
                vSql.AppendLine("INNER JOIN RenglonFactura ON (Factura.Numero = RenglonFactura.NumeroFactura AND Factura.TipoDeDocumento = RenglonFactura.TipoDeDocumento AND Factura.ConsecutivoCompania = RenglonFactura.ConsecutivoCompania)");
            }

            if (vUsaModuloContabilidad) {
                vSql.Append(SqlUsaContabilidad());
            }

            vSQLWhere = insSql.SqlIntValueWithAnd(vSQLWhere, "Factura.ConsecutivoCompania", valConsecutivoCompania);
            vSQLWhere = insSql.SqlDateValueBetween(vSQLWhere, "Factura.Fecha", valFechaDesde, valFechaHasta);
            vSQLWhere = insSql.SqlEnumValueWithAnd(vSQLWhere, "Factura.StatusFactura", (int)Ccl.Venta.eStatusFactura.Emitida);
            vSQLWhere = insSql.SqlEnumValueWithOperators(vSQLWhere, "Factura.TipoDeDocumento", (int)Saw.Ccl.SttDef.eTipoDocumentoFactura.ResumenDiarioDeVentas, "AND", "<>");
            vSQLWhere = insSql.SqlEnumValueWithOperators(vSQLWhere, "Factura.TipoDeDocumento", (int)Saw.Ccl.SttDef.eTipoDocumentoFactura.NotaEntrega, "AND", "<>");

            if (!LibString.IsNullOrEmpty(valNombreOperador)) {
                vSQLWhere = insSql.SqlValueWithAnd(vSQLWhere, "Factura.NombreOperador", valNombreOperador);
            }

            vSQLWhere = insSql.WhereSql(vSQLWhere);
            vSql.AppendLine(vSQLWhere);

            vSql.AppendLine("ORDER BY");
            vSql.AppendLine("Factura.NombreOperador,");
            vSql.AppendLine("Moneda,");
            vSql.AppendLine("Factura.Fecha,");
            vSql.AppendLine("Factura.TipoDeDocumento,");
            vSql.AppendLine("Factura.Numero");

            return vSql.ToString();
        }

        public string SqlFacturaBorradorGenerico(int valConsecutivoCompania, string valNumeroDocumento, eTipoDocumentoFactura valTipoDocumento, eStatusFactura valStatusDocumento, eTalonario valTalonario, eFormaDeOrdenarDetalleFactura valFormaDeOrdenarDetalleFactura, bool valImprimirFacturaConSubtotalesPorLineaDeProducto, string valCiudadCompania, string valNombreOperador) {
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("; WITH ");
            #region CTE_Moneda
            vSql.AppendLine("CTE_Moneda AS(");
            vSql.AppendLine("SELECT ML.CodigoMoneda, M.Nombre AS NombreMoneda, M.Simbolo");
            vSql.AppendLine("FROM Comun.MonedaLocal ML INNER JOIN Moneda M");
            vSql.AppendLine("ON ML.CodigoMoneda = M.Codigo");
            vSql.AppendLine(")");
            #endregion CTE_Moneda
            #region CTE_SttByCia
            vSql.AppendLine(", ");
            vSql.AppendLine("CTE_SttByCia AS (");
            vSql.AppendLine("SELECT ConsecutivoCompania, ");
            vSql.AppendLine("       CodigoMonedaLocal,");
            vSql.AppendLine("       NombreMonedaLocal,");
            vSql.AppendLine("       CodigoMonedaExtranjera,");
            vSql.AppendLine("       NombreMonedaExtranjera,");
            vSql.AppendLine("       UsaCamposDefinibles,");
            vSql.AppendLine("       NombreCampoDefinible1, NombreCampoDefinible2, NombreCampoDefinible3, NombreCampoDefinible4, ");
            vSql.AppendLine("       NombreCampoDefinible5, NombreCampoDefinible6, NombreCampoDefinible7, NombreCampoDefinible8, ");
            vSql.AppendLine("       NombreCampoDefinible9, NombreCampoDefinible10, NombreCampoDefinible11, NombreCampoDefinible12, ");
            vSql.AppendLine("       NombreCampoDefinibleCliente1,");
            vSql.AppendLine("       NombreCampoDefinibleInventario1, NombreCampoDefinibleInventario2, NombreCampoDefinibleInventario3, NombreCampoDefinibleInventario4, NombreCampoDefinibleInventario5,");
            vSql.AppendLine("       UsaCamposExtrasEnRenglonFactura");
            vSql.AppendLine("FROM (");
            vSql.AppendLine("    SELECT Value, NameSettDefinition, ConsecutivoCompania");
            vSql.AppendLine("    FROM Comun.SettValueByCompany");
            vSql.AppendLine("    WHERE ConsecutivoCompania = " + insSql.ToSqlValue(valConsecutivoCompania));
            vSql.AppendLine(") AS SourceTable PIVOT (MAX(Value) FOR NameSettDefinition IN (");
            vSql.AppendLine("        CodigoMonedaLocal,");
            vSql.AppendLine("        NombreMonedaLocal,");
            vSql.AppendLine("        CodigoMonedaExtranjera,");
            vSql.AppendLine("        NombreMonedaExtranjera,");
            vSql.AppendLine("        UsaCamposDefinibles,");
            vSql.AppendLine("        NombreCampoDefinible1, NombreCampoDefinible2, NombreCampoDefinible3, NombreCampoDefinible4, ");
            vSql.AppendLine("        NombreCampoDefinible5, NombreCampoDefinible6, NombreCampoDefinible7, NombreCampoDefinible8, ");
            vSql.AppendLine("        NombreCampoDefinible9, NombreCampoDefinible10, NombreCampoDefinible11, NombreCampoDefinible12, ");
            vSql.AppendLine("        NombreCampoDefinibleCliente1,");
            vSql.AppendLine("        NombreCampoDefinibleInventario1, NombreCampoDefinibleInventario2, NombreCampoDefinibleInventario3, NombreCampoDefinibleInventario4, NombreCampoDefinibleInventario5,");
            vSql.AppendLine("        UsaCamposExtrasEnRenglonFactura");
            vSql.AppendLine("    )) AS PivotTable");
            vSql.AppendLine(") ");
            #endregion CTE_SttByCia
            vSql.AppendLine();
            vSql.AppendLine("SELECT");
            #region Datos del Documento
            vSql.AppendLine("    Fact.Numero, ");
            vSql.AppendLine("    Fact.TipoDeDocumento,");
            vSql.AppendLine("    (CASE Fact.TipoDeDocumento WHEN '0' THEN 'Factura' WHEN '1' THEN 'Nota de Crédito' WHEN '2' THEN 'Nota de Débito' WHEN '3' THEN 'Resumen Diario de Ventas' WHEN '4' THEN 'NO ASIGNADO' WHEN '5' THEN 'Comprobante Fiscal' WHEN '6' THEN 'BOLETA' WHEN '7' THEN 'Nota de Crédito Comprobante Fiscal' WHEN '8' THEN 'Nota de Entrega' ELSE 'NO ASIGNADO'  END) AS TipoDocumentoStr,");
            vSql.AppendLine("    Fact.StatusFactura,");
            vSql.AppendLine("    (CASE Fact.StatusFactura WHEN '2' THEN 'BORRADOR' WHEN '1' THEN 'ANULADO' ELSE 'EMITIDO' END) AS StatusFacturaStr,");
            vSql.AppendLine("    Fact.Fecha, ");
            vSql.AppendLine("    " + insSql.ToSqlValue(valCiudadCompania) + " AS CiudadCia,");
            vSql.AppendLine("    Fact.NumeroFacturaAfectada, ");
            vSql.AppendLine("    Fact.FechaDeVencimiento, ");
            vSql.AppendLine("    Fact.CodigoMoneda AS CodigoMonedaFact,");
            vSql.AppendLine("    (SELECT CTE_Moneda.Simbolo FROM CTE_Moneda WHERE CTE_Moneda.CodigoMoneda = Fact.CodigoMoneda) AS SimboloMonedaFact,");
            vSql.AppendLine("    MonedaDoc.NombreMoneda AS MonedaDocumento, ");
            vSql.AppendLine("    SttByCia.CodigoMonedaLocal, ");
            vSql.AppendLine("    (SELECT CTE_Moneda.Simbolo FROM CTE_Moneda WHERE CTE_Moneda.CodigoMoneda = SttByCia.CodigoMonedaLocal) AS SimboloMonedaLocal,");
            vSql.AppendLine("    SttByCia.CodigoMonedaExtranjera,");
            vSql.AppendLine("    (SELECT CTE_Moneda.Simbolo FROM CTE_Moneda WHERE CTE_Moneda.CodigoMoneda = SttByCia.CodigoMonedaExtranjera) AS SimboloMonedaExtranjera,");
            vSql.AppendLine("    ISNULL((SELECT CambioAMonedaLocal FROM Comun.Cambio WHERE CodigoMoneda = SttByCia.CodigoMonedaExtranjera AND FechaDeVigencia = Fact.Fecha), 1) AS CambioFechaDocumento, ");
            vSql.AppendLine("    Fact.CondicionesDePago, ");
            vSql.AppendLine("    Fact.FormaDePago, ");
            vSql.AppendLine("    (CASE Fact.FormaDePago WHEN '1' THEN 'CRÉDITO' ELSE 'CONTADO' END) AS FormaDePagoStr, ");
            vSql.AppendLine("	 Fact.NumeroPlanillaExportacion, ");
            #endregion Datos del Documento
            #region Datos del Cliente
            vSql.AppendLine("    Fact.CodigoCliente, ");
            vSql.AppendLine("    C.Nombre AS NombreCliente, ");
            vSql.AppendLine("    C.NumeroRIF AS RifCliente,");
            vSql.AppendLine("	 C.Email AS EmailCliente,");
            vSql.AppendLine("	 C.Direccion AS DireccionCliente,");
            vSql.AppendLine("	 C.Ciudad AS CiudadCliente,");
            vSql.AppendLine("	 C.Telefono AS TelefonoCliente,");
            vSql.AppendLine("	 C.Fax AS FaxCliente,");
            vSql.AppendLine("	 C.Contacto,");
            vSql.AppendLine("	 ISNULL(SttByCia.NombreCampoDefinibleCliente1, '') AS NombreCampoDef1Cliente,");
            vSql.AppendLine("	 ISNULL(C.CampoDefinible1, '') AS CampoDef1Cliente,");
            #endregion Datos del Cliente
            #region Datos de Despacho
            vSql.AppendLine("	 ISNULL(Fact.NoDirDespachoAimprimir, 0) AS NoDirDespachoAimprimir, ");
            vSql.AppendLine("	 ISNULL(DirDespacho.Direccion, '') AS DireccionDeDespacho,");
            vSql.AppendLine("    ISNULL(DirDespacho.Ciudad, '') AS CiudadDeDespacho,");
            vSql.AppendLine("    ISNULL(DirDespacho.ZonaPostal, '') AS ZonaPostaDeDespacho,");
            vSql.AppendLine("    ISNULL(DirDespacho.PersonaContacto, '') AS ContactoDeDespacho,");
            vSql.AppendLine("    " + insSql.ToSqlValue(valNombreOperador) + " AS NombreOperador, ");
            vSql.AppendLine("    V.Codigo AS CodigoVendedor,");
            vSql.AppendLine("	 V.Nombre AS NombreVendedor,");
            #endregion Datos de Despacho
            #region Datos Adicionales
            vSql.AppendLine("	 ISNULL(SttByCia.UsaCamposDefinibles, 'N') AS UsaCamposDefFact,");
            vSql.AppendLine("	 ISNULL(SttByCia.NombreCampoDefinible1, '') AS NombreCampoDef1,");
            vSql.AppendLine("	 ISNULL(SttByCia.NombreCampoDefinible2, '') AS NombreCampoDef2,");
            vSql.AppendLine("	 ISNULL(SttByCia.NombreCampoDefinible3, '') AS NombreCampoDef3,");
            vSql.AppendLine("	 ISNULL(SttByCia.NombreCampoDefinible4, '') AS NombreCampoDef4,");
            vSql.AppendLine("	 ISNULL(SttByCia.NombreCampoDefinible5, '') AS NombreCampoDef5,");
            vSql.AppendLine("	 ISNULL(SttByCia.NombreCampoDefinible6, '') AS NombreCampoDef6,");
            vSql.AppendLine("	 ISNULL(SttByCia.NombreCampoDefinible7, '') AS NombreCampoDef7,");
            vSql.AppendLine("	 ISNULL(SttByCia.NombreCampoDefinible8, '') AS NombreCampoDef8,");
            vSql.AppendLine("	 ISNULL(SttByCia.NombreCampoDefinible9, '') AS NombreCampoDef9,");
            vSql.AppendLine("	 ISNULL(SttByCia.NombreCampoDefinible10, '') AS NombreCampoDef10,");
            vSql.AppendLine("	 ISNULL(SttByCia.NombreCampoDefinible11, '') AS NombreCampoDef11,");
            vSql.AppendLine("	 ISNULL(SttByCia.NombreCampoDefinible12, '') AS NombreCampoDef12,");

            vSql.AppendLine("	 ISNULL(CamposDef.CampoDefinible1, '') AS CampoDefinible1,");
            vSql.AppendLine("    ISNULL(CamposDef.CampoDefinible2, '') AS CampoDefinible2,");
            vSql.AppendLine("    ISNULL(CamposDef.CampoDefinible3, '') AS CampoDefinible3,");
            vSql.AppendLine("    ISNULL(CamposDef.CampoDefinible4, '') AS CampoDefinible4,");
            vSql.AppendLine("    ISNULL(CamposDef.CampoDefinible5, '') AS CampoDefinible5,");
            vSql.AppendLine("    ISNULL(CamposDef.CampoDefinible6, '') AS CampoDefinible6,");
            vSql.AppendLine("    ISNULL(CamposDef.CampoDefinible7, '') AS CampoDefinible7,");
            vSql.AppendLine("    ISNULL(CamposDef.CampoDefinible8, '') AS CampoDefinible8,");
            vSql.AppendLine("    ISNULL(CamposDef.CampoDefinible9, '') AS CampoDefinible9,");
            vSql.AppendLine("    ISNULL(CamposDef.CampoDefinible10, '') AS CampoDefinible10,");
            vSql.AppendLine("    ISNULL(CamposDef.CampoDefinible11, '') AS CampoDefinible11,");
            vSql.AppendLine("    ISNULL(CamposDef.CampoDefinible12, '') AS CampoDefinible12,");
            #endregion Datos Adicionales
            #region GH Detail -> Renglón Factura
            vSql.AppendLine("    RF.ConsecutivoRenglon,");
            vSql.AppendLine("    RF.Articulo,");
            vSql.AppendLine("	 REPLACE(RF.Descripcion, 'total', 'tot..') AS Descripcion,");
            vSql.AppendLine("	 RF.Cantidad,");
            vSql.AppendLine("	 AI.UnidadDeVenta,");
            vSql.AppendLine("	 AI.UnidadDeVentaSecundaria,");
            vSql.AppendLine("	 RF.PrecioSinIVA,");
            vSql.AppendLine("	 RF.PorcentajeDescuento,");
            vSql.AppendLine("	 RF.PorcentajeAlicuota,");
            vSql.AppendLine("	 RF.TotalRenglon,");
            vSql.AppendLine("	 AI.TipoDeArticulo,");
            vSql.AppendLine("	 AI.TipoArticuloInv AS TipoMercancia,");
            vSql.AppendLine("	 (CASE WHEN RF.AlicuotaIVA = '0' THEN 'E' ELSE '' END) AS Exento,");
            vSql.AppendLine("	 ISNULL(SttByCia.NombreCampoDefinibleInventario1, '') AS NombreCampoDefArtInv1,");
            vSql.AppendLine("	 ISNULL(SttByCia.NombreCampoDefinibleInventario2, '') AS NombreCampoDefArtInv2,");
            vSql.AppendLine("	 ISNULL(SttByCia.NombreCampoDefinibleInventario3, '') AS NombreCampoDefArtInv3,");
            vSql.AppendLine("	 ISNULL(SttByCia.NombreCampoDefinibleInventario4, '') AS NombreCampoDefArtInv4,");
            vSql.AppendLine("	 ISNULL(SttByCia.NombreCampoDefinibleInventario5, '') AS NombreCampoDefArtInv5,");
            vSql.AppendLine("    AI.CampoDefinible1 AS CampoDefArtInv1,");
            vSql.AppendLine("	 AI.CampoDefinible2 AS CampoDefArtInv2,");
            vSql.AppendLine("	 AI.CampoDefinible3 AS CampoDefArtInv3,");
            vSql.AppendLine("	 AI.CampoDefinible4 AS CampoDefArtInv4,");
            vSql.AppendLine("	 AI.CampoDefinible5 AS CampoDefArtInv5,");
            vSql.AppendLine("	 ISNULL(SttByCia.UsaCamposExtrasEnRenglonFactura, 'N') AS UsaCamposExtrasRF,");
            vSql.AppendLine("	 ISNULL(RF.CampoExtraEnRenglonFactura1, '') AS CampoExtraEnRenglon1,");
            vSql.AppendLine("    ISNULL(RF.CampoExtraEnRenglonFactura2, '') AS CampoExtraEnRenglon2,");
            vSql.AppendLine("    ISNULL(RF.Serial, '') AS Serial,");
            vSql.AppendLine("    ISNULL(RF.Rollo, '') AS Rollo,");
            vSql.AppendLine("    ISNULL(LoteAI.CodigoLote, '') AS LoteDeInventario,");
            vSql.AppendLine("    ISNULL(LoteAI.FechaDeElaboracion, '') AS FechaElab,");
            vSql.AppendLine("    ISNULL(LoteAI.FechaDeVencimiento, '') AS FechaVenc,");
            #endregion GH Detail -> Renglón Factura
            #region Detail -> Detalle Producto Compuesto
            vSql.AppendLine("    ISNULL(ProdComp.CodigoArticulo, '') AS CodigoArticuloCompuesto,");
            vSql.AppendLine("    ISNULL(PCAI.Descripcion, '') AS DescripcionCompuesto,");
            vSql.AppendLine("    ISNULL(ProdComp.Cantidad, '') AS CantidadCompuesto,");
            vSql.AppendLine("    ISNULL(PCAI.UnidadDeVenta, '') AS UnidadDeVentaCompuesto,");
            vSql.AppendLine("    ISNULL(PCAI.UnidadDeVentaSecundaria, 0) AS UnidadDeVentaSecundariaCompuesto,");
            vSql.AppendLine("    ISNULL(CASE Fact.NivelDePrecio WHEN '3' THEN PCAI.PrecioSinIva4 WHEN '2' THEN PCAI.PrecioSinIva3 WHEN '1' THEN PCAI.PrecioSinIva2 ELSE PCAI.PrecioSinIva END, 0) AS PrecioUnitarioCompuesto,");
            vSql.AppendLine("    ISNULL(PCAI.CampoDefinible1, '') AS CampoDefArtInvCompuesto1,");
            vSql.AppendLine("    ISNULL(PCAI.CampoDefinible2, '') AS CampoDefArtInvCompuesto2,");
            vSql.AppendLine("    ISNULL(PCAI.CampoDefinible3, '') AS CampoDefArtInvCompuesto3,");
            vSql.AppendLine("    ISNULL(PCAI.CampoDefinible4, '') AS CampoDefArtInvCompuesto4,");
            vSql.AppendLine("    ISNULL(PCAI.CampoDefinible5, '') AS CampoDefArtInvCompuesto5,");
            #endregion Detail -> Detalle Producto Compuesto
            #region Subtotal por línea de producto
            vSql.AppendLine("    AI.LineaDeProducto,");
            #endregion Subtotal por línea de producto
            #region GFTotales
            vSql.AppendLine("	 ISNULL(NF1.Descripcion, '') + ' ' + ISNULL(NF2.Descripcion, '') AS NotasFinales,");
            #endregion GFTotales
            #region GFTitulos
            vSql.AppendLine("    REPLACE(ISNULL(Fact.Observaciones, ''), 'total', 'tot..') AS Observaciones,");
            #endregion GFTitulos
            #region ML
            vSql.AppendLine("	 (CASE WHEN Fact.CodigoMoneda = SttByCia.CodigoMonedaLocal THEN SttByCia.NombreMonedaExtranjera ELSE Fact.Moneda END) AS MonedaExtranjera,");
            vSql.AppendLine("    Fact.CambioABolivares AS CambioABolivares,");
            vSql.AppendLine("	 Fact.CambioMonedaCXC AS CambioCxC,");
            vSql.AppendLine("	 Fact.CambioMostrarTotalEnDivisas AS CambioTotalEnDivisas,");
            vSql.AppendLine("	 Fact.TotalRenglones,");
            vSql.AppendLine("	 Fact.PorcentajeDescuento1 AS PorcentajeDesc1,");
            vSql.AppendLine("	 Fact.PorcentajeDescuento2 AS PorcentajeDesc2,");
            vSql.AppendLine("	 Fact.MontoDescuento1,");
            vSql.AppendLine("	 Fact.MontoDescuento2,");
            vSql.AppendLine("	 Fact.TotalRenglones - (Fact.MontoDescuento1 - Fact.MontoDescuento2) AS SubTotal,");
            vSql.AppendLine("    Fact.TotalMontoExento,");
            vSql.AppendLine("	 Fact.PorcentajeAlicuota1,");
            vSql.AppendLine("	 Fact.PorcentajeAlicuota2,");
            vSql.AppendLine("	 Fact.PorcentajeAlicuota3,");
            vSql.AppendLine("	 Fact.MontoGravableAlicuota1,");
            vSql.AppendLine("	 Fact.MontoGravableAlicuota2,");
            vSql.AppendLine("	 Fact.MontoGravableAlicuota3,");
            vSql.AppendLine("	 Fact.MontoIVAAlicuota1,");
            vSql.AppendLine("	 Fact.MontoIVAAlicuota2,");
            vSql.AppendLine("	 Fact.MontoIVAAlicuota3,");
            vSql.AppendLine("	 MonedaDoc.Simbolo,");
            vSql.AppendLine("	 Fact.TotalFactura,");
            vSql.AppendLine("	 Fact.AlicuotaIGTF,");
            vSql.AppendLine("	 Fact.BaseImponibleIGTF,");
            vSql.AppendLine("	 (CASE WHEN Fact.CodigoMoneda = 'VED' THEN Fact.IGTFML ELSE Fact.IGTFME END) AS IGTF,");
            vSql.AppendLine("	 (Fact.TotalFactura + (CASE WHEN Fact.CodigoMoneda = 'VED' THEN Fact.IGTFML ELSE Fact.IGTFME END)) AS TotalFacturaMasIGTF,");
            vSql.AppendLine("    Fact.MontoDelAbono,");
            vSql.AppendLine("	 ((Fact.TotalFactura + (CASE WHEN Fact.CodigoMoneda = 'VED' THEN Fact.IGTFML ELSE Fact.IGTFME END)) -Fact.MontoDelAbono) AS TotalFacturaMenosAbono");
            #endregion ML
            #region FROM
            vSql.AppendLine();
            vSql.AppendLine("FROM");
            vSql.AppendLine("    Saw.NotaFinal AS NF1");
            vSql.AppendLine("    RIGHT JOIN Saw.NotaFinal AS NF2");
            vSql.AppendLine("    RIGHT JOIN factura AS Fact");
            vSql.AppendLine("    INNER JOIN renglonFactura AS RF ON Fact.ConsecutivoCompania = RF.ConsecutivoCompania");
            vSql.AppendLine("    AND Fact.Numero = RF.NumeroFactura");
            vSql.AppendLine("    AND Fact.TipoDeDocumento = RF.TipoDeDocumento");
            vSql.AppendLine("    INNER JOIN Cliente AS C ON Fact.ConsecutivoCompania = C.ConsecutivoCompania");
            vSql.AppendLine("    AND Fact.CodigoCliente = C.Codigo");
            vSql.AppendLine("    INNER JOIN Adm.Vendedor AS V ON Fact.ConsecutivoCompania = V.ConsecutivoCompania");
            vSql.AppendLine("    AND Fact.ConsecutivoVendedor = V.Consecutivo");
            vSql.AppendLine("    INNER JOIN ArticuloInventario AS AI ON RF.ConsecutivoCompania = AI.ConsecutivoCompania");
            vSql.AppendLine("    AND RF.Articulo = AI.Codigo ON NF2.CodigoDeLaNota = Fact.CodigoNota2");
            vSql.AppendLine("    AND NF2.ConsecutivoCompania = Fact.ConsecutivoCompania ON NF1.CodigoDeLaNota = Fact.CodigoNota1");
            vSql.AppendLine("    AND NF1.ConsecutivoCompania = Fact.ConsecutivoCompania");
            vSql.AppendLine("    LEFT JOIN Saw.LoteDeInventario AS LoteAI ON RF.LoteDeInventario = LoteAI.CodigoLote");
            vSql.AppendLine("    AND RF.Articulo = LoteAI.CodigoArticulo");
            vSql.AppendLine("    LEFT JOIN ArticuloInventario AS PCAI");
            vSql.AppendLine("    INNER JOIN ProductoCompuesto AS ProdComp ON PCAI.ConsecutivoCompania = ProdComp.ConsecutivoCompania");
            vSql.AppendLine("    AND PCAI.Codigo = ProdComp.CodigoConexionConElMaster");
            vSql.AppendLine("    AND PCAI.ConsecutivoCompania = ProdComp.ConsecutivoCompania");
            vSql.AppendLine("    AND PCAI.Codigo = ProdComp.CodigoConexionConElMaster");
            vSql.AppendLine("    AND PCAI.ConsecutivoCompania = ProdComp.ConsecutivoCompania");
            vSql.AppendLine("    AND PCAI.Codigo = ProdComp.CodigoConexionConElMaster");
            vSql.AppendLine("    AND PCAI.ConsecutivoCompania = ProdComp.ConsecutivoCompania");
            vSql.AppendLine("    AND PCAI.Codigo = ProdComp.CodigoConexionConElMaster ON AI.ConsecutivoCompania = ProdComp.ConsecutivoCompania");
            vSql.AppendLine("    AND AI.Codigo = ProdComp.CodigoConexionConElMaster");
            vSql.AppendLine("    AND AI.ConsecutivoCompania = ProdComp.ConsecutivoCompania");
            vSql.AppendLine("    AND AI.Codigo = ProdComp.CodigoConexionConElMaster");
            vSql.AppendLine("    AND AI.ConsecutivoCompania = ProdComp.ConsecutivoCompania");
            vSql.AppendLine("    AND AI.Codigo = ProdComp.CodigoConexionConElMaster");
            vSql.AppendLine("    AND AI.ConsecutivoCompania = ProdComp.ConsecutivoCompania");
            vSql.AppendLine("    AND AI.Codigo = ProdComp.CodigoConexionConElMaster");
            vSql.AppendLine("    LEFT JOIN DireccionDeDespacho AS DirDespacho ON Fact.NoDirDespachoAimprimir = DirDespacho.ConsecutivoDireccion");
            vSql.AppendLine("    AND Fact.ConsecutivoCompania = DirDespacho.ConsecutivoCompania");
            vSql.AppendLine("    AND Fact.CodigoCliente = DirDespacho.CodigoCliente");
            vSql.AppendLine("    LEFT JOIN camposDefFactura AS CamposDef ON Fact.ConsecutivoCompania = CamposDef.ConsecutivoCompania");
            vSql.AppendLine("    AND Fact.Numero = CamposDef.NumeroFactura");
            vSql.AppendLine("    AND Fact.TipoDeDocumento = CamposDef.TipoDeDocumento");
            vSql.AppendLine("    INNER JOIN CTE_Moneda MonedaDoc ON Fact.CodigoMoneda = MonedaDoc.CodigoMoneda");
            vSql.AppendLine("    LEFT JOIN CTE_SttByCia AS SttByCia ON Fact.ConsecutivoCompania = SttByCia.ConsecutivoCompania");
            #endregion FROM
            #region WHERE / ORDER BY
            vSql.AppendLine();
            vSql.AppendLine("WHERE (Fact.ConsecutivoCompania = " + insSql.ToSqlValue(valConsecutivoCompania) + ") AND (Fact.Numero = " + insSql.ToSqlValue(valNumeroDocumento) + ") AND (Fact.TipoDeDocumento = " + insSql.EnumToSqlValue((int)valTipoDocumento) + ") AND (Fact.Talonario = " + insSql.EnumToSqlValue((int)valTalonario) + ")");
            vSql.AppendLine("ORDER BY ");
            if (valImprimirFacturaConSubtotalesPorLineaDeProducto) {
                vSql.AppendLine("AI.LineaDeProducto, ");
            }
            if (valFormaDeOrdenarDetalleFactura == eFormaDeOrdenarDetalleFactura.PorCodigodeArticulo) {
                vSql.AppendLine("RF.Articulo ");
            } else if (valFormaDeOrdenarDetalleFactura == eFormaDeOrdenarDetalleFactura.PorDescripciondeArticulo) {
                vSql.AppendLine("RF.Descripcion");
            } else {
                vSql.AppendLine("RF.ConsecutivoRenglon");
            }
            #endregion WHERE
            return vSql.ToString();
        }
        #endregion //Metodos Generados

        #region Código Programador
        private string SqlCambioParaReportesDeFactura(bool valIsInMonedaLocal, bool valIsInTasaDelDia) {
            string vSqlCambio;
            if (valIsInMonedaLocal) {
                if (valIsInTasaDelDia) {
                    vSqlCambio = new Saw.Lib.clsLibSaw().CampoMontoPorTasaDeCambioSql("Factura.CambioAbolivares", "Factura.Moneda", "1", false, "");
                } else {
                    vSqlCambio = insSql.IIF("Factura.CambioABolivares IS NULL OR Factura.CambioABolivares = 0", "1", "Factura.CambioABolivares", true);
                }
            } else {
                vSqlCambio = "1";
            }
            return vSqlCambio;
        }

        private string CteArticuloInventarioSql() {
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine(";WITH CTE_ArticuloInventario(ConsecutivoCompania,Codigo,Descripcion,UnidadDeVenta,Serial,Rollo)");
            vSql.AppendLine("AS");
            vSql.AppendLine("(SELECT");
            vSql.AppendLine("ArticuloInventario.ConsecutivoCompania,");
            vSql.AppendLine("ArticuloInventario.Codigo,");
            vSql.AppendLine("ArticuloInventario.Descripcion,");
            vSql.AppendLine("ArticuloInventario.UnidadDeVenta,");
            vSql.AppendLine(insSql.IIF("ExistenciaPorGrupo.Serial IS NULL", insSql.ToSqlValue("0"), "ExistenciaPorGrupo.Serial", true) + " AS Serial,");
            vSql.AppendLine(insSql.IIF("ExistenciaPorGrupo.Rollo IS NULL", insSql.ToSqlValue("0"), "ExistenciaPorGrupo.Rollo", true) + " AS Rollo");

            vSql.AppendLine("FROM ArticuloInventario");
            vSql.AppendLine("LEFT OUTER JOIN ExistenciaPorGrupo");
            vSql.AppendLine("ON ArticuloInventario.ConsecutivoCompania = ExistenciaPorGrupo.ConsecutivoCompania");
            vSql.AppendLine("AND ArticuloInventario.CodigoGrupo = " + insSql.IIF("ExistenciaPorGrupo.CodigoGrupo = " + insSql.ToSqlValue("0"), insSql.ToSqlValue(""), "ExistenciaPorGrupo.CodigoGrupo", true));
            vSql.AppendLine("AND ArticuloInventario.Codigo = ExistenciaPorGrupo.CodigoArticulo)");

            return vSql.ToString();
        }

        private string SqlUsaContabilidad() {
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("LEFT JOIN CTE_Comprobante ON");
            vSql.AppendLine("(Factura.TipoDeDocumento + " + insSql.Char(9) + " + Factura.Numero) = CTE_Comprobante.NoDocumentoOrigen AND");
            vSql.AppendLine("Factura.ConsecutivoCompania = CTE_Comprobante.ConsecutivoCompania AND");
            vSql.AppendLine("Factura.Fecha = CTE_Comprobante.FechaComprobante");
            return vSql.ToString();
        }

        private string CteComprobantesSql(int valConsecutivoCompania, DateTime valFechaDesde, DateTime valFechaHasta) {
            string vSqlWhere = "";
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine(";WITH CTE_Comprobante(ConsecutivoCompania,NumeroComprobante,NoDocumentoOrigen,FechaComprobante)");
            vSql.AppendLine("AS");
            vSql.AppendLine("(SELECT");
            vSql.AppendLine("Periodo.ConsecutivoCompania,");
            vSql.AppendLine("Numero,");
            vSql.AppendLine("NoDocumentoOrigen,");
            vSql.AppendLine("Fecha");
            vSql.AppendLine("FROM");
            vSql.AppendLine("COMPROBANTE");
            vSql.AppendLine("INNER JOIN PERIODO ON");
            vSql.AppendLine("COMPROBANTE.ConsecutivoPeriodo = PERIODO.ConsecutivoPeriodo AND");
            vSql.AppendLine("COMPROBANTE.GeneradoPor = ':' AND");
            vSql.AppendLine("COMPROBANTE.Fecha BETWEEN PERIODO.FechaAperturaDelPeriodo AND PERIODO.FechaCierreDelPeriodo");
            vSqlWhere = insSql.SqlDateValueBetween(vSqlWhere, "Comprobante.Fecha", valFechaDesde, valFechaHasta);
            vSqlWhere = insSql.SqlIntValueWithAnd(vSqlWhere, "Periodo.ConsecutivoCompania", valConsecutivoCompania);
            vSqlWhere = insSql.WhereSql(vSqlWhere);
            vSql.Append(vSqlWhere + ")");
            return vSql.ToString();
        }
        #endregion //Código Programador

    } //End of class clsFacturaSql

} //End of namespace Galac.Adm.Brl.Venta