using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Base;

namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersion5_63 : clsVersionARestructurar {

        public clsVersion5_63(string valCurrentDataBaseName)
            : base(valCurrentDataBaseName) {
            _VersionDataBase = "5.63";
        }

        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
            RecrearParametrosMsdeManagerSiEsNecesario();
            CrearCamposManejoDeAnticipoEnCobroDirecto();
            DisposeConnectionNoTransaction();
            return true;
        }

        private void RecrearParametrosMsdeManagerSiEsNecesario() {
            if (IsExpressEdition()) {
                if (!TableExists("dbo.ParametrosMsdeManager")) {
                    Execute(SqlCreateTableParametrosMsdeManager());
                    if (TableExists("dbo.ParametrosMsdeManager")) {
                        Execute(SqlInsertDefaultRecordToParametrosMsdeManager());
                    }
                }
            }
        }

        private bool IsExpressEdition() {
            return (LibString.S1IsInS2("Express", new LibGalac.Aos.Dal.LibServiceInfo().ServerEditionInfo));
        }

        private string SqlCreateTableParametrosMsdeManager() {
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine(InsSql.CreateTable("ParametrosMsdeManager", "dbo") + " ( ");
            vSql.AppendLine("SecuencialInterno" + InsSql.NumericTypeForDb(10, 0) + " CONSTRAINT nnParametrosMsdeManagerSecuencial NOT NULL, ");
            vSql.AppendLine("ReestructuracionAutomatica" + InsSql.CharTypeForDb(1) + " CONSTRAINT nnParametrosMsdeManagerReestructu NOT NULL, ");
            vSql.AppendLine("TablasCreadas" + InsSql.CharTypeForDb(1) + " CONSTRAINT nnParametrosMsdeManagerTablasCrea NOT NULL, ");
            vSql.AppendLine("fldTimeStamp" + InsSql.TimeStampTypeForDb());
            vSql.AppendLine("CONSTRAINT p_ParametrosMsdeManager PRIMARY KEY CLUSTERED (SecuencialInterno ASC)");
            vSql.AppendLine(")");
            return vSql.ToString();
        }

        private string SqlInsertDefaultRecordToParametrosMsdeManager() {
            StringBuilder vSql = new StringBuilder();
            vSql.Append(" INSERT INTO dbo.ParametrosMsdeManager");
            vSql.Append(" (SecuencialInterno, ReestructuracionAutomatica, TablasCreadas)");
            vSql.Append(" VALUES (1, 'S', 'S') ");
            return vSql.ToString();
        }

        private void CrearCamposManejoDeAnticipoEnCobroDirecto() {
            if (!ColumnExists("Anticipo", "AsociarAnticipoACaja")) {
                AddColumnBoolean("Anticipo", "AsociarAnticipoACaja", "AsociarAnt NOT NULL", false);
            }
            if (!ColumnExists("Anticipo", "ConsecutivoCaja")) {
                AddColumnInteger("Anticipo", "ConsecutivoCaja", "");
            }
        }

    }
}
