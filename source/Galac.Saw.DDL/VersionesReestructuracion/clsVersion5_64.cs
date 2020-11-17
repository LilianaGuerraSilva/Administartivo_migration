using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Dal;
using Galac.Comun.Ccl.TablasLey;

namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersion5_64 : clsVersionARestructurar {

        public clsVersion5_64(string valCurrentDataBaseName)
            : base(valCurrentDataBaseName) {
            _VersionDataBase = "5.64";
        }

        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
            CorregirTablaRetencionSiEsNecesario();
			AgregaCamposAPlanillaForma00030();
            DisposeConnectionNoTransaction();
            return true;
        }

        private void CorregirTablaRetencionSiEsNecesario() {
            QAdvSql vQAdvSQL = new QAdvSql("");
            StringBuilder vSqlSb = new StringBuilder();
            vSqlSb.AppendLine("Update Comun.TablaRetencion SET BaseImponible = 100");
            vSqlSb.AppendLine("WHERE Codigo = " + vQAdvSQL.ToSqlValue("HONMAN") + " AND TipoDePersona = " + vQAdvSQL.ToSqlValue("0"));
            vSqlSb.AppendLine("AND BaseImponible = 10 ");
            Execute(vSqlSb.ToString());
        }

        private void AgregaCamposAPlanillaForma00030() {
            if (!ColumnExists("dbo.planillaForma00030", "RetencionDescontadaEnExcesoPeriodosAnt")) {
                AddColumnCurrency("dbo.planillaForma00030", "RetencionDescontadaEnExcesoPeriodosAnt", "", 0);
            }
            if (!ColumnExists("dbo.planillaForma00030", "RetencionesDejadasDeDescontar")) {
                AddColumnCurrency("dbo.planillaForma00030", "RetencionesDejadasDeDescontar", "", 0);
            }
        }
    }
}
