using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.DefGen;
using Galac.Saw.Lib;

namespace Galac.Adm.Brl.GestionCompras.Reportes {
    public class clsCxPSql {
		private QAdvSql insSql;

		public clsCxPSql() {
			insSql = new QAdvSql("");
        }

		#region Metodos Generados
		public string SqlCuentasPorPagarEntreFechas(int valConsecutivoCompania){
            StringBuilder vSql = new StringBuilder();
			string vSQLWhere = "";
			vSQLWhere = "";
			vSql.AppendLine("SELECT dbo.Gv_CxP_B1.Numero");
			vSql.AppendLine(" FROM dbo.Gv_CxP_B1");
			//if (LibString.Len(txtNumero.Text) > 0) {
			//	vSQLWhere = new QAdvSql("").SqlValueWithAnd(vSQLWhere, "dbo.Gv_CxP_B1.Numero", txtNumero.Text);
			//}
			vSQLWhere = new QAdvSql("").SqlIntValueWithAnd(vSQLWhere, "dbo.Gv_CxP_B1.ConsecutivoCompania", valConsecutivoCompania);
			if (LibString.Len(vSQLWhere) > 0) {
				vSql.AppendLine(" WHERE " + vSQLWhere);
			}
			return vSql.ToString();
		}
        #endregion //Metodos Generados


    } //End of class clsCxPSql

} //End of namespace Galac..Brl.ComponenteNoEspecificado

