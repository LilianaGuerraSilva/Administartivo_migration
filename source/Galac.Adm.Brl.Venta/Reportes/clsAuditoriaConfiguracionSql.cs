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
            vSQLWhere=insSql.SqlIntValueWithAnd(vSQLWhere, "adm.AuditoriaConfiguracion.ConsecutivoCompania", valConsecutivoCompania);
            vSQLWhere =insSql.SqlDateValueBetween(vSQLWhere, "adm.AuditoriaConfiguracion.FechaYHora", valFechaDesde, valFechaHasta);
            vSql.AppendLine("SELECT adm.AuditoriaConfiguracion.FechaYHora");
			vSql.AppendLine(", adm.AuditoriaConfiguracion.VersionPrograma");
			vSql.AppendLine(", adm.AuditoriaConfiguracion.Accion");
			vSql.AppendLine(", adm.AuditoriaConfiguracion.Motivo");
			vSql.AppendLine(", adm.AuditoriaConfiguracion.ConfiguracionOriginal");
			vSql.AppendLine(", adm.AuditoriaConfiguracion.ConfiguracionNueva");
			vSql.AppendLine(", adm.AuditoriaConfiguracion.NombreOperador");
			vSql.AppendLine(" FROM adm.AuditoriaConfiguracion");			
			if (LibString.Len(vSQLWhere) > 0) {
				vSql.AppendLine(" WHERE " + vSQLWhere);
			}
			return vSql.ToString();
		}
        #endregion //Metodos Generados
    } //End of class clsAuditoriaConfiguracionSql
} //End of namespace Galac.Saw.Brl.Venta

