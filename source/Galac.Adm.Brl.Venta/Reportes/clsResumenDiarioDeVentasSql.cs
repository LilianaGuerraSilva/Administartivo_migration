using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.DefGen;

namespace Galac.Adm.Brl.Venta.Reportes {
    public class clsResumenDiarioDeVentasSql {
        private QAdvSql vSqlUtil = new QAdvSql("");

        #region Metodos Generados
		public string SqlResumenDiarioDeVentasEntreFechas(int valConsecutivoCompania, DateTime valFechaDesde, DateTime valFechaHasta, bool valAgruparPorMaquinaFiscal, string valConsecutivoMaquinaFiscal) {
            StringBuilder vSql = new StringBuilder();
			string vSQLWhere = "";
            vSql.AppendLine("SELECT");
            vSql.AppendLine("MaquinaFiscal.ConsecutivoMaquinaFiscal,");
            vSql.AppendLine("MaquinaFiscal.Descripcion,");
            vSql.AppendLine("MaquinaFiscal.NumeroRegistro,");
            vSql.AppendLine("Factura.Moneda,");
            vSql.AppendLine("Factura.Fecha,");
            vSql.AppendLine("Factura.NumeroDesde,");
            vSql.AppendLine("Factura.NumeroHasta,");
            vSql.AppendLine(GetCadenaCampoMontoSql("TotalBaseImponible") + " AS TotalBaseImponible,");
            vSql.AppendLine(GetCadenaCampoMontoSql("TotalMontoExento") + " AS TotalMontoExento,");
            vSql.AppendLine(GetCadenaCampoMontoSql("TotalIVA") + " AS TotalIVA,");
            vSql.AppendLine(GetCadenaCampoMontoSql("TotalFactura") + " AS TotalFactura,");

            vSql.AppendLine(GetCadenaCampoMontoSql("MontoIVAAlicuota1") + " AS MontoIVAAlicuota1,");
            vSql.AppendLine(GetCadenaCampoMontoSql("MontoIVAAlicuota2") + " AS MontoIVAAlicuota2,");
            vSql.AppendLine(GetCadenaCampoMontoSql("MontoIVAAlicuota3") + " AS MontoIVAAlicuota3,");
            vSql.AppendLine(GetCadenaCampoMontoSql("MontoGravableAlicuota1") + " AS MontoGravableAlicuota1,");
            vSql.AppendLine(GetCadenaCampoMontoSql("MontoGravableAlicuota2") + " AS MontoGravableAlicuota2,");
            vSql.AppendLine(GetCadenaCampoMontoSql("MontoGravableAlicuota3") + " AS MontoGravableAlicuota3");

            vSql.AppendLine("FROM");
            vSql.AppendLine("Factura");
            vSql.AppendLine("INNER JOIN MaquinaFiscal ON Factura.ConsecutivoCompania = MaquinaFiscal.ConsecutivoCompania");
            vSql.AppendLine("AND Factura.CodigoMaquinaRegistradora = MaquinaFiscal.ConsecutivoMaquinaFiscal");

            vSQLWhere = vSqlUtil.SqlIntValueWithAnd(vSQLWhere, "Factura.ConsecutivoCompania", valConsecutivoCompania);
            vSQLWhere = vSqlUtil.SqlDateValueBetween(vSQLWhere, "Factura.Fecha", valFechaDesde, valFechaHasta);
            vSQLWhere = vSqlUtil.SqlEnumValueWithAnd(vSQLWhere, "Factura.TipoDeDocumento", ( int ) Saw.Ccl.SttDef.eTipoDocumentoFactura.ResumenDiarioDeVentas);

            if (!LibString.IsNullOrEmpty(valConsecutivoMaquinaFiscal)) {
                vSQLWhere = vSqlUtil.SqlValueWithAnd(vSQLWhere, "MaquinaFiscal.ConsecutivoMaquinaFiscal", valConsecutivoMaquinaFiscal);
            }

            vSQLWhere = vSqlUtil.WhereSql(vSQLWhere);
            vSql.AppendLine(vSQLWhere);

            vSql.AppendLine("ORDER BY");

            if (valAgruparPorMaquinaFiscal) {
                vSql.AppendLine("MaquinaFiscal.ConsecutivoMaquinaFiscal,");
            }

            vSql.AppendLine("Factura.Moneda,");
            vSql.AppendLine("Factura.Fecha");

			return vSql.ToString();
		}
        #endregion //Metodos Generados

        #region Código Programador
        private string GetCadenaCampoMontoSql(string valCampoMonto) => vSqlUtil.IIF("Factura.StatusFactura = " + vSqlUtil.EnumToSqlValue(( int ) Ccl.Venta.eStatusFactura.Anulada), "0", "Factura." + valCampoMonto, true);
        #endregion //Código Programador

    } //End of class clsFacturaSql

} //End of namespace Galac.Adm.Brl.Venta

