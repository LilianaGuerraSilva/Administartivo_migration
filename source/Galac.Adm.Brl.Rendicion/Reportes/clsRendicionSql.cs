using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using Galac.Adm.Ccl.CajaChica;
namespace Galac.Adm.Brl.CajaChica.Reportes {
    public class clsRendicionSql {
        #region Metodos Generados
        public string SqlReposicionesEntreFechas(int valConsecutivoCompania, DateTime valFechaInicial, DateTime valFechaFinal, bool valImprimeUna, string valCodigoCtaBancariaCajaChica) {
            StringBuilder vSql = new StringBuilder();
            QAdvSql vUtilSql = new QAdvSql("");
            string vSQLWhere = "";
            string[] vComparative = new string[] {"StatusRendicion = " + vUtilSql.ToSqlValue(eStatusRendicion.Anulada.GetHashCode().ToString()),""};
            string[] vResult = new string[] {vUtilSql.ToSqlValue("0"), "TotalGastos" };
            vSql.AppendLine("SELECT Numero, FechaApertura, FechaCierre, StatusRendicionStr, CodigoCuentaBancaria, ");
            vSql.AppendLine (vUtilSql.CaseIf(vComparative, vResult, "TotalGastos") + ", ");            
            vSql.AppendLine("CodigoCuentaBancaria, NumeroDocumento, CodigoCtaBancariaCajaChica, Descripcion, NombreCuentaBancaria, NombreCuentaBancariaCajaChica");
			vSql.AppendLine(" FROM Adm.Gv_Rendicion_B1");
			vSQLWhere = vUtilSql.SqlIntValueWithAnd(vSQLWhere, "Adm.Gv_Rendicion_B1.ConsecutivoCompania", valConsecutivoCompania);
            vSQLWhere = vUtilSql.SqlDateValueBetween(vSQLWhere, "Adm.Gv_Rendicion_B1.FechaCierre", valFechaInicial, valFechaFinal);
            if (valImprimeUna) {
                vSQLWhere = vUtilSql.SqlValueWithAnd(vSQLWhere, "Adm.Gv_Rendicion_B1.CodigoCtaBancariaCajaChica", valCodigoCtaBancariaCajaChica);
            }
			if (LibString.Len(vSQLWhere) > 0) {
				vSql.AppendLine(" WHERE " + vSQLWhere);
			}
            vSql.AppendLine(" ORDER BY CodigoCtaBancariaCajaChica");
			return vSql.ToString();
		}
        #endregion //Metodos Generados


    } //End of class clsRendicionSql

} //End of namespace Galac.Adm.Brl.CajaChica