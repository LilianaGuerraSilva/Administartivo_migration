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
		public string SqlAuditoriaConfiguracionDeCaja(int valConsecutivoCompania, DateTime valFechaDesde, DateTime valFechaHasta){
            StringBuilder vSql = new StringBuilder();
			string vSQLWhere = "";
			vSql.AppendLine("SELECT adm.Gv_AuditoriaConfiguracion_B1.FechaYHora");
			vSql.AppendLine(", adm.Gv_AuditoriaConfiguracion_B1.VersionPrograma");
			vSql.AppendLine(", adm.Gv_AuditoriaConfiguracion_B1.Accion");
			vSql.AppendLine(", adm.Gv_AuditoriaConfiguracion_B1.Motivo");
			vSql.AppendLine(", adm.Gv_AuditoriaConfiguracion_B1.ConfiguracionOriginal");
			vSql.AppendLine(", adm.Gv_AuditoriaConfiguracion_B1.ConfiguracionNueva");
			vSql.AppendLine(", adm.Gv_AuditoriaConfiguracion_B1.NombreOperador");
			vSql.AppendLine(" FROM adm.Gv_AuditoriaConfiguracion_B1");
			vSQLWhere = new QAdvSql("").SqlIntValueWithAnd(vSQLWhere, "adm.Gv_AuditoriaConfiguracion_B1.ConsecutivoCompania", valConsecutivoCompania);
			if (LibString.Len(vSQLWhere) > 0) {
				vSql.AppendLine(" WHERE " + vSQLWhere);
			}
			return vSql.ToString();
		}
        #endregion //Metodos Generados


    } //End of class clsAuditoriaConfiguracionSql

} //End of namespace Galac.Saw.Brl.Venta

