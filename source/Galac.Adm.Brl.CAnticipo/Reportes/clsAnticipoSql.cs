using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using Galac.Saw.Lib;
using Galac.Adm.Ccl.CAnticipo;

namespace Galac.Adm.Brl.CAnticipo.Reportes {
    public class clsAnticipoSql {
        #region Metodos Generados
		public string SqlAnticipoPorProveedorOCliente(int valConsecutivoCompania, eStatusAnticipo valStatusAnticipo, eCantidadAImprimir valCantidadAImprimir, string valCodigoClienteProveedor, bool valOrdenamientoClienteStatus, eMonedaDelInformeMM valMonedaDelInformeMM, bool valProveedorCliente){
            StringBuilder vSql = new StringBuilder();
			string vSQLWhere = "";
			vSql.AppendLine("SELECT dbo.Gv_Anticipo_B1.Fecha");
			vSql.AppendLine(", dbo.Gv_Anticipo_B1.Numero");
			vSql.AppendLine(", dbo.Gv_Anticipo_B1.NumeroCheque");
			vSql.AppendLine(", MontoAnulado");
			vSql.AppendLine(", dbo.Gv_Anticipo_B1.MontoTotal");
			vSql.AppendLine(", dbo.Gv_Anticipo_B1.MontoUsado");
			vSql.AppendLine(", dbo.Gv_Anticipo_B1.MontoDevuelto");
			vSql.AppendLine(", DifDevolucion");
			vSql.AppendLine(" FROM dbo.Gv_Anticipo_B1");
			vSQLWhere = new QAdvSql("").SqlIntValueWithAnd(vSQLWhere, "dbo.Gv_Anticipo_B1.ConsecutivoCompania", valConsecutivoCompania);
			if (LibString.Len(vSQLWhere) > 0) {
				vSql.AppendLine(" WHERE " + vSQLWhere);
			}
			return vSql.ToString();
		}
        #endregion //Metodos Generados


    } //End of class clsAnticipoSql

} //End of namespace Galac.Adm.Brl.CAnticipo

