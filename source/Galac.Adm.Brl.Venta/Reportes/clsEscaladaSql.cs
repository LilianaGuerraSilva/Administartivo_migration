using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using Galac.Adm.Ccl.Venta;
using Galac.Saw.Ccl.Cliente;
using System.Reflection;
namespace Galac.Adm.Brl.Venta.Reportes {
    public class clsEscaladaSql {
        private QAdvSql insSql = new QAdvSql("");
        #region Metodos Generados

        public string SqlFacturacionEntreFechasVerificacion(int valConsecutivoCompania, DateTime valFechaDesde, DateTime valFechaHasta) {
            StringBuilder vSql = new StringBuilder();
            string vSQLWhere = "";


            vSql.AppendLine("SET DATEFORMAT 'DMY'; ");
            vSql.AppendLine("WITH CTE_Factura AS( ");
            vSql.AppendLine("SELECT HASHBYTES('SHA2_256', ");
            vSql.AppendLine("CONCAT(CASE WHEN TipoDeDocumento = '5' OR TipoDeDocumento = '7'");
            vSql.AppendLine("THEN factura.Numero ELSE ");
            vSql.AppendLine("factura.NumeroComprobanteFiscal END,");
            vSql.AppendLine("TipoDeDocumento, Fecha, HoraModificacion, ");
            vSql.AppendLine("Cliente.NumeroRIF, ");
            vSql.AppendLine("NumeroControl, MontoGravableAlicuota1, MontoGravableAlicuota2, MontoGravableAlicuota3, ");
            vSql.AppendLine("MontoIVAAlicuota1, MontoIVAAlicuota2, MontoIVAAlicuota3,");
            vSql.AppendLine("TotalMontoExento,");
            vSql.AppendLine("TotalRenglones, TotalBaseImponible, TotalIVA, TotalFactura");
            vSql.AppendLine(")) AS Verificacion, Fecha, Numero, TipoDeDocumento, HoraModificacion AS 'Hora', factura.ConsecutivoCompania ");
            vSql.AppendLine("FROM factura INNER JOIN Cliente ON factura.ConsecutivoCompania = Cliente.ConsecutivoCompania ");
            vSql.AppendLine("AND factura.CodigoCliente = Cliente.Codigo ");
            vSql.AppendLine("WHERE  StatusFactura = '0' ");
            vSql.AppendLine("AND TipoDeDocumento <> '3')");
            vSql.AppendLine(", CTE_Verificacion AS(");

            vSql.AppendLine("SELECT");
            vSql.AppendLine("Fecha = ISNULL(Fecha, Escalada.Escalada32), ");
            vSql.AppendLine("Numero = ISNULL(CTE_Factura.Numero, SUBSTRING(Escalada.Escalada73, CHARINDEX('##', Escalada.Escalada73) + 2, CHARINDEX('@@', Escalada.Escalada73) - CHARINDEX('##', Escalada.Escalada73) - 3)), ");
            vSql.AppendLine("Hora = ISNULL(Hora, '--'), ");
            vSql.AppendLine("TipoDeDocumento = CASE ISNULL(CTE_Factura.TipoDeDocumento, SUBSTRING(Escalada.Escalada73, CHARINDEX('@@', Escalada.Escalada73) - 1, 1)) ");
            vSql.AppendLine("WHEN '0' THEN 'Factura' ");            
            vSql.AppendLine("WHEN '1' THEN 'Nota de Crédito' ");
            vSql.AppendLine("WHEN '2' THEN 'Nota de Débito' ");
            vSql.AppendLine("WHEN '5' THEN 'Comprobante Fiscal' ");
            vSql.AppendLine("WHEN '7' THEN 'Nota de Crédito Comprobante Fiscal' ");
            vSql.AppendLine("ELSE 'No Definido' END, ");

            vSql.AppendLine("Escalada.Escalada41 AS 'ConsecutivoCompania', Escalada.Escalada24, ");
            vSql.AppendLine("CASE WHEN(CTE_Factura.Verificacion = Escalada.Escalada100) THEN 'CORRECTO' ELSE ");
            vSql.AppendLine("CASE WHEN(CTE_Factura.Verificacion IS NULL) THEN 'FALTA REGISTRO' ELSE 'NO COINCIDE' END ");
            vSql.AppendLine("END AS TextoValidacion ");

            vSql.AppendLine("FROM Escalada LEFT JOIN CTE_Factura ON Escalada.Escalada41 = CTE_Factura.ConsecutivoCompania ");
            vSql.AppendLine("AND CONCAT(CTE_Factura.Numero,CTE_Factura.TipoDeDocumento) = SUBSTRING(Escalada.Escalada73, CHARINDEX('##', Escalada.Escalada73) + 2, CHARINDEX('@@', Escalada.Escalada73) - CHARINDEX('##', Escalada.Escalada73) - 2 ) ");
            
            vSQLWhere = insSql.SqlIntValueWithAnd(vSQLWhere, "Escalada.Escalada41", valConsecutivoCompania);
            vSQLWhere = insSql.SqlDateValueBetween(vSQLWhere, "Escalada.Escalada32", valFechaDesde, valFechaHasta);

            //vSQLWhere = vSQLWhere + " AND (CTE_Factura.Verificacion <> Escalada.Escalada100) ";
            //vSQLWhere = insSql.SqlValueWithOperators(vSQLWhere, "CTE_Factura.Verificacion", "Escalada.Escalada100", true, "AND", "<>");

            //vSQLWhere = insSql.SqlDateValueBetween(vSQLWhere, "Factura.Fecha", valFechaDesde, valFechaHasta);
            //vSQLWhere = insSql.SqlEnumValueWithOperators(vSQLWhere, "Factura.TipoDeDocumento", (int)Saw.Ccl.SttDef.eTipoDocumentoFactura.NotaEntrega, "AND", "<>");

            vSQLWhere = insSql.WhereSql(vSQLWhere);
            vSql.AppendLine(vSQLWhere);

            vSql.AppendLine(") SELECT * FROM CTE_Verificacion ");
            vSql.AppendLine("WHERE CTE_Verificacion.TextoValidacion <> 'CORRECTO' ");
            vSql.AppendLine("ORDER BY CTE_Verificacion.Fecha, Numero ");

            return vSql.ToString();
        }
        #endregion //Metodos Generados


    } //End of class clsEscaladaSql

} //End of namespace Galac.Adm.Brl.Venta

