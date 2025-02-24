using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
namespace Galac.Adm.Brl.Venta.Reportes {
    public class clsAuditoriaConfiguracionSql {
        #region Metodos Generados

        QAdvSql insSql;

		public clsAuditoriaConfiguracionSql() {
			insSql = new QAdvSql("");
		}

		public string SqlAuditoriaConfiguracionDeCaja(int valConsecutivoCompania, DateTime valFechaDesde, DateTime valFechaHasta) {
			StringBuilder vSql = new StringBuilder();
			string vSQLWhere = "";
            vSQLWhere=insSql.SqlIntValueWithAnd(vSQLWhere, "ConsecutivoCompania", valConsecutivoCompania);
			vSQLWhere += $" AND CAST(FechaYHora as date) >= CAST({insSql.ToSqlValue(valFechaDesde)} AS date) AND CAST(FechaYHora as date) <= CAST({insSql.ToSqlValue(valFechaHasta)} AS date)";
            vSql.AppendLine("SELECT FechaYHora");
			vSql.AppendLine(", VersionPrograma");
			vSql.AppendLine(", Accion");
			vSql.AppendLine(", Motivo");
			vSql.AppendLine(", ConfiguracionOriginal");
			vSql.AppendLine(", ConfiguracionNueva");
			vSql.AppendLine(", NombreOperador");
			vSql.AppendLine(" FROM adm.AuditoriaConfiguracion");			
			if (LibString.Len(vSQLWhere) > 0) {
				vSql.AppendLine(" WHERE " + vSQLWhere);
			}
			vSql.AppendLine(" ORDER BY FechaYHora, ConfiguracionOriginal, ConfiguracionNueva ASC");
            return vSql.ToString();
		}
        #endregion //Metodos Generados
    } //End of class clsAuditoriaConfiguracionSql
} //End of namespace Galac.Saw.Brl.Venta

