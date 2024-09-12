using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
namespace Galac.Saw.Brl.Inventario.Reportes {
    public class clsLoteDeInventarioSql {
        #region Metodos Generados
		public string SqlArticulosPorVencer(int valConsecutivoCompania, string valLineaDeProducto, string valCodigoArticulo, int valDiasPorVencer) {
            StringBuilder vSql = new StringBuilder();
			string vSQLWhere = "";
			vSql.AppendLine(" FROM Saw.Gv_LoteDeInventario_B1");
			vSQLWhere = new QAdvSql("").SqlIntValueWithAnd(vSQLWhere, "Saw.Gv_LoteDeInventario_B1.ConsecutivoCompania", valConsecutivoCompania);
			if (LibString.Len(vSQLWhere) > 0) {
				vSql.AppendLine(" WHERE " + vSQLWhere);
			}
			return vSql.ToString();
		}
        #endregion //Metodos Generados


    } //End of class clsLoteDeInventarioSql

} //End of namespace Galac.Saw.Brl.Inventario

